using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmLabOutReceiveView : Form
    {
        BangnaControl bc;

        C1FlexGrid grfHn;
        Font fEdit, fEditB, fEditBig;
        int colDateReq = 1, colHN = 2, colFullName = 3, colVN=4, colDateReceive = 5, colReqNo = 6, colId = 7;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmLabOutReceiveView(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);

            btnOk.Click += BtnOk_Click;

            initGrf();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrf();
        }

        private void initGrf()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfHn.DoubleClick += GrfHn_DoubleClick;
            //grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            panel2.Controls.Add(grfHn);
            grfHn.Rows[0].Visible = false;
            grfHn.Cols[0].Visible = false;
            //theme1.SetTheme(grf, "Office2010Blue");

        }
        private void GrfHn_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String dscid = "";
            if (grfHn.Row <= 0) return;
            if (grfHn.Col <= 0) return;

            dscid = grfHn[grfHn.Row, colId].ToString();
            DocScan dsc = new DocScan();
            dsc = bc.bcDB.dscDB.selectByPk(dscid);
            if (!dsc.doc_scan_id.Equals(""))
            {
                if (!Directory.Exists("report"))
                {
                    Directory.CreateDirectory("report");
                }
                MemoryStream stream;
                int bufferSize = 2048;
                Stream ftpStream = null;
                String dgssid = "", filename = "", ftphost = "", id = "", folderftp = "", datetick="";
                datetick = DateTime.Now.Ticks.ToString();
                stream = new MemoryStream();
                FtpWebRequest ftpRequest = null;
                FtpWebResponse ftpResponse = null;
                FtpClient ftpc = new FtpClient(dsc.host_ftp, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                
                stream = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path);
                if (stream == null) return;
                if (stream.Length == 0) return;
                stream.Seek(0, SeekOrigin.Begin);
                var fileStream = new FileStream("report\\" + datetick + ".pdf", FileMode.Create, FileAccess.Write);
                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Dispose();
                Application.DoEvents();
                Thread.Sleep(200);

                Form frm = new Form();
                frm.Size = new Size(600, 800);
                frm.StartPosition = FormStartPosition.CenterScreen;
                Panel pn = new Panel();
                pn.Dock = DockStyle.Fill;
                frm.Controls.Add(pn);
                string currentDirectory = Directory.GetCurrentDirectory();
                WebBrowser webb = new WebBrowser();
                webb.Dock = DockStyle.Fill;
                webb.Name = "webBrowser1";
                pn.Controls.Add(webb);
                webb.Navigate(currentDirectory+"\\report\\" + datetick + ".pdf");

                frm.ShowDialog(this);
            }
            
        }
        private void setGrf()
        {
            String datestart = "", dateend = "", hn = "", txt = "";
            DataTable dt = new DataTable();
            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);

            dt = bc.bcDB.dscDB.selectLabOutByDateReceive(datestart, dateend);
            grfHn.Clear();
            grfHn.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfHn.Cols.Count = 8;
            grfHn.Cols[colDateReq].Caption = "Date Req";
            grfHn.Cols[colHN].Caption = "HN";
            grfHn.Cols[colFullName].Caption = "Name";
            grfHn.Cols[colDateReceive].Caption = "Date Rec";
            grfHn.Cols[colReqNo].Caption = "Req No";
            grfHn.Cols[colVN].Caption = "VN";
            grfHn.Cols[colId].Caption = "id";
            grfHn.Cols[colDateReq].Width = 100;
            grfHn.Cols[colHN].Width = 80;
            grfHn.Cols[colFullName].Width = 300;
            grfHn.Cols[colDateReceive].Width = 100;
            grfHn.Cols[colReqNo].Width = 80;
            grfHn.Cols[colVN].Width = 80;
            //grfHn.Cols[colHnPrnStaffNote].Width = 60;

            grfHn.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfHn.ContextMenu = menuGw;
            int i = 1;
            grfHn.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfHn[i, 0] = (i);
                    grfHn[i, colHN] = row["hn"].ToString();
                    grfHn[i, colFullName] = "";//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfHn[i, colDateReceive] = bc.datetoShow(row["date_create"].ToString());
                    grfHn[i, colDateReq] = bc.datetoShow(row["date_req"].ToString());
                    grfHn[i, colReqNo] = row["req_id"].ToString();
                    grfHn[i, colVN] = row["vn"].ToString();
                    grfHn[i, colId] = row["doc_scan_id"].ToString();
                }
                catch(Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView setGrf ex " + ex.Message);
                }
                i++;
            }
        }
        private void FrmLabOutReceiveView_Load(object sender, EventArgs e)
        {

        }
    }
}
