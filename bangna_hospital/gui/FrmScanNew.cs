using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
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
        Font fEdit, fEditB;
        //private IOcrEngine _ocrEngine;

        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        ArrayList array1 = new ArrayList();
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
            bc.bcDB.dgsDB.setCboBsp(cboDgs, "");
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(-1);
            txtVisitDate.Value = dt.Year + "-" + dt.ToString("MM-dd");
            array1 = new ArrayList();
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
            theme1.SetTheme(grfScan, theme1.Theme);
            foreach (Control con in groupBox1.Controls)
            {
                theme1.SetTheme(con, theme1.Theme);
            }
            foreach (Control con in grfScan.Controls)
            {
                theme1.SetTheme(con, theme1.Theme);
            }

            btnOpen.Click += BtnOpen_Click;
            btnHn.Click += BtnHn_Click;

            sb1.Text = "aaaaaaaaaa";
            initGrf();
            setGrf();
        }

        private void BtnHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            txtHn.Value = bc.sPtt.Hn;
            txtName.Value = bc.sPtt.Name;
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
            int i = 1, j = 1, row = grf.Rows.Count;
            grf.Rows.Add();
            row = grf.Rows.Count;
            String re = "";
            array1.Clear();
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
                        MemoryStream stream = new MemoryStream();

                        loadedImage.Save(stream, ImageFormat.Jpeg);
                        loadedImage.Dispose();
                        loadedImage = Image.FromStream(stream);
                        int originalWidth = 0;
                        originalWidth = loadedImage.Width;
                        int newWidth = 280;
                        resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                        
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
                    grf.Cols[colPic1].ImageAndText = true;
                    grf.Cols[colPic2].ImageAndText = true;
                    grf.Cols[colPic3].ImageAndText = true;
                    grf.Cols[colPic4].ImageAndText = true;
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
                    
                    //resizedImage.Dispose();
                }
                catch (Exception ex)
                {
                    re = ex.Message;
                }
            }
            grf.AutoSizeCols();
            grf.AutoSizeRows();
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

            grfScan.Controls.Add(grf);

            theme1.SetTheme(grf, "Office2010Blue");

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
            grf.Cols[colPic1].Width = 100;
            grf.Cols[colPic2].Width = 100;
            grf.Cols[colPic3].Width = 100;
            grf.Cols[colPic4].Width = 100;
            grf.ShowCursor = true;

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
                String dgssid = "";
                dgssid = bc.bcDB.dgssDB.getIdDgss(dgss.doc_group_sub_name);
                if (!dgssid.Equals(idOld))
                {
                    idOld = dgssid;
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
            try
            {
                filename = filename.Substring(filename.IndexOf('*') + 1);
                String[] ext = filename.Split('.');
                dgs = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = dgs;
                dsc.hn = txtHn.Text;
                dsc.vn = txtVN.Text;
                dsc.an = txtAN.Text;
                dsc.visit_date = txtVisitDate.Text;
                if (!txtVN.Text.Equals(""))
                {
                    dsc.row_no = bc.bcDB.dscDB.selectRowNoByHnVn(txtHn.Text,txtVN.Text, dgs);
                }
                else
                {
                    dsc.row_no = bc.bcDB.dscDB.selectRowNoByHn(txtHn.Text, dgs);
                }
                dsc.host_ftp = bc.iniC.hostFTP;
                dsc.image_path = txtHn.Text+"//"+txtHn.Text+"_"+dgs+"_"+ dsc.row_no + "." + ext[ext.Length - 1];
                bc.bcDB.dscDB.insertDocScan(dsc, bc.userId);
                FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                ftp.createDirectory(txtHn.Text);
                ftp.delete(dsc.image_path);
                ftp.upload(dsc.image_path, filename);
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
