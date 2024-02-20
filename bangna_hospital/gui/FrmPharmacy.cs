using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.C1Tile;
using C1.Win.TouchToolKit;
using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bangna_hospital.gui
{
    public partial class FrmPharmacy : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        //C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbLoading;
        Timer timeOperList;
        C1TileControl TileDrugINimg;
        PanelElement pnTopTilesLab;
        C1FlexGrid grfReport, grfOrder;
        
        ImageElement imgTopTilesLab, imgFolder;
        TextElement txtTopTilesLab;
        Template tempFolder;
        Group grpDrug;
        Boolean pageLoad = false;
        Image imgCorr, imgTran;
        Color backColor, checkBackColor, hotBackColor, hotCheckBackColor, subgroupLineColor, subgroupTitleColor;
        AutoCompleteStringCollection autoLab, autoXray, autoProcedure, autoWard, autoDrug;
        Image imgDrug;
        int newHeight = 0;
        float ImageScale = 1.0f;
        Point pDown = Point.Empty;
        Rectangle rect = Rectangle.Empty;
        public FrmPharmacy(BangnaControl bc)
        {
            InitializeComponent();
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
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            timeOperList = new Timer();
            timeOperList.Interval = 30000;
            timeOperList.Enabled = false;
            imgCorr = Resources.red_checkmark_png_16;
            imgTran = Resources.DeleteTable_small;
            
            backColor = Color.FromArgb(29, 29, 29);
            checkBackColor = Color.FromArgb(244, 179, 0);
            hotBackColor = Color.FromArgb(58, 58, 58);
            hotCheckBackColor = Color.FromArgb(246, 188, 35);
            subgroupLineColor = Color.FromArgb(38, 38, 38);
            subgroupTitleColor = Color.FromArgb(255, 200, 63);

            autoLab = new AutoCompleteStringCollection();
            autoXray = new AutoCompleteStringCollection();
            autoProcedure = new AutoCompleteStringCollection();
            autoDrug = new AutoCompleteStringCollection();
            
            //pnDrugINimg.Dock = DockStyle.None;
            //pnDrugINimg.Dock = DockStyle.Fill;
            //picDrugIN.SizeMode = PictureBoxSizeMode.StretchImage;
            picDrugIN.MouseWheel += PicDrugIN_MouseWheel;
            picDrugIN.MouseDown += PicDrugIN_MouseDown;
            picDrugIN.MouseMove += PicDrugIN_MouseMove;
            picDrugIN.MouseUp += PicDrugIN_MouseUp;
            initTileImg();

            setEvent();
            
        }

        private void PicDrugIN_MouseUp(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            Rectangle iR = ImageArea(picDrugIN);
            rect = new Rectangle(pDown.X - iR.X, pDown.Y - iR.Y,
                                 e.X - pDown.X, e.Y - pDown.Y);
            Rectangle rectSrc = Scaled(rect, picDrugIN, true);
            Rectangle rectDest = new Rectangle(Point.Empty, rectSrc.Size);

            Bitmap bmp = new Bitmap(rectDest.Width, rectDest.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(picDrugIN.Image, rectDest, rectSrc, GraphicsUnit.Pixel);
            }
            picDrugIN.Image = bmp;
        }

        private void PicDrugIN_MouseMove(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            if (!e.Button.HasFlag(MouseButtons.Left)) return;

            rect = new Rectangle(pDown, new Size(e.X - pDown.X, e.Y - pDown.Y));
            using (Graphics g = picDrugIN.CreateGraphics())
            {
                picDrugIN.Refresh();
                g.DrawRectangle(Pens.Orange, rect);
            }
        }

        private void PicDrugIN_MouseDown(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            pDown = e.Location;
            picDrugIN.Refresh();
        }

        private void PicDrugIN_MouseWheel(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            //const float scale_per_delta = 0.1f / 120;

            //// Update the drawing based upon the mouse wheel scrolling.
            //ImageScale += e.Delta * scale_per_delta;
            //if (ImageScale < 0) ImageScale = 0;

            //// Size the image.
            //Size newsize = new Size((int)(imgDrug.Width * ImageScale), (int)(imgDrug.Height * ImageScale));
            //picDrugIN.Image = ZoomPicture(imgDrug, newsize);
        }
        Rectangle ImageArea(PictureBox pbox)
        {
            Size si = pbox.Image.Size;
            Size sp = pbox.ClientSize;

            if (pbox.SizeMode == PictureBoxSizeMode.StretchImage)
                return pbox.ClientRectangle;
            if (pbox.SizeMode == PictureBoxSizeMode.Normal ||
                pbox.SizeMode == PictureBoxSizeMode.AutoSize)
                return new Rectangle(Point.Empty, si);
            if (pbox.SizeMode == PictureBoxSizeMode.CenterImage)
                return new Rectangle(new Point((sp.Width - si.Width) / 2,
                                    (sp.Height - si.Height) / 2), si);

            //  PictureBoxSizeMode.Zoom
            float ri = 1f * si.Width / si.Height;
            float rp = 1f * sp.Width / sp.Height;
            if (rp > ri)
            {
                int width = si.Width * sp.Height / si.Height;
                int left = (sp.Width - width) / 2;
                return new Rectangle(left, 0, width, sp.Height);
            }
            else
            {
                int height = si.Height * sp.Width / si.Width;
                int top = (sp.Height - height) / 2;
                return new Rectangle(0, top, sp.Width, height);
            }
        }
        Rectangle Scaled(Rectangle rect, PictureBox pbox, bool scale)
        {
            float factor = GetFactor(pbox);
            if (!scale) factor = 1f / factor;
            return Rectangle.Round(new RectangleF(rect.X * factor, rect.Y * factor,
                                       rect.Width * factor, rect.Height * factor));
        }
        float GetFactor(PictureBox pBox)
        {
            if (pBox.Image == null) return 0;
            Size si = pBox.Image.Size;
            Size sp = pBox.ClientSize;
            float ri = 1f * si.Width / si.Height;
            float rp = 1f * sp.Width / sp.Height;
            float factor = 1f * pBox.Image.Width / pBox.ClientSize.Width;
            if (rp > ri) factor = 1f * pBox.Image.Height / pBox.ClientSize.Height;
            return factor;
        }
        private void setEvent()
        {
            timeOperList.Tick += TimeOperList_Tick;
            btnDrugINZoom.Click += BtnDrugINZoom_Click;
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            txtSearchItem.Enter += TxtSearchItem_Enter;
            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            btnDrugINWard.Click += BtnDrugINWard_Click;
        }

        private void BtnDrugINWard_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Form frm = new Form();
            C1FlexGrid grf = new C1FlexGrid();
            frm.Controls.Add(grf);
            frm.ShowDialog(this);
        }

        private void TxtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setGrfOrderItem();
            }
        }
        private void setGrfOrderItem()
        {
            setGrfOrderItem(txtItemCode.Text.Trim(), lbItemName.Text, txtItemQTY.Text.Trim(), "drug");
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void setGrfOrderItem(String code, String name, String qty, String flag)
        {
            if (grfOrder == null) { return; }
            ////if(grfOrder.Row<=0) { return; }
            //Row rowitem = grfOrder.Rows.Add();
            //rowitem[colgrfOrderCode] = code;
            //rowitem[colgrfOrderName] = name;
            //rowitem[colgrfOrderQty] = qty;
            //rowitem[colgrfOrderStatus] = flag;
            //rowitem[colgrfOrderID] = "";
            //rowitem[colgrfOrderReqNO] = "";
            //rowitem[colgrfOrdFlagSave] = "0";//ต้องการ save ลง table temp_order
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void TxtSearchItem_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.Focus();
        }

        private void TxtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearchItem.Text.Trim().Length <= 0) return;
                setOrderItem();
                txtItemCode.Focus();
            }
        }
        private void setOrderItem()
        {
            String[] txt = txtSearchItem.Text.Split('#');
            if (txt.Length <= 1)
            {
                lfSbMessage.Text = "no item";
                txtItemCode.Value = "";
                lbItemName.Text = "";
                txtItemQTY.Value = "1";
                return;
            }
            String name = txt[0].Trim();
            String code = txt[1].Trim();
            PharmacyM01 drug = new PharmacyM01();
            String name1 = bc.bcDB.pharM01DB.SelectNameByPk(code);
            txtItemCode.Value = code;
            lbItemName.Text = name1;
            txtItemQTY.Visible = false;
            txtItemQTY.Value = "1";
        }
        private void BtnDrugINZoom_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //pnDrugINimg.Dock = DockStyle.None;
            picDrugIN.Image = ZoomPicture(imgDrug, new Size(2, 2));

        }
        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width), Convert.ToInt32(img.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }
        private void ChkDrugWardDrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            clearControlOrder();
        }
        private void clearControlOrder()
        {
            txtItemCode.Value = "";
            txtSearchItem.Value = "";
            lbItemName.Text = "";
            txtItemRemark.Value = "";
        }
        private void TimeOperList_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setDrugINimgList();
        }
        private void setDrugINimgList()
        {
            try
            {
                timeOperList.Stop();
                showLbLoading();
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, "u_drugin", "u_drugin");
                String[] listFile = ftpc.directoryListSimple("drugin");
                grpDrug.Tiles.Clear();
                foreach (String filename in listFile)
                {
                    MemoryStream streamCertiDtr = ftpc.download(filename);
                    streamCertiDtr.Position = 0;
                    Image image = (Image)new Bitmap((Stream)streamCertiDtr);
                    int width = image.Width;
                    int imgScanWidth = this.bc.imgScanWidth;
                    Image thumbnailImage = image.GetThumbnailImage(210, 230 * image.Height / width, (Image.GetThumbnailImageAbort)null, IntPtr.Zero);

                    var tile = new Tile();
                    tile.HorizontalSize = 2;
                    tile.VerticalSize = 3;
                    tile.Image = thumbnailImage;
                    tile.Text = filename;
                    tile.Text1 = filename;
                    tile.Template = tempFolder;
                    //tileitemdrug.Text3 = dr["MNC_LB_TYP_CD"].ToString();
                    tile.BackColor = Color.IndianRed;
                    tile.Tag = image;
                    tile.Click += Tile_Click;
                    grpDrug.Tiles.Add(tile);
                }
            }
            catch(Exception ex)
            {

            }
            timeOperList.Start();
            hideLbLoading();
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((Tile)sender).Tag != null)
            {
                //((Tile)sender).Image = imgCorr;
                foreach (Tile tile in grpDrug.Tiles)
                {
                    ((Tile)sender).Checked = false;
                    ((Tile)sender).BackColor = Color.IndianRed;
                }
                imgDrug = (Image)((Tile)sender).Tag;
                pnDrugINimg.Dock = DockStyle.Fill;
                picDrugIN.Image = imgDrug;
                newHeight = 0;
                ((Tile)sender).Checked = true;
                ((Tile)sender).BackColor = Color.SteelBlue;
            }
            else
            {
                //((Tile)sender).Image = null;
            }
            //setGrfItem(((Tile)sender).Text1, ((Tile)sender).Text, "drug");
        }

        private void initTileImg()
        {
            TileDrugINimg = new C1TileControl();
            TileDrugINimg.Orientation = LayoutOrientation.Horizontal;
            //TileFoods.DefaultTemplate.Elements.Add(peOrd);
            TileDrugINimg.Dock = DockStyle.Fill;

            //TileFoods[i].Templates.Add(this.tempFlickr);
            //TileFoods = new C1TileControl();
            //TileFoods[i].Name = "tile" + i;
            //TileFoods[i].Dock = DockStyle.Fill;
            //TileFoods[i].BackColor = tilecolor;     // tile color
            //pnOrder.Controls.Add(TileFoods);                    
            TileDrugINimg.ScrollOffset = 0;
            TileDrugINimg.SurfaceContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            TileDrugINimg.Padding = new System.Windows.Forms.Padding(0);
            TileDrugINimg.GroupPadding = new System.Windows.Forms.Padding(10);
            //TileDrugINimg.BackColor = backColor;
            TileDrugINimg.TileBackColor = backColor;
            TileDrugINimg.CheckBackColor = checkBackColor;
            TileDrugINimg.HotBackColor = hotBackColor;
            TileDrugINimg.HotCheckBackColor = hotCheckBackColor;

            grpDrug = new Group();
            TileDrugINimg.Groups.Add(grpDrug);
            TileCollection tiles = TileDrugINimg.Groups[0].Tiles;
            imgTopTilesLab = new C1.Win.C1Tile.ImageElement();
            imgTopTilesLab.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            txtTopTilesLab = new C1.Win.C1Tile.TextElement();
            imgFolder = new C1.Win.C1Tile.ImageElement();
            imgFolder.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);

            pnTopTilesLab = new C1.Win.C1Tile.PanelElement();
            pnTopTilesLab.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            pnTopTilesLab.Children.Add(imgTopTilesLab);
            pnTopTilesLab.Children.Add(txtTopTilesLab);
            pnTopTilesLab.ChildSpacing = 2;
            pnTopTilesLab.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);

            tempFolder = new C1.Win.C1Tile.Template();
            tempFolder.Elements.Add(pnTopTilesLab);
            //tempFolder.Elements.Add(imgFolder);

            pnDrugINimgList.Controls.Add(TileDrugINimg);
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
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
        private void FrmPharmacy_Load(object sender, EventArgs e)
        {
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            scDrugIN.HeaderHeight = 0;
            spOrder.HeaderHeight = 0;
            setDrugINimgList();
            
            timeOperList.Enabled = true;
            autoDrug = bc.bcDB.pharM01DB.getlDrugAll();            
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            pnDrugINimg.Dock = DockStyle.Fill;
            picDrugIN.Dock = DockStyle.Fill;
            picDrugIN.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Text = "Last Update 2021-03-23";
        }
    }
}
