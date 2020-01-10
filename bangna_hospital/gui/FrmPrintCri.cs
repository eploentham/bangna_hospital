using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmPrintCri : Form
    {
        BangnaControl bc;
        Login login;
        int colHnId = 1, colHnPrn=2, colHnPrnDrug = 3, colHnPrnStaffNote = 4, colHnPrnReqLab = 5, colHnPrnReqXray = 6, colHnPrnResLab = 7, colHnPrnResXray = 8, colHnVnShow = 9, colHnHn = 10, colHnPttName = 11, colHnVsDate = 12, colHnVsTime = 13, colHnSex = 14, colHnAge = 15, colHnPaid = 16, colHnSymptom = 17, colHnPreNo = 18, colHnDsc = 19;

        C1FlexGrid grfHn;
        Font fEdit, fEditB, fEditBig;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmPrintCri(BangnaControl bc, FrmSplash splash)
        {
            InitializeComponent();
            this.bc = bc;
            login = new Login(bc, splash);
            login.ShowDialog(this);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                bc.bcDB = new objdb.BangnaHospitalDB(bc.conn);
                bc.getInit();
            }).Start();
            splash.Dispose();
            if (login.LogonSuccessful.Equals("1"))
            {
                initConfig();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */

                }).Start();
            }
            else
            {
                Application.Exit();
            }
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);

            btnSel.Click += BtnSel_Click;
            BtnPrepare.Click += BtnPrepare_Click;

            initGrfHn();
        }

        private void BtnPrepare_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfHn()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfHn.DoubleClick += GrfHn_DoubleClick;
            grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnHn.Controls.Add(grfHn);
            grfHn.Rows[0].Visible = false;
            grfHn.Cols[0].Visible = false;
            //theme1.SetTheme(grf, "Office2010Blue");

        }

        private void GrfHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHn == null) return;
            if (grfHn.Row <= 0) return;
            if (grfHn.Col <= 0) return;

            if(grfHn.Col == colHnPrn)
            {
                if(!(Boolean)grfHn[grfHn.Row, colHnPrn] == true)
                {
                    grfHn[grfHn.Row, colHnPrn] = true;
                }
                else
                {
                    grfHn[grfHn.Row, colHnPrn] = false;
                }
            }
        }

        private void setGrfHn()
        {
            String datestart = "", dateend = "", hn = "",txt="";
            String[] hn1;
            DataTable dt = new DataTable();

            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);
            hn = txtHn.Text.Trim();
            hn1 = hn.Split(',');
            if (hn1.Length <= 0)
            {
                MessageBox.Show("No data HN", "");
                return;
            }

            for (int j = 0; j < hn1.Length; j++)
            {
                hn1[j] = "'" + hn1[j].Trim() + "'";
                txt += hn1[j] + ",";
            }
            if (txt.Length > 0)
            {
                if (txt.Substring(txt.Length-1).Equals(","))
                {
                    txt = txt.Substring(0,txt.Length-1);
                }
            }
            dt = bc.bcDB.vsDB.selectVisitByLikeHn(txt, datestart, dateend);

            grfHn.Clear();
            grfHn.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfHn.Cols.Count = 20;
            Column colChk = grfHn.Cols[colHnPrnReqLab];
            colChk.DataType = typeof(Boolean);
            Column colChk2 = grfHn.Cols[colHnPrnReqXray];
            colChk2.DataType = typeof(Boolean);
            Column colChk3 = grfHn.Cols[colHnPrnResLab];
            colChk3.DataType = typeof(Boolean);
            Column colChk4 = grfHn.Cols[colHnPrnResXray];
            colChk4.DataType = typeof(Boolean);
            Column colChk5= grfHn.Cols[colHnPrnDrug];
            colChk5.DataType = typeof(Boolean);
            Column colChk6 = grfHn.Cols[colHnPrnStaffNote];
            colChk6.DataType = typeof(Boolean);
            grfHn.Cols[colHnPrnDrug].Caption = "ใบยา";
            grfHn.Cols[colHnPrnStaffNote].Caption = "Staff N.";
            grfHn.Cols[colHnPrnReqLab].Caption = "Req Lab";
            grfHn.Cols[colHnPrnReqXray].Caption = "Req Xray";
            grfHn.Cols[colHnPrnResLab].Caption = "Req Lab";
            grfHn.Cols[colHnPrnResXray].Caption = "Res Xray";
            grfHn.Cols[colHnVnShow].Caption = "VN";
            grfHn.Cols[colHnHn].Caption = "HN";
            grfHn.Cols[colHnPttName].Caption = "Patient Name";
            grfHn.Cols[colHnVsDate].Caption = "Date";
            grfHn.Cols[colHnVsTime].Caption = "Time";
            grfHn.Cols[colHnSex].Caption = "Sex";
            grfHn.Cols[colHnAge].Caption = "Age";
            grfHn.Cols[colHnPaid].Caption = "Paid";
            grfHn.Cols[colHnDsc].Caption = "Description";

            grfHn.Cols[colHnPrnStaffNote].Width = 60;
            grfHn.Cols[colHnPrnReqLab].Width = 60;
            grfHn.Cols[colHnPrnReqLab].Width = 60;
            grfHn.Cols[colHnPrnReqXray].Width = 60;
            grfHn.Cols[colHnPrnResLab].Width = 60;
            grfHn.Cols[colHnPrnResXray].Width = 65;
            grfHn.Cols[colHnVnShow].Width = 80;
            grfHn.Cols[colHnHn].Width = 80;
            grfHn.Cols[colHnPttName].Width = 310;
            grfHn.Cols[colHnVsDate].Width = 110;
            grfHn.Cols[colHnVsTime].Width = 80;
            grfHn.Cols[colHnSex].Width = 60;
            grfHn.Cols[colHnAge].Width = 80;
            grfHn.Cols[colHnPaid].Width = 110;
            grfHn.Cols[colHnDsc].Width = 300;

            grfHn.ShowCursor = true;
            //grf.Cols[colPic1].ImageAndText = true;
            //grf.Cols[colPic2].ImageAndText = true;
            //grf.Cols[colPic3].ImageAndText = true;
            //grf.Cols[colPic4].ImageAndText = true;
            //grf.Styles.Normal.ImageAlign = ImageAlignEnum.CenterTop;
            //grf.Styles.Normal.TextAlign = TextAlignEnum.CenterBottom;
            ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&แก้ไข Image", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&Rotate Image", new EventHandler(ContextMenu_retate));
            //menuGw.MenuItems.Add("Delete Image", new EventHandler(ContextMenu_delete));
            grfHn.ContextMenu = menuGw;
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image ["+dgs.doc_group_name+"]", new EventHandler(ContextMenu_upload));
            String date = "";
            //if (lDgss.Count <= 0) getlBsp();
            date = txtDateStart.Text;
            
            int i = 1;
            grfHn.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    String status = "", vn = "";
                    Patient ptt = new Patient();
                    vn = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
                    ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
                    grfHn[i, 0] = (i);
                    grfHn[i, colHnPrn] = true;
                    grfHn[i, colHnPrnDrug] = chkDrug.Checked;
                    grfHn[i, colHnPrnStaffNote] = chkStaffNote.Checked;
                    grfHn[i, colHnPrnReqLab] = chkReqLab.Checked;
                    grfHn[i, colHnPrnReqXray] = chkReqXray.Checked;
                    grfHn[i, colHnPrnResLab] = chkResLab.Checked;
                    grfHn[i, colHnPrnResXray] = chkResXray.Checked;
                    grfHn[i, colHnId] = vn;
                    grfHn[i, colHnVnShow] = vn;
                    grfHn[i, colHnVsDate] = bc.datetoShow(row["mnc_date"].ToString());
                    grfHn[i, colHnHn] = row["MNC_HN_NO"].ToString();
                    grfHn[i, colHnPttName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfHn[i, colHnVsTime] = bc.FormatTime(row["mnc_time"].ToString());
                    //grfQue[i, colQueVnShow] = row["MNC_REQ_DAT"].ToString();
                    grfHn[i, colHnSex] = row["mnc_sex"].ToString();
                    grfHn[i, colHnAge] = ptt.AgeStringShort();
                    grfHn[i, colHnPaid] = row["MNC_FN_TYP_DSC"].ToString();
                    grfHn[i, colHnSymptom] = row["MNC_SHIF_MEMO"].ToString();

                    grfHn[i, colHnPreNo] = row["MNC_pre_no"].ToString();
                    grfHn[i, colHnDsc] = row["MNC_ref_dsc"].ToString();
                    if ((i % 2) == 0)
                        grfHn.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

                    i++;
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmPrintCri setGrfHn ex " + ex.Message);
                }
            }

            //addDevice.MenuItems.Add("", new EventHandler(ContextMenu_upload));
            //menuGw.MenuItems.Add(addDevice);
            //}

            //row1[colVSE2] = row[ic.ivfDB.pApmDB.pApm.e2].ToString().Equals("1") ? imgCorr : imgTran;
            grfHn.Cols[colHnId].Visible = false;
            grfHn.Cols[colHnPreNo].Visible = false;
            grfHn.Cols[colHnPrn].Visible = false;

            grfHn.Cols[colHnVnShow].AllowEditing = false;
            grfHn.Cols[colHnHn].AllowEditing = false;
            grfHn.Cols[colHnPttName].AllowEditing = false;
            grfHn.Cols[colHnVsDate].AllowEditing = false;
            grfHn.Cols[colHnVsTime].AllowEditing = false;
            grfHn.Cols[colHnSex].AllowEditing = false;
            grfHn.Cols[colHnAge].AllowEditing = false;
            grfHn.Cols[colHnPaid].AllowEditing = false;
            grfHn.Cols[colHnSymptom].AllowEditing = false;
            grfHn.Cols[colHnPreNo].AllowEditing = false;
            grfHn.Cols[colHnDsc].AllowEditing = false;
            //grfQue.Cols[colQueVnShow].AllowEditing = false;
        }
        private void GrfHn_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void BtnSel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfHn();
            
        }

        private void FrmPrintCri_Load(object sender, EventArgs e)
        {
            tC1.SelectedTab = tabPrn1;
            sC1.HeaderHeight = 0;
        }
    }
}
