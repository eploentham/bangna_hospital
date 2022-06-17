using bangna_hospital.control;
using bangna_hospital.object1;
using C1.C1Pdf;
using C1.Win.C1Command;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmReceptionCovidSend:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;

        C1DockingTab tcMain;
        C1DockingTabPage tabImportDf;
        C1FlexGrid grfSelect;
        C1Button btnPrint, btnDeleteAll, btnGet;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1ThemeController theme1;
        Label lbDateStart, lbHn, lbtxtPaidType, lbLoading;
        C1DateEdit txtDateStart, txtDateEnd;
        C1TextBox txtPaidType, txtHn;
        C1ComboBox cboDocGrp;

        int colHn = 1, colFullName = 2, colMobile = 3, colDoc = 4, colattachnote = 5, colID=6,colPID=7;

        Boolean pageLoad = false;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);

        public FrmReceptionCovidSend(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            sep = new C1SuperErrorProvider();
            stt = new C1SuperTooltip();
            initCompoment();
            setControl();
            this.Load += FrmReceptionCovidSend_Load;
            btnPrint.Click += BtnPrint_Click;
            cboDocGrp.SelectedIndexChanged += CboDocGrp_SelectedIndexChanged;
            txtHn.KeyUp += TxtHn_KeyUp;
            grfSelect.DoubleClick += GrfSelect_DoubleClick;
            btnDeleteAll.Click += BtnDeleteAll_Click;
            btnGet.Click += BtnGet_Click;
            
            pageLoad = false;
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DateTime dtstart = new DateTime();
            dtstart = DateTime.Now;
            DateTime.TryParse(txtDateStart.Text, out dtstart);

            String date = "";
            date = dtstart.ToString("yyyy-MM-dd");
            DataTable dt = new DataTable();
            dt = bc.bcDB.pttDB.selectPatientCovidByDate(date);
            if (dt.Rows.Count > 0)
            {
                int chk = 0;
                Patient ptt = new Patient();
                PatientSmartcard pttsc = new PatientSmartcard();
                foreach (DataRow drow in dt.Rows)
                {
                    ptt.Hn = drow["MNC_HN_NO"].ToString();
                    ptt.Age = drow["MNC_AGE"].ToString();
                    ptt.patient_birthday = drow["MNC_bday"].ToString();
                    ptt.Name = drow["prefix"].ToString() + " " + drow["MNC_FNAME_T"].ToString() + " " + drow["MNC_LNAME_T"].ToString();
                    ptt.idcard = drow["mnc_id_no"].ToString();
                    ptt.hnyr = drow["mnc_hn_yr"].ToString();
                    ptt.MNC_HN_NO = drow["MNC_HN_NO"].ToString();
                    ptt.MNC_HN_YR = drow["mnc_hn_yr"].ToString();
                    ptt.MNC_CUR_CHW = drow["MNC_CUR_CHW"].ToString();
                    ptt.MNC_CUR_AMP = drow["MNC_CUR_AMP"].ToString();
                    ptt.MNC_CUR_TUM = drow["MNC_CUR_TUM"].ToString();
                    ptt.MNC_CUR_ADD = drow["MNC_CUR_ADD"].ToString();
                    ptt.MNC_CUR_MOO = drow["MNC_CUR_MOO"].ToString();
                    ptt.MNC_CUR_SOI = drow["MNC_CUR_SOI"].ToString();
                    ptt.MNC_FNAME_T = drow["MNC_FNAME_T"].ToString();
                    ptt.MNC_LNAME_T = drow["MNC_LNAME_T"].ToString();
                    ptt.MNC_FNAME_E = drow["MNC_FNAME_E"].ToString();
                    ptt.MNC_LNAME_E = drow["MNC_LNAME_E"].ToString();
                    ptt.MNC_PFIX_CDT = drow["MNC_PFIX_CDT"].ToString();
                    ptt.MNC_CUR_TEL = drow["MNC_CUR_TEL"].ToString();
                    ptt.MNC_PFIX_CDE = drow["MNC_PFIX_CDE"].ToString();
                    ptt.MNC_ATT_NOTE = drow["MNC_ATT_NOTE"].ToString();
                    ptt.MNC_OCC_CD = drow["MNC_OCC_CD"].ToString();
                    ptt.MNC_EDU_CD = drow["MNC_EDU_CD"].ToString();
                    ptt.MNC_NAT_CD = drow["MNC_NAT_CD"].ToString();
                    ptt.MNC_REL_CD = drow["MNC_REL_CD"].ToString();
                    ptt.MNC_NATI_CD = drow["MNC_NATI_CD"].ToString();
                    ptt.MNC_CUR_ROAD = drow["MNC_CUR_ROAD"].ToString();
                    setPatientSmartcard(pttsc, ptt);
                    String re = bc.bcDB.pttscDB.insertPatientSmartcard(pttsc, "");
                }
                setGrfOPBKKMainCHRGITEM();
            }
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการ ลบข้อมุลทั้งหมด ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                bc.bcDB.pttscDB.deleteAll();
                setGrfOPBKKMainCHRGITEM();
            }
        }

        private void GrfSelect_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSelect.Row <= 0) return;

            String id = "";
            id = grfSelect[grfSelect.Row, colID] != null ? grfSelect[grfSelect.Row, colID].ToString() : "";
            bc.bcDB.pttscDB.delete(id);
            setGrfOPBKKMainCHRGITEM();
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                sep.Clear();
                if (txtPaidType.Text.Length < 9)
                {
                    sep.SetError(txtPaidType, "error");
                    return;
                }
                try
                {
                    Patient ptt = new Patient();
                    if (txtHn.Text.Length == 13)
                    {
                        ptt = bc.bcDB.pttDB.selectPatinetByID1(txtHn.Text.Trim());
                    }
                    else
                    {
                        ptt = bc.bcDB.pttDB.selectPatinetByHn(txtHn.Text.Trim());
                    }
                    
                    if (ptt.MNC_HN_NO.Length > 0)
                    {
                        int chk = 0;
                        PatientSmartcard pttsc = new PatientSmartcard();
                        setPatientSmartcard(pttsc, ptt);
                        Visit vs = new Visit();
                        //vs = bc.bcDB.vsDB.selectVisit
                        String re = bc.bcDB.pttscDB.insertPatientSmartcard(pttsc, "");
                        if (int.TryParse(re, out chk))
                        {
                            setGrfOPBKKMainCHRGITEM();
                            txtHn.SelectAll();
                            //txtPaidType.SelectAll();
                        }
                        else
                        {
                            MessageBox.Show("error", "");
                        }
                    }
                    else
                    {
                        sep.SetError(txtHn, "error");
                        txtHn.SelectAll();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("error "+ex.Message, "");
                    txtHn.SelectAll();
                }
                
            }
        }
        private PatientSmartcard setPatientSmartcard(PatientSmartcard pttsc, Patient ptt)
        {
            String doc = "", datestart = "", pid1="";
            DateTime dtstart = new DateTime();
            DateTime.TryParse(txtDateStart.Text, out dtstart);
            if (dtstart.Year > 2500)
            {
                dtstart = dtstart.AddYears(-543);
            }
            else if (dtstart.Year < 2000)
            {
                dtstart = dtstart.AddYears(543);
            }
            datestart = dtstart.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            pid1 = dtstart.ToString("yyMMdd", new CultureInfo("en-US"));
            pid1 += ptt.MNC_HN_NO;

            pttsc.patient_smartcard_id = "";
            pttsc.prefixname = ptt.MNC_PFIX_CDT;
            pttsc.first_name = ptt.MNC_FNAME_T;
            pttsc.middle_name = "";
            pttsc.last_name = ptt.MNC_LNAME_T;
            pttsc.first_name_e = ptt.MNC_FNAME_E;
            pttsc.middle_name_e = "";
            pttsc.last_name_e = ptt.MNC_LNAME_E;
            pttsc.pid = !ptt.idcard.Equals("") ? ptt.idcard : pid1;
            pttsc.dob = ptt.patient_birthday;
            pttsc.home_no = ptt.MNC_CUR_ADD;
            pttsc.moo = ptt.MNC_CUR_MOO;
            pttsc.trok = "";
            pttsc.soi = ptt.MNC_CUR_SOI;
            pttsc.road = ptt.MNC_CUR_ROAD;
            pttsc.district_name = ptt.MNC_CUR_TUM;
            pttsc.amphur_name = ptt.MNC_CUR_AMP;
            pttsc.province_name = ptt.MNC_CUR_CHW;
            pttsc.date_order = datestart;
            pttsc.status_send = "";
            pttsc.doc = txtPaidType.Text.Trim();
            pttsc.hn = ptt.MNC_HN_NO;
            pttsc.hn_year = ptt.MNC_HN_YR;
            pttsc.mobile = ptt.MNC_CUR_TEL;
            pttsc.prefixname_e = ptt.MNC_PFIX_CDE;
            pttsc.attach_note = ptt.MNC_ATT_NOTE;
            pttsc.MNC_OCC_CD = ptt.MNC_OCC_CD;
            pttsc.MNC_EDU_CD = ptt.MNC_EDU_CD;
            pttsc.MNC_NAT_CD = ptt.MNC_NAT_CD;
            pttsc.MNC_REL_CD = ptt.MNC_REL_CD;
            pttsc.MNC_NATI_CD = ptt.MNC_NATI_CD;
            //pttsc.attach_note = ptt.MNC_ATT_NOTE;
            return pttsc;
        }
        private void CboDocGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String doc = "", datestart="", brancdid="";
            new LogWriter("d", "FrmReceptionCovidSend CboDocGrp_SelectedIndexChanged txtDateStart.Text " + txtDateStart.Text);
            DateTime dtstart = new DateTime();
            dtstart = DateTime.Now;
            DateTime.TryParse(txtDateStart.Text, out dtstart);
            new LogWriter("d", "FrmReceptionCovidSend CboDocGrp_SelectedIndexChanged dtstart1 " + dtstart);
            if (bc.iniC.windows.Equals("windosxp"))
            {
                if (dtstart.Year > 2500)
                {
                    new LogWriter("d", "FrmReceptionCovidSend CboDocGrp_SelectedIndexChanged if (dtstart.Year > 2500) ");
                    dtstart = dtstart.AddYears(-543);
                }
                else if (dtstart.Year < 2000)
                {
                    new LogWriter("d", "FrmReceptionCovidSend CboDocGrp_SelectedIndexChanged if (dtstart.Year < 2500) ");
                    dtstart = dtstart.AddYears(543);
                }
            }
            brancdid = bc.iniC.branchId.Replace("00", "");
            new LogWriter("d", "FrmReceptionCovidSend CboDocGrp_SelectedIndexChanged dtstart2 " + dtstart);
            datestart = dtstart.ToString("yyyyMMdd", new CultureInfo("en-US"));
            new LogWriter("d", "FrmReceptionCovidSend CboDocGrp_SelectedIndexChanged datestart " + datestart);
            doc = brancdid + datestart + (cboDocGrp.SelectedItem != null ? ((ComboBoxItem)cboDocGrp.SelectedItem).Value : "");
            txtPaidType.Value = doc;
            setGrfOPBKKMainCHRGITEM();
            txtHn.Focus();
        }

        private void initCompoment()
        {
            int gapLine = 25, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 80, xCol3 = 330, xCol4 = 640, xCol5 = 950;
            Size size = new Size();

            tcMain = new C1DockingTab();
            tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMain.Location = new System.Drawing.Point(0, 266);
            tcMain.Name = "tcMain";
            tcMain.Size = new System.Drawing.Size(669, 200);
            tcMain.TabIndex = 0;
            tcMain.TabsSpacing = 5;

            tabImportDf = new C1DockingTabPage();
            tabImportDf.Dock = System.Windows.Forms.DockStyle.Fill;
            tabImportDf.Name = "tabImportDf";
            tabImportDf.Text = "Import Item DF";

            grfSelect = new C1FlexGrid();
            grfSelect.Font = fEdit;
            grfSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            grfSelect.Location = new System.Drawing.Point(0, 0);
            grfSelect.Rows.Count = 1;

            lbDateStart = new Label();
            txtDateStart = new C1DateEdit();
            btnDeleteAll = new C1Button();
            cboDocGrp = new C1ComboBox();
            txtPaidType = new C1TextBox();
            lbHn = new Label();
            txtHn = new C1TextBox();
            btnPrint = new C1Button();
            btnGet = new C1Button();

            bc.setControlLabel(ref lbDateStart, fEdit, "วันที่เริ่มต้น :", "lbDateStart", gapX, gapY);
            size = bc.MeasureString(lbDateStart);
            bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);


            bc.setControlC1ComboBox(ref cboDocGrp, "cboDocGrp",80, txtDateStart.Location.X + txtDateStart.Width + 25, gapY);
            cboDocGrp.Font = fEdit;
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "รอบ 1";
            cboDocGrp.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "รอบ 2";
            cboDocGrp.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "รอบ 3";
            cboDocGrp.Items.Add(item);
            //item = new ComboBoxItem();
            //item.Value = "3";
            //item.Text = "รอบ 3";
            //cboDocGrp.Items.Add(item);

            bc.setControlC1TextBox(ref txtPaidType, fEdit, "txtPaidType", 140, cboDocGrp.Location.X + cboDocGrp.Width + 25, gapY);

            bc.setControlLabel(ref lbHn, fEdit, "search :", "lbHn", txtPaidType.Location.X + txtPaidType.Width + 55, gapY);
            size = bc.MeasureString(lbHn);
            bc.setControlC1TextBox(ref txtHn, fEdit, "txtHn", 140, lbHn.Location.X + lbHn.Width + 10, gapY);


            bc.setControlC1Button(ref btnDeleteAll, fEdit, "Clear Data", "btnDeleteAll", txtHn.Location.X + txtHn.Width + 20, gapY);
            btnDeleteAll.Width = 100;
            btnDeleteAll.Height = btnDeleteAll.Height + 10;

            bc.setControlC1Button(ref btnPrint, fEdit, "Print", "btnPrint", btnDeleteAll.Location.X + btnDeleteAll.Width + 20, gapY);
            btnPrint.Width = 90;
            btnPrint.Height = btnPrint.Height + 10;

            bc.setControlC1Button(ref btnGet, fEdit, "get", "btnGet", btnPrint.Location.X + btnPrint.Width + 20, gapY);
            btnGet.Width = 90;
            btnGet.Height = btnGet.Height + 10;

            tabImportDf.Controls.Add(grfSelect);
            theme1.SetTheme(grfSelect, "Office2010Red");

            tabImportDf.Controls.Add(lbDateStart);
            tabImportDf.Controls.Add(txtDateStart);
            tabImportDf.Controls.Add(cboDocGrp);
            tabImportDf.Controls.Add(btnDeleteAll);
            tabImportDf.Controls.Add(btnPrint);
            tabImportDf.Controls.Add(txtPaidType);
            tabImportDf.Controls.Add(lbHn);
            tabImportDf.Controls.Add(txtHn);
            tabImportDf.Controls.Add(btnGet);

            tcMain.Controls.Add(tabImportDf);
            this.Controls.Add(tcMain);
        }
        private void setControl()
        {
            txtDateStart.Value = DateTime.Now;
        }
        private void setGrfOPBKKMainCHRGITEM()
        {
            pageLoad = true;
            DataTable dt = new DataTable();

            dt = bc.bcDB.pttscDB.SelectByDoc(txtPaidType.Text.Trim());
            grfSelect.Rows.Count = 1;
            grfSelect.Cols.Count = 8;
            grfSelect.Rows.Count = dt.Rows.Count + 1;
            grfSelect.Cols[colHn].Caption = "HN";
            grfSelect.Cols[colFullName].Caption = "Full Name";
            grfSelect.Cols[colMobile].Caption = "Mobile ";
            grfSelect.Cols[colDoc].Caption = "DOC ";
            grfSelect.Cols[colattachnote].Caption = "attach note";
            
            grfSelect.Cols[colHn].Width = 120;
            grfSelect.Cols[colFullName].Width = 350;
            grfSelect.Cols[colMobile].Width = 150;
            grfSelect.Cols[colDoc].Width = 130;
            grfSelect.Cols[colattachnote].Width = 350;
            
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                grfSelect[i, colHn] = row1["hn"].ToString();
                grfSelect[i, colFullName] = row1["first_name"].ToString() + " " + row1["last_name"].ToString();
                grfSelect[i, colMobile] = row1["mobile"].ToString();
                grfSelect[i, colDoc] = row1["doc"].ToString();
                grfSelect[i, colattachnote] = row1["attach_note"].ToString();
                grfSelect[i, colID] = row1["patient_smartcard_id"].ToString();
                grfSelect[i, colPID] = row1["pid"].ToString();

                grfSelect[i, 0] = i;
                if (i % 2 == 0)
                    grfSelect.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            grfSelect.Cols[colID].Visible = false;
            grfSelect.Cols[colHn].AllowEditing = false;
            grfSelect.Cols[colFullName].AllowEditing = false;
            grfSelect.Cols[colMobile].AllowEditing = false;
            grfSelect.Cols[colDoc].AllowEditing = false;
            grfSelect.Cols[colPID].AllowEditing = false;
            grfSelect.Cols[colattachnote].AllowEditing = false;
            
            pageLoad = false;
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            int gapLine = 40, gapX = 40, gapY = 20, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            Size size = new Size();

            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", doccd = "", docno = "", anyr = "", vn1 = "", pathFolder = "", datetick = "", filename = "", docyr = "", amt2 = "";
            String doc = "", datestart = "";
            //DataTable dt = new DataTable();
            DataTable dt = new DataTable();

            dt = bc.bcDB.pttscDB.SelectByDoc(txtPaidType.Text.Trim());

            DateTime dtstart = new DateTime();
            DateTime.TryParse(txtDateStart.Text, out dtstart);
            if (bc.iniC.windows.Equals("windosxp"))
            {
                if (dtstart.Year > 2500)
                {
                    dtstart = dtstart.AddYears(-543);
                }
                else if (dtstart.Year < 2000)
                {
                    dtstart = dtstart.AddYears(543);
                }
            }
            datestart = dtstart.ToString("dd-MM-yyyy", new CultureInfo("en-US"));

            //throw new NotImplementedException();
            C1PdfDocument pdf = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            StringFormat _sfRight, _sfRightCenter;
            //Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            //RectangleF rc = GetPageRect(pdf);
            Font titleFont = new Font(bc.iniC.pdfFontName, 18, FontStyle.Bold);
            Font hdrFont = new Font(bc.iniC.pdfFontName, 14, FontStyle.Regular);
            Font hdrFontB = new Font(bc.iniC.pdfFontName, 16, FontStyle.Bold);
            Font ftrFont = new Font(bc.iniC.pdfFontName, 8);
            Font txtFont = new Font(bc.iniC.pdfFontName, 10, FontStyle.Regular);
            //pdf.Clear();
            pdf.FontType = FontTypeEnum.Embedded;

            //newPagePDFSummaryBorder(pdf, dt, titleFont, hdrFont, ftrFont, txtFont, false);

            //get page rectangle, discount margins
            RectangleF rcPage = pdf.PageRectangle;
            rcPage = RectangleF.Empty;
            rcPage.Inflate(-72, -92);
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 610;
            rcPage.Height = gapLine;

            gapY += gapLine;
            gapY += gapLine;

            String space2 = "    ", space3 = "      ", space4 = "        ", space5 = "          ", space = "";
            int rowline = 0, i = 0;
            rowline = 205;

            String txt = "ใบนำส่งรายชื่อ ผู้มาตรวจ COVID " + (bc.iniC.branchId.Equals("001") ? "บางนา 1" : bc.iniC.branchId.Equals("002") ? "บางนา 2" : "ไม่ระบุ");

            pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);

            gapY += gapLine;
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 610;
            txt = " ประจำวันที่ " + datestart + " จำนวนผู้มาตรวจ " + dt.Rows.Count;
            pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);

            gapY += gapLine;
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 610;
            txt = " เลขที่ " + txtPaidType.Text.Trim();
            pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);

            datetick = DateTime.Now.Ticks.ToString();
            if (!Directory.Exists("report"))
            {
                Directory.CreateDirectory("report");
            }
            filename = "report\\" + datetick + ".pdf";
            pdf.Save(filename);
            pdf.Clear();
            pdf.Dispose();

            if (File.Exists(filename))
            {
                //bool isExists = System.IO.File.Exists(filename);
                //if (isExists)
                System.Diagnostics.Process.Start(filename);
            }
        }
        private void newPagePDFSummaryBorder(C1PdfDocument pdf, DataTable dt, Font titleFont, Font hdrFont, Font ftrFont, Font txtFont, Boolean loopfor)
        {
            newPagePDFBorder(pdf, loopfor);
            //newPagePDFHeaderPage(pdf, dt, titleFont, hdrFont, ftrFont, txtFont);
        }
        private void newPagePDFBorder(C1PdfDocument pdf, Boolean loopfor)
        {
            int gapLine = 20, gapX = 40, gapY = 135, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            Size size = new Size();
            //if(pdf.Pages.Count>1) pdf.NewPage();
            //if ((loopfor) && (pdf.CurrentPage != 0)) pdf.NewPage();
            if ((loopfor)) pdf.NewPage();
            RectangleF rcHdr = new RectangleF();
            rcHdr.Width = 542;
            rcHdr.Height = 500;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            //rcHdr.Location
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางใหญ่

            pdf.DrawLine(Pens.Black, rcHdr.X, rcHdr.Y + 50, rcHdr.X + rcHdr.Width, rcHdr.Y + 50);
            pdf.DrawLine(Pens.Black, rcHdr.X, rcHdr.Y + 80, rcHdr.X + rcHdr.Width, rcHdr.Y + 80);
            pdf.DrawLine(Pens.Black, rcHdr.X + rcHdr.Width - 90, rcHdr.Y + 50, rcHdr.X + rcHdr.Width - 90, rcHdr.Y + rcHdr.Height);     //เส้นตั้ง จำนวนเงิน
            pdf.DrawLine(Pens.Black, rcHdr.X + 260, rcHdr.Y, rcHdr.X + 260, rcHdr.Y + 50);      //  เส้นตั้ง ชื่อ - นามสกุล
            float xxx = rcHdr.X + rcHdr.Width - 90;
            float yyy = rcHdr.Y + rcHdr.Height;

            gapY += 510;
            rcHdr.Width = rcHdr.X + rcHdr.Width - 230;
            rcHdr.Height = 30;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางจำนวนเงิน ตัวอักษร

            pdf.DrawLine(Pens.Black, rcHdr.X + 452, rcHdr.Y + 15, rcHdr.X + 542, rcHdr.Y + 15);       //เส้นแบ่ง รวมเงิน
            pdf.DrawLine(Pens.Black, rcHdr.X + 452, rcHdr.Y + 40, rcHdr.X + 542, rcHdr.Y + 40);       //เส้นแบ่ง รวมเงิน

            rcHdr.Width = 500 - xxx + gapX + 42;
            rcHdr.Height = 75;
            rcHdr.X = xxx;
            rcHdr.Y = yyy;
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางรวมเงิน ด้านล่าง


        }
        private void FrmReceptionCovidSend_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;

            grfSelect.Size = new Size(scrW - 20, scrH - btnDeleteAll.Location.Y - 140);
            grfSelect.Location = new Point(5, btnDeleteAll.Location.Y + 40);

            this.Text = "Last Update 2021-07-08 pid empty";
        }
    }
}
