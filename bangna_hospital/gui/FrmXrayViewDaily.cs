using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmXrayViewDaily : Form
    {
        BangnaControl bc;

        C1FlexGrid grfReq, grfProc, grfFin, grfSer;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1ThemeController theme1;

        Font fEdit, fEditB, ff, ffB;
        Color bg, fc, color;

        int colReqId = 1, colReqHn = 2, colReqName = 3, colReqVn = 4, colReqXn = 5, colReqDtr = 6, colReqDpt = 7, colReqreqyr=8, colReqreqno=9, colreqhnyr=10, colreqpreno=11, colreqsex=12, colreqdob=13, colreqsickness=14, colxrdesc=15;
        Timer timer;
        public FrmXrayViewDaily(BangnaControl x)
        {
            InitializeComponent();
            bc = x;

            initConfig();
        }
        private void initConfig()
        {
            theme1 = new C1ThemeController();
            theme1.SetTheme(sB, "BeigeOne");
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            //color = ColorTranslator.FromHtml(ic.iniC.grfRowColor);
            tC1.TabClick += TC1_TabClick;
            int timerlab = 0;
            int.TryParse(bc.iniC.timerImgScanNew, out timerlab);
            timer = new Timer();
            timer.Interval = timerlab * 1000;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;

            initGrfReq();
            initGrfProc();
            setGrfReq();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfReq();
        }

        private void TC1_TabClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tC1.SelectedTab == tabRequest)
            {
                setGrfReq();
            }
            else if (tC1.SelectedTab == tabProcess)
            {
                setGrfProc();
            }
            else if (tC1.SelectedTab == tabFinish)
            {

            }
        }
        private void initGrfProc()
        {
            grfProc = new C1FlexGrid();
            grfProc.Font = fEdit;
            grfProc.Dock = System.Windows.Forms.DockStyle.Fill;
            grfProc.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            //grfQue.AfterRowColChange += GrfReq_AfterRowColChange;
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfBillD.AfterDataRefresh += GrfBillD_AfterDataRefresh;
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("ออก บิล", new EventHandler(ContextMenu_edit_bill));
            //menuGw.MenuItems.Add("ส่งกลับ", new EventHandler(ContextMenu_send_back));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            //grfBillD.ContextMenu = menuGw;
            //grfBillD.SubtotalPosition = SubtotalPositionEnum.BelowData;
            pnProcess.Controls.Add(grfProc);

            theme1.SetTheme(grfProc, "Office2010Red");


            //theme1.SetTheme(tabDiag, "Office2010Blue");
            //theme1.SetTheme(tabFinish, "Office2010Blue");

        }
        private void setGrfProc()
        {
            //grfDept.Rows.Count = 7;
            grfProc.Clear();
            grfProc.Rows.Count = 0;
            DataTable dt = new DataTable();
            String date = "";
            date = System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd");

            dt = bc.bcDB.xrDB.selectVisitStatusPacsReqByDate(date);
            //grfExpn.Rows.Count = dt.Rows.Count + 1;

            grfProc.Rows.Count = dt.Rows.Count + 1;
            grfProc.Cols.Count = 15;
            //C1TextBox txt = new C1TextBox();
            //C1ComboBox cboproce = new C1ComboBox();
            //ic.ivfDB.itmDB.setCboItem(cboproce);
            //grfBillD.Cols[colName].Editor = txt;
            //grfBillD.Cols[colAmt].Editor = txt;
            //grfBillD.Cols[colDiscount].Editor = txt;
            //grfBillD.Cols[colNetAmt].Editor = txt;

            grfProc.Cols[colReqHn].Width = 80;
            grfProc.Cols[colReqName].Width = 200;
            grfProc.Cols[colReqVn].Width = 80;
            grfProc.Cols[colReqXn].Width = 80;
            grfProc.Cols[colReqDtr].Width = 100;
            grfProc.Cols[colReqDpt].Width = 100;
            //grfReq.Cols[colqty].Width = 80;

            grfProc.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            grfProc.Cols[colReqHn].Caption = "HN";
            grfProc.Cols[colReqName].Caption = "Name";
            grfProc.Cols[colReqVn].Caption = "VN";
            grfProc.Cols[colReqXn].Caption = "XN";
            grfProc.Cols[colReqDtr].Caption = "Doctor";
            grfProc.Cols[colReqDpt].Caption = "Department";
            //grfReq.Cols[colqty].Caption = "QTY";

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
                    grfProc[i, 0] = i;
                    grfProc[i, colReqId] = "";
                    grfProc[i, colReqHn] = row["mnc_hn_no"].ToString();
                    grfProc[i, colReqName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfProc[i, colReqVn] = row["mnc_vn_no"].ToString() + " " + row["mnc_vn_seq"].ToString() + " " + row["mnc_vn_sum"].ToString();

                    grfProc[i, colReqXn] = "";
                    grfProc[i, colReqDtr] = "";
                    grfProc[i, colReqDpt] = "";
                    grfProc[i, colReqreqyr] = row["mnc_req_yr"].ToString();
                    grfProc[i, colReqreqno] = row["mnc_req_no"].ToString();
                    grfProc[i, colreqhnyr] = row["mnc_hn_yr"].ToString();
                    grfProc[i, colreqpreno] = row["mnc_pre_no"].ToString();

                    i++;
                }
                catch (Exception ex)
                {
                    String err = "";
                }
            }
            CellNoteManager mgr = new CellNoteManager(grfProc);
            grfProc.Cols[colReqId].Visible = false;
            grfProc.Cols[colReqreqyr].Visible = false;
            grfProc.Cols[colReqreqno].Visible = false;
            grfProc.Cols[colreqhnyr].Visible = false;
            grfProc.Cols[colreqpreno].Visible = false;

            grfProc.Cols[colReqHn].AllowEditing = false;
            grfProc.Cols[colReqName].AllowEditing = false;
            grfProc.Cols[colReqVn].AllowEditing = false;
            grfProc.Cols[colReqXn].AllowEditing = false;
            grfProc.Cols[colReqDtr].AllowEditing = false;
            grfProc.Cols[colReqDpt].AllowEditing = false;

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
            pnRequest.Controls.Add(grfReq);

            theme1.SetTheme(grfReq, "Office2010Red");
            
            //theme1.SetTheme(tabDiag, "Office2010Blue");
            //theme1.SetTheme(tabFinish, "Office2010Blue");

        }

        private void GrfReq_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfReq.Row <= 0) return;
            if (grfReq.Col <= 0) return;
            String hn = "", name = "", sex = "", dob = "", sickness = "", vn="", hnreqyear="", preno="", reqno="", xray="";
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
            String re = bc.bcDB.resoDB.insertResOrderTab(reso, "");
            //MessageBox.Show("re " + re, "");
            long chk = 0, chk1=0;
            if(long.TryParse(re, out chk))
            {
                //MessageBox.Show("chk " + chk, "");
                String re1 = "";
                re1 = bc.bcDB.xrDB.updateStatusPACs(reqno, hnreqyear);
                //MessageBox.Show("re1 " + re1, "");
                if (long.TryParse(re1, out chk1))
                {
                    setGrfReq();
                }
            }
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
                    grfReq[i, colReqName] = row["prefix"].ToString()+" "+ row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
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

                    grfReq[i, colreqdob] = dt1.Year+ dt1.ToString("MMdd");
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
        private void FrmXrayViewDaily_Load(object sender, EventArgs e)
        {
            tC1.SelectedTab = tabRequest;
            this.Text = "Update 16-10-2562";
        }
    }
}
