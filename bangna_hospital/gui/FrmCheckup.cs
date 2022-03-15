using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.C1Tile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmCheckup : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit5B;

        Color bg, fc;
        Font ff, ffB;
        int colID = 1, colName = 2;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1ThemeController theme1;
        C1FlexGrid grfCheck;

        C1TileControl TileCat;
        Group grRec;
        Template tempRec;
        ImageElement imageElementRec, ieOrd;
        PanelElement peCat;
        TextElement teCat, teOrd;
        Label lbLoading;

        int colCheckupRow = 1, colCheckupTilNum = 2, colCheckupTilImgNum = 3, colCheckupPID = 4, colCheckupPassport = 5, colCheckupNameE = 6, colCheckupNameT=7, colCheckupDOB = 8, colCheckupNat=9, colCheckupPrefix=10, colCheckupFname=11, colCheckupLname=12;

        public FrmCheckup(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            theme1 = new C1ThemeController();
            theme1.Theme = C1ThemeController.ApplicationTheme;

            btnLoad.Click += BtnLoad_Click;
            btnClear.Click += BtnClear_Click;

            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            lbLoading.Text = "กรุณารอซักครู่...";
            this.Controls.Add(lbLoading);

            initGrfCheckup();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pnTilImage.Controls.Clear();
            grfCheck.Rows.Count = 1;
            pnImage.Controls.Clear();
            //initTileCategory();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            initTileCategory();
        }

        private void initTileCategory()
        {
            TileCat = new C1TileControl();
            TileCat.Dock = DockStyle.Fill;
            TileCat.Font = fEdit;
            
            TileCat.Orientation = LayoutOrientation.Vertical;
            
            grRec = new Group();
            TileCat.Groups.Add(this.grRec);

            //peRec = new C1.Win.C1Tile.PanelElement();
            //ieRec = new C1.Win.C1Tile.ImageElement();
            tempRec = new C1.Win.C1Tile.Template();
            imageElementRec = new C1.Win.C1Tile.ImageElement();
            peCat = new C1.Win.C1Tile.PanelElement();
            ieOrd = new C1.Win.C1Tile.ImageElement();
            teOrd = new TextElement();
            teOrd.Font = fEdit;
            teCat = new TextElement();
            teCat.BackColorSelector = C1.Win.C1Tile.BackColorSelector.Unbound;
            teCat.ForeColor = System.Drawing.Color.Black;
            teCat.ForeColorSelector = C1.Win.C1Tile.ForeColorSelector.Unbound;
            teCat.SingleLine = true;

            imageElementRec.ImageLayout = C1.Win.C1Tile.ForeImageLayout.Stretch;
            //imageElementRec.
            //peOrd = new PanelElement();
            //peOrd.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            //peOrd.Children.Add(ieOrd);
            //peOrd.Children.Add(teOrd);
            //peOrd.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            //peCat.BackColor = tileFoodsNameColor;
            peCat.Children.Add(teCat);
            peCat.Dock = System.Windows.Forms.DockStyle.Top;
            peCat.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);

            //TileCat.DefaultTemplate.Elements.Add(peOrd);
            TileCat.Templates.Add(this.tempRec);
            tempRec.Elements.Add(imageElementRec);
            
                tempRec.Elements.Add(peCat);
            
            //tempRec.Elements.Add(pnFoodsPrice);
            tempRec.Name = "tempFlickrrec";
            TileCat.ScrollOffset = 0;
            TileCat.SurfaceContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            TileCat.Padding = new System.Windows.Forms.Padding(0);
            TileCat.GroupPadding = new System.Windows.Forms.Padding(0);
            //TileCat.BackColor = tileCatColor;       // tab recommend color
            //TileCat.Templates.Add(this.tempRec);

            //pnTilImage.BackColor = tilecolor;
            pnTilImage.Controls.Add(TileCat);
            setTileCategory();
            //for (int i = 0; i < dtCat.Rows.Count; i++)
            //{
            //    LoadFoods(false, i, dtCat.Rows[i]["foods_cat_id"].ToString());
            //}
        }
        private void setTileCategory()
        {
            int index = 0;
            String pathScan = "";
            pathScan = bc.iniC.pathImageScan;
            TileCollection tiles = TileCat.Groups[0].Tiles;
            foreach(String filename in Directory.GetFiles(pathScan))
            {
                var tile = new Tile();
                Patient fooc = new Patient();
                tile.HorizontalSize = 1;
                tile.VerticalSize = 2;
                //tile.VerticalSize = 1;
                //tile.HorizontalSize = 2;
                tile.Template = tempRec;
                tile.Text = index.ToString();
                //tile.HorizontalSize = 2;
                //tile.VerticalSize = 1;
                //tile.Template = tempFlickr;
                //tile.Text1 = "ราคา " + foo1.foods_price;
                //tile.Tag = foo1;
                fooc.filename = filename;
                tile.Tag = fooc;
                tile.Name = index.ToString();
                tile.Index = index;
                tile.Click += Tile_Click;
                tile.Image = null;
                try
                {
                    tile.Image = null;
                    tiles.Add(tile);
                    Image loadedImage = null;
                    if (fooc.filename.Equals("")) continue;
                    loadedImage = Image.FromFile(filename);
                    if (loadedImage != null)
                    {
                        int originalWidth = loadedImage.Width;
                        int newWidth = 180;
                        tile.Image = loadedImage;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("" + ex.Message, "showImg");
                }
                index++;
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            try
            {
                Patient ptt = new Patient();
                ptt = (Patient)((Tile)sender).Tag;
                pnImage.Controls.Clear();

                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(ptt.filename);
                pic.Dock = DockStyle.Fill;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pnImage.Controls.Add(pic);
                //pnImage.
                String filename = "";
                filename = DateTime.Now.Ticks.ToString();
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "C:\\Tesseract-OCR\\tesseract.exe";
                process.StartInfo.Arguments = ptt.filename + " d:\\ocr\\" + filename+" -l eng+tha";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                //grfCheck.Rows.Count = 0;
                grfCheck.Rows.Count = 1;
                if (File.Exists("d:\\ocr\\" + filename + ".txt"))
                {
                    String txt = "";
                    //txt = File.ReadAllText("e:\\ocr\\" + filename + ".txt");
                    //int indexmiss = txt.IndexOf("MISS");
                    //int indexmrs = txt.IndexOf("MRS");
                    //int indexmr = txt.IndexOf("MR");
                    using (StreamReader file = new StreamReader("d:\\ocr\\" + filename + ".txt"))
                    {
                        int counter = 0, cntCheckName=0;
                        string ln;

                        while ((ln = file.ReadLine()) != null)
                        {
                            if (!ln.Equals("\n"))
                            {
                                int indexmiss = ln.IndexOf("MISS");
                                int indexmrs = ln.IndexOf("MRS");
                                int indexmr = ln.IndexOf("MR");
                                if ((indexmiss > 0) || (indexmrs > 0) || (indexmr > 0))
                                {
                                    String[] aaaa = ln.Split(' ');
                                    if (aaaa.Length > 0)
                                    {
                                        Boolean chkname = false;
                                        String prefix = "", dob = "", name = "", surname = "";
                                        DateTime dt1 = new DateTime();
                                        int i = 0;
                                        foreach (string ttt in aaaa)
                                        {
                                            if (ttt.IndexOf("MISS") >= 0)
                                            {
                                                prefix = ttt;
                                                name = aaaa[i + 1];
                                                surname = aaaa[i + 2];
                                                chkname = true;
                                            }
                                            if (ttt.IndexOf("MRS") >= 0)
                                            {
                                                prefix = ttt;
                                                name = aaaa[i + 1];
                                                surname = aaaa[i + 2];
                                                chkname = true;
                                            }
                                            if (ttt.IndexOf("MR") >= 0)
                                            {
                                                prefix = ttt;
                                                name = aaaa[i + 1];
                                                surname = aaaa[i + 2];
                                                chkname = true;
                                            }
                                            if (DateTime.TryParse(ttt, out dt1))
                                            {
                                                dob = ttt;
                                            }
                                            i++;
                                        }
                                        if (chkname)
                                        {
                                            cntCheckName++;
                                            Row row = grfCheck.Rows.Add();
                                            row[colCheckupNameE] = prefix + " " + name + " " + surname;
                                            row[colCheckupDOB] = dob;
                                            row[colCheckupTilImgNum] = cntCheckName;
                                            row[colCheckupTilNum] = ((Tile)sender).Text;
                                            row[colCheckupRow] = grfCheck.Rows.Count-1;
                                            row[colCheckupPrefix] = prefix;
                                            row[colCheckupFname] = name;
                                            row[colCheckupLname] = surname;
                                            //row[3] = surname;
                                            //row[4] = dob;
                                        }
                                    }
                                }
                            }
                            //Console.WriteLine(ln);
                            counter++;
                        }
                        file.Close();
                        //Console.WriteLine($ "File has {counter} lines.");
                    }
                    //int indexmiss = txt.IndexOf("Miss");
                    //String aaa = "";
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmCheckup  Tile_Click " + ex.Message);
            }
            
            hideLbLoading();
        }
        private void initGrfCheckup()
        {
            grfCheck = new C1FlexGrid();
            grfCheck.Font = fEdit;
            grfCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCheck.Location = new System.Drawing.Point(0, 0);
            grfCheck.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnGrd.Controls.Add(grfCheck);

            grfCheck.Rows.Count = 1;
            grfCheck.Cols.Count = 13;
            grfCheck.Cols[colCheckupRow].Caption = "ลำดับ";
            grfCheck.Cols[colCheckupTilNum].Caption = "รูป";
            grfCheck.Cols[colCheckupTilImgNum].Caption = "num";
            grfCheck.Cols[colCheckupPID].Caption = "PID";
            grfCheck.Cols[colCheckupPassport].Caption = "passport";
            grfCheck.Cols[colCheckupNameE].Caption = "Name E";
            grfCheck.Cols[colCheckupNameT].Caption = "Name T";
            grfCheck.Cols[colCheckupDOB].Caption = "DOB";
            grfCheck.Cols[colCheckupNat].Caption = "Nation";

            grfCheck.Cols[colCheckupRow].Width = 50;
            grfCheck.Cols[colCheckupTilNum].Width = 50;
            grfCheck.Cols[colCheckupTilImgNum].Width = 50;
            grfCheck.Cols[colCheckupPID].Width = 100;
            grfCheck.Cols[colCheckupPassport].Width = 100;
            grfCheck.Cols[colCheckupNameE].Width = 200;
            grfCheck.Cols[colCheckupNameT].Width = 200;
            grfCheck.Cols[colCheckupDOB].Width = 120;
            grfCheck.Cols[colCheckupNat].Width = 200;

            grfCheck.Cols[colCheckupRow].AllowEditing = false;
            grfCheck.Cols[colCheckupTilNum].AllowEditing = false;
            grfCheck.Cols[colCheckupTilImgNum].AllowEditing = false;
            grfCheck.Cols[colCheckupPID].AllowEditing = false;
            grfCheck.Cols[colCheckupPassport].AllowEditing = false;
            grfCheck.Cols[colCheckupNameE].AllowEditing = false;
            grfCheck.Cols[colCheckupNameT].AllowEditing = false;
            grfCheck.Cols[colCheckupDOB].AllowEditing = false;
            grfCheck.Cols[colCheckupNat].AllowEditing = false;

            grfCheck.Click += GrfCheck_Click;

            theme1.SetTheme(grfCheck, bc.iniC.themeApp);
            
        }

        private void GrfCheck_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfCheck.Row <=0) return;
            if (grfCheck.Col <= 0) return;

            txtPrefix.Value = grfCheck[grfCheck.Row, colCheckupPrefix] != null ? grfCheck[grfCheck.Row, colCheckupPrefix].ToString():"";
            txtFname.Value = grfCheck[grfCheck.Row, colCheckupFname] != null ? grfCheck[grfCheck.Row, colCheckupFname].ToString() : "";
            txtLname.Value = grfCheck[grfCheck.Row, colCheckupLname] != null ? grfCheck[grfCheck.Row, colCheckupLname].ToString() : "";
            txtDOB.Value = grfCheck[grfCheck.Row, colCheckupDOB] != null ? grfCheck[grfCheck.Row, colCheckupDOB].ToString() : "";
            txtFullname.Value = txtPrefix.Text + " " + txtFname.Text + " " + txtLname.Text;
        }

        private void showLbLoading()
        {
            lbLoading.Show();
            lbLoading.BringToFront();
            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();
            Application.DoEvents();
        }
        private void FrmCheckup_Load(object sender, EventArgs e)
        {
            sC.HeaderHeight = 0;
            tC.SelectedTab = tabImage;

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
        }
    }
}
