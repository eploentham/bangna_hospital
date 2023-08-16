using bangna_hospital.control;
using bangna_hospital.object1;
using C1.C1Excel;
using C1.C1Pdf;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Tile;
using C1.Win.FlexViewer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmExcel : Form
    {
        BangnaControl bc;

        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B;
        C1FlexGrid grfOutlab;
        C1TileControl TileRec;
        Group grRec;
        Template tempFlickr;
        ImageElement imageElement8;
        MemoryStream streamOutLab;
        public FrmExcel(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            btnOpenExcel.Click += BtnOpenExcel_Click;
            btnReadExcel.Click += BtnReadExcel_Click;
            btnDrugCOpenExcel.Click += BtnDrugCOpenExcel_Click;
            btnDrugCRead.Click += BtnDrugCRead_Click;

            btnPdfBrow.Click += BtnPdfBrow_Click;
            btnPdfRead.Click += BtnPdfRead_Click;
            btnPdfAddImg.Click += BtnPdfAddImg_Click;
            btnPdfDelImg.Click += BtnPdfDelImg_Click;
            btnPdfMergeImg.Click += BtnPdfMergeImg_Click;
            btnPdfMergePdf.Click += BtnPdfMergePdf_Click;
            btnOpenFolder.Click += BtnOpenFolder_Click;
            btnPdfMorgeBrow.Click += BtnPdfMorgeBrow_Click;
            btnOutlabBrow.Click += BtnOutlabBrow_Click;

            imageElement8 = new C1.Win.C1Tile.ImageElement();
            imageElement8.ImageLayout = C1.Win.C1Tile.ForeImageLayout.ScaleOuter;
            tempFlickr = new C1.Win.C1Tile.Template();
            tempFlickr.Elements.Add(imageElement8);

            TileRec = new C1TileControl();
            TileRec.Dock = DockStyle.Fill;
            TileRec.Orientation = LayoutOrientation.Vertical;
            TileRec.Templates.Add(tempFlickr);
            grRec = new Group();
            TileRec.Groups.Add(this.grRec);
            panel4.Controls.Add(TileRec);
        }

        private void BtnOutlabBrow_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtOutlabHN.Text.Length <= 0) return;
            Form frm = new Form();
            frm.Size = new Size(600, 300);
            frm.StartPosition = FormStartPosition.CenterScreen;
            grfOutlab = new C1FlexGrid();
            grfOutlab = new C1FlexGrid();
            grfOutlab.Font = fEdit;
            grfOutlab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOutlab.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfOutlab.Cols[0].Visible = false;
            grfOutlab.Cols[5].Visible = false;
            grfOutlab.Cols[6].Visible = false;
            grfOutlab.Rows.Count = 1;
            grfOutlab.Cols.Count = 7;
            grfOutlab.Cols[1].Caption = "ชื่อ";
            grfOutlab.Cols[2].Caption = "req date";
            grfOutlab.Cols[3].Caption = "VN";
            grfOutlab.Cols[4].Caption = "AN";
            
            grfOutlab.Cols[1].Width = 300;
            grfOutlab.Cols[2].Width = 120;
            grfOutlab.Cols[3].Width = 80;
            grfOutlab.Cols[4].Width = 80;
            grfOutlab.SelectionMode = SelectionModeEnum.Row;
            grfOutlab.Name = "grfOutlab";
            grfOutlab.Cols[0].AllowEditing = false;
            grfOutlab.Cols[1].AllowEditing = false;
            grfOutlab.Cols[2].AllowEditing = false;
            grfOutlab.Cols[3].AllowEditing = false;
            grfOutlab.DoubleClick += GrfOutlab_DoubleClick;
            ContextMenu menuGwOrder = new ContextMenu();
            menuGwOrder.MenuItems.Add("ต้องการ Merge OutLab ", new EventHandler(ContextMenu_Outlab_Merge));
            grfOutlab.ContextMenu = menuGwOrder;
            frm.Controls.Add(grfOutlab);

            DataTable dtoutlab = new DataTable();
            dtoutlab = bc.bcDB.dscDB.selectLabOutByHn(txtOutlabHN.Text.Trim());
            int i = 0;
            decimal aaa = 0;
            //pB1.Maximum = dt.Rows.Count;
            try
            {
                String labname = "", labnameold = "", reqno = "", reqnoold = "";
                grfOutlab.Rows.Count = dtoutlab.Rows.Count+1;
                foreach (DataRow row1 in dtoutlab.Rows)
                {
                    i++;
                    grfOutlab[i, 0] = row1["doc_scan_id"].ToString();
                    grfOutlab[i, 1] = row1["patient_fullname"].ToString();
                    grfOutlab[i, 2] = row1["date_req"].ToString();
                    grfOutlab[i, 3] = row1["vn"].ToString();
                    grfOutlab[i, 4] = row1["an"].ToString();
                    grfOutlab[i, 5] = row1["folder_ftp"].ToString();
                    grfOutlab[i, 6] = row1["image_path"].ToString();

                    //row1[0] = (i - 2);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmExcel BtnOutlabBrow_Click grfOutlab " + ex.Message);
            }
            frm.ShowDialog(this);
        }
        private void GrfOutlab_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfOutlab == null) return;
            if (grfOutlab.Rows.Count <=0) return;
            if (grfOutlab[grfOutlab.Row, 0].ToString().Length<=0) return;

            String folderftp = "", imagepath = "";
            folderftp = grfOutlab[grfOutlab.Row, 5].ToString();
            imagepath = grfOutlab[grfOutlab.Row, 6].ToString();

            Form frm = new Form();
            frm.Size = new Size(1000, 800);
            frm.StartPosition = FormStartPosition.CenterScreen;

            C1FlexViewer fview = new C1FlexViewer();
            bc.setControlC1FlexViewer(ref fview, "fviewoutlab");
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            streamOutLab = null;

            if (!Directory.Exists("report"))
            {
                Directory.CreateDirectory("report");
            }
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            streamOutLab = ftpc.download(folderftp + "/" + imagepath);
            if ((streamOutLab != null) && (streamOutLab.Length > 0))
            {
                streamOutLab.Seek(0, SeekOrigin.Begin);
                pds.LoadFromStream(streamOutLab);
                fview.DocumentSource = pds;
            }
            frm.Controls.Add(fview);
            frm.ShowDialog(this);
        }
        private void ContextMenu_Outlab_Merge(object sender, System.EventArgs e)
        {
            if (streamOutLab == null) return;
            try
            {
                FileInfo file = new FileInfo(txtPdfPath.Text);
                String filenamenew = "";
                filenamenew = file.Directory.FullName + "\\" + file.Name.Replace(file.Extension, "") + "_1" + file.Extension;
                if (File.Exists(filenamenew)) File.Delete(filenamenew);
                PdfControl pdfc = new PdfControl();
                pdfc.MergeFileslab(filenamenew, txtPdfPath.Text.Trim(), streamOutLab);
                string argument = "/select, \"" + filenamenew + "\"";

                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            catch (Exception ex)
            {
                //Response.Write(e.Message);
            }
        }
        private void BtnPdfMorgeBrow_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "pdf",
                Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPdfMergePath.Text = openFileDialog1.FileName;
            }
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Process.Start("explorer.exe", bc.iniC.pathDownloadFile);
        }

        private void BtnPdfMergePdf_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtPdfPath.Text.Length <= 0) return;
            if (txtPdfMergePath.Text.Length <= 0) return;

            FileInfo file = new FileInfo(txtPdfPath.Text);

            PdfControl pdfc = new PdfControl();
            String[] files;
            files = new string[2];
            files[0] = txtPdfPath.Text;
            files[1] = txtPdfMergePath.Text;
            String filenamenew = "";
            filenamenew = file.Directory.FullName + "\\" + file.Name.Replace(file.Extension, "") + "_1" + file.Extension;
            if (File.Exists(filenamenew)) File.Delete(filenamenew);
            pdfc.MergeFileslab1(file.Directory.FullName + "\\" + file.Name.Replace(file.Extension, "") + "_1" + file.Extension, files);

            Process.Start("explorer.exe", file.FullName + "_1" + file.Extension);
        }

        private void BtnPdfMergeImg_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtPdfPath.Text.Length <= 0) return;

            //C1PdfDocumentSource pds = new C1PdfDocumentSource();
            //pds.LoadFromFile(txtPdfPath.Text);
            //C1PdfDocument aaa = new C1PdfDocument();



            //using (Stream inputPdfStream = new FileStream(txtPdfPath.Text, FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (Stream outputPdfStream = new FileStream(file.FullName + "_1" + file.Extension, FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    var reader = new PdfReader(inputPdfStream);
            //    var stamper = new PdfStamper(reader, outputPdfStream);
            //    var pdfContentByte = stamper.GetOverContent(1);
            //foreach (Tile tile in TileRec.Groups[0].Tiles)
            //{
            //    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(tile.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    pic.SetAbsolutePosition(100, 100);

            //}
            //    stamper.Close();
            //}
            
            FileInfo file = new FileInfo(txtPdfPath.Text);
            String filenamenew = "";
            filenamenew = file.Directory.FullName + "\\" + file.Name.Replace(file.Extension, "")+"_1"+file.Extension;
            if (File.Exists(filenamenew)) File.Delete(filenamenew);
            using (Stream inputPdfStream = new FileStream(txtPdfPath.Text, FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (Stream inputImageStream = new FileStream("some_image.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(filenamenew, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                int pagenumber=0;
                pagenumber = reader.NumberOfPages;
                foreach (Tile tile in TileRec.Groups[0].Tiles)
                {
                    pagenumber++;
                    iTextSharp.text.Rectangle rectangle = reader.GetPageSize(1);


                    iTextSharp.text.Rectangle pageSize = null;
                    
                    using (var srcImage = new Bitmap(tile.Image))
                    {
                        pageSize = new iTextSharp.text.Rectangle(0, 0, PageSize.A4.Width, PageSize.A4.Height);
                    }

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bc.ResizeImage(tile.Image, int.Parse(pageSize.Width.ToString()), int.Parse(pageSize.Height.ToString())), ImageFormat.Jpeg);
                    //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(tile.Image, ImageFormat.Jpeg);
                    image.SetAbsolutePosition(0, 0);
                    
                    stamper.InsertPage(pagenumber, pageSize);
                    var pdfContentByte = stamper.GetOverContent(pagenumber);
                    pdfContentByte.AddImage(image);
                }
                stamper.Close();
            }
            txtPdfPath.Text = filenamenew;
            BtnPdfRead_Click(null, null);
        }

        private void BtnPdfDelImg_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TileRec.Groups[0].Tiles.Clear();
        }

        private void BtnPdfAddImg_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                 FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            var codecs = ImageCodecInfo.GetImageEncoders();
            var codecFilter = "Image Files|";
            foreach (var codec in codecs)
            {
                codecFilter += codec.FilenameExtension + ";";
            }
            openFileDialog1.Filter = codecFilter;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TileRec.BeginUpdate();
                var tile = new Tile();
                tile.HorizontalSize = 3;
                tile.VerticalSize = 4;
                tile.Template = tempFlickr;
                tile.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                tile.Text1 = openFileDialog1.FileName;
                TileCollection tiles1 = TileRec.Groups[0].Tiles;
                //tiles1.Clear(true);
                tiles1.Add(tile);
                TileRec.EndUpdate();
            }
        }

        private void BtnPdfRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            pds.LoadFromFile(txtPdfPath.Text);
            c1FlexViewer1.DocumentSource = pds;
            
        }

        private void BtnPdfBrow_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = bc.iniC.pathDownloadFile,
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "pdf",
                Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPdfPath.Text = openFileDialog1.FileName;
                BtnPdfRead_Click(null, null);
            }
        }

        private void BtnDrugCRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1.C1Excel.C1XLBook _c1xl = new C1.C1Excel.C1XLBook();
            _c1xl.Load(txtDrugCPathExcel.Text);
            XLSheet sheet = _c1xl.Sheets[0];

            for (int i = 0; i < sheet.Rows.Count; i++)
            {
                int chk = 0;
                String hosdrugcode = "", tmtcode = "", name = "", cid = "", sql = "", datestart = "", dateend = "";
                DateTime datestart1, dateend1;
                hosdrugcode = sheet[i, 0].Text;
                tmtcode = sheet[i, 2].Text;
                if ((hosdrugcode.Length > 0) && (tmtcode.Length>0))
                {
                    sql = "update pharmacy_m01 set tmt_code_opbkk = '"+tmtcode+"' Where mnc_ph_cd = '"+hosdrugcode+"' ";
                    String re = bc.conn.ExecuteNonQuery(bc.conn.connMainHIS, sql);
                    if(int.TryParse(re, out chk))
                    {
                        label1.Text = (int.Parse(label1.Text)+ chk).ToString();
                    }
                    else
                    {
                        label2.Text += (int.Parse(label2.Text)).ToString();
                    }
                }
                Application.DoEvents();
            }
        }

        private void BtnDrugCOpenExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xlsx",
                Filter = "excel files (*.xls)|*.xls",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDrugCPathExcel.Text = openFileDialog1.FileName;
            }
        }

        private void BtnReadExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1.C1Excel.C1XLBook _c1xl = new C1.C1Excel.C1XLBook();
            _c1xl.Load(txtPath.Text);
            XLSheet sheet = _c1xl.Sheets[0];

            for(int i =0; i < sheet.Rows.Count; i++)
            {
                int chk = 0;
                String no = "", vsdate = "", name = "", cid = "", sql="", datestart="", dateend="";
                DateTime datestart1, dateend1;
                no = sheet[i, 0].Text;
                if(int.TryParse(no, out chk))
                {
                    vsdate = sheet[i, 1].Text;
                    name = sheet[i, 2].Text.Trim();
                    cid = sheet[i, 5].Text.Replace("'", "");
                    if (cid.Length > 0)
                    {
                        sheet[i, 8].Value = "cid ok";
                        if (vsdate.Length >= 10)
                        {
                            datestart = vsdate.Substring(vsdate.Length-4, 4)+"-"+vsdate.Substring(3, 2)+"-"+ vsdate.Substring(0,2);
                            DateTime.TryParse(datestart, out datestart1);
                            DateTime.TryParse(datestart, out dateend1);
                            datestart1 = datestart1.AddDays(-3);
                            dateend1 = dateend1.AddDays(5);
                            datestart = datestart1.ToString("yyyy-MM-dd");
                            dateend = dateend1.ToString("yyyy-MM-dd");
                            sheet[i, 9].Value = "date ok";
                            sheet[i, 10].Value = datestart;
                            sheet[i, 11].Value = dateend;
                        }
                        
                        sql = "Select pt01.mnc_hn_no, labt05.MNC_LB_RES_CD,labt05.MNC_LB_CD,labt05.MNC_RES_VALUE , convert(VARCHAR(20),labt05.MNC_STAMP_DAT,23) as MNC_STAMP_DAT "
                        + "From bng5_dbms_front.dbo.patient_m01 pm01 " +
                        "inner Join patient_t01 pt01 on pm01.mnc_hn_no = pt01.mnc_hn_no and pm01.mnc_hn_yr = pt01.mnc_hn_yr " +
                        "left join LAB_T01 labt01 ON pt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pt01.MNC_DATE = labt01.MNC_DATE and pt01.MNC_hn_NO = labt01.MNC_hn_NO and pt01.MNC_hn_yr = labt01.MNC_hn_yr " +
                        "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT  " +
                        "inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  " +
                        "Where pm01.MNC_ID_NO= '" + cid + "' and labt02.MNC_LB_CD in ('SE629','SE184','SE640','SE649','SE650')  and pt01.mnc_date >= '"+ datestart + "' and pt01.mnc_date <= '"+ dateend + "'  " +
                        "Order By labt05.MNC_LB_RES_CD ";
                        DataTable dt = bc.conn.selectData(bc.conn.connMainHIS, sql);
                        if (dt.Rows.Count > 0)
                        {
                            sheet[i, 12].Value = dt.Rows[0][0].ToString();
                            sheet[i, 13].Value = dt.Rows[0][1].ToString();
                            sheet[i, 14].Value = dt.Rows[0][2].ToString();
                            sheet[i, 15].Value = dt.Rows[0][3].ToString();
                            sheet[i, 16].Value = dt.Rows[0][4].ToString();
                        }
                        else
                        {

                            sql = "Select mnc_hn_no " +
                                "From bng5_dbms_front.dbo.patient_m01 pm01 " +
                                //"inner Join patient_t01 pt01 on pm01.mnc_hn_no = pt01.mnc_hn_no and pm01.mnc_hn_yr = pt01.mnc_hn_yr " +
                                "Where pm01.MNC_FNAME_T like '%" + (name.Replace("MR","").Replace("MR.", "").Replace("Mr", "").Replace("Mr.", "").Replace("คุณ", "")).Replace(".", "").Trim() + "%'   " +
                                "";
                            DataTable dthn = bc.conn.selectData(bc.conn.connMainHIS, sql);
                            if (dthn.Rows.Count > 0)
                            {
                                sheet[i, 17].Value = dthn.Rows[0][0].ToString();
                                sql = "Select pt01.mnc_hn_no, labt05.MNC_LB_RES_CD,labt05.MNC_LB_CD,labt05.MNC_RES_VALUE , convert(VARCHAR(20),labt05.MNC_STAMP_DAT,23) as MNC_STAMP_DAT "
                                    + "From bng5_dbms_front.dbo.patient_m01 pm01 " +
                                    "inner Join patient_t01 pt01 on pm01.mnc_hn_no = pt01.mnc_hn_no and pm01.mnc_hn_yr = pt01.mnc_hn_yr " +
                                    "left join LAB_T01 labt01 ON pt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pt01.MNC_DATE = labt01.MNC_DATE and pt01.MNC_hn_NO = labt01.MNC_hn_NO and pt01.MNC_hn_yr = labt01.MNC_hn_yr " +
                                    "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT  " +
                                    "inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  " +
                                    "Where pm01.MNC_hn_NO = '" + dthn.Rows[0][0].ToString() + "' and labt02.MNC_LB_CD in ('SE629','SE184','SE640','SE649','SE650')  and pt01.mnc_date >= '" + datestart + "' and pt01.mnc_date <= '" + dateend + "'  " +
                                    "Order By labt05.MNC_LB_RES_CD ";
                                DataTable dtchk = bc.conn.selectData(bc.conn.connMainHIS, sql);
                                if (dtchk.Rows.Count > 0)
                                {
                                    sheet[i, 18].Value = dtchk.Rows[0][0].ToString();
                                    sheet[i, 19].Value = dtchk.Rows[0][1].ToString();
                                    sheet[i, 20].Value = dtchk.Rows[0][2].ToString();
                                    sheet[i, 21].Value = dtchk.Rows[0][3].ToString();
                                    sheet[i, 22].Value = dtchk.Rows[0][4].ToString();
                                }
                                else
                                {
                                    sql = "select MNC_AN_NO from patient_t08 where MNC_hn_NO = '" + dthn.Rows[0][0].ToString() + "' and mnc_date >= '" + datestart + "' and mnc_date <= '" + dateend + "'  ";
                                    DataTable dtadmit = bc.conn.selectData(bc.conn.connMainHIS, sql);
                                    if (dtadmit.Rows.Count > 0)
                                    {
                                        sheet[i, 23].Value = "admit "+dtchk.Rows[0][0].ToString();
                                    }
                                }
                            }
                        }
                    }

                }
                
            }
            _c1xl.Save(txtPath.Text);
        }

        private void BtnOpenExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xlsx",
                Filter = "excel files (*.xlsx)|*.xlsx",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void FrmExcel_Load(object sender, EventArgs e)
        {
            c1SplitContainer1.HeaderHeight = 0;
            c1FlexViewer1.Ribbon.Minimized = true;
            this.Text = "Last Update 2023-05-22";
        }
    }
}
