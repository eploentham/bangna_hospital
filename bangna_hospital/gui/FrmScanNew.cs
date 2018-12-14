using bangna_hospital.control;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leadtools;
using Leadtools.Ocr;

namespace bangna_hospital.gui
{
    public partial class FrmScanNew : Form
    {
        BangnaControl bc;
        MainMenu menu;
        C1FlexGrid grf;
        Font fEdit, fEditB;
        private IOcrEngine _ocrEngine;

        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        ArrayList array1 = new ArrayList();
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
            array1 = new ArrayList();
            _ocrEngine = OcrEngineManager.CreateEngine(OcrEngineType.LEAD, false);

            theme1.Theme = bc.iniC.themeApplication;
            theme1.SetTheme(sb1, "BeigeOne");
            btnOpen.Click += BtnOpen_Click;

            sb1.Text = "aaaaaaaaaa";
            initGrf();
            setGrf();
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
                int i = 1,j = 1,row = grf.Rows.Count;
                grf.Rows.Add();
                row = grf.Rows.Count;
                String re = "";
                array1.Clear();
                foreach (String file in ofd.FileNames)
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
                            int originalWidth = loadedImage.Width;
                            int newWidth = 180;
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
                        array1.Add(i+","+j+",*"+file);
                        if (j==1)
                            grf[i, colPic1] = resizedImage;
                        else if (j == 2)
                            grf[i, colPic2] = resizedImage;
                        else if (j == 3)
                            grf[i, colPic3] = resizedImage;
                        else if (j == 4)
                            grf[i, colPic4] = resizedImage;
                        j++;
                        
                    }
                    catch (Exception ex)
                    {
                        re = ex.Message;
                    }
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
            grf.ContextMenu = menuGw;

            //row1[colVSE2] = row[ic.ivfDB.pApmDB.pApm.e2].ToString().Equals("1") ? imgCorr : imgTran;

        }
        //private void Grf_AfterRowColChange(object sender, RangeEventArgs e)
        //{
        //    //throw new NotImplementedException();

        //}
        private void ContextMenu_edit(object sender, System.EventArgs e)
        {
            String pttId = "", name = "", id = "";
            int i = 0;
            Boolean chk = false;
            foreach (String aa in array1)
            {
                i++;
                if(aa.IndexOf(grf.Row + "," + grf.Col) >= 0)
                {
                    name = array1[i-1].ToString();
                    chk = true;
                    break;
                }
            }
            try
            {
                name = name.Substring(name.IndexOf('*') + 1);
                //MessageBox.Show("row " + grf.Row + " col " + grf.Col + "\n" + name, "");
                if (chk)
                {
                    FrmScanView frm = new FrmScanView(bc, txtHn.Text, txtVN.Text, txtNameFeMale.Text, name);
                    frm.ShowDialog(this);
                }
            }
            catch(Exception ex)
            {

            }
            
            //id = grfPtt[grfPtt.Row, colID] != null ? grfPtt[grfPtt.Row, colID].ToString() : "";
        }
        private void FrmScanNew_Load(object sender, EventArgs e)
        {

        }
    }
}
