using bangna_hospital.control;
using bangna_hospital.FlexGrid;
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
        C1FlexGrid grfReq, grfProc, grfFinish, grfMaster;
        C1DockingTabPage tabReq, tabProc, tabFinish, tabListen, tabmaster;
        C1SplitContainer splitContainer1, sCMaster;
        C1SplitterPanel c1SplitterPanel1, c1SplitterPanel2, scpMasterTop, scpMasterButton;
        
        C1Button btnLisStart, btnModality, btnBrowTxtInfinitt;
        C1TextBox txtIp, txtPort,txtXrCode, txtXrName, txtPacsCode, txtModality;
        Label lbTxtIp, lbTxtPort, lbXrCode, lbXrName, lbPacsCode, lbModality;
        C1ComboBox cboModality;
        ListBox lboxServer, lboxClient;

        int colReqId = 1, colReqHn = 2, colReqName = 3, colReqVn = 4, colReqXn = 5, colReqDtr = 6, colReqDpt = 7, colReqreqyr = 8, colReqreqno = 9, colreqhnyr = 10, colreqpreno = 11, colreqsex = 12, colreqdob = 13, colreqsickness = 14, colxrdesc = 15, colxrcode = 16, colxrstfcode = 17, colxrstfname = 18, colxrdepno = 19, colxrdepname = 20, colpttstatus = 21, colxrgrpcd = 22, colxrgrpdsc = 23, colxrdate=24;

        int colMasId = 1, colMasCode = 2, colMasDsc = 3, colMasTyp = 4, colMasGrp = 5, colMasDis = 6, colMasDeccode = 7, colMasDecNo = 8, colMasInfinittCode=9, colMasModalityCode=10;
        Timer timer1;

        Panel panel1, pnHead, pnBotton, pnQue, pnListen, pnMaster, pnProc;
        Form frmFlash;
        Image imgStart, imgStop, imgSave;

        private StreamWriter serverStreamWriter;
        private StreamReader serverStreamReader;
        private Thread n_server;

        //private StreamReader clientStreamReader;
        private StreamWriter clientStreamWriter;
        //TcpClient tcpClient;
        Socket serverSocket;
        TcpListener tcpServerListener;
        NetworkStream serverSockStream;
        Boolean pageLoad = false;
        public FrmXrayPACsAdd(BangnaControl bc)
        {
            showFormWaiting();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            //MessageBox.Show("0000000", "");
            initCompoment();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 7, FontStyle.Regular);

            timer1 = new System.Windows.Forms.Timer();
            timer1.Enabled = true;
            timer1.Interval = bc.timerCheckLabOut * 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Stop();
            //MessageBox.Show("11111", "");
            txtIp.Value = bc.iniC.pacsServerIP;
            txtPort.Value = bc.iniC.pacsServerPort;

            this.Load += FrmXrayPACsAdd_Load;
            btnLisStart.Click += BtnLisStart_Click;
            btnModality.Click += BtnModality_Click;
            cboModality.SelectedItemChanged += CboModality_SelectedItemChanged;
            this.Disposed += FrmXrayPACsAdd_Disposed;
            tC1.SelectedIndexChanged += TC1_SelectedIndexChanged;
            tabReq.DoubleClick += TabReq_DoubleClick;
            btnBrowTxtInfinitt.Click += BtnBrowTxtInfinitt_Click;

            //this.c1List1.AddItemTitles("First Name; LastName; Phone Number");
            initGrfReq();
            setGrfReq();
            initGrfMaster();
            setGrfMaster();
            initGrfProcess();
            setGrfProc();
            //MessageBox.Show("22222", "");
            if (bc.iniC.statusLabOutReceiveOnline.Equals("1"))
            {
                timer1.Start();
                tabListen.Hide();
                //tC1.TabPages[3].Hide();
            }
            else
            {
                timer1.Stop();
            }
            pageLoad = false;
        }

        private void BtnBrowTxtInfinitt_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog res = new OpenFileDialog();

            //Filter
            res.Filter = "Txt Files|*.txt;";

            //When the user select the file
            if (res.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                var filePath = res.FileName;
                //Do something
                string text, err="";
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(filePath, Encoding.UTF8))
                {
                    text = streamReader.ReadToEnd();
                }

                //lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Date Time : " + System.DateTime.Now.ToString() + "write file success "); });
                Application.DoEvents();
                try
                {
                    String assionno = "", hn = "", reqno = "", reqdate = "", code = "", hnyr = "", diag = "", dtrcode = "";
                    String[] txt = text.Split(Environment.NewLine.ToCharArray());
                    foreach (String txt1 in txt)
                    {
                        String[] txt2 = txt1.Split('|');
                        if (txt2.Length > 4)
                        {
                            if (txt2[0] == "PID")
                            {
                                hn = txt2[3].Replace("BN5", "").Replace("^", "");
                            }
                            else if (txt2[0] == "OBR")
                            {
                                assionno = txt2[3];
                                if (assionno.Length > 10)
                                {
                                    hnyr = assionno.Substring(0, 4);
                                    reqdate = assionno.Substring(0, 8);
                                    code = assionno.Substring(assionno.Length - 4);
                                    reqno = assionno.Replace(reqdate, "").Replace(code, "");
                                    int hnyr1 = 0;
                                    int.TryParse(hnyr, out hnyr1);
                                    hnyr1 += 543;
                                    hnyr = hnyr1.ToString();
                                }
                            }
                            else if (txt2[0] == "OBX")
                            {
                                diag += txt2[5] + Environment.NewLine;
                                if (dtrcode.Length == 0)
                                {
                                    dtrcode = txt2[txt2.Length - 1];
                                    String[] dtr = dtrcode.Split('^');
                                    if (dtr.Length > 1)
                                    {
                                        dtrcode = dtr[0];
                                    }
                                }
                            }
                        }
                    }
                    lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Doctor : " + dtrcode + " hn " + hn + " reqdate " + reqdate + " reqno " + reqno + " code " + code); });
                    Application.DoEvents();
                    String xrcode = "", chkinsert = "", re = "";
                    err = "00";
                    xrcode = bc.bcDB.xrDB.selectXrayByCode1(code);
                    err = "01";
                    lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Doctor : " + dtrcode + " hn " + hn + " reqdate " + reqdate + " reqno " + reqno + " code " + code + " xrcode " + xrcode); });
                    Application.DoEvents();
                    chkinsert = bc.bcDB.xrt04DB.selectByPk(hnyr, reqdate, reqno, xrcode);
                    err = "02";
                    XrayT04 xrt04 = new XrayT04();
                    xrt04.MNC_REQ_YR = hnyr;
                    xrt04.MNC_REQ_NO = reqno;
                    xrt04.MNC_REQ_DAT = reqdate;
                    xrt04.MNC_XR_DSC = diag;
                    xrt04.MNC_DOT_DF_CD = dtrcode;
                    xrt04.MNC_XR_CD = xrcode;
                    xrt04.MNC_STS = "N";
                    xrt04.MNC_XR_USR = "00000";
                    if (chkinsert.Equals("insert"))
                    {
                        err = "03";
                        re = bc.bcDB.xrt04DB.insert(xrt04);
                        long chk11 = 0;
                        if (long.TryParse(re, out chk11))
                        {
                            lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Insert success "); });
                        }
                        else
                        {
                            lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("no Insert error " + re); });
                            new LogWriter("e", "getMessageClient Insert  err " + re);
                        }
                        Application.DoEvents();
                    }
                    else
                    {
                        err = "04";
                        re = bc.bcDB.xrt04DB.update(xrt04);
                        lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Update success "); });
                        Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "getMessageClient 00 ex " + ex.Message + " err " + err);
                    lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Error " + ex.Message); });
                    Application.DoEvents();
                }
            }
        }

        private void TabReq_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfReq();
        }

        private void TC1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(tC1.SelectedTab == tabReq)
            {
                setGrfReq();
            }
            else if (tC1.SelectedTab == tabProc)
            {
                setGrfProc();
            }
        }

        private void CboModality_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!pageLoad)
            {
                ComboBoxItem item = (ComboBoxItem)cboModality.SelectedItem;

                txtModality.Value = cboModality.SelectedItem != null ? ((ComboBoxItem)cboModality.SelectedItem).Value : "";
            }
        }

        private void BtnModality_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("Save Modality ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String id = "", name = "", modality = "", pacscpde = "", re="";
                id = txtXrCode.Text.Trim();
                modality = cboModality.SelectedItem != null ? ((ComboBoxItem)cboModality.SelectedItem).Value : "";
                re = bc.bcDB.xrDB.updateModality(id, modality);
                if(int.TryParse(re, out chk) && chk > 0)
                {
                    MessageBox.Show("save Modality success "+ modality, "");
                    setGrfMaster();
                }
            }
        }

        private void FrmXrayPACsAdd_Disposed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if(tcpClient != null)
            //{
            //    if (tcpClient.Connected)
            //    {
            //        tcpClient.Close();
            //    }
            //}
        }
        private void initGrfMaster()
        {
            grfMaster = new C1FlexGrid();
            grfMaster.Font = fEdit;
            grfMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMaster.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfMaster.DoubleClick += GrfMasterDoubleClick;
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfBillD.AfterDataRefresh += GrfBillD_AfterDataRefresh;
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("ออก บิล", new EventHandler(ContextMenu_edit_bill));
            //menuGw.MenuItems.Add("ส่งกลับ", new EventHandler(ContextMenu_send_back));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            //grfBillD.ContextMenu = menuGw;
            //grfBillD.SubtotalPosition = SubtotalPositionEnum.BelowData;
            scpMasterTop.Controls.Add(grfMaster);

            theme1.SetTheme(grfMaster, bc.iniC.themeApp);

            //theme1.SetTheme(tabDiag, "Office2010Blue");
            //theme1.SetTheme(tabFinish, "Office2010Blue");

        }

        private void GrfMasterDoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMaster == null) return;
            if (grfMaster.Row <= 0) return;
            if (grfMaster.Col <= 0) return;

            String id = "", name = "", modality = "", pacscpde = "";
            id = grfMaster[grfMaster.Row, colMasId].ToString();
            name = grfMaster[grfMaster.Row, colMasDsc].ToString();
            modality = grfMaster[grfMaster.Row, colMasModalityCode].ToString();
            pacscpde = grfMaster[grfMaster.Row, colMasInfinittCode].ToString();
            txtXrCode.Value = id;
            txtXrName.Value = name;
            txtPacsCode.Value = pacscpde;
            bc.setC1Combo(cboModality, modality);
        }
        private void setGrfMaster()
        {
            //grfDept.Rows.Count = 7;
            grfMaster.Clear();
            grfMaster.Rows.Count = 1;
            DataTable dt = new DataTable();
            
            dt = bc.bcDB.xrDB.selectAll();
            //grfExpn.Rows.Count = dt.Rows.Count + 1;

            grfMaster.Rows.Count = dt.Rows.Count + 1;
            grfMaster.Cols.Count = 11;            

            grfMaster.Cols[colMasCode].Width = 80;
            grfMaster.Cols[colMasDsc].Width = 200;
            grfMaster.Cols[colMasTyp].Width = 80;
            grfMaster.Cols[colMasGrp].Width = 80;
            grfMaster.Cols[colMasDis].Width = 80;
            grfMaster.Cols[colMasDeccode].Width = 100;
            grfMaster.Cols[colMasDecNo].Width = 60;
            grfMaster.Cols[colMasInfinittCode].Width = 60;
            grfMaster.Cols[colMasModalityCode].Width = 80;            

            grfReq.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            grfMaster.Cols[colMasCode].Caption = "Code";
            grfMaster.Cols[colMasDsc].Caption = "Name";
            grfMaster.Cols[colMasTyp].Caption = "TYP_CD";
            grfMaster.Cols[colMasGrp].Caption = "GRP_CD";
            grfMaster.Cols[colMasDis].Caption = "MNC_XR_DIS_STS";
            grfMaster.Cols[colMasDeccode].Caption = "MNC_DEC_CD";
            grfMaster.Cols[colMasDecNo].Caption = "MNC_DEC_NO";
            grfMaster.Cols[colMasInfinittCode].Caption = "Infinitt";
            grfMaster.Cols[colMasModalityCode].Caption = "Modality";
            //grfReq.Cols[colxrdesc].Caption = "X-Ray";
            //grfReq.Cols[colxrcode].Caption = "";

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
                    grfMaster[i, 0] = i;
                    grfMaster[i, colMasId] = row["MNC_XR_CD"].ToString();
                    grfMaster[i, colMasCode] = row["MNC_XR_CTL_CD"].ToString();
                    grfMaster[i, colMasDsc] = row["MNC_XR_DSC"].ToString();
                    grfMaster[i, colMasTyp] = row["MNC_XR_TYP_CD"].ToString();

                    grfMaster[i, colMasGrp] = row["MNC_XR_GRP_CD"].ToString();
                    grfMaster[i, colMasDis] = row["MNC_XR_DIS_STS"].ToString();
                    grfMaster[i, colMasDeccode] = row["MNC_DEC_CD"].ToString();
                    grfMaster[i, colMasDecNo] = row["MNC_DEC_NO"].ToString();
                    grfMaster[i, colMasInfinittCode] = row["pacs_infinitt_code"].ToString();
                    grfMaster[i, colMasModalityCode] = row["modality_code"].ToString();
                    
                    i++;
                }
                catch (Exception ex)
                {
                    String err = "";
                }
            }
            CellNoteManager mgr = new CellNoteManager(grfReq);
            //grfReq.Cols[colReqId].Visible = false;
            //grfReq.Cols[colReqreqyr].Visible = false;
            //grfReq.Cols[colReqreqno].Visible = false;
            //grfReq.Cols[colreqhnyr].Visible = false;
            //grfReq.Cols[colreqpreno].Visible = false;
            //grfReq.Cols[colReqDpt].Visible = false;
            //grfReq.Cols[colReqDtr].Visible = false;

            grfMaster.Cols[colMasId].AllowEditing = false;
            grfMaster.Cols[colMasCode].AllowEditing = false;
            grfMaster.Cols[colMasDsc].AllowEditing = false;
            grfMaster.Cols[colMasTyp].AllowEditing = false;
            grfMaster.Cols[colMasGrp].AllowEditing = false;
            grfMaster.Cols[colMasDis].AllowEditing = false;
            grfMaster.Cols[colMasDeccode].AllowEditing = false;
            grfMaster.Cols[colMasDecNo].AllowEditing = false;
            grfMaster.Cols[colMasInfinittCode].AllowEditing = false;
            grfMaster.Cols[colMasModalityCode].AllowEditing = false;
        }
        private void initGrfProcess()
        {
            grfProc = new C1FlexGrid();
            grfProc.Font = fEdit;
            grfProc.Dock = System.Windows.Forms.DockStyle.Fill;
            grfProc.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            //grfReq.DoubleClick += GrfReq_DoubleClick;
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfBillD.AfterDataRefresh += GrfBillD_AfterDataRefresh;
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("ออก บิล", new EventHandler(ContextMenu_edit_bill));
            //menuGw.MenuItems.Add("ส่งกลับ", new EventHandler(ContextMenu_send_back));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            //grfBillD.ContextMenu = menuGw;
            //grfBillD.SubtotalPosition = SubtotalPositionEnum.BelowData;
            pnProc.Controls.Add(grfProc);

            theme1.SetTheme(grfProc, bc.iniC.themeApp);
            FilterRow fr = new FilterRow(grfProc);
            grfProc.AllowFiltering = true;
            grfProc.AfterFilter += GrfProc_AfterFilter;
            //theme1.SetTheme(tabDiag, "Office2010Blue");
            //theme1.SetTheme(tabFinish, "Office2010Blue");

        }
        private void setGrfProc()
        {
            //grfDept.Rows.Count = 7;
            //grfReq.Clear();
            grfProc.Rows.Count = 1;
            DataTable dt = new DataTable();
            String date = "";
            date = System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd");

            dt = bc.bcDB.xrDB.selectVisitStatus1PacsReqByDate(date);
            //dt = bc.bcDB.xrDB.selectVisitStatusPacsReqByDateTestHN(date, "5203491");
            //grfExpn.Rows.Count = dt.Rows.Count + 1;

            grfProc.Rows.Count = dt.Rows.Count + 1;
            grfProc.Cols.Count = 31;

            grfProc.Cols[colReqHn].Width = 80;
            grfProc.Cols[colReqName].Width = 200;
            grfProc.Cols[colReqVn].Width = 80;
            grfProc.Cols[colReqXn].Width = 80;
            grfProc.Cols[colReqDtr].Width = 100;
            grfProc.Cols[colReqDpt].Width = 100;
            grfProc.Cols[colreqsex].Width = 60;
            grfProc.Cols[colreqdob].Width = 60;
            grfProc.Cols[colreqsickness].Width = 160;
            grfProc.Cols[colxrdesc].Width = 180;
            grfProc.Cols[colxrcode].Width = 80;
            grfProc.Cols[colxrcode].Width = 80;
            grfProc.Cols[colxrdepno].Width = 80;

            grfProc.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            

            Color color = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            //CellRange rg1 = grfBank.GetCellRange(1, colE, grfBank.Rows.Count, colE);
            //rg1.Style = grfBank.Styles["date"];
            //grfCu.Cols[colID].Visible = false;
            int i = 0;
            Decimal inc = 0, ext = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfProc.Cols[col + 1].DataType = dt.Columns[col].DataType;
                grfProc.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfProc.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            grfProc.Cols[colReqHn].Caption = "HN";
            grfProc.Cols[colReqName].Caption = "Name";
            grfProc.Cols[colReqVn].Caption = "VN";
            grfProc.Cols[colReqXn].Caption = "XN";
            grfProc.Cols[colReqDtr].Caption = "Doctor";
            grfProc.Cols[colReqDpt].Caption = "Department";
            grfProc.Cols[colreqsex].Caption = "Sex";
            grfProc.Cols[colreqdob].Caption = "DOB";
            grfProc.Cols[colreqsickness].Caption = "Sickness";
            grfProc.Cols[colxrdesc].Caption = "X-Ray";
            grfProc.Cols[colxrcode].Caption = "";
            grfProc.Cols[colxrgrpdsc].Caption = "Group";
            grfProc.Cols[colxrdate].Caption = "xr date";
            grfProc.Cols[colReqreqno].Caption = "req no";
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    i++;
                    if (i == 1) continue;
                    grfProc[i, 0] = (i-1);
                    grfProc[i, colReqId] = "";
                    grfProc[i, colReqHn] = row["mnc_hn_no"].ToString();
                    grfProc[i, colReqName] = row["fullname"].ToString();
                    grfProc[i, colReqVn] = row["mnc_vn_no"].ToString() + "." + row["mnc_vn_seq"].ToString() + "." + row["mnc_vn_sum"].ToString();

                    grfProc[i, colReqXn] = "";
                    grfProc[i, colReqDtr] = "";
                    grfProc[i, colReqDpt] = "";
                    grfProc[i, colReqreqyr] = row["mnc_req_yr"].ToString();
                    grfProc[i, colReqreqno] = row["mnc_req_no"].ToString();
                    //grfProc[i, colreqhnyr] = row["mnc_hn_yr"].ToString();
                    //grfProc[i, colreqpreno] = row["mnc_pre_no"].ToString();
                    grfProc[i, colreqsex] = row["mnc_sex"].ToString();
                    grfProc[i, colreqsickness] = row["mnc_shif_memo"].ToString();

                    grfProc[i, colreqdob] = row["mnc_bday"].ToString();
                    grfProc[i, colxrdesc] = row["MNC_XR_DSC"].ToString();
                    grfProc[i, colxrcode] = row["MNC_XR_CD"].ToString();
                    grfProc[i, colxrstfcode] = row["MNC_DOT_CD"].ToString();
                    grfProc[i, colxrstfname] = row["mnc_usr_full"].ToString();
                    grfProc[i, colxrdepno] = row["mnc_req_dep"].ToString();
                    grfProc[i, colxrdepname] = row["MNC_MD_DEP_DSC"].ToString();
                    grfProc[i, colpttstatus] = row["MNC_STS"].ToString();
                    grfProc[i, colxrgrpcd] = row["MNC_XR_GRP_CD"].ToString();
                    grfProc[i, colxrgrpdsc] = row["MNC_XR_GRP_dsc"].ToString();
                    grfProc[i, colxrdate] = row["mnc_req_dat1"].ToString();
                    
                }
                catch (Exception ex)
                {
                    String err = "";
                }
            }
            CellNoteManager mgr = new CellNoteManager(grfProc);
            grfProc.Cols[colReqId].Visible = false;
            grfProc.Cols[colReqreqyr].Visible = false;
            //grfProc.Cols[colReqreqno].Visible = false;
            grfProc.Cols[colreqhnyr].Visible = false;
            grfProc.Cols[colreqpreno].Visible = false;
            grfProc.Cols[colReqDpt].Visible = false;
            grfProc.Cols[colReqDtr].Visible = false;
            //grfProc.Cols[colxrgrpcd].Visible = false;

            grfProc.Cols[colReqHn].AllowEditing = false;
            grfProc.Cols[colReqName].AllowEditing = false;
            grfProc.Cols[colReqVn].AllowEditing = false;
            grfProc.Cols[colReqXn].AllowEditing = false;
            grfProc.Cols[colReqDtr].AllowEditing = false;
            grfProc.Cols[colReqDpt].AllowEditing = false;
            grfProc.Cols[colxrcode].AllowEditing = false;
            grfProc.Cols[colxrdepno].AllowEditing = false;
            grfProc.Cols[colxrdepname].AllowEditing = false;
            grfProc.Cols[colpttstatus].AllowEditing = false;
            grfProc.Cols[colxrgrpdsc].AllowEditing = false;
            grfProc.Cols[colxrdate].AllowEditing = false;
            
        }
        private void GrfProc_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfProc.Cols.Fixed; col < grfProc.Cols.Count; ++col)
            {
                var filter = grfProc.Cols[col].ActiveFilter;
            }
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
            String hn = "", name = "", sex = "", dob = "", sickness = "", vn = "", hnreqyear = "", preno = "", reqno = "", xray = "", xrcode="", stfcode="", stfname="", dtrid="";
            String opdtype = "", depcode = "", depname = "", xrgrpcd="", reqdate="", re = "0", re2 = "", reqyr="", modality = "";
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
            xrcode = grfReq[grfReq.Row, colxrcode] != null ? grfReq[grfReq.Row, colxrcode].ToString() : "";
            stfcode = grfReq[grfReq.Row, colxrstfcode] != null ? grfReq[grfReq.Row, colxrstfcode].ToString() : "";
            dtrid = grfReq[grfReq.Row, colReqDtr] != null ? grfReq[grfReq.Row, colReqDtr].ToString() : "";
            stfname = grfReq[grfReq.Row, colxrstfname] != null ? grfReq[grfReq.Row, colxrstfname].ToString() : "";
            depcode = grfReq[grfReq.Row, colxrdepno] != null ? grfReq[grfReq.Row, colxrdepno].ToString() : "";
            depname = grfReq[grfReq.Row, colxrdepname] != null ? grfReq[grfReq.Row, colxrdepname].ToString() : "";
            opdtype = grfReq[grfReq.Row, colpttstatus] != null ? grfReq[grfReq.Row, colpttstatus].ToString() : "";
            xrgrpcd = grfReq[grfReq.Row, colxrgrpcd] != null ? grfReq[grfReq.Row, colxrgrpcd].ToString() : "";
            reqdate = grfReq[grfReq.Row, colxrdate] != null ? grfReq[grfReq.Row, colxrdate].ToString() : "";
            reqyr = grfReq[grfReq.Row, colReqreqyr] != null ? grfReq[grfReq.Row, colReqreqyr].ToString() : "";
            opdtype = opdtype.Trim().Equals("I") ? "I" : "O";
            //reqdate = bc.datetoDB(reqdate);
            ResOrderTab reso = new ResOrderTab();
            //MessageBox.Show("reqno " + reqno+ "\n hnreqyear "+ hnreqyear, "");
            if (!xrgrpcd.Equals("U") || !xrgrpcd.Equals("C") || !xrgrpcd.Equals("G") || !xrgrpcd.Equals("M"))
            {
                String accessnumber="";
                if (xrgrpcd.Equals("U"))
                {
                    modality = "US";
                }
                else if (xrgrpcd.Equals("C"))
                {
                    modality = "CT";
                }
                else
                {
                    modality = "CR";
                    modality = "DX";  // ทาง ติ๊ก แจ้งให้เปลี่ยนเป็น DX
                }
                accessnumber = bc.bcDB.xrayM01DB.insertXrayAccessNumber(hn, reqdate, reqno, xrcode);
                reso = bc.bcDB.resoDB.setResOrderTab(hn, name, vn, preno, hnreqyear, reqno, dob, sex, sickness, xray, xrcode, depname, dtrid, stfname, modality, accessnumber, reqdate, reqyr);
                //re = "";
                ////MessageBox.Show("InsertDate " + reso.InsertDate , "");
                //new LogWriter("d", "FrmXrayPACsAdd  reso.AccessNumber " + reso.AccessNumber);

                ////ใช้งานจริงๆ เอา comment ออก

                re = bc.bcDB.resoDB.insertResOrderTab(reso, "");
                re2 = bc.bcDB.xrayT02DB.updateAccessNumber(hnreqyear, reqno, reqdate, xrcode, accessnumber);
            }

            ////MessageBox.Show("re " + re, "");
            long chk = 0, chk1 = 0;
            if (long.TryParse(re, out chk))
            {
                //MessageBox.Show("chk " + chk, "");
                String re1 = "";
                String date = "";
                date = System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd");
                re1 = bc.bcDB.xrDB.updateStatusPACs(reqno, hnreqyear, xrcode, reqdate);
                lboxClient.Items.Add("send success " + hn + "  " + System.DateTime.Now.ToString()+ " reso.AccessNumber " + reso.AccessNumber+ " modality "+ modality);
                //re1 = bc.bcDB.xrDB.updateStatusPACs(reqno, hnreqyear, xrcode, date);
                //MessageBox.Show("re1 " + re1, "");
                if (long.TryParse(re1, out chk1))
                {
                    setGrfReq();
                }
            }
            else
            {
                lboxClient.Items.Add("error " + re);
            }
            //if (tcpClient == null || !tcpClient.Connected)
            //{
            //    ConnectToServer();
            //}
            //if(tcpClient == null)
            //{
            //    Console.WriteLine("");
            //    lboxClient.Items.Add("Error tcpclient is null  " + System.DateTime.Now.ToString());
            //    Application.DoEvents();
            //    return;
            //}
            //if (tcpClient.Connected)
            //{
            if (bc.iniC.pacsServerIP.Length>0)
            {
                String txtADT = "", txtORM = "", resp = "";
                try
                {
                    //send message to server
                    //txt = reso.KPatientName + " " + reso.PatientID;
                    String[] aaa = name.Split(' ');
                    if (aaa.Length > 2)
                    {
                        String grp = "";
                        if (xrgrpcd.Equals("C"))
                        {
                            grp = "CT";
                        }
                        else if (xrgrpcd.Equals("U"))
                        {
                            grp = "US";
                        }
                        else if (xrgrpcd.Equals("G"))
                        {
                            grp = "MG";
                        }
                        else if (xrgrpcd.Equals("M"))
                        {
                            grp = "MR";
                        }
                        else
                        {
                            grp = "CR";
                        }
                        txtADT = bc.genADT("xray", hn, aaa[0], aaa[1], aaa[2], dob, sex, "THAI", opdtype, depcode, depname);
                        //txtORM = bc.genORM("xray", hn, aaa[0], aaa[1], aaa[2], dob, sex, "THAI"
                        //    , hnreqyear, reqno, xrcode, xray, grp, "333","Ekapop", grp, opdtype, depcode, depname, sickness);
                        txtORM = bc.genORM("xray", hn, aaa[0], aaa[1], aaa[2], dob, sex, "THAI"
                            , reqdate.Replace("-", ""), reqno, xrcode, xray, grp, stfcode, stfname, grp, opdtype, depcode, depname, sickness);
                        //clientStreamWriter.WriteLine(hn+" "+ aaa[0] + " " + aaa[1] + " " + aaa[2]);
                        //Test process

                        //StreamWriter clientStreamWriter = new StreamWriter(;
                        TcpClient tcpClient = new TcpClient(bc.iniC.pacsServerIP, int.Parse(bc.iniC.pacsServerPort));

                        Console.WriteLine("Connected to Server");
                        lboxServer.Items.Add("Connected to Server " + bc.iniC.pacsServerIP + " " + bc.iniC.pacsServerPort + " " + System.DateTime.Now.ToString());
                        Application.DoEvents();
                        //get a network stream from server
                        NetworkStream clientSockStream = tcpClient.GetStream();
                        StreamReader clientStreamReader = new StreamReader(clientSockStream);
                        StreamWriter clientStreamWriter = new StreamWriter(clientSockStream);

                        //byte[] byteArrayADT = Encoding.UTF8.GetBytes(txtADT);
                        //MemoryStream streamADT = new MemoryStream(byteArrayADT);
                        //streamADT.Position = 0;
                        //using (StreamWriter writetext = new StreamWriter(streamADT, Encoding.UTF8))
                        //{
                        //    writetext.WriteLine(txtADT);
                        //    //writetext.Flush();
                        //    //writetext.Close();
                        //}

                        clientStreamWriter.Write(txtADT);
                        clientStreamWriter.Flush();
                        //clientStreamWriter.Close();
                        Application.DoEvents();

                        resp = clientStreamReader.ReadToEnd();

                        //clientStreamReader.Close();
                        //clientSockStream.Close();

                        //tcpClient.Close();
                        lboxClient.Items.Add("SERVER " + resp + "  " + System.DateTime.Now.ToString());
                        Application.DoEvents();

                        TcpClient tcpClientORM = new TcpClient(bc.iniC.pacsServerIP, int.Parse(bc.iniC.pacsServerPort));
                        Console.WriteLine("Connected to Server");
                        //lboxServer.Items.Add("Connected to Server " + bc.iniC.pacsServerIP + " " + bc.iniC.pacsServerPort + " " + System.DateTime.Now.ToString());
                        Application.DoEvents();
                        //get a network stream from server
                        NetworkStream clientSockStreamORM = tcpClientORM.GetStream();
                        StreamReader clientStreamReaderORM = new StreamReader(clientSockStreamORM);
                        StreamWriter clientStreamWriterORM = new StreamWriter(clientSockStreamORM);

                        //byte[] byteArrayORM = Encoding.UTF8.GetBytes(txtORM);
                        //MemoryStream streamORM = new MemoryStream(byteArrayORM);
                        //streamORM.Position = 0;
                        //using (StreamWriter writetext = new StreamWriter(streamORM, Encoding.UTF8))
                        //{
                        //    writetext.WriteLine(txtORM);
                        //    //writetext.Close();
                        //}
                        //Application.DoEvents();
                        //Thread.Sleep(500);
                        resp = "";
                        //resp = clientStreamReaderORM.ReadLine();
                        clientStreamWriterORM.Write(txtORM);
                        clientStreamWriterORM.Flush();
                        Application.DoEvents();
                        resp = "";
                        resp = clientStreamReaderORM.ReadToEnd();
                        clientStreamWriterORM.Close();
                        clientStreamReaderORM.Close();
                        clientSockStreamORM.Close();
                        tcpClientORM.Close();
                        Console.WriteLine("SERVER: " + resp);
                        lboxClient.Items.Add("SERVER " + resp + "  " + System.DateTime.Now.ToString());
                        Application.DoEvents();
                        if (xrcode.ToUpper().Equals("SMM005"))
                        {
                            try
                            {
                                //grp = "US";
                                //txtADT = bc.genADT("xray", hn, aaa[0], aaa[1], aaa[2], dob, sex, "THAI", opdtype, depcode, depname);
                                ////txtORM = bc.genORM("xray", hn, aaa[0], aaa[1], aaa[2], dob, sex, "THAI"
                                ////    , hnreqyear, reqno, xrcode, xray, grp, "333","Ekapop", grp, opdtype, depcode, depname, sickness);
                                //txtORM = bc.genORM("xray", hn, aaa[0], aaa[1], aaa[2], dob, sex, "THAI"
                                //    , reqdate.Replace("-", ""), reqno+"1", xrcode, xray, grp, stfcode, stfname, grp, opdtype, depcode, depname, sickness);

                                //TcpClient tcpClient1 = new TcpClient(bc.iniC.pacsServerIP, int.Parse(bc.iniC.pacsServerPort));

                                //Console.WriteLine("Connected to Server");
                                //lboxServer.Items.Add("Connected to Server " + bc.iniC.pacsServerIP + " " + bc.iniC.pacsServerPort + " " + System.DateTime.Now.ToString());
                                //Application.DoEvents();
                                ////get a network stream from server
                                //NetworkStream clientSockStream1 = tcpClient1.GetStream();
                                //StreamReader clientStreamReader1 = new StreamReader(clientSockStream1);
                                //StreamWriter clientStreamWriter1 = new StreamWriter(clientSockStream1);

                                //clientStreamWriter1.Write(txtADT);
                                //clientStreamWriter1.Flush();
                                ////clientStreamWriter.Close();
                                //Application.DoEvents();

                                //resp = clientStreamReader1.ReadToEnd();

                                ////clientStreamReader.Close();
                                ////clientSockStream.Close();

                                ////tcpClient.Close();
                                //lboxClient.Items.Add("SERVER " + resp + "  " + System.DateTime.Now.ToString());
                                //Application.DoEvents();

                                //TcpClient tcpClientORM1 = new TcpClient(bc.iniC.pacsServerIP, int.Parse(bc.iniC.pacsServerPort));
                                //Console.WriteLine("Connected to Server");
                                ////lboxServer.Items.Add("Connected to Server " + bc.iniC.pacsServerIP + " " + bc.iniC.pacsServerPort + " " + System.DateTime.Now.ToString());
                                //Application.DoEvents();
                                ////get a network stream from server
                                //NetworkStream clientSockStreamORM1 = tcpClientORM1.GetStream();
                                //StreamReader clientStreamReaderORM1 = new StreamReader(clientSockStreamORM1);
                                //StreamWriter clientStreamWriterORM1 = new StreamWriter(clientSockStreamORM1);

                                //resp = "";
                                ////resp = clientStreamReaderORM.ReadLine();
                                //clientStreamWriterORM1.Write(txtORM);
                                //clientStreamWriterORM1.Flush();
                                //Application.DoEvents();
                                //resp = "";
                                //resp = clientStreamReaderORM1.ReadToEnd();
                                ////clientSockStream1.Close();
                                //clientStreamWriterORM1.Close();
                                //clientStreamReaderORM1.Close();
                                //clientSockStreamORM1.Close();
                                //tcpClientORM1.Close();
                                //Console.WriteLine("SERVER: " + resp);
                                //lboxClient.Items.Add("SERVER " + resp + "  " + System.DateTime.Now.ToString());
                                //Application.DoEvents();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                catch (Exception se)
                {
                    Console.WriteLine(se.StackTrace);
                    lboxClient.Items.Add("Error " + se.StackTrace + "  " + System.DateTime.Now.ToString());
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
            
            //}
        }        
        private void setGrfReq()
        {
            //grfDept.Rows.Count = 7;
            //grfReq.Clear();
            grfReq.Rows.Count = 1;
            DataTable dt = new DataTable();
            String date = "";
            date = System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd");

            dt = bc.bcDB.xrDB.selectVisitStatusPacsReqByDate(date);
            //dt = bc.bcDB.xrDB.selectVisitStatusPacsReqByDateTestHN(date, "5132957");
            //grfExpn.Rows.Count = dt.Rows.Count + 1;

            grfReq.Rows.Count = dt.Rows.Count + 1;
            grfReq.Cols.Count = 25;
            
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
            grfReq.Cols[colxrcode].Width = 80;
            grfReq.Cols[colxrcode].Width = 80;
            grfReq.Cols[colxrdepno].Width = 80;

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
            grfReq.Cols[colxrcode].Caption = "";
            grfReq.Cols[colxrgrpdsc].Caption = "Group";
            grfReq.Cols[colxrdate].Caption = "xr date";
            grfReq.Cols[colReqreqno].Caption = "req no";

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
                    grfReq[i, colReqDtr] = row["MNC_DOT_CD"].ToString();
                    grfReq[i, colReqDpt] = "";
                    grfReq[i, colReqreqyr] = row["mnc_req_yr"].ToString();
                    grfReq[i, colReqreqno] = row["mnc_req_no"].ToString();
                    grfReq[i, colreqhnyr] = row["mnc_hn_yr"].ToString();
                    grfReq[i, colreqpreno] = row["mnc_pre_no"].ToString();
                    grfReq[i, colreqsex] = row["mnc_sex"].ToString();
                    grfReq[i, colreqsickness] = row["mnc_shif_memo"].ToString();

                    grfReq[i, colreqdob] = row["mnc_bday"].ToString();
                    grfReq[i, colxrdesc] = row["MNC_XR_DSC"].ToString();
                    grfReq[i, colxrcode] = row["MNC_XR_CD"].ToString();
                    grfReq[i, colxrstfcode] = row["MNC_DOT_CD"].ToString();
                    grfReq[i, colxrstfname] = row["mnc_usr_full"].ToString();
                    grfReq[i, colxrdepno] = row["mnc_req_dep"].ToString();
                    grfReq[i, colxrdepname] = row["MNC_MD_DEP_DSC"].ToString();
                    grfReq[i, colpttstatus] = row["MNC_STS"].ToString();
                    grfReq[i, colxrgrpcd] = row["MNC_XR_GRP_CD"].ToString();
                    grfReq[i, colxrgrpdsc] = row["MNC_XR_GRP_dsc"].ToString();
                    grfReq[i, colxrdate] = row["mnc_req_dat1"].ToString();
                    grfReq[i, colReqreqyr] = row["mnc_req_yr"].ToString();
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
            //grfReq.Cols[colReqreqno].Visible = false;
            grfReq.Cols[colreqhnyr].Visible = false;
            grfReq.Cols[colreqpreno].Visible = false;
            grfReq.Cols[colReqDpt].Visible = false;
            grfReq.Cols[colReqDtr].Visible = false;
            //grfReq.Cols[colxrgrpcd].Visible = false;

            grfReq.Cols[colReqHn].AllowEditing = false;
            grfReq.Cols[colReqName].AllowEditing = false;
            grfReq.Cols[colReqVn].AllowEditing = false;
            grfReq.Cols[colReqXn].AllowEditing = false;
            grfReq.Cols[colReqDtr].AllowEditing = false;
            grfReq.Cols[colReqDpt].AllowEditing = false;
            grfReq.Cols[colxrcode].AllowEditing = false;
            grfReq.Cols[colxrdepno].AllowEditing = false;
            grfReq.Cols[colxrdepname].AllowEditing = false;
            grfReq.Cols[colpttstatus].AllowEditing = false;
            grfReq.Cols[colxrgrpdsc].AllowEditing = false;
            grfReq.Cols[colxrdate].AllowEditing = false;
        }
        private bool StartServer()
        {
            //create server's tcp listener for incoming connection
            IPAddress ipad = IPAddress.Parse(txtIp.Text);
            tcpServerListener = new TcpListener(ipad, int.Parse(txtPort.Text));
            tcpServerListener.Start();      //start server
            //Console.WriteLine("Server Started");
            //listBox1.Items.Add("Start Listening " + System.DateTime.Now.ToString());
            lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Start Listening " + System.DateTime.Now.ToString());});
            Application.DoEvents();
            //this.btnStartServer.Enabled = false;
            //block tcplistener to accept incoming connection
            serverSocket = tcpServerListener.AcceptSocket();

            try
            {
                if (serverSocket.Connected)
                {
                    //Console.WriteLine("Client connected");
                    //listBox1.Items.Add("Client connected " + System.DateTime.Now.ToString());
                    lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Client connected " + System.DateTime.Now.ToString()); });
                    Application.DoEvents();
                    //open network stream on accepted socket
                    serverSockStream = new NetworkStream(serverSocket);
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
            //if (StartServer())
            //{
            IPAddress ipad = IPAddress.Parse(txtIp.Text);
            tcpServerListener = new TcpListener(ipad, int.Parse(txtPort.Text));
            tcpServerListener.Start();      //start server
            lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Start Listening " + System.DateTime.Now.ToString()); });
            Application.DoEvents();
            while (true)
            {
                try
                {
                    TcpClient tcpClient = tcpServerListener.AcceptTcpClient();
                    lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Client connected " + System.DateTime.Now.ToString()); });
                    Application.DoEvents();
                    //NetworkStream serverSockStream = tcpClient.GetStream();
                    Thread n_server = new Thread(new ParameterizedThreadStart(getMessageClient));
                    //n_server.IsBackground = true;
                    n_server.Start(tcpClient);
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmXrayPACsAdd listenServer error " + ex.Message);
                }
            }
        }
        private void getMessageClient(object tcpclient)
        {
            string line = "";
            TcpClient tcpClient = (TcpClient)tcpclient;
            //serverStreamWriter = new StreamWriter(serverSockStream);
            Application.DoEvents();
            byte[] messsage = new byte[4096];
            int byteread = 0;
            String ackmsg = "";

            using (NetworkStream serverSockStream = tcpClient.GetStream())
            {
                //line = serverStreamReader.ReadToEnd();
                try
                {
                    byteread = serverSockStream.Read(messsage, 0, messsage.Length);
                    ackmsg = " MSA|AA";
                    serverSockStream.Write(System.Text.Encoding.UTF8.GetBytes(ackmsg), 0, System.Text.Encoding.Default.GetByteCount(ackmsg));
                }
                catch (Exception ex)
                {

                }
            }
            line = System.Text.Encoding.UTF8.GetString(messsage, 0, byteread);

            lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Date Time : " + System.DateTime.Now.ToString() + "Receive : " + line); });
            Application.DoEvents();
            String fileName = "", path = "", err = "";
            path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            new LogWriter("e", "FrmXrayPACsAdd path " + path);
            if (!Directory.Exists(path + "message"))
            {
                Directory.CreateDirectory(path + "message");
            }
            lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Date Time : " + System.DateTime.Now.ToString() + "Directory.CreateDirectory : " + path + "\\message\\" + fileName); });
            Application.DoEvents();
            fileName = DateTime.Now.Ticks.ToString() + ".txt";
            new LogWriter("e", "FrmXrayPACsAdd fileName " + path + "message\\" + fileName);
            FileStream stream = new FileStream(path + "message\\" + fileName, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(line);
            }
            stream.Close();
            lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Date Time : " + System.DateTime.Now.ToString() + "write file success "); });
            Application.DoEvents();
            try
            {
                String assionno = "", hn = "", reqno = "", reqdate = "", code = "", hnyr = "", diag = "", dtrcode = "";
                String[] txt = line.Split(Environment.NewLine.ToCharArray());
                foreach(String txt1 in txt)
                {
                    String[] txt2 = txt1.Split('|');
                    if (txt2.Length > 4)
                    {
                        if (txt2[0] == "PID")
                        {
                            hn = txt2[3].Replace("BN5","").Replace("^","");
                        }
                        else if (txt2[0] == "OBR")
                        {
                            assionno = txt2[3];
                            if (assionno.Length > 10)
                            {
                                hnyr = assionno.Substring(0, 4);
                                reqdate = assionno.Substring(0, 8);
                                code = assionno.Substring(assionno.Length - 4);
                                reqno = assionno.Replace(reqdate, "").Replace(code, "");
                                int hnyr1 = 0;
                                int.TryParse(hnyr, out hnyr1);
                                hnyr1 += 543;
                                hnyr = hnyr1.ToString();
                            }
                        }
                        else if (txt2[0] == "OBX")
                        {
                            diag += txt2[5] + Environment.NewLine;
                            if (dtrcode.Length == 0)
                            {
                                dtrcode = txt2[txt2.Length - 1];
                                String[] dtr = dtrcode.Split('^');
                                if (dtr.Length > 1)
                                {
                                    dtrcode = dtr[0];
                                }
                            }
                        }
                    }
                }
                lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Doctor : " + dtrcode + " hn " + hn + " reqdate " + reqdate + " reqno " + reqno + " code " + code ); });
                Application.DoEvents();
                String xrcode = "", chkinsert="", re="";
                err = "00";
                xrcode = bc.bcDB.xrDB.selectXrayByCode1(code);
                err = "01";
                lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Doctor : " + dtrcode + " hn " + hn + " reqdate " + reqdate + " reqno " + reqno + " code " + code + " xrcode " + xrcode); });
                Application.DoEvents();
                chkinsert = bc.bcDB.xrt04DB.selectByPk(hnyr, reqdate, reqno, xrcode);
                err = "02";
                XrayT04 xrt04 = new XrayT04();
                xrt04.MNC_REQ_YR = hnyr;
                xrt04.MNC_REQ_NO = reqno;
                xrt04.MNC_REQ_DAT = reqdate;
                xrt04.MNC_XR_DSC = diag;
                xrt04.MNC_DOT_DF_CD = dtrcode;
                xrt04.MNC_XR_CD = xrcode;
                xrt04.MNC_STS = "N";
                xrt04.MNC_XR_USR = "00000";
                if (chkinsert.Equals("insert"))
                {
                    err = "03";
                    re = bc.bcDB.xrt04DB.insert(xrt04);
                    long chk11 = 0;
                    if(long.TryParse(re, out chk11))
                    {
                        lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Insert success "); });
                    }
                    else
                    {
                        lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("no Insert error "+re); });
                        new LogWriter("e", "getMessageClient Insert  err " + re);
                    }
                    Application.DoEvents();
                }
                else
                {
                    err = "04";
                    re = bc.bcDB.xrt04DB.update(xrt04);
                    lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Update success "); });
                    Application.DoEvents();
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "getMessageClient 00 ex " +ex.Message+" err "+err);
                lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Error " + ex.Message); });
                Application.DoEvents();
            }
        }
        private void BtnLisStart_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(btnLisStart.Image.GetHashCode() == imgStart.GetHashCode())
            {
                btnLisStart.Image = imgStop;
                btnLisStart.Text = "Stop";
                lboxServer.Items.Clear();
                //listenServer();
                n_server = new Thread(new ThreadStart(listenServer));
                n_server.IsBackground = true;
                n_server.Start();
                //ServerListen();
                //listBox1.Items.Add("Start Listening "+ System.DateTime.Now.ToString());
            }
            else
            {
                btnLisStart.Image = imgStart;
                btnLisStart.Text = "Start";
                lboxServer.Items.Add("Stop Listening " + System.DateTime.Now.ToString());
                serverSocket.Close();
                serverSockStream.Close();
                tcpServerListener.Stop();
                //tcpServerListener.cl
            }
        }
        private void initCompoment()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            imgStart = Resources.start128;
            imgStop = Resources.stop_red128;
            imgSave = Resources.save;

            theme1 = new C1ThemeController();
            sb1 = new C1StatusBar();
            panel1 = new Panel();
            tC1 = new C1DockingTab();
            tabReq = new C1DockingTabPage();
            tabProc = new C1DockingTabPage();
            tabFinish = new C1DockingTabPage();
            tabListen = new C1DockingTabPage();
            tabmaster = new C1DockingTabPage();
            splitContainer1 = new C1SplitContainer();
            c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            sCMaster = new C1SplitContainer();
            scpMasterTop = new C1.Win.C1SplitContainer.C1SplitterPanel();
            scpMasterButton = new C1.Win.C1SplitContainer.C1SplitterPanel();

            pnListen = new Panel();
            pnMaster = new Panel();
            lboxServer = new System.Windows.Forms.ListBox();
            pnQue = new Panel();
            pnProc = new Panel();
            lboxClient = new System.Windows.Forms.ListBox();

            panel1.SuspendLayout();
            tC1.SuspendLayout();
            tabReq.SuspendLayout();
            tabProc.SuspendLayout();
            tabFinish.SuspendLayout();
            tabListen.SuspendLayout();
            tabmaster.SuspendLayout();
            splitContainer1.SuspendLayout();
            c1SplitterPanel1.SuspendLayout();
            c1SplitterPanel2.SuspendLayout();
            sCMaster.SuspendLayout();
            scpMasterTop.SuspendLayout();
            scpMasterButton.SuspendLayout();
            pnListen.SuspendLayout();
            pnQue.SuspendLayout();
            pnMaster.SuspendLayout();
            pnProc.SuspendLayout();

            this.SuspendLayout();

            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Name = "panel1";

            tabReq.Name = "tabReq";
            tabReq.TabIndex = 0;
            tabReq.Text = "Request";
            tabReq.Font = fEditB;

            tabProc.Name = "tabApm";
            tabProc.TabIndex = 1;
            tabProc.Text = "Process";
            tabProc.Font = fEditB;

            tabFinish.Name = "tabFinish";
            tabFinish.TabIndex = 2;
            tabFinish.Text = "Finish";
            tabFinish.Font = fEditB;

            tabListen.Name = "tabListen";
            tabListen.TabIndex = 3;
            tabListen.Text = "Listen PACs Infinitt";
            tabListen.Font = fEditB;

            tabmaster.Name = "tabmaster";
            tabmaster.TabIndex = 4;
            tabmaster.Text = "Master X-ray";
            tabmaster.Font = fEditB;

            pnListen.Dock = System.Windows.Forms.DockStyle.Fill;
            pnListen.Name = "pnListen";
            pnQue.Dock = System.Windows.Forms.DockStyle.Fill;
            pnQue.Name = "pnQue";
            pnMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            pnMaster.Name = "pnMaster";
            pnProc.Dock = System.Windows.Forms.DockStyle.Fill;
            pnProc.Name = "pnProc";

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

            sCMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            sCMaster.Location = new System.Drawing.Point(0, 0);
            sCMaster.Name = "sCMaster";
            sCMaster.Size = new System.Drawing.Size(800, 450);
            sCMaster.TabIndex = 0;

            c1SplitterPanel1.Collapsible = true;
            c1SplitterPanel1.Height = 86;
            c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            c1SplitterPanel1.Name = "c1SplitterPanel1";
            c1SplitterPanel1.Size = new System.Drawing.Size(298, 58);
            c1SplitterPanel1.TabIndex = 0;
            c1SplitterPanel1.Text = "Master List";
            c1SplitterPanel1.Dock = PanelDockStyle.Top;
            c1SplitterPanel2.Height = 85;
            c1SplitterPanel2.Location = new System.Drawing.Point(0, 111);
            c1SplitterPanel2.Name = "c1SplitterPanel2";
            c1SplitterPanel2.Size = new System.Drawing.Size(298, 64);
            c1SplitterPanel2.TabIndex = 1;
            c1SplitterPanel2.Text = "Master Add/Edit";

            scpMasterTop.Collapsible = true;
            scpMasterTop.Height = 86;
            scpMasterTop.Location = new System.Drawing.Point(0, 21);
            scpMasterTop.Name = "scpMasterTop";
            scpMasterTop.Size = new System.Drawing.Size(298, 58);
            scpMasterTop.TabIndex = 0;
            scpMasterTop.Text = "Panel 1";
            scpMasterTop.Dock = PanelDockStyle.Top;
            scpMasterButton.Height = 85;
            scpMasterButton.Location = new System.Drawing.Point(0, 111);
            scpMasterButton.Name = "scpMasterButton";
            scpMasterButton.Size = new System.Drawing.Size(298, 64);
            scpMasterButton.TabIndex = 1;
            scpMasterButton.Text = "Panel 2";
            //MessageBox.Show("aaaaaaa", "");
            setControlComponent();

            this.Controls.Add(panel1);
            this.Controls.Add(this.sb1);
            panel1.Controls.Add(tC1);
            tC1.Controls.Add(tabReq);
            tC1.Controls.Add(tabProc);
            tC1.Controls.Add(tabFinish);
            tC1.Controls.Add(tabListen);
            tC1.Controls.Add(tabmaster);
            tabReq.Controls.Add(splitContainer1);
            splitContainer1.Panels.Add(c1SplitterPanel1);
            splitContainer1.Panels.Add(c1SplitterPanel2);
            tabListen.Controls.Add(pnListen);
            tabmaster.Controls.Add(pnMaster);
            pnMaster.Controls.Add(sCMaster);
            c1SplitterPanel1.Controls.Add(pnQue);
            tabProc.Controls.Add(pnProc);
            //tabmaster.Controls.Add(sCMaster);
            sCMaster.Panels.Add(scpMasterTop);
            sCMaster.Panels.Add(scpMasterButton);

            pnListen.Controls.Add(btnLisStart);
            pnListen.Controls.Add(lbTxtIp);
            pnListen.Controls.Add(txtIp);
            pnListen.Controls.Add(lbTxtPort);
            pnListen.Controls.Add(txtPort);
            pnListen.Controls.Add(lboxServer);
            pnListen.Controls.Add(btnBrowTxtInfinitt);
            c1SplitterPanel2.Controls.Add(lboxClient);

            scpMasterButton.Controls.Add(lbXrCode);
            scpMasterButton.Controls.Add(lbXrName);
            scpMasterButton.Controls.Add(lbPacsCode);
            scpMasterButton.Controls.Add(lbModality);
            scpMasterButton.Controls.Add(txtXrCode);
            scpMasterButton.Controls.Add(txtXrName);
            scpMasterButton.Controls.Add(txtPacsCode);
            scpMasterButton.Controls.Add(cboModality);
            scpMasterButton.Controls.Add(btnModality);
            scpMasterButton.Controls.Add(txtModality);

            panel1.ResumeLayout(false);
            tC1.ResumeLayout(false);
            tabReq.ResumeLayout(false);
            tabProc.ResumeLayout(false);
            tabFinish.ResumeLayout(false);
            tabListen.ResumeLayout(false);
            tabmaster.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            c1SplitterPanel1.ResumeLayout(false);
            c1SplitterPanel2.ResumeLayout(false);
            scpMasterButton.ResumeLayout(false);
            scpMasterTop.ResumeLayout(false);
            sCMaster.ResumeLayout(false);
            pnListen.ResumeLayout(false);
            pnMaster.ResumeLayout(false);
            pnQue.ResumeLayout(false);
            pnProc.ResumeLayout(false);

            this.ResumeLayout(false);

            panel1.PerformLayout();
            pnListen.PerformLayout();
            pnMaster.PerformLayout();
            pnQue.PerformLayout();
            pnProc.PerformLayout();
            tabReq.PerformLayout();
            tabProc.PerformLayout();
            tabFinish.PerformLayout();
            tabListen.PerformLayout();
            tabmaster.PerformLayout();
            splitContainer1.PerformLayout();
            c1SplitterPanel1.PerformLayout();
            c1SplitterPanel2.PerformLayout();
            tC1.PerformLayout();
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
            //btnBrowTxtInfinitt

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
            //MessageBox.Show("mmmmmm", "");
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

            lboxServer.Dock = System.Windows.Forms.DockStyle.None;
            lboxServer.FormattingEnabled = true;
            lboxServer.Location = new System.Drawing.Point(btnLisStart.Location.X, btnLisStart.Height + gapLine+10);
            lboxServer.Name = "listBox1";
            lboxServer.Size = new System.Drawing.Size(900, 450);
            lboxServer.TabIndex = 0;

            lboxClient.Dock = System.Windows.Forms.DockStyle.Fill;
            lboxClient.FormattingEnabled = true;
            lboxClient.Location = new System.Drawing.Point(btnLisStart.Location.X, btnLisStart.Height + gapLine + 10);
            lboxClient.Name = "listBox2";
            lboxClient.Size = new System.Drawing.Size(600, 450);
            lboxClient.TabIndex = 0;

            lbXrCode = new Label();
            lbXrCode.Text = "Xray Code : ";
            lbXrCode.Font = fEditBig;
            lbXrCode.Location = new System.Drawing.Point(gapX, gapLine);
            lbXrCode.AutoSize = true;
            lbXrCode.Name = "lbXrCode";
            txtXrCode = new C1TextBox();
            txtXrCode.Font = fEdit;
            size = bc.MeasureString(lbXrCode);
            txtXrCode.Location = new System.Drawing.Point(lbXrCode.Location.X + size.Width+10, lbXrCode.Location.Y);
            txtXrCode.Size = new Size(120, 20);

            gapLine += 20;
            lbXrName = new Label();
            lbXrName.Text = "Xray Name : ";
            lbXrName.Font = fEditBig;
            lbXrName.Location = new System.Drawing.Point(gapX, gapLine);
            lbXrName.AutoSize = true;
            lbXrName.Name = "lbXrName";
            txtXrName = new C1TextBox();
            txtXrName.Font = fEdit;
            size = bc.MeasureString(lbXrName);
            txtXrName.Location = new System.Drawing.Point(lbXrName.Location.X + size.Width + 10, lbXrName.Location.Y);
            txtXrName.Size = new Size(120, 20);

            gapLine += 20;
            lbPacsCode = new Label();
            lbPacsCode.Text = "Pacs Code : ";
            lbPacsCode.Font = fEditBig;
            lbPacsCode.Location = new System.Drawing.Point(gapX, gapLine);
            lbPacsCode.AutoSize = true;
            lbPacsCode.Name = "lbPacsCode";
            txtPacsCode = new C1TextBox();
            txtPacsCode.Font = fEdit;
            size = bc.MeasureString(lbPacsCode);
            txtPacsCode.Location = new System.Drawing.Point(lbPacsCode.Location.X + lbPacsCode.Width + 20, lbPacsCode.Location.Y);
            txtPacsCode.Size = new Size(120, 20);

            gapLine += 20;
            lbModality = new Label();
            lbModality.Text = "Modality : ";
            lbModality.Font = fEditBig;
            lbModality.Location = new System.Drawing.Point(gapX, gapLine);
            lbModality.AutoSize = true;
            lbModality.Name = "lbModality";
            cboModality = new C1ComboBox();
            cboModality.Font = fEdit;
            size = bc.MeasureString(lbModality);
            cboModality.Location = new System.Drawing.Point(lbModality.Location.X + lbModality.Width + 20, lbModality.Location.Y);
            //txtPacsCode.Size = new Size(120, 20);
            txtModality = new C1TextBox();
            txtModality.Font = fEdit;
            size = bc.MeasureString(lbPacsCode);
            txtModality.Location = new System.Drawing.Point(cboModality.Location.X + cboModality.Width + 20, cboModality.Location.Y);
            txtModality.Size = new Size(120, 20);

            gapLine += 40;
            btnModality = new C1Button();
            btnModality.Name = "btnModality";
            btnModality.Text = "Save";
            btnModality.Font = fEdit;
            //size = bc.MeasureString(btnHnSearch);
            btnModality.Location = new System.Drawing.Point(gapX, gapLine);
            btnModality.Size = new Size(200, 140);
            btnModality.Font = fEdit;
            btnModality.Image = Resources.Save_small;
            btnModality.TextAlign = ContentAlignment.MiddleRight;
            btnModality.ImageAlign = ContentAlignment.MiddleLeft;

            btnBrowTxtInfinitt = new C1Button();
            btnBrowTxtInfinitt.Name = "btnBrowTxtInfinitt";
            btnBrowTxtInfinitt.Text = "Browe Txt";
            btnBrowTxtInfinitt.Font = fEdit;
            //size = bc.MeasureString(btnHnSearch);
            btnBrowTxtInfinitt.Location = new System.Drawing.Point(txtPort.Location.X + txtPort.Width +20, txtPort.Location.Y);
            btnBrowTxtInfinitt.Size = new Size(80, 80);
            btnBrowTxtInfinitt.Font = fEdit;
            //btnBrowTxtInfinitt.Image = imgStart;

            bc.setCboMOdality(cboModality, "");
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
            String year = "", mm = "", dd = "";
            year = DateTime.Now.Year.ToString();
            mm = DateTime.Now.ToString("MM");
            dd = DateTime.Now.ToString("dd");
            this.Text = "Lasst Update 2022-11-23 pacsServerIP " + bc.iniC.pacsServerIP + " pacsServerPort " + bc.iniC.pacsServerPort+ "bc.timerCheckLabOut " + bc.timerCheckLabOut + " status online " + bc.iniC.statusLabOutReceiveOnline+" Format date "+ year + " "+mm + " "+dd;
            frmFlash.Dispose();
            this.WindowState = FormWindowState.Maximized;
            c1SplitterPanel1.SizeRatio = 80;
            if (bc.iniC.statusLabOutReceiveOnline.Equals("1"))
            {
                tabListen.Hide();
            }
        }
    }
}
