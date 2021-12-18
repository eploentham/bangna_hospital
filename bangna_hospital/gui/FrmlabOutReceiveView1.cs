using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace bangna_hospital.gui
{
    public class FrmLabOutReceiveView1:Form
    {
        BangnaControl bc;

        C1FlexGrid grfHn, grfLabOut, grfMas, grfLabOutView;
        Font fEdit, fEditB, fEditBig;
        int colDateReq = 1, colHN = 2, colFullName = 3, colVN = 4, colDateReceive = 5, colReqNo = 6, colId = 7, colComp=8;
        int colHISDateReq = 1, colHISHN = 2, colHISFullName = 3, colHISVN = 4, colHISVsDate=5, colHISLabCode = 6, colHISLabName = 7, colHISReqNo = 8, colHISEdit=9, colHISpreno=10, colHISStatusRes=11, colHISDateResult=12, colHISPeriodResult=13, colHISid=14;
        int colMasId = 1, colMasCode = 2, colMasName = 3, colMasCompName=4, colMasPrice=5, colMasPeriod=6;

        C1DockingTab tC1;
        C1DockingTabPage tabSearch, tabLabOut, tabMasLabOut, tabLabOutView;
        Panel pnLabOut, panel1, panel2, panel3, pnLabOutTop, pnLabOutBotton, pnLabOutView, pnLabOutViewTop, pnLabOutViewBotton;
        C1TextBox txtHn, txtLabOutViewHn;
        C1Label lbtxtHn, lbtxtDateEnd, lbtxtDateStart, lbtxtDate, lbLabOutViewDate, lbLabOutViewHn;
        C1Button btnOk, btnHISSearch, btnImport, btnLabOutViewDate, btntabLabOutViewChk;
        C1DateEdit txtDateStart, txtDateEnd, txtDate, txtLabOutViewDate;
        RadioButton chkDateLabOut, chkDateReq, chkDateReqHIS;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;

        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private C1.Win.C1Themes.C1ThemeController theme1;
        Form frmFlash;
        Timer timerStt;
        Boolean flagImport = true;
        ConditionFilter _searchFilter = new ConditionFilter();

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmLabOutReceiveView1(BangnaControl bc)
        {
            showFormWaiting();
            this.bc = bc;
            initConfig();
            frmFlash.Dispose();
        }
        private void initConfig()
        {
            initCompoment();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            timerStt = new System.Windows.Forms.Timer();
            timerStt.Interval = 3000;
            timerStt.Enabled = true;
            timerStt.Stop();
            timerStt.Tick += TimerStt_Tick;

            this.Load += FrmlabOutReceiveView1_Load;
            btnOk.Click += BtnOk_Click;
            btnHISSearch.Click += BtnHISSearch_Click;
            btnImport.Click += BtnImport_Click;
            btnLabOutViewDate.Click += BtnLabOutViewDate_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            btntabLabOutViewChk.Click += BtntabLabOutViewChk_Click;
            txtLabOutViewHn.KeyUp += TxtLabOutViewHn_KeyUp;

            theme1.SetTheme(sb1, bc.iniC.themeApplication);
            theme1.SetTheme(tC1, bc.iniC.themeApplication);
            theme1.SetTheme(panel1, bc.iniC.themeApplication);
            theme1.SetTheme(panel2, bc.iniC.themeApplication);
            theme1.SetTheme(panel3, bc.iniC.themeApplication);
            foreach (Control con in panel1.Controls)
            {
                theme1.SetTheme(con, bc.iniC.themeApp);
            }
            //foreach (Control con in panel3.Controls)
            //{
            //    theme1.SetTheme(con, bc.iniC.themeApp);
            //}
            chkDateLabOut.Checked = true;
            initGrf();
            setGrf();
            initGrfLabOut();
            initGrfMas();
            setGrfMas();
            initGrfLabOutView();
            txtDateStart.Value = System.DateTime.Now;
            txtDateEnd.Value = System.DateTime.Now;
            txtLabOutViewDate.Value = System.DateTime.Now;
        }
        private void TxtLabOutViewHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                String hn = "", pttname = "";
                Patient ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatient(txtLabOutViewHn.Text.Trim());
                openNewForm(txtLabOutViewHn.Text.Trim(), ptt.Name);
            }
        }
        private void BtntabLabOutViewChk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            foreach(Row row in grfLabOutView.Rows)
            {
                if (row[colHISStatusRes] != null && row[colHISStatusRes].ToString().Equals(""))
                {
                    DataTable dt = new DataTable();
                    String hn = "", reqid = "", reqdate = "", labcode = "", preno = "", vsdate = "", reqno = "", id="";
                    id = row[colHISid] != null ? row[colHISid].ToString() : "";
                    hn = row[colHISHN] != null ? row[colHISHN].ToString() : "";
                    reqid = row[colHISDateReq] != null ? row[colHISDateReq].ToString() : "";
                    reqdate = row[colHISDateReq] != null ? row[colHISDateReq].ToString() : "";
                    labcode = row[colHISLabCode] != null ? row[colHISLabCode].ToString() : "";
                    preno = row[colHISpreno] != null ? row[colHISpreno].ToString() : "";
                    vsdate = row[colHISVsDate] != null ? row[colHISVsDate].ToString() : "";
                    reqno = row[colHISReqNo] != null ? row[colHISReqNo].ToString() : "";
                    vsdate = bc.datetoDB(vsdate);
                    reqdate = bc.datetoDB(reqdate);
                    dt = bc.bcDB.dscDB.selectLabOutByHnReqDateReqNo(hn, vsdate, preno, !reqdate.Equals("") ? reqdate : vsdate, reqno);
                    if (dt.Rows.Count > 0)
                    {
                        row[colHISStatusRes] = "OK";
                        row[colHISDateResult] = bc.datetoShow(dt.Rows[0]["date_create"].ToString());
                        bc.bcDB.laboDB.updateStatusResultByID(id);
                    }
                }
            }
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                setGrf();
            }
        }

        private void BtnLabOutViewDate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setgrfLabOutView();
        }

        private void TimerStt_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            stt.Hide();
            timerStt.Stop();
            //timerStt.Enabled = false;
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (flagImport)
            {
                setImport();
            }
            else
            {
                setVoid();
            }
        }
        private void setVoid()
        {
            String re = bc.bcDB.laboDB.voidByDate(bc.datetoDB(txtDate.Text), "");
           
            stt.Show("<p><b>Delete Success "+ re + "</b></p>", txtDate, 60, 30);
            Application.DoEvents();
            setHISSearch("auto");
            timerStt.Start();
        }
        private void setImport()
        {
            if (grfLabOut.Rows.Count <= 1) return;
            
            foreach (Row row in grfLabOut.Rows)
            {
                long chk = 0;
                if (row[colHISDateReq] == null) continue;
                if (row[colHISDateReq].ToString().Equals("Date Req")) continue;
                if (row[colHISEdit] == null) continue;
                if (!row[colHISEdit].ToString().Equals("1")) continue;
                //String datereq = "", hn = "", pttfullname = "", vn = "", labcode = "", labname = "", reqno = "";
                LabOut labo = new LabOut();
                labo.visit_date = bc.datetoDB(row[colHISDateReq].ToString());
                labo.hn = row[colHISHN].ToString();
                labo.patient_fullname = row[colHISFullName].ToString();
                labo.vn = row[colHISVN].ToString();
                labo.lab_code = row[colHISLabCode].ToString();
                labo.lab_name = row[colHISLabName].ToString();
                labo.req_no = row[colHISReqNo].ToString();
                labo.pre_no = row[colHISpreno].ToString();
                String re = bc.bcDB.laboDB.insertLabOut(labo);
                if (long.TryParse(re, out chk))
                {
                    //row.StyleNew.BackColor = Color.White;
                }
            }
            setHISSearch("");
            stt.Show("<p><b>Import Success</b></p>", txtDate, 30, 30);
            Application.DoEvents();
            timerStt.Start();
            //stt.Hide(txtDate);
        }
        private void showFormWaiting()
        {
            frmFlash = new Form();
            frmFlash.Size = new Size(300, 300);
            frmFlash.StartPosition = FormStartPosition.CenterScreen;
            C1PictureBox picFlash = new C1PictureBox();
            //Image img = new Image();
            picFlash.SuspendLayout();
            picFlash.Image = Resources.loading_transparent;
            picFlash.Width = 230;
            picFlash.Height = 230;
            picFlash.Location = new Point(30, 10);
            picFlash.SizeMode = PictureBoxSizeMode.StretchImage;
            frmFlash.Controls.Add(picFlash);
            picFlash.ResumeLayout();
            frmFlash.Show();
            Application.DoEvents();
        }
        private void BtnHISSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setHISSearch("auto");
        }
        private void initGrfMas()
        {
            grfMas = new C1FlexGrid();
            grfMas.Font = fEdit;
            grfMas.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMas.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfMas.DoubleClick += GrfMas_DoubleClick;
            //grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfMas.Rows.Count = 1;
            tabMasLabOut.Controls.Add(grfMas);
            grfMas.Rows[0].Visible = false;
            grfMas.Cols[0].Visible = false;
            theme1.SetTheme(grfMas, bc.iniC.themeApplication);

            //foreach (Control con in panel1.Controls)
            //{
            //    theme1.SetTheme(con, bc.iniC.themeApplication);
            //}
        }
        private void GrfMas_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfMas()
        {
            String datestart = "", dateend = "", hn = "", txt = "";
            DataTable dt = new DataTable();
            
            grfMas.Clear();
            grfMas.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfMas.Cols.Count = 7;
            grfMas.Cols[colMasCode].Caption = "Code";
            grfMas.Cols[colMasName].Caption = "Name";
            grfMas.Cols[colMasCompName].Caption = "Comp outLab Name";
            grfMas.Cols[colMasPrice].Caption = "Price";
            grfMas.Cols[colMasPeriod].Caption = "Period";

            grfMas.Cols[colMasCode].Width = 80;
            grfMas.Cols[colMasName].Width = 400;
            grfMas.Cols[colMasCompName].Width = 100;
            grfMas.Cols[colMasPrice].Width = 100;
            grfMas.Cols[colMasPeriod].Width = 60;
            //MessageBox.Show("1111", "");            
            dt = bc.bcDB.labbDB.selectAll();

            grfMas.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfHn.ContextMenu = menuGw;
            int i = 1;
            grfMas.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfMas[i, 0] = (i);
                    grfMas[i, colMasId] = row["lab_id"].ToString();
                    grfMas[i, colMasCode] = row["lab_code"].ToString();
                    grfMas[i, colMasName] = row["lab_name"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfMas[i, colMasCompName] = row["out_lab_comp_code"].ToString();
                    grfMas[i, colMasPrice] = row["out_lab_price"].ToString();
                    grfMas[i, colMasPeriod] = row["period_result"].ToString();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView setGrf ex " + ex.Message);
                }
                i++;
            }
            grfMas.Cols[colMasId].Visible = false;
            grfMas.Cols[colMasCode].AllowEditing = false;
            grfMas.Cols[colMasName].AllowEditing = false;
            grfMas.Cols[colMasCompName].AllowEditing = false;
            grfMas.Cols[colMasPrice].AllowEditing = false;
            grfMas.Cols[colMasPeriod].AllowEditing = false;
        }
        private void initGrfLabOutView()
        {
            grfLabOutView = new C1FlexGrid();
            grfLabOutView.Font = fEdit;
            grfLabOutView.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLabOutView.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfLabOutView.DoubleClick += GrfLabOutView_DoubleClick;
            //grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfLabOutView.Rows.Count = 1;
            pnLabOutViewBotton.Controls.Add(grfLabOutView);
            grfLabOutView.Rows[0].Visible = false;
            grfLabOutView.Cols[0].Visible = false;
            theme1.SetTheme(grfLabOutView, bc.iniC.themeApplication);

            //foreach (Control con in panel1.Controls)
            //{
            //    theme1.SetTheme(con, bc.iniC.themeApplication);
            //}
        }

        private void GrfLabOutView_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfLabOutView.Row <= 1) return;
            if (grfLabOutView.Col <= 0) return;
            showFormWaiting();

            String hn = "";
            hn = grfLabOutView[grfLabOutView.Row, colHISHN].ToString();
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatient(hn);
            if (ptt.Name.Length <= 0)
            {
                frmFlash.Dispose();
                MessageBox.Show("ไม่พบ hn ในระบบ", "");
                return;
            }
            openNewForm(ptt.Hn, ptt.Name);

            frmFlash.Dispose();
        }

        private void initGrfLabOut()
        {
            grfLabOut = new C1FlexGrid();
            grfLabOut.Font = fEdit;
            grfLabOut.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLabOut.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfLabOut.DoubleClick += GrfHn_DoubleClick;
            //grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfLabOut.Rows.Count = 1;
            pnLabOutBotton.Controls.Add(grfLabOut);
            grfLabOut.Rows[0].Visible = false;
            grfLabOut.Cols[0].Visible = false;
            theme1.SetTheme(grfLabOut, bc.iniC.themeApplication);
            grfLabOut.AllowFiltering = true;
            //foreach (Control con in panel1.Controls)
            //{
            //    theme1.SetTheme(con, bc.iniC.themeApplication);
            //}
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
            theme1.SetTheme(grfHn, bc.iniC.themeApplication);
            grfHn.AllowFiltering = true;
            //foreach (Control con in panel1.Controls)
            //{
            //    theme1.SetTheme(con, bc.iniC.themeApplication);
            //}
        }

        private void GrfHn_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String dscid = "";
            if (grfHn.Row <= 0) return;
            if (grfHn.Col <= 0) return;

            dscid = grfHn[grfHn.Row, colId].ToString();
            if (bc.iniC.statusShowLabOutFrmLabOutReceiveView.Equals("windows10"))
            {
                String hn = "";
                hn = grfHn[grfHn.Row, colHN].ToString();
                showFormWaiting();
                Patient ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatient(hn);

                if (ptt.Name.Length <= 0)
                {
                    frmFlash.Dispose();
                    MessageBox.Show("ไม่พบ hn ในระบบ", "");
                    return;
                }
                openNewForm(ptt.Hn, ptt.Name);
                frmFlash.Dispose();
            }
            else
            {
                showResultWindowsXP(dscid);
            }
        }
        private void openNewForm(String hn, String txt)
        {
            FrmScanView1 frm = new FrmScanView1(bc, hn, "hide");
            frm.FormBorderStyle = FormBorderStyle.None;
            AddNewTab(frm, txt);
        }
        public C1DockingTabPage AddNewTab(Form frm, String label)
        {
            frm.FormBorderStyle = FormBorderStyle.None;
            C1DockingTabPage tab = new C1DockingTabPage();
            tab.SuspendLayout();
            frm.TopLevel = false;
            tab.Width = tC1.Width - 10;
            tab.Height = tC1.Height - 35;
            tab.Name = frm.Name;


            frm.Parent = tab;
            frm.Dock = DockStyle.Fill;
            frm.Width = tab.Width;
            frm.Height = tab.Height;
            tab.Text = label;
            
            frm.Visible = true;

            tC1.TabPages.Add(tab);

            //frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);
            frm.Location = new Point(0, 0);
            tab.ResumeLayout();
            tab.Refresh();
            tab.Text = label;
            tab.Closing += Tab_Closing;

            theme1.SetTheme(tC1, bc.iniC.themeApplication);

            tC1.SelectedTab = tab;
            
            return tab;
        }

        private void Tab_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void showResultWindowsXP(String dscid)
        {
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
                String dgssid = "", filename = "", ftphost = "", id = "", folderftp = "", datetick = "", ext = "";
                datetick = DateTime.Now.Ticks.ToString();
                stream = new MemoryStream();
                FtpWebRequest ftpRequest = null;
                FtpWebResponse ftpResponse = null;
                FtpClient ftpc = new FtpClient(dsc.host_ftp, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);

                filename = dsc.image_path;
                ext = Path.GetExtension(filename);
                stream = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path);
                if (stream == null) return;
                if (stream.Length == 0) return;
                stream.Seek(0, SeekOrigin.Begin);
                var fileStream = new FileStream("report\\" + datetick + ext, FileMode.Create, FileAccess.Write);
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
                webb.Navigate(currentDirectory + "\\report\\" + datetick + ext);

                frm.ShowDialog(this);
            }
        }
        private void setGrf()
        {
            String datestart = "", dateend = "", hn = "", txt = "";
            DataTable dt = new DataTable();
            //MessageBox.Show("txtDateStart " + txtDateStart.Text, "");
            //MessageBox.Show("txtDateEnd " + txtDateEnd.Text, "");
            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);
            //MessageBox.Show("datestart "+ datestart, "");
            //MessageBox.Show("dateend "+ dateend, "");
            grfHn.Clear();
            grfHn.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfHn.Cols.Count = 9;
            grfHn.Cols[colDateReq].Caption = "Date Req";
            grfHn.Cols[colHN].Caption = "HN";
            grfHn.Cols[colFullName].Caption = "Name";
            grfHn.Cols[colDateReceive].Caption = "Date Rec";
            grfHn.Cols[colReqNo].Caption = "Req No";
            grfHn.Cols[colVN].Caption = "VN";
            grfHn.Cols[colId].Caption = "id";
            grfHn.Cols[colComp].Caption = "Comp";
            grfHn.Cols[colDateReq].Width = 100;
            grfHn.Cols[colHN].Width = 80;
            grfHn.Cols[colFullName].Width = 300;
            grfHn.Cols[colDateReceive].Width = 100;
            grfHn.Cols[colReqNo].Width = 80;
            grfHn.Cols[colVN].Width = 80;
            grfHn.Cols[colComp].Width = 100;
            //MessageBox.Show("1111", "");
            if (datestart.Length <= 0 && dateend.Length <= 0 && txtHn.Text.Length <= 0)
            {
                return;
            }
            if (chkDateReq.Checked)
            {
                dt = bc.bcDB.dscDB.selectLabOutByDateReq(datestart, dateend, txtHn.Text.Trim(), "daterequest");
            }
            else if (chkDateLabOut.Checked)
            {
                dt = bc.bcDB.dscDB.selectLabOutByDateReq(datestart, dateend, txtHn.Text.Trim(), "datecreate");
            }
            else if (chkDateReqHIS.Checked)
            {
                //dt = bc.bcDB.dscDB.selectLabOutByDateReq(datestart, dateend, txtHn.Text.Trim(), "datecreate");
            }
            //grfHn.Cols[colHnPrnStaffNote].Width = 60;

            //if (datestart.Length <= 0 && dateend.Length <= 0)
            //{
            //    MessageBox.Show("วันทีเริ่มต้น ไม่มีค่า", "");
            //    return;
            //}
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
                    grfHn[i, colFullName] = row["patient_fullname"].ToString();     //row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfHn[i, colDateReceive] = bc.datetoShow(row["date_create"].ToString());
                    grfHn[i, colDateReq] = bc.datetoShow(row["date_req"].ToString());
                    grfHn[i, colReqNo] = row["req_id"].ToString();
                    grfHn[i, colVN] = row["vn"].ToString();
                    grfHn[i, colId] = row["doc_scan_id"].ToString();
                    grfHn[i, colComp] = row["ml_fm"].ToString().Equals("FM-LAB-998") ? "InnoTech" : row["ml_fm"].ToString().Equals("FM-LAB-997") ? "InnoTech" : row["ml_fm"].ToString().Equals("FM-LAB-996") ? "RIA" : row["ml_fm"].ToString().Equals("FM-LAB-995") ? "Medica" : row["ml_fm"].ToString().Equals("FM-LAB-994") ? "GM" : "";
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView1 setGrf ex " + ex.Message);
                }
                i++;
            }
            grfHn.Cols[colId].Visible = false;
            grfHn.Cols[colHN].AllowEditing = false;
            grfHn.Cols[colFullName].AllowEditing = false;
            grfHn.Cols[colDateReceive].AllowEditing = false;
            grfHn.Cols[colDateReq].AllowEditing = false;
            grfHn.Cols[colReqNo].AllowEditing = false;
            grfHn.Cols[colVN].AllowEditing = false;
            grfHn.Cols[colComp].AllowEditing = false;
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrf();
        }
        private void setHISSearch(String flagauto)
        {
            showFormWaiting();
            String datestart = "", dateend = "", hn = "", txt = "";
            DataTable dt = new DataTable();
            //MessageBox.Show("txtDateStart " + txtDateStart.Text, "");
            //MessageBox.Show("txtDateEnd " + txtDateEnd.Text, "");
            datestart = bc.datetoDB(txtDate.Text);
            //dateend = bc.datetoDB(txtDateEnd.Text);
            //MessageBox.Show("datestart "+ datestart, "");
            //MessageBox.Show("dateend "+ dateend, "");
            //grfLabOut.Clear();
            grfLabOut.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfLabOut.Cols.Count = 12;
            grfLabOut.Cols[colHISDateReq].Caption = "Date Req";
            grfLabOut.Cols[colHISHN].Caption = "HN";
            grfLabOut.Cols[colHISFullName].Caption = "Name";
            grfLabOut.Cols[colHISVN].Caption = "VN";
            grfLabOut.Cols[colHISLabCode].Caption = "Code";
            grfLabOut.Cols[colHISLabName].Caption = "Lab Name";
            grfLabOut.Cols[colHISReqNo].Caption = "req id";
            grfLabOut.Cols[colHISVsDate].Caption = "Date Visit";
            grfLabOut.Cols[colHISDateReq].Width = 100;
            grfLabOut.Cols[colHISHN].Width = 80;
            grfLabOut.Cols[colHISFullName].Width = 260;
            grfLabOut.Cols[colHISVN].Width = 100;
            grfLabOut.Cols[colHISLabCode].Width = 80;
            grfLabOut.Cols[colHISLabName].Width = 300;
            grfLabOut.Cols[colHISReqNo].Width = 60;
            grfLabOut.Cols[colHISVsDate].Width = 100;
            //MessageBox.Show("1111", "");
            String flag = "";
            dt = bc.bcDB.laboDB.selectByDateReq(datestart);
            theme1.SetTheme(grfLabOut, bc.iniC.themeApplication);
            if (flagauto.Equals("auto"))
            {
                if (dt.Rows.Count <= 0)
                {
                    dt = bc.bcDB.vsDB.selectRequestOutLabbyDateReq(datestart);
                    theme1.SetTheme(grfLabOut, bc.iniC.themeDonor);
                    flag = "new";
                    flagImport = true;
                    btnImport.Text = "Import Data";
                }
                else
                {
                    theme1.SetTheme(grfLabOut, bc.iniC.themeApplication);
                    flag = "old";
                    flagImport = false;
                    btnImport.Text = "Void Data";
                }
            }

            //grfHn.Cols[colHnPrnStaffNote].Width = 60;

            grfLabOut.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfLabOut.ContextMenu = menuGw;
            int i = 1;
            grfLabOut.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfLabOut[i, 0] = (i);
                    if (flag.Equals("new"))
                    {
                        grfLabOut[i, colHISHN] = row["mnc_hn_no"].ToString();
                        grfLabOut[i, colHISFullName] = row["mnc_patname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                        grfLabOut[i, colHISDateReq] = bc.datetoShow(row["mnc_req_dat"].ToString());
                        grfLabOut[i, colHISVN] = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
                        grfLabOut[i, colHISLabCode] = row["MNC_LB_CD"].ToString();
                        grfLabOut[i, colHISLabName] = row["MNC_LB_DSC"].ToString();
                        grfLabOut[i, colHISReqNo] = row["mnc_req_no"].ToString();
                        grfLabOut[i, colHISpreno] = row["mnc_pre_no"].ToString();
                        grfLabOut[i, colHISVsDate] = row["mnc_date"].ToString();
                        grfLabOut[i, colHISEdit] = "1";
                    }
                    else
                    {
                        grfLabOut[i, colHISHN] = row["hn"].ToString();
                        grfLabOut[i, colHISFullName] = row["patient_fullname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                        grfLabOut[i, colHISDateReq] = bc.datetoShow(row["visit_date"].ToString());
                        grfLabOut[i, colHISVN] = row["vn"].ToString();
                        grfLabOut[i, colHISLabCode] = row["lab_code"].ToString();
                        grfLabOut[i, colHISLabName] = row["lab_name"].ToString();
                        grfLabOut[i, colHISReqNo] = row["req_no"].ToString();
                        grfLabOut[i, colHISpreno] = row["pre_no"].ToString();
                        grfLabOut[i, colHISVsDate] = bc.datetoShow(row["date_req"].ToString());
                        grfLabOut[i, colHISEdit] = "0";
                    }
                    
                    //if ((i % 2) == 0)
                    //    grfLabOutWait.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    //grfLabOut.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView setGrf ex " + ex.Message);
                }
                i++;
            }
            grfLabOut.Cols[colHISEdit].Visible = false;
            grfLabOut.Cols[colHISpreno].Visible = false;
            grfLabOut.Cols[colHISHN].AllowEditing = false;
            grfLabOut.Cols[colHISFullName].AllowEditing = false;
            grfLabOut.Cols[colHISDateReq].AllowEditing = false;
            grfLabOut.Cols[colHISVN].AllowEditing = false;
            grfLabOut.Cols[colHISLabCode].AllowEditing = false;
            grfLabOut.Cols[colHISLabName].AllowEditing = false;
            grfLabOut.Cols[colHISReqNo].AllowEditing = false;
            grfLabOut.Cols[colHISVsDate].AllowEditing = false;
            frmFlash.Dispose();
        }
        private void setgrfLabOutView()
        {
            showFormWaiting();
            String datestart = "", dateend = "", hn = "", txt = "";
            DataTable dt = new DataTable();
            DataTable dtM01 = new DataTable();
            //MessageBox.Show("txtDateStart " + txtDateStart.Text, "");
            //MessageBox.Show("txtDateEnd " + txtDateEnd.Text, "");
            datestart = bc.datetoDB(txtLabOutViewDate.Text);
            //dateend = bc.datetoDB(txtDateEnd.Text);
            //MessageBox.Show("datestart "+ datestart, "");
            //MessageBox.Show("dateend "+ dateend, "");
            //grfLabOutView.Clear();
            grfLabOutView.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfLabOutView.Cols.Count = 15;
            grfLabOutView.Cols[colHISDateReq].Caption = "Date Req";
            grfLabOutView.Cols[colHISHN].Caption = "HN";
            grfLabOutView.Cols[colHISFullName].Caption = "Name";
            grfLabOutView.Cols[colHISVN].Caption = "VN";
            grfLabOutView.Cols[colHISLabCode].Caption = "Code";
            grfLabOutView.Cols[colHISLabName].Caption = "Lab Name";
            grfLabOutView.Cols[colHISReqNo].Caption = "req id";
            grfLabOutView.Cols[colHISStatusRes].Caption = "Status";
            grfLabOutView.Cols[colHISVsDate].Caption = "Date Visit";
            grfLabOutView.Cols[colHISDateResult].Caption = "Date Result";
            grfLabOutView.Cols[colHISDateReq].Width = 100;
            grfLabOutView.Cols[colHISHN].Width = 80;
            grfLabOutView.Cols[colHISFullName].Width = 260;
            grfLabOutView.Cols[colHISVN].Width = 100;
            grfLabOutView.Cols[colHISLabCode].Width = 80;
            grfLabOutView.Cols[colHISLabName].Width = 300;
            grfLabOutView.Cols[colHISReqNo].Width = 60;
            grfLabOutView.Cols[colHISStatusRes].Width = 60;
            grfLabOutView.Cols[colHISVsDate].Width = 90;
            grfLabOutView.Cols[colHISDateResult].Width = 90;
            //MessageBox.Show("1111", "");
            String flag = "";
            //dt = bc.bcDB.laboDB.selectByDateReq(datestart);
            theme1.SetTheme(grfLabOutView, bc.iniC.themeApplication);

            dt = bc.bcDB.laboDB.selectByDateReq(datestart);

            //grfHn.Cols[colHnPrnStaffNote].Width = 60;

            grfLabOutView.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfLabOutView.ContextMenu = menuGw;
            int i = 0;
            grfLabOutView.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    i++;
                    grfLabOutView[i, 0] = (i);
                    grfLabOutView[i, colHISid] = row["lab_out_id"].ToString();
                    grfLabOutView[i, colHISHN] = row["hn"].ToString();
                    grfLabOutView[i, colHISFullName] = row["patient_fullname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfLabOutView[i, colHISDateReq] = bc.datetoShow(row["date_req"].ToString());
                    grfLabOutView[i, colHISVN] = row["vn"].ToString();
                    grfLabOutView[i, colHISLabCode] = row["lab_code"].ToString();
                    grfLabOutView[i, colHISLabName] = row["lab_name"].ToString();
                    grfLabOutView[i, colHISReqNo] = row["req_no"].ToString();
                    grfLabOutView[i, colHISpreno] = row["pre_no"].ToString();
                    grfLabOutView[i, colHISVsDate] = bc.datetoShow(row["visit_date"].ToString());
                    grfLabOutView[i, colHISEdit] = "0";
                    if (row["status_result"].ToString().Equals("1"))
                    {
                        grfLabOutView[i, colHISStatusRes] = "OK";
                    }
                    else
                    {
                        grfLabOutView[i, colHISStatusRes] = "";
                    }
                    grfLabOutView[i, colHISDateResult] = bc.datetoShow(row["date_result"].ToString());
                    if (row["lab_code"] != null)
                    {
                        dtM01 = bc.bcDB.laboDB.selectLabM01By(row["lab_code"].ToString());
                        if (dtM01.Rows.Count > 0)
                        {
                            grfLabOutView[i, colHISPeriodResult] = dtM01.Rows[0]["MNC_SCH_ACT"].ToString();
                        }
                    }
                    //if ((i % 2) == 0)
                    //    grfLabOutWait.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    //grfLabOut.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView setGrf ex " + ex.Message);
                }
            }
            grfLabOutView.Rows[0].Visible = true;
            grfLabOutView.Cols[0].Visible = true;
            grfLabOutView.Cols[colHISid].Visible = false;
            grfLabOutView.Cols[colHISEdit].Visible = false;
            grfLabOutView.Cols[colHISpreno].Visible = false;
            grfLabOutView.Cols[colHISHN].AllowEditing = false;
            grfLabOutView.Cols[colHISFullName].AllowEditing = false;
            grfLabOutView.Cols[colHISDateReq].AllowEditing = false;
            grfLabOutView.Cols[colHISVN].AllowEditing = false;
            grfLabOutView.Cols[colHISLabCode].AllowEditing = false;
            grfLabOutView.Cols[colHISLabName].AllowEditing = false;
            grfLabOutView.Cols[colHISReqNo].AllowEditing = false;
            grfLabOutView.Cols[colHISStatusRes].AllowEditing = false;
            grfLabOutView.Cols[colHISVsDate].AllowEditing = false;
            grfLabOutView.Cols[colHISHN].AllowSorting = true;
            grfLabOutView.AllowFiltering = true;
            frmFlash.Dispose();
        }
        private void initCompoment()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            //pnSearch = new Panel();
            pnLabOut = new Panel();
            pnLabOutTop = new Panel();
            pnLabOutBotton = new Panel();
            pnLabOutView = new Panel();
            pnLabOutViewTop = new Panel();
            pnLabOutViewBotton = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            tC1 = new C1DockingTab();
            tabSearch = new C1DockingTabPage();
            tabLabOut = new C1DockingTabPage();
            tabMasLabOut = new C1DockingTabPage();
            tabLabOutView = new C1DockingTabPage();
            lbtxtDateStart = new C1Label();
            lbtxtDateEnd = new C1Label();
            lbtxtDate = new C1Label();
            lbLabOutViewDate = new C1Label();
            txtDateStart = new C1DateEdit();
            txtDateEnd = new C1DateEdit();
            txtDate = new C1DateEdit();
            txtLabOutViewDate = new C1DateEdit();
            lbtxtHn = new C1Label();
            txtHn = new C1TextBox();
            chkDateLabOut = new RadioButton();
            chkDateReq = new RadioButton();
            chkDateReqHIS = new RadioButton();
            btnOk = new C1Button();
            btnHISSearch = new C1Button();
            btnLabOutViewDate = new C1Button();
            btntabLabOutViewChk = new C1Button();
            txtLabOutViewHn = new C1TextBox();
            lbLabOutViewHn = new C1Label();

            //pnSearch.SuspendLayout();
            pnLabOut.SuspendLayout();
            pnLabOutBotton.SuspendLayout();
            pnLabOutTop.SuspendLayout();
            pnLabOutView.SuspendLayout();
            pnLabOutViewBotton.SuspendLayout();
            pnLabOutViewTop.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            tabSearch.SuspendLayout();
            tabLabOut.SuspendLayout();
            tabMasLabOut.SuspendLayout();
            tabLabOutView.SuspendLayout();
            this.SuspendLayout();

            //pnSearch.Dock = DockStyle.Fill;
            pnLabOut.Dock = DockStyle.Fill;
            pnLabOutView.Dock = DockStyle.Fill;
            pnLabOutView.Name = "pnLabOutView";
            panel1.Size = new System.Drawing.Size(990, 55);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Name = "panel1";
            panel2.Size = new System.Drawing.Size(990, 55);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Name = "panel2";

            sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            sb1.Location = new System.Drawing.Point(0, 620);
            sb1.Name = "sb1";
            sb1.Size = new System.Drawing.Size(956, 22);
            sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;

            tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            tC1.HotTrack = true;
            tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            tC1.TabsShowFocusCues = true;
            tC1.Alignment = TabAlignment.Bottom;
            tC1.SelectedTabBold = true;
            tC1.Name = "tC1";
            tC1.CanCloseTabs = true;
            tC1.CloseBox = CloseBoxPositionEnum.ActivePage;

            tabSearch.Name = "tabSearch";
            tabSearch.TabIndex = 0;
            tabSearch.Text = "Search result Out LAB(receive result)";
            tabLabOut.Name = "tabLabOut";
            tabLabOut.TabIndex = 0;
            tabLabOut.Text = "Import HIS  to Out LAB";
            tabMasLabOut.Name = "tabMasLabOut";
            tabMasLabOut.TabIndex = 0;
            tabMasLabOut.Text = "Master Out LAB";
            tabLabOutView.Name = "tabLabOutView";
            tabLabOutView.TabIndex = 0;
            tabLabOutView.Text = "Out LAB Transaction(HIS order outlab)";

            //c1Label1.AutoSize = true;

            lbtxtDateStart.AutoSize = true;
            lbtxtDateStart.BorderColor = System.Drawing.Color.Transparent;
            lbtxtDateStart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbtxtDateStart.Font = fEdit;
            lbtxtDateStart.ForeColor = System.Drawing.SystemColors.ControlText;
            lbtxtDateStart.Location = new System.Drawing.Point(gapX, 10);
            lbtxtDateStart.Value = "วันที่เริ่มต้น:";
            txtDateStart.AllowSpinLoop = false;
            txtDateStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtDateStart.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txtDateStart.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateStart.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateStart.CurrentTimeZone = false;
            txtDateStart.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txtDateStart.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDateStart.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDateStart.EditFormat.CustomFormat = "dd/MM/yyyy";
            txtDateStart.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDateStart.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtDateStart.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txtDateStart.ImagePadding = new System.Windows.Forms.Padding(0);
            size = bc.MeasureString(lbtxtDateStart);
            txtDateStart.Location = new System.Drawing.Point(lbtxtDateStart.Location.X + size.Width +25, lbtxtDateStart.Location.Y);
            txtDateStart.Name = "txtDateStart";
            txtDateStart.Size = new System.Drawing.Size(111, 20);
            txtDateStart.TabIndex = 12;
            txtDateStart.Tag = null;
            theme1.SetTheme(this.txtDateStart, "(default)");
            txtDateStart.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;

            lbtxtDateEnd.AutoSize = true;
            lbtxtDateEnd.BorderColor = System.Drawing.Color.Transparent;
            lbtxtDateEnd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbtxtDateEnd.Font = fEdit;
            lbtxtDateEnd.ForeColor = System.Drawing.SystemColors.ControlText;
            lbtxtDateEnd.Location = new System.Drawing.Point(txtDateStart.Location.X + txtDateStart.Width +20, lbtxtDateStart.Location.Y);
            lbtxtDateEnd.Value = "วันที่เริ่มต้น:";
            txtDateEnd.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txtDateEnd.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateEnd.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateEnd.Culture = 1054;
            txtDateEnd.CurrentTimeZone = false;
            txtDateEnd.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txtDateEnd.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDateEnd.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDateEnd.EditFormat.CustomFormat = "dd/MM/yyyy";
            txtDateEnd.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDateEnd.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtDateEnd.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txtDateEnd.ImagePadding = new System.Windows.Forms.Padding(0);
            size = bc.MeasureString(lbtxtDateEnd);
            txtDateEnd.Location = new System.Drawing.Point(lbtxtDateEnd.Location.X+ size.Width + 25, lbtxtDateStart.Location.Y);
            txtDateEnd.Name = "txtDateEnd";
            txtDateEnd.Size = new System.Drawing.Size(111, 20);
            txtDateEnd.TabIndex = 14;
            txtDateEnd.Tag = null;
            theme1.SetTheme(this.txtDateEnd, "(default)");
            txtDateEnd.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateEnd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;

            lbtxtHn.AutoSize = true;
            lbtxtHn.BorderColor = System.Drawing.Color.Transparent;
            lbtxtHn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbtxtHn.Font = fEdit;
            lbtxtHn.ForeColor = System.Drawing.SystemColors.ControlText;
            lbtxtHn.Location = new System.Drawing.Point(txtDateEnd.Location.X + txtDateEnd.Width + 20, lbtxtDateStart.Location.Y);
            lbtxtHn.Value = "HN:";

            txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtHn.Font = fEdit;
            size = bc.MeasureString(lbtxtHn);
            txtHn.Location = new System.Drawing.Point(lbtxtHn.Location.X + size .Width+ 15, lbtxtDateStart.Location.Y);
            txtHn.Name = "txtHn";

            chkDateReq.AutoSize = true;
            chkDateReq.BackColor = Color.Transparent;
            chkDateReq.Font = fEdit;
            chkDateReq.Name = "chkDateReq";
            chkDateReq.Text = "วันที่ Request";
            chkDateReq.Location = new Point(3, 8);

            chkDateLabOut.AutoSize = true;
            chkDateLabOut.BackColor = System.Drawing.Color.Transparent;
            chkDateLabOut.Font = fEdit;
            chkDateLabOut.Name = "chkDateLabOut";
            chkDateLabOut.Text = "วันที่ รับผลจาก out lab";
            size = bc.MeasureString(chkDateLabOut);
            chkDateLabOut.Location = new System.Drawing.Point(chkDateReq.Location.X + size.Width+20, 8);
            
            chkDateReqHIS.AutoSize = true;
            chkDateReqHIS.BackColor = Color.Transparent;
            chkDateReqHIS.Font = fEdit;
            chkDateReqHIS.Name = "chkDateReqHIS";
            chkDateReqHIS.Text = "วันที่ Request HIS";
            size = bc.MeasureString(chkDateReqHIS);
            chkDateReqHIS.Location = new Point(chkDateLabOut.Location.X + size.Width+20, 8);
            chkDateReqHIS.Hide();

            panel3.BackColor = System.Drawing.Color.White;
            panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            panel3.Location = new System.Drawing.Point(txtHn.Location.X + txtHn.Width +10, lbtxtDateStart.Location.Y);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(390, 37);
            panel3.TabIndex = 5;
            theme1.SetTheme(this.panel3, "(default)");

            btnOk = new C1Button();
            btnOk.Name = "btnOk";
            btnOk.Text = "Search";
            btnOk.Font = fEdit;
            btnOk.Location = new System.Drawing.Point(panel3.Location.X + panel3.Width+10, lbtxtDateStart.Location.Y);
            size = bc.MeasureString(btnOk);
            btnOk.Size = new Size(size.Width+30, 40);
            btnOk.Font = fEdit;

            lbtxtDate.AutoSize = true;
            lbtxtDate.BorderColor = System.Drawing.Color.Transparent;
            lbtxtDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbtxtDate.Font = fEdit;
            lbtxtDate.ForeColor = System.Drawing.SystemColors.ControlText;
            lbtxtDate.Location = new System.Drawing.Point(gapX, 10);
            lbtxtDate.Value = "วันที่: ";
            txtDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txtDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.Culture = 1054;
            txtDate.CurrentTimeZone = false;
            txtDate.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txtDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            txtDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtDate.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txtDate.ImagePadding = new System.Windows.Forms.Padding(0);
            size = bc.MeasureString(lbtxtDate);
            txtDate.Location = new System.Drawing.Point(lbtxtDate.Location.X + size.Width + 5, lbtxtDate.Location.Y);
            txtDate.Name = "txtDateEnd";
            txtDate.Size = new System.Drawing.Size(111, 20);
            txtDate.TabIndex = 14;
            txtDate.Tag = null;
            theme1.SetTheme(this.txtDateEnd, "(default)");
            txtDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            btnHISSearch = new C1Button();
            btnHISSearch.Name = "btnHISSearch";
            btnHISSearch.Text = "Search";
            btnHISSearch.Font = fEdit;
            btnHISSearch.Location = new System.Drawing.Point(txtDate.Location.X + txtDate.Width + 10, lbtxtDate.Location.Y);
            size = bc.MeasureString(btnHISSearch);
            btnHISSearch.Size = new Size(size.Width + 80, 25);
            btnHISSearch.Font = fEdit;
            btnImport = new C1Button();
            btnImport.Name = "btnImport";
            btnImport.Text = "Import";
            btnImport.Font = fEdit;
            btnImport.Location = new System.Drawing.Point(btnHISSearch.Location.X + btnHISSearch.Width + 10, lbtxtDate.Location.Y);
            size = bc.MeasureString(btnImport);
            btnImport.Size = new Size(size.Width + 80, 25);
            btnImport.Font = fEdit;
            

            lbLabOutViewDate.AutoSize = true;
            lbLabOutViewDate.BorderColor = System.Drawing.Color.Transparent;
            lbLabOutViewDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbLabOutViewDate.Font = fEdit;
            lbLabOutViewDate.ForeColor = System.Drawing.SystemColors.ControlText;
            lbLabOutViewDate.Location = new System.Drawing.Point(gapX, 10);
            lbLabOutViewDate.Value = "วันที่: ";
            lbLabOutViewDate.Name = "lbLabOutViewDate";
            txtLabOutViewDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txtLabOutViewDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtLabOutViewDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtLabOutViewDate.Culture = 1054;
            txtLabOutViewDate.CurrentTimeZone = false;
            txtLabOutViewDate.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txtLabOutViewDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLabOutViewDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtLabOutViewDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            txtLabOutViewDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLabOutViewDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtLabOutViewDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtLabOutViewDate.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txtLabOutViewDate.ImagePadding = new System.Windows.Forms.Padding(0);
            size = bc.MeasureString(lbLabOutViewDate);
            txtLabOutViewDate.Location = new System.Drawing.Point(lbLabOutViewDate.Location.X + size.Width + 5, lbLabOutViewDate.Location.Y);
            txtLabOutViewDate.Name = "txtLabOutViewDate";
            txtLabOutViewDate.Size = new System.Drawing.Size(111, 20);
            txtLabOutViewDate.TabIndex = 14;
            txtLabOutViewDate.Tag = null;
            theme1.SetTheme(this.txtDateEnd, "(default)");
            txtLabOutViewDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtLabOutViewDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            btnLabOutViewDate = new C1Button();
            btnLabOutViewDate.Name = "btnLabOutViewDate";
            btnLabOutViewDate.Text = "Search";
            btnLabOutViewDate.Font = fEdit;
            size = bc.MeasureString(btnLabOutViewDate);
            btnLabOutViewDate.Location = new System.Drawing.Point(txtLabOutViewDate.Location.X + txtLabOutViewDate.Width + 10, lbLabOutViewDate.Location.Y);
            size = bc.MeasureString(btnLabOutViewDate);
            btnLabOutViewDate.Size = new Size(size.Width + 80, 25);
            btnLabOutViewDate.Font = fEdit;
            btntabLabOutViewChk = new C1Button();
            btntabLabOutViewChk.Name = "btntabLabOutViewChk";
            btntabLabOutViewChk.Text = "Check Result outlab";
            btntabLabOutViewChk.Font = fEdit;
            btntabLabOutViewChk.Location = new System.Drawing.Point(btnLabOutViewDate.Location.X + btnLabOutViewDate.Width + 10, lbLabOutViewDate.Location.Y);
            size = bc.MeasureString(btntabLabOutViewChk);
            btntabLabOutViewChk.Size = new Size(size.Width + 80, 25);
            btntabLabOutViewChk.Font = fEdit;
            lbLabOutViewHn.AutoSize = true;
            lbLabOutViewHn.BorderColor = System.Drawing.Color.Transparent;
            lbLabOutViewHn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbLabOutViewHn.Font = fEdit;
            lbLabOutViewHn.ForeColor = System.Drawing.SystemColors.ControlText;
            lbLabOutViewHn.Location = new System.Drawing.Point(btntabLabOutViewChk.Location.X + btntabLabOutViewChk.Width + 20, btntabLabOutViewChk.Location.Y);
            lbLabOutViewHn.Value = "HN:";
            txtLabOutViewHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtLabOutViewHn.Font = fEdit;
            size = bc.MeasureString(lbLabOutViewHn);
            txtLabOutViewHn.Location = new System.Drawing.Point(lbLabOutViewHn.Location.X + size.Width + 15, lbLabOutViewHn.Location.Y);
            txtLabOutViewHn.Name = "txtLabOutViewHn";

            pnLabOutTop.Size = new System.Drawing.Size(990, 55);
            pnLabOutTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnLabOutTop.Name = "pnLabOutTop";
            pnLabOutBotton.Size = new System.Drawing.Size(990, 55);
            pnLabOutBotton.Dock = System.Windows.Forms.DockStyle.Fill;
            pnLabOutBotton.Name = "pnLabOutBotton";

            pnLabOutViewTop.Size = new System.Drawing.Size(990, 55);
            pnLabOutViewTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnLabOutViewTop.Name = "pnLabOutViewTop";
            pnLabOutViewBotton.Size = new System.Drawing.Size(990, 55);
            pnLabOutViewBotton.Dock = System.Windows.Forms.DockStyle.Fill;
            pnLabOutViewBotton.Name = "pnLabOutViewBotton";

            theme1.Theme = bc.iniC.themeApplication;
            this.Controls.Add(tC1);
            this.Controls.Add(sb1);

            panel3.Controls.Add(this.chkDateReqHIS);
            panel3.Controls.Add(this.chkDateLabOut);
            panel3.Controls.Add(this.chkDateReq);
            panel1.Controls.Add(this.txtHn);
            panel1.Controls.Add(this.lbtxtHn);
            panel1.Controls.Add(this.panel3);
            panel1.Controls.Add(this.btnOk);
            panel1.Controls.Add(this.txtDateEnd);
            panel1.Controls.Add(this.lbtxtDateEnd);
            panel1.Controls.Add(this.txtDateStart);
            panel1.Controls.Add(this.lbtxtDateStart);
            pnLabOut.Controls.Add(this.pnLabOutBotton);
            pnLabOut.Controls.Add(this.pnLabOutTop);
            pnLabOutView.Controls.Add(this.pnLabOutViewBotton);
            pnLabOutView.Controls.Add(this.pnLabOutViewTop);
            pnLabOutViewTop.Controls.Add(lbLabOutViewDate);
            pnLabOutViewTop.Controls.Add(txtLabOutViewDate);
            pnLabOutViewTop.Controls.Add(btnLabOutViewDate);
            pnLabOutViewTop.Controls.Add(btntabLabOutViewChk);
            pnLabOutViewTop.Controls.Add(txtLabOutViewHn);
            pnLabOutViewTop.Controls.Add(lbLabOutViewHn);

            pnLabOutTop.Controls.Add(lbtxtDate);
            pnLabOutTop.Controls.Add(txtDate);
            pnLabOutTop.Controls.Add(btnHISSearch);
            pnLabOutTop.Controls.Add(btnImport);

            pnLabOutViewTop.Controls.Add(lbLabOutViewDate);
            pnLabOutViewTop.Controls.Add(txtLabOutViewDate);
            pnLabOutViewTop.Controls.Add(btnLabOutViewDate);

            tC1.Controls.Add(tabSearch);
            tC1.Controls.Add(tabLabOut);
            tC1.Controls.Add(tabMasLabOut);
            tC1.Controls.Add(tabLabOutView);
            //tabSearch.Controls.Add(pnSearch);
            tabLabOut.Controls.Add(pnLabOut);
            tabSearch.Controls.Add(this.panel2);
            tabSearch.Controls.Add(this.panel1);
            tabLabOutView.Controls.Add(pnLabOutView);

            this.WindowState = FormWindowState.Maximized;

            //pnSearch.ResumeLayout(false);
            pnLabOut.ResumeLayout(false);
            pnLabOutBotton.ResumeLayout(false);
            pnLabOutTop.ResumeLayout(false);
            pnLabOutView.ResumeLayout(false);
            pnLabOutViewBotton.ResumeLayout(false);
            pnLabOutViewTop.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tabSearch.ResumeLayout(false);
            tabLabOut.ResumeLayout(false);
            tabLabOutView.ResumeLayout(false);
            tabMasLabOut.ResumeLayout(false);
            this.ResumeLayout(false);
            //pnSearch.PerformLayout();
            pnLabOut.PerformLayout();
            pnLabOutBotton.PerformLayout();
            pnLabOutTop.PerformLayout();
            pnLabOutView.PerformLayout();
            pnLabOutViewBotton.PerformLayout();
            pnLabOutViewTop.PerformLayout();
            panel1.PerformLayout();
            panel2.PerformLayout();
            panel3.PerformLayout();
            this.PerformLayout();
        }
        
        private void FrmlabOutReceiveView1_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Text = "Last Update 2020-07-27 ";
            tC1.Font = fEdit;
            theme1.SetTheme(tC1, bc.iniC.themeApp);
            theme1.SetTheme(panel3, bc.iniC.themeApp);
            theme1.SetTheme(panel1, bc.iniC.themeApp);
        }
    }
}
