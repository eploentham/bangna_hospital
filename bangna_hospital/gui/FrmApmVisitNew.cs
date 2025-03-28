using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
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
using Font = System.Drawing.Font;

namespace bangna_hospital.gui
{
    public partial class FrmApmVisitNew : Form
    {
        BangnaControl bc;
        Boolean pageLoad = false;
        PatientT07 APM;
        Label lbLoading;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, fque, fqueB, famt5, famt1;
        C1FlexGrid grfApm, grfPttApm, grfApmOrder;
        C1ThemeController theme1;
        Patient PTT = new Patient();
        AutoCompleteStringCollection autoApm;
        int colgrfOrderCode = 1, colgrfOrderName = 2, colgrfOrderStatus = 3, colgrfOrderQty = 4, colgrfOrderID = 5, colgrfOrderReqNO = 6, colgrfOrdFlagSave = 7;
        int colgrfPttApmVsDate = 1, colgrfPttApmApmDateShow = 2, colgrfPttApmApmTime = 3, colgrfPttApmHN = 4, colgrfPttApmPttName = 5, colgrfPttApmDeptR = 6, colgrfPttApmDeptMake = 7, colgrfPttApmNote = 8, colgrfPttApmOrder = 9, colgrfPttApmDocYear = 10, colgrfPttApmDocNo = 11, colgrfPttApmDtrname = 12, colgrfPttApmPhone = 13, colgrfPttApmPaidName = 14, colgrfPttApmRemarkCall = 15, colgrfPttApmStatusRemarkCall = 16, colgrfPttApmRemarkCallDate = 17, colgrfPttApmApmDate1 = 18;
        public FrmApmVisitNew(BangnaControl bc, PatientT07 apm)
        {
            this.bc = bc;
            this.APM = apm;
            InitializeComponent(); 
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            famt1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            lbLoading.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lbLoading);

            theme1 = new C1ThemeController();
            autoApm = new AutoCompleteStringCollection();
            autoApm = bc.bcDB.pm13DB.getlApm();

            bc.bcDB.pttDB.setCboDeptOPDNew(cboVsDept, "");     //ส่งตัวไป
            bc.bcDB.pttDB.setCboDeptOPDNew(cboAppViewDept, "");     //ส่งตัวไป
            bc.bcDB.pttDB.setCboDeptOPDNew(cboApmDept, "");
            initGrfAppNew();
            if ((APM.MNC_DOC_NO.Length > 0) && (APM.apm_cnt_inday.Equals("1")))            {                setControl();                c1DockingTab1.SelectedTab = tabAppVisit;            }
            else            {                setGrfApm();                c1DockingTab1.SelectedTab = tabAppView;            }
            btnVsSave.Click += BtnVsSave_Click;
            txtVsUser.KeyUp += TxtVsUser_KeyUp;
            chkNewVisit.Click += ChkNewVisit_Click;
            btnAppViewVisitNew.Click += BtnAppViewVisitNew_Click;
            txtAppViewUser.KeyUp += TxtAppViewUser_KeyUp;
            txtApmDoctorId.KeyUp += TxtApmDoctorId_KeyUp;
            txtApmDoctorId.KeyPress += TxtApmDoctorId_KeyPress;

            lbApm7Week.Click += LbApm7Week_Click;
            lbApm14Week.Click += LbApm14Week_Click;
            lbApm1Month.Click += LbApm1Month_Click;
            txtApmPlusDay.KeyPress += TxtApmPlusDay_KeyPress;
            txtApmPlusDay.KeyUp += TxtApmPlusDay_KeyUp;
            txtPttApmDate.DropDownClosed += TxtPttApmDate_DropDownClosed;
            btnApmOrder.Click += BtnApmOrder_Click;
            btnApmNew.Click += BtnApmNew_Click;
            btnApmSave.Click += BtnApmSave_Click;
            btnApmPrint.Click += BtnApmPrint_Click;

            initGrfPttApm(ref grfPttApm, ref pnPttApm, "grfPttApm");
            chlApmList.Hide();
            foreach (String txt in autoApm)
            {
                chlApmList.Items.Add(txt);
            }
        }

        private void BtnApmPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printAppointment();
        }
        private void printAppointment()
        {
            if (txtApmNO.Text.Trim().Length <= 0)
            {
                lfSbMessage.Text = "ไม่พบ เลขที่นัด";
                return;
            }
            APM = bc.bcDB.pt07DB.selectAppointment(txtApmDocYear.Text.Trim(), txtApmNO.Text.Trim());
            if (APM.MNC_APP_NO.Length <= 0)
            {
                lfSbMessage.Text = "ค้นหา นัด ไม่พบ";
                return;
            }
            PrintDocument pdStaffNote = new PrintDocument();
            pdStaffNote.PrinterSettings.PrinterName = bc.iniC.printerA5;
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
            Size textSize = TextRenderer.MeasureText("", fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            float centerpage = e.PageSettings.PaperSize.Width / 2;
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText(bc.iniC.hostname, famt5, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(bc.iniC.hostname, famt5, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText(bc.iniC.hostnamee, famt5, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(bc.iniC.hostnamee, famt5, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText("ใบนัดพบแพทย์ Appointment Note", famt5, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("ใบนัดพบแพทย์ Appointment Note", famt5, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("เลขที่:", famt5, Brushes.Black, col41 + 70, yPos, flags);
            e.Graphics.DrawString(APM.MNC_DOC_YR.Substring(APM.MNC_DOC_YR.Length - 2) + "-" + APM.MNC_DOC_NO, famt5, Brushes.Black, col41 + 120, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("HN:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(txtApmHn.Text.Trim(), famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("แผนกที่นัด:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SEC_NO), famt5, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Name/ชื่อผู้ป่วย:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(label13.Text, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("วันที่พิมพ์:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(DateTime.Now.ToString(), famt5, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Age/อายุ:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(PTT.AgeStringShort1(), famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("สิทธิ์การรักษา:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString("-", famt5, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Date/นัดมาวันที่:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(bc.datetoShow(APM.MNC_APP_DAT) + " " + APM.apm_time, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("Dept/นัดตรวจที่แผนก:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SECR_NO), famt5, Brushes.Black, col41 + 60, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Doctor/แพทย์:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.doctor_name, famt5, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("สิ่งที่ต้องเตรียม:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.MNC_REM_MEMO, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("รายการตรวจ: ", famt5, Brushes.Black, col4, yPos, flags);
            String txt = "";
            if (grfApmOrder.Rows.Count > 1)
            {
                foreach (Row rowa in grfApmOrder.Rows)
                {
                    String name = "";
                    name = rowa[colgrfOrderName].ToString();
                    if (name.Equals("name")) continue;
                    txt += name + "\r\n";
                }
                if (txt.Length > 1)
                {
                    txt = txt.Substring(0, txt.Length - 1);
                }
                e.Graphics.DrawString(txt, famt1, Brushes.Black, col41, yPos, flags);
            }

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("เพื่อ:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.MNC_APP_DSC, famt5, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่


            e.Graphics.DrawString("เบอร์โทรติดต่อ:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString("02-1381155-60", famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("ผู้บันทึก:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString("---", famt5, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("หมายเหตุ:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString("เพื่อประโยชน์และความสะดวกของท่าน  กรุณามาให้ตรงตามวัน และเวลาที่แพทย์นัดทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("กรณีที่ไม่สามารถมาตรวจตามนัดได้  กรุณาโทรเพื่อแจ้งยกเลิกหรือเลื่อนนัดกับทางโรงพยาบาลทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            g.Dispose();
            Brush.Dispose();
            blackPen.Dispose();
        }
        private void BtnApmSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            PatientT07 apm = getApm();
            if (apm != null)
            {
                string re = bc.bcDB.pt07DB.insertPatientT07(apm);
                if (long.TryParse(re, out long chk))
                {
                    txtApmNO.Value = re;
                    txtApmDocYear.Value = (DateTime.Now.Year + 543).ToString();
                    if (grfApmOrder.Rows.Count > 1)
                    {
                        foreach (Row rowa in grfApmOrder.Rows)
                        {
                            String ordercode = rowa[colgrfOrderCode].ToString(); if (ordercode.Equals("code")) continue;
                            String ordername = rowa[colgrfOrderName].ToString();
                            String flag = rowa[colgrfOrderStatus].ToString();//O hotcharge, X xray, L lab
                            flag = flag.Equals("lab") ? "L" : flag.Equals("xray") ? "X" : flag.Equals("procedure") ? "O" : "";
                            String re1 = bc.bcDB.pt07DB.insertPatientT073(re, txtApmDocYear.Text.Trim(), ordercode, "", flag);
                            if (!long.TryParse(re1, out long chk1))
                            {
                                new LogWriter("e", "FrmOPD BtnApmSave_Click " + re1);
                                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnApmSave_Click save  ", re1);
                                lfSbMessage.Text = re1;
                            }
                        }
                    }
                    lbStatus.Text = txtApmNO.Text.Length > 0 ? "update appointment OK" : "insert appointment OK";
                    setGrfPttApm();
                    if (txtApmNO.Text.Length <= 0)
                    {
                        txtApmNO.Value = re;//insert ถ้าแก้ไข  นัด ไม่ต้องใส่ค่า
                    }
                }
                else
                {
                    new LogWriter("e", "FrmOPD BtnApmSave_Click " + re);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnApmSave_Click save  ", re);
                    lfSbMessage.Text = re;
                }
            }
            else
            {
                lfSbMessage.Text = "ไม่พบ visit";
            }
        }

        private void BtnApmNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlTabApm(true);
        }

        private void BtnApmOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmItemSearch frm = new FrmItemSearch(bc);
            frm.ShowDialog();
            if ((bc.items != null) && (bc.items.Count > 0))
            {
                pnApmOrder.Show();
                foreach (Item item in bc.items)
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

        private void initGrfPttApm(ref C1FlexGrid grf, ref Panel pn, String grfname)
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Rows.Count = 1;
            grf.Cols.Count = 19;
            grf.Name = grfname;

            grf.Cols[colgrfPttApmVsDate].Width = 100;
            grf.Cols[colgrfPttApmApmDateShow].Width = 100;
            grf.Cols[colgrfPttApmApmTime].Width = 60;
            grf.Cols[colgrfPttApmNote].Width = 500;
            grf.Cols[colgrfPttApmOrder].Width = 500;
            grf.Cols[colgrfPttApmHN].Width = 80;
            grf.Cols[colgrfPttApmPttName].Width = 250;
            grf.Cols[colgrfPttApmDeptR].Width = 120;
            grf.Cols[colgrfPttApmDeptMake].Width = 150;

            grf.ShowCursor = true;
            grf.Cols[colgrfPttApmVsDate].Caption = "date";
            grf.Cols[colgrfPttApmApmDateShow].Caption = "นัดวันที่";
            grf.Cols[colgrfPttApmApmTime].Caption = "นัดเวลา";
            grf.Cols[colgrfPttApmDeptR].Caption = "นัดตรวจที่แผนก";
            grf.Cols[colgrfPttApmDeptMake].Caption = "แผนกทำนัด";
            grf.Cols[colgrfPttApmNote].Caption = "รายละเอียด";
            grf.Cols[colgrfPttApmOrder].Caption = "Order";

            grf.Cols[colgrfPttApmApmDateShow].DataType = typeof(String);
            grf.Cols[colgrfPttApmApmTime].DataType = typeof(String);
            grf.Cols[colgrfPttApmDeptR].DataType = typeof(String);
            grf.Cols[colgrfPttApmNote].DataType = typeof(String);
            grf.Cols[colgrfPttApmOrder].DataType = typeof(String);
            grf.Cols[colgrfPttApmHN].DataType = typeof(String);
            grf.Cols[colgrfPttApmPttName].DataType = typeof(String);
            grf.Cols[colgrfPttApmDeptMake].DataType = typeof(String);

            grf.Cols[colgrfPttApmApmDateShow].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[colgrfPttApmApmTime].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[colgrfPttApmDeptR].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfPttApmDeptMake].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfPttApmNote].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfPttApmOrder].TextAlign = TextAlignEnum.LeftCenter;

            grf.Cols[colgrfPttApmVsDate].Visible = true;
            grf.Cols[colgrfPttApmApmDateShow].Visible = true;
            grf.Cols[colgrfPttApmDeptR].Visible = true;
            grf.Cols[colgrfPttApmNote].Visible = true;
            grf.Cols[colgrfPttApmDocNo].Visible = false;
            grf.Cols[colgrfPttApmDocYear].Visible = false;
            grf.Cols[colgrfPttApmVsDate].Visible = false;
            grf.Cols[colgrfPttApmHN].Visible = false;
            grf.Cols[colgrfPttApmPttName].Visible = true;
            grf.Cols[colgrfPttApmApmDate1].Visible = false;

            grf.Cols[colgrfPttApmVsDate].AllowEditing = false;
            grf.Cols[colgrfPttApmApmDateShow].AllowEditing = false;
            grf.Cols[colgrfPttApmDeptR].AllowEditing = false;
            grf.Cols[colgrfPttApmNote].AllowEditing = false;
            grf.Cols[colgrfPttApmApmTime].AllowEditing = false;
            grf.Cols[colgrfPttApmOrder].AllowEditing = false;
            grf.Cols[colgrfPttApmDeptMake].AllowEditing = false;

            grfPttApm.Click += GrfPttApm_Click;
            
            pn.Controls.Add(grf);
            theme1.SetTheme(grf, bc.iniC.themeApp);
        }
        private void GrfPttApm_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            if (((C1FlexGrid)sender).Name.Equals("grfPttApm"))
            {
                String apmno = "", apmyear = "";
                apmno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo].ToString();
                apmyear = grfPttApm[grfPttApm.Row, colgrfPttApmDocYear].ToString();
                setControlApm(apmyear, apmno);
            }
            else if (((C1FlexGrid)sender).Name.Equals("grfApm"))
            {

            }
        }
        private void setControlApm(String apmyear, String apmno)
        {
            PatientT07 apm = new PatientT07();
            apm = bc.bcDB.pt07DB.selectAppointment(apmyear, apmno);
            txtApmTime.Value = apm.apm_time;
            txtPttApmDate.Value = apm.MNC_APP_DAT;
            txtApmNO.Value = apm.MNC_DOC_NO;
            txtApmDocYear.Value = apm.MNC_DOC_YR;
            txtApmDtr.Value = apm.MNC_DOT_CD;
            //txtPttApmDate.Value = apm.MNC_DOT_CD;
            //cboApmDept.Value =  apm.MNC_SECR_NO;
            bc.setC1Combo(cboApmDept, apm.MNC_SECR_NO);
            txtApmDsc.Value = apm.MNC_APP_DSC;
            txtApmList.Value = apm.MNC_APP_DSC;
            txtApmTel.Value = apm.MNC_APP_TEL;
            txtApmRemark.Value = apm.MNC_DOT_CD;
            lbApmDtrName.Text = bc.selectDoctorName(apm.MNC_DOT_CD);
            DataTable dt = new DataTable();
            dt = bc.bcDB.pt07DB.selectAppointmentOrder(txtApmDocYear.Text, txtApmNO.Text);
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
                    chk = bc.bcDB.pm30DB.SelectNameByPk(code);
                    name = chk;
                }
                else if (flag.Equals("L"))
                {
                    LabM01 lab = new LabM01();
                    lab = bc.bcDB.labM01DB.SelectByPk(code);
                    name = lab.MNC_LB_DSC;
                }
                else if (flag.Equals("X"))
                {
                    XrayM01 xray = new XrayM01();
                    xray = bc.bcDB.xrayM01DB.SelectByPk(code);
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
        private void setGrfPttApm()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.pt07DB.selectByHnAll(txtApmHn.Text.Trim(), "desc");
            grfPttApm.Rows.Count = 1;
            grfPttApm.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfPttApm.Rows.Count;
            String time = "";
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfPttApm.Rows[i];
                rowa[colgrfPttApmApmDateShow] = bc.datetoShow1(row1["MNC_APP_DAT"].ToString());
                rowa[colgrfPttApmApmTime] = bc.showTime(row1["MNC_APP_TIM"].ToString());
                rowa[colgrfPttApmDeptR] = row1["mnc_md_dep_dsc"].ToString();//นัดตรวจที่แผนก
                rowa[colgrfPttApmDeptMake] = bc.bcDB.pm32DB.getDeptNameOPD(row1["mnc_sec_no"].ToString());
                rowa[colgrfPttApmNote] = row1["MNC_APP_DSC"].ToString();
                rowa[colgrfPttApmOrder] = row1["MNC_REM_MEMO"].ToString();

                rowa[colgrfPttApmDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfPttApmDocYear] = row1["MNC_DOC_YR"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void TxtPttApmDate_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtApmTime.Focus();
        }
        private PatientT07 getApm()
        {
            if (txtApmHn.Text.Trim().Length <= 0) return null;

            DateTime.TryParse(txtPttApmDate.Text, out DateTime apmdate);
            if (apmdate == null) return null;
            PatientT07 apm = new PatientT07();
            apm.MNC_HN_NO = txtApmHn.Text.Trim();
            apm.MNC_HN_YR = "";
            apm.MNC_DATE = "";
            apm.MNC_PRE_NO = "";
            apm.MNC_DOC_YR = txtApmDocYear.Text.Trim();
            apm.MNC_DOC_NO = txtApmNO.Text.Trim();
            apm.MNC_TIME = "";
            apm.MNC_APP_DAT = apmdate.Year + "-" + apmdate.ToString("MM-dd");
            apm.MNC_APP_TIM = bc.bcDB.pt07DB.setAppTime(txtApmTime.Text);
            //apm.MNC_APP_DSC = txtApmDsc.Text.Trim();
            apm.MNC_APP_DSC = txtApmList.Text.Trim();
            apm.MNC_APP_STS = "";
            apm.MNC_APP_TEL = txtApmTel.Text;
            apm.MNC_DOT_CD = txtApmDtr.Text.Trim();
            apm.apm_time = txtApmTime.Text;
            apm.MNC_SEC_NO = bc.iniC.station;
            apm.MNC_DEP_NO = "";
            apm.MNC_SECR_NO = cboApmDept.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept.SelectedItem).Value;
            apm.MNC_DEPR_NO = bc.bcDB.pm32DB.getDeptNoOPD(apm.MNC_SECR_NO);

            //apm.MNC_SEC_NO = cboApmDept.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept.SelectedItem).Value;
            return apm;
        }
        private void clearControlTabApm(Boolean new1)
        {
            txtPttApmDate.Value = DateTime.Now;
            txtApmTime.Value = "";
            bc.setC1Combo(cboApmDept, "");
            txtApmDsc.Value = "";
            txtApmRemark.Value = "";
            txtApmDtr.Value = "";
            lbApmDtrName.Text = "";
            txtApmTel.Value = "";
            txtApmNO.Value = "";
            txtApmList.Value = "";
            grfApmOrder.Rows.Count = 1;
            if (!new1)
            {
                grfPttApm.Rows.Count = 1;
            }
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
        private void TxtApmPlusDay_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            calApmDate();
        }

        private void TxtApmPlusDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
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

        private void TxtApmDoctorId_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void TxtApmDoctorId_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtApmDoctorName.Text = bc.selectDoctorName(txtApmDoctorId.Text.Trim());
            }
        }

        private void TxtAppViewUser_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                String txt = txtAppViewUser.Text.Trim();
                if (txt.Length > 0)
                {
                    setLbLoading("กำลังค้นหา กรุณารอสักครู่ ...");
                    showLbLoading();
                    lbAppViewUser.Text = bc.bcDB.stfDB.selectByPassword(txtAppViewUser.Text.Trim());
                    if (lbAppViewUser.Text.Length > 0)
                    {
                        BtnAppViewVisitNew_Click(null, null);
                    }
                    hideLbLoading();
                }
            }
        }

        private void BtnAppViewVisitNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String err = "", preno = "";
            if(cboAppViewDept.Text.Length<=0) { lfSbMessage.Text = "ส่งตัวไปแผนก ไม่พบข้อมูล"; return; }
            if (txtAppViewSymptom.Text.Length <= 0) { lfSbMessage.Text = "อาการ ไม่พบข้อมูล"; return; }
            Visit vs = new Visit();
            vs.HN = txtAppViewHn.Text.Trim();
            vs.PaidCode = APM.MNC_FN_TYP_CD;
            vs.symptom = txtAppViewSymptom.Text.Trim();
            err = "01";
            vs.DeptCode = cboAppViewDept.SelectedItem == null ? "" : ((ComboBoxItem)cboAppViewDept.SelectedItem).Value;
            vs.remark = txtAppRemark.Text.Trim();
            err = "02";
            vs.VisitType = "A";                 //ใน source  Fieldนี้ MNC_PT_FLG appoint ให้เป็น A
            vs.DoctorId = "0";       //IF CboDotCD.TEXT = '' THEN MNC_DOT_CD:= '00000'
            vs.VisitNote = "";
            if (vs.PaidCode.Equals("02"))
            {//สิทธิ เงินสด ให้เอาชื่อบริษัทออก
                vs.compcode = "";
                vs.insurcode = "";
            }
            else
            {
                vs.compcode = PTT.MNC_COM_CD;
                vs.insurcode = PTT.MNC_COM_CD2;
            }
            err = "03";
            //MNC_FIX_DOT_CD := edtDotcd2.TEXT  แพทย์เจ้าของไข้
            vs.DoctorOwn = "";
            vs.status_doe = "0";

            preno = bc.bcDB.vsDB.insertVisit1(vs.HN, vs.PaidCode, vs.symptom, vs.DeptCode, vs.remark, vs.DoctorId, vs.VisitType, vs.compcode, vs.insurcode, "auto");
            if (int.TryParse(preno, out int chk1))
            {
                lbStatus.Text = "ออก visit เรียบร้อย";
                btnAppViewVisitNew.Enabled = false;
            }
        }
        private void ChkNewVisit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkNewVisit.Checked)
            {
                txtAppViewHn.Value = PTT.MNC_HN_NO;
                label5.Text = PTT.Name;
                label6.Text = PTT.MNC_ATT_NOTE;
                lbAppViewFindPaidSSO.Text = bc.checkPaidSSO(PTT.MNC_ID_NO, lbLoading);
                lbAppViewPaidNameT.Text = bc.shortPaidName(bc.bcDB.finM02DB.getPaidName(PTT.MNC_FN_TYP_CD));
            }
            else
            {
                
            }
        }
        private void TxtVsUser_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                String txt = txtVsUser.Text.Trim();
                if (txt.Length > 0)
                {
                    if(btnVsSave.Enabled==false)                    {                        return;                    }
                    setLbLoading("กำลังค้นหา กรุณารอสักครู่ ...");
                    showLbLoading();
                    lbVsUser.Text = bc.bcDB.stfDB.selectByPassword(txtVsUser.Text.Trim());
                    if (lbVsUser.Text.Length > 0)
                    {
                        BtnVsSave_Click(null, null);
                    }
                    hideLbLoading();
                }
            }
        }
        private void setControl()
        {
            bc.setC1Combo(cboVsDept, APM.MNC_SECR_NO);
            
            PTT = bc.bcDB.pttDB.selectPatinetByHn(APM.MNC_HN_NO);
            byte[] pic = bc.bcDB.pttDB.SelectPatientImage(APM.MNC_HN_NO, APM.MNC_HN_YR);
            picVsPtt.SizeMode = PictureBoxSizeMode.StretchImage;
            picVsPtt.Image = C1.Win.C1Input.C1PictureBox.ImageFromByteArray(pic);
            txtPttHN.Value = APM.MNC_HN_NO;
            lbPttNameT.Text = APM.patient_name;
            txtApmDesc.Value = APM.MNC_APP_DSC;
            lbVsPaidNameT.Text = APM.MNC_FN_TYP_CD.Length<=0 ? "สิทธิ ไม่พบสิทธิ ตอนทำนัด" : bc.bcDB.finM02DB.getPaidName(APM.MNC_FN_TYP_CD);
            txtPttApmDate.Value = bc.datetoDBCultureInfo(APM.MNC_APP_DAT);
            txtPttApmTime.Value = bc.showTime(APM.MNC_APP_TIM);
            txtApmDept.Value = APM.deptname;
            txtApmDeptMake.Value = bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SEC_NO);
            lbFindPaidSSO.Text = bc.checkPaidSSO(PTT.MNC_ID_NO, lbLoading);
            txtApmOrder.Value = APM.MNC_REM_MEMO;
            txtAppRemark.Value = "";
            txtApmDoctorId.Text = APM.MNC_DOT_CD;
            txtApmDoctorName.Text = bc.selectDoctorName(txtApmDoctorId.Text.Trim());
        }
        private void initGrfAppNew()
        {
            grfApm = new C1FlexGrid();
            grfApm.Font = fEdit;
            grfApm.Dock = System.Windows.Forms.DockStyle.Fill;
            grfApm.Location = new System.Drawing.Point(0, 0);
            grfApm.Rows.Count = 1;
            grfApm.Cols.Count = 19;
            grfApm.Name = "grfname";

            grfApm.Cols[colgrfPttApmVsDate].Width = 100;
            grfApm.Cols[colgrfPttApmApmDateShow].Width = 100;
            grfApm.Cols[colgrfPttApmApmTime].Width = 60;
            grfApm.Cols[colgrfPttApmNote].Width = 500;
            grfApm.Cols[colgrfPttApmOrder].Width = 500;
            grfApm.Cols[colgrfPttApmHN].Width = 80;
            grfApm.Cols[colgrfPttApmPttName].Width = 250;
            grfApm.Cols[colgrfPttApmDeptR].Width = 120;
            grfApm.Cols[colgrfPttApmDeptMake].Width = 150;

            grfApm.ShowCursor = true;
            grfApm.Cols[colgrfPttApmVsDate].Caption = "date";
            grfApm.Cols[colgrfPttApmApmDateShow].Caption = "นัดวันที่";
            grfApm.Cols[colgrfPttApmApmTime].Caption = "นัดเวลา";
            grfApm.Cols[colgrfPttApmDeptR].Caption = "นัดตรวจที่แผนก";
            grfApm.Cols[colgrfPttApmDeptMake].Caption = "แผนกทำนัด";
            grfApm.Cols[colgrfPttApmNote].Caption = "รายละเอียด";
            grfApm.Cols[colgrfPttApmOrder].Caption = "Order";

            grfApm.Cols[colgrfPttApmApmDateShow].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmApmTime].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmDeptR].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmNote].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmOrder].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmHN].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmPttName].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmDeptMake].DataType = typeof(String);

            grfApm.Cols[colgrfPttApmApmDateShow].TextAlign = TextAlignEnum.CenterCenter;
            grfApm.Cols[colgrfPttApmApmTime].TextAlign = TextAlignEnum.CenterCenter;
            grfApm.Cols[colgrfPttApmDeptR].TextAlign = TextAlignEnum.LeftCenter;
            grfApm.Cols[colgrfPttApmDeptMake].TextAlign = TextAlignEnum.LeftCenter;
            grfApm.Cols[colgrfPttApmNote].TextAlign = TextAlignEnum.LeftCenter;
            grfApm.Cols[colgrfPttApmOrder].TextAlign = TextAlignEnum.LeftCenter;

            grfApm.Cols[colgrfPttApmVsDate].Visible = true;
            grfApm.Cols[colgrfPttApmApmDateShow].Visible = true;
            grfApm.Cols[colgrfPttApmDeptR].Visible = true;
            grfApm.Cols[colgrfPttApmNote].Visible = true;
            grfApm.Cols[colgrfPttApmDocNo].Visible = false;
            grfApm.Cols[colgrfPttApmDocYear].Visible = false;
            grfApm.Cols[colgrfPttApmVsDate].Visible = false;
            grfApm.Cols[colgrfPttApmHN].Visible = false;
            grfApm.Cols[colgrfPttApmPttName].Visible = true;
            grfApm.Cols[colgrfPttApmApmDate1].Visible = false;

            grfApm.Cols[colgrfPttApmHN].AllowEditing = false;
            grfApm.Cols[colgrfPttApmVsDate].AllowEditing = false;
            grfApm.Cols[colgrfPttApmApmDateShow].AllowEditing = false;
            grfApm.Cols[colgrfPttApmDeptR].AllowEditing = false;
            grfApm.Cols[colgrfPttApmNote].AllowEditing = false;
            grfApm.Cols[colgrfPttApmApmTime].AllowEditing = false;
            grfApm.Cols[colgrfPttApmOrder].AllowEditing = false;
            grfApm.Cols[colgrfPttApmDeptMake].AllowEditing = false;

            grfApm.DoubleClick += GrfApm_DoubleClick;

            panel1.Controls.Add(grfApm);
            theme1.SetTheme(grfApm, bc.iniC.themeApp);
        }

        private void GrfApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfApm.Row < 0) return;
            String docno = grfApm[grfApm.Row, colgrfPttApmDocNo].ToString();
            String docyear = grfApm[grfApm.Row, colgrfPttApmDocYear].ToString();
            APM = bc.bcDB.pt07DB.selectAppointment(docyear,docno);
            setControl();
            c1DockingTab1.SelectedTab = tabAppVisit;
        }
        private void setGrfApm()
        {
            if (pageLoad) return;
            PTT = bc.bcDB.pttDB.selectPatinetByHn(APM.MNC_HN_NO);
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.pt07DB.selectByHnAll(APM.MNC_HN_NO, "desc");
            grfApm.Rows.Count = 1; grfApm.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfApm.Rows[i];
                rowa[colgrfPttApmApmDateShow] = bc.datetoShowShort(row1["MNC_APP_DAT"].ToString());
                //rowa[colgrfPttApmApmDateShow] = row1["MNC_APP_DAT"].ToString();
                rowa[colgrfPttApmApmTime] = bc.showTime(row1["MNC_APP_TIM"].ToString());
                rowa[colgrfPttApmDeptR] = row1["mnc_md_dep_dsc"].ToString();
                rowa[colgrfPttApmDeptMake] = bc.bcDB.pm32DB.getDeptNameOPD(row1["mnc_sec_no"].ToString());
                rowa[colgrfPttApmNote] = row1["MNC_APP_DSC"].ToString();
                rowa[colgrfPttApmOrder] = row1["MNC_REM_MEMO"].ToString();

                rowa[colgrfPttApmDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfPttApmDocYear] = row1["MNC_DOC_YR"].ToString();
                rowa[colgrfPttApmHN] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfPttApmPttName] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfPttApmDtrname] = row1["dtr_name"].ToString();
                rowa[colgrfPttApmPhone] = row1["MNC_CUR_TEL"].ToString();
                rowa[colgrfPttApmPaidName] = row1["MNC_FN_TYP_DSC"].ToString();

                rowa[colgrfPttApmRemarkCall] = row1["remark_call"].ToString();
                rowa[colgrfPttApmRemarkCallDate] = row1["remark_call_date"].ToString();
                if (row1["status_remark_call"].ToString().Equals("1")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย รับสาย บุคคลอื่นเป็นคนรับ"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor); }
                else if (row1["status_remark_call"].ToString().Equals("2")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย ไม่รับสาย"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); }//#EBBDB6
                else if (row1["status_remark_call"].ToString().Equals("3")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรไม่ติด สายไม่ว่าง"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#CCCCFF"); }
                else if (row1["status_remark_call"].ToString().Equals("4")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย รับสาย แจ้งคนไข้ ครบถ้วน"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#9FE2BF"); }
                else if (row1["status_remark_call"].ToString().Equals("5")) { rowa[colgrfPttApmStatusRemarkCall] = "ไม่สามารถโทรได้ ไม่มีเบอร์โทร"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#FF7F50"); }
                else rowa[colgrfPttApmStatusRemarkCall] = "";

                rowa[0] = i.ToString();
                i++;
            }
            lfSbMessage.Text = "พบ " + dtvs.Rows.Count + "รายการ";
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt; Application.DoEvents();
        }
        private void showLbLoading()
        {
            lbLoading.Show(); lbLoading.BringToFront(); Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide(); Application.DoEvents();
        }
        private void BtnVsSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String err = "", preno="";
            Visit vs = new Visit();
            vs.HN = txtPttHN.Text.Trim();
            if (APM.MNC_FN_TYP_CD.Length <= 0) { vs.PaidCode = bc.checkPaidSSO(PTT.MNC_ID_NO);}
            else { vs.PaidCode = APM.MNC_FN_TYP_CD; }
            vs.symptom = txtApmDesc.Text.Trim();
            err = "01";
            vs.DeptCode = cboVsDept.SelectedItem == null ? "" : ((ComboBoxItem)cboVsDept.SelectedItem).Value;
            vs.remark = txtAppRemark.Text.Trim();
            err = "02";
            vs.VisitType = "A";                 //ใน source  Fieldนี้ MNC_PT_FLG Appointment
            vs.DoctorId = txtApmDoctorId.Text.Trim();       //IF CboDotCD.TEXT = '' THEN MNC_DOT_CD:= '00000'
            if(vs.DoctorId.Length <= 0) vs.DoctorId = "00000";
            vs.VisitNote = "";
            if (vs.PaidCode.Equals("02"))
            {//สิทธิ เงินสด ให้เอาชื่อบริษัทออก
                vs.compcode = "";
                vs.insurcode = "";
            }
            else
            {
                vs.compcode = PTT.MNC_COM_CD;
                vs.insurcode = PTT.MNC_COM_CD2;
            }
            err = "03";
            //MNC_FIX_DOT_CD := edtDotcd2.TEXT  แพทย์เจ้าของไข้
            vs.DoctorOwn = "";
            vs.status_doe = "0";
            
            preno = bc.bcDB.vsDB.insertVisit1(vs.HN, vs.PaidCode, vs.symptom, vs.DeptCode, vs.remark, vs.DoctorId, vs.VisitType, vs.compcode, vs.insurcode, "auto");
            if (int.TryParse(preno, out int chk1))
            {
                lbStatus.Text = "ออก visit เรียบร้อย";
                btnVsSave.Enabled = false;
            }
        }
        private void FrmApmVisitNew_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 2025-03-26";
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();

            txtApmDsc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtApmDsc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtApmDsc.AutoCompleteCustomSource = autoApm;
        }
    }
}
