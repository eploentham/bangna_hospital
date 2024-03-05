using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.C1Tile;
using C1.Win.TouchToolKit;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.Printing;
using GrapeCity.Viewer.Common.Model;

namespace bangna_hospital.gui
{
    public partial class FrmPharmacy : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, fPrnBil, famtB, fEditS, famtB30;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        //C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbLoading;
        Timer timeImgDrugINward;
        C1TileControl TileDrugINimg;
        PanelElement pnTopTilesLab;
        C1FlexGrid grfReport, grfOrder, grfOPDFinishList, grfOPDFinishReq;
        
        ImageElement imgTopTilesLab, imgFolder;
        TextElement txtTopTilesLab;
        Template tempFolder;
        Group grpDrug;
        Boolean pageLoad = false;
        Image imgCorr, imgTran;
        Color backColor, checkBackColor, hotBackColor, hotCheckBackColor, subgroupLineColor, subgroupTitleColor;
        AutoCompleteStringCollection autoLab, autoXray, autoProcedure, autoWard, autoDrug;
        Image imgDrug;
        int newHeight = 0,rowindexgrfOPDFinishList = 0, rowindexgrfOPDFinishReq= 0,TIMERCNT=0;
        String TC1ACTIVE = "", TCOPDACTIVE="", PRENO = "", VSDATE = "", HN = "", DEPTNO = "", DTRCODE = "", DOCGRPID = "", TABVSACTIVE = "", ITEMCODE="";
        float ImageScale = 1.0f;
        int colOPDFinishListHN = 1,colOPDFinishListPttName = 2, colOPDFinishListQueNo = 3, colOPDFinishListReqTime = 4, colOPDFinishListCFRTime = 5, colOPDFinishListVnno = 6, colOPDFinishListDeptName = 7, colOPDFinishListDocCD = 8, colOPDFinishListCFRYr = 9, colOPDFinishListCFRNo = 10, colOPDFinishListCFRDATE = 11, colOPDFinishListCFRSTS = 12, colOPDFinishListReqYr = 13, colOPDFinishListReqNo = 14, colOPDFinishListReqDate = 15, colOPDFinishListVsDate = 16, colOPDFinishListPreNo = 17, colOPDFinishListDtrcode = 18, colOPDFinishListDtrName = 19, colOPDFinishListUsrReq = 20, colOPDFinishListUsrPhar = 21, colOPDFinishListPttDOB = 22, colOPDFinishListPttSex = 23, colOPDFinishListPaidName = 24, colOPDFinishListCompName = 25, colOPDFinishListPttAttachNote = 26, colOPDFinishListFinNote = 27, colOPDFinishListDeptNo = 28, colOPDFinishListSecNo = 29;
        int colOPDFinishReqItemCode = 1, colOPDFinishReqItemName = 2, colOPDFinishReqDocCD = 3, colOPDFinishReqCFRYr = 4, colOPDFinishReqCFRNo = 5, colOPDFinishReqCFRDate = 6, colOPDFinishReqQty = 7, colOPDFinishReqDirDesc = 8, colOPDFinishReqPttName=9;
        Point pDown = Point.Empty;
        Rectangle rect = Rectangle.Empty;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        DataTable DTSTCDRUG;
        DataRow DROWDRUG;
        FrmReportNew frmDrugStricker;
        public FrmPharmacy(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            initFont();
            initControl();
            setEvent();
        }
        private void initFont()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            famtB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            famtB30 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
        }
        private void initControl()
        {
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            timeImgDrugINward = new Timer();
            timeImgDrugINward.Interval = 1000;
            timeImgDrugINward.Enabled = false;
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
            frmDrugStricker = new FrmReportNew(bc, "drug_stricker");
            initTileImg();
            initGrfOPDFinishList();
            initGrfOPDFinishItem();
        }
        private void initGrfOPDFinishItem()
        {
            grfOPDFinishReq = new C1FlexGrid();
            grfOPDFinishReq.Font = fEdit;
            grfOPDFinishReq.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPDFinishReq.Location = new System.Drawing.Point(0, 0);
            grfOPDFinishReq.Rows.Count = 1;
            grfOPDFinishReq.Cols.Count = 10;

            grfOPDFinishReq.Cols[colOPDFinishReqItemCode].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqItemName].Width = 300;
            grfOPDFinishReq.Cols[colOPDFinishReqDocCD].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRYr].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRNo].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRDate].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqQty].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqDirDesc].Width = 500;

            grfOPDFinishReq.ShowCursor = true;
            grfOPDFinishReq.Cols[1].Caption = "-";

            grfOPDFinishReq.Cols[colOPDFinishReqDocCD].Visible = false;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRYr].Visible = false;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRNo].Visible = false;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRDate].Visible = false;

            grfOPDFinishReq.Cols[colOPDFinishReqItemCode].AllowEditing = false;
            grfOPDFinishReq.Cols[colOPDFinishReqItemName].AllowEditing = false;
            grfOPDFinishReq.Cols[colOPDFinishReqQty].AllowEditing = false;
            grfOPDFinishReq.Cols[colOPDFinishReqDirDesc].AllowEditing = false;
            grfOPDFinishReq.Cols[colOPDFinishListPttName].AllowEditing = false;

            grfOPDFinishReq.AfterRowColChange += GrfOPDFinishReq_AfterRowColChange;
            pnOPDFinishReq.Controls.Add(grfOPDFinishReq);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOPDFinishReq, "MacBlue");
        }
        private void GrfOPDFinishReq_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                if (rowindexgrfOPDFinishReq != ((C1FlexGrid)(sender)).Row) { rowindexgrfOPDFinishReq = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                ITEMCODE = grfOPDFinishReq[grfOPDFinishReq.Row, colOPDFinishReqItemCode].ToString();
                rbItemName.Text = grfOPDFinishReq[grfOPDFinishReq.Row, colOPDFinishReqItemName].ToString();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmPatient GrfOPD_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmPatient GrfOPD_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }
        private void setGrfOPDFinishReq(String doccd, String cfryear, String cfrdate, String cfrno)
        {
            DTSTCDRUG = bc.bcDB.pharT06DB.selectByCFRNoDrug1(cfryear, cfrno, doccd, cfrdate);
            int i = 1, j = 1;
            grfOPDFinishReq.Rows.Count = 1; grfOPDFinishReq.Rows.Count = DTSTCDRUG.Rows.Count + 1;
            foreach (DataRow row1 in DTSTCDRUG.Rows)
            {
                try
                {
                    C1.Win.C1FlexGrid.Row rowa = grfOPDFinishReq.Rows[i];
                    rowa[colOPDFinishReqItemCode] = row1["MNC_PH_CD"].ToString();
                    rowa[colOPDFinishReqItemName] = row1["MNC_PH_TN"].ToString();
                    rowa[colOPDFinishReqDocCD] = row1["MNC_DOC_CD"].ToString();
                    rowa[colOPDFinishReqCFRYr] = row1["MNC_CFR_YR"].ToString();
                    rowa[colOPDFinishReqCFRNo] = row1["MNC_CFR_NO"].ToString();
                    rowa[colOPDFinishReqCFRDate] = row1["MNC_CFG_DAT"].ToString();
                    rowa[colOPDFinishReqQty] = row1["MNC_PH_QTY_PAID"].ToString();
                    rowa[colOPDFinishReqPttName] = row1["pttname"].ToString();
                    rowa[colOPDFinishReqDirDesc] = row1["drug_using"].ToString().Replace("/","").Trim();
                    i++;
                }
                catch(Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD setGrfOrder " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfOrder ", ex.Message);
                }
            }
        }
        private void initGrfOPDFinishList()
        {
            grfOPDFinishList = new C1FlexGrid();
            grfOPDFinishList.Font = fEdit;
            grfOPDFinishList.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPDFinishList.Location = new System.Drawing.Point(0, 0);
            grfOPDFinishList.Rows.Count = 1;
            grfOPDFinishList.Cols.Count = 30;
            grfOPDFinishList.Cols[colOPDFinishListHN].Width = 80;
            grfOPDFinishList.Cols[colOPDFinishListPttName].Width = 300;
            grfOPDFinishList.Cols[colOPDFinishListQueNo].Width = 60;
            grfOPDFinishList.Cols[colOPDFinishListReqTime].Width = 70;
            grfOPDFinishList.Cols[colOPDFinishListCFRTime].Width = 70;
            grfOPDFinishList.Cols[colOPDFinishListVnno].Width = 60;
            grfOPDFinishList.Cols[colOPDFinishListDeptName].Width = 100;

            grfOPDFinishList.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOPDFinishList.Cols[1].Caption = "-";

            grfOPDFinishList.Cols[colOPDFinishListDocCD].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRYr].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRNo].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRDATE].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRSTS].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListReqYr].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListReqNo].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListReqDate].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListVsDate].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListPreNo].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListDtrcode].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListPttDOB].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListPttSex].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListDeptNo].Visible = false;

            grfOPDFinishList.Cols[colOPDFinishListHN].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListPttName].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListQueNo].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListReqTime].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRTime].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListVnno].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListUsrPhar].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListUsrReq].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListDtrName].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListPaidName].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListCompName].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListPttAttachNote].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListFinNote].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListSecNo].AllowEditing = false;

            grfOPDFinishList.AfterRowColChange += GrfOPDFinishList_AfterRowColChange;
            pnOPDFinishList.Controls.Add(grfOPDFinishList);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOPDFinishList, "Office2010Red");
        }
        private void GrfOPDFinishList_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                if (rowindexgrfOPDFinishList != ((C1FlexGrid)(sender)).Row) { rowindexgrfOPDFinishList = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                String doccd = "", cfryear = "", cfrdate = "", cfrno = "";
                doccd = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListDocCD].ToString();
                cfryear = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListCFRYr].ToString();
                cfrdate = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListCFRDATE].ToString();
                cfrno = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListCFRNo].ToString();
                HN = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListHN].ToString();
                VSDATE = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListVsDate].ToString();
                PRENO = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListPreNo].ToString();
                rbPttName.Text = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListPttName].ToString();

                clearControlReq();

                setGrfOPDFinishReq(doccd, cfryear, cfrdate, cfrno);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmPatient GrfOPD_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmPatient GrfOPD_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }
        private void setGrfOPDFinishList()
        {//ดึงจาก table temp_order
            DataTable dt = new DataTable();
            dt = bc.bcDB.pharT05DB.SelectByCFGCurrentDate();
            int i = 1, j = 1;
            grfOPDFinishList.Rows.Count = 1; grfOPDFinishList.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                try
                {
                    C1.Win.C1FlexGrid.Row rowa = grfOPDFinishList.Rows[i];
                    rowa[colOPDFinishListHN] = row1["MNC_HN_NO"].ToString();
                    rowa[colOPDFinishListPttName] = row1["PATIENTNAME"].ToString();
                    rowa[colOPDFinishListQueNo] = row1["MNC_QUE_NO"].ToString();
                    rowa[colOPDFinishListReqTime] = bc.showTime(row1["MNC_REQ_TIM"].ToString());
                    rowa[colOPDFinishListVnno] = row1["MNC_VN_NO"].ToString()+"."+ row1["MNC_VN_SEQ"].ToString()+"."+ row1["MNC_VN_SUM"].ToString();
                    
                    rowa[colOPDFinishListDeptName] = row1["MNC_SEC_DSC"].ToString();

                    rowa[colOPDFinishListDocCD] = row1["MNC_DOC_CD"].ToString();
                    rowa[colOPDFinishListCFRYr] = row1["MNC_CFR_YR"].ToString();
                    rowa[colOPDFinishListCFRNo] = row1["MNC_CFR_NO"].ToString();
                    rowa[colOPDFinishListCFRDATE] = row1["MNC_CFG_DAT"].ToString();
                    rowa[colOPDFinishListCFRTime] = bc.showTime(row1["MNC_CFR_TIME"].ToString());

                    rowa[colOPDFinishListReqNo] = row1["MNC_REQ_NO"].ToString();
                    rowa[colOPDFinishListVsDate] = row1["MNC_DATE"].ToString();
                    rowa[colOPDFinishListPreNo] = row1["MNC_PRE_NO"].ToString();
                    rowa[colOPDFinishListReqDate] = row1["MNC_REQ_DAT"].ToString();
                    rowa[colOPDFinishListDtrcode] = row1["MNC_ORD_DOT"].ToString();
                    rowa[colOPDFinishListDtrName] = row1["ORDDOTNAME"].ToString();
                    rowa[colOPDFinishListPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                    rowa[colOPDFinishListCompName] = row1["MNC_COM_DSC"].ToString();
                    rowa[colOPDFinishListPttAttachNote] = row1["MNC_ATT_NOTE"].ToString();
                    rowa[colOPDFinishListFinNote] = row1["MNC_FIN_NOTE"].ToString();
                    rowa[colOPDFinishListSecNo] = row1["MNC_SEC_NO"].ToString();
                    //rowa[colgrfOrderReqNO] = "";
                    //rowa[colgrfOrdFlagSave] = "1";//ดึงข้อมูลจาก table temp_order 
                    rowa[0] = i.ToString();
                    //rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    i++;
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD setGrfOrder " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfOrder ", ex.Message);
                }
            }
        }
        private void clearControlReq()
        {
            ITEMCODE = "";
            rbItemName.Text = "";
            grfOPDFinishReq.Rows.Count = 1;
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
            timeImgDrugINward.Tick += TimeImgDrugINward_Tick;
            btnDrugINZoom.Click += BtnDrugINZoom_Click;
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            txtSearchItem.Enter += TxtSearchItem_Enter;
            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            btnDrugINWard.Click += BtnDrugINWard_Click;
            tC1.SelectedTabChanged += TC1_SelectedTabChanged;
            tcOPD.SelectedTabChanged += TcOPD_SelectedTabChanged;

            picDrugIN.MouseWheel += PicDrugIN_MouseWheel;
            picDrugIN.MouseDown += PicDrugIN_MouseDown;
            picDrugIN.MouseMove += PicDrugIN_MouseMove;
            picDrugIN.MouseUp += PicDrugIN_MouseUp;

            btnPrint.Click += BtnPrint_Click;
            btnPrintItem.Click += BtnPrintItem_Click;
            rbTimerStart.Click += RbTimerStart_Click;
        }
        private void RbTimerStart_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (rbTimerStart.Text.Equals("Start"))
            {
                rbTimerStart.Text = "Stop";
                rbTimerStart.SmallImage = Resources.start128;
                timeImgDrugINward.Start();
            }
            else
            {
                rbTimerStart.Text = "Start";
                rbTimerStart.SmallImage = Resources.stop_red48;
                timeImgDrugINward.Stop();
            }
        }

        private void BtnPrintItem_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String printer = LocalPrintServer.GetDefaultPrintQueue().FullName;
            lfSbStatus.Text = bc.iniC.printerStickerDrug;
            if (!printer.Equals(bc.iniC.printerStickerDrug))
            {
                lfSbMessage.Text = "change Printer " + LocalPrintServer.GetDefaultPrintQueue().FullName+" to "+ bc.iniC.printerStickerDrug;
                SetDefaultPrinter(bc.iniC.printerStickerDrug);
            }
            setReportStricker(ref DTSTCDRUG);
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printStickerDrug();
        }
        private void printStickerDrug()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerStickerDrug;
            document.PrintPage += Document_PrintPage_Sticker_Drug;
            document.DefaultPageSettings.Landscape = false;
            if (int.TryParse("1", out int num))
            {
                document.PrinterSettings.Copies = short.Parse(num.ToString());
                foreach (DataRow drow in DTSTCDRUG.Rows)
                {
                    //if (!drow["MNC_PH_cd"].ToString().Equals("FA014")) continue;
                    DROWDRUG = drow;
                    for (int i = 0; i < num; i++) { document.Print(); }
                }
            }

            PrintDocument documentSum = new PrintDocument();
            documentSum.PrinterSettings.PrinterName = bc.iniC.printerStickerDrug;
            documentSum.PrintPage += Document_PrintPage_Sticker_DrugSum;
            documentSum.DefaultPageSettings.Landscape = false;
            num = 0;
            if (int.TryParse("1", out num))
            {
                documentSum.PrinterSettings.Copies = short.Parse(num.ToString());
                for (int i = 0; i < num; i++){ documentSum.Print(); }
            }
        }
        private void Document_PrintPage_Sticker_Drug(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, ydate = 0, gapline = 17, col1 = 10, col2 = 60, col4 = 200;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps

            String text = "";
            if (DTSTCDRUG.Rows.Count > 0)
            {
                e.Graphics.DrawString(bc.iniC.hostname, fPrnBil, Brushes.Black, col2, yPos, flags);
                e.Graphics.DrawString(bc.iniC.hosttel, fPrnBil, Brushes.Black, col4, yPos, flags);
                yPos += gapline;
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fPrnBil, Brushes.Black, col2, yPos, flags);
                e.Graphics.DrawString("HN " + HN, fEditB, Brushes.Black, col4, yPos, flags);
                yPos += gapline;
                e.Graphics.DrawString(rbPttName.Text, famtB, Brushes.Black, col1, yPos, flags);
                yPos += gapline + 15;
                e.Graphics.DrawString(DROWDRUG["MNC_PH_DIR_DSC"].ToString(), fEdit, Brushes.Black, col1, yPos, flags);
                yPos += gapline;                yPos += gapline;                yPos += gapline;                yPos += gapline;
                yPos += gapline - 5;
                e.Graphics.DrawString(DROWDRUG["MNC_PH_TN"].ToString() + " [" + DROWDRUG["MNC_PH_QTY_PAID"].ToString() + " " + DROWDRUG["MNC_PH_UNT_CD"].ToString() + "]", fEdit, Brushes.Black, col1, yPos, flags);
            }
        }
        private void Document_PrintPage_Sticker_DrugSum(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, ydate = 0, gapline = 14, gapline1 = 17, col1 = 10, col2 = 60, col3 = 140, col4 = 200;

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            StringFormat flagsr = new StringFormat(StringFormatFlags.DirectionRightToLeft);  //wraps
            String text = "", chk = "";
            if (DTSTCDRUG.Rows.Count > 0)
            {
                e.Graphics.DrawString(bc.iniC.hostname, fPrnBil, Brushes.Black, col2 + 15, yPos, flags);
                e.Graphics.DrawString(bc.iniC.hosttel, fPrnBil, Brushes.Black, col4, yPos, flags);
                yPos += gapline1;
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fPrnBil, Brushes.Black, col2 + 15, yPos, flags);
                e.Graphics.DrawString("HN " + HN, fEditB, Brushes.Black, col4, yPos, flags);
                yPos += gapline1;
                
                e.Graphics.DrawString(rbPttName.Text, famtB, Brushes.Black, col1, yPos, flags);
                yPos += gapline;
                yPos += gapline;
                float price = 0, qty = 0, amt = 0, sum = 0;
                foreach (DataRow drow in DTSTCDRUG.Rows)
                {
                    text = drow["MNC_PH_cd"].ToString();
                    float.TryParse(drow["MNC_PH_QTY_PAID"].ToString(), out qty);
                    float.TryParse(drow["MNC_PH_PRI"].ToString(), out price);
                    amt = price * qty;
                    sum += amt;
                    e.Graphics.DrawString(drow["MNC_PH_TN"].ToString(), fEditS, Brushes.Black, col1, yPos, flags);
                    e.Graphics.DrawString(qty.ToString("###"), fEditS, Brushes.Black, col3 + 100, yPos, flags);
                    e.Graphics.DrawString(amt.ToString("#.00"), fEditS, Brushes.Black, col3 + 130, yPos, flags);
                    e.Graphics.DrawString(drow["MNC_PH_UNT_CD"].ToString(), fEditS, Brushes.Black, col4, yPos, flags);
                    yPos += gapline;
                    //yPos = 10;
                    //e.HasMorePages = true;
                }
                e.Graphics.DrawString("รวม. " + sum.ToString("#,00"), fEditB, Brushes.Black, col4 + 25, 48, flags);
            }
            //string chk1 = "";
        }
        private void setReportStricker(ref DataTable dtdrug)
        {
            Boolean chk = false;
            FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\drug_stricker.rdlx");
            if (!File.Exists(rptPath.FullName))
            {
                lfSbMessage.Text = "File report not found";
                return;
            }
            int i = 1;
            DateTime dt = DateTime.Now;
            if (dt.Year < 1900)
            {
                dt = dt.AddYears(543);
            }
            String datetime = "";
            datetime = dt.ToString("dd-MM")+"-"+ (dt.Year+543)+" "+ dt.ToString("HH:mm:ss");
            foreach (DataRow drow in dtdrug.Rows)
            {
                drow["hostname"] = bc.iniC.hostname;
                drow["tel"] = "โทร 02 138 1155-64";
                drow["drug_using"] = drow["drug_using"].ToString().Replace("/", "").Trim();
                drow["printdatetime"] = datetime;
                //drow["drugname_t"] = drow["drugname_t"].ToString().Trim()+" "+ drow["unitqty"].ToString().Trim();
            }
            frmDrugStricker.DT = dtdrug;
            if (bc.iniC.statusPrintSticker.Equals("0"))
            {
                frmDrugStricker.ShowDialog(this);
            }
            else
            {
                if (frmDrugStricker.PrintReport())
                {
                    if(dtdrug.Rows.Count>0)
                        bc.bcDB.pharT05DB.updateStatusPrintStrickered(dtdrug.Rows[0]["MNC_DOC_CD"].ToString(), dtdrug.Rows[0]["MNC_CFR_YR"].ToString(), dtdrug.Rows[0]["MNC_CFR_NO"].ToString(), dtdrug.Rows[0]["MNC_CFG_DAT"].ToString());
                }
            }
        }
        private void TcOPD_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tcOPD.SelectedTab == tabOPDQue) { TCOPDACTIVE = tabOPDQue.Name; }
            else if (tcOPD.SelectedTab == tabOPDFinish) { TCOPDACTIVE = tabOPDFinish.Name; setGrfOPDFinishList(); }
            else if (tcOPD.SelectedTab == tabOPDDispensing) { TCOPDACTIVE = tabOPDDispensing.Name; }
        }

        private void TC1_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tC1.SelectedTab == tabDrugWard) {TC1ACTIVE = tabDrugWard.Name;}

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
        private void TimeImgDrugINward_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TIMERCNT++;
            if (TIMERCNT % 9==0)
            {
                if (TC1ACTIVE.Equals(tabDrugWard.Name)) { /*setDrugINimgList();*/ }
                else if (TC1ACTIVE.Equals(tabOPD.Name))
                {
                    if (TCOPDACTIVE.Equals(tabOPDFinish.Name))
                    {
                        setGrfOPDFinishList();
                    }
                }
            }
            else if (TIMERCNT % 2 == 0)
            {
                DTSTCDRUG = bc.bcDB.pharT06DB.selectByCFRNoDrug();
                if (DTSTCDRUG.Rows.Count > 0)
                {
                    DTSTCDRUG = bc.bcDB.pharT06DB.selectByCFRNoDrug1(DTSTCDRUG.Rows[0]["MNC_CFR_YR"].ToString(), DTSTCDRUG.Rows[0]["MNC_CFR_NO"].ToString(), DTSTCDRUG.Rows[0]["MNC_DOC_CD"].ToString(), DTSTCDRUG.Rows[0]["MNC_CFG_DAT"].ToString());
                    setReportStricker(ref DTSTCDRUG);
                }
            }
        }
        private void setDrugINimgList()
        {
            try
            {
                timeImgDrugINward.Stop();
                lfSbMessage.Text = "setDrugINimgList";
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
            finally
            {
                lfSbMessage.Text = "";
                hideLbLoading();
            }
            timeImgDrugINward.Start();
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
            
            timeImgDrugINward.Enabled = true;
            timeImgDrugINward.Stop();
            autoDrug = bc.bcDB.pharM01DB.getlDrugAll();            
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            pnDrugINimg.Dock = DockStyle.Fill;
            picDrugIN.Dock = DockStyle.Fill;
            picDrugIN.SizeMode = PictureBoxSizeMode.StretchImage;
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String stationname = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);
            lfSbStation.Text = DEPTNO + "[" + bc.iniC.station + "]" + stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            this.Text = "Last Update 2024-03-04-5";
        }
    }
}
