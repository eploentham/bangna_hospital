﻿using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
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

namespace bangna_hospital.gui
{
    public class FrmLabOutReceiveView1:Form
    {
        BangnaControl bc;

        C1FlexGrid grfHn, grfLabOut;
        Font fEdit, fEditB, fEditBig;
        int colDateReq = 1, colHN = 2, colFullName = 3, colVN = 4, colDateReceive = 5, colReqNo = 6, colId = 7;
        int colHISDateReq = 1, colHISHN = 2, colHISFullName = 3, colHISVN = 4, colHISLabCode = 5, colHISLabName = 6, colHISReqNo = 7;

        C1DockingTab tC1;
        C1DockingTabPage tabSearch, tabLabOut;
        Panel pnLabOut, panel1, panel2, panel3, pnLabOutTop, pnLabOutBotton;
        C1TextBox txtHn;
        C1Label lbtxtHn, lbtxtDateEnd, lbtxtDateStart, lbtxtDate;
        C1Button btnOk, btnHISSearch, btnImport;
        C1DateEdit txtDateStart, txtDateEnd, txtDate;
        RadioButton chkDateLabOut, chkDateReq, chkDateReqHIS;

        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private C1.Win.C1Themes.C1ThemeController theme1;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmLabOutReceiveView1(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            initCompoment();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);

            this.Load += FrmlabOutReceiveView1_Load;
            btnOk.Click += BtnOk_Click;
            btnHISSearch.Click += BtnHISSearch_Click;

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

            initGrf();
            setGrf();
            initGrfLabOut();
        }

        private void BtnHISSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setHISSearch();
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
                String dgssid = "", filename = "", ftphost = "", id = "", folderftp = "", datetick = "";
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
                webb.Navigate(currentDirectory + "\\report\\" + datetick + ".pdf");

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
                    grfHn[i, colFullName] = row["patient_fullname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfHn[i, colDateReceive] = bc.datetoShow(row["date_create"].ToString());
                    grfHn[i, colDateReq] = bc.datetoShow(row["date_req"].ToString());
                    grfHn[i, colReqNo] = row["req_id"].ToString();
                    grfHn[i, colVN] = row["vn"].ToString();
                    grfHn[i, colId] = row["doc_scan_id"].ToString();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView setGrf ex " + ex.Message);
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
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrf();
        }
        private void setHISSearch()
        {
            String datestart = "", dateend = "", hn = "", txt = "";
            DataTable dt = new DataTable();
            //MessageBox.Show("txtDateStart " + txtDateStart.Text, "");
            //MessageBox.Show("txtDateEnd " + txtDateEnd.Text, "");
            datestart = bc.datetoDB(txtDate.Text);
            //dateend = bc.datetoDB(txtDateEnd.Text);
            //MessageBox.Show("datestart "+ datestart, "");
            //MessageBox.Show("dateend "+ dateend, "");
            grfLabOut.Clear();
            grfLabOut.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfLabOut.Cols.Count = 8;
            grfLabOut.Cols[colHISDateReq].Caption = "Date Req";
            grfLabOut.Cols[colHISHN].Caption = "HN";
            grfLabOut.Cols[colHISFullName].Caption = "Name";
            grfLabOut.Cols[colHISVN].Caption = "VN";
            grfLabOut.Cols[colHISLabCode].Caption = "Code";
            grfLabOut.Cols[colHISLabName].Caption = "Lab Name";
            grfLabOut.Cols[colHISReqNo].Caption = "req id";
            grfLabOut.Cols[colHISDateReq].Width = 100;
            grfLabOut.Cols[colHISHN].Width = 80;
            grfLabOut.Cols[colHISFullName].Width = 260;
            grfLabOut.Cols[colHISVN].Width = 100;
            grfLabOut.Cols[colHISLabCode].Width = 80;
            grfLabOut.Cols[colHISLabName].Width = 300;
            grfLabOut.Cols[colHISReqNo].Width = 60;
            //MessageBox.Show("1111", "");
            
            dt = bc.bcDB.vsDB.selectRequestOutLabbyDateReq(datestart);

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
                    grfLabOut[i, colHISHN] = row["mnc_hn_no"].ToString();
                    grfLabOut[i, colHISFullName] = row["mnc_patname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfLabOut[i, colHISDateReq] = bc.datetoShow(row["mnc_req_dat"].ToString());
                    grfLabOut[i, colHISVN] = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
                    grfLabOut[i, colHISLabCode] = row["MNC_LB_CD"].ToString();
                    grfLabOut[i, colHISLabName] = row["MNC_LB_DSC"].ToString();
                    grfLabOut[i, colHISReqNo] = row["mnc_req_no"].ToString();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView setGrf ex " + ex.Message);
                }
                i++;
            }
            grfLabOut.Cols[colHISHN].AllowEditing = false;
            grfLabOut.Cols[colHISFullName].AllowEditing = false;
            grfLabOut.Cols[colHISDateReq].AllowEditing = false;
            grfLabOut.Cols[colHISVN].AllowEditing = false;
            grfLabOut.Cols[colHISLabCode].AllowEditing = false;
            grfLabOut.Cols[colHISLabName].AllowEditing = false;
            grfLabOut.Cols[colHISReqNo].AllowEditing = false;
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
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            tC1 = new C1DockingTab();
            tabSearch = new C1DockingTabPage();
            tabLabOut = new C1DockingTabPage();
            lbtxtDateStart = new C1Label();
            lbtxtDateEnd = new C1Label();
            lbtxtDate = new C1Label();
            txtDateStart = new C1DateEdit();
            txtDateEnd = new C1DateEdit();
            txtDate = new C1DateEdit();
            lbtxtHn = new C1Label();
            txtHn = new C1TextBox();
            chkDateLabOut = new RadioButton();
            chkDateReq = new RadioButton();
            chkDateReqHIS = new RadioButton();
            btnOk = new C1Button();
            btnHISSearch = new C1Button();

            //pnSearch.SuspendLayout();
            pnLabOut.SuspendLayout();
            pnLabOutBotton.SuspendLayout();
            pnLabOutTop.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            tabSearch.SuspendLayout();
            tabLabOut.SuspendLayout();
            this.SuspendLayout();

            //pnSearch.Dock = DockStyle.Fill;
            pnLabOut.Dock = DockStyle.Fill;
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

            tabSearch.Name = "tabSearch";
            tabSearch.TabIndex = 0;
            tabSearch.Text = "Search result Out LAB";
            tabLabOut.Name = "tabLabOut";
            tabLabOut.TabIndex = 0;
            tabLabOut.Text = "Import HIS  to Out LAB";

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
            txtDateStart.Location = new System.Drawing.Point(lbtxtDateStart.Location.X + size.Width +5, lbtxtDateStart.Location.Y);
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
            txtDateEnd.Location = new System.Drawing.Point(lbtxtDateEnd.Location.X+ size.Width + 5, lbtxtDateStart.Location.Y);
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
            txtHn.Location = new System.Drawing.Point(lbtxtHn.Location.X + size .Width+ 5, lbtxtDateStart.Location.Y);
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
            size = bc.MeasureString(chkDateReq);
            chkDateLabOut.Location = new System.Drawing.Point(chkDateReq.Location.X + size.Width+20, 8);
            
            chkDateReqHIS.AutoSize = true;
            chkDateReqHIS.BackColor = Color.Transparent;
            chkDateReqHIS.Font = fEdit;
            chkDateReqHIS.Name = "chkDateReqHIS";
            chkDateReqHIS.Text = "วันที่ Request HIS";
            size = bc.MeasureString(chkDateLabOut);
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
            btnOk.Size = new Size(size.Width+10, 25);
            btnOk.Font = fEdit;

            lbtxtDate.AutoSize = true;
            lbtxtDate.BorderColor = System.Drawing.Color.Transparent;
            lbtxtDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbtxtDate.Font = fEdit;
            lbtxtDate.ForeColor = System.Drawing.SystemColors.ControlText;
            lbtxtDate.Location = new System.Drawing.Point(gapX, 10);
            lbtxtDate.Value = "วันที่:";
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

            pnLabOutTop.Size = new System.Drawing.Size(990, 55);
            pnLabOutTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnLabOutTop.Name = "pnLabOutTop";
            pnLabOutBotton.Size = new System.Drawing.Size(990, 55);
            pnLabOutBotton.Dock = System.Windows.Forms.DockStyle.Fill;
            pnLabOutBotton.Name = "pnLabOutBotton";

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
            
            pnLabOutTop.Controls.Add(lbtxtDate);
            pnLabOutTop.Controls.Add(txtDate);
            pnLabOutTop.Controls.Add(btnHISSearch);
            pnLabOutTop.Controls.Add(btnImport);

            tC1.Controls.Add(tabSearch);
            tC1.Controls.Add(tabLabOut);
            //tabSearch.Controls.Add(pnSearch);
            tabLabOut.Controls.Add(pnLabOut);
            tabSearch.Controls.Add(this.panel2);
            tabSearch.Controls.Add(this.panel1);

            this.WindowState = FormWindowState.Maximized;

            //pnSearch.ResumeLayout(false);
            pnLabOut.ResumeLayout(false);
            pnLabOutBotton.ResumeLayout(false);
            pnLabOutTop.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tabSearch.ResumeLayout(false);
            tabLabOut.ResumeLayout(false);
            this.ResumeLayout(false);
            //pnSearch.PerformLayout();
            pnLabOut.PerformLayout();
            pnLabOutBotton.PerformLayout();
            pnLabOutTop.PerformLayout();
            panel1.PerformLayout();
            panel2.PerformLayout();
            panel3.PerformLayout();
            this.PerformLayout();
        }
        private void FrmlabOutReceiveView1_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Text = "Last Update 2020-02-22 ";
        }
    }
}