using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1Ribbon;
using C1.Win.C1Themes;
using C1.Win.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class UCAppointment : UserControl
    {
        BangnaControl BC;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        String PRENO = "", VSDATE = "", HN = "", DTRCODE = "", StatusFormUs = "", TXT = "", STATUSFORMUS="";
        AutoCompleteStringCollection acmTime, ACMDTR, autoApm;
        C1FlexGrid grfApmOrder;
        C1ThemeController theme1;
        Patient PTT;
        PatientT07 APM;
        Visit VS;
        Boolean isLoaded = false;
        List<String> lApm;
        int colgrfOrderCode = 1, colgrfOrderName = 2, colgrfOrderStatus = 3, colgrfOrderQty = 4, colgrfOrderID = 5, colgrfOrderReqNO = 6, colgrfOrdFlagSave = 7;
        string[] listTIME = { "07:00-08:00","08:00-09:00","08:00-15:00","09:00-10:00","09:00-15:00","10:00-11:00","10:00-15:00","11:00-12:00","12:00-13:00","13:00-14:00","14:00-15:00","15:00-16:00","16:00-17:00","17:00-18:00"
                ,"18:00-19:00","19:00-20:00","20:00-21:00"
        };
        RibbonLabel lfSbMessage;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern IntPtr GetFocus();
        public UCAppointment(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt, Visit vs, String statusformus,ref RibbonLabel lfSbMessage)
        {
            this.PTT = ptt;
            this.VS = vs;
            this.BC = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.STATUSFORMUS = statusformus;
            this.lfSbMessage = lfSbMessage;
            InitializeComponent();
            initConfig();
            setControl(dtrcode, hn, vsdate, preno, ptt, statusformus);
        }
        private void initConfig()
        {
            isLoaded = true;
            initFont();
            APM = new PatientT07();
            theme1 = new C1ThemeController();
            BC = new BangnaControl();
            acmTime = new AutoCompleteStringCollection();
            acmTime.AddRange(listTIME);
            ACMDTR = new AutoCompleteStringCollection();
            autoApm = new AutoCompleteStringCollection();
            BC.bcDB.pttDB.setCboDeptOPDNew(cboApmDept, "");
            autoApm = BC.bcDB.pm13DB.getlApm();
            StatusFormUs = "";
            txtPttApmDate.Value = DateTime.Now;
            chlApmList.Hide();
            foreach (String txt in autoApm)
            {
                chlApmList.Items.Add(txt);
            }
            initGrfOrder(ref grfApmOrder, ref pnApmOrder, "grfApmOrder");
            lbApm7Week.Click += LbApm7Week_Click;
            lbApm14Week.Click += LbApm14Week_Click;
            lbApm1Month.Click += LbApm1Month_Click;
            txtApmPlusDay.KeyUp += TxtApmPlusDay_KeyUp;
            txtApmDtr.KeyUp += TxtApmDtr_KeyUp;
            btnApmNew.Click += BtnApmNew_Click;
            btnApmOrder.Click += BtnApmOrder_Click;
            btnApmSave.Click += BtnApmSave_Click;
            btnApmPrint.Click += BtnApmPrint_Click;
            lbApmList.Click += LbApmList_Click;
            isLoaded = false;
        }

        private void LbApmList_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chlApmList.Visible == true)
            {
                chlApmList.Hide();
                String txt = "";
                foreach (var chk in chlApmList.Items)
                {
                    if (chk.Selected)
                    {
                        txt += chk.Value.ToString() + "\r\n";
                    }
                }
                txtApmList.Value += "\r\n" + txt;
                txtApmList.ScrollBars = ScrollBars.Both;
            }
            else if (chlApmList.Visible == false)
            {
                chlApmList.Top = cboApmDept.Top;
                chlApmList.Left = cboApmDept.Left;
                chlApmList.BringToFront();
                chlApmList.Show();
                foreach (var chk in chlApmList.Items)
                {
                    if (chk.Selected)
                    {
                        chk.Selected = false;
                    }
                }
            }
        }
        private void initFont()
        {
            fEdit = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new System.Drawing.Font(BC.iniC.grdViewFontName,BC.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 5, FontStyle.Regular);
            famt5BL = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 5, FontStyle.Underline);
            famt5B = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 5, FontStyle.Bold);
            famt2 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Regular);
            famt2B = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Bold);
            famt2BL = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Underline);
            famt4B = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 4, FontStyle.Bold);

            famt7 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 7, FontStyle.Regular);
            famt7B = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize, FontStyle.Regular);
            fque = new System.Drawing.Font(BC.iniC.queFontName, BC.queFontSize + 3, FontStyle.Bold);
            fqueB = new System.Drawing.Font(BC.iniC.queFontName, BC.queFontSize + 7, FontStyle.Bold);
            fEditS = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 1, FontStyle.Regular);

            fPDF = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize, FontStyle.Regular);
            fPDFs2 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 2, FontStyle.Regular);
            fPDFl2 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 6, FontStyle.Regular);
            fPDFs8 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 8, FontStyle.Regular);
            //fStaffN = new System.Drawing.Font(BC.iniC.staffNoteFontName, BC.staffNoteFontSize, FontStyle.Regular);

        }
        private void BtnApmPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (BC.cStf == null)
            {
                FrmPasswordConfirm frm = new FrmPasswordConfirm(BC);
                frm.ShowDialog();
                frm.Dispose();
                if (BC.USERCONFIRMID.Length <= 0)
                {
                    lfSbMessage.Text = "Password ไม่ถูกต้อง";
                    return;
                }
            }
            printAppointment();
        }

        private void BtnApmSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmPasswordConfirm frm = new FrmPasswordConfirm(BC);
            frm.ShowDialog();
            frm.Dispose();
            if (BC.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            PatientT07 apm = getApm();
            if (apm != null)
            {
                string re = BC.bcDB.pt07DB.insertPatientT07(apm);
                if (long.TryParse(re, out long chk))
                {
                    txtApmNO.Value = apm.MNC_DOC_NO.Length > 3 ? txtApmNO.Text.Trim() : re;
                    txtApmDocYear.Value = apm.MNC_DOC_YR;
                    if (grfApmOrder.Rows.Count > 1)
                    {
                        foreach (Row rowa in grfApmOrder.Rows)
                        {
                            String ordercode = rowa[colgrfOrderCode].ToString(); if (ordercode.Equals("code")) continue;
                            String ordername = rowa[colgrfOrderName].ToString();
                            String flag = rowa[colgrfOrderStatus].ToString();//O hotcharge, X xray, L lab
                            flag = flag.Equals("lab") ? "L" : flag.Equals("xray") ? "X" : flag.Equals("procedure") ? "O" : flag;

                            String re1 = BC.bcDB.pt07DB.insertPatientT073(txtApmNO.Text.Trim(), txtApmDocYear.Text.Trim(), ordercode, "", flag);
                            if (!long.TryParse(re1, out long chk1))
                            {
                                new LogWriter("e", "FrmOPD BtnApmSave_Click " + re1);
                                BC.bcDB.insertLogPage(BC.userId, this.Name, "BtnApmSave_Click save  ", re1);
                                lfSbMessage.Text = re1;
                            }
                        }
                    }
                    lfSbMessage.Text = txtApmNO.Text.Length > 0 ? "update appointment OK" : "insert appointment OK";
                    //setGrfPttApm();
                    if (txtApmNO.Text.Length <= 0)
                    {
                        txtApmNO.Value = re;//insert ถ้าแก้ไข  นัด ไม่ต้องใส่ค่า
                    }
                }
                else
                {
                    new LogWriter("e", "FrmOPD BtnApmSave_Click " + re);
                    BC.bcDB.insertLogPage(BC.userId, this.Name, "BtnApmSave_Click save  ", re);
                    lfSbMessage.Text = re;
                }
            }
            else
            {
                lfSbMessage.Text = "ไม่พบ visit";
            }
        }

        private void BtnApmOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmItemSearch frm = new FrmItemSearch(BC);
            frm.ShowDialog();
            if ((BC.items != null) && (BC.items.Count > 0))
            {
                pnApmOrder.Show();
                foreach (Item item in BC.items)
                {
                    Row rowa = grfApmOrder.Rows.Add();
                    rowa[colgrfOrderCode] = item.code;
                    rowa[colgrfOrderName] = item.name;
                    rowa[colgrfOrderStatus] = item.flag;
                    rowa[colgrfOrderQty] = "1";
                    rowa[0] = grfApmOrder.Rows.Count - 1;
                }
            }
        }

        private void BtnApmNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlTabApm(true);
        }

        private void TxtApmDtr_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //lbApmDtrName.Text = bc.selectDoctorName(txtApmDtr.Text.Trim());
                PatientM26 dtr = new PatientM26();
                dtr = BC.bcDB.pm26DB.selectByPk(txtApmDtr.Text.Trim());
                lbApmDtrName.Text = dtr.dtrname;
                lbDtrApmCnt.Text = dtr.MNC_APP_NO;
            }
        }

        private void TxtApmPlusDay_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            calApmDate();
        }

        private void LbApm1Month_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "28";
            calApmDate();
        }

        private void LbApm14Week_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "14";
            calApmDate();
        }

        private void LbApm7Week_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "7";
            calApmDate();
        }
        private void calApmDate()
        {
            if (txtApmPlusDay.Text.Length <= 0) return;
            DateTime dtcal = DateTime.Now;
            if (dtcal.Year > 2500)
            {
                dtcal.AddYears(-543);
            }
            dtcal = dtcal.AddDays(int.Parse(txtApmPlusDay.Text));
            txtPttApmDate.Value = dtcal.Year.ToString() + "-" + dtcal.ToString("MM-dd");
        }
        private void clearControlTabApm(Boolean new1)
        {
            txtPttApmDate.Value = DateTime.Now;
            txtApmTime.Value = "";
            BC.setC1Combo(cboApmDept, "");
            txtApmDsc.Value = "";
            txtApmRemark.Value = "";
            txtApmDtr.Value = "";
            lbApmDtrName.Text = "";
            txtApmTel.Value = "";
            txtApmNO.Value = "";
            txtApmDocYear.Value = "";
            txtApmList.Value = "";
            grfApmOrder.Rows.Count = 1;
            if (!new1)
            {
                //grfPttApm.Rows.Count = 1;
            }
        }
        private PatientT07 getApm()
        {
            if (VSDATE.Length <= 0) return null;
            if (PRENO.Length <= 0) return null;
            if (HN.Length <= 0) return null;
            if (VS == null) return null;
            //String deptcode = cboApmDept.Text;
            //String stationname = bc.bcDB.pttDB.selectDeptOPD(deptcode);
            DateTime.TryParse(txtPttApmDate.Text, out DateTime apmdate);
            if (apmdate == null) return null;
            if (apmdate.Year < 1900) apmdate = apmdate.AddYears(543);
            PatientT07 apm = new PatientT07();
            apm.MNC_HN_NO = HN;
            apm.MNC_HN_YR = VS.MNC_HN_YR;
            apm.MNC_DATE = VSDATE;
            apm.MNC_PRE_NO = PRENO;
            apm.MNC_DOC_YR = txtApmNO.Text.Trim().Length == 0 ? (DateTime.Now.Year + 543).ToString() : txtApmDocYear.Text.Trim();
            apm.MNC_DOC_NO = txtApmNO.Text.Trim();
            apm.MNC_TIME = VS.VisitTime;
            apm.MNC_APP_DAT = apmdate.Year + "-" + apmdate.ToString("MM-dd");
            apm.MNC_APP_TIM = BC.bcDB.pt07DB.setAppTime(txtApmTime.Text);
            //apm.MNC_APP_DSC = txtApmDsc.Text.Trim();
            apm.MNC_APP_DSC = txtApmList.Text.Trim();
            apm.MNC_APP_STS = "";
            apm.MNC_APP_TEL = txtApmTel.Text;
            apm.MNC_DOT_CD = txtApmDtr.Text.Trim();
            apm.apm_time = txtApmTime.Text;
            apm.MNC_SEC_NO = BC.iniC.station;
            apm.MNC_DEP_NO = VS.DeptCode;
            apm.MNC_SECR_NO = cboApmDept.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept.SelectedItem).Value;
            apm.MNC_DEPR_NO = BC.bcDB.pm32DB.getDeptNoOPD(apm.MNC_SECR_NO);
            apm.apm_time = txtApmTime.Text.Trim();
            apm.MNC_NAME = cboApmDept.Text;
            //apm.MNC_SEC_NO = cboApmDept.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept.SelectedItem).Value;
            return apm;
        }
        private void printAppointment()
        {
            if (txtApmNO.Text.Trim().Length <= 0)
            {
                lfSbMessage.Text = "ไม่พบ เลขที่นัด";
                return;
            }
            APM = BC.bcDB.pt07DB.selectAppointment(txtApmDocYear.Text.Trim(), txtApmNO.Text.Trim());
            if (APM.MNC_APP_NO.Length <= 0)
            {
                lfSbMessage.Text = "ค้นหา นัด ไม่พบ";
                return;
            }
            PrintDocument pdStaffNote = new PrintDocument();
            pdStaffNote.PrinterSettings.PrinterName = BC.iniC.printerA5;
            pdStaffNote.DefaultPageSettings.PaperSize = new PaperSize("A4", 826, 1169);
            pdStaffNote.DefaultPageSettings.Landscape = false;
            pdStaffNote.PrintPage += Document_PrintPageAppointment;

            pdStaffNote.Print();
            pdStaffNote.Dispose();

            lfSbMessage.Text = "พิมพ์ นัด OK";
        }
        private void Document_PrintPageAppointment(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, col1 = 0, col2 = 50, col21 = 180, col3 = 250, col4 = 450, col41 = 560, col5 = 300, line = 25;
            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            StringFormat flagsR = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            flags.Alignment = StringAlignment.Near;
            flagsR.Alignment = StringAlignment.Far;
            Size textSize = TextRenderer.MeasureText("", fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            float centerpage = e.PageSettings.PaperSize.Width / 2;
            //yPos = yPos + line;//ขึ้นบันทัดใหม่        ชื่อโรงพยาบาล ต่ำไป
            col2 = 20;
            textSize = TextRenderer.MeasureText(BC.iniC.hostname, famt2, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(BC.iniC.hostname, famt2B, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText(BC.iniC.hostnamee, famt2, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(BC.iniC.hostnamee, famt2B, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("print date " + DateTime.Now, fEditS, Brushes.Black, col41 + 220, yPos, flagsR);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText("ใบนัดพบแพทย์ Appointment Note", famt2B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("ใบนัดพบแพทย์ Appointment Note", famt4B, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("เลขที่: " + APM.MNC_DOC_YR.Substring(APM.MNC_DOC_YR.Length - 2) + "-" + APM.MNC_DOC_NO, famt2, Brushes.Black, col41 + 220, yPos, flagsR);
            //e.Graphics.DrawString(APM.MNC_DOC_YR.Substring(APM.MNC_DOC_YR.Length-2) + "-"+APM.MNC_DOC_NO, famt5, Brushes.Black, col41+120, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("HN:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(HN, famt2B, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("แผนกที่นัด:", famt2B, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(BC.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SEC_NO), famt2B, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Name/ชื่อผู้ป่วย:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(VS.PatientName, famt2B, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("วันที่พิมพ์:", famt2B, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(DateTime.Now.ToString(), famt2B, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Age/อายุ:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(PTT.AgeStringTHlong(), famt2B, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("สิทธิ์การรักษา:", famt2B, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(VS.PaidName, famt2B, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Date/นัดมาวันที่:", famt2B, Brushes.Black, col2, yPos, flags);
            if (APM.nationcode.Equals("01") || APM.nationcode.Equals("TH"))
            {
                e.Graphics.DrawString(BC.datetoShowTHMMM(APM.MNC_APP_DAT) + " " + APM.apm_time, famt2B, Brushes.Black, col21, yPos, flags);
            }
            else
            {
                e.Graphics.DrawString(BC.datetoShowEN(APM.MNC_APP_DAT) + " " + APM.apm_time, famt2B, Brushes.Black, col21, yPos, flags);
            }
            e.Graphics.DrawLine(blackPen, col21, yPos + line, col21 + 160, yPos + line);
            //e.Graphics.DrawString(bc.datetoShow(APM.MNC_APP_DAT) +" "+APM.apm_time, famt5, Brushes.Black, col21, yPos, flags);
            //e.Graphics.DrawString("Dept/นัดตรวจที่แผนก:", famt5B, Brushes.Black, col4, yPos, flags);
            //e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SECR_NO), famt5, Brushes.Black, col41+60, yPos, flags);
            e.Graphics.DrawString("สิ่งที่ต้องเตรียม:", famt2B, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(APM.MNC_REM_MEMO, famt2, Brushes.Black, col41, yPos, flags);
            float ypos1 = yPos;
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Doctor/แพทย์:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.doctor_name, famt2B, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            //e.Graphics.DrawString("สิ่งที่ต้องเตรียม:", famt5B, Brushes.Black, col2, yPos, flags);
            //e.Graphics.DrawString(APM.MNC_REM_MEMO, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("Dept/นัดตรวจที่แผนก:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(BC.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SECR_NO), famt2B, Brushes.Black, col21, yPos, flags);
            //e.Graphics.DrawString("รายการตรวจ: ", famt2B, Brushes.Black, col4, yPos, flags);
            String txt = "";
            if (grfApmOrder.Rows.Count > 1)
            {
                foreach (Row rowa in grfApmOrder.Rows)
                {
                    String name = "", code = "";
                    name = rowa[colgrfOrderName].ToString();
                    code = rowa[colgrfOrderCode].ToString();
                    if (name.Equals("name")) continue;
                    txt += code + " " + name + "\r\n";
                }
                if (txt.Length > 1)
                {
                    txt = txt.Substring(0, txt.Length - 1);
                }
                e.Graphics.DrawString(txt, famt1, Brushes.Black, col41, ypos1, flags);
            }

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("เพื่อ:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.MNC_APP_DSC, famt2B, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            //Staff stf = new Staff();
            //stf = bc.bcDB.stfDB.selectByPasswordConfirm1("1618");
            //bc.cStf = stf;

            e.Graphics.DrawString("เบอร์โทรติดต่อ:", famt2, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(BC.iniC.deptphone, famt2, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("ผู้บันทึก:", famt2, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawLine(blackPen, col4 + 60, yPos + 22, col4 + 270, yPos + 22);
            if (BC.cStf != null) { e.Graphics.DrawString(BC.cStf.fullname, famt2, Brushes.Black, col4 + 60, yPos - 3, flags); }

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("หมายเหตุ:", famt2, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString("เพื่อประโยชน์และความสะดวกของท่าน  กรุณามาให้ตรงตามวัน และเวลาที่แพทย์นัดทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("กรณีที่ไม่สามารถมาตรวจตามนัดได้  กรุณาโทรเพื่อแจ้งยกเลิกหรือเลื่อนนัดกับทางโรงพยาบาลทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            g.Dispose();
            Brush.Dispose();
            blackPen.Dispose();
        }
        private void initGrfOrder(ref C1FlexGrid grf, ref Panel pn, String grfname)
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Rows.Count = 1;
            grf.Cols.Count = 8;
            grf.Cols[colgrfOrderCode].Width = 100;
            grf.Cols[colgrfOrderName].Width = 400;
            grf.Cols[colgrfOrderQty].Width = 70;
            grf.Name = grfname;
            grf.ShowCursor = true;
            grf.Cols[colgrfOrderCode].Caption = "code";
            grf.Cols[colgrfOrderName].Caption = "name";
            grf.Cols[colgrfOrderQty].Caption = "qty";
            grf.Cols[colgrfOrderReqNO].Caption = "reqno";

            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grf.Cols[colgrfOrderCode].DataType = typeof(String);
            grf.Cols[colgrfOrderName].DataType = typeof(String);
            grf.Cols[colgrfOrderQty].DataType = typeof(String);

            grf.Cols[colgrfOrderCode].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[colgrfOrderName].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderQty].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderReqNO].TextAlign = TextAlignEnum.CenterCenter;

            grf.Cols[colgrfOrderCode].Visible = true;
            grf.Cols[colgrfOrderName].Visible = true;
            grf.Cols[colgrfOrderStatus].Visible = true;
            grf.Cols[colgrfOrderID].Visible = false;
            grf.Cols[colgrfOrdFlagSave].Visible = false;
            if (grfname.Equals("grfOrder"))
            {
                grf.Cols[colgrfOrderQty].Visible = true;
            }
            else
            {
                grf.Cols[colgrfOrderQty].Visible = false;
            }
            grf.Cols[colgrfOrderCode].AllowEditing = false;
            grf.Cols[colgrfOrderName].AllowEditing = false;
            grf.Cols[colgrfOrderReqNO].AllowEditing = false;
            grf.DoubleClick += GrfOrder_DoubleClick;
            grf.AllowSorting = AllowSortingEnum.None;
            pn.Controls.Add(grf);
            theme1.SetTheme(grf, BC.iniC.themeApp);
        }

        private void GrfOrder_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;

            if (((C1FlexGrid)sender).Name.Equals("grfApmOrder"))
            {
                String code = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderCode].ToString();
                String re = BC.bcDB.pt07DB.deleteOrderApm(txtApmDocYear.Text.Trim(), txtApmNO.Text.Trim(), code);
                ((C1FlexGrid)sender).Rows.Remove(((C1FlexGrid)sender).Row);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    {
                        // Add logic here if needed, or explicitly break/return
                        Control ctl = new Control();
                        ctl = GetFocusedControl();
                        if ((ctl is C1CheckList) && (ctl.Name.Equals(chlApmList.Name)))
                        {
                            chlApmList.Hide();
                        }
                        else
                        {
                            if (StatusFormUs.Equals("M"))
                            {
                                ParentForm.Close();
                            }
                            else
                            {
                                this.Hide();
                            }
                        }
                        break;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private Control GetFocusedControl()
        {
            Control focusedControl = null;
            // To get hold of the focused control:
            IntPtr focusedHandle = GetFocus();
            if (focusedHandle != IntPtr.Zero)
                // Note that if the focused Control is not a .Net control, then this will return null.
                focusedControl = Control.FromHandle(focusedHandle);
            return focusedControl;
        }
        private void setControlApm(String apmyear, String apmno)
        {
            PatientT07 apm = new PatientT07();
            apm = BC.bcDB.pt07DB.selectAppointment(apmyear, apmno);
            txtApmTime.Value = apm.apm_time;
            txtPttApmDate.Value = apm.MNC_APP_DAT;
            txtApmNO.Value = apm.MNC_DOC_NO;
            txtApmDocYear.Value = apm.MNC_DOC_YR;
            txtApmDtr.Value = apm.MNC_DOT_CD;
            //txtPttApmDate.Value = apm.MNC_DOT_CD;
            //cboApmDept.Value =  apm.MNC_SECR_NO;
            BC.setC1Combo(cboApmDept, apm.MNC_SECR_NO);
            txtApmDsc.Value = apm.MNC_APP_DSC;
            txtApmList.Value = apm.MNC_APP_DSC;
            txtApmTel.Value = apm.MNC_APP_TEL;
            txtApmRemark.Value = apm.MNC_DOT_CD;
            lbApmDtrName.Text = BC.selectDoctorName(apm.MNC_DOT_CD);
            DataTable dt = new DataTable();
            dt = BC.bcDB.pt07DB.selectAppointmentOrder(txtApmDocYear.Text, txtApmNO.Text);
            grfApmOrder.Rows.Count = 1;
            foreach (DataRow item in dt.Rows)
            {
                Row rowa = grfApmOrder.Rows.Add();
                String flag = "", name = "", code = "";
                code = item["MNC_OPR_CD"].ToString();
                flag = item["MNC_OPR_FLAG"].ToString();
                if (flag.Equals("O"))
                {
                    String chk = "";
                    chk = BC.bcDB.pm30DB.SelectNameByPk(code);
                    name = chk;
                }
                else if (flag.Equals("L"))
                {
                    LabM01 lab = new LabM01();
                    lab = BC.bcDB.labM01DB.SelectByPk(code);
                    name = lab.MNC_LB_DSC;
                }
                else if (flag.Equals("X"))
                {
                    XrayM01 xray = new XrayM01();
                    xray = BC.bcDB.xrayM01DB.SelectByPk(code);
                    name = xray.MNC_XR_DSC;
                }
                rowa[colgrfOrderCode] = item["MNC_OPR_CD"].ToString();
                rowa[colgrfOrderName] = name;
                rowa[colgrfOrderStatus] = flag;
                rowa[colgrfOrderQty] = "1";
                rowa[0] = grfApmOrder.Rows.Count - 1;
            }
            pnApmOrder.Visible = (grfApmOrder.Rows.Count > 1) ? true : false;
        }
        private void setControl(String dtrcode, String hn, String vsdate, String preno, Patient ptt, String statusformus)
        {
            DTRCODE = dtrcode;
            HN = hn;
            VSDATE = vsdate;
            PRENO = preno;
            PTT = ptt;
            StatusFormUs = statusformus;
        }
        private void UCAppointment_Load(object sender, EventArgs e)
        {

        }
    }
}
