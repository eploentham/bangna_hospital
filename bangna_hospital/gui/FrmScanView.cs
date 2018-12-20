using bangna_hospital.control;
using bangna_hospital.object1;
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

        C1FlexGrid grf;
        Font fEdit, fEditB;

        int colId=1, colPic1 = 2, colVn = 3, colVisitDate=4, colDocGroup = 5, colRowNo = 6, colImagePath=7;
        ArrayList array1 = new ArrayList();
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
            //timer1 = new Timer();
            //int chk = 0;
            //int.TryParse(bc.iniC.timerImgScanNew, out chk);
            //timer1.Interval = chk;
            //timer1.Enabled = true;
            //timer1.Tick += Timer1_Tick;
            //timer1.Stop();

            theme1.SetTheme(sb1, "ExpressionDark");
            theme1.SetTheme(groupBox1, "ExpressionDark");
            theme1.SetTheme(grfScan, "ExpressionDark");
            foreach (Control con in groupBox1.Controls)
            {
                theme1.SetTheme(con, "ExpressionDark");
            }
            foreach (Control con in grfScan.Controls)
            {
                theme1.SetTheme(con, "ExpressionDark");
            }
            initGrf();
            setGrf();
            

            btnHn.Click += BtnHn_Click;
            btnOpen.Click += BtnOpen_Click;
            btnRefresh.Click += BtnRefresh_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
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
            grf.AutoSizeCols();
            grf.AutoSizeRows();
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
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

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

            theme1.SetTheme(grf, "ExpressionDark");

        }
        private void setGrf()
        {
            grf.Clear();
            grf.Rows.Count = 1;
            grf.Cols.Count = 8;
            String txt = "";
            foreach(DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            {
                txt += "|" + dgs.doc_group_name;
            }
            C1TextBox text = new C1TextBox();
            grf.Cols[colVn].Editor = text;
            C1ComboBox cbo = new C1ComboBox();
            bc.bcDB.dgsDB.setCboBsp(cbo, "");
            Column colpic1 = grf.Cols[colPic1];
            colpic1.DataType = typeof(Image);
            Column colDocGrp = grf.Cols[colDocGroup];
            colDocGrp.DataType = typeof(String);
            colDocGrp.ComboList = txt;
            Column colVisitDate1 = grf.Cols[colVisitDate];
            colVisitDate1.DataType = typeof(DateTime);
            //cs.Format = "dd-MMM-yy";
            //Column colpic3 = grf.Cols[colDocGroup];
            //colpic3.DataType = typeof(Image);
            //Column colpic4 = grf.Cols[colRowNo];
            //colpic4.DataType = typeof(Image);
            grf.Cols[colPic1].Width = 100;
            grf.Cols[colVn].Width = 800;
            grf.Cols[colDocGroup].Width = 100;
            grf.Cols[colRowNo].Width = 100;
            grf.Cols[colVisitDate].Width = 100;
            grf.ShowCursor = true;
            grf.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grf.Cols[colPic1].AllowMerging = true;
            grf.Cols[colRowNo].AllowMerging = true;
            grf.Cols[colDocGroup].AllowMerging = true;
            grf.Cols[colVn].AllowMerging = true;
            if (txtHn.Text.Equals(""))
                return;
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByHn(txtHn.Text);
            int i = 1, j = 1, row = grf.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            foreach (DataRow row1 in dt.Rows)
            {
                
                if (row1[bc.bcDB.dscDB.dsc.image_path] != null && !row1[bc.bcDB.dscDB.dsc.image_path].ToString().Equals(""))
                {
                    //txtName.Value += row1[bc.bcDB.dscDB.dsc.image_path].ToString();
                    Row rowa = grf.Rows.Add();
                    rowa[colId] = row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                    rowa[colRowNo] = row1[bc.bcDB.dscDB.dsc.row_no].ToString();
                    rowa[colVn] = row1[bc.bcDB.dscDB.dsc.vn].ToString();
                    rowa[colVisitDate] =  bc.datetoShow(row1[bc.bcDB.dscDB.dsc.visit_date].ToString());
                    rowa[colDocGroup] = bc.bcDB.dgsDB.getNameDgs(row1[bc.bcDB.dscDB.dsc.doc_group_id].ToString());
                    rowa[colImagePath] = row1[bc.bcDB.dscDB.dsc.image_path].ToString();
                    new Thread(() =>
                    {                            
                        row = grf.Rows.Count;
                        
                        Thread.CurrentThread.IsBackground = true;
                        Image loadedImage = null, resizedImage;
                        String aaa = row1[bc.bcDB.dscDB.dsc.image_path].ToString();
                        FtpWebRequest ftpRequest = null;
                        FtpWebResponse ftpResponse = null;
                        Stream ftpStream = null;
                        int bufferSize = 2048;
                        MemoryStream stream = new MemoryStream();
                        string host = null;
                        string user = null;
                        string pass = null;     //iniC.hostFTP, iniC.userFTP, iniC.passFTP
                        host = bc.iniC.hostFTP; user = bc.iniC.userFTP; pass = bc.iniC.passFTP;
                        try
                        {
                            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + aaa);
                            ftpRequest.Credentials = new NetworkCredential(user, pass);
                            ftpRequest.UseBinary = true;
                            ftpRequest.UsePassive = false;
                            ftpRequest.KeepAlive = true;
                            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                            ftpStream = ftpResponse.GetResponseStream();
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
                            }
                            loadedImage = new Bitmap(stream);
                            ftpStream.Close();
                            ftpResponse.Close();
                            ftpRequest = null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                                                
                        if (loadedImage != null)
                        {
                            int originalWidth = loadedImage.Width;
                            int newWidth = 280;
                            resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                            
                            rowa[colPic1] = resizedImage;
                            
                        }
                        //j++;
                    }).Start();
                }
            }
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            grf.ContextMenu = menuGw;
            grf.Cols[colId].Visible = false;
            grf.Cols[colImagePath].Visible = false;
            //row1[colVSE2] = row[ic.ivfDB.pApmDB.pApm.e2].ToString().Equals("1") ? imgCorr : imgTran;
            grf.AutoSizeCols();
            grf.AutoSizeRows();
            grf.Refresh();
            theme1.SetTheme(grf, "ExpressionDark");
        }
        private void ContextMenu_Void(object sender, System.EventArgs e)
        {
            String id = "", colImagePath1 = "";
            id = grf[grf.Row, colId] !=null ? grf[grf.Row, colId].ToString() : "";
            if (MessageBox.Show("ต้องการ ยกเลิก ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                String re = "";
                int chk = 0;
                bc.cStf.staff_id = "";
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog(this);
                if (!bc.cStf.staff_id.Equals(""))
                {
                    colImagePath1 = grf[grf.Row, colImagePath] != null ? grf[grf.Row, colImagePath].ToString() : "";
                    re = bc.bcDB.dscDB.voidDocScan(id, bc.user.staff_id);
                    String[] sur = colImagePath1.Split('.');
                    String ex = "", filenew="";
                    if (sur.Length == 2)
                    {
                        ex = sur[1];
                        filenew = sur[0];
                        filenew = filenew + "_old";
                    }
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    ftp.rename(colImagePath1,filenew + "."+ ex);
                    if (int.TryParse(re, out chk))
                    {
                        setGrf();
                    }
                }
            }
        }
        private void ContextMenu_Update(object sender, System.EventArgs e)
        {
            String id = "", vn="", visitDate="", docGrp="", colRowNo1="", colImagePath1="";
            id = grf[grf.Row, colId] != null ? grf[grf.Row, colId].ToString() : "";
            if (MessageBox.Show("ต้องการ ยกเลิก ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                String re = "";
                int chk = 0;
                bc.cStf = new Staff();
                bc.cStf.staff_id = "";
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog(this);
                if (!bc.cStf.staff_id.Equals(""))
                {
                    vn = grf[grf.Row, colVn] != null ? grf[grf.Row, colVn].ToString() : "";
                    visitDate = grf[grf.Row, colVisitDate] != null ? grf[grf.Row, colVisitDate].ToString() : "";
                    docGrp = grf[grf.Row, colDocGroup] != null ? grf[grf.Row, colDocGroup].ToString() : "";
                    colRowNo1 = grf[grf.Row, colRowNo] != null ? grf[grf.Row, colRowNo].ToString() : "";
                    colImagePath1 = grf[grf.Row, colImagePath] != null ? grf[grf.Row, colImagePath].ToString() : "";

                    DocScan dsc = new DocScan();
                    dsc.doc_scan_id = id;
                    dsc.doc_group_id = bc.bcDB.dgsDB.getIdDgs(docGrp);
                    dsc.hn = txtHn.Text;
                    dsc.vn = vn;
                    dsc.visit_date = bc.datetoDB(visitDate);
                    dsc.row_no = colRowNo1;
                    dsc.image_path = colImagePath1;
                    //dsc.visit_date = txtVisitDate.Text;

                    re = bc.bcDB.dscDB.insertDocScan(dsc, bc.user.staff_id);
                    if (int.TryParse(re, out chk))
                    {
                        setGrf();
                    }
                }
            }
        }
        private void FrmScanView_Load(object sender, EventArgs e)
        {

        }
    }
}
