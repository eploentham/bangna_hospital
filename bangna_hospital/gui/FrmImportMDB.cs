using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        int colHn = 1, colPID=2, colDOB = 3, colSex = 4, colAdmitDate = 5, colTotal = 6, colNetTotal = 7;
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

            theme = new C1ThemeController();
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);

            btnMDBbrow.Click += BtnMDBbrow_Click;
            btnMDBimport.Click += BtnMDBimport_Click;
            btnMDBOk.Click += BtnMDBOk_Click;

            txtPaidType.Value = bc.iniC.importMDBpaidcode;
            txtMDBdatestart.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtMDBdateend.Value = DateTime.Now.ToString("yyyy-MM-dd");

            initGrfAdmit();

            pageLoad = false;
        }
        private void BtnMDBOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            setGrfSrc();
            hideLbLoading();
        }

        private void BtnMDBimport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            OutPatientDB outpDB = new OutPatientDB(bc.conn);
            if (File.Exists(txtMDBpathfile.Text.Trim()))
            {
                if (dtaAdmin.Rows.Count > 0)
                {
                    System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
                    conn.ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data source= " + txtMDBpathfile.Text.Trim();
                    try
                    {
                        outpDB.deleteAll(txtMDBpathfile.Text.Trim(),"");
                        long chk = 0;
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        foreach (DataRow drow in dtaAdmin.Rows)
                        {
                            OutPatient outp = new OutPatient();
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
                            //outp.MainHospitalCode = drow[""] != null ? drow[""].ToString() : "";

                            cmd.Connection = conn;
                            String re = outpDB.insertNoClose(cmd, outp, "");
                            if(!long.TryParse(re, out chk))
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed due to" + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            hideLbLoading();
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
            grfAdmit.Cols.Count = 8;

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
            showLbLoading();
            
            DateTime dtdatestart = new DateTime();
            DateTime dtdateend = new DateTime();

            DateTime.TryParse(txtMDBdateend.Text, out dtdateend);
            DateTime.TryParse(txtMDBdatestart.Text, out dtdatestart);
            lsbMessage.Text = dtdatestart.ToString("yyyy-MM-dd");
            lsbMessage1.Text = dtdateend.ToString("yyyy-MM-dd");
            dtaAdmin = new DataTable();
            dtaAdmin = bc.bcDB.vsDB.selectImportMDB(dtdatestart.ToString("yyyy-MM-dd"), dtdateend.ToString("yyyy-MM-dd"), txtPaidType.Text);
            int i = 1, j = 1, row = grfAdmit.Rows.Count;

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
                rowa[colHn] = row1["MNC_HN_NO"].ToString();
                rowa[colDOB] = row1["mnc_bday"].ToString();
                rowa[colSex] = row1["mnc_sex"].ToString();
                rowa[colAdmitDate] = row1["mnc_date"].ToString();
                rowa[colTotal] = row1["MNC_SUM_PRI"].ToString();
                rowa[colNetTotal] = row1["MNC_SUM_PRI"].ToString();
                rowa[colPID] = row1["MNC_id_no"].ToString();
                rowa[0] = i.ToString();
                i++;
                rsbPb.Value++;
            }
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            //menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            //grfVs.ContextMenu = menuGw;
            hideLbLoading();
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
            this.Text = "Update 2565-04-02";

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
        }
    }
}
