using bangna_hospital.control;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmOPDCovid:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;
        Panel pnTop;

        Label lbtxtHospName, lbtxtHn, lbtxtPttName, lbtxtVsDate, lbtxtSex, lbtxtAge, lbtxtDOB, lbtxtPID, lbtxtPassport, lbtxtAddr1, lbtxtAddr2, lbtxtDtrId, lbtxtLabCode, lbtxtLabResult;
        C1TextBox txtHospName, txtHn, txtPttName, txtSex, txtAge, txtDOB, txtPID, txtPassport, txtAddr1, txtAddr2, txtDtrId, txtDtrName, txtLabCode, txtLabName, txtLabResult;
        C1DateEdit txtVsDate;
        C1Button btnHn, btnPrnThai, btnPrnEng;

        public FrmOPDCovid(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            this.Load += FrmOPDCovid_Load;
            initCompoment();
        }
        private void initCompoment()
        {
            int gapLine = 20, gapX = 20, gapY = 20, col1 = 130, col2 = 330, col3 = 630;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            lbtxtHospName = new Label();
            bc.setControlLabel(ref lbtxtHospName, fEdit, "โรงพยาบาล", "lbTxtHospName", gapX, gapY);
            txtHospName = new C1TextBox();
            bc.setControlC1TextBox(ref txtHospName, fEdit, "txtHospName", 250, lbtxtHospName.Location.X + lbtxtHospName.Width, gapY);

            gapY += gapLine;
            lbtxtHn = new Label();
            bc.setControlLabel(ref lbtxtHn, fEdit, "HN :", "lbTxtHn", gapX, gapY);
            txtHn = new C1TextBox();
            bc.setControlC1TextBox(ref txtHn, fEdit, "txtHn", 80, lbtxtHn.Location.X + lbtxtHn.Width+20, gapY);
            btnHn = new C1Button();
            bc.setControlC1Button(ref btnHn, fEdit, "...", "btnHn", txtHn.Location.X + lbtxtHn.Width+20, gapY);
            lbtxtVsDate = new Label();
            bc.setControlLabel(ref lbtxtVsDate, fEdit, "วันที่ตรวจ :", "lbtxtVsDate", gapX, gapY);
            txtVsDate = new C1DateEdit();
            bc.setControlC1DateTimeEdit(ref txtVsDate, "txtVsDate", lbtxtVsDate.Location.X + lbtxtVsDate.Width + 20, gapY);

            gapY += gapLine;
            lbtxtPttName = new Label();
            bc.setControlLabel(ref lbtxtPttName, fEdit, "ชื่อ-นามสกุล :", "lbtxtPttName", gapX, gapY);
            txtPttName = new C1TextBox();
            bc.setControlC1TextBox(ref txtPttName, fEdit, "txtPttName", 200, lbtxtPttName.Location.X + lbtxtPttName.Width + 20, gapY);

            gapY += gapLine;
            lbtxtSex = new Label();
            bc.setControlLabel(ref lbtxtSex, fEdit, "เพศ :", "lbtxtSex", gapX, gapY);
            txtSex = new C1TextBox();
            bc.setControlC1TextBox(ref txtSex, fEdit, "txtSex", 80, lbtxtSex.Location.X + lbtxtSex.Width + 20, gapY);
            lbtxtAge = new Label();
            bc.setControlLabel(ref lbtxtAge, fEdit, "อายุ :", "lbtxtAge", gapX, gapY);
            txtAge = new C1TextBox();
            bc.setControlC1TextBox(ref txtAge, fEdit, "txtAge", 80, lbtxtAge.Location.X + lbtxtAge.Width + 20, gapY);
            lbtxtDOB = new Label();
            bc.setControlLabel(ref lbtxtDOB, fEdit, "DOB :", "lbtxtDOB", gapX, gapY);
            txtDOB = new C1TextBox();
            bc.setControlC1TextBox(ref txtDOB, fEdit, "txtDOB", 80, lbtxtDOB.Location.X + lbtxtDOB.Width + 20, gapY);

            gapY += gapLine;
            lbtxtPID = new Label();
            bc.setControlLabel(ref lbtxtPID, fEdit, "บัตรประชาชน :", "lbtxtPID", gapX, gapY);
            txtPID = new C1TextBox();
            bc.setControlC1TextBox(ref txtPID, fEdit, "txtSex", 80, lbtxtPID.Location.X + lbtxtPID.Width + 20, gapY);
            lbtxtPassport = new Label();
            bc.setControlLabel(ref lbtxtPassport, fEdit, "Passport :", "lbtxtPassport", gapX, gapY);
            txtPassport = new C1TextBox();
            bc.setControlC1TextBox(ref txtPassport, fEdit, "txtPassport", 80, lbtxtPassport.Location.X + lbtxtPassport.Width + 20, gapY);

            gapY += gapLine;
            lbtxtAddr1 = new Label();
            bc.setControlLabel(ref lbtxtAddr1, fEdit, "สถานที่อยู่ :", "lbtxtAddr1", gapX, gapY);
            txtAddr1 = new C1TextBox();
            bc.setControlC1TextBox(ref txtAddr1, fEdit, "txtAddr1", 80, lbtxtAddr1.Location.X + lbtxtAddr1.Width + 20, gapY);
            gapY += gapLine;
            txtAddr2 = new C1TextBox();
            bc.setControlC1TextBox(ref txtAddr2, fEdit, "txtAddr2", 80, lbtxtAddr2.Location.X + lbtxtAddr2.Width + 20, gapY);

            gapY += gapLine;
            lbtxtDtrId = new Label();
            bc.setControlLabel(ref lbtxtDtrId, fEdit, "แพทย์ผู้ตรวจ :", "lbtxtDtrId", gapX, gapY);
            txtDtrId = new C1TextBox();
            bc.setControlC1TextBox(ref txtDtrId, fEdit, "txtDtrId", 80, lbtxtDtrId.Location.X + lbtxtDtrId.Width + 20, gapY);
            txtDtrName = new C1TextBox();
            bc.setControlC1TextBox(ref txtDtrName, fEdit, "txtDtrName", 80, txtDtrName.Location.X + txtDtrName.Width + 10, gapY);
            btnPrnThai = new C1Button();
            bc.setControlC1Button(ref btnPrnThai, fEdit, "...", "btnPrnThai", txtDtrName.Location.X + txtDtrName.Width + 40, gapY);
            btnPrnEng = new C1Button();
            bc.setControlC1Button(ref btnPrnEng, fEdit, "...", "btnPrnEng", btnPrnThai.Location.X + btnPrnThai.Width + 20, gapY);

            gapY += gapLine;
            gapY += gapLine;
            lbtxtLabCode = new Label();
            bc.setControlLabel(ref lbtxtLabCode, fEdit, "Lab code :", "lbtxtLabCode", gapX, gapY);
            txtLabCode = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabCode, fEdit, "txtLabCode", 80, lbtxtLabCode.Location.X + lbtxtLabCode.Width + 20, gapY);
            txtLabName = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabName, fEdit, "txtLabName", 80, txtLabName.Location.X + txtLabName.Width + 10, gapY);
            lbtxtLabResult = new Label();
            bc.setControlLabel(ref lbtxtLabResult, fEdit, "Result :", "lbtxtLabResult", txtLabName.Location.X + txtLabName.Width + 40, gapY);
            txtLabResult = new C1TextBox();
            bc.setControlC1TextBox(ref txtLabResult, fEdit, "txtLabResult", 80, lbtxtLabResult.Location.X + lbtxtLabResult.Width + 20, gapY);


            this.Controls.Add(lbtxtHospName);
            this.Controls.Add(txtHospName);
            this.Controls.Add(lbtxtHn);
            this.Controls.Add(txtHn);
            this.Controls.Add(lbtxtVsDate);
            this.Controls.Add(txtVsDate);
            this.Controls.Add(lbtxtPttName);
            this.Controls.Add(txtPttName);
            this.Controls.Add(lbtxtSex);
            this.Controls.Add(txtSex);
            this.Controls.Add(lbtxtAge);
            this.Controls.Add(txtAge);
            this.Controls.Add(lbtxtDOB);
            this.Controls.Add(txtDOB);
            this.Controls.Add(lbtxtPID);
            this.Controls.Add(txtPID);
            this.Controls.Add(lbtxtPassport);
            this.Controls.Add(txtPassport);
            this.Controls.Add(lbtxtAddr1);
            this.Controls.Add(txtAddr1);
            this.Controls.Add(lbtxtAddr2);
            this.Controls.Add(txtAddr2);
            this.Controls.Add(lbtxtDtrId);
            this.Controls.Add(txtDtrId);
            this.Controls.Add(txtDtrId);
            this.Controls.Add(txtDtrName);
            this.Controls.Add(btnPrnThai);
            this.Controls.Add(btnPrnEng);
            this.Controls.Add(lbtxtLabCode);
            this.Controls.Add(txtLabCode);
            this.Controls.Add(txtLabName);
            this.Controls.Add(lbtxtLabResult);
            this.Controls.Add(txtLabResult);
        }
        private void FrmOPDCovid_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
    }
}
