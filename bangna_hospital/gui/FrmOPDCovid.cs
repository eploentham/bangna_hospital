using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.Win.C1Command;
using C1.Win.C1Document;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmOPDCovid:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;
        Panel pnPrint, pnThai, pnEng;

        C1DockingTab tcMain;
        C1DockingTabPage tabAdd, tabView;
        Label lbtxtHospName, lbtxtHn, lbtxtPttNameT, lbtxtPttNameE, lbtxtVsDate, lbtxtSex, lbtxtAge, lbtxtDOB, lbtxtPID, lbtxtPassport, lbtxtAddr1, lbtxtAddr2, lbtxtDtrId, lbtxtLabCode, lbtxtLabResult;
        C1TextBox txtHospName, txtHn, txtPttNameT, txtPttNameE, txtSex, txtAge, txtDOB, txtPID, txtPassport, txtAddr1, txtAddr2, txtDtrId, txtDtrNameT, txtDtrNameE, txtLabCode, txtLabName, txtLabResult;
        C1DateEdit txtVsDate, txtDate, txtHospStartDate, txtHospEndDate, txtStopStartDate, txtStopEndDate;
        C1Button btnHn, btnPrnThai, btnPrnEng, btnLabResult;
        Label lbtxtLabName1, lbtxtLabName2, lbtxtLabName3, lbtxtLabUnit, lbtxtLabNormal, lbtxtLabReport, lbtxtLabApprove, lbtxtDate, lbtxtSympbtom, lbtxtCountry;
        C1TextBox txtLabName1, txtLabName2, txtLabName3, txtLabResult1, txtLabResult2, txtLabResult3, txtLabUnit, txtLabNormal, txtLabReport, txtLabApprove, txtLabApproveDate, txtLabReportDate;
        C1TextBox txtVsTime, txtSympbtom, txtCountry, txtThaiOther, txtStop,txtTrue, txtNation, txtLabCodeSe184, txtLabNameSe184, txtLabResultSe184, txtLabUnitSe184, txtlab184IgG;
        RadioButton chkEng, chkThai;
        C1CheckBox chkThai1, chkThai2, chkThai3, chkThai4, chkThai5, chkThaiOther, chkDraw, chkHosp, chkStop, chkTrue, chktxtLabCodeSe184, chkLab184Nas, chkLab184Saliva, chkLab184Nucl, chkLab184Lamp, chkLab184Antigen, chklab184IgG, chklab184IgM;
        Label lbtxtHospEndDate, lbtxtStopEndDate, lbtxtNation, lbtxtLabCodeSe184, lbtxtLabResultSe184, lbtxtLabReportDate, lbLoading;
        String vn, preno, vsdate, paidtypecode="";

        Patient ptt;
        Staff dtr;
        public FrmOPDCovid(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            ptt = new Patient();
            dtr = new Staff();

            this.Load += FrmOPDCovid_Load;
            initCompoment();
            btnHn.Click += BtnHn_Click;
            btnPrnThai.Click += BtnPrnThai_Click;
            btnPrnEng.Click += BtnPrnEng_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            txtDtrId.KeyUp += TxtDtrId_KeyUp;
            txtStopStartDate.DropDownClosed += TxtStopStartDate_DropDownClosed;
            txtStopEndDate.DropDownClosed += TxtStopEndDate_DropDownClosed;
            txtVsDate.Value = DateTime.Now;
            txtHospName.Value = "โรงพยาบาล ทั่วไปขนาดใหญ่ บางนา5";
        }

        private void TxtStopEndDate_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            setTxtStop();
        }

        private void TxtStopStartDate_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            setTxtStop();
        }
        private void setTxtStop()
        {
            txtStop.Value = "";
            String startdate = "", enddate = "";
            int cnt = 0;
            DateTime startdate1 = new DateTime();
            DateTime enddate1 = new DateTime();
            Boolean chkstart = DateTime.TryParse(txtStopStartDate.Text, out startdate1);
            Boolean chkend = DateTime.TryParse(txtStopEndDate.Text, out enddate1);
            if(chkstart && chkend)
            {
                System.TimeSpan cnt1 = enddate1.Subtract(startdate1);
                if (cnt1.Days > 0)
                {
                    txtStop.Value = cnt1.Days;
                }
            }
        }
        private void TxtDtrId_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setTxtDoctor();
            }
        }
        private void setTxtDoctor()
        {
            txtDtrNameT.Text = bc.selectDoctorName(txtDtrId.Text.Trim());
            txtDtrNameE.Text = bc.selectDoctorNameE(txtDtrId.Text.Trim());
        }
        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setControl(txtHn.Text.Trim());
            }
        }

        private void BtnPrnEng_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //printCOVID("english");
            printCOVIDlabresultEng();
        }

        private void BtnPrnThai_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //printCOVID("thai");
            if (chkThai.Checked)
            {
                printCOVIDcertThai();
            }
            else if (chkEng.Checked)
            {
                printCOVIDcertEng();
            }
        }

        private void BtnHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControl(txtHn.Text.Trim());
        }
        private void setControl(String hn)
        {
            if (hn.Equals(""))
            {
                return;
            }
            String date = "";
            DateTime vsdate = new DateTime();
            DateTime.TryParse(txtVsDate.Text, out vsdate);
            //MessageBox.Show("2121212 vsdate.Year " + vsdate.Year, "");
            if (vsdate.Year < 2000)
            {
                vsdate = vsdate.AddYears(543);
            }
            date = vsdate.Year + "-" + vsdate.ToString("MM-dd");
            System.Data.DataTable dt = bc.bcDB.vsDB.selectVisitByHn5(hn);
            
            //DataTable dtor = bc.selectOPDViewOR(hn);
            if (dt.Rows.Count <= 0)
            {
                clearControl();
                return;
            }
            ptt = bc.bcDB.pttDB.selectPatinet(hn);
            vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "." + dt.Rows[0]["MNC_VN_SEQ"].ToString();
            preno = dt.Rows[0]["MNC_pre_NO"].ToString();
            paidtypecode = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();

            txtPttNameT.Value = ptt.Name;
            txtPID.Value = dt.Rows[0]["mnc_id_no"].ToString();
            //txtAddr1.Value = dt.Rows[0]["mnc_full_add"].ToString() != "" ? dt.Rows[0]["mnc_full_add"].ToString() : dt.Rows[0]["mnc_dom_add"].ToString() + " ต." + dt.Rows[0]["mnc_tum_dsc"].ToString() + " อ." + dt.Rows[0]["mnc_amp_dsc"].ToString() + " จ." + dt.Rows[0]["mnc_chw_dsc"].ToString() + " " + dt.Rows[0]["mnc_cur_poc"].ToString();
            txtAddr2.Value = "";
            txtAge.Value = ptt.AgeStringShort();
            txtDtrId.Value = dt.Rows[0]["doctor_id"].ToString();
            //MessageBox.Show("1111 " , "");
            DateTime dob = new DateTime();
            DateTime.TryParse(dt.Rows[0]["mnc_bday"].ToString(), out dob);
            txtDOB.Value = dob.ToString("dd-MM-yyyy");
            DateTime dtvs = new DateTime();
            //MessageBox.Show("mnc_date " + dt.Rows[0]["mnc_date"].ToString(), "");
            DateTime.TryParse(dt.Rows[0]["mnc_date"].ToString(), out dtvs);
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                dtvs = dtvs.AddYears(543);
                //MessageBox.Show("11111 ", "");
            }
            //MessageBox.Show("2222 ", "");
            //txtVsDate.Value = dtvs.ToString("dd-MM-yyyy");
            //txtLabReportDate.Value = dtvs.ToString("dd-MM-yyyy");
            txtVsDate.Value = dtvs.ToString("yyyy-MM-dd");
            txtLabReportDate.Value = dtvs.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
            String time = "";
            time = "0000" + dt.Rows[0]["mnc_time"].ToString();
            time = time.Substring(time.Length - 4, 4);
            time = time.Substring(0, 2) + ":" + time.Substring(time.Length - 2, 2);
            txtVsTime.Value = time;
            //MessageBox.Show("2121212 date"+ date, "");
            txtHn.Value = dt.Rows[0]["MNC_HN_NO"].ToString();
            //txtOROther.Text = dt.Rows[0]["MNC_HN_NO"].ToString();
            txtSex.Value = dt.Rows[0]["mnc_sex"].ToString();
            txtNation.Value = dt.Rows[0]["mnc_nat_dsc_e"].ToString();

            DataTable dtcovid = new DataTable();
            DataTable dtcovidSE184 = new DataTable();
            dtcovid = bc.bcDB.vsDB.selectLabCOVIDbyHN(date, date, hn);
            txtLabNameSe184.Value = "COVID-19 IgG/IgM (Venous Blood)";
            txtLabResultSe184.Value = "Nagative";
            txtLabUnitSe184.Value = "Nagative";
            txtlab184IgG.Value = "Nagative";
            txtLabName.Value = "SARS-CoV-2 (Covid-19) (Real-Time PCR Method)";
            txtLabResult.Value = "Undetectable";
            txtLabUnit.Value = "Undetectable";
            //MessageBox.Show("3333 ", "");
            if (dtcovid.Rows.Count > 2)
            {
                //txtLabName1.Value = dtcovid.Rows[3]["mnc_res"].ToString();     //Detection Method :    mnc_lb_res_cd = 04
                
                txtLabName2.Value = dtcovid.Rows[1]["mnc_res"].ToString();     //Gene Detection :      mnc_lb_res_cd = 02
                txtLabName3.Value = dtcovid.Rows[2]["mnc_res"].ToString();     //Reagent :             mnc_lb_res_cd = 03
                txtLabCode.Value = dtcovid.Rows[2]["mnc_lb_cd"].ToString();
                txtLabName.Value= dtcovid.Rows[0]["mnc_lb_dsc"].ToString();
                txtLabResult.Value = dtcovid.Rows[0]["mnc_res_value"].ToString();
                txtLabUnit.Value = dtcovid.Rows[0]["mnc_res_unt"].ToString();
                txtLabNormal.Value = dtcovid.Rows[0]["mnc_lb_res"].ToString();

                txtLabReport.Value = dtcovid.Rows[0]["MNC_USR_NAME_report"].ToString()+" "+ dtcovid.Rows[0]["user_report"].ToString();
                txtLabApprove.Value = dtcovid.Rows[0]["MNC_USR_NAME_approve"].ToString() + " " + dtcovid.Rows[0]["user_check"].ToString();
                txtLabReportDate.Value = bc.datetoShow(dtcovid.Rows[0]["MNC_STAMP_DAT"].ToString());
                txtLabApproveDate.Value = bc.datetoShow(dtcovid.Rows[0]["MNC_RESULT_DAT"].ToString());
                //String time = "";
                //time = "0000"+dtcovid.Rows[0]["mnc_time"].ToString();
                //time = time.Substring(time.Length-4, 4);
                //time = time.Substring(0, 2) + ":" + time.Substring(time.Length - 2, 2);
                //txtVsTime.Value = time;
                if (txtLabResult.Text.ToLower().Equals("undetectable"))
                {
                    txtSympbtom.Value = "ไม่พบเชื้อโควิด";
                }
                txtTrue.Value = time;
            }
            //MessageBox.Show("4444 ", "");
            dtcovidSE184 = bc.bcDB.vsDB.selectLabCOVIDSE184byHN(date, date, hn);
            if (dtcovidSE184.Rows.Count > 0)
            {
                if (dtcovidSE184.Rows[1]["mnc_lb_res_cd"].ToString().Equals("02"))
                {
                    txtLabNameSe184.Value = dtcovidSE184.Rows[1]["mnc_lb_dsc"].ToString();
                    txtLabNameSe184.Value = "COVID-19 IgG/IgM (Venous Blood)";
                    txtLabCodeSe184.Value = dtcovidSE184.Rows[1]["mnc_lb_cd"].ToString();
                    txtLabResultSe184.Value = dtcovidSE184.Rows[1]["mnc_res_value"].ToString();
                    txtLabUnitSe184.Value = dtcovidSE184.Rows[1]["mnc_res_unt"].ToString();
                    if (txtLabResultSe184.Text.ToLower().Equals("undetectable"))
                    {
                        txtLabResultSe184.Value = "Nagative";
                        txtLabUnitSe184.Value = "Nagative";
                    }
                }
            }

            //DateTime dt1 = new DateTime();
            //int year = 0;
            //dt1 = DateTime.Parse(dt.Rows[0]["mnc_bday"].ToString());
            //year = dt1.Year;
            //txtAge.Text = String.Concat(System.DateTime.Now.Year - year);
            //}
            setTxtDoctor();
        }
        private void clearControl()
        {
            txtAddr1.Value = "";
            txtAddr2.Value = "";
            txtAge.Value = "";
            txtHn.Value = "";
            txtSex.Value = "";
            txtLabCode.Value = "";
            txtLabCodeSe184.Value = "";
            txtLabName.Value = "";
            txtLabResult.Value = "";
            txtLabResultSe184.Value = "";
            txtLabUnit.Value = "";
            txtLabResult1.Value = "";
            txtLabResult2.Value = "";
            txtLabResult3.Value = "";
            txtLabUnitSe184.Value = "";
        }
        private void printCOVIDlabresultEng()
        {
            String pathFolder = "", filename = "", datetick = "", vsdate="", reqno="", reportdate="", paidname="", chkyear="";
            int gapLine = 20, gapLine1 = 15, gapX = 40, gapY = 20, xCol1 = 100,xCol13=130, xCol15 = 150, xCol2 = 200, xCol25 = 250, xCol3 = 300, xCol4 = 400, xCol41 = 450, xCol47 = 470, xCol5 = 500;
            Size size = new Size();
            DataTable dtcovid = new DataTable();
            DataTable dtcovidSE184 = new DataTable();
            DateTime vsdate1 = new DateTime();

            C1PdfDocument pdf = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            StringFormat _sfRight, _sfRightCenter;
            //Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            Font titleFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetitleFont, FontStyle.Bold);
            Font hdrFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizehdrFont, FontStyle.Regular);
            Font hdrFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizehdrFont, FontStyle.Bold);
            Font ftrFont = new Font(bc.iniC.pdfFontName, 8);
            Font txtFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFont, FontStyle.Regular);
            //Font txtFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFontB, FontStyle.Bold);//
            Font txtFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFontB, FontStyle.Bold);
            pdf.FontType = FontTypeEnum.Embedded;

            RectangleF rcPage = pdf.PageRectangle;
            rcPage = RectangleF.Empty;
            rcPage.Inflate(-72, -92);
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 110;

            DateTime.TryParse(txtVsDate.Text, out vsdate1);
            //if (vsdate1.Year > 2500)
            //{
            vsdate = vsdate1.Year + "-" + vsdate1.ToString("MM-dd");
            //MessageBox.Show("111  vsdate " + vsdate + " preno " + preno, "");
            if (vsdate.Length > 4)
            {
                chkyear = vsdate.Substring(0, 4);
                if (int.Parse(chkyear) > 2500)
                {
                    vsdate1 = vsdate1.AddYears(-543);
                    vsdate = vsdate1.Year + "-" + vsdate1.ToString("MM-dd");
                }
                else
                {

                }
            }
            //}
            //else
            //{
            //    vsdate = vsdate1.ToString("yyyy-MM-dd");
            //}
            //MessageBox.Show("222  vsdate " + vsdate + " preno " + preno, "");
            //if (bc.iniC.windows.Equals("windowsxp"))
            //{
            //    vsdate1 = vsdate1.AddYears(-543);
            //    vsdate = vsdate1.Year + "-" + vsdate1.ToString("MM-dd");
            //}
            //MessageBox.Show("222  vsdate " + vsdate + " preno " + preno, "");
            dtcovid = bc.bcDB.vsDB.selectLabCOVIDbyHNpreno(txtHn.Text.Trim(), vsdate, preno);
            if (dtcovid.Rows.Count <= 2)
            {
                MessageBox.Show("ผลยังไม่ออก ", "");
                return;
            }
            if (dtcovid.Rows.Count > 0)
            {
                reqno = dtcovid.Rows[0]["MNC_REQ_NO"].ToString();
                reportdate= dtcovid.Rows[0]["MNC_RESULT_DAT"].ToString();
            }
            else
            {
                if (dtcovid.Rows.Count < 3)
                {
                    MessageBox.Show("No Result  vsdate " + vsdate + " preno " + preno, "");
                    setLbLoading("No Result");
                    Thread.Sleep(2000);
                    return;
                }
            }
            reqno += vsdate1.ToString("dd-MM-yy");
            paidname = paidtypecode.Equals("02") ? "Cash" : paidtypecode;
            //gapY += gapLine;
            gapY = 10;
            gapX = 20;
            gapLine = 15;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            String txt = bc.iniC.hostnamee;
            size = bc.MeasureString(txt, titleFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);

            txt = "Patient Name : "+txtPttNameT.Text.Trim();
            rcPage.X = xCol25;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            //MessageBox.Show("333  vsdate " + vsdate + " preno " + preno, "");

            String[] age = txtAge.Text.Split('.');
            String age1 = "";
            if (age.Length > 2)
            {
                age1 = age[0] + "Years" + age[1] + "Months" + age[2] + "Days";
            }
            else if (age.Length > 1)
            {
                age1 = age[0] + "Years" + age[1] + "Months";
            }
            ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            //MessageBox.Show("ptt  AgeStringShort " + ptt.AgeStringShort(), "");
            txt = "Age : " + age1;
            //txt = "Age : " + age1;
            rcPage.X = xCol47;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine;
            txt = "HN : " + txtHn.Text;
            rcPage.X = xCol25;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = "VN : " + vn;
            rcPage.X = xCol47;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            
            gapY += gapLine;
            txt = "Req NO : " + reqno;
            rcPage.X = xCol25;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            //MessageBox.Show("444  vsdate " + vsdate + " preno " + preno, "");
            txt = "Date : " + vsdate1.ToString("dd-MM")+"-"+ vsdate1.Year;
            rcPage.X = xCol47;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine;
            txt = "Department : OPD-COVID";
            rcPage.X = gapX;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = "Doctor : " + txtDtrNameE.Text.Trim() +" "+txtDtrId.Text;
            rcPage.X = xCol25;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            
            txt = "Date Result : " + bc.datetoShow(reportdate);
            rcPage.X = xCol47;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine;
            txt = "Patient Type : " + paidname;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            //MessageBox.Show("555  vsdate " + vsdate + " preno " + preno, "");
            txt = "Company : ";
            rcPage.X = xCol25;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = "page : 1/1";
            rcPage.X = xCol5;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine;
            txt = "Report LAB Result";
            rcPage.X = gapX;
            rcPage.Y = gapY-2;
            size = bc.MeasureString(txt, hdrFontB);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, hdrFontB, Brushes.Black, rcPage);

            gapY += gapLine;
            pdf.DrawLine(Pens.Black, gapX,100,xCol5 + 80, 100);
            pdf.DrawLine(Pens.Black, gapX, 100 + 20, xCol5 + 80, 100 + 20);
            float tempy3=0, tempy4=0;
            tempy3 = 200 + 140;
            tempy4 = 200 + 140 + 20;
            pdf.DrawLine(Pens.Black, gapX, tempy3, xCol5 + 80, tempy3);
            pdf.DrawLine(Pens.Black, gapX, tempy4, xCol5 + 80, tempy4);

            gapY += gapLine;
            txt = "DESCRIPTION ";
            rcPage.X = xCol1;
            rcPage.Y = gapY-13;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = "RESULT ";
            rcPage.X = xCol3;
            rcPage.Y = gapY-13;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine;
            txt = txtLabName.Text.Trim();
            rcPage.X = gapX + 20;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            //MessageBox.Show("666  vsdate " + vsdate + " preno " + preno, "");
            gapY += gapLine;
            txt = dtcovid.Rows[0]["MNC_RES"].ToString();
            rcPage.X = xCol13;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            

            txt = dtcovid.Rows[0]["MNC_RES_VALUE"].ToString();
            rcPage.X = xCol3;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = "...................................................................";
            rcPage.X = xCol3 - 10;
            rcPage.Y = gapY + 2;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = dtcovid.Rows[0]["MNC_RES_UNT"].ToString();
            rcPage.X = xCol41;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            //MessageBox.Show("777  vsdate " + vsdate + " preno " + preno, "");
            gapY += gapLine;
            txt = dtcovid.Rows[1]["MNC_RES"].ToString();
            rcPage.X = xCol13;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = dtcovid.Rows[1]["MNC_RES_VALUE"].ToString();
            rcPage.X = xCol3;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = "...................................................................";
            rcPage.X = xCol3-10;
            rcPage.Y = gapY + 2;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = dtcovid.Rows[1]["MNC_RES_UNT"].ToString();
            rcPage.X = xCol41;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            //MessageBox.Show("888  vsdate " + vsdate + " preno " + preno, "");
            gapY += gapLine;
            txt = dtcovid.Rows[2]["MNC_RES"].ToString();
            rcPage.X = xCol13;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = "...................................................................";
            rcPage.X = xCol3 - 10;
            rcPage.Y = gapY + 2;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = dtcovid.Rows[2]["MNC_RES_VALUE"].ToString();
            rcPage.X = xCol3;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = dtcovid.Rows[2]["MNC_RES_UNT"].ToString();
            rcPage.X = xCol41;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            //MessageBox.Show("999  vsdate " + vsdate + " preno " + preno, "");
            gapY = 400;
            gapY += gapLine;
            txt = "Key by : "+dtcovid.Rows[2]["user_lab"].ToString();
            rcPage.X = gapX;
            rcPage.Y = tempy3;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = "Report by : " + dtcovid.Rows[2]["user_report"].ToString();
            rcPage.X = xCol25;
            rcPage.Y = tempy3;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = "Approve by : " + dtcovid.Rows[2]["user_check"].ToString();
            rcPage.X = xCol4;
            rcPage.Y = tempy3;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine;
            txt = "FM-LAB-096 00-17/07/55) ";
            rcPage.X = gapX;
            rcPage.Y = tempy4;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            pathFolder = bc.iniC.medicalrecordexportpath + "\\COVID\\";
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            datetick = DateTime.Now.Ticks.ToString();
            filename = pathFolder + "\\" + txtHn.Text.TrimEnd() + "_" + datetick + ".pdf";
            pdf.Save(filename);
            pdf.Clear();
            pdf.Dispose();

            if (File.Exists(filename))
            {
                Process p = new Process();
                ProcessStartInfo s = new ProcessStartInfo(filename);
                p.StartInfo = s;
                p.Start();
            }
        }
        private void printCOVIDcertEng()
        {
            String pathFolder = "", filename = "", datetick = "";
            int gapLine = 20, gapLine1 = 15, gapX = 40, gapY = 20, xCol2 = 200, xCol1 = 100, xCol3 = 350, xCol4 = 430, xCol41 = 450, xCol5 = 500;
            int lineDot = 2;
            Size size = new Size();
            String txt = "";
            float temp = 0;

            C1PdfDocument pdf = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            StringFormat _sfRight, _sfRightCenter;
            //Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            Font titleFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetitleFont, FontStyle.Bold);
            Font hdrFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizehdrFont, FontStyle.Regular);
            Font hdrFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizehdrFont, FontStyle.Bold);
            Font ftrFont = new Font(bc.iniC.pdfFontName, 8);
            Font txtFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFont, FontStyle.Regular);
            //Font txtFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFontB, FontStyle.Bold);//
            Font txtFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFontB, FontStyle.Regular);
            pdf.FontType = FontTypeEnum.Embedded;

            RectangleF rcPage = pdf.PageRectangle;
            rcPage = RectangleF.Empty;
            rcPage.Inflate(-72, -92);
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 110;
            //logo
            Image loadedImage;
            loadedImage = Resources.LOGO_BW_tran;
            float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
            float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

            float widthFactor = 4.8F;
            float heightFactor = 4.8F;

            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    widthFactor = 1;
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                    //newWidth = newWidth / 1.2;
                    //newHeight = newHeight / 1.2;
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }

            RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            pdf.DrawImage(loadedImage, recf);
            //logo

            rcPage.X = gapX + recf.Width - 10;
            rcPage.Y = gapY;
            size = bc.MeasureString(bc.iniC.hostnamee, titleFont);
            rcPage.Width = size.Width;
            pdf.DrawString(bc.iniC.hostnamee, titleFont, Brushes.Black, rcPage);
            gapY += gapLine;
            rcPage.Y = gapY;
            size = bc.MeasureString(bc.iniC.hostaddresse, hdrFont);
            rcPage.Width = size.Width;
            pdf.DrawString(bc.iniC.hostaddresse, hdrFont, Brushes.Black, rcPage);
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;

            RectangleF rcHdr = new RectangleF();
            rcHdr.Width = 542;
            rcHdr.Height = 25;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            //rcHdr.Location
            //pdf.DrawRectangle(Pens.Black, rcHdr);       // ตาราง
            size = bc.MeasureString("Certificate of Testing for COVID-19", titleFont);
            rcPage.Width = size.Width;
            rcPage.Y = gapY - 2;
            rcPage.X = (542 / 2) - (size.Width / 2) + 40;
            pdf.DrawString("Certificate of Testing for COVID-19", titleFont, Brushes.Black, rcPage);

            gapY += gapLine + 5;
            gapY += gapLine1;

            txt = "HN : " + txtHn.Text.Trim();
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            //rcPage.X = gapX+10;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            rcPage.X = xCol41-20;
            rcPage.Y = gapY;
            DateTime dttime = new DateTime();
            DateTime.TryParse(txtVsDate.Text, out dttime);
            //if (bc.iniC.windows.Equals("windowsxp"))
            //{
            //    dttime.AddYears(543);
            //    //MessageBox.Show("11111 ", "");
            //}
            

            txt = "Date of issue : " + dttime.ToString("MMM-dd-yyyy", new CultureInfo("en-US"));
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "Name : "+txtPttNameT.Text.Trim();
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            //rcPage.X = gapX+10;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            if ((txtPID.Text.Trim().Length > 0) && (txtPassport.Text.Trim().Length > 0))
            {
                txt = "Passport No / ID No : " + txtPID.Text.Trim()+" / "+ txtPassport.Text.Trim();
            }
            else if ((txtPID.Text.Trim().Length > 0) && (txtPassport.Text.Trim().Length <= 0))
            {
                txt = "ID No : " + txtPID.Text.Trim();
            }
            else if ((txtPID.Text.Trim().Length <= 0) && (txtPassport.Text.Trim().Length > 0))
            {
                txt = "Passport No : " + txtPassport.Text.Trim();
            }
            else if ((txtPID.Text.Trim().Length <= 0) && (txtPassport.Text.Trim().Length <= 0))
            {
                txt = "Passport No / ID No : ";
            }
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = xCol41 - 20;
            //rcPage.X = gapX+10;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            
            gapY += gapLine1;
            txt = "Nationality : " + txtNation.Text.Trim();
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);        //อินเดีย

            DateTime.TryParse(txtDOB.Text, out dttime);
            txt = "Date of Birth : " + dttime.ToString("MMM-dd-yyyy", new CultureInfo("en-US"));
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = xCol2;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            
            if (txtSex.Text.Trim().Equals("M"))
            {
                txt = "Sex : Male";
            }
            else if(txtSex.Text.Trim().Equals("F"))
            {
                txt = "Sex : Female";
            }
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = xCol41 - 20;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "This is to certify the following results which have been confirmed by testing for COVID-19 conducted with";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = xCol1;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "the sample taken from the above-menthioned person.";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            float hei = 0;
            gapY += gapLine1;
            gapY += gapLine1;
            rcHdr.Width = 530;
            rcHdr.Height = 400;
            hei = rcHdr.Height;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            pdf.DrawLine(Pens.Black, gapX, gapY+33, 530+gapX, gapY+ 33);
            pdf.DrawLine(Pens.Black, xCol2, gapY, xCol2, rcHdr.Height + gapY);
            pdf.DrawLine(Pens.Black, xCol3, gapY, xCol3, rcHdr.Height + gapY);
            pdf.DrawLine(Pens.Black, xCol4, gapY, xCol4, rcHdr.Height + gapY);

            //gapY += gapLine1;
            txt = "Sample";
            size = bc.MeasureString(txt, hdrFont);
            rcPage.X = gapX+30;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, hdrFont, Brushes.Black, rcPage);
            
            txt = "Testing for COVID-19";
            size = bc.MeasureString(txt, hdrFont);
            rcPage.X = xCol2+25;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, hdrFont, Brushes.Black, rcPage);

            txt = "Result";
            size = bc.MeasureString(txt, hdrFont);
            rcPage.X = xCol3+30;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, hdrFont, Brushes.Black, rcPage);

            txt = "Sampleing Date and Time /";
            size = bc.MeasureString(txt, hdrFont);
            rcPage.X = xCol4+8;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, hdrFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "(Check one of the boxes below)";
            size = bc.MeasureString(txt, hdrFont);
            rcPage.X = gapX+10;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, hdrFont, Brushes.Black, rcPage);

            txt = "(Check one of the boxes below)";
            size = bc.MeasureString(txt, hdrFont);
            rcPage.X = xCol2+10;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, hdrFont, Brushes.Black, rcPage);

            txt = "Result Date";
            size = bc.MeasureString(txt, hdrFont);
            rcPage.X = xCol4 + 30;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, hdrFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            rcHdr = new RectangleF();
            rcHdr.Width = 8;
            rcHdr.Height = 8;
            rcHdr.X = gapX+3;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkLab184Nas.Checked)
            {
                rcPage.X = gapX + 4;
                rcPage.Y = gapY - 7;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "Nasopharyngeal";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = gapX + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            rcHdr.X = xCol2+3;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkLab184Nucl.Checked)
            {
                rcPage.X = xCol2 + 4;
                rcPage.Y = gapY - 7;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "Nucleic acid amplification test";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 14;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = txtLabResult.Text.Trim();
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol3 + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            if (txtLabReport.Text.Trim().ToLower().Equals("undetectable"))
            {
                txt = "(ไม่พบเชื้อโควิด)";
            }
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol3 + 12;
            rcPage.Y = gapY+20;
            rcPage.Width = size.Width;
            //pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            txt = "(1) Sampling Date and Time";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol4 + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "(real time RT-PCR)";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            DateTime.TryParse(txtVsDate.Text + " "+txtVsTime.Text, out dttime);
            //if (bc.iniC.windows.Equals("windowsxp"))
            //{
            //    dttime.AddYears(543);
            //}
            txt = dttime.ToString("MMM-dd-yyyy HH:mm tt", new CultureInfo("en-US"));
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol4 + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            DateTime.TryParse(txtLabReportDate.Text, out dttime);
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                dttime = dttime.AddYears(543);
            }
            txt = "Result Date : "+ dttime.ToString("MMM-dd-yyyy", new CultureInfo("en-US"));
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol4 + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcHdr.X = gapX + 3;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkLab184Saliva.Checked)
            {
                rcPage.X = gapX + 4;
                rcPage.Y = gapY - 7;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "Saliva ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = gapX + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            rcHdr.X = xCol2 + 3;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkLab184Lamp.Checked)
            {
                rcPage.X = xCol2 + 4;
                rcPage.Y = gapY - 7;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "Nucleic acid amplification test ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 14;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "(LAMP)";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcHdr.X = xCol2 + 3;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkLab184Antigen.Checked)
            {
                rcPage.X = xCol2 + 4;
                rcPage.Y = gapY - 7;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "Antigen test (CLEIA)";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 14;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            rcHdr.X = gapX + 3;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chktxtLabCodeSe184.Checked)
            {
                rcPage.X = gapX + 4;
                rcPage.Y = gapY - 7;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = txtLabNameSe184.Text.Trim();
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = gapX + 14;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);



            txt = "COVID-19 IgM";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 14;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            //gapY += gapLine1;
            rcHdr.X = xCol2 + 3;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chklab184IgM.Checked)
            {
                rcPage.X = xCol2 + 4;
                rcPage.Y = gapY - 7;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);

                txt = txtLabResultSe184.Text.Trim();
                size = bc.MeasureString(txt, txtFont);
                rcPage.X = xCol3 + 12;
                rcPage.Y = gapY;
                rcPage.Width = size.Width;
                pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            }

            

            gapY += gapLine1;
            rcHdr.X = xCol2 + 3;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chklab184IgG.Checked)
            {
                rcPage.X = xCol2 + 4;
                rcPage.Y = gapY - 7;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);

                txt = txtlab184IgG.Text.Trim();
                size = bc.MeasureString(txt, txtFont);
                rcPage.X = xCol3 + 12;
                rcPage.Y = gapY;
                rcPage.Width = size.Width;
                pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            }

            txt = "COVID-19 IgG";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 14;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "Sensitivity 94.1%";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 14;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "Specitcity 98.1%";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol2 + 14;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY = 650;
            txt = "Remarks";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = gapX + 12;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            gapY += gapLine1;
            pdf.DrawLine(Pens.Black, gapX + 50, gapY , 530 + gapX, gapY);
            gapY += gapLine1;
            pdf.DrawLine(Pens.Black, gapX, gapY , 530 + gapX, gapY);
            gapY += gapLine1;
            pdf.DrawLine(Pens.Black, gapX, gapY , 530 + gapX, gapY );

            gapY += gapLine1;
            gapY += gapLine1;
            txt = "Signature by doctor ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol4-80;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            pdf.DrawLine(Pens.Black, xCol4 +size.Width - 120, gapY+14, size.Width + xCol4 + 40, gapY+14);

            gapY += gapLine1;
            txt = txtDtrNameE.Text;
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol4 + 30;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "Attending Physican License NO. " + " " + txtDtrId.Text;
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = xCol4-20;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "FM-MED-023(29-07-63)";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = gapX;
            rcPage.Y = gapY;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            pathFolder = bc.iniC.medicalrecordexportpath + "\\COVID\\";
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            datetick = DateTime.Now.Ticks.ToString();
            filename = pathFolder + "\\" + txtHn.Text.TrimEnd() + "_" + datetick + ".pdf";
            pdf.Save(filename);
            pdf.Clear();
            pdf.Dispose();

            if (File.Exists(filename))
            {
                Process p = new Process();
                ProcessStartInfo s = new ProcessStartInfo(filename);
                p.StartInfo = s;
                p.Start();
            }
        }
        private void printCOVIDcertThai()
        {
            String pathFolder = "", filename = "", datetick = "";
            int gapLine = 20, gapLine1 = 15, gapX = 40, gapY = 20, xCol2 = 200, xCol1 = 160, xCol3 = 300, xCol4 = 390, xCol41=450, xCol5 = 500;
            int lineDot = 2;
            Size size = new Size();
            String txt = "";
            float temp = 0;

            C1PdfDocument pdf = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            StringFormat _sfRight, _sfRightCenter;
            //Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            Font titleFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetitleFont, FontStyle.Bold);
            Font hdrFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizehdrFont, FontStyle.Regular);
            Font hdrFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizehdrFont, FontStyle.Bold);
            Font ftrFont = new Font(bc.iniC.pdfFontName, 8);
            Font txtFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFont, FontStyle.Regular);
            //Font txtFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFontB, FontStyle.Bold);//
            Font txtFontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFontB, FontStyle.Regular);
            pdf.FontType = FontTypeEnum.Embedded;

            RectangleF rcPage = pdf.PageRectangle;
            rcPage = RectangleF.Empty;
            rcPage.Inflate(-72, -92);
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 110;
            //logo
            Image loadedImage;
            loadedImage = Resources.LOGO_BW_tran;
            float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
            float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

            float widthFactor = 4.8F;
            float heightFactor = 4.8F;

            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    widthFactor = 1;
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                    //newWidth = newWidth / 1.2;
                    //newHeight = newHeight / 1.2;
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }

            RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            pdf.DrawImage(loadedImage, recf);
            //logo


            rcPage.X = gapX + recf.Width - 10;
            rcPage.Y = gapY;
            size = bc.MeasureString(bc.iniC.hostname, titleFont);
            rcPage.Width = size.Width;
            pdf.DrawString(bc.iniC.hostname, titleFont, Brushes.Black, rcPage);
            gapY += gapLine;
            rcPage.Y = gapY;
            size = bc.MeasureString(bc.iniC.hostaddresst, hdrFont);
            rcPage.Width = size.Width;
            pdf.DrawString(bc.iniC.hostaddresst, hdrFont, Brushes.Black, rcPage);
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            //gapY += gapLine;

            RectangleF rcHdr = new RectangleF();
            rcHdr.Width = 542;
            rcHdr.Height = 25;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            //rcHdr.Location
            //pdf.DrawRectangle(Pens.Black, rcHdr);       // ตาราง
            size = bc.MeasureString("ใบรับรองแพทย์", titleFont);
            rcPage.Width = size.Width;
            rcPage.Y = gapY - 2;
            rcPage.X = (542 / 2) - (size.Width / 2) + 40;
            pdf.DrawString("ใบรับรองแพทย์", titleFont, Brushes.Black, rcPage);

            gapY += gapLine + 5;
            gapY += gapLine1;
            rcPage.X = xCol41;
            rcPage.Y = gapY;
            txt = "เลขที่ผู้ป่วย HN : ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt , txtFont, Brushes.Black, rcPage);
            rcPage.Y = gapY + lineDot;
            rcPage.X = xCol41 + 65;
            //rcPage.X = xCol41 + 25;
            rcPage.Width = size.Width + 10;
            size = bc.MeasureString("......................", txtFont);
            pdf.DrawString("......................", txtFont, Brushes.Black, rcPage);
            txt = txtHn.Text.Trim();
            size = bc.MeasureString(txt, txtFontB);
            rcPage.Width = size.Width;
            rcPage.X = xCol41 + 45 +25;
            //rcPage.X = xCol41 + 25 + 5;
            rcPage.Y = gapY - lineDot;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "ข้าพเจ้า";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            //rcPage.X = gapX+10;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = txtDtrNameT.Text.Trim();
            size = bc.MeasureString(txt, txtFontB);
            rcPage.Width = size.Width;
            rcPage.X = gapX+35;
            rcPage.Y = gapY - lineDot;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            txt = "................................................................................................................... ";
            rcPage.X = gapX + 22;
            rcPage.X = gapX + 32;
            rcPage.Y = gapY + lineDot;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = gapX + size.Width - 20;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            temp = size.Width;

            txt = "แพทย์แผนปัจจุบันชั้นหนึ่งสาขาเวชกรรมเลขที่ ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            //rcPage.X = temp+25;
            rcPage.X = temp - 10;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            temp = rcPage.X + size.Width - 70;
            txt = "...................";
            size = bc.MeasureString(txt, txtFont);
            //rcPage.X = temp + 5;
            rcPage.X = temp ;
            rcPage.Y = gapY + lineDot;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = txtDtrId.Text.Trim();
            size = bc.MeasureString(txt, txtFontB);
            rcPage.Width = size.Width;
            //rcPage.X = temp + 7;
            rcPage.X = temp +10;
            rcPage.Y = gapY - lineDot;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "ขอรับรองว่า ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = ".................................................................................................................................................................................................................. ";
            rcPage.X = gapX + 33;
            rcPage.X = gapX + 50;
            rcPage.Y = gapY + lineDot;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = gapX + size.Width - 20;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = txtPttNameT.Text.Trim();
            size = bc.MeasureString(txt, txtFontB);
            rcPage.Width = size.Width;
            rcPage.X = gapX + 33 + 5;
            rcPage.X = gapX + 60;
            rcPage.Y = gapY - lineDot;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "ได้รับการตรวจร่างกาย / ทำการซักประวัติเมื่อวันที่ - เวลามาตรวจ ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            temp = size.Width ;
            txt = ".............................................................................................................................";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = temp -62;
            rcPage.X = temp - 52;
            rcPage.Y = gapY + lineDot;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = txtVsDate.Text + "  " + txtVsTime.Text;
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = rcPage.X + 5;
            rcPage.X = rcPage.X + 5+10;
            rcPage.Y = gapY - lineDot;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "ปรากฏว่าเป็น";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = "..................................................................................................................................................................................................................";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = gapX +52;
            rcPage.Y = gapY + lineDot;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = txtSympbtom.Text.Trim();
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = rcPage.X + 5;
            rcPage.Y = gapY - lineDot;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "พบว่า หรือคนในครอบครัวเดินทางกลับจากประเทศ ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            temp = size.Width;
            txt = "......................................................................................................";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = temp -30;
            rcPage.Y = gapY + lineDot;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            temp = size.Width+70;
            txt = txtCountry.Text.Trim();
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = rcPage.X + 5;
            rcPage.Y = gapY - lineDot;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            txt = "ในวันที่";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = temp+90;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            temp = size.Width;
            txt = "............................";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = rcPage.X + 25;
            rcPage.Y = gapY + lineDot;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            if (txtCountry.Text.Length > 0)
            {
                txt = txtDate.Text;
            }
            else
            {
                txt = "";
            }
            
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = temp + 360 + 95;
            rcPage.Y = gapY - lineDot;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            rcHdr = new RectangleF();
            rcHdr.Width = 8;
            rcHdr.Height = 8;
            rcHdr.X = gapX;
            rcHdr.Y = gapY+2;
            //rcHdr.Location
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            txt = "ไม่มีอาการป่วย";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX +15;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            rcHdr.X = gapX + size.Width;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkThai1.Checked)
            {
                rcPage.X = gapX+2;
                rcPage.Y = gapY - 11;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }

            txt = "ไข้";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX + 101;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            rcHdr.X = gapX + 120;
            rcPage.Y = gapY+2;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkThai2.Checked)
            {
                rcPage.X = rcPage.X - 12;
                rcPage.Y = gapY - 11;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "ไอ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX + 133;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            rcHdr.X = gapX + 170;
            rcPage.Y = gapY + 2;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkThai3.Checked)
            {
                rcPage.X = rcPage.X - 11;
                rcPage.Y = gapY - 11;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "มีน้ำมูก";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX + 183;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            rcHdr.X = gapX + 223;
            rcPage.Y = gapY + 2;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkThai4.Checked)
            {
                rcPage.X = rcPage.X -11;
                rcPage.Y = gapY - 11;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "เจ็บคอ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX + 234;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            rcHdr.X = gapX + 223;
            rcPage.Y = gapY + 2;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkThai5.Checked)
            {
                rcPage.X = rcPage.X - 9;
                rcPage.Y = gapY - 11;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            gapY += gapLine1;
            txt = "อื่นๆ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            rcPage.X = gapX + 15;
            rcPage.Y = gapY;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            rcHdr.X = gapX;
            rcHdr.Y = gapY + 2;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            if (chkThaiOther.Checked)
            {
                rcPage.X = gapX + 2;
                rcPage.Y = gapY - 11;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }
            txt = "........................................................................................................................................................................................................................";
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = gapX + 38;
            rcPage.Y = gapY + lineDot;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = txtThaiOther.Text.Trim();
            size = bc.MeasureString(txt, txtFont);
            rcPage.X = rcPage.X + 5;
            rcPage.Y = gapY - lineDot;
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            txt = "ความเห็นและการรับรองแพทย์";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "1.  รับรองว่ามารับการักษาที่โรงพยาบาลนี้ จริง";
            rcPage.X = gapX +15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "2.  ยังไม่มีอาการเข้าข่ายผู้ป่วยต้องสงสัยโรคไวรัสโคโรน่า 2019 (COVID-19)";
            rcPage.X = gapX+15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "3.  แนะนำให้สังเกตอาการ";
            rcPage.X = gapX+15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            txt = "หมายเหตุ";
            rcPage.X = gapX;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "กรณีพบว่าเดินทางกลับจากต่างประเทศที่เป็นกลุ่มเสี่ยงแนะนำให้ผู้ป่วยอยู่ที่บ้าน/ที่พัก  จนครบ 14 วัน ";
            rcPage.X = gapX+15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "นับจากวันที่กลับมาจากประเทศที่มีความเสี่ยง โดยหลีกเลี่ยงการไป ที่สาธารณะที่มีคนอยู่หนาแน่น โดยไม่จำเป็น งดใช้ของใช้ร่วมกับผู้อื่น ";
            rcPage.X = gapX+15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "ให้สวมหน้ากากอนามัน เมื่อยู่ร่วมกับผู้อื่น หมั่นล้างมือ หากภายใน 14 วัน มีไข้ ร่วมกับ ไอ จาม หรือมีอาการเหนื่อยหอบ หายใจลำบาก";
            rcPage.X = gapX+15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "ให้มาพบแพทย์ ";
            rcPage.X = gapX + 15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "ตามคำแนะนำของศูนย์ปฎิบัติการฉุกเฉินด้านการแพทย์และกระทรวงสาธารณสุข ";
            rcPage.X = gapX+15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            txt = "** เอกสารนี้ไม่สามารถใช้ประกอบการดำเนินคดีได้ ";
            rcPage.X = gapX+15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            //gapY += gapLine1;
            //gapY += gapLine1;
            //gapY += gapLine1;
            //gapY += gapLine1;
            //gapY += gapLine1;
            //gapY += gapLine1;
            //gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            rcHdr.X = gapX;
            rcHdr.Y = gapY+5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            txt = "1.  ประกอบเบิก";
            rcPage.X = gapX + 15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            if (chkDraw.Checked)
            {
                rcPage.X = gapX + 2;
                rcPage.Y = gapY - 9;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
            }

            gapY += gapLine1;
            rcHdr.X = gapX;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            txt = "2.  เข้าพักรักษาตัวในโรงพยาบาล  ตั้งแต่วันที่ ............................... ถึง ...............................";
            rcPage.X = gapX + 15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            if (chkHosp.Checked)
            {
                rcPage.X = gapX + 2;
                rcPage.Y = gapY - 9;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
                txt = txtHospStartDate.Text.Trim();
                size = bc.MeasureString(txt, txtFont);
                rcPage.X += 185;
                rcPage.Y = gapY - lineDot-4;
                rcPage.Width = size.Width;
                pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);

                txt = txtHospEndDate.Text.Trim();
                size = bc.MeasureString(txt, txtFont);
                rcPage.X += 85;
                rcPage.Y = gapY - lineDot-4;
                rcPage.Width = size.Width;
                pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);
            }

            gapY += gapLine1;
            rcHdr.X = gapX;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            txt = "3.  หยุดพักรักษาตัว  (หยุดงาน)  มีกำหนด ............... วัน   ตั้งแต่วันที่ ............................... ถึง ............................... ";
            rcPage.X = gapX + 15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            if (chkStop.Checked)
            {
                rcPage.X = gapX + 2;
                rcPage.Y = gapY - 9;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
                txt = txtStopStartDate.Text.Trim();
                size = bc.MeasureString(txt, txtFont);
                rcPage.X += 270;
                rcPage.Y = gapY - lineDot-4;
                rcPage.Width = size.Width;
                pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);
                txt = txtStopEndDate.Text.Trim();
                size = bc.MeasureString(txt, txtFont);
                rcPage.X += 80;
                rcPage.Y = gapY - lineDot-4;
                rcPage.Width = size.Width;
                pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);
                txt = txtStop.Text.Trim();
                size = bc.MeasureString(txt, txtFont);
                rcPage.X = 220;
                rcPage.Y = gapY - lineDot - 4;
                rcPage.Width = size.Width;
                pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);
            }

            gapY += gapLine1;
            rcHdr.X = gapX;
            rcHdr.Y = gapY + 5;
            pdf.DrawRectangle(Pens.Black, rcHdr, new SizeF(1, 1));       // ตาราง
            txt = "4.  มารับการตรวจจริงเมื่อเวลา ..................... ";
            rcPage.X = gapX + 15;
            rcPage.Y = gapY;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            if (chkTrue.Checked)
            {
                rcPage.X = gapX + 2;
                rcPage.Y = gapY - 9;
                txt = "/";
                pdf.DrawString(txt, titleFont, Brushes.Black, rcPage);
                txt = txtTrue.Text.Trim();
                size = bc.MeasureString(txt, txtFont);
                rcPage.X = rcPage.X + 130;
                rcPage.Y = gapY - lineDot-4;
                rcPage.Width = size.Width;
                pdf.DrawString(txt, txtFontB, Brushes.Black, rcPage);
            }

            gapY += gapLine1;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            txt = "(กรณีที่4  ใช้รับรองว่ามารับการตรวจรักษาจริงเท่านั้น  มิใช่เป็นใบรับรองแพทย์ลาป่วย)";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            rcPage.X = xCol41-40;
            rcPage.Y = gapY-30;
            txt = "ลงชื่อ ...........................................................";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = ".........................................................";
            rcPage.X = gapX+30;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);
            txt = "ผู้รับเอกสารใบรับรองแพทย์";
            rcPage.X = gapX + 30 + 10;
            rcPage.Y = gapY+13;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = xCol41;
            rcPage.Y = gapY-30;
            txt = txtDtrNameT.Text.Trim() +" "+txtDtrId.Text;
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = xCol41;
            rcPage.Y = gapY-30;
            txt = "แพทย์ผู้ตรวจรักษา ";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

            //gapY += gapLine1;
            //gapY += gapLine1;
            gapY += gapLine1;
            //gapY += gapLine1;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            txt = "FM-MED-022 (00-05/3/62)(1/1)";
            size = bc.MeasureString(txt, txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);


            pathFolder = bc.iniC.medicalrecordexportpath + "\\COVID\\";
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            datetick = DateTime.Now.Ticks.ToString();
            filename = pathFolder + "\\" + txtHn.Text.TrimEnd() + "_" + datetick + ".pdf";
            pdf.Save(filename);
            pdf.Clear();
            pdf.Dispose();

            if (File.Exists(filename))
            {
                Process p = new Process();
                ProcessStartInfo s = new ProcessStartInfo(filename);
                p.StartInfo = s;
                p.Start();
            }
        }
        private void printCOVID(String flag)
        {
            String pathFolder = "", filename="", datetick="";
            int gapLine = 20,gapLine1 = 15, gapX = 40, gapY = 20, xCol2 = 200, xCol1 = 160, xCol3 = 300, xCol4 = 390, xCol5 = 500;
            Size size = new Size();

            C1PdfDocument pdf = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            StringFormat _sfRight, _sfRightCenter;
            //Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            Font titleFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetitleFont, FontStyle.Bold);
            Font hdrFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizehdrFont, FontStyle.Regular);
            Font hdrFontB = new Font(bc.iniC.pdfFontName, 16, FontStyle.Bold);
            Font ftrFont = new Font(bc.iniC.pdfFontName, 8);
            Font txtFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFont, FontStyle.Regular);
            pdf.FontType = FontTypeEnum.Embedded;
            
            RectangleF rcPage = pdf.PageRectangle;
            rcPage = RectangleF.Empty;
            rcPage.Inflate(-72, -92);
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 110;

            Image loadedImage;
            loadedImage = Resources.LOGO_BW_tran;
            float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
            float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

            float widthFactor = 4.8F;
            float heightFactor = 4.8F;

            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    widthFactor = 1;
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                    //newWidth = newWidth / 1.2;
                    //newHeight = newHeight / 1.2;
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }

            RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            pdf.DrawImage(loadedImage, recf);
            rcPage.X = gapX + recf.Width - 10;
            rcPage.Y = gapY;
            if (flag.Equals("thai"))
            {
                size = bc.MeasureString(bc.iniC.hostname, titleFont);
                rcPage.Width = size.Width;
                pdf.DrawString(bc.iniC.hostname, titleFont, Brushes.Black, rcPage);
                gapY += gapLine;
                rcPage.Y = gapY;
                size = bc.MeasureString(bc.iniC.hostaddresst, hdrFont);
                rcPage.Width = size.Width;
                pdf.DrawString(bc.iniC.hostaddresst, hdrFont, Brushes.Black, rcPage);
            }
            else
            {
                size = bc.MeasureString(bc.iniC.hostnamee, titleFont);
                rcPage.Width = size.Width;
                pdf.DrawString(bc.iniC.hostnamee, titleFont, Brushes.Black, rcPage);
                gapY += gapLine;
                rcPage.Y = gapY;
                size = bc.MeasureString(bc.iniC.hostaddresse, hdrFont);
                rcPage.Width = size.Width;
                pdf.DrawString(bc.iniC.hostaddresse, hdrFont, Brushes.Black, rcPage);
            }
            

            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            //gapY += gapLine;

            RectangleF rcHdr = new RectangleF();
            rcHdr.Width = 542;
            rcHdr.Height = 25;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            //rcHdr.Location
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตาราง
            size = bc.MeasureString("LABORATORY RESULT", titleFont);
            rcPage.Width = size.Width;
            rcPage.Y = gapY-2;
            rcPage.X = (542 / 2) - (size.Width / 2) + 40;
            pdf.DrawString("LABORATORY RESULT", titleFont, Brushes.Black, rcPage);

            gapY += gapLine +5;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            size = bc.MeasureString("HN : " + txtHn.Text.Trim(), txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString("HN : " + txtHn.Text.Trim(), txtFont, Brushes.Black, rcPage);
            if (flag.Equals("thai"))
            {
                size = bc.MeasureString("SEX : " + txtSex.Text.Trim(), txtFont);
                rcPage.Width = size.Width;
                rcPage.X = xCol5;
                pdf.DrawString("SEX : " + txtSex.Text.Trim(), txtFont, Brushes.Black, rcPage);
            }
            else
            {
                size = bc.MeasureString("SEX : " + txtSex.Text.Trim(), txtFont);
                rcPage.Width = size.Width;
                rcPage.X = xCol5;
                pdf.DrawString("SEX : " + txtSex.Text.Trim(), txtFont, Brushes.Black, rcPage);
            }
            

            gapY += gapLine1;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            if (flag.Equals("thai"))
            {
                size = bc.MeasureString("ชื่อ-นามสกุล/Name : " + txtPttNameT.Text.Trim(), txtFont);
                rcPage.Width = size.Width;
                pdf.DrawString("ชื่อ-นามสกุล/Name : " + txtPttNameT.Text.Trim(), txtFont, Brushes.Black, rcPage);
            }
            else
            {
                size = bc.MeasureString("Name : " + txtPttNameE.Text.Trim(), txtFont);
                rcPage.Width = size.Width;
                pdf.DrawString("Name : " + txtPttNameE.Text.Trim(), txtFont, Brushes.Black, rcPage);
            }

            rcPage.Width = bc.MeasureString("วันเกิด/DOB : " + txtDOB.Text.Trim(), txtFont).Width;
            rcPage.X = xCol3;
            pdf.DrawString("DOB : " + txtDOB.Text.Trim(), txtFont, Brushes.Black, rcPage);

            rcPage.Width = bc.MeasureString("AGE : " + txtAge.Text.Trim(), txtFont).Width;
            rcPage.X = xCol5;
            pdf.DrawString("AGE : " + txtAge.Text.Trim(), txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            size = bc.MeasureString("ID : " + txtPID.Text.Trim(), txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString("ID : " + txtPID.Text.Trim(), txtFont, Brushes.Black, rcPage);

            rcPage.Width = bc.MeasureString("PASSPORT : " + txtPassport.Text.Trim(), txtFont).Width;
            rcPage.X = xCol3;
            pdf.DrawString("PASSPORT : " + txtPassport.Text.Trim(), txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            size = bc.MeasureString("Department : OPD-COVID", txtFont);
            rcPage.Width = size.Width;
            pdf.DrawString("Department : OPD-COVID", txtFont, Brushes.Black, rcPage);

            rcPage.Width = bc.MeasureString("COLLECTED Date/Time : ", txtFont).Width;
            rcPage.X = xCol4;
            pdf.DrawString("COLLECTED Date/Time : ", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("RECEIVED Date/Time", txtFont).Width;
            rcPage.X = xCol4;
            pdf.DrawString("RECEIVED Date/Time", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1+5;
            pdf.DrawLine(Pens.Black, gapX, gapY, rcHdr.X + rcHdr.Width, gapY);

            //gapY += gapLine1;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("DESCRIPTION", txtFont).Width;
            pdf.DrawString("DESCRIPTION", txtFont, Brushes.Black, rcPage);
            rcPage.Width = bc.MeasureString("RECEIVED Date/Time", txtFont).Width;
            rcPage.X = xCol3+40;
            pdf.DrawString("RESULT", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol4+30;
            pdf.DrawString("Unit", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol5;
            pdf.DrawString("Reference Value", txtFont, Brushes.Black, rcPage);
            //rcPage.X = xCol5;
            //pdf.DrawString("Remark", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1+3;
            pdf.DrawLine(Pens.Black, gapX, gapY, rcHdr.X + rcHdr.Width, gapY);

            //gapY += gapLine1;
            rcPage.Y = gapY + 5;
            rcPage.X = gapX + 10;
            rcPage.Width = bc.MeasureString(txtLabName.Text.Trim(), txtFont).Width;
            pdf.DrawString(txtLabName.Text.Trim(), txtFont, Brushes.Black, rcPage);

            rcPage.X = xCol3+40;
            pdf.DrawString(txtLabResult.Text.Trim(), txtFont, Brushes.Black, rcPage);         //      result

            rcPage.X = xCol4+30;
            //pdf.DrawString("-", txtFont, Brushes.Black, rcPage);                    // unit
            pdf.DrawString(txtLabUnit.Text.Trim(), txtFont, Brushes.Black, rcPage);

            rcPage.X = xCol5;
            //pdf.DrawString("Negative", txtFont, Brushes.Black, rcPage);             // normal
            pdf.DrawString(txtLabNormal.Text.Trim(), txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            rcPage.X = gapX + 10;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("Specimen :", txtFont).Width;
            pdf.DrawString("Specimen :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol1;
            pdf.DrawString("Nasopharyngeal and throat swab", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = gapX + 10;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("Detection Method :", txtFont).Width;
            pdf.DrawString("Detection Method :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol1;
            //pdf.DrawString("Real Time PCR", txtFont, Brushes.Black, rcPage);
            pdf.DrawString(txtLabName1.Text.Trim(), txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = gapX + 10;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("Gene Detection :", txtFont).Width;
            pdf.DrawString("Gene Detection :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol1;
            //pdf.DrawString("ORF1ab and N gene", txtFont, Brushes.Black, rcPage);
            pdf.DrawString(txtLabName2.Text.Trim(), txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = gapX + 10;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("Reagent :", txtFont).Width;
            pdf.DrawString("Reagent :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol1;
            //pdf.DrawString("Da An gene", txtFont, Brushes.Black, rcPage);
            pdf.DrawString(txtLabName3.Text.Trim(), txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.X = gapX + 10;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("Internal Control :", txtFont).Width;
            pdf.DrawString("Internal Control :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol1;
            pdf.DrawString("Valid Internal Control", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1 + 5;
            pdf.DrawLine(Pens.Black, gapX, gapY, rcHdr.X + rcHdr.Width, gapY);

            rcPage.Y = gapY + 5;
            rcPage.X = gapX + 10;
            rcPage.Width = bc.MeasureString("Report By :", txtFont).Width;
            pdf.DrawString("Report By :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol1;
            rcPage.Width = bc.MeasureString(txtLabReport.Text.Trim(), txtFont).Width;
            pdf.DrawString(txtLabReport.Text.Trim(), txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol4;
            rcPage.Width = bc.MeasureString("Approved By :", txtFont).Width;
            pdf.DrawString("Approved By :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol4 + rcPage.Width-10;
            rcPage.Width = bc.MeasureString(txtLabApprove.Text.Trim(), txtFont).Width;
            pdf.DrawString(txtLabApprove.Text.Trim(), txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.Y = gapY + 5;
            rcPage.X = gapX + 10;
            rcPage.Width = bc.MeasureString("Report Date :", txtFont).Width;
            pdf.DrawString("Report Date :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol1;
            rcPage.Width = bc.MeasureString(txtLabReportDate.Text.Trim(), txtFont).Width;
            pdf.DrawString(txtLabReportDate.Text.Trim(), txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol4;
            rcPage.Width = bc.MeasureString("Approved By :", txtFont).Width;
            pdf.DrawString("Approved Date :", txtFont, Brushes.Black, rcPage);
            rcPage.X = xCol4 + rcPage.Width - 10;
            rcPage.Width = bc.MeasureString(txtLabApproveDate.Text.Trim(), txtFont).Width;
            pdf.DrawString(txtLabApproveDate.Text.Trim(), txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.Y = gapY + 5;
            rcPage.X = gapX + 10;
            rcPage.Width = bc.MeasureString("Print By :", txtFont).Width;
            pdf.DrawString("Print By :", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.Y = gapY + 5;
            rcPage.X = gapX + 10;
            rcPage.Width = bc.MeasureString("LL,HH = Flag Critical Lab Value L,H = Lab Value that are low or higher than the reference range A = Qualitative result R", txtFont).Width;
            pdf.DrawString("LL,HH = Flag Critical Lab Value L,H = Lab Value that are low or higher than the reference range A = Qualitative result R", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            rcHdr.Height = 40;
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตาราง

            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("This result certifies only the result on the specimen/sample we obtained may.", txtFont).Width;
            pdf.DrawString("This result certifies only the result on the specimen/sample we obtained may.", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("Not be reproduced in print or any other media without written permission", txtFont).Width;
            pdf.DrawString("Not be reproduced in print or any other media without written permission", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString("# Refer to the tests approved and certified by ISO 15189, item without # Are outside  the scope of ISO 15189", txtFont).Width;
            pdf.DrawString("# Refer to the tests approved and certified by ISO 15189, item without # Are outside  the scope of ISO 15189", txtFont, Brushes.Black, rcPage);

            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            gapY += gapLine1;
            rcPage.X = xCol4;
            rcPage.Y = gapY;
            rcPage.Width = bc.MeasureString(txtDtrId.Text.Trim()+" "+ txtDtrNameT.Text.Trim(), txtFont).Width;
            pdf.DrawString(txtDtrId.Text.Trim() + " " + txtDtrNameT.Text.Trim(), txtFont, Brushes.Black, rcPage);
            pdf.DrawLine(Pens.Black, xCol4 - 20, gapY, (xCol4 - 20) + rcPage.Width + 0, gapY);
            
            rcPage.Width = bc.MeasureString("แพทย์ผู้ตรวจ/Physician", txtFont).Width;
            rcPage.X = xCol4+15;
            rcPage.Y = gapY + 14;
            pdf.DrawString("แพทย์ผู้ตรวจ/Physician", txtFont, Brushes.Black, rcPage);

            pathFolder = bc.iniC.medicalrecordexportpath + "\\COVID\\";
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            datetick = DateTime.Now.Ticks.ToString();
            filename = pathFolder + "\\" + txtHn.Text.TrimEnd() + "_"+ datetick + ".pdf";
            pdf.Save(filename);
            pdf.Clear();
            pdf.Dispose();

            if (File.Exists(filename))
            {
                Process p = new Process();
                ProcessStartInfo s = new ProcessStartInfo(filename);
                p.StartInfo = s;
                p.Start();
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
        private void initCompoment()
        {
            int gapLine = 30, gapX = 20, gapY = 20, col1 = 165, col2 = 330, col3 = 630, col4=750, col5=850, col6=1050;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            
            tcMain = new C1DockingTab();
            tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMain.Location = new System.Drawing.Point(0, 266);
            tcMain.Name = "tcDtr";
            tcMain.Size = new System.Drawing.Size(669, 200);
            tcMain.TabIndex = 0;
            tcMain.TabsSpacing = 5;

            tabAdd = new C1DockingTabPage();
            tabAdd.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabAdd.Size = new System.Drawing.Size(667, 175);
            tabAdd.TabIndex = 0;
            tabAdd.Text = "ใบยา / Staff's Note";
            tabAdd.Name = "tabAdd";
            tcMain.Controls.Add(tabAdd);

            tabView = new C1DockingTabPage();
            tabView.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabView.Size = new System.Drawing.Size(667, 175);
            tabView.TabIndex = 0;
            tabView.Text = "ประวัติ";
            tabView.Name = "tabView";
            tcMain.Controls.Add(tabView);

            lbtxtHospName = new Label();
            bc.setControlLabel(ref lbtxtHospName, fEdit, "โรงพยาบาล", "lbTxtHospName", gapX, gapY);
            txtHospName = new C1TextBox();
            bc.setControlC1TextBox(ref txtHospName, fEdit, "txtHospName", 250, col1, gapY);

            gapY += gapLine;
            lbtxtHn = new Label();
            bc.setControlLabel(ref lbtxtHn, fEdit, "HN :", "lbTxtHn", gapX, gapY);
            txtHn = new C1TextBox();
            bc.setControlC1TextBox(ref txtHn, fEdit, "txtHn", 90, col1, gapY);
            btnHn = new C1Button();
            bc.setControlC1Button(ref btnHn, fEdit, "...", "btnHn", txtHn.Location.X + txtHn.Width+5, gapY-3);
            btnHn.Width = 30;
            lbtxtVsDate = new Label();
            bc.setControlLabel(ref lbtxtVsDate, fEdit, "วันที่ตรวจ :", "lbtxtVsDate", btnHn.Location.X + btnHn.Width + 20, gapY);
            size = bc.MeasureString(lbtxtVsDate);
            txtVsDate = new C1DateEdit();
            bc.setControlC1DateTimeEdit(ref txtVsDate, "txtVsDate", lbtxtVsDate.Location.X + size.Width + 5, gapY);
            txtVsTime = new C1TextBox();
            bc.setControlC1TextBox(ref txtVsTime, fEdit, "txtVsTime",60, txtVsDate.Location.X + size.Width + 40, gapY);

            gapY += gapLine;
            lbtxtPttNameT = new Label();
            bc.setControlLabel(ref lbtxtPttNameT, fEdit, "ชื่อ-นามสกุล :", "lbtxtPttNameT", gapX, gapY);
            txtPttNameT = new C1TextBox();
            bc.setControlC1TextBox(ref txtPttNameT, fEdit, "txtPttName", 300, col1, gapY);

            gapY += gapLine;
            lbtxtPttNameE = new Label();
            bc.setControlLabel(ref lbtxtPttNameE, fEdit, "Name English :", "lbtxtPttNameE", gapX, gapY);
            txtPttNameE = new C1TextBox();
            bc.setControlC1TextBox(ref txtPttNameE, fEdit, "txtPttNameE", 300, col1, gapY);

            gapY += gapLine;
            lbtxtSex = new Label();
            bc.setControlLabel(ref lbtxtSex, fEdit, "เพศ :", "lbtxtSex", gapX, gapY);
            txtSex = new C1TextBox();
            bc.setControlC1TextBox(ref txtSex, fEdit, "txtSex", 80, col1, gapY);
            lbtxtAge = new Label();
            bc.setControlLabel(ref lbtxtAge, fEdit, "อายุ :", "lbtxtAge", txtSex.Location.X + txtSex.Width + 20, gapY);
            size = bc.MeasureString(lbtxtAge);
            txtAge = new C1TextBox();
            bc.setControlC1TextBox(ref txtAge, fEdit, "txtAge", 80, lbtxtAge.Location.X + size.Width + 5, gapY);
            lbtxtDOB = new Label();
            bc.setControlLabel(ref lbtxtDOB, fEdit, "DOB :", "lbtxtDOB", txtAge.Location.X + txtAge.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDOB);
            txtDOB = new C1TextBox();
            bc.setControlC1TextBox(ref txtDOB, fEdit, "txtDOB", 100, lbtxtDOB.Location.X + size.Width + 20, gapY);
            lbtxtNation = new Label();
            bc.setControlLabel(ref lbtxtNation, fEdit, "สัญชาติ :", "lbtxtNation", txtDOB.Location.X + txtDOB.Width + 20, gapY);
            size = bc.MeasureString(lbtxtNation);
            txtNation = new C1TextBox();
            bc.setControlC1TextBox(ref txtNation, fEdit, "txtNation", 100, lbtxtNation.Location.X + size.Width + 20, gapY);

            gapY += gapLine;
            lbtxtPID = new Label();
            bc.setControlLabel(ref lbtxtPID, fEdit, "บัตรประชาชน :", "lbtxtPID", gapX, gapY);
            txtPID = new C1TextBox();
            bc.setControlC1TextBox(ref txtPID, fEdit, "txtSex", 160, col1, gapY);
            lbtxtPassport = new Label();
            bc.setControlLabel(ref lbtxtPassport, fEdit, "Passport :", "lbtxtPassport", txtPID.Location.X + txtPID.Width + 20, gapY);
            size = bc.MeasureString(lbtxtPassport);
            txtPassport = new C1TextBox();
            bc.setControlC1TextBox(ref txtPassport, fEdit, "txtPassport", 120, lbtxtPassport.Location.X + size.Width + 5, gapY);

            gapY += gapLine;
            lbtxtAddr1 = new Label();
            bc.setControlLabel(ref lbtxtAddr1, fEdit, "สถานที่อยู่ :", "lbtxtAddr1", gapX, gapY);
            txtAddr1 = new C1TextBox();
            bc.setControlC1TextBox(ref txtAddr1, fEdit, "txtAddr1", 400, col1, gapY);
            gapY += gapLine;
            txtAddr2 = new C1TextBox();
            bc.setControlC1TextBox(ref txtAddr2, fEdit, "txtAddr2", 400, txtAddr1.Location.X, gapY);

            gapY += gapLine;
            lbtxtDtrId = new Label();
            bc.setControlLabel(ref lbtxtDtrId, fEdit, "แพทย์ผู้ตรวจ :", "lbtxtDtrId", gapX, gapY);
            txtDtrId = new C1TextBox();
            bc.setControlC1TextBox(ref txtDtrId, fEdit, "txtDtrId", 80, col1, gapY);
            txtDtrNameT = new C1TextBox();
            bc.setControlC1TextBox(ref txtDtrNameT, fEdit, "txtDtrName", 300, txtDtrId.Location.X + txtDtrId.Width + 10, gapY);

            pnPrint = new Panel();
            pnPrint.Location = new Point(txtDtrNameT.Location.X + txtDtrNameT.Width + 40, gapY);
            pnPrint.Size = new Size(500, 50);
            //pnPrint.BackColor = Color.Red;
            chkEng = new RadioButton();
            bc.setControlRadioBox(ref chkEng, fEdit, "English ", "chkEng", 10, 10);
            size = bc.MeasureString(chkEng);
            chkThai = new RadioButton();
            bc.setControlRadioBox(ref chkThai, fEdit, "Thai  ", "chkThai", size.Width + 30, chkEng.Location.Y);

            btnPrnThai = new C1Button();
            bc.setControlC1Button(ref btnPrnThai, fEdit, "Print Cert", "btnPrnThai", chkThai.Location.X + chkThai.Width + 40, chkEng.Location.Y);
            btnPrnEng = new C1Button();
            bc.setControlC1Button(ref btnPrnEng, fEdit, "Print Lab", "btnPrnEng", btnPrnThai.Location.X + btnPrnThai.Width + 20, chkEng.Location.Y);
            pnPrint.Controls.Add(btnPrnThai);
            pnPrint.Controls.Add(btnPrnEng);
            pnPrint.Controls.Add(chkEng);
            pnPrint.Controls.Add(chkThai);

            gapY += gapLine;
            txtDtrNameE = new C1TextBox();
            bc.setControlC1TextBox(ref txtDtrNameE, fEdit, "txtDtrNameE", 300, txtDtrNameT.Location.X, gapY);

            gapY += gapLine;
            lbtxtLabResult = new Label();
            bc.setControlLabel(ref lbtxtLabResult, fEdit, "Result", "lbtxtLabResult", col4, gapY);
            lbtxtLabUnit = new Label();
            bc.setControlLabel(ref lbtxtLabUnit, fEdit, "Unit", "lbtxtLabUnit", col5, gapY);
            lbtxtLabNormal = new Label();
            bc.setControlLabel(ref lbtxtLabNormal, fEdit, "Normal", "lbtxtLabNormal", col6, gapY);

            gapY += gapLine;
            lbtxtLabCode = new Label();
            bc.setControlLabel(ref lbtxtLabCode, fEdit, "Lab code :", "lbtxtLabCode", gapX, gapY);
            txtLabCode = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabCode, fEdit, "txtLabCode", 80, col1, gapY);
            txtLabName = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabName, fEdit, "txtLabName", 450, txtLabCode.Location.X + txtLabCode.Width + 10, gapY);
            
            txtLabResult = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabResult, fEdit, "txtLabResult", 120, txtLabName.Location.X + txtLabName.Width + 5, gapY);
            
            txtLabUnit = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabUnit, fEdit, "txtLabUnit", 160, txtLabResult.Location.X + txtLabResult.Width + 5, gapY);

            txtLabNormal = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabNormal, fEdit, "txtLabNormal", 160, txtLabUnit.Location.X + txtLabUnit.Width + 5, gapY);
            
            gapY += gapLine;
            lbtxtLabName1 = new Label();
            bc.setControlLabel(ref lbtxtLabName1, fEdit, "Detection Method :", "lbtxtLabName1", gapX, gapY);
            txtLabName1 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabName1, fEdit, "txtLabName1", 160, col1, gapY);
            txtLabResult1 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabResult1, fEdit, "txtLabResult1", 200, txtLabName1.Location.X + txtLabName1.Width + 5, gapY);
            gapY += gapLine;
            lbtxtLabName2 = new Label();
            bc.setControlLabel(ref lbtxtLabName2, fEdit, "Gene Detection :", "lbtxtLabName2", gapX, gapY);
            txtLabName2 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabName2, fEdit, "txtLabName2", 160, col1, gapY);
            txtLabResult2 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabResult2, fEdit, "txtLabResult2", 200, txtLabName2.Location.X + txtLabName2.Width + 5, gapY);
            gapY += gapLine;
            lbtxtLabName3 = new Label();
            bc.setControlLabel(ref lbtxtLabName3, fEdit, "Reagent :", "lbtxtLabName3", gapX, gapY);
            txtLabName3 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabName3, fEdit, "txtLabName3", 160, col1, gapY);
            txtLabResult3 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabResult3, fEdit, "txtLabResult3", 200, txtLabName3.Location.X + txtLabName3.Width + 5, gapY);

            gapY += gapLine;
            chkLab184Nas = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkLab184Nas, fEdit, "Nasopharyngeal", "chkLab184Nas", gapX, gapY);
            size = bc.MeasureString(chkLab184Nas);
            chkLab184Nucl = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkLab184Nucl, fEdit, "Nucleic acid real time RT-PCR", "chkLab184Nucl", chkLab184Nas.Location.X + size.Width + 30, gapY);
            size = bc.MeasureString(chkLab184Nucl);
            chkLab184Saliva = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkLab184Saliva, fEdit, "Saliva", "chkLab184Saliva", chkLab184Nucl.Location.X + size.Width + 30, gapY);
            size = bc.MeasureString(chkLab184Saliva);
            chkLab184Lamp = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkLab184Lamp, fEdit, "Nucleic acid LAMP", "chkLab184Lamp", chkLab184Saliva.Location.X + size.Width + 30, gapY);
            size = bc.MeasureString(chkLab184Lamp);
            chkLab184Antigen = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkLab184Antigen, fEdit, "Antigen test", "chkLab184Antigen", chkLab184Lamp.Location.X + size.Width + 30, gapY);

            gapY += gapLine;
            chktxtLabCodeSe184 = new C1CheckBox();
            bc.setControlC1CheckBox(ref chktxtLabCodeSe184, fEdit, "Lab SE184 :", "chktxtLabCodeSe184", gapX, gapY);
            txtLabCodeSe184 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabCodeSe184, fEdit, "txtLabCodeSe184", 80, col1, gapY);
            txtLabNameSe184 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabNameSe184, fEdit, "txtLabNameSe184", 300, txtLabCodeSe184.Location.X + txtLabCodeSe184.Width + 10, gapY);

            //size = bc.MeasureString(chkLab184Lamp);
            chklab184IgM = new C1CheckBox();
            bc.setControlC1CheckBox(ref chklab184IgM, fEdit, "COVID-19 IgM", "chklab184IgM", txtLabNameSe184.Location.X + txtLabNameSe184.Width + 25, gapY);

            txtLabResultSe184 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabResultSe184, fEdit, "txtLabResultSe184", 120, chklab184IgM.Location.X + chklab184IgM.Width + 20, gapY);
            txtLabUnitSe184 = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabUnitSe184, fEdit, "txtLabUnitSe184", 160, txtLabResultSe184.Location.X+ txtLabResultSe184.Width+10, gapY);
            

            gapY += gapLine;
            chklab184IgG = new C1CheckBox();
            bc.setControlC1CheckBox(ref chklab184IgG, fEdit, "COVID-19 IgG", "chklab184IgG", chklab184IgM.Location.X, gapY);
            txtlab184IgG = new C1TextBox();
            bc.setControlC1TextBox(ref txtlab184IgG, fEdit, "txtlab184G", 120, txtLabResultSe184.Location.X, gapY);


            gapY += gapLine;
            gapY += gapLine;
            lbtxtLabReport = new Label();
            bc.setControlLabel(ref lbtxtLabReport, fEdit, "Report by :", "lbtxtLabReport", gapX, gapY);
            txtLabReport = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabReport, fEdit, "txtLabReport", 300, col1, gapY);
            //txtLabReportDate = new C1DateEdit();
            //bc.setControlC1DateTimeEdit(ref txtLabReportDate, "txtLabReportDate", txtLabReport.Location.X+ txtLabReport.Width+5, gapY);
            txtLabReportDate = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabReportDate, fEdit, "txtLabReportDate", 120, txtLabReport.Location.X + txtLabReport.Width + 5, gapY);

            gapY += gapLine;
            lbtxtLabApprove = new Label();
            bc.setControlLabel(ref lbtxtLabApprove, fEdit, "Approced by :", "lbtxtLabApprove", gapX, gapY);
            txtLabApprove = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabApprove, fEdit, "txtLabApprove", 300, col1, gapY);
            txtLabApproveDate = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabApproveDate, fEdit, "txtLabApproveDate", 100, txtLabApprove.Location.X+ txtLabApprove.Width+5, gapY);

            gapY += gapLine;
            pnThai = new Panel();
            pnThai.Location = new Point(gapX, gapY);
            pnThai.Size = new Size(1200, 130);
            pnThai.BackColor = Color.Yellow;
            lbtxtSympbtom = new Label();
            bc.setControlLabel(ref lbtxtSympbtom, fEdit, "ปรากฏว่าเป็น :", "lbtxtSympbtom", gapX-20, 5);
            size = bc.MeasureString(lbtxtSympbtom);
            txtSympbtom = new C1TextBox();
            bc.setControlC1TextBox(ref txtSympbtom, fEdit, "txtSympbtom", 200, gapX -20 + size.Width +5, lbtxtSympbtom.Location.Y);
            txtSympbtom.Value = "ไม่พบเชื้อโควิด-19";

            lbtxtCountry = new Label();
            bc.setControlLabel(ref lbtxtCountry, fEdit, "เดินทางกลับจากต่างประเทศ :", "lbtxtCountry", txtSympbtom.Location.X+txtSympbtom.Width+15, lbtxtSympbtom.Location.Y);
            size = bc.MeasureString(lbtxtCountry);
            txtCountry = new C1TextBox();
            bc.setControlC1TextBox(ref txtCountry, fEdit, "txtCountry", 300, lbtxtCountry.Location.X + size.Width+5, lbtxtCountry.Location.Y);
            lbtxtDate = new Label();
            bc.setControlLabel(ref lbtxtDate, fEdit, "ในวันที่ :", "lbtxtDate", txtCountry.Location.X + txtCountry.Width + 15, lbtxtSympbtom.Location.Y);
            size = bc.MeasureString(lbtxtDate);
            txtDate = new C1DateEdit();
            bc.setControlC1DateTimeEdit(ref txtDate, "txtDate", lbtxtDate.Location.X + size.Width + 5, lbtxtCountry.Location.Y);

            gapY = 10;
            gapY += gapLine;
            chkThai1 = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkThai1, fEdit, "ไม่มีอาการป่วย", "chkThai1", gapX, gapY);
            size = bc.MeasureString(lbtxtDate);
            chkThai2 = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkThai2, fEdit, "ไข้", "chkThai2", chkThai1.Location.X+ size.Width + 70, gapY);
            chkThai3 = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkThai3, fEdit, "ไอ", "chkThai3", chkThai2.Location.X + size.Width + 20, gapY);
            chkThai4 = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkThai4, fEdit, "มีน้ำมูก", "chkThai4", chkThai3.Location.X + size.Width + 30, gapY);
            chkThai4.Height += 3;
            chkThai5 = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkThai5, fEdit, "เจ็บคอ", "chkThai5", chkThai4.Location.X + size.Width + 40, gapY);
            //gapY += gapLine;
            //gapY += gapLine;
            chkThaiOther = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkThaiOther, fEdit, "อื่นๆ", "chkThaiOther", chkThai5.Location.X + 85, gapY);
            txtThaiOther = new C1TextBox();
            bc.setControlC1TextBox(ref txtThaiOther, fEdit, "txtThaiOther", 250, chkThaiOther.Location.X + 70, chkThai5.Location.Y);
            gapY += gapLine;
            chkDraw = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkDraw, fEdit, "1. ประกอบเบิก", "chkDraw", gapX, gapY);
            size = bc.MeasureString(chkDraw);
            chkHosp = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkHosp, fEdit, "2. เข้าพักรักษาตัว วันที่ ", "chkHosp", chkDraw.Location.X+size.Width + 30, gapY);
            size = bc.MeasureString(chkHosp);
            txtHospStartDate = new C1DateEdit();
            bc.setControlC1DateTimeEdit(ref txtHospStartDate, "txtHospStartDate", chkHosp.Location.X + size.Width +30, gapY);
            lbtxtHospEndDate = new Label();
            bc.setControlLabel(ref lbtxtHospEndDate, fEdit, "ถึง ", "lbtxtHospEndDate", txtHospStartDate.Location.X + txtHospStartDate.Width , gapY);
            size = bc.MeasureString(lbtxtHospEndDate);
            txtHospEndDate = new C1DateEdit();
            bc.setControlC1DateTimeEdit(ref txtHospEndDate, "txtHospEndDate", lbtxtHospEndDate.Location.X + size.Width + 5, gapY);
            chkStop = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkStop, fEdit, "3. หยุดพักรักษาตัว จำนวนวัน ", "chkHosp", txtHospEndDate.Location.X + txtHospEndDate.Width + 30, gapY);
            chkStop.Height += 3;
            chkStop.Width = chkStop.Width - 10;
            txtStop = new C1TextBox();
            bc.setControlC1TextBox(ref txtStop, fEdit, "txtStop", 50, chkStop.Location.X + chkStop.Width + 5, gapY);
            txtStopStartDate = new C1DateEdit();
            bc.setControlC1DateTimeEdit(ref txtStopStartDate, "txtStopStartDate", txtStop.Location.X + 70, gapY);
            lbtxtStopEndDate = new Label();
            bc.setControlLabel(ref lbtxtStopEndDate, fEdit, "ถึง ", "lbtxtStopEndDate", txtStopStartDate.Location.X + txtStopStartDate.Width, gapY);
            txtStopEndDate = new C1DateEdit();
            bc.setControlC1DateTimeEdit(ref txtStopEndDate, "txtStopEndDate", txtStopStartDate.Location.X + 140, gapY);

            gapY += gapLine;
            chkTrue = new C1CheckBox();
            bc.setControlC1CheckBox(ref chkTrue, fEdit, "4. มารับการตรวจจริงเมื่อเวลา ", "chkTrue", gapX, gapY);
            chkTrue.Width = chkTrue.Width - 10;
            txtTrue = new C1TextBox();
            bc.setControlC1TextBox(ref txtTrue, fEdit, "txtTrue", 50, chkTrue.Location.X + chkTrue.Width + 5, gapY);

            gapY += gapLine;
            pnEng = new Panel();
            pnEng.Location = new Point(gapX, pnThai.Location.Y+ pnThai.Width+30);
            pnEng.Size = new Size(600, 100);
            pnEng.BackColor = Color.Red;

            tabAdd.Controls.Add(txtlab184IgG);
            tabAdd.Controls.Add(chklab184IgG);
            tabAdd.Controls.Add(chklab184IgM);
            tabAdd.Controls.Add(lbtxtHospName);
            tabAdd.Controls.Add(txtHospName);
            tabAdd.Controls.Add(lbtxtHn);
            tabAdd.Controls.Add(txtHn);
            tabAdd.Controls.Add(lbtxtVsDate);
            tabAdd.Controls.Add(txtVsDate);
            tabAdd.Controls.Add(lbtxtPttNameT);
            tabAdd.Controls.Add(txtPttNameT);
            tabAdd.Controls.Add(lbtxtSex);
            tabAdd.Controls.Add(txtSex);
            tabAdd.Controls.Add(lbtxtAge);
            tabAdd.Controls.Add(txtAge);
            tabAdd.Controls.Add(lbtxtDOB);
            tabAdd.Controls.Add(txtDOB);
            tabAdd.Controls.Add(lbtxtPID);
            tabAdd.Controls.Add(txtPID);
            tabAdd.Controls.Add(lbtxtPassport);
            tabAdd.Controls.Add(txtPassport);
            tabAdd.Controls.Add(lbtxtAddr1);
            tabAdd.Controls.Add(txtAddr1);
            tabAdd.Controls.Add(lbtxtAddr2);
            tabAdd.Controls.Add(txtAddr2);
            tabAdd.Controls.Add(lbtxtDtrId);
            tabAdd.Controls.Add(txtDtrId);
            tabAdd.Controls.Add(txtDtrId);
            tabAdd.Controls.Add(txtDtrNameT);
            tabAdd.Controls.Add(pnPrint);
            tabAdd.Controls.Add(txtVsTime);
            tabAdd.Controls.Add(lbtxtLabCode);
            tabAdd.Controls.Add(txtLabCode);
            tabAdd.Controls.Add(txtLabName);
            tabAdd.Controls.Add(lbtxtLabResult);
            tabAdd.Controls.Add(txtLabResult);
            tabAdd.Controls.Add(btnHn);
            tabAdd.Controls.Add(lbtxtPttNameE);
            tabAdd.Controls.Add(txtPttNameE);
            tabAdd.Controls.Add(txtDtrNameE);
            tabAdd.Controls.Add(lbtxtLabName1);
            tabAdd.Controls.Add(txtLabName1);
            tabAdd.Controls.Add(txtLabResult1);
            tabAdd.Controls.Add(lbtxtLabName2);
            tabAdd.Controls.Add(txtLabName2);
            tabAdd.Controls.Add(txtLabResult2);
            tabAdd.Controls.Add(lbtxtLabName3);
            tabAdd.Controls.Add(txtLabName3);
            tabAdd.Controls.Add(txtLabResult3);
            tabAdd.Controls.Add(txtLabUnit);
            tabAdd.Controls.Add(lbtxtLabUnit);
            tabAdd.Controls.Add(txtLabNormal);
            tabAdd.Controls.Add(lbtxtLabNormal);
            tabAdd.Controls.Add(lbtxtLabReport);
            tabAdd.Controls.Add(lbtxtLabApprove);
            tabAdd.Controls.Add(txtLabReport);
            tabAdd.Controls.Add(txtLabApprove);
            tabAdd.Controls.Add(txtLabReportDate);
            tabAdd.Controls.Add(txtLabApproveDate);
            tabAdd.Controls.Add(pnThai);
            tabAdd.Controls.Add(pnEng);
            pnThai.Controls.Add(lbtxtSympbtom);
            pnThai.Controls.Add(txtSympbtom);
            pnThai.Controls.Add(lbtxtCountry);
            pnThai.Controls.Add(txtCountry);
            pnThai.Controls.Add(lbtxtDate);
            pnThai.Controls.Add(txtDate);
            pnThai.Controls.Add(chkThai1);
            pnThai.Controls.Add(chkThai2);
            pnThai.Controls.Add(chkThai3);
            pnThai.Controls.Add(chkThai4);
            pnThai.Controls.Add(chkThai5);
            pnThai.Controls.Add(chkThaiOther);
            pnThai.Controls.Add(txtThaiOther);

            pnThai.Controls.Add(chkDraw);
            pnThai.Controls.Add(chkHosp);
            pnThai.Controls.Add(lbtxtHospEndDate);
            pnThai.Controls.Add(txtHospStartDate);
            pnThai.Controls.Add(txtHospEndDate);
            pnThai.Controls.Add(chkStop);
            pnThai.Controls.Add(txtStop);
            pnThai.Controls.Add(chkTrue);
            pnThai.Controls.Add(txtTrue);
            pnThai.Controls.Add(txtStopStartDate);
            pnThai.Controls.Add(txtStopEndDate);
            pnThai.Controls.Add(lbtxtStopEndDate);
            tabAdd.Controls.Add(txtNation);
            tabAdd.Controls.Add(lbtxtNation);
            tabAdd.Controls.Add(txtLabCodeSe184);
            tabAdd.Controls.Add(txtLabNameSe184);
            tabAdd.Controls.Add(txtLabResultSe184);
            tabAdd.Controls.Add(chktxtLabCodeSe184);
            tabAdd.Controls.Add(txtLabUnitSe184);
            tabAdd.Controls.Add(chkLab184Nas);
            tabAdd.Controls.Add(chkLab184Saliva);
            tabAdd.Controls.Add(chkLab184Nucl);
            tabAdd.Controls.Add(chkLab184Lamp);
            tabAdd.Controls.Add(chkLab184Antigen);
            this.Controls.Add(tcMain);

            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
        }
        private void FrmOPDCovid_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.WindowState = FormWindowState.Maximized;
            this.Text = bc.iniC.pdfFontName +" Last Update 2021-01-13";

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
        }
    }
}
