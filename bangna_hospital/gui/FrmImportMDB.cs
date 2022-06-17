using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1List;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmImportMDB : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, famtB14, fque, fqueB, famtB30;
        int grdViewFontSize = 0, printBillTextFoodsSize = 0;
        Label lbLoading;

        C1FlexGrid grfAdmit;
        C1ThemeController theme;
        DataTable dtaAdmin = new DataTable();
        int colHn = 1, colPID=2, colDOB = 3, colSex = 4, colAdmitDate = 5, colTotal = 6, colNetTotal = 7, colpreno=8, colTime=9;
        int colChkHn = 1, colChkTotal = 2, colChkSo1 = 3, colChkSo2 = 4, colChkSo3 = 5, colChkSo4 = 6, colChkSo5 = 7, colChkSo6 = 8, colChkSo7 = 9, colChkSo8 = 10, colChkSo9 = 11, colChkSo10 = 12, colChkSo11 = 13, colChkSo12 = 14, colChkSo13 = 15, colChkSo14 = 16, colChkSo15 = 17, colChkSo16 = 18, colChkSo17 = 19, colChkSo18 = 20, colChkErr1=21, colChkErr2 = 22;
        String reAccess = "", reCntOk = "", reCntNobill="";
        int rowCntNobill = 0, rowCntOk = 0, rowCntErr=0;
        Boolean statusStickerIPD = false, pageLoad = false, chkStkDfPrint = false;
        public FrmImportMDB(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);
            fEdit = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 3, FontStyle.Bold);
            famt = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famtB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            txtMDBdatestart.DropDownClosed += TxtMDBdatestart_DropDownClosed;
            txtMDBdateend.DropDownClosed += TxtMDBdateend_DropDownClosed;

            theme = new C1ThemeController();
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(650, 80);
            this.Controls.Add(lbLoading);

            btnMDBbrow.Click += BtnMDBbrow_Click;
            btnMDBimport.Click += BtnMDBimport_Click;
            btnMDBOk.Click += BtnMDBOk_Click;
            btnChk.Click += BtnChk_Click;

            txtPaidType.Value = bc.iniC.importMDBpaidcode;
            txtMDBdatestart.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtMDBdateend.Value = DateTime.Now.ToString("yyyy-MM-dd");

            initGrfAdmit();
            btnMDBimport.Enabled = false;

            pageLoad = false;
        }

        private void BtnChk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfChk();
            setFromCheck();
        }

        private void TxtMDBdateend_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            dtaAdmin.Clear();
            btnMDBimport.Enabled = false;
        }

        private void TxtMDBdatestart_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            dtaAdmin.Clear();
            btnMDBimport.Enabled = false;
        }

        private void BtnMDBOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            setGrfSrc();
            btnMDBimport.Enabled = true;
            hideLbLoading();
        }
        private void importMBDNew()
        {
            //lbLoading.Text = "1. กำลัง insert sqlserver";
            lsbStatus.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lsbMessage2.Text = "";
            Application.DoEvents();
            OutPatientDB outpDB = new OutPatientDB(bc.conn);
            if (File.Exists(txtMDBpathfile.Text.Trim()))
            {
                if (dtaAdmin.Rows.Count > 0)
                {
                    OleDbConnection connMDB = new System.Data.OleDb.OleDbConnection();
                    connMDB.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + txtMDBpathfile.Text.Trim();
                    //SqlConnection conn = new SqlConnection();
                    //conn.ConnectionString = "Server=" + bc.iniC.hostDBMainHIS + ";Database=" + bc.iniC.nameDBMainHIS + ";Uid=" + bc.iniC.userDBMainHIS + ";Pwd=" + bc.iniC.passDBMainHIS + ";";
                    try
                    {
                        //outpDB.deleteAll("");

                        long chk = 0;
                        DateTime dtdatestart = new DateTime();
                        DateTime dtdateend = new DateTime();

                        DateTime.TryParse(txtMDBdateend.Text, out dtdateend);
                        DateTime.TryParse(txtMDBdatestart.Text, out dtdatestart);
                        lsbMessage.Text = dtdatestart.ToString("yyyy-MM-dd");
                        lsbMessage1.Text = dtdateend.ToString("yyyy-MM-dd");
                        //conn.Open();
                        OleDbCommand cmdMDB = new OleDbCommand();
                        //SqlCommand cmd = new SqlCommand();
                        //foreach (DataRow drow in dtaAdmin.Rows)
                        //{
                        //    OutPatient outp = new OutPatient();
                        //    String vstime = "";
                        //    vstime = drow["mnc_time"] != null ? "0000" + drow["mnc_time"].ToString() : "";
                        //    vstime = vstime.Substring(vstime.Length - 4);
                        //    vstime = vstime.Substring(0, 2) + ":" + vstime.Substring(vstime.Length - 2);

                        //    outp.MainHospitalCode = "2210028";
                        //    outp.ServiceHospitalCode = "2210028";
                        //    outp.IDCardNumber = drow["mnc_id_no"] != null ? drow["mnc_id_no"].ToString() : "";
                        //    outp.HN = drow["MNC_HN_NO"] != null ? drow["MNC_HN_NO"].ToString() : "";
                        //    outp.Birthdate = drow["mnc_bday"] != null ? drow["mnc_bday"].ToString() : "";
                        //    outp.Sex = drow["mnc_sex"] != null ? drow["mnc_sex"].ToString() : "";
                        //    outp.AdmitDate = drow["mnc_date"] != null ? drow["mnc_date"].ToString() : "";
                        //    outp.TransferToHospitalCode = " ";
                        //    outp.TotalSocialPaid = drow["MNC_SUM_PRI"] != null ? drow["MNC_SUM_PRI"].ToString() : "0";
                        //    outp.TotalPatientPaid = "0";
                        //    outp.TotalOthersPaid = "0";
                        //    outp.GrandTotal = drow["MNC_SUM_PRI"] != null ? drow["MNC_SUM_PRI"].ToString() : "0";
                        //    outp.preno = drow["MNC_pre_no"] != null ? drow["MNC_pre_no"].ToString().Trim() : "";
                        //    outp.visit_date = drow["mnc_date"] != null ? drow["mnc_date"].ToString().Trim() : "";
                        //    outp.visit_time = vstime;

                        //    cmd.Connection = conn;
                        //    String re = outpDB.insertNoClose1(cmd, outp, "");
                        //    if (!long.TryParse(re, out chk))
                        //    {

                        //    }
                        //}
                        lbLoading.Text = "2. กำลัง updateMDB";
                        Application.DoEvents();
                        String re1 = outpDB.updateMDBNew(dtdatestart.ToString("yyyy-MM-dd"), dtdateend.ToString("yyyy-MM-dd"), txtPaidType.Text.Trim(), chkDeleteAll.Checked ? "1":"");
                        lbLoading.Text = "3. กำลัง delete temp";
                        Application.DoEvents();
                        outpDB.deleteAll(txtMDBpathfile.Text.Trim(), "");
                        connMDB.Open();
                        cmdMDB.Connection = connMDB;
                        lbLoading.Text = "4. กำลัง insertMDBNoClose";
                        Application.DoEvents();
                        reAccess = outpDB.insertMDBNoClose(cmdMDB);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed due to" + ex.Message);
                    }
                    finally
                    {
                        //conn.Close();
                        connMDB.Close();
                    }
                }
            }
            lsbMessage2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void importMBD()
        {
            lsbStatus.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lsbMessage2.Text = "";
            lbLoading.Text = "1. กำลัง insert sqlserver";
            Application.DoEvents();
            OutPatientDB outpDB = new OutPatientDB(bc.conn);
            if (File.Exists(txtMDBpathfile.Text.Trim()))
            {
                if (dtaAdmin.Rows.Count > 0)
                {
                    OleDbConnection connMDB = new System.Data.OleDb.OleDbConnection();
                    connMDB.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + txtMDBpathfile.Text.Trim();
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = "Server=" + bc.iniC.hostDBMainHIS + ";Database=" + bc.iniC.nameDBMainHIS + ";Uid=" + bc.iniC.userDBMainHIS + ";Pwd=" + bc.iniC.passDBMainHIS + "; Connection Timeout=300;";
                    try
                    {
                        if (chkDeleteAll.Checked)
                        {
                            outpDB.deleteAll("");
                        }
                        long chk = 0;
                        conn.Open();
                        OleDbCommand cmdMDB = new OleDbCommand();
                        SqlCommand cmd = new SqlCommand();
                        foreach (DataRow drow in dtaAdmin.Rows)
                        {
                            OutPatient outp = new OutPatient();
                            String vstime = "";
                            vstime = drow["mnc_time"] != null ? "0000" + drow["mnc_time"].ToString() : "";
                            vstime = vstime.Substring(vstime.Length - 4);
                            vstime = vstime.Substring(0, 2) + ":" + vstime.Substring(vstime.Length - 2);

                            outp.MainHospitalCode = "2210028";
                            outp.ServiceHospitalCode = "2210028";
                            outp.IDCardNumber = drow["mnc_id_no"] != null ? drow["mnc_id_no"].ToString() : "";
                            outp.HN = drow["MNC_HN_NO"] != null ? drow["MNC_HN_NO"].ToString() : "";
                            outp.Birthdate = drow["mnc_bday"] != null ? drow["mnc_bday"].ToString() : "";
                            outp.Sex = drow["mnc_sex"] != null ? drow["mnc_sex"].ToString() : "";
                            outp.AdmitDate = drow["mnc_date"] != null ? drow["mnc_date"].ToString() : "";
                            outp.TransferToHospitalCode = " ";
                            outp.TotalSocialPaid = drow["MNC_SUM_PRI"] != null ? drow["MNC_SUM_PRI"].ToString() : "0";
                            outp.TotalPatientPaid = "0";
                            outp.TotalOthersPaid = "0";
                            outp.GrandTotal = drow["MNC_SUM_PRI"] != null ? drow["MNC_SUM_PRI"].ToString() : "0";
                            outp.preno = drow["MNC_pre_no"] != null ? drow["MNC_pre_no"].ToString().Trim() : "";
                            outp.visit_date = drow["mnc_date"] != null ? drow["mnc_date"].ToString().Trim() : "";
                            outp.visit_time = vstime;

                            cmd.Connection = conn;
                            String re = outpDB.insertNoClose1(cmd, outp, "");
                            if (!long.TryParse(re, out chk))
                            {

                            }
                        }
                        lbLoading.Text = "1.1. thread sleep 5000";
                        Application.DoEvents();
                        //Thread.Sleep(5000);
                        lbLoading.Text = "2. กำลัง updateMDB";
                        Application.DoEvents();
                        String re1 = outpDB.updateMDB();
                        lbLoading.Text = "3. กำลัง delete temp";
                        Application.DoEvents();
                        outpDB.deleteAll(txtMDBpathfile.Text.Trim(), "");
                        connMDB.Open();
                        cmdMDB.Connection = connMDB;
                        lbLoading.Text = "4. กำลัง insertMDBNoClose";
                        Application.DoEvents();
                        outpDB.insertMDBNoClose(cmdMDB);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed due to" + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        connMDB.Close();
                    }
                }

            }
            lsbMessage2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void BtnMDBimport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (File.Exists(txtMDBpathfile.Text.Trim()))
            {
                showLbLoading();
                //importMBD();
                importMBDNew();
                hideLbLoading();
            }
            else
            {
                MessageBox.Show("ไม่พบ File Access", "");
            }
        }

        private void BtnMDBbrow_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = bc.iniC.pathImageScan+"\\",
                Title = "Browse mdb Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "mdb",
                Filter = "mdb files (*.mdb)|*.mdb",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtMDBpathfile.Value = openFileDialog1.FileName;
            }
        }
        private void initGrfAdmit()
        {
            grfAdmit = new C1FlexGrid();
            grfAdmit.Font = fEdit;
            grfAdmit.Dock = System.Windows.Forms.DockStyle.Fill;
            grfAdmit.Location = new System.Drawing.Point(0, 0);
            grfAdmit.Rows.Count = 1;
            grfAdmit.Cols.Count = 10;

            grfAdmit.Cols[colHn].Width = 100;
            grfAdmit.Cols[colDOB].Width = 300;
            grfAdmit.Cols[colSex].Width = 150;
            grfAdmit.Cols[colAdmitDate].Width = 150;
            grfAdmit.Cols[colTotal].Width = 150;
            grfAdmit.Cols[colNetTotal].Width = 150;
            grfAdmit.ShowCursor = true;
            grfAdmit.Cols[colHn].Caption = "HN";
            grfAdmit.Cols[colPID].Caption = "PID";
            grfAdmit.Cols[colDOB].Caption = "DOB";
            grfAdmit.Cols[colSex].Caption = "SEX";
            grfAdmit.Cols[colAdmitDate].Caption = "date";
            grfAdmit.Cols[colTotal].Caption = "total";
            grfAdmit.Cols[colNetTotal].Caption = "nettotal";

            grfAdmit.Cols[colHn].Visible = true;
            grfAdmit.Cols[colDOB].Visible = true;
            grfAdmit.Cols[colSex].Visible = true;
            grfAdmit.Cols[colAdmitDate].Visible = true;
            grfAdmit.Cols[colTotal].Visible = true;
            grfAdmit.Cols[colNetTotal].Visible = true;

            grfAdmit.Cols[colHn].AllowEditing = false;
            grfAdmit.Cols[colDOB].AllowEditing = false;
            grfAdmit.Cols[colSex].AllowEditing = false;
            grfAdmit.Cols[colAdmitDate].AllowEditing = false;
            grfAdmit.Cols[colTotal].AllowEditing = false;
            grfAdmit.Cols[colNetTotal].AllowEditing = false;
            grfAdmit.Cols[colPID].AllowEditing = false;
            grfAdmit.Cols[colTime].AllowEditing = false;
            //grfPttComp.AllowFiltering = true;            
            //grfSrc.AutoSizeCols();
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            c1SplitterPanel2.Controls.Add(grfAdmit);
            theme.SetTheme(grfAdmit, bc.iniC.themeApp);
        }
        private void setGrfSrc()
        {
            //showLbLoading();
            rsbPb.Visible = true;
            DateTime dtdatestart = new DateTime();
            DateTime dtdateend = new DateTime();

            DateTime.TryParse(txtMDBdateend.Text, out dtdateend);
            DateTime.TryParse(txtMDBdatestart.Text, out dtdatestart);
            lsbMessage.Text = dtdatestart.ToString("yyyy-MM-dd");
            lsbMessage1.Text = dtdateend.ToString("yyyy-MM-dd");
            dtaAdmin = new DataTable();
            dtaAdmin = bc.bcDB.vsDB.selectImportMDB(dtdatestart.ToString("yyyy-MM-dd"), dtdateend.ToString("yyyy-MM-dd"), txtPaidType.Text.Trim());
            int i = 1, j = 1, row = grfAdmit.Rows.Count;
            grfAdmit.Cols[colHn].Caption = "HN";
            grfAdmit.Cols[colPID].Caption = "PID";
            grfAdmit.Cols[colDOB].Caption = "DOB";
            grfAdmit.Cols[colSex].Caption = "SEX";
            grfAdmit.Cols[colAdmitDate].Caption = "date";
            grfAdmit.Cols[colTotal].Caption = "total";
            grfAdmit.Cols[colNetTotal].Caption = "nettotal";

            grfAdmit.Cols.Count = 10;
            grfAdmit.Rows.Count = 1;
            grfAdmit.Rows.Count = dtaAdmin.Rows.Count + 1;
            rsbPb.Maximum = dtaAdmin.Rows.Count;
            rsbPb.Minimum = 0;
            rsbPb.Value=0;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dtaAdmin.Rows)
            {
                //pB1.Value++;
                Row rowa = grfAdmit.Rows[i];
                if (row1["MNC_HN_NO"].ToString().Equals("5004225"))
                {
                    String aaa = "";
                }
                rowa[colHn] = row1["MNC_HN_NO"].ToString();
                rowa[colDOB] = row1["mnc_bday"].ToString();
                rowa[colSex] = row1["mnc_sex"].ToString();
                rowa[colAdmitDate] = row1["mnc_date"].ToString();
                rowa[colTotal] = row1["MNC_SUM_PRI"].ToString();
                rowa[colNetTotal] = row1["MNC_SUM_PRI"].ToString();
                rowa[colPID] = row1["MNC_id_no"].ToString();
                rowa[colpreno] = row1["MNC_pre_no"].ToString();
                rowa[colTime] = row1["mnc_time"].ToString();
                rowa[0] = i.ToString();
                i++;
                rsbPb.Value++;
                //Application.DoEvents();
            }
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            //menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            //grfVs.ContextMenu = menuGw;
            //hideLbLoading();
            rsbPb.Visible = false;
        }
        private void setGrfChk()
        {
            //showLbLoading();
            rsbPb.Visible = true;
            //clear header
            grfAdmit.Cols.Count = 23;
            grfAdmit.Cols[colHn].Caption = "HN";
            grfAdmit.Cols[colPID].Caption = "";
            grfAdmit.Cols[colDOB].Caption = "";
            grfAdmit.Cols[colSex].Caption = "";
            grfAdmit.Cols[colAdmitDate].Caption = "";
            grfAdmit.Cols[colTotal].Caption = "";
            grfAdmit.Cols[colNetTotal].Caption = "";

            grfAdmit.Cols[colChkHn].Width = 90;
            grfAdmit.Cols[colChkTotal].Width = 80;
            grfAdmit.Cols[colChkSo1].Width = 80;
            grfAdmit.Cols[colChkSo2].Width = 80;
            grfAdmit.Cols[colChkSo3].Width = 80;
            grfAdmit.Cols[colChkSo4].Width = 80;
            grfAdmit.Cols[colChkSo5].Width = 80;
            grfAdmit.Cols[colChkSo6].Width = 80;
            grfAdmit.Cols[colChkSo7].Width = 80;
            grfAdmit.Cols[colChkSo8].Width = 80;
            grfAdmit.Cols[colChkSo9].Width = 80;
            grfAdmit.Cols[colChkSo10].Width = 80;
            grfAdmit.Cols[colChkSo11].Width = 80;
            grfAdmit.Cols[colChkSo12].Width = 80;
            grfAdmit.Cols[colChkSo13].Width = 80;
            grfAdmit.Cols[colChkSo14].Width = 80;
            grfAdmit.Cols[colChkSo15].Width = 80;
            grfAdmit.Cols[colChkSo16].Width = 80;
            grfAdmit.Cols[colChkSo17].Width = 80;
            grfAdmit.Cols[colChkSo18].Width = 80;

            OleDbConnection connMDB = new System.Data.OleDb.OleDbConnection();
            OleDbCommand cmdMDB = new OleDbCommand();
            connMDB.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data source=" + txtMDBpathfile.Text.Trim();
            connMDB.Open();
            cmdMDB.Connection = connMDB;
            OutPatientDB outpDB = new OutPatientDB(bc.conn);
            try
            {
                rowCntOk = 0;
                rowCntNobill = 0;
                rowCntErr = 0;
                CellNote cellnote = new CellNote("No bill");
                CellStyle cs = grfAdmit.Styles.Normal;
                //cs.ForeColor = Color.SlateGray;
                cs.BackColor = Color.FromArgb(231, 76, 60);
                cs.Border.Direction = BorderDirEnum.Horizontal;
                //cs.Border.Color = Color.FromArgb(196, 228, 223);
                cs.Font = new Font(grfAdmit.Styles.Normal.Font, FontStyle.Bold);

                DataTable dtaAdmin1 = new DataTable();
                dtaAdmin1 = outpDB.selectAll(cmdMDB);
                int i = 1, j = 1, row = grfAdmit.Rows.Count;
                
                grfAdmit.Rows.Count = 1;
                grfAdmit.Rows.Count = dtaAdmin1.Rows.Count + 1;

                //pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row1 in dtaAdmin1.Rows)
                {
                    //pB1.Value++;
                    Row rowa = grfAdmit.Rows[i];
                    if (row1["hn"].ToString().Equals("5004225"))
                    {
                        String aaa = "";
                    }
                    rowa[colChkHn] = row1["hn"].ToString();
                    rowa[colChkTotal] = row1["TotalSocialPaid"].ToString();
                    rowa[colChkSo1] = row1["SocialPaid1"].ToString();
                    rowa[colChkSo2] = row1["SocialPaid2"].ToString();
                    rowa[colChkSo3] = row1["SocialPaid3"].ToString();
                    rowa[colChkSo4] = row1["SocialPaid4"].ToString();
                    rowa[colChkSo5] = row1["SocialPaid5"].ToString();
                    rowa[colChkSo6] = row1["SocialPaid6"] != null ? row1["SocialPaid6"].ToString():"";
                    rowa[colChkSo7] = row1["SocialPaid7"].ToString();
                    rowa[colChkSo8] = row1["SocialPaid8"].ToString();
                    rowa[colChkSo9] = row1["SocialPaid9"].ToString();
                    rowa[colChkSo10] = row1["SocialPaid10"].ToString();
                    rowa[colChkSo11] = row1["SocialPaid11"].ToString();
                    rowa[colChkSo12] = row1["SocialPaid12"].ToString();
                    rowa[colChkSo13] = row1["SocialPaid13"].ToString();
                    rowa[colChkSo14] = row1["SocialPaid14"].ToString();
                    rowa[colChkSo15] = row1["SocialPaid15"].ToString();
                    rowa[colChkSo16] = row1["SocialPaid16"].ToString();
                    rowa[colChkSo17] = row1["SocialPaid17"].ToString();
                    rowa[colChkSo18] = row1["SocialPaid18"].ToString();
                    rowa[0] = i.ToString();
                    if (row1["aa"].ToString().Equals("No bill"))
                    {
                        
                        CellRange rg = grfAdmit.GetCellRange(colChkHn, colChkHn);
                        rg.UserData = cellnote;
                    }
                    i++;
                    //rsbPb.Value++;
                    //Application.DoEvents();
                }
                int ii = 1;
                foreach(Row rowa in grfAdmit.Rows)
                {
                    if (rowa[colChkHn].ToString().Equals("HN")) continue;
                    if (rowa[colChkHn].ToString().Equals("5273456"))
                    {
                        String aaa = "";
                    }
                    Boolean chkTotal0 = false, chkSocial118=false;
                    float total = 0, social1=0, social2 = 0, social3 = 0, social4 = 0, social5 = 0, social6 = 0, social7 = 0, social8 = 0, social9 = 0, social10 = 0;
                    float social11 = 0, social12 = 0, social13 = 0, social14 = 0, social15 = 0, social16 = 0, social17 = 0, social18 = 0, socialtotal=0;
                    String total1 = "", social111="";
                    total1 = rowa[colChkTotal].ToString();
                    float.TryParse(total1, out total);
                    chkTotal0 = total <= 0 ? true : false;
                    float.TryParse(rowa[colChkSo1].ToString(), out social1);
                    float.TryParse(rowa[colChkSo2].ToString(), out social2);
                    float.TryParse(rowa[colChkSo3].ToString(), out social3);
                    float.TryParse(rowa[colChkSo4].ToString(), out social4);
                    float.TryParse(rowa[colChkSo5].ToString(), out social5);
                    float.TryParse(rowa[colChkSo6].ToString(), out social6);
                    float.TryParse(rowa[colChkSo7].ToString(), out social7);
                    float.TryParse(rowa[colChkSo8].ToString(), out social8);
                    float.TryParse(rowa[colChkSo9].ToString(), out social9);
                    float.TryParse(rowa[colChkSo10].ToString(), out social10);
                    float.TryParse(rowa[colChkSo11].ToString(), out social11);
                    float.TryParse(rowa[colChkSo12].ToString(), out social12);
                    float.TryParse(rowa[colChkSo13].ToString(), out social13);
                    float.TryParse(rowa[colChkSo14].ToString(), out social14);
                    float.TryParse(rowa[colChkSo15].ToString(), out social15);
                    float.TryParse(rowa[colChkSo16].ToString(), out social16);
                    float.TryParse(rowa[colChkSo17].ToString(), out social17);
                    float.TryParse(rowa[colChkSo18].ToString(), out social18);
                    socialtotal = social1 + social2 + social3 + social4 + social5 + social6 + social7 + social8 + social9 + social10 + social11 + social12 + social13 + social14 + social15 + social16 + social17 + social18;
                    if ((total > 0) && (socialtotal <= 0))
                    {
                        //rowa.StyleDisplay.BackColor = Color.FromArgb(231, 76, 60);
                        rowa[colChkErr1] = "((total > 0) && (socialtotal <= 0)";
                        grfAdmit.Rows[ii].StyleNew.BackColor = Color.FromArgb(231, 76, 60);
                        //grfAdmit.Rows[ii][colChkHn].
                        rowCntErr++;
                    }
                    else if (total <= 0)
                    {
                        //rowa.StyleDisplay.BackColor = Color.FromArgb(169, 223, 191);
                        grfAdmit.Rows[ii].StyleNew.BackColor = Color.FromArgb(169, 223, 191);
                        rowa[colChkErr2] = "(total <= 0)";
                        rowCntNobill++;
                    }
                    else
                    {
                        grfAdmit.Rows[ii].StyleNew.BackColor = Color.FromArgb(212, 239, 223);
                        rowCntOk++;
                    }
                    ii++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed due to" + ex.Message);
            }
            finally
            {
                connMDB.Close();
            }
            
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            //menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            //grfVs.ContextMenu = menuGw;
            //hideLbLoading();
            rsbPb.Visible = false;
        }
        private void setFromCheck()
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Normal;
            frm.Size = new Size(800, 600);
            C1List list = new C1.Win.C1List.C1List();
            list.Dock = DockStyle.Fill;
            list.Font = fEdit;
            list.DataMode = DataModeEnum.AddItem;
            frm.Controls.Add(list);
            list.ClearItems();
            //list.col
            list.AddItemCols = 2;
            list.Columns.Add(new C1DataColumn());
            list.Columns.Add(new C1DataColumn());
            list.ColumnWidth = 600;
            //list.Columns[0].Caption = "1111";
            list.Columns[0].Caption="-";
            list.Columns[1].Caption = "-";
            list.AddItem("ดึงข้อมูลได้;"+ grfAdmit.Rows.Count);
            String[] reaccess = reAccess.Split(';');
            if (reaccess.Length > 1)
            {
                foreach(String txt in reaccess)
                {
                    list.AddItem(txt.Replace("\r\n",""));
                }
            }
            
            list.AddItem("cnt OK "+ rowCntOk.ToString());
            list.AddItem("cnt no bill " + rowCntNobill.ToString());
            list.AddItem("cnt error " + rowCntErr.ToString());
            frm.ShowDialog(this);
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
        private void FrmImportMDB_Load(object sender, EventArgs e)
        {
            this.Text = "Update 2565-04-06";
            c1SplitContainer1.HeaderHeight = 0;
            c1SplitterPanel1.Height = 100;
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 300, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Font = famtB30;
            lbLoading.Hide();
        }
    }
}
