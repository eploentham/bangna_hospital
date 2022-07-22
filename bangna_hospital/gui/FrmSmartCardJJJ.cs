using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmSmartCardJJJ : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, famtB14, fque, fqueB, famtB30;
        Patient ptt;
        String lbPaidName = "", vn="", compname="", dtrcode="", dtrname="", symptom="", remark="", deptname="", vsdate="", preno="", hiid="";
        Boolean pageLoad = false;
        DataTable dtstcDrug;
        DataRow drowdrug;
        C1ThemeController theme1;
        public FrmSmartCardJJJ(BangnaControl bc, Patient ptt,String lbPaidName, String vn, String compname, String dtrcode, String dtrname, String symptom, String remark, String deptname, String vsdate, String preno, String queno, String sympton, String hiid)
        {
            InitializeComponent();
            this.bc = bc;
            this.ptt = ptt;
            this.lbPaidName = lbPaidName;
            this.vn = vn;
            this.compname = compname;
            this.dtrcode = dtrcode;
            this.dtrname = dtrname;
            this.remark = remark;
            this.deptname = deptname;
            this.vsdate = vsdate;
            this.preno = preno;
            txtQue.Text = queno;
            this.symptom = sympton;
            this.txtJJJhiid.Value = hiid;
            this.hiid = hiid;
            theme1 = new C1ThemeController();
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

            btnJJJPrnStaffNote.Click += BtnJJJPrnStaffNote_Click;

            btnJJJDrugSet.Click += BtnJJJDrugSet_Click;
            chkJJJDrugSetA.Click += ChkJJJDrugSetA_Click;
            chkJJJDrugSetB.Click += ChkJJJDrugSetB_Click;
            lbJJJname.DoubleClick += LbJJJname_DoubleClick;
            chkJJJDrugSetC.Click += ChkJJJDrugSetC_Click;
            chkJJJDrugSetD.Click += ChkJJJDrugSetD_Click;

            setControl();
        }

        private void ChkJJJDrugSetD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setDrugDClick();
        }

        private void ChkJJJDrugSetC_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setDrugCClick();
        }

        private void LbJJJname_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genImgStaffNoteJJJ();
        }

        private void setControl()
        {
            txtJJJhn.Value = ptt.MNC_HN_NO;
            lbJJJname.Text = ptt.Name;
            txtVsdate.Text = vsdate;
            txtPreno.Text = preno;
            txtJJJvn.Value = vn;
            txtJJJhiid.Value = hiid;
            chkJJJc11.Checked = true;
            chkJJJc12.Checked = true;
            chkJJJc21.Checked = true;
            chkJJJc22.Checked = true;
            chkJJJc23.Checked = true;
            chkJJJc31.Checked = true;
            chkJJJc32.Checked = true;
            setDrugAClick();
        }

        private void BtnJJJDrugSet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setOrderDrug();
        }
        private void setOrderDrug()
        {
            if (ptt.MNC_HN_NO.Length < 5)
            {
                MessageBox.Show("MNC_HN_NO", "");
                return;
            }
            if (deptname.Length <= 0)
            {
                MessageBox.Show("txtDept", "");
                return;
            }
            if (txtPreno.Text.Length <= 0)
            {
                MessageBox.Show("txtPreno", "");
                return;
            }
            if (dtrcode.Length <= 0)
            {
                MessageBox.Show("กรุณาป้อน ว.แพทย์", "");
                return;
            }
            //new LogWriter("d", "BtnReqLab_Click ");
            //MessageBox.Show("6666666", "");
            String sql = "", re = "", preno = "", vsdate = "", labcode = "", drugset="";
            long chk = 0;
            DateTime datechk = new DateTime();
            if (DateTime.TryParse(txtVsdate.Text.Trim(), out datechk))
            {
                //MessageBox.Show("7777777", "");
                //new LogWriter("d", "BtnReqLab_Click 1");
                if (chkJJJDrugSetA.Checked)
                {
                    drugset = "OSPI_A";
                    re = bc.bcDB.pharT01DB.insertPharmacyOPDDrugJJJa(ptt.MNC_HN_YR, txtJJJhn.Text.Trim(), txtVsdate.Text.Trim(), txtPreno.Text.Trim(), dtrcode, "1618");
                }
                else if (chkJJJDrugSetB.Checked)
                {
                    drugset = "OSPI_B";
                    re = bc.bcDB.pharT01DB.insertPharmacyOPDDrugJJJb(ptt.MNC_HN_YR, ptt.MNC_HN_NO, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), dtrcode, "1618", txtJJJsetbFA01450qty.Text.Trim());
                }
                else if (chkJJJDrugSetC.Checked)
                {
                    drugset = "OSPI_C";
                    re = bc.bcDB.pharT01DB.insertPharmacyOPDDrugJJJc(ptt.MNC_HN_YR, ptt.MNC_HN_NO, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), dtrcode, "1618");
                }
                else if (chkJJJDrugSetD.Checked)
                {
                    drugset = "OSPI_D";
                    re = bc.bcDB.pharT01DB.insertPharmacyOPDDrugJJJd(ptt.MNC_HN_YR, ptt.MNC_HN_NO, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), dtrcode, "1618",txtJJJsetdFA01464qty.Text.Trim());
                }
                String reupdatehidrug = bc.bcDB.ptthiDB.updateStatusDrugOrder(txtJJJhiid.Text.Trim(), drugset, txtVsdate.Text.Trim(), re, "1618");
                //ribbonLabel3.Text = "hi drug " + reupdatehidrug;
                if (long.TryParse(re, out chk))
                {
                    //stt.SetToolTip(btnReqLab, "ออก ORder drug เรียบร้อย");
                    txtJJJreqnoDrug.Text = re;
                    String rephart06 = "";
                    int reqyear = 0;
                    reqyear = DateTime.Now.Year + 543;
                    rephart06 = bc.bcDB.pharT05DB.insertPharmacyT0506jjj(ptt.MNC_HN_YR, ptt.MNC_HN_NO, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), reqyear.ToString(), re, txtVsdate.Text.Trim(), drugset, "1618");
                    txtJJJdrugdocno.Text = rephart06;
                    //btnReqLab.Enabled = false;
                    //btnReqLab.BackColor = Color.Yellow;
                    //printLabReqNo();
                    genImgStaffNoteJJJ();
                    bc.bcDB.vsDB.updateStatusCloseVisitNoLAB(txtJJJhn.Text.Trim(), ptt.MNC_HN_YR, txtPreno.Text.Trim(), txtVsdate.Text.Trim());
                    printStickerDrug();
                    lbsbPttStatus.Text = "พิมพ์ sticker ที่ห้องยา เรียบร้อย";
                }
            }
            else
            {
                MessageBox.Show("88888888", "");
                //sep.SetError(txtVsdate, "");
                return;
            }
        }
        private void genImgStaffNoteJJJ()
        {
            String err = "";
            float mmpi = 25.4f;
            int dpi = 150, line1Len = 965, x2Right = 1740;

            err = "00";
            Image imgLogo = Resources.LOGO_Green_Transparent;
            int newHeight = 50, yPos = 0, gapLine = 45, col2 = 470, col22 = 300, col3 = 980, col31 = 460, recx = 15, recy = 15, col2int = 0, col4 = 0, col40 = 0, col40int = 0, yPosint = 0, col5 = 980, col6 = 1340;
            String line = "", date = "";
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps

            Image resizedImageLogo = imgLogo.GetThumbnailImage((newHeight * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
            //Bitmap imgA4 = new Bitmap((int)(210 / mmpi * dpi), (int)(297 / mmpi * dpi));        //Port
            Bitmap imgA4 = new Bitmap((int)(297 / mmpi * dpi), (int)(210 / mmpi * dpi));        //lang

            Pen penGreen3 = new Pen(Color.FromArgb(26, 173, 79), 3);
            Pen penBlue3 = new Pen(Color.FromArgb(79, 111, 108), 3);
            Pen blackPen = new Pen(Color.Black, 1);
            Pen penBorder = penBlue3;
            SolidBrush BrushBlack = new SolidBrush(Color.Black);
            SolidBrush brushBule = new SolidBrush(Color.Blue);
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
                //resizedImageLogo = imgLogo.GetThumbnailImage(((newHeight+3000) * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
                //resizedImageLogo = imgLogo.GetThumbnailImage((5 * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
                int newWidth = bc.imgScanWidth;
                newWidth = 8000;
                resizedImageLogo = imgLogo.GetThumbnailImage(newWidth, (newWidth * imgLogo.Height) / imgLogo.Width, null, IntPtr.Zero);
            }
            col2 = 105;
            col3 = 300;
            col4 = 1000;
            //col40 = 650;
            col40 = 1050;
            yPos = 15;
            col6 = 1340;
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            imgA4.SetResolution(dpi, dpi);
            err = "01";

            using (Graphics gfx = Graphics.FromImage(imgA4))
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                String line1 = "";
                gfx.FillRectangle(brush, 0, 0, imgA4.Width, imgA4.Height);
                gfx.DrawImage(resizedImageLogo, 120, 40);
                gfx.DrawImage(resizedImageLogo, 1000, 40);
                line = "โรงพยาบาล บางนา5";
                gfx.DrawString(line, famtB, brushBule, 195, 35, flags);
                gfx.DrawString(line, famtB, brushBule, 1070, 35, flags);
                //gfx.DrawRectangle(penBorder, 3, 3, imgA4.Width - 9, imgA4.Height - 9);            // Border
                line = "H.N. " + ptt.MNC_HN_NO + "     " + txtJJJvn.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 35, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 35, flags);
                line = "ชื่อ " + ptt.Name;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 60, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 60, flags);
                line = "เลขที่บัตร " + ptt.MNC_ID_NO;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 85, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, 1340, 85, flags);
                line = lbPaidName;
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 115, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 115, flags);

                line = compname.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 140, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 140, flags);
                line = "โรคประจำตัว        ไม่มี";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 140, flags);
                rec = new Rectangle(col2int + 125, 145, recx, recy);
                gfx.DrawRectangle(blackPen, rec);
                line = "อายุ " + ptt.AgeStringShort1() + " [" + bc.datetoShow(ptt.MNC_BDAY) + "]";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 140, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 140, flags);

                line = "มีโรค ระบุ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 35, 170, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 35 - recx - 10, 180, recx, recy));
                line = "วันที่เวลา " + date;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 170, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 170, flags);

                line = "โรคเรื้อรัง";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 200, flags);
                line = "ชื่อแพทย์ " + dtrcode.Trim() + " " + dtrname;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 200, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 200, flags);

                line = "DR Time.                               ปิดใบยา";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 230, flags);
                line = "อาการเบื้องต้น " + symptom.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 230, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 230, flags);

                line = "Temp";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, 260, flags);

                line = "H.Rate";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 80, 260, flags);
                line = "R.Rate";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 160, 260, flags);
                line = "BP1";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 240, 260, flags);
                line = "Time :";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 300, 260, flags);
                line = "BP2 ";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 380, 260, flags);
                line = "Time :";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 440, 260, flags);

                line = "Wt.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, 290, flags);
                line = "Ht.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 80, 290, flags);
                line = "BMI.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 100, 290, flags);
                line = "CC.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 180, 290, flags);
                line = "CC.IN";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 240, 290, flags);
                line = "CC.EX";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 300, 290, flags);
                line = "Ab.C";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 400, 290, flags);
                line = "H.C.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 460, 290, flags);

                line = "Precaution (Med) _________________________________________ ";
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 290, flags);
                err = "02";
                line = "แพ้ยา/อาหาร/อื่นๆ         ไม่มี";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 320, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 320, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 180, 330, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col4 - recx + 200, 330, recx, recy));
                line = "มี ระบุอาการ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + recx + 10, 350, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, 360, recx, recy));
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + recx + 10, 350, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col4, 360, recx, recy));
                line = "O2 Sat __________        Pain Score __________";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 350, flags);

                gfx.DrawRectangle(penBorder, 100, 20, 865, 1200);// Border
                gfx.DrawRectangle(penBorder, col5, 20, 760, 1200);// Border

                yPos = 390;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //1
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawLine(penBorder, 1550, yPos, 1550, 1020);        //

                //gfx.DrawString("",)
                line = "Staff Note";
                gfx.DrawString(line, famtB, brushBule, 380, yPos, flags);
                line = "Rx";
                gfx.DrawString(line, famtB, brushBule, 980, yPos, flags);
                line = "ใบสั่งยา";
                gfx.DrawString(line, famtB, brushBule, 1280, yPos, flags);
                line = "จำนวนหน่วย";
                gfx.DrawString(line, famtB, brushBule, 1550, yPos, flags);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);

                gfx.DrawLine(penBorder, col3, yPos, x2Right, yPos);

                err = "03";
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("CHIEF COMPLIANT", fEditB, brushBule, 110, yPos, flags);
                line = remark + " " + deptname.Trim();

                gfx.DrawString(line, fEdit, Brushes.Black, col4 + 100, 200, flags);
                //line = "Medication                       No Medication";
                //gfx.DrawString(line, fEdit, Brushes.Black, col4 + 100, yPos, flags);
                //gfx.DrawRectangle(blackPen, new Rectangle(col4 + 80 - recx - 5, yPos + 10, recx, recy));
                //gfx.DrawRectangle(blackPen, new Rectangle(col4 + 300 - recx, yPos + 10, recx, recy));
                line = symptom.Trim().Replace("โครงการ เจอ แจก จบ", "โครงการ OPSI");
                //gfx.DrawString(line, fEdit, Brushes.Black, 110 + 100, yPos + 50, flags);
                gfx.DrawString(symptom.Trim(), fEditB, Brushes.Black, col40 ,240, flags);
                gfx.DrawString(symptom.Trim(), fEditB, Brushes.Black, 330, 480, flags);
                //btnSavePic
                line = "อาการ";
                gfx.DrawString(line, fEditB, Brushes.Black, 280,570, flags);
                //line = "เอกซเรย์ปอด";
                //gfx.DrawString(line, fEditB, Brushes.Black, col40 + 460, yPos + 175, flags);

                //line = "สัมผัสผู้ป่วย ชื่อ";
                //gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 230, flags);

                //gfx.DrawString(chkJJJDrugSetA.Checked ? "Set A" : "Set A", fEditB, Brushes.Black, col40 + 460, yPos + 230, flags);


                //line = "โครงการ " + lbPaidName;
                ////textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                //gfx.DrawString(line, fEdit, Brushes.Black, col3, 295, flags);
                //gfx.DrawString(line, fEdit, Brushes.Black, col40 + 40, 295, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(400 - recx - 5, 580, recx, recy));
                gfx.DrawString(chkJJJ11.Text, fEdit, Brushes.Black, 400, 572, flags);     //"ปกติ"
                gfx.DrawString(chkJJJ11.Checked ? "/" : "", famtB, Brushes.Black, 400 - recx - 4, 565, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(400 + 70 - recx, 580, recx, recy));
                gfx.DrawString(chkJJJ12.Text + " " + txtJJJ12.Text + " วัน", fEdit, Brushes.Black, 400 + 70, 572, flags);    //"ไข้   วัน"
                gfx.DrawString(chkJJJ12.Checked ? "/" : "", famtB, Brushes.Black, 400 + 70 - recx - 2, 565, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(400 + 220 - recx, 580, recx, recy));
                gfx.DrawString(chkJJJ13.Text, fEdit, Brushes.Black, 400 + 220, 572, flags);    //"ไอ   เจ็บคอ"
                gfx.DrawString(chkJJJ13.Checked ? "/" : "", famtB, Brushes.Black, 400 + 220 - recx - 2, 565, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(400 + 400 - recx, 580, recx, recy));
                gfx.DrawString(chkJJJ14.Text, fEdit, Brushes.Black, 400 + 400, 572, flags);    //"มีน้ำมูก"
                gfx.DrawString(chkJJJ14.Checked ? "/" : "", famtB, Brushes.Black, 400 + 400 - recx - 2, 565, flags);



                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 625, recx, recy));
                gfx.DrawString(chkJJJ21.Text, fEdit, Brushes.Black, col22 - 20, 620, flags);     //"อ่อนเพลีย"
                gfx.DrawString(chkJJJ21.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) - 20 - recx - 2, 615, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 100 - recx - 5, 625, recx, recy));
                gfx.DrawString(chkJJJ22.Text, fEdit, Brushes.Black, col22 + 100, 620, flags);     //"ปวดตามร่างกาย"
                gfx.DrawString(chkJJJ22.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 100 - recx - 2, 615, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 260 - recx - 5, 625, recx, recy));
                gfx.DrawString(chkJJJ23.Text, fEdit, Brushes.Black, col22 + 260, 620, flags);     //"ปวดศีรษะ"
                gfx.DrawString(chkJJJ23.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 260 - recx - 2, 615, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 390 - recx - 5, 625, recx, recy));
                gfx.DrawString(chkJJJ24.Text, fEdit, Brushes.Black, col22 + 390, 620, flags);     //"มีเสมหะ"
                gfx.DrawString(chkJJJ24.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 390 - recx - 2, 615, flags);



                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 670, recx, recy));
                gfx.DrawString(chkJJJ31.Text, fEdit, Brushes.Black, col22 - 20, 663, flags);     //"แน่นหน้าอก"
                gfx.DrawString(chkJJJ31.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) - 20 - recx - 2, 658, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 125 - recx - 5, 670, recx, recy));
                gfx.DrawString(chkJJJ32.Text + " " + txtJJJ32.Text + " ครั้ง", fEdit, Brushes.Black, col22 + 125, 663, flags);     //"ถ่ายเหลว"
                gfx.DrawString(chkJJJ32.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 125 - recx - 2, 658, flags);


                //line = symptom.Trim();
                //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                //gfx.DrawString(line, fEditB, Brushes.Black, col3 + 40, yPos + 315, flags);

                //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                //gfx.DrawString("aaaa "+symptom.Trim(), fEditB, Brushes.Black, col40 + 70, yPos + 330, flags);

                String drugset = "";
                drugset = chkJJJDrugSetB.Checked ? "B" : chkJJJDrugSetC.Checked ? "C" : chkJJJDrugSetD.Checked ? "D" : "A";
                line = "Set " + drugset.ToUpper();
                gfx.DrawString(line, famtB14, Brushes.Black, col40 + 70, 465, flags);
                if (chkJJJDrugSetA.Checked)
                {
                    if (chkJJJsetaPA007.Checked)
                    {
                        gfx.DrawString("PARAcetamol 500 MG. (3K2)", fEditB, Brushes.Black, col40, 523, flags);
                        gfx.DrawString(txtJJJsetaPA007qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 523, flags);
                    }
                    if (chkJJJsetaCP001.Checked)
                    {
                        gfx.DrawString("C.P.M. 4 MG. (3J2)", fEditB, Brushes.Black, col40, 568, flags);
                        gfx.DrawString(txtJJJsetaCP001qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 568, flags);
                    }
                    if (chkJJJsetaBI005.Checked)
                    {
                        gfx.DrawString("biSOLVON( BROMHEXINE ) 8 MG. (3J2)", fEditB, Brushes.Black, col40, 610, flags);
                        gfx.DrawString(txtJJJsetaBI005qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 610, flags);
                    }
                    if (chkJJJsetaAN023.Checked)
                    {
                        gfx.DrawString("ANDROGRAPHOLIDE 20MG (ฟ้าทะลายโจร)", fEditB, Brushes.Black, col40, 653, flags);
                        gfx.DrawString(txtJJJsetaAN023qty.Text.Trim() + " CAP", fEditB, Brushes.Black, col40 + 540, 653, flags);
                    }
                }
                else if (chkJJJDrugSetC.Checked)
                {
                    if (chkJJJsetcPA007.Checked)
                    {
                        gfx.DrawString("PARAcetamol 500 MG. (3K2)", fEditB, Brushes.Black, col40, 523, flags);
                        gfx.DrawString(txtJJJsetcPA007qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 523, flags);
                    }
                    if (chkJJJsetcCP001.Checked)
                    {
                        gfx.DrawString("C.P.M. 4 MG. (3J2)", fEditB, Brushes.Black, col40, 568, flags);
                        gfx.DrawString(txtJJJsetcCP001qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 568, flags);
                    }
                    if (chkJJJsetcBI005.Checked)
                    {
                        gfx.DrawString("biSOLVON( BROMHEXINE ) 8 MG. (3J2)", fEditB, Brushes.Black, col40, 610, flags);
                        gfx.DrawString(txtJJJsetcBI005qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 610, flags);
                    }
                }
                else if (chkJJJDrugSetB.Checked)
                {
                    if (chkJJJsetbPA007.Checked)
                    {
                        gfx.DrawString("PARAcetamol 500 MG. (3K2)", fEditB, Brushes.Black, col40, 523, flags);
                        gfx.DrawString(txtJJJsetbPA007qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 523, flags);
                    }
                    if (chkJJJsetbCP001.Checked)
                    {
                        gfx.DrawString("C.P.M. 4 MG. (3J2)", fEditB, Brushes.Black, col40, 568, flags);
                        gfx.DrawString(txtJJJsetbCP001qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 568, flags);
                    }
                    if (chkJJJsetbBI005.Checked)
                    {
                        gfx.DrawString("biSOLVON( BROMHEXINE ) 8 MG. (3J2)", fEditB, Brushes.Black, col40, 610, flags);
                        gfx.DrawString(txtJJJsetbBI005qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 610, flags);
                    }
                    if (chkJJJsetbFA01450.Checked)
                    {
                        gfx.DrawString("FAVIPIRAVIR 200 MG.", fEditB, Brushes.Black, col40, 653, flags);
                        gfx.DrawString(txtJJJsetbFA01450qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 653, flags);
                    }
                }
                else if (chkJJJDrugSetD.Checked)
                {
                    if (chkJJJsetdPA007.Checked)
                    {
                        gfx.DrawString("PARAcetamol 500 MG. (3K2)", fEditB, Brushes.Black, col40, 523, flags);
                        gfx.DrawString(txtJJJsetdPA007qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 523, flags);
                    }
                    if (chkJJJsetdCP001.Checked)
                    {
                        gfx.DrawString("C.P.M. 4 MG. (3J2)", fEditB, Brushes.Black, col40, 568, flags);
                        gfx.DrawString(txtJJJsetdCP001qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 568, flags);
                    }
                    if (chkJJJsetdBI005.Checked)
                    {
                        gfx.DrawString("biSOLVON( BROMHEXINE ) 8 MG. (3J2)", fEditB, Brushes.Black, col40, 610, flags);
                        gfx.DrawString(txtJJJsetdBI005qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 610, flags);
                    }

                    if (chkJJJsetdFA01464.Checked)
                    {
                        gfx.DrawString("FAVIPIRAVIR 200 MG.", fEditB, Brushes.Black, col40, 653, flags);
                        gfx.DrawString(txtJJJsetdFA01464qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 540, 653, flags);
                    }
                }

                line = "COVID 19";
                gfx.DrawString(line, fEditB, Brushes.Black, col2 + 130, 885, flags);
                line = "คำแนะนำ";
                //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 930, flags);
                //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 75 - recx, yPosint + 620, recx, recy));

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 930, recx, recy));
                gfx.DrawString(chkJJJc11.Text, fEdit, Brushes.Black, col22 - 20, 930, flags);     //"กักกันตัว 10 วัน"
                gfx.DrawString(chkJJJc22.Checked ? "/" : "", famtB, Brushes.Black, col22 - 20 - recx - 2, 915, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 330 - recx - 5, 930, recx, recy));
                gfx.DrawString(chkJJJc12.Text, fEdit, Brushes.Black, col22 + 330, 930, flags);     //"ห้ามออกจากบ้าน"
                gfx.DrawString(chkJJJc22.Checked ? "/" : "", famtB, Brushes.Black, col22 + 330 - recx - 2, 915, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 980, recx, recy));
                gfx.DrawString(chkJJJc21.Text, fEdit, Brushes.Black, col22 - 20, 980, flags);     //"งดการใช้ของร่วมกับผู้อื่น"
                gfx.DrawString(chkJJJc22.Checked ? "/" : "", famtB, Brushes.Black, col22 - 20 - recx - 2, 970, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 250 - recx - 5, 980, recx, recy));
                gfx.DrawString(chkJJJc22.Text, fEdit, Brushes.Black, col22 + 250, 980, flags);     //"สวมหน้ากากอนามัย"
                gfx.DrawString(chkJJJc22.Checked ? "/" : "", famtB, Brushes.Black, col22 + 250 - recx - 2, 970, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 450 - recx - 5, 980, recx, recy));
                gfx.DrawString(chkJJJc23.Text, fEdit, Brushes.Black, col22 + 450, 980, flags);     //"หมั่นล้างมือ"
                gfx.DrawString(chkJJJc23.Checked ? "/" : "", famtB, Brushes.Black, col22 + 450 - recx - 2, 970, flags);

                gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 1023, recx, recy));
                gfx.DrawString(chkJJJc31.Text, fEdit, Brushes.Black, col22 - 20, 1020, flags);     //"กินร้อน ช้อนกลาง"
                gfx.DrawString(chkJJJc31.Checked ? "/" : "", famtB, Brushes.Black, col22 - 20 - recx - 2, 1010, flags);

                //gfx.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 220 - recx - 5, 690, recx, recy));
                //gfx.DrawString(chkJJJc32.Text, fEdit, Brushes.Black, col22 + 220, 690, flags);     //"ใบรับรองแพทย์"
                //gfx.DrawString(chkJJJc32.Checked ? "/" : "", famtB, Brushes.Black, col22 + 220 - recx - 2, 675, flags);
















                //line = "สัมผัสล่าสุด";
                //gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 275, flags);
                //line = "เครื่องวัดออกซิเจน ปลายนิ้ว    1";
                //gfx.DrawString(line, fEditB, Brushes.Black, col40 + 460, yPos + 275, flags);
                //line = "ปรอทวัดอุณหภูมิ     1";
                //gfx.DrawString(line, fEditB, Brushes.Black, col40 + 460, yPos + 305, flags);

                //line = "คำแนะนำ       การออกกำลังกาย               การรับประทานอาหารที่ถูกสัดส่วน";
                //gfx.DrawString(line, fEdit, Brushes.Black, col2, yPos + 320, flags);
                //gfx.DrawRectangle(blackPen, new Rectangle(col2 + 105, yPos + 325, recx, recy));
                //gfx.DrawRectangle(blackPen, new Rectangle(col2 + 310, yPos + 325, recx, recy));

                //line = "การตรวจสุขภาพประจำปี          การพบแพทย์เฉพาะทาง       อื่นๆ";
                //gfx.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 365, flags);
                //gfx.DrawRectangle(blackPen, new Rectangle(col2 + 35 - recx, yPos + 375, recx, recy));
                //gfx.DrawRectangle(blackPen, new Rectangle(col2 + 280 - recx, yPos + 375, recx, recy));
                //gfx.DrawRectangle(blackPen, new Rectangle(col2 + 510 - recx, yPos + 375, recx, recy));

                err = "04";
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("PYSICAL EXAM", fEditB, brushBule, 110, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //10
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("DIAGNOSIS", fEditB, brushBule, 110, yPos, flags);
                gfx.DrawString("คำแนะนำ", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //15
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("รหัสโรค", fEditB, brushBule, 110, yPos, flags);
                gfx.DrawString("FOLLOW UP", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("ATTENDIND PHYSICAN", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);

                //yPos += gapLine;
                err = "05";
                line = "ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 240 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 305 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 410 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 515 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 590 - recx, yPos + 10, recx, recy));

                //line = "Health Education :";
                //gfx.DrawString(line, fEdit, Brushes.Black, col2, imgA4.Height - 90, flags);
                //line = "ชื่อผู้รับ _____________________________";
                //gfx.DrawString(line, fEdit, Brushes.Black, col2 + 500, imgA4.Height - 90, flags);

                //line = "ลงชื่อพยาบาล: _____________________________________";
                //gfx.DrawString(line, fEdit, Brushes.Black, col2, imgA4.Height - 60, flags);
                line = "FM-REC-002 (00 10/09/53)(1/1)";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, imgA4.Height - 30, flags);
                gfx.DrawString(line, fEditS, Brushes.Black, col6, imgA4.Height - 30, flags);
                //gfx.DrawString("",)
            }
            //imgA4.Save("aaaa.jpg");
            try
            {
                err = "06";
                Rectangle rectL = new Rectangle(0, 0, 980, imgA4.Height);
                //Bitmap imgL = new Bitmap(980, imgA4.Height);
                Bitmap imgL = new Bitmap(630, imgA4.Width - 960);
                Graphics gfxL = Graphics.FromImage(imgL);
                gfxL.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfxL.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfxL.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfxL.DrawImage(imgA4, 0, 0, rectL, GraphicsUnit.Pixel);

                String filenameS = "";
                filenameS = "000000" + txtPreno.Text.Trim();
                filenameS = filenameS.Substring(filenameS.Length - 6);

                Rectangle rectR = new Rectangle(975, 0, imgA4.Width - 960, imgA4.Height);
                //Bitmap imgR = new Bitmap(imgA4.Width - 975, imgA4.Height);
                Bitmap imgR = new Bitmap(502, imgA4.Width - 960);
                Graphics gfxR = Graphics.FromImage(imgR);
                gfxR.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfxR.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfxR.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfxR.DrawImage(imgA4, 0, 0, rectR, GraphicsUnit.Pixel);
                String filenameR = "", path = "\\\\172.25.10.5\\image\\OPD\\", year = DateTime.Now.ToString("yyyy"), mon = DateTime.Now.ToString("MM"), day = DateTime.Now.ToString("dd");
                year = txtVsdate.Text.Substring(0, 4);
                path += year + "\\" + mon + "\\" + day + "\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filenameR = "000000" + txtPreno.Text.Trim();
                filenameR = filenameR.Substring(filenameR.Length - 6);
                err = "07";
                new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameS);
                imgL.Save(path + filenameS + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgL.Save(filenameS + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                err = "08";
                new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameR);
                imgR.Save(path + filenameR + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgR.Save(filenameR + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "genImgStaffNote err" + err + " error " + ex.Message);
                MessageBox.Show("" + ex.Message, "");
            }
        }
        private void printStickerDrug()
        {
            PrintDocument document = new PrintDocument();
            //dtstcDrug = bc.bcDB.pharT06DB.selectByReqNo((DateTime.Now.Year + 543).ToString(), "35919", "5021045", "140", "2022-03-19");
            dtstcDrug = bc.bcDB.pharT06DB.selectByReqNo((DateTime.Now.Year + 543).ToString(), txtJJJreqnoDrug.Text.Trim(), txtJJJhn.Text.Trim(), txtPreno.Text.Trim(), txtVsdate.Text.Trim());
            document.PrinterSettings.PrinterName = bc.iniC.printerStickerDrug;
            document.PrintPage += Document_PrintPage_Sticker_Drug;
            document.DefaultPageSettings.Landscape = false;
            int num = 0;
            if (int.TryParse("1", out num))
            {
                document.PrinterSettings.Copies = short.Parse(num.ToString());
                foreach (DataRow drow in dtstcDrug.Rows)
                {
                    if (!drow["MNC_PH_cd"].ToString().Equals("FA014")) continue;
                    drowdrug = drow;
                    for (int i = 0; i < num; i++)
                    {
                        document.Print();
                    }
                }
            }
            else
            {
                //sep.SetError(txtStickerNum, "");
            }

            PrintDocument documentSum = new PrintDocument();
            documentSum.PrinterSettings.PrinterName = bc.iniC.printerStickerDrug;
            documentSum.PrintPage += Document_PrintPage_Sticker_DrugSum;
            documentSum.DefaultPageSettings.Landscape = false;
            num = 0;
            if (int.TryParse("1", out num))
            {
                documentSum.PrinterSettings.Copies = short.Parse(num.ToString());
                for (int i = 0; i < num; i++)
                {
                    documentSum.Print();
                }
            }
            else
            {
                //sep.SetError(txtStickerNum, "");
            }
        }
        private void Document_PrintPage_Sticker_Drug(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, ydate = 0, gapline = 17, col1 = 10, col2 = 60, col4 = 200;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps

            String text = "";
            //dtstcDrug = bc.bcDB.pharT06DB.selectByReqNo((DateTime.Now.Year + 543).ToString(), txtreqno.Text.Trim(), txtHn.Text.Trim(), txtPreno.Text.Trim(), txtVsdate.Text.Trim());
            //dtstcDrug = bc.bcDB.pharT06DB.selectByReqNo((DateTime.Now.Year + 543).ToString(), "37220", "5175935", "1178", "2022-03-21");
            if (dtstcDrug.Rows.Count > 0)
            {
                //yPos += gapline;
                //foreach(DataRow drow in dtstcDrug.Rows)
                //{
                text = bc.iniC.hostname;
                e.Graphics.DrawString(text, fPrnBil, Brushes.Black, col2, yPos, flags);
                text = bc.iniC.hosttel;
                e.Graphics.DrawString(text, fPrnBil, Brushes.Black, col4, yPos, flags);

                yPos += gapline;
                text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                e.Graphics.DrawString(text, fPrnBil, Brushes.Black, col2, yPos, flags);
                text = "HN " + ptt.MNC_HN_NO;
                e.Graphics.DrawString(text, fEditB, Brushes.Black, col4, yPos, flags);

                yPos += gapline;
                text = ptt.Name;
                e.Graphics.DrawString(text, famtB, Brushes.Black, col1, yPos, flags);



                yPos += gapline + 15;
                if (drowdrug["MNC_PH_CD"].ToString().Equals("FA014"))
                {
                    text = "วันแรกทานครั้ง 9 เม็ด หลังอาหารเช้า-เย็นหลัง";
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                    yPos += gapline + 5;
                    text = "จากนั้นครั้งละ 4 เม็ด หลังอาหารเช้า-เย็น";
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                }
                else if (drowdrug["MNC_PH_CD"].ToString().Equals("CP001"))
                {
                    text = "รับประทานครั้งละ 1 เม็ด/วันละ 3 ครั้ง หลังอาหาร ";
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                    yPos += gapline + 5;
                    text = "เช้า-กลางวัน-เย็น/เวลาเป็นหวัด มีน้ำมูก";
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                }
                else if (drowdrug["MNC_PH_CD"].ToString().Equals("BI005"))
                {
                    text = "รับประทานครั้งละ 1 เม็ด/วันละ 3 ครั้ง";
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                    yPos += gapline + 5;
                    text = "หลังอาหาร เช้า-กลางวัน-เย็น";
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                }
                else if (drowdrug["MNC_PH_CD"].ToString().Equals("PA007"))
                {
                    text = "รับประทานครั้งละ 1 เม็ด/ทุก 6 ชั่วโมง /เมื่อมีอาการ  ";
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                    yPos += gapline + 5;
                    text = "เวลาปวด หรือ มีไข้/";
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                }
                else
                {
                    text = drowdrug["MNC_PH_DIR_DSC"].ToString();
                    e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                }

                yPos += gapline;
                yPos += gapline;
                yPos += gapline;
                yPos += gapline;
                yPos += gapline - 5;
                text = drowdrug["MNC_PH_TN"].ToString() + " [" + drowdrug["MNC_PH_QTY_PAID"].ToString() + " " + drowdrug["MNC_PH_UNT_CD"].ToString() + "]";
                e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);
                //text = drowdrug["MNC_PH_QTY_PAID"].ToString()+" "+ drowdrug["MNC_PH_UNT_CD"].ToString();
                //e.Graphics.DrawString(text, fEdit, Brushes.Black, col1, yPos, flags);

                //yPos = 10;
                //e.HasMorePages = true;
                //}
            }
        }
        private void Document_PrintPage_Sticker_DrugSum(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, ydate = 0, gapline = 14, gapline1 = 17, col1 = 10, col2 = 60, col3 = 140, col4 = 200;

            //Graphics g = e.Graphics;
            //SolidBrush Brush = new SolidBrush(Color.Black);
            //Rectangle rec = new Rectangle(0, 0, 20, 20);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            StringFormat flagsr = new StringFormat(StringFormatFlags.DirectionRightToLeft);  //wraps
            String text = "", chk = "";
            if (dtstcDrug.Rows.Count > 0)
            {
                //yPos += gapline;
                //text = bc.iniC.hostname;
                //chk += bc.iniC.hostname+Environment.NewLine;
                e.Graphics.DrawString(bc.iniC.hostname, fPrnBil, Brushes.Black, col2 + 15, yPos, flags);
                //text = bc.iniC.hosttel;
                //chk += bc.iniC.hosttel + Environment.NewLine;
                e.Graphics.DrawString(bc.iniC.hosttel, fPrnBil, Brushes.Black, col4, yPos, flags);

                yPos += gapline1;
                //text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                //chk += DateTime.Now.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine;
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fPrnBil, Brushes.Black, col2 + 15, yPos, flags);
                text = "HN " + ptt.MNC_HN_NO;
                //chk += text + Environment.NewLine;
                e.Graphics.DrawString(text, fEditB, Brushes.Black, col4, yPos, flags);
                yPos += gapline1;
                text = ptt.Name;
                //chk += text + Environment.NewLine;
                e.Graphics.DrawString(text, famtB, Brushes.Black, col1, yPos, flags);
                yPos += gapline;
                yPos += gapline;
                float price = 0, qty = 0, amt = 0, sum = 0;
                foreach (DataRow drow in dtstcDrug.Rows)
                {
                    text = drow["MNC_PH_cd"].ToString();
                    //if (text.Equals("YOX023")) continue;
                    //if (text.Equals("YTH037")) continue;
                    //chk += drow["MNC_PH_TN"].ToString() + Environment.NewLine;
                    float.TryParse(drow["MNC_PH_QTY_PAID"].ToString(), out qty);
                    float.TryParse(drow["MNC_PH_PRI"].ToString(), out price);
                    amt = price * qty;
                    sum += amt;
                    e.Graphics.DrawString(drow["MNC_PH_TN"].ToString(), fEditS, Brushes.Black, col1, yPos, flags);
                    e.Graphics.DrawString(qty.ToString("###"), fEditS, Brushes.Black, col3 + 100, yPos, flags);
                    e.Graphics.DrawString(amt.ToString("#.00"), fEditS, Brushes.Black, col3 + 130, yPos, flags);
                    e.Graphics.DrawString(drow["MNC_PH_UNT_CD"].ToString(), fEditS, Brushes.Black, col4, yPos, flags);
                    yPos += gapline;
                    //yPos = 10;
                    //e.HasMorePages = true;
                }
                text = txtQue.Text.Trim();
                //chk += text + Environment.NewLine;
                e.Graphics.DrawString(text, famtB30, Brushes.Black, 0, 0, flags);
                e.Graphics.DrawString("รวม. " + sum.ToString("#,00"), fEditB, Brushes.Black, col4 + 25, 48, flags);
            }
            //string chk1 = "";
        }
        private void ChkJJJDrugSetB_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setDrugBClick();
        }
        private void ChkJJJDrugSetA_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setDrugAClick();
        }
        private void setDrugAClick()
        {
            chkJJJsetaPA007.Checked = true;
            chkJJJsetaCP001.Checked = true;
            chkJJJsetaBI005.Checked = true;
            chkJJJsetaAN023.Checked = true;
            chkJJJsetaMI047.Checked = true;
            chkJJJsetaAN023.Checked = true;
            chkJJJsetaEL021.Checked = true;

            txtJJJsetaPA007qty.Value = "20";
            txtJJJsetaCP001qty.Value = "20";
            txtJJJsetaBI005qty.Value = "20";
            txtJJJsetaAN023qty.Value = "45";
            //txtJJJsetaPA007qty.Value = "20";
            txtJJJsetaMI047qty.Value = "1";
            txtJJJsetaEL021qty.Value = "3";

            chkJJJsetbPA007.Checked = false;
            chkJJJsetbCP001.Checked = false;
            chkJJJsetbBI005.Checked = false;
            chkJJJsetbFA01450.Checked = false;
            chkJJJsetbMI047.Checked = false;
            chkJJJsetbEL021.Checked = false;

            chkJJJsetcPA007.Checked = false;
            chkJJJsetcCP001.Checked = false;
            chkJJJsetcBI005.Checked = false;
            chkJJJsetcMI047.Checked = false;
            chkJJJsetcEL021.Checked = false;

            chkJJJsetdPA007.Checked = false;
            chkJJJsetdCP001.Checked = false;
            chkJJJsetdBI005.Checked = false;
            chkJJJsetdFA01464.Checked = false;
            chkJJJsetdMI047.Checked = false;
            chkJJJsetdEL021.Checked = false;

            txtJJJsetbPA007qty.Value = "0";
            txtJJJsetbCP001qty.Value = "0";
            txtJJJsetbBI005qty.Value = "0";
            txtJJJsetbFA01450qty.Value = "0";

            txtJJJsetcPA007qty.Value = "0";
            txtJJJsetcCP001qty.Value = "0";
            txtJJJsetcBI005qty.Value = "0";

            txtJJJsetdPA007qty.Value = "0";
            txtJJJsetdCP001qty.Value = "0";
            txtJJJsetdBI005qty.Value = "0";
            txtJJJsetdFA01464qty.Value = "0";

            btnJJJDrugSet.Text = "สั่งยา Set A";
            btnJJJDrugSet.BackColor = Color.DarkGoldenrod;
        }
        private void setDrugBClick()
        {
            chkJJJsetbPA007.Checked = true;
            chkJJJsetbCP001.Checked = true;
            chkJJJsetbBI005.Checked = true;
            chkJJJsetbFA01450.Checked = true;
            chkJJJsetbMI047.Checked = true;
            chkJJJsetbEL021.Checked = true;

            txtJJJsetbPA007qty.Value = "20";
            txtJJJsetbCP001qty.Value = "20";
            txtJJJsetbBI005qty.Value = "20";
            txtJJJsetbFA01450qty.Value = "50";
            txtJJJsetbMI047qty.Value = "1";
            txtJJJsetbEL021qty.Value = "3";

            //txtJJJsetaPA007qty.Value = "20";
            chkJJJsetaPA007.Checked = false;
            chkJJJsetaCP001.Checked = false;
            chkJJJsetaBI005.Checked = false;
            chkJJJsetaAN023.Checked = false;
            chkJJJsetaMI047.Checked = false;
            chkJJJsetaEL021.Checked = false;

            chkJJJsetcPA007.Checked = false;
            chkJJJsetcCP001.Checked = false;
            chkJJJsetcBI005.Checked = false;
            chkJJJsetcMI047.Checked = false;
            chkJJJsetcEL021.Checked = false;

            chkJJJsetdPA007.Checked = false;
            chkJJJsetdCP001.Checked = false;
            chkJJJsetdBI005.Checked = false;
            chkJJJsetdFA01464.Checked = false;
            chkJJJsetdMI047.Checked = false;
            chkJJJsetdEL021.Checked = false;

            txtJJJsetaPA007qty.Value = "0";
            txtJJJsetaCP001qty.Value = "0";
            txtJJJsetaBI005qty.Value = "0";
            txtJJJsetaAN023qty.Value = "0";

            txtJJJsetcPA007qty.Value = "0";
            txtJJJsetcCP001qty.Value = "0";
            txtJJJsetcBI005qty.Value = "0";

            txtJJJsetdPA007qty.Value = "0";
            txtJJJsetdCP001qty.Value = "0";
            txtJJJsetdBI005qty.Value = "0";
            txtJJJsetdFA01464qty.Value = "0";

            btnJJJDrugSet.Text = "สั่งยา Set B";
            btnJJJDrugSet.BackColor = Color.DarkKhaki;
        }
        private void setDrugCClick()
        {
            chkJJJsetcPA007.Checked = true;
            chkJJJsetcCP001.Checked = true;
            chkJJJsetcBI005.Checked = true;
            chkJJJsetcMI047.Checked = true;
            chkJJJsetcEL021.Checked = true;

            txtJJJsetcPA007qty.Value = "20";
            txtJJJsetcCP001qty.Value = "20";
            txtJJJsetcBI005qty.Value = "20";
            txtJJJsetcMI047qty.Value = "1";
            txtJJJsetcEL021qty.Value = "3";

            //txtJJJsetaPA007qty.Value = "20";
            chkJJJsetaPA007.Checked = false;
            chkJJJsetaCP001.Checked = false;
            chkJJJsetaBI005.Checked = false;
            chkJJJsetaAN023.Checked = false;
            chkJJJsetaMI047.Checked = false;
            chkJJJsetaEL021.Checked = false;

            chkJJJsetbPA007.Checked = false;
            chkJJJsetbCP001.Checked = false;
            chkJJJsetbBI005.Checked = false;
            chkJJJsetbFA01450.Checked = false;
            chkJJJsetbMI047.Checked = false;
            chkJJJsetbEL021.Checked = false;

            chkJJJsetdPA007.Checked = false;
            chkJJJsetdCP001.Checked = false;
            chkJJJsetdBI005.Checked = false;
            chkJJJsetdFA01464.Checked = false;
            chkJJJsetdMI047.Checked = false;
            chkJJJsetdEL021.Checked = false;

            txtJJJsetaPA007qty.Value = "0";
            txtJJJsetaCP001qty.Value = "0";
            txtJJJsetaBI005qty.Value = "0";
            txtJJJsetaAN023qty.Value = "0";

            txtJJJsetbPA007qty.Value = "0";
            txtJJJsetbCP001qty.Value = "0";
            txtJJJsetbBI005qty.Value = "0";
            txtJJJsetbFA01450qty.Value = "0";

            txtJJJsetdPA007qty.Value = "0";
            txtJJJsetdCP001qty.Value = "0";
            txtJJJsetdBI005qty.Value = "0";
            txtJJJsetdFA01464qty.Value = "0";


            btnJJJDrugSet.Text = "สั่งยา Set C";
            btnJJJDrugSet.BackColor = Color.DarkSalmon;
        }
        private void setDrugDClick()
        {
            chkJJJsetdPA007.Checked = true;
            chkJJJsetdCP001.Checked = true;
            chkJJJsetdBI005.Checked = true;
            chkJJJsetdFA01464.Checked = true;
            chkJJJsetdMI047.Checked = true;
            chkJJJsetdEL021.Checked = true;

            chkJJJsetcPA007.Checked = false;
            chkJJJsetcCP001.Checked = false;
            chkJJJsetcBI005.Checked = false;
            chkJJJsetcMI047.Checked = false;
            chkJJJsetcEL021.Checked = false;

            txtJJJsetdPA007qty.Value = "20";
            txtJJJsetdCP001qty.Value = "20";
            txtJJJsetdBI005qty.Value = "20";
            txtJJJsetdFA01464qty.Value = "64";
            txtJJJsetdMI047qty.Value = "1";
            txtJJJsetdEL021qty.Value = "3";

            //txtJJJsetaPA007qty.Value = "20";
            chkJJJsetaPA007.Checked = false;
            chkJJJsetaCP001.Checked = false;
            chkJJJsetaBI005.Checked = false;
            chkJJJsetaAN023.Checked = false;
            chkJJJsetaMI047.Checked = false;
            chkJJJsetaEL021.Checked = false;

            chkJJJsetbPA007.Checked = false;
            chkJJJsetbCP001.Checked = false;
            chkJJJsetbBI005.Checked = false;
            chkJJJsetbFA01450.Checked = false;
            chkJJJsetbMI047.Checked = false;
            chkJJJsetbEL021.Checked = false;

            txtJJJsetaPA007qty.Value = "0";
            txtJJJsetaCP001qty.Value = "0";
            txtJJJsetaBI005qty.Value = "0";
            txtJJJsetaAN023qty.Value = "0";

            txtJJJsetbPA007qty.Value = "0";
            txtJJJsetbCP001qty.Value = "0";
            txtJJJsetbBI005qty.Value = "0";
            txtJJJsetbFA01450qty.Value = "0";

            txtJJJsetcPA007qty.Value = "0";
            txtJJJsetcCP001qty.Value = "0";
            txtJJJsetcBI005qty.Value = "0";

            btnJJJDrugSet.Text = "สั่งยา Set D";
            btnJJJDrugSet.BackColor = Color.LightSteelBlue;
        }
        private void BtnJJJPrnStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printStaffNoteJJJ();
        }
        private void printStaffNoteJJJ()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerStaffNote;
            
            document.PrintPage += Document_PrintPage_StaffNote_JJJ;
            document.DefaultPageSettings.Landscape = true;
            
            document.Print();
        }
        private void Document_PrintPage_StaffNote_JJJ(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col22=150, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0, printadjust=0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            float col35=0, col45=0, ypos5 = 0;
            lbsbPttMessage.Text = bc.iniC.windows;
            lbsbPttStatus.Text = "col2 " + col2.ToString();
            int.TryParse(bc.iniC.printadjust, out printadjust);
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }
            else
            {
                col35 = col3 -15;
                col45 = col4 - 25;
                ypos5 = yPos + 10;
            }
            if (printadjust > 0)
            {
                col2 += printadjust;
                col4 += printadjust;
                col40 += printadjust;
                col40int += printadjust;
                col2int += printadjust;
                col35 += printadjust;
                col45 += printadjust;
                col3 += printadjust;
            }
            line = "5";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fPrn, Brushes.Black, xOffset, yPos, new StringFormat());leftMargin
            e.Graphics.DrawString(line, famtB, Brushes.Black, col35, ypos5, flags);
            e.Graphics.DrawString(line, famtB, Brushes.Black, col45, ypos5, flags);
            line = "H.N. " + ptt.MNC_HN_NO + "     " + vn.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 + 25, yPos + 5, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 + 30, yPos + 5, flags);

            line = "ชื่อ " + ptt.Name;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 + 20, yPos + 20, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 + 10, yPos + 20, flags);
            line = "เลขที่บัตร " + ptt.MNC_ID_NO;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 40, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 40, flags);
            line = lbPaidName;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 40, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 40, flags);

            line = compname.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 40, flags);

            line = "โรคประจำตัว        ไม่มี";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 60, flags);
            rec = new Rectangle(col2int + 75, 72, recx, recy);
            e.Graphics.DrawRectangle(blackPen, rec);

            line = "อายุ " + ptt.AgeStringShort1() + " [" + bc.datetoShow(ptt.MNC_BDAY) + "]";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 60, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 60, flags);
            //line = lbPaidName.Text.Trim();
            //textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);

            line = "มีโรค ระบุ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 70, yPos + 80, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 67 - recx, 92, recx, recy));

            line = "วันที่เวลา " + date;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 80, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 80, flags);

            line = "โรคเรื้อรัง";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 100, flags);
            line = "ชื่อแพทย์ " + dtrcode.Trim() + " " + dtrname;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 100, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 - 50, yPos + 120, flags);

            line = "DR Time.                               ปิดใบยา";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 120, flags);

            line = "อาการเบื้องต้น " + symptom.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 120, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 100, flags);

            line = "Temp";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 140, flags);

            line = "H.Rate";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 80, yPos + 140, flags);
            line = "R.Rate";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 160, yPos + 140, flags);
            line = "BP1";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 240, yPos + 140, flags);
            line = "Time :";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 300, yPos + 140, flags);
            line = "BP2 ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 380, yPos + 140, flags);
            line = "Time :";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 440, yPos + 140, flags);

            line = "Wt.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 160, flags);
            line = "Ht.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 80, yPos + 160, flags);
            line = "BMI.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 100, yPos + 160, flags);
            line = "CC.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 180, yPos + 160, flags);
            line = "CC.IN";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 240, yPos + 160, flags);
            line = "CC.EX";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 300, yPos + 160, flags);
            line = "Ab.C";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 400, yPos + 160, flags);
            line = "H.C.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 460, yPos + 160, flags);

            line = "Precaution (Med) _________________________________________ ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = "แพ้ยา/อาหาร/อื่นๆ         ไม่มี";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 180, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 180, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 123 - recx, yPosint + 180, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx, yPosint + 180, recx, recy));
            line = "มี ระบุอาการ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 20, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, yPosint + 200, recx, recy));
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 15, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 15 - recx, yPosint + 200, recx, recy));

            //line = "อาการเบื้อต้น  "+ txtSymptom.Text;
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 10, yPos + 220, flags);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = "O2 Sat __________        Pain Score __________";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 220, flags);

            //line = "โครงการ "+lbPaidName ;
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 , 295, flags);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 40, 295, flags);

            line = "อาการ ";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col22, 348, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 75 - recx - 5, 348, recx, recy));
            e.Graphics.DrawString(chkJJJ11.Text, fEdit, Brushes.Black, col22 + 70, 348, flags);     //"ปกติ"
            e.Graphics.DrawString(chkJJJ11.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 75 - recx -4, 335, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 140 - recx, 348, recx, recy));
            e.Graphics.DrawString(chkJJJ12.Text+" "+txtJJJ12.Text + " วัน", fEdit, Brushes.Black, col22 + 140, 348, flags);    //"ไข้   วัน"
            e.Graphics.DrawString(chkJJJ12.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 140 - recx - 2, 335, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 220 - recx, 348, recx, recy));
            e.Graphics.DrawString(chkJJJ13.Text, fEdit, Brushes.Black, col22 + 220, 348, flags);    //"ไอ   เจ็บคอ"
            e.Graphics.DrawString(chkJJJ13.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 220 - recx - 2, 335, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 310 - recx, 348, recx, recy));
            e.Graphics.DrawString(chkJJJ14.Text, fEdit, Brushes.Black, col22 + 310, 348, flags);    //"มีน้ำมูก"
            e.Graphics.DrawString(chkJJJ14.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 310 - recx - 2, 335, flags);



            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString())-20 - recx - 5, 378, recx, recy));
            e.Graphics.DrawString(chkJJJ21.Text, fEdit, Brushes.Black, col22 - 20, 378, flags);     //"อ่อนเพลีย"
            e.Graphics.DrawString(chkJJJ21.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) - 20 - recx - 2, 360, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 85 - recx - 5, 378, recx, recy));
            e.Graphics.DrawString(chkJJJ22.Text, fEdit, Brushes.Black, col22 + 85, 378, flags);     //"ปวดตามร่างกาย"
            e.Graphics.DrawString(chkJJJ22.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) +85 - recx - 2, 360, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 220 - recx - 5, 378, recx, recy));
            e.Graphics.DrawString(chkJJJ23.Text, fEdit, Brushes.Black, col22 + 220, 378, flags);     //"ปวดศีรษะ"
            e.Graphics.DrawString(chkJJJ23.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 220 - recx - 2, 360, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 310 - recx - 5, 378, recx, recy));
            e.Graphics.DrawString(chkJJJ24.Text, fEdit, Brushes.Black, col22 + 310, 378, flags);     //"มีเสมหะ"
            e.Graphics.DrawString(chkJJJ24.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) + 310 - recx - 2, 360, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 408, recx, recy));
            e.Graphics.DrawString(chkJJJ31.Text, fEdit, Brushes.Black, col22 - 20, 408, flags);     //"แน่นหน้าอก"
            e.Graphics.DrawString(chkJJJ31.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) - 20 - recx - 2, 395, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 85 - recx - 5, 408, recx, recy));
            e.Graphics.DrawString(chkJJJ32.Text + " " + txtJJJ32.Text + " ครั้ง", fEdit, Brushes.Black, col22 + 85, 408, flags);     //"ถ่ายเหลว"
            e.Graphics.DrawString(chkJJJ32.Checked ? "/" : "", famtB, Brushes.Black, int.Parse(col22.ToString()) +85 - recx - 2, 392, flags);


            line = symptom.Trim().Replace("โครงการ เจอ แจก จบ", "โครงการ OPSI");
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col3 -30, 280, flags);

            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 , 280, flags);

            //line = "อาการ";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            ////e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 360, flags);
            //line = "เอกซเรย์ปอด";
            //e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 70, yPos + 360, flags);

            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 50, yPos + 280, flags);

            //line = "สัมผัสผู้ป่วย ชื่อ";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 430, flags);
            String drugset = "";
            drugset = chkJJJDrugSetB.Checked ? "B" : chkJJJDrugSetC.Checked ? "C": chkJJJDrugSetD.Checked ? "D" : "A";
            line = "Set " + drugset.ToUpper();
            e.Graphics.DrawString(line, famtB14, Brushes.Black, col40 + 70, 335, flags);
            if (chkJJJDrugSetA.Checked)
            {
                if (chkJJJsetaPA007.Checked)
                {
                    e.Graphics.DrawString("PARAcetamol 500 MG. (3K2)", fEditB, Brushes.Black, col40, 403, flags);
                    e.Graphics.DrawString(txtJJJsetaPA007qty.Text.Trim()+ " TAB", fEditB, Brushes.Black, col40+340, 403, flags);
                }
                if (chkJJJsetaCP001.Checked)
                {
                    e.Graphics.DrawString("C.P.M. 4 MG. (3J2)", fEditB, Brushes.Black, col40, 463, flags);
                    e.Graphics.DrawString(txtJJJsetaCP001qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 463, flags);
                }
                if (chkJJJsetaBI005.Checked)
                {
                    e.Graphics.DrawString("biSOLVON( BROMHEXINE ) 8 MG. (3J2)", fEditB, Brushes.Black, col40, 520, flags);
                    e.Graphics.DrawString(txtJJJsetaBI005qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 520, flags);
                }
                if (chkJJJsetaAN023.Checked)
                {
                    e.Graphics.DrawString("ANDROGRAPHOLIDE 20MG (ฟ้าทะลายโจร)", fEditB, Brushes.Black, col40, 580, flags);
                    e.Graphics.DrawString(txtJJJsetaAN023qty.Text.Trim() + " BOT", fEditB, Brushes.Black, col40 + 340, 580, flags);
                }
            }
            else if (chkJJJDrugSetC.Checked)
            {
                if (chkJJJsetcPA007.Checked)
                {
                    e.Graphics.DrawString("PARAcetamol 500 MG. (3K2)", fEditB, Brushes.Black, col40, 403, flags);
                    e.Graphics.DrawString(txtJJJsetcPA007qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 403, flags);
                }
                if (chkJJJsetcCP001.Checked)
                {
                    e.Graphics.DrawString("C.P.M. 4 MG. (3J2)", fEditB, Brushes.Black, col40, 463, flags);
                    e.Graphics.DrawString(txtJJJsetcCP001qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 463, flags);
                }
                if (chkJJJsetcBI005.Checked)
                {
                    e.Graphics.DrawString("biSOLVON( BROMHEXINE ) 8 MG. (3J2)", fEditB, Brushes.Black, col40, 520, flags);
                    e.Graphics.DrawString(txtJJJsetcBI005qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 520, flags);
                }
            }
            else if (chkJJJDrugSetB.Checked)
            {
                if (chkJJJsetbPA007.Checked)
                {
                    e.Graphics.DrawString("PARAcetamol 500 MG. (3K2)", fEditB, Brushes.Black, col40, 403, flags);
                    e.Graphics.DrawString(txtJJJsetbPA007qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 403, flags);
                }
                if (chkJJJsetbCP001.Checked)
                {
                    e.Graphics.DrawString("C.P.M. 4 MG. (3J2)", fEditB, Brushes.Black, col40, 463, flags);
                    e.Graphics.DrawString(txtJJJsetbCP001qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 463, flags);
                }
                if (chkJJJsetbBI005.Checked)
                {
                    e.Graphics.DrawString("biSOLVON( BROMHEXINE ) 8 MG. (3J2)", fEditB, Brushes.Black, col40, 520, flags);
                    e.Graphics.DrawString(txtJJJsetbBI005qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 520, flags);
                }
                
                if (chkJJJsetbFA01450.Checked)
                {
                    e.Graphics.DrawString("FAVIPIRAVIR 200 MG.", fEditB, Brushes.Black, col40, 580, flags);
                    e.Graphics.DrawString(txtJJJsetbFA01450qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 580, flags);
                }
            }
            else if (chkJJJDrugSetD.Checked)
            {
                if (chkJJJsetdPA007.Checked)
                {
                    e.Graphics.DrawString("PARAcetamol 500 MG. (3K2)", fEditB, Brushes.Black, col40, 403, flags);
                    e.Graphics.DrawString(txtJJJsetdPA007qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 403, flags);
                }
                if (chkJJJsetdCP001.Checked)
                {
                    e.Graphics.DrawString("C.P.M. 4 MG. (3J2)", fEditB, Brushes.Black, col40, 463, flags);
                    e.Graphics.DrawString(txtJJJsetdCP001qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 463, flags);
                }
                if (chkJJJsetdBI005.Checked)
                {
                    e.Graphics.DrawString("biSOLVON( BROMHEXINE ) 8 MG. (3J2)", fEditB, Brushes.Black, col40, 520, flags);
                    e.Graphics.DrawString(txtJJJsetdBI005qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 520, flags);
                }

                if (chkJJJsetdFA01464.Checked)
                {
                    e.Graphics.DrawString("FAVIPIRAVIR 200 MG.", fEditB, Brushes.Black, col40, 580, flags);
                    e.Graphics.DrawString(txtJJJsetdFA01464qty.Text.Trim() + " TAB", fEditB, Brushes.Black, col40 + 340, 580, flags);
                }
            }
            e.Graphics.DrawString(chkJJJsetcMI047.Text.Trim(), fEditB, Brushes.Black, col40, 640, flags);
            e.Graphics.DrawString(txtJJJsetcMI047qty.Text.Trim() + " BOT", fEditB, Brushes.Black, col40 + 340, 640, flags);
            e.Graphics.DrawString(chkJJJsetaEL021.Text.Trim(), fEditB, Brushes.Black, col40, 700, flags);
            e.Graphics.DrawString(txtJJJsetaEL021qty.Text.Trim() + " PIE", fEditB, Brushes.Black, col40 + 340, 700, flags);

            //line = "สัมผัสล่าสุด";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            ////e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 475, flags);
            //line = "เครื่องวัดออกซิเจน ปลายนิ้ว    1";
            //e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 70, yPos + 475, flags);
            //line = "ปรอทวัดอุณหภูมิ     1";
            //e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 70, yPos + 515, flags);

            line = "COVID 19";
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col2+80, 600, flags);
            line = "คำแนะนำ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 620, flags);
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 75 - recx, yPosint + 620, recx, recy));

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 635, recx, recy));
            e.Graphics.DrawString(chkJJJc11.Text, fEdit, Brushes.Black, col22 - 20, 635, flags);     //"กักกันตัว 10 วัน"
            e.Graphics.DrawString(chkJJJc22.Checked ? "/" : "", famtB, Brushes.Black, col22 -20 - recx - 2, 617, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 220 - recx - 5, 635, recx, recy));
            e.Graphics.DrawString(chkJJJc12.Text, fEdit, Brushes.Black, col22 + 220, 635, flags);     //"ห้ามออกจากบ้าน"
            e.Graphics.DrawString(chkJJJc22.Checked ? "/" : "", famtB, Brushes.Black, col22 + 220 - recx - 2, 617, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 660, recx, recy));
            e.Graphics.DrawString(chkJJJc21.Text, fEdit, Brushes.Black, col22 - 20, 660, flags);     //"งดการใช้ของร่วมกับผู้อื่น"
            e.Graphics.DrawString(chkJJJc22.Checked ? "/" : "", famtB, Brushes.Black, col22 -20 - recx - 2, 645, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 150 - recx - 5, 660, recx, recy));
            e.Graphics.DrawString(chkJJJc22.Text, fEdit, Brushes.Black, col22 + 150, 660, flags);     //"สวมหน้ากากอนามัย"
            e.Graphics.DrawString(chkJJJc22.Checked ? "/" : "", famtB, Brushes.Black, col22 + 150 - recx - 2, 645, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 290 - recx - 5, 660, recx, recy));
            e.Graphics.DrawString(chkJJJc23.Text, fEdit, Brushes.Black, col22 + 290, 660, flags);     //"หมั่นล้างมือ"
            e.Graphics.DrawString(chkJJJc23.Checked ? "/" : "", famtB, Brushes.Black, col22 + 290 - recx - 2, 645, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) - 20 - recx - 5, 690, recx, recy));
            e.Graphics.DrawString(chkJJJc31.Text, fEdit, Brushes.Black, col22 - 20, 690, flags);     //"กินร้อน ช้อนกลาง"
            e.Graphics.DrawString(chkJJJc31.Checked ? "/" : "", famtB, Brushes.Black, col22 -20 - recx - 2, 675, flags);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(int.Parse(col22.ToString()) + 220 - recx - 5, 690, recx, recy));
            e.Graphics.DrawString(chkJJJc32.Text, fEdit, Brushes.Black, col22 + 220, 690, flags);     //"ใบรับรองแพทย์"
            e.Graphics.DrawString(chkJJJc32.Checked ? "/":"", famtB, Brushes.Black, col22 + 220 - recx - 2, 675, flags);

            
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 210 - recx, yPosint + 620, recx, recy));

            //line = "การตรวจสุขภาพประจำปี          การพบแพทย์เฉพาะทาง       อื่นๆ";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 640, flags);
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 35 - recx, yPosint + 640, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 190 - recx, yPosint + 640, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 3350 - recx, yPosint + 640, recx, recy));

            //line = "ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 660, flags);
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 170 - recx, yPosint + 660, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 215 - recx, yPosint + 660, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 345 - recx, yPosint + 660, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 385 - recx, yPosint + 660, recx, recy));

            //line = "ชื่อผู้รับ _____________________________";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 680, flags);

            //line = "Health Education :";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 730, flags);

            //line = "ลงชื่อพยาบาล: _____________________________________";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 750, flags);

            line = "FM-REC-002 (00 10/09/53)(1/1)";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 770, flags);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col40, yPos + 770, flags);
        }
        private void setTheme(String theme)
        {
            theme1.SetTheme(this, theme);//pnSrcTop
            //theme1.SetTheme(groupBox1, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox2, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox3, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox5, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox6, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox4, bc.iniC.themeApp);
            //theme1.SetTheme(pnPttComp, bc.iniC.themeApp);
            //theme1.SetTheme(tC1, theme);
            //theme1.SetTheme(tabPttNew, theme);
            //theme1.SetTheme(c1StatusBar1, theme);
            //foreach (Control c in this.Controls)
            //{
            //    if(c is C1TextBox)
            //    {
            //        theme1.SetTheme(c, bc.iniC.themeApp);
            //    }
            //}
            panel3.BackColor = Color.DarkGoldenrod;
            chkJJJDrugSetA.BackColor = Color.DarkGoldenrod;
            panel4.BackColor = Color.DarkKhaki;
            chkJJJDrugSetB.BackColor = Color.DarkKhaki;
            panel5.BackColor = Color.DarkSalmon;
            chkJJJDrugSetC.BackColor = Color.DarkSalmon;
            panel6.BackColor = Color.LightSteelBlue;
            chkJJJDrugSetD.BackColor = Color.LightSteelBlue;
        }
        private void FrmSmartCardJJJ_Load(object sender, EventArgs e)
        {
            setTheme("Office2010Red");
            lbsbPttMessage.Text = bc.iniC.windows;
            lbsbPttStatus.Text = bc.iniC.printadjust;
        }
    }
}
