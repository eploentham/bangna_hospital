using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static C1.C1Preview.Strings;

namespace bangna_hospital.gui
{
    public partial class FrmCashier : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        C1ThemeController theme1;
        C1FlexGrid grfOperList, grfFinish, grfInv;
        DataTable DTINV = new DataTable();
        Label lbLoading;
        String PRENO = "", VSDATE = "", HN = "", DEPTNO = "", DTRCODE = "", DOCGRPID = "", TABVSACTIVE = "", PTTNAME="", VSTIME="", DOCNO="", DOCDATE="";
        Boolean pageLoad = false;
        int colgrfOperListHn = 1, colgrfOperListFullNameT = 2, colgrfOperListSymptoms = 3, colgrfOperListPaidName = 4, colgrfOperListPreno = 5, colgrfOperListVsDate = 6, colgrfOperListVsTime = 7, colgrfOperListActNo = 8, colgrfOperListDtrName = 9, colgrfOperListLab = 10, colgrfOperListXray = 11;
        int colgrfFinishHn = 1, colgrfFinishFullNameT = 2, colgrfFinishPaidName = 3, colgrfFinishSymptoms = 4, colgrfFinishPreno = 5, colgrfFinishVsDate = 6, colgrfFinishVsTime = 7, colgrfFinishActNo = 8, colgrfFinishDocno = 9;
        public FrmCashier(BangnaControl bc)
        {
            this.bc = bc; this.PTT = null; InitializeComponent();
            initConfig();
        }
        public FrmCashier(BangnaControl bc, Patient ptt)
        {
            this.bc = bc; this.PTT = ptt; InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            theme1 = new C1ThemeController();
            initFont();
            initLoading();
            setEvent(); setTheme();
            initGrfFinish();
            initGrfInv();
            pageLoad = false;
        }
        private void initLoading()
        {
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
        }
        private void initFont()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt7B = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);

        }
        private void setTheme()
        {
            //theme1.SetTheme(pnPtt, "ExpressionLight");
            //foreach (Control c in pnPtt.Controls) if (c is C1TextBox) theme1.SetTheme(c, "ExpressionLight");
        }
        private void setEvent()
        {
            tabMain.SelectedTabChanged += TabMain_SelectedTabChanged;
            rgPrn.Click += RgPrn_Click;
            rgSearch.KeyPress += RgSearch_KeyPress;
            rgPrn1.Click += RgPrn1_Click;
            rgPrnReceipt.Click += RgPrnReceipt_Click;
        }

        private void RgPrnReceipt_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (DTINV == null || DTINV.Rows.Count <= 0)
            {
                MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการพิมพ์");
                return;
            }
            printReceipt("original/ต้นฉบับ", bc.iniC.statusPrintPreview.Equals("1") ? "preview" : "");
            printReceipt("copy/สำเนา", bc.iniC.statusPrintPreview.Equals("1") ? "preview" : "");
        }

        private void RgSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyChar == (char)Keys.Enter)
            {
                String txt = rgSearch.Text.Trim();
                if (txt.Length > 0)
                {
                    
                }
                else
                {
                    
                }
            }
        }
        private void RgPrn1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printReceipt("copy/สำเนา", bc.iniC.statusPrintPreview.Equals("1") ? "preview":"");
        }
        private void RgPrn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printReceipt("original/ต้นฉบับ", bc.iniC.statusPrintPreview.Equals("1") ? "preview":"");
        }
        private void printReceipt(String original, String preview)
        {
            setPrintINV();
            
            DataTable dtinv = new DataTable();
            dtinv = DTINV.Copy();
            if (dtinv == null || dtinv.Rows.Count <= 0) return;
            if (!dtinv.Columns.Contains("apmmake")) dtinv.Columns.Add("apmmake", typeof(String));
            if (!dtinv.Columns.Contains("phone")) dtinv.Columns.Add("phone", typeof(String));
            float total = 0;
            foreach (DataRow arow in dtinv.Rows)
            {
                float.TryParse(arow["amount"].ToString(), out float amt);
                total+=amt;
                arow["amount"] = amt.ToString("#,##0.00");
                if (float.Parse(arow["amount"].ToString()) <= 0)
                {
                    arow.Delete();
                }
                else
                {
                    arow["apmmake"] = original;
                    arow["phone"] = "0";
                }
            }
            dtinv.AcceptChanges();
            //DataTable dtinv1 = new DataTable();
            //dtinv1 = dtinv.Copy();
            //foreach (DataRow arow in dtinv.Rows)
            //{
            //    DataRow newRow = dtinv1.NewRow();
            //    foreach (DataColumn col in dtinv.Columns)
            //    {
            //        newRow[col.ColumnName] = arow[col.ColumnName];
            //    }
            //    newRow["apmmake"] = "copy/สำเนา";
            //    newRow["phone"] = "1";
            //    dtinv1.Rows.Add(newRow);
            //}
            FrmReportNew frm = new FrmReportNew(bc, "cashier_receipt", total.ToString("#,##0.00"), original,"");
            frm.DT = dtinv;
            if (preview.Equals("preview"))
            {
                frm.ShowDialog(this);
            }
            else
            {
                frm.PrintReport();
            }
        }
        private void setPrintINV()
        {
            if (DTINV == null) return;
            DataTable dttem01 = bc.bcDB.tem01DB.selectM01(DOCDATE,DOCNO);
            if(dttem01 == null || dttem01.Rows.Count <= 0) return;
            if(!DTINV.Columns.Contains("hn")) DTINV.Columns.Add("hn", typeof(String));
            if (!DTINV.Columns.Contains("desc1")) DTINV.Columns.Add("desc1", typeof(String));
            if (!DTINV.Columns.Contains("pttname")) DTINV.Columns.Add("pttname", typeof(String));
            if (!DTINV.Columns.Contains("inv_no")) DTINV.Columns.Add("inv_no", typeof(String));
            if (!DTINV.Columns.Contains("apm_date")) DTINV.Columns.Add("apm_date", typeof(String));
            if (!DTINV.Columns.Contains("end_date")) DTINV.Columns.Add("end_date", typeof(String));
            if (!DTINV.Columns.Contains("apm_time")) DTINV.Columns.Add("apm_time", typeof(String));
            if (!DTINV.Columns.Contains("amount")) DTINV.Columns.Add("amount", typeof(String));
            foreach (DataRow drow in DTINV.Rows)
            {
                Boolean chkname = false;
                String invno = dttem01.Rows[0]["MNC_DOC_NO2"].ToString();
                String[] invno1 = invno.Split('#');
                if(invno1.Length > 1)
                {
                    String invno2 = invno1[1].Length>2 ? invno1[1].Substring(invno1[1].Length-2) : invno1[1];
                    String invno3= invno1[1].Length > 2 ? invno1[1].Substring(0,invno1[1].Length - 4) : invno1[1];
                    invno = "BO"+ invno3 + invno2;
                }
                drow["hn"] = HN;
                drow["desc1"] = drow["MNC_DEF_CD"].ToString()+" "+drow["MNC_DEF_DSC"].ToString();
                drow["pttname"] = PTTNAME;
                drow["inv_no"] = DOCNO;
                drow["apm_date"] = bc.datetoShow1(VSDATE);
                drow["end_date"] = bc.datetoShow1(VSDATE);
                drow["apm_time"] = bc.showTime(VSTIME);
                drow["amount"] = drow["MNC_AMT"].ToString();
                drow["inv_no"] = invno;
                //chkname = txtName.Text.Any(c => !Char.IsLetterOrDigit(c));
                //if (chkname)
                //{
                //    drow["patient_age"] = ptt.AgeString().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                //}
                //else
                //{
                //    drow["patient_age"] = ptt.AgeString();
                //}
            }
        }
        private void TabMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tabMain.SelectedTab == tabOper)
            {
                setLbLoading("กำลังโหลดข้อมูล ...");
                showLbLoading();
                //setGrfOperList();
                hideLbLoading();
                TABVSACTIVE = tabOper.Name;
            }
            else if (tabMain.SelectedTab == tabFinish)
            {
                setLbLoading("กำลังโหลดข้อมูล ...");
                showLbLoading();
                setGrfFinish();
                hideLbLoading();
                TABVSACTIVE = tabFinish.Name;
            }
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
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
        private void initGrfInv()
        {
            grfInv = new C1FlexGrid();
            grfInv.Font = fEdit;
            grfInv.Dock = System.Windows.Forms.DockStyle.Fill;
            grfInv.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfInv.Rows.Count = 1;
            grfInv.Cols.Count = 10;
            grfInv.Cols[colgrfFinishHn].DataType = typeof(string);
            grfInv.Cols[colgrfFinishHn].Caption = "MNC_DEF_CD";
            grfInv.Cols[colgrfFinishFullNameT].Caption = "MNC_DEF_DSC";
            grfInv.Cols[colgrfFinishSymptoms].Caption = "MNC_ORD_BY";
            grfInv.Cols[colgrfFinishPaidName].Caption = "MNC_AMT";
            grfInv.Cols[colgrfFinishPreno].Caption = "MNC_DEF_LEV";
            grfInv.Cols[colgrfFinishVsDate].Caption = "MNC_TOT_AMT";
            grfInv.Cols[colgrfFinishVsTime].Caption = "MNC_DIS_AMT";
            grfInv.Cols[colgrfFinishActNo].Caption = "";
            grfInv.Cols[colgrfFinishDocno].Caption = "MNC_DOC_NO";
            grfInv.Cols[colgrfFinishHn].Width = 90;
            grfInv.Cols[colgrfFinishFullNameT].Width = 300;
            grfInv.Cols[colgrfFinishSymptoms].Width = 100;
            grfInv.Cols[colgrfFinishPaidName].Width = 100;
            grfInv.Cols[colgrfFinishPreno].Width = 50;
            grfInv.Cols[colgrfFinishVsDate].Width = 100;
            grfInv.Cols[colgrfFinishVsTime].Width = 70;
            grfInv.Cols[colgrfFinishActNo].Width = 100;
            grfInv.Cols[colgrfFinishDocno].Width = 200;
            grfInv.Cols[colgrfFinishHn].AllowEditing = false;
            grfInv.Cols[colgrfFinishFullNameT].AllowEditing = false;
            grfInv.Cols[colgrfFinishSymptoms].AllowEditing = false;
            grfInv.Cols[colgrfFinishPaidName].AllowEditing = false;
            grfInv.Cols[colgrfFinishPreno].AllowEditing = false;
            grfInv.Cols[colgrfFinishVsDate].AllowEditing = false;
            grfInv.Cols[colgrfFinishVsTime].AllowEditing = false;
            grfInv.Cols[colgrfFinishActNo].AllowEditing = false;
            grfInv.Cols[colgrfFinishDocno].AllowEditing = false;
            grfInv.Cols[colgrfFinishPreno].Visible = false;
            grfInv.Name = "grfInv";
            pnGrfFinishInv.Controls.Add(grfInv);
            theme1.SetTheme(grfInv, bc.iniC.themegrfIpd);
        }
        private void setGrfInv(String docno, String docdate)
        {
            if (pageLoad) return;
            setLbLoading("กำลังโหลดข้อมูล ...");
            showLbLoading();
            DTINV = bc.bcDB.tem02DB.SelectByDocno1(docno, docdate);
            grfInv.Rows.Count = 1; grfInv.Rows.Count = DTINV.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in DTINV.Rows)
            {
                Row rowa = grfInv.Rows[i];
                rowa[colgrfFinishHn] = row1["MNC_DEF_CD"].ToString();
                rowa[colgrfFinishFullNameT] = row1["MNC_DEF_DSC"].ToString();
                rowa[colgrfFinishSymptoms] = row1["MNC_ORD_BY"].ToString();
                rowa[colgrfFinishPaidName] = row1["MNC_AMT"].ToString();
                rowa[colgrfFinishPreno] = row1["MNC_DEF_LEV"].ToString();
                rowa[colgrfFinishVsDate] = row1["MNC_TOT_AMT"].ToString();
                rowa[colgrfFinishVsTime] = row1["MNC_DIS_AMT"].ToString();

                rowa[colgrfFinishDocno] = row1["MNC_DOC_NO"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            lfSbMessage.Text = "พบ " + DTINV.Rows.Count + "รายการ";
            hideLbLoading();
        }
        private void initGrfFinish()
        {
            grfFinish = new C1FlexGrid();
            grfFinish.Font = fEdit;
            grfFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            grfFinish.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfFinish.Rows.Count = 1;
            grfFinish.Cols.Count = 10;
            grfFinish.Cols[colgrfFinishHn].Caption = "HN";
            grfFinish.Cols[colgrfFinishFullNameT].Caption = "Patient Name";
            grfFinish.Cols[colgrfFinishSymptoms].Caption = "อาการ";
            grfFinish.Cols[colgrfFinishPaidName].Caption = "สิทธิ";
            grfFinish.Cols[colgrfFinishPreno].Caption = "preno";
            grfFinish.Cols[colgrfFinishVsDate].Caption = "วันที่";
            grfFinish.Cols[colgrfFinishVsTime].Caption = "เวลา";
            grfFinish.Cols[colgrfFinishActNo].Caption = "Act No";
            grfFinish.Cols[colgrfFinishDocno].Caption = "Dtr Name";
            grfFinish.Cols[colgrfFinishHn].Width = 90;
            grfFinish.Cols[colgrfFinishFullNameT].Width = 200;
            grfFinish.Cols[colgrfFinishSymptoms].Width = 300;
            grfFinish.Cols[colgrfFinishPaidName].Width = 120;
            grfFinish.Cols[colgrfFinishPreno].Width = 50;
            grfFinish.Cols[colgrfFinishVsDate].Width = 100;
            grfFinish.Cols[colgrfFinishVsTime].Width = 70;
            grfFinish.Cols[colgrfFinishActNo].Width = 100;
            grfFinish.Cols[colgrfFinishDocno].Width = 200;
            grfFinish.Cols[colgrfFinishHn].AllowEditing = false;
            grfFinish.Cols[colgrfFinishFullNameT].AllowEditing = false;
            grfFinish.Cols[colgrfFinishSymptoms].AllowEditing = false;
            grfFinish.Cols[colgrfFinishPaidName].AllowEditing = false;
            grfFinish.Cols[colgrfFinishPreno].AllowEditing = false;
            grfFinish.Cols[colgrfFinishVsDate].AllowEditing = false;
            grfFinish.Cols[colgrfFinishVsTime].AllowEditing = false;
            grfFinish.Cols[colgrfFinishActNo].AllowEditing = false;
            grfFinish.Cols[colgrfFinishDocno].AllowEditing = false;
            grfFinish.Cols[colgrfFinishPreno].Visible = false;
            grfFinish.Name = "grfFinish";
            grfFinish.Click += GrfFinish_Click;
            pnGrfFinishView.Controls.Add(grfFinish);
            theme1.SetTheme(grfFinish, bc.iniC.themegrfIpd);
        }
        private void GrfFinish_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfFinish.Row <= 0) return;
            HN = grfFinish[grfFinish.Row, colgrfFinishHn].ToString();
            PRENO = grfFinish[grfFinish.Row, colgrfFinishPreno].ToString();
            PTTNAME = grfFinish[grfFinish.Row, colgrfFinishFullNameT].ToString();
            VSDATE = grfFinish[grfFinish.Row, colgrfFinishVsDate].ToString();
            VSTIME = grfFinish[grfFinish.Row, colgrfFinishVsTime].ToString();
            DOCDATE = grfFinish[grfFinish.Row, colgrfFinishVsDate].ToString();
            DOCNO = grfFinish[grfFinish.Row, colgrfFinishDocno].ToString();
            
            setGrfInv(DOCNO, DOCDATE);
        }
        private void setGrfFinish()
        {
            if (pageLoad) return;
            DataTable dtvs = new DataTable();
            
            dtvs = bc.bcDB.fint01DB.SelectPaidOPDByDate(System.DateTime.Now.Year+"-"+DateTime.Now.ToString("MM-dd"));

            grfFinish.Rows.Count = 1; grfFinish.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfFinish.Rows[i];
                rowa[colgrfFinishHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfFinishFullNameT] = row1["pttfullname"].ToString();
                rowa[colgrfFinishSymptoms] = row1["MNC_DIA_DSC"].ToString();
                rowa[colgrfFinishPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfFinishPreno] = row1["MNC_PRE_NO"].ToString();
                rowa[colgrfFinishVsDate] = row1["MNC_DOC_DAT"].ToString();
                rowa[colgrfFinishVsTime] = row1["MNC_DOC_TIM"].ToString();

                rowa[colgrfFinishDocno] = row1["MNC_DOC_NO"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            lfSbMessage.Text = "พบ " + dtvs.Rows.Count + "รายการ";
        }
        private void FrmCashier_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 20250807  ";
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            scFinish.HeaderHeight = 0;
        }
    }
}
