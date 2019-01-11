using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Leadtools;
//using Leadtools.Ocr;

namespace bangna_hospital.gui
{
    public partial class FrmScanNew : Form
    {
        BangnaControl bc;
        MainMenu menu;
        C1FlexGrid grf;
        C1DockingTab tcDtr;
        C1DockingTabPage tabScan;
        C1DockingTabPage[] tabPage1;
        Font fEdit, fEditB;
        //private IOcrEngine _ocrEngine;

        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        ArrayList array1,arrayImg;
        Timer timer1;
        public FrmScanNew(BangnaControl bc, MainMenu m)
        {
            InitializeComponent();
            this.bc = bc;
            menu = m;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            tcDtr = new C1DockingTab();
            tcDtr.Dock = System.Windows.Forms.DockStyle.Fill;
            tcDtr.Location = new System.Drawing.Point(0, 266);
            tcDtr.Name = "c1DockingTab1";
            tcDtr.Size = new System.Drawing.Size(669, 200);
            tcDtr.TabIndex = 0;
            tcDtr.TabsSpacing = 5;
            panel1.Controls.Add(tcDtr);
            //theme1.SetTheme(tcDtr, "Office2010Blue");
            tabScan = new C1DockingTabPage();
            tabScan.Location = new System.Drawing.Point(1, 24);
            tabScan.Name = "c1DockingTabPage1";
            tabScan.Size = new System.Drawing.Size(667, 175);
            tabScan.TabIndex = 0;
            tabScan.Text = "PageScan";
            tabScan.Name = "Page Scan";
            tcDtr.Controls.Add(tabScan);
            txtAnDate.Left = txtVisitDate.Left;
            txtAnDate.Top = txtVisitDate.Top;

            bc.bcDB.dgsDB.setCboBsp(cboDgs, "");
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(-1);
            txtVisitDate.Value = dt.Year + "-" + dt.ToString("MM-dd");
            array1 = new ArrayList();
            arrayImg = new ArrayList();
            timer1 = new Timer();
            int chk = 0;
            int.TryParse(bc.iniC.timerImgScanNew, out chk);
            timer1.Interval = chk;
            timer1.Enabled = true;
            timer1.Tick += Timer1_Tick;
            timer1.Stop();
            //_ocrEngine = OcrEngineManager.CreateEngine(OcrEngineType.LEAD, false);

            theme1.Theme = bc.iniC.themeApplication;
            theme1.SetTheme(sb1, "BeigeOne");
            theme1.SetTheme(groupBox1, theme1.Theme);
            theme1.SetTheme(panel1, theme1.Theme);
            theme1.SetTheme(tcDtr, theme1.Theme);
            foreach (Control con in groupBox1.Controls)
            {
                if(con is ProgressBar)
                {
                    
                }
                else
                {
                    theme1.SetTheme(con, theme1.Theme);
                }
                
            }
            foreach (Control con in panel1.Controls)
            {
                //theme1.SetTheme(con, theme1.Theme);
            }
            
            btnOpen.Click += BtnOpen_Click;
            btnHn.Click += BtnHn_Click;

            sb1.Text = "aaaaaaaaaa";
            initGrf();
            setGrf();
            //pB1.Hide();
            int i = 0;
            String idOld = "";
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    C1DockingTabPage tabPage = new C1DockingTabPage();
            //    tabPage.Location = new System.Drawing.Point(1, 24);
            //    tabPage.Size = new System.Drawing.Size(667, 175);
                
            //    tabPage.TabIndex = 0;
            //    tabPage.Text = "Page" + dgs.doc_group_name;
            //    tabPage.Name = " " + dgs.doc_group_id+"  ";
            //    tcDtr.Controls.Add(tabPage);
            //    i++;
            //    String dgsid = "";
            //    dgsid = bc.bcDB.dgssDB.getDgsIdDgss(dgss.doc_group_sub_name);
            //    if (!dgsid.Equals(idOld))
            //    {

            //    }
            //}
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
                    tabPage.Text = " " + name+"  ";
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
                            tabPage2.Name = "tab"+dgsss.doc_group_sub_id;
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
                            tabPage2.Controls.Add(grf);
                        }
                    }
                }
                else
                {

                }
            }
        }

        private void BtnHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            String[] an = bc.sPtt.an.Split('/');
            //if (an.Length > 1)
            //{
            //    txtAN.Value = an[0];
            //    txtAnCnt.Value = an[1];
            //}
            //else
            //{
                txtAN.Value = bc.sPtt.an;
                txtAnCnt.Value = "";
            //}
            txtHn.Value = bc.sPtt.Hn;
            txtName.Value = bc.sPtt.Name;
            txtVN.Value = bc.sPtt.vn;
            txtVisitDate.Value = bc.sPtt.visitDate;
            txtPreNo.Value = bc.sPtt.preno;
            
            txtAnDate.Value = bc.sPtt.anDate;
            chkIPD.Checked = bc.sPtt.statusIPD.Equals("I") ? true : false;
            
            if (chkIPD.Checked)
            {
                txtVisitDate.Hide();
                txtAnDate.Show();
                label6.Text = "AN Date :";
            }
            else
            {
                txtVisitDate.Show();
                txtAnDate.Hide();
                label6.Text = "Visit Date :";
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //bool exists = System.IO.Directory.Exists(bc.iniC.pathImgScanNew);
            DirectoryInfo folderImg = null;
            if (!Directory.Exists(bc.iniC.pathImageScan))
                folderImg = Directory.CreateDirectory(bc.iniC.pathImageScan);
            setImage1(true);
            //String[] Files = Directory.GetFiles(bc.iniC.pathImgScanNew,"*.*", SearchOption.AllDirectories);
            //if (Files.Length > 0)
            //{
            //    setImage(Files);
            //    timer1.Stop();
            //}
        }
        private void setImage1(Boolean flagStop)
        {
            String[] Files = Directory.GetFiles(bc.iniC.pathImageScan, "*.*", SearchOption.AllDirectories);
            if (Files.Length > 0)
            {
                setGrf();
                setImage(Files);
                if (flagStop)
                {
                    timer1.Stop();
                }
            }
        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.BMP;*.JPG;*.Jepg;*.Png;*.GIF)|*.BMP;*.JPG;*.Jepg;*.Png;*.GIF|Pdf Files|*.pdf|All files (*.*)|*.*";
            ofd.Multiselect = true;
            ofd.Title = "My Image Browser";
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                String[] file1 = ofd.FileNames;
                setImage(file1);
            }
            
        }
        private void setImage(String[] file1)
        {
            txtVN.Hide();
            btnVn.Hide();
            label3.Hide();
            txtAN.Hide();
            txtAnCnt.Hide();
            chkIPD.Hide();
            label6.Hide();
            txtVisitDate.Hide();
            txtAnDate.Hide();
            txtPreNo.Hide();

            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(113, 36);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            groupBox1.Controls.Add(pB1);
            pB1.Show();
            int i = 1, j = 1, row = grf.Rows.Count;
            grf.Rows.Add();
            row = grf.Rows.Count;
            String re = "";
            array1.Clear();
            try
            {
                pB1.Value = 0;
                pB1.Minimum = 0;
                pB1.Maximum = file1.Length;
                foreach (String file in file1)
                {
                    try
                    {
                        Image loadedImage, resizedImage;
                        String[] sur = file.Split('.');
                        String ex = "";
                        if (sur.Length == 2)
                        {
                            ex = sur[1];
                        }

                        if (!ex.Equals("pdf"))
                        {
                            loadedImage = Image.FromFile(file);
                            //byte[] buff = System.IO.File.ReadAllBytes(file);
                            //System.IO.MemoryStream ms = new System.IO.MemoryStream(buff);
                            //MemoryStream stream = new MemoryStream(buff);

                            //loadedImage.Save(stream, ImageFormat.Jpeg);
                            //loadedImage.Dispose();
                            //loadedImage = Image.FromStream(stream);
                            int originalWidth = 0;
                            originalWidth = loadedImage.Width;
                            int newWidth = 280;
                            resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                            arrayImg.Add(file+",");
                        }
                        else
                        {
                            resizedImage = Resources.pdf_symbol_80_2;
                        }
                        if (j > 4)
                        {
                            grf.Rows.Add();
                            row = grf.Rows.Count;
                            j = 1;
                            i++;
                        }
                        //grf.Cols[colPic1].ImageAndText = true;
                        //grf.Cols[colPic2].ImageAndText = true;
                        //grf.Cols[colPic3].ImageAndText = true;
                        //grf.Cols[colPic4].ImageAndText = true;
                        int hei = grf.Rows.DefaultSize;

                        //grf[row - 1, colDay2PathPic] = file;
                        //grfDay2Img[row - 1, colBtn] = "send";
                        array1.Add(i + "," + j + ",*" + file);
                        if (j == 1)
                            grf[i, colPic1] = resizedImage;
                        else if (j == 2)
                            grf[i, colPic2] = resizedImage;
                        else if (j == 3)
                            grf[i, colPic3] = resizedImage;
                        else if (j == 4)
                            grf[i, colPic4] = resizedImage;
                        j++;
                        pB1.Value++;
                        //resizedImage.Dispose();
                    }
                    catch (Exception ex)
                    {
                        re = ex.Message;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error"+ex.Message, "");
            }
            
            //grf.AutoSizeCols();
            grf.AutoSizeRows();
            grf.Rows[0].Visible = false;
            grf.Cols[0].Visible = false;
            pB1.Dispose();
            txtVN.Show();
            btnVn.Show();
            label3.Show();
            txtAN.Show();
            txtAnCnt.Show();
            chkIPD.Show();
            label6.Show();
            if (chkIPD.Checked)
            {
                txtVisitDate.Hide();
                txtAnDate.Show();
            }
            else
            {
                txtVisitDate.Show();
                txtAnDate.Hide();
            }
            txtPreNo.Show();
        }
        private void initGrf()
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            //grf.AfterRowColChange += Grf_AfterRowColChange;
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabScan.Controls.Add(grf);
            grf.Rows[0].Visible = false;
            grf.Cols[0].Visible = false;
            //theme1.SetTheme(grf, "Office2010Blue");

        }
        private void setGrf()
        {
            grf.Clear();
            grf.Rows.Count = 1;
            grf.Cols.Count = 5;
            Column colpic1 = grf.Cols[colPic1];
            colpic1.DataType = typeof(Image);
            Column colpic2 = grf.Cols[colPic2];
            colpic2.DataType = typeof(Image);
            Column colpic3 = grf.Cols[colPic3];
            colpic3.DataType = typeof(Image);
            Column colpic4 = grf.Cols[colPic4];
            colpic4.DataType = typeof(Image);
            grf.Cols[colPic1].Width = 310;
            grf.Cols[colPic2].Width = 310;
            grf.Cols[colPic3].Width = 310;
            grf.Cols[colPic4].Width = 310;
            grf.ShowCursor = true;
            grf.Cols[colPic1].ImageAndText = true;
            grf.Cols[colPic2].ImageAndText = true;
            grf.Cols[colPic3].ImageAndText = true;
            grf.Cols[colPic4].ImageAndText = true;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("&แก้ไข Image", new EventHandler(ContextMenu_edit));
            menuGw.MenuItems.Add("&Rotate Image", new EventHandler(ContextMenu_retate));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
                //menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image ["+dgs.doc_group_name+"]", new EventHandler(ContextMenu_upload));
            String idOld = "";
            //if (lDgss.Count <= 0) getlBsp();
            if (bc.bcDB.dgssDB.lDgss.Count <= 0) bc.bcDB.dgssDB.getlBsp();
            foreach(DocGroupSubScan dgss in bc.bcDB.dgssDB.lDgss)
            {
                String dgsid = "";
                dgsid = bc.bcDB.dgssDB.getDgsIdDgss(dgss.doc_group_sub_name);
                if (!dgsid.Equals(idOld))
                {
                    idOld = dgsid;
                    String name = "";
                    name = bc.bcDB.dgsDB.getNameDgs(dgss.doc_group_id);
                    MenuItem addDevice = new MenuItem("[" + name + "]");
                    menuGw.MenuItems.Add(addDevice);
                    foreach (DocGroupSubScan dgsss in bc.bcDB.dgssDB.lDgss)
                    {
                        if (dgsss.doc_group_id.Equals(dgss.doc_group_id))
                        {
                            addDevice.MenuItems.Add(new MenuItem(dgsss.doc_group_sub_name, new EventHandler(ContextMenu_upload)));

                        }
                    }
                }
                else
                {
                    
                }
            }
                
            //addDevice.MenuItems.Add("", new EventHandler(ContextMenu_upload));
            //menuGw.MenuItems.Add(addDevice);
            //}
            grf.ContextMenu = menuGw;

            //row1[colVSE2] = row[ic.ivfDB.pApmDB.pApm.e2].ToString().Equals("1") ? imgCorr : imgTran;

        }
        //private void Grf_AfterRowColChange(object sender, RangeEventArgs e)
        //{
        //    //throw new NotImplementedException();

        //}
        private String searchInArray()
        {
            String dgs = "", name = "";
            int i = 0;
            Boolean chk = false;
            foreach (String aa in array1)
            {
                i++;
                if (aa.IndexOf(grf.Row + "," + grf.Col) >= 0)
                {
                    name = array1[i - 1].ToString();
                    chk = true;
                    break;
                }
            }
            return name;
        }
        private String addInArrayImg(String filename, String dgssid)
        {
            String dgs = "", name = "";
            int i = 0;
            Boolean chk = false;
            foreach (String aa in arrayImg)
            {
                i++;
                if (aa.IndexOf(filename) >= 0)
                {
                    if (aa.Equals(filename + ","))
                    {
                        arrayImg.Remove(aa);
                        arrayImg.Add(filename + ","+ dgssid);
                    }
                    else
                    {
                        String[] aaa = aa.Split(',');
                        if (aaa.Length > 1)
                        {
                            String filename1 = "", dgssidold = "";
                            filename1 = aaa[0];
                            dgssidold = aaa[1];
                            arrayImg.Remove(aa);
                            arrayImg.Add(aa + "," + dgssid);
                            foreach (Control con in this.Controls)
                            {
                                if (con is Panel)
                                {
                                    foreach (Control conp in con.Controls)
                                    {
                                        if (conp is C1DockingTab)
                                        {
                                            foreach (Control cond in conp.Controls)
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
                                                                            if (congd1.Name.Equals(dgssidold))
                                                                            {
                                                                                C1FlexGrid grf1;
                                                                                grf1 = (C1FlexGrid)congd1;
                                                                                int j = 0;
                                                                                foreach(Row rowa in grf1.Rows)
                                                                                {
                                                                                    String namerow = "";
                                                                                    if(rowa[colPic2] != null)
                                                                                    {
                                                                                        namerow = rowa[colPic2].ToString();
                                                                                        if (namerow.Equals(filename1))
                                                                                        {
                                                                                            grf1.RemoveItem(j);
                                                                                        }
                                                                                    }
                                                                                    
                                                                                    j++;
                                                                                }
                                                                                grf1.AutoSizeRows();
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
                            }
                        }
                    }
                    break;
                }
            }
            return name;
        }
        private void ContextMenu_retate(object sender, System.EventArgs e)
        {
            String dgs = "", filename = "", id = "";
            filename = searchInArray();
            try
            {
                filename = filename.Substring(filename.IndexOf('*') + 1);
                Image img = Image.FromFile(filename);
                Image resizedImage;
                int originalWidth = img.Width;
                int newWidth = 280;
                resizedImage = img.GetThumbnailImage(newWidth, (newWidth * img.Height) / originalWidth, null, IntPtr.Zero);
                resizedImage = bc.RotateImage(resizedImage);
                grf[grf.Row, grf.Col] = resizedImage;
                grf.AutoSizeCols();
                grf.AutoSizeRows();
            }
            catch (Exception ex)
            {

            }
        }
        private void ContextMenu_upload(object sender, System.EventArgs e)
        {
            String dgs = "", filename = "", id = "";
            filename = searchInArray();
            if (txtHn.Text.Equals(""))
            {
                MessageBox.Show("กรุณาป้อน HN", "");
                return;
            }
            try
            {
                filename = filename.Substring(filename.IndexOf('*') + 1);
                //String[] ext = filename.Split('.');
                string ext = Path.GetExtension(filename);
                String dgssname = "", dgssid="";
                dgssname = ((MenuItem)sender).Text;
                dgssid = bc.bcDB.dgssDB.getIdDgss(dgssname);
                DocGroupSubScan dgss = new DocGroupSubScan();
                dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
                if (!dgss.doc_group_sub_id.Equals(""))
                {
                    //dgs = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
                    int row = 0, col = 0;
                    row = grf.Row;
                    col = grf.Col;
                    DocScan dsc = new DocScan();
                    dsc.active = "1";
                    dsc.doc_scan_id = "";
                    dsc.doc_group_id = dgss.doc_group_id;
                    dsc.hn = txtHn.Text;
                    dsc.vn = txtVN.Text;
                    dsc.an = txtAN.Text;
                    dsc.visit_date = bc.datetoDB(txtVisitDate.Text);
                    //if (!txtVN.Text.Equals(""))
                    //{
                    //    dsc.row_no = bc.bcDB.dscDB.selectRowNoByHnVn(txtHn.Text, txtVN.Text, dgs);
                    //}
                    //else
                    //{
                    //    dsc.row_no = bc.bcDB.dscDB.selectRowNoByHn(txtHn.Text, dgs);
                    //}
                    dsc.host_ftp = bc.iniC.hostFTP;
                    //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                    dsc.image_path = ext;
                    dsc.doc_group_sub_id = dgssid;
                    dsc.pre_no = txtPreNo.Text;
                    dsc.an = txtAN.Text;
                    DateTime dt = new DateTime();

                    dsc.an_date = (DateTime.TryParse(txtAnDate.Text, out dt)) ? bc.datetoDB(txtAnDate.Text) : "";
                    if (dsc.an_date.Equals("1-01-01"))
                    {
                        dsc.an_date = "";
                    }
                    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
                    String re = bc.bcDB.dscDB.insertDocScan(dsc, bc.userId);
                    dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + re + ext;
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    ftp.createDirectory(txtHn.Text);
                    ftp.delete(dsc.image_path);
                    ftp.upload(dsc.image_path, filename);
                    foreach(Control con in this.Controls)
                    {
                        if(con is Panel)
                        {
                            foreach (Control conp in con.Controls)
                            {
                                if (conp is C1DockingTab)
                                {
                                    foreach (Control cond in conp.Controls)
                                    {
                                        if(cond is C1DockingTabPage)
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
                                                                    if (congd1.Name.Equals(dsc.doc_group_sub_id))
                                                                    {
                                                                        String aa = "";
                                                                        C1FlexGrid grf1;
                                                                        grf1 = (C1FlexGrid)congd1;
                                                                        Row rowg = grf1.Rows.Add();
                                                                        Image loadedImage, resizedImage;
                                                                        loadedImage = Image.FromFile(filename);
                                                                        int originalWidth = 0;
                                                                        originalWidth = loadedImage.Width;
                                                                        int newWidth = 280;
                                                                        resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                                                                        //
                                                                        rowg[colPic1] = resizedImage;
                                                                        rowg[colPic2] = filename;
                                                                        grf[grf.Row, grf.Col] = dsc.doc_group_sub_id;
                                                                        grf1.AutoSizeRows();
                                                                        Application.DoEvents();
                                                                        addInArrayImg(filename, dsc.doc_group_sub_id);
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
                    }
                    //grf.AutoSizeRows();
                    //grf.AutoSizeCols();
                    //CellRange cr = grf.GetCellRange(grf.Row, grf.Col);
                    CellStyle cs = grf.Styles.Add("CellNeWStyle");
                    cs.BackColor = Color.Green;
                    cs.Border.Color = Color.FromArgb(196, 228, 223);
                    cs.Border.Color = Color.Black;
                    //cr.Style = cs;
                    grf.SetCellStyle(grf.Row, grf.Col, cs);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ContextMenu_edit(object sender, System.EventArgs e)
        {
            String pttId = "", filename = "", id = "";
            int i = 0;
            Boolean chk = false;
            foreach (String aa in array1)
            {
                i++;
                if(aa.IndexOf(grf.Row + "," + grf.Col) >= 0)
                {
                    filename = array1[i-1].ToString();
                    chk = true;
                    break;
                }
            }
            try
            {
                filename = filename.Substring(filename.IndexOf('*') + 1);
                //MessageBox.Show("row " + grf.Row + " col " + grf.Col + "\n" + name, "");
                if (chk)
                {
                    String dgs = "";
                    dgs = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
                    FrmScanNewView frm = new FrmScanNewView(bc, txtHn.Text, txtVN.Text, txtName.Text, filename, dgs, txtVisitDate.Text);
                    frm.ShowDialog(this);
                    setGrf();
                    setImage1(false);
                }
            }
            catch(Exception ex)
            {
                filename = ex.Message;
            }
            //id = grfPtt[grfPtt.Row, colID] != null ? grfPtt[grfPtt.Row, colID].ToString() : "";
        }
        private void FrmScanNew_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
