using bangna_hospital.control;
using bangna_hospital.object1;
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
        Font fEdit, fEditB;

        Color bg, fc;
        Font ff, ffB;
        int colID = 1, colName = 2;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1ThemeController theme1;

        C1TileControl TileCat;
        Group grRec;
        Template tempRec;
        ImageElement imageElementRec, ieOrd;
        PanelElement peCat;
        TextElement teCat, teOrd;

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

            theme1 = new C1ThemeController();
            theme1.Theme = C1ThemeController.ApplicationTheme;

            btnLoad.Click += BtnLoad_Click;
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
            
            TileCat.Orientation = LayoutOrientation.Horizontal;
            
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
            peCat.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);

            //TileCat.DefaultTemplate.Elements.Add(peOrd);
            TileCat.Templates.Add(this.tempRec);
            tempRec.Elements.Add(imageElementRec);
            
                tempRec.Elements.Add(peCat);
            
            //tempRec.Elements.Add(pnFoodsPrice);
            tempRec.Name = "tempFlickrrec";
            TileCat.ScrollOffset = 0;
            TileCat.SurfaceContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            TileCat.Padding = new System.Windows.Forms.Padding(0);
            TileCat.GroupPadding = new System.Windows.Forms.Padding(20);
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
            process.StartInfo.FileName = "c:\\Program Files\\Tesseract-OCR\\tesseract.exe";
            process.StartInfo.Arguments = ptt.filename + " e:\\ocr\\" + filename;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

            if (File.Exists("e:\\ocr\\" + filename + ".txt"))
            {
                String txt = "";
                //txt = File.ReadAllText("e:\\ocr\\" + filename + ".txt");
                //int indexmiss = txt.IndexOf("MISS");
                //int indexmrs = txt.IndexOf("MRS");
                //int indexmr = txt.IndexOf("MR");
                using (StreamReader file = new StreamReader("e:\\ocr\\" + filename + ".txt"))
                {
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        if (!ln.Equals("\n"))
                        {
                            int indexmiss = ln.IndexOf("MISS");
                            int indexmrs = ln.IndexOf("MRS");
                            int indexmr = ln.IndexOf("MR");
                            if (indexmiss > 0)
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
                                        //Row row = grfOCR.Rows.Add();
                                        //row[1] = prefix;
                                        //row[2] = name;
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

        private void FrmCheckup_Load(object sender, EventArgs e)
        {
            sC.HeaderHeight = 0;
            tC.SelectedTab = tabImage;
        }
    }
}
