using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1Ribbon;
using C1.Win.C1SplitContainer;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace bangna_hospital.gui
{
    public class FrmXrayPACsAdd:Form
    {
        BangnaControl bc;
        MainMenu menu;
        Login login;
        Font fEdit, fEditB, fEditBig, ffB;
        Color bg, fc, color;

        C1StatusBar sb1;
        C1ThemeController theme1;
        C1DockingTab tC1;
        C1FlexGrid grfReq, grfProc;
        C1DockingTabPage tabReq, tabApm, tabFinish, tabListen;
        C1SplitContainer splitContainer1;
        C1SplitterPanel c1SplitterPanel1;
        C1SplitterPanel c1SplitterPanel2;
        C1Button btnLisStart;
        C1TextBox txtIp, txtPort;
        Label lbTxtIp, lbTxtPort;
        ListBox listBox1, listBox2;

        int colReqId = 1, colReqHn = 2, colReqName = 3, colReqVn = 4, colReqXn = 5, colReqDtr = 6, colReqDpt = 7, colReqreqyr = 8, colReqreqno = 9, colreqhnyr = 10, colreqpreno = 11, colreqsex = 12, colreqdob = 13, colreqsickness = 14, colxrdesc = 15;
        Timer timer1;

        Panel panel1, pnHead, pnBotton, pnQue, pnListen;
        Form frmFlash;
        Image imgStart, imgStop;

        private StreamWriter serverStreamWriter;
        private StreamReader serverStreamReader;
        private Thread n_server;

        private StreamReader clientStreamReader;
        private StreamWriter clientStreamWriter;
        TcpClient tcpClient;
        public FrmXrayPACsAdd(BangnaControl bc)
        {
            showFormWaiting();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            initCompoment();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 7, FontStyle.Regular);

            timer1 = new System.Windows.Forms.Timer();
            timer1.Enabled = true;
            timer1.Interval = bc.timerCheckLabOut * 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Stop();

            txtIp.Value = bc.iniC.pacsServerIP;
            txtPort.Value = bc.iniC.pacsServerPort;

            this.Load += FrmXrayPACsAdd_Load;
            btnLisStart.Click += BtnLisStart_Click;

            //this.c1List1.AddItemTitles("First Name; LastName; Phone Number");
            initGrfReq();
            setGrfReq();
        }
        private void initGrfReq()
        {
            grfReq = new C1FlexGrid();
            grfReq.Font = fEdit;
            grfReq.Dock = System.Windows.Forms.DockStyle.Fill;
            grfReq.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfReq.DoubleClick += GrfReq_DoubleClick;
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfBillD.AfterDataRefresh += GrfBillD_AfterDataRefresh;
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("ออก บิล", new EventHandler(ContextMenu_edit_bill));
            //menuGw.MenuItems.Add("ส่งกลับ", new EventHandler(ContextMenu_send_back));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            //grfBillD.ContextMenu = menuGw;
            //grfBillD.SubtotalPosition = SubtotalPositionEnum.BelowData;
            pnQue.Controls.Add(grfReq);

            theme1.SetTheme(grfReq, "Office2010Red");

            //theme1.SetTheme(tabDiag, "Office2010Blue");
            //theme1.SetTheme(tabFinish, "Office2010Blue");

        }

        private void GrfReq_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfReq.Row <= 0) return;
            if (grfReq.Col <= 0) return;
            String hn = "", name = "", sex = "", dob = "", sickness = "", vn = "", hnreqyear = "", preno = "", reqno = "", xray = "";
            hn = grfReq[grfReq.Row, colReqHn] != null ? grfReq[grfReq.Row, colReqHn].ToString() : "";
            name = grfReq[grfReq.Row, colReqName] != null ? grfReq[grfReq.Row, colReqName].ToString() : "";
            sex = grfReq[grfReq.Row, colreqsex] != null ? grfReq[grfReq.Row, colreqsex].ToString() : "";
            dob = grfReq[grfReq.Row, colreqdob] != null ? grfReq[grfReq.Row, colreqdob].ToString() : "";
            sickness = grfReq[grfReq.Row, colreqsickness] != null ? grfReq[grfReq.Row, colreqsickness].ToString() : "";
            vn = grfReq[grfReq.Row, colReqVn] != null ? grfReq[grfReq.Row, colReqVn].ToString() : "";
            hnreqyear = grfReq[grfReq.Row, colReqreqyr] != null ? grfReq[grfReq.Row, colReqreqyr].ToString() : "";
            preno = grfReq[grfReq.Row, colreqpreno] != null ? grfReq[grfReq.Row, colreqpreno].ToString() : "";
            reqno = grfReq[grfReq.Row, colReqreqno] != null ? grfReq[grfReq.Row, colReqreqno].ToString() : "";
            xray = grfReq[grfReq.Row, colxrdesc] != null ? grfReq[grfReq.Row, colxrdesc].ToString() : "";
            ResOrderTab reso = new ResOrderTab();
            //MessageBox.Show("reqno " + reqno+ "\n hnreqyear "+ hnreqyear, "");
            reso = bc.bcDB.resoDB.setResOrderTab(hn, name, vn, hnreqyear, preno, reqno, dob, sex, sickness, xray);
            //MessageBox.Show("InsertDate " + reso.InsertDate , "");




            //ใช้งานจริงๆ เอา comment ออก
            //String re = bc.bcDB.resoDB.insertResOrderTab(reso, "");
            ////MessageBox.Show("re " + re, "");
            //long chk = 0, chk1 = 0;
            //if (long.TryParse(re, out chk))
            //{
            //    //MessageBox.Show("chk " + chk, "");
            //    String re1 = "";
            //    re1 = bc.bcDB.xrDB.updateStatusPACs(reqno, hnreqyear);
            //    //MessageBox.Show("re1 " + re1, "");
            //    if (long.TryParse(re1, out chk1))
            //    {
            //        setGrfReq();
            //    }
            //}
            if (tcpClient == null || !tcpClient.Connected)
            {
                ConnectToServer();
            }
            if (tcpClient.Connected)
            {
                try
                {
                    //send message to server
                    String txt = "", txt1="";
                    txt = reso.KPatientName + " " + reso.PatientID;
                    clientStreamWriter.WriteLine(txt);
                    clientStreamWriter.Flush();
                    Application.DoEvents();
                    txt1 = clientStreamReader.ReadLine();
                    Console.WriteLine("SERVER: " + txt1);
                    listBox2.Items.Add("SERVER " + txt1 + "  " + System.DateTime.Now.ToString());
                    Application.DoEvents();
                }
                catch (Exception se)
                {
                    Console.WriteLine(se.StackTrace);
                    listBox2.Items.Add("Error " + se.StackTrace + "  " + System.DateTime.Now.ToString());
                    Application.DoEvents();
                }
                finally
                {
                    //tcpClient.Close();
                    //clientStreamWriter.Close();
                    //clientStreamReader.Close();
                    Application.DoEvents();
                    //tcpClient = null;
                    //clientStreamWriter.Dispose();
                    //clientStreamReader.Dispose();

                }
            }
        }
        private bool ConnectToServer()
        {
            //connect to server at given port
            try
            {
                tcpClient = new TcpClient(bc.iniC.pacsServerIP, int.Parse(bc.iniC.pacsServerPort));
                Console.WriteLine("Connected to Server");
                listBox1.Items.Add("Connected to Server " + bc.iniC.pacsServerIP + " " + bc.iniC.pacsServerPort + " " + System.DateTime.Now.ToString());
                Application.DoEvents();
                //get a network stream from server
                NetworkStream clientSockStream = tcpClient.GetStream();
                clientStreamReader = new StreamReader(clientSockStream);
                clientStreamWriter = new StreamWriter(clientSockStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                listBox1.Items.Add("Error " + e.StackTrace + " " + System.DateTime.Now.ToString());
                Application.DoEvents();
                return false;
            }

            return true;
        }
        private void setGrfReq()
        {
            //grfDept.Rows.Count = 7;
            grfReq.Clear();
            grfReq.Rows.Count = 1;
            DataTable dt = new DataTable();
            String date = "";
            date = System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd");

            dt = bc.bcDB.xrDB.selectVisitStatusPacsReqByDate(date);
            //grfExpn.Rows.Count = dt.Rows.Count + 1;

            grfReq.Rows.Count = dt.Rows.Count + 1;
            grfReq.Cols.Count = 16;
            //C1TextBox txt = new C1TextBox();
            //C1ComboBox cboproce = new C1ComboBox();
            //ic.ivfDB.itmDB.setCboItem(cboproce);
            //grfBillD.Cols[colName].Editor = txt;
            //grfBillD.Cols[colAmt].Editor = txt;
            //grfBillD.Cols[colDiscount].Editor = txt;
            //grfBillD.Cols[colNetAmt].Editor = txt;

            grfReq.Cols[colReqHn].Width = 80;
            grfReq.Cols[colReqName].Width = 200;
            grfReq.Cols[colReqVn].Width = 80;
            grfReq.Cols[colReqXn].Width = 80;
            grfReq.Cols[colReqDtr].Width = 100;
            grfReq.Cols[colReqDpt].Width = 100;
            grfReq.Cols[colreqsex].Width = 60;
            grfReq.Cols[colreqdob].Width = 60;
            grfReq.Cols[colreqsickness].Width = 160;
            grfReq.Cols[colxrdesc].Width = 180;

            grfReq.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            grfReq.Cols[colReqHn].Caption = "HN";
            grfReq.Cols[colReqName].Caption = "Name";
            grfReq.Cols[colReqVn].Caption = "VN";
            grfReq.Cols[colReqXn].Caption = "XN";
            grfReq.Cols[colReqDtr].Caption = "Doctor";
            grfReq.Cols[colReqDpt].Caption = "Department";
            grfReq.Cols[colreqsex].Caption = "Sex";
            grfReq.Cols[colreqdob].Caption = "DOB";
            grfReq.Cols[colreqsickness].Caption = "Sickness";
            grfReq.Cols[colxrdesc].Caption = "X-Ray";

            Color color = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            //CellRange rg1 = grfBank.GetCellRange(1, colE, grfBank.Rows.Count, colE);
            //rg1.Style = grfBank.Styles["date"];
            //grfCu.Cols[colID].Visible = false;
            int i = 1;
            Decimal inc = 0, ext = 0;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfReq[i, 0] = i;
                    grfReq[i, colReqId] = "";
                    grfReq[i, colReqHn] = row["mnc_hn_no"].ToString();
                    grfReq[i, colReqName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfReq[i, colReqVn] = row["mnc_vn_no"].ToString() + "." + row["mnc_vn_seq"].ToString() + "." + row["mnc_vn_sum"].ToString();

                    grfReq[i, colReqXn] = "";
                    grfReq[i, colReqDtr] = "";
                    grfReq[i, colReqDpt] = "";
                    grfReq[i, colReqreqyr] = row["mnc_req_yr"].ToString();
                    grfReq[i, colReqreqno] = row["mnc_req_no"].ToString();
                    grfReq[i, colreqhnyr] = row["mnc_hn_yr"].ToString();
                    grfReq[i, colreqpreno] = row["mnc_pre_no"].ToString();
                    grfReq[i, colreqsex] = row["mnc_sex"].ToString();
                    grfReq[i, colreqsickness] = row["mnc_shif_memo"].ToString();
                    DateTime dt1 = new DateTime();
                    DateTime.TryParse(row["mnc_bday"].ToString(), out dt1);

                    grfReq[i, colreqdob] = dt1.Year + dt1.ToString("MMdd");
                    grfReq[i, colxrdesc] = row["MNC_XR_DSC"].ToString();
                    i++;
                }
                catch (Exception ex)
                {
                    String err = "";
                }
            }
            CellNoteManager mgr = new CellNoteManager(grfReq);
            grfReq.Cols[colReqId].Visible = false;
            grfReq.Cols[colReqreqyr].Visible = false;
            grfReq.Cols[colReqreqno].Visible = false;
            grfReq.Cols[colreqhnyr].Visible = false;
            grfReq.Cols[colreqpreno].Visible = false;
            grfReq.Cols[colReqDpt].Visible = false;
            grfReq.Cols[colReqDtr].Visible = false;

            grfReq.Cols[colReqHn].AllowEditing = false;
            grfReq.Cols[colReqName].AllowEditing = false;
            grfReq.Cols[colReqVn].AllowEditing = false;
            grfReq.Cols[colReqXn].AllowEditing = false;
            grfReq.Cols[colReqDtr].AllowEditing = false;
            grfReq.Cols[colReqDpt].AllowEditing = false;

        }
        private bool StartServer()
        {
            //create server's tcp listener for incoming connection
            IPAddress ipad = IPAddress.Parse(txtIp.Text);
            TcpListener tcpServerListener = new TcpListener(ipad, int.Parse(txtPort.Text));
            tcpServerListener.Start();      //start server
            //Console.WriteLine("Server Started");
            //listBox1.Items.Add("Start Listening " + System.DateTime.Now.ToString());
            listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add("Start Listening " + System.DateTime.Now.ToString());});
            Application.DoEvents();
            //this.btnStartServer.Enabled = false;
            //block tcplistener to accept incoming connection
            Socket serverSocket = tcpServerListener.AcceptSocket();

            try
            {
                if (serverSocket.Connected)
                {
                    //Console.WriteLine("Client connected");
                    //listBox1.Items.Add("Client connected " + System.DateTime.Now.ToString());
                    listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add("Client connected " + System.DateTime.Now.ToString()); });
                    Application.DoEvents();
                    //open network stream on accepted socket
                    NetworkStream serverSockStream = new NetworkStream(serverSocket);
                    serverStreamWriter = new StreamWriter(serverSockStream);
                    serverStreamReader = new StreamReader(serverSockStream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

            return true;
        }
        private void listenServer()
        {
            if (StartServer())
            {
                while (true)
                {
                    //Console.WriteLine("CLIENT: " + serverStreamReader.ReadLine());
                    string line = "";
                    if ((line = serverStreamReader.ReadLine()) != null)
                    {
                        listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add("Date Time : " + System.DateTime.Now.ToString() + "Receive : " + line); });
                        Application.DoEvents();
                        serverStreamWriter.WriteLine("Hi!");
                        serverStreamWriter.Flush();
                    }

                    //listBox1.Items.Add("Date Time : " + System.DateTime.Now.ToString() + " Receive " + serverStreamReader.ReadLine());

                }//while
            }
        }
        private void BtnLisStart_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(btnLisStart.Image.GetHashCode() == imgStart.GetHashCode())
            {
                btnLisStart.Image = imgStop;
                btnLisStart.Text = "Stop";
                listBox1.Items.Clear();
                //listenServer();
                n_server = new Thread(new ThreadStart(listenServer));
                n_server.IsBackground = true;
                n_server.Start();
                //listBox1.Items.Add("Start Listening "+ System.DateTime.Now.ToString());
            }
            else
            {
                btnLisStart.Image = imgStart;
                btnLisStart.Text = "Start";
                listBox1.Items.Add("Stop Listening " + System.DateTime.Now.ToString());
            }
        }
        private void initCompoment()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            imgStart = Resources.start128;
            imgStop = Resources.stop_red128;

            theme1 = new C1ThemeController();
            sb1 = new C1StatusBar();
            panel1 = new Panel();
            tC1 = new C1DockingTab();
            tabReq = new C1DockingTabPage();
            tabApm = new C1DockingTabPage();
            tabFinish = new C1DockingTabPage();
            tabListen = new C1DockingTabPage();
            splitContainer1 = new C1SplitContainer();
            c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            pnListen = new Panel();
            listBox1 = new System.Windows.Forms.ListBox();
            pnQue = new Panel();
            listBox2 = new System.Windows.Forms.ListBox();

            panel1.SuspendLayout();
            tC1.SuspendLayout();
            tabReq.SuspendLayout();
            tabApm.SuspendLayout();
            tabFinish.SuspendLayout();
            tabListen.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnListen.SuspendLayout();
            pnQue.SuspendLayout();

            this.SuspendLayout();

            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Name = "panel1";

            tabReq.Name = "tabReq";
            tabReq.TabIndex = 0;
            tabReq.Text = "Request";
            tabReq.Font = fEditB;

            tabApm.Name = "tabApm";
            tabApm.TabIndex = 1;
            tabApm.Text = "Process";
            tabApm.Font = fEditB;

            tabFinish.Name = "tabFinish";
            tabFinish.TabIndex = 2;
            tabFinish.Text = "Finish";
            tabFinish.Font = fEditB;

            tabListen.Name = "tabListen";
            tabListen.TabIndex = 2;
            tabListen.Text = "Listen PACs Infinitt";
            tabListen.Font = fEditB;

            pnListen.Dock = System.Windows.Forms.DockStyle.Fill;
            pnListen.Name = "pnListen";
            pnQue.Dock = System.Windows.Forms.DockStyle.Fill;
            pnQue.Name = "pnQue";

            sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            sb1.Name = "sb1";

            tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            tC1.HotTrack = true;
            tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            tC1.TabsShowFocusCues = true;
            tC1.Alignment = TabAlignment.Top;
            tC1.SelectedTabBold = true;
            tC1.Name = "tC1";
            tC1.Font = fEditB;
            tC1.CanCloseTabs = true;
            tC1.CanAutoHide = false;
            tC1.SelectedTabBold = true;

            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Size = new System.Drawing.Size(800, 450);
            splitContainer1.TabIndex = 0;
            
            c1SplitterPanel1.Collapsible = true;
            c1SplitterPanel1.Height = 86;
            c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            c1SplitterPanel1.Name = "c1SplitterPanel1";
            c1SplitterPanel1.Size = new System.Drawing.Size(298, 58);
            c1SplitterPanel1.TabIndex = 0;
            c1SplitterPanel1.Text = "Panel 1";
            c1SplitterPanel1.Dock = PanelDockStyle.Top;
            this.c1SplitterPanel2.Height = 85;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(0, 111);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(298, 64);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            
            setControlComponent();

            this.Controls.Add(panel1);
            this.Controls.Add(this.sb1);
            panel1.Controls.Add(tC1);
            tC1.Controls.Add(tabReq);
            tC1.Controls.Add(tabApm);
            tC1.Controls.Add(tabFinish);
            tC1.Controls.Add(tabListen);
            tabReq.Controls.Add(splitContainer1);
            splitContainer1.Panels.Add(c1SplitterPanel1);
            splitContainer1.Panels.Add(c1SplitterPanel2);
            tabListen.Controls.Add(pnListen);
            c1SplitterPanel1.Controls.Add(pnQue);

            pnListen.Controls.Add(btnLisStart);
            pnListen.Controls.Add(lbTxtIp);
            pnListen.Controls.Add(txtIp);
            pnListen.Controls.Add(lbTxtPort);
            pnListen.Controls.Add(txtPort);
            pnListen.Controls.Add(listBox1);
            c1SplitterPanel2.Controls.Add(listBox2);

            panel1.ResumeLayout(false);
            tC1.ResumeLayout(false);
            tabReq.ResumeLayout(false);
            tabApm.ResumeLayout(false);
            tabFinish.ResumeLayout(false);
            tabListen.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            pnListen.ResumeLayout(false);
            pnQue.ResumeLayout(false);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void setControlComponent()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));

            btnLisStart = new C1Button();
            btnLisStart.Name = "btnLisStart";
            btnLisStart.Text = "Start";
            btnLisStart.Font = fEdit;
            //size = bc.MeasureString(btnHnSearch);
            btnLisStart.Location = new System.Drawing.Point(gapX, 20);
            btnLisStart.Size = new Size(200, 140);
            btnLisStart.Font = fEdit;
            btnLisStart.Image = imgStart;
            btnLisStart.TextAlign = ContentAlignment.MiddleRight;
            btnLisStart.ImageAlign = ContentAlignment.MiddleLeft;

            lbTxtIp = new Label();
            lbTxtIp.Text = "IP PACs : ";
            lbTxtIp.Font = fEditBig;
            lbTxtIp.Location = new System.Drawing.Point(btnLisStart.Location.X + btnLisStart.Width + 10, btnLisStart.Location.Y);
            lbTxtIp.AutoSize = true;
            lbTxtIp.Name = "lbTxtIp";

            txtIp = new C1TextBox();
            txtIp.Font = fEdit;
            size = bc.MeasureString(lbTxtIp);
            txtIp.Location = new System.Drawing.Point(lbTxtIp.Location.X + size.Width + 5, lbTxtIp.Location.Y);
            txtIp.Size = new Size(120, 20);

            lbTxtPort = new Label();
            lbTxtPort.Text = "PORT PACs : ";
            lbTxtPort.Font = fEditBig;
            lbTxtPort.Location = new System.Drawing.Point(btnLisStart.Location.X + btnLisStart.Width  + 10, btnLisStart.Location.Y + gapLine);
            lbTxtPort.AutoSize = true;
            lbTxtPort.Name = "lbTxtPort";

            txtPort = new C1TextBox();
            txtPort.Font = fEdit;
            size = bc.MeasureString(lbTxtPort);
            txtPort.Location = new System.Drawing.Point(lbTxtPort.Location.X + size.Width + 5, lbTxtPort.Location.Y);
            txtPort.Size = new Size(120, 20);

            listBox1.Dock = System.Windows.Forms.DockStyle.None;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new System.Drawing.Point(btnLisStart.Location.X, btnLisStart.Height + gapLine+10);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(600, 450);
            listBox1.TabIndex = 0;

            listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox2.FormattingEnabled = true;
            listBox2.Location = new System.Drawing.Point(btnLisStart.Location.X, btnLisStart.Height + gapLine + 10);
            listBox2.Name = "listBox2";
            listBox2.Size = new System.Drawing.Size(600, 450);
            listBox2.TabIndex = 0;

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
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void FrmXrayPACsAdd_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Text = "Lasst Update 2020-03-07 pacsServerIP " + bc.iniC.pacsServerIP+ " pacsServerPort " + bc.iniC.pacsServerPort;
            frmFlash.Dispose();
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
