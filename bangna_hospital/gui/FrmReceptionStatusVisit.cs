using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace bangna_hospital.gui
{
    public partial class FrmReceptionStatusVisit : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, fque, fqueB;
        C1FlexGrid grfStatus, grfLimit;
        C1ThemeController theme1;
        String HN = "", PRENO = "", VSDATE = "", FORMNAME="", PTTNAME="", SYMPTOMS="", COMPNAME="", INSURNAME="", VSREMARK="", PAD="'", VN="", DEPTNAME="";
        Patient ptt;
        PatientT013 pt013;
        AutoCompleteStringCollection autoPaid, autoComp;
        Boolean flagNew=false;

        int colGrfActNo = 1, colGrfActDesc = 2, colGrfActNoDate = 3, colGrfActTime = 4, colGrfActNoDeptName = 5, colGrfActUserName=6;
        int colGrfLimitDesc = 1, colGrfLimitCredit = 2, colGrfLimitVsDate = 3, colGrfLimitpad = 4, colGrfLimitRunNo = 5, colGrfLimitRunDate = 6, colGrfLimitDocNo = 7, colGrfLimitDocDate = 8;
        Boolean pageLoad = false;
        public FrmReceptionStatusVisit(BangnaControl bc, String FormName, String hn, String pttname, String preno,String vsdate, String symptoms, String compname, String insurname, String remark, String pad, String vn, String deptname)
        {
            this.bc = bc;
            this.HN = hn;
            this.PRENO = preno;
            this.VSDATE = vsdate;
            this.FORMNAME = FormName;
            this.PTTNAME = pttname;
            this.SYMPTOMS = symptoms;
            this.COMPNAME = compname;
            this.INSURNAME = insurname;
            this.VSREMARK = remark;
            this.PAD = pad;
            this.VN = vn;
            this.DEPTNAME = deptname;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 20, FontStyle.Bold);

            theme1 = new C1ThemeController();
            
            pt013 = new PatientT013();
            autoPaid = new AutoCompleteStringCollection();
            autoComp = new AutoCompleteStringCollection();

            btnNew.Click += BtnNew_Click;
            btnSave.Click += BtnSave_Click;
            txtCreditLimit.KeyPress += TxtCreditLimit_KeyPress;            
            txtAmt.KeyPress += TxtAmt_KeyPress;
            txtCreditLimit.KeyUp += TxtCreditLimit_KeyUp;
            txtRemark.KeyUp += TxtRemark_KeyUp;
            txtInsur.KeyUp += TxtInsur_KeyUp;
            txtName.KeyUp += TxtName_KeyUp;
            txtPaidCode.KeyUp += TxtPaidCode_KeyUp;
            cboLimitCredit.SelectedItemChanged += CboLimitCredit_SelectedItemChanged;
            txtVsPaid.KeyUp += TxtVsPaid_KeyUp;
            btnVsSave.Click += BtnVsSave_Click;
            txtVsUser.KeyUp += TxtVsUser_KeyUp;

            bc.bcDB.pt013DB.setCboPaidName(cboLimitCredit, "", HN);
            bc.bcDB.pttDB.setCboDeptOPDNew(cboVsDept, "");     //ส่งตัวไป
            cboVsDept.DropDownClosed += CboVsDept_DropDownClosed;
            initGrfStatus();
            initGrfLimit();
            setControl();
            setControlRew();
            //c1SplitterPanel2
            theme1.SetTheme(panel6, "Office2010Blue");
            foreach (Control c in c1SplitterPanel2.Controls)
            {
                theme1.SetTheme(c, "Office2010Blue");
            }
            pageLoad = false;
        }

        private void CboVsDept_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void TxtVsUser_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbPttUser.Text = bc.bcDB.stfDB.selectByPassword(txtVsUser.Text.Trim());
                if (lbPttUser.Text.Length > 0)
                {
                    BtnVsSave_Click(null, null);
                    Thread.Sleep(500);
                    this.Dispose();
                }
            }
        }

        private void BtnVsSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (lbPttUser.Text.Length <= 0)
            {
                lfSbMessage.Text = "user ผู้แก้ไข";
                txtVsUser.Select();
                return;
            }
            String paidcode = bc.bcDB.finM02DB.getPaidCode(txtVsPaid.Text.Trim());
            String secno = cboVsDept.SelectedItem == null ? "" : ((ComboBoxItem)cboVsDept.SelectedItem).Value;
            String deptCode = bc.bcDB.pm32DB.selectDeptOPDBySecNO(secno);
            String insurcode = bc.bcDB.pm24DB.getPaidCode(txtPttInsur.Text.Trim());
            String compcode = bc.bcDB.pm24DB.getPaidCode(txtPttComp.Text.Trim());
            String re = bc.bcDB.vsDB.updateVisit(txtHN.Text, VSDATE, PRENO, paidcode, txtVsSymptom.Text.Trim(), deptCode, secno, txtVsUser.Text.Trim());
            String re1 = bc.bcDB.vsDB.updateVisitInsurComp(txtHN.Text, VSDATE, PRENO, insurcode, compcode, txtVsUser.Text.Trim());
            lfSbMessage.Text = re;
        }

        private void TxtVsPaid_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txtVsPaid.Text, out _))//ป้อนเป็น ตัวเลข
                {
                    txtVsPaid.Value = bc.bcDB.finM02DB.getPaidName(txtVsPaid.Text.Trim());
                }
                txtVsUser.SelectAll();
                txtVsUser.Select();
            }
        }

        private void CboLimitCredit_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String txt = cboLimitCredit.SelectedItem == null ? "" : ((ComboBoxItem)cboLimitCredit.SelectedItem).Value;
            if(txt.Length<=0) return;
            String[] chk = txt.Split('@');
            txtDocNo.Value = chk[0];
            txtDocDate.Value = chk[1];
            rbSbMessage.Text = txtDocNo.Text+"."+txtDocDate.Text;
            PatientT014 pt014 = new PatientT014();
            pt014 = bc.bcDB.pt013DB.selectByRunNo(HN, txtDocDate.Text, txtDocNo.Text);
            txtName.Text = pt014.MNC_DIA_DSC;
            txtCreditLimit.Value = pt014.MNC_REW_PRI;
            txtAmt.Value = pt014.MNC_SUM_PAD;
            lfSbMessage.Text = "ต้องการใช้วงเงิน " + pt014.MNC_DIA_DSC+ " วงเงิน " + pt014.MNC_REW_PRI+ " กับ visitนี้";
            setGrfLimit(pt014.MNC_DOC_NO,pt014.MNC_DOC_DAT);
        }

        private void TxtPaidCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //bc.setC1Combo(cboVsPaid, txtVsPaidCode.Text.Trim());
                if (int.TryParse(txtPaidCode.Text, out _))//ป้อนเป็น ตัวเลข
                {
                    txtPaidCode.Value = bc.bcDB.finM02DB.getPaidName(txtPaidCode.Text.Trim());
                }
                //โปรแกรม ช่วยตรวจสอบ และแสดง message
                String chk = bc.bcDB.finM02DB.getPaidCode(txtPaidCode.Text.Trim());
                lfSbMessage.Text = "เลือกสิทธิการรักษา " + chk;
            }
        }

        private void TxtName_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCreditLimit.Focus();
            }
        }

        private void TxtInsur_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCardNo.Focus();
            }
        }

        private void TxtRemark_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtInsur.Focus();
            }
        }

        private void TxtCreditLimit_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (flagNew)
            {
                txtAmt.Value = txtCreditLimit.Text;
            }
            if(e.KeyCode == Keys.Enter)
            {
                txtCurPaid.Value = flagNew ? "0" : txtCurPaid.Text.Trim();
                txtRemark.Focus();
            }
        }

        private void TxtAmt_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
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
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (getPatientT013())
            {
                String re = bc.bcDB.pt013DB.insertLimitCredit(pt013, rbDocNoOld.Text, rbDocDateOld.Text);
                if (int.TryParse(re, out int chk))
                {
                    flagNew = false;
                    lfSbMessage.Text = "บันทึกข้อมูลเรียบร้อย";
                    PatientT013 pt013 = bc.bcDB.pt013DB.selectByPreno(HN, VSDATE, PRENO);
                    setGrfLimit(pt013.MNC_DOC_NO, pt013.MNC_DOC_DAT);
                }
                else
                {
                    lfSbMessage.Text = re;
                }
            }
        }
        private Boolean getPatientT013()
        {
            Boolean chk = true;
            try
            {
                pt013.MNC_DATE = VSDATE;
                pt013.MNC_DOC_CD = "";
                pt013.MNC_DOC_DAT = txtDocDate.Text.Trim();
                pt013.MNC_DOC_NO = txtDocNo.Text.Trim();
                pt013.MNC_DOC_STS = "";
                pt013.MNC_DOC_YR = "";
                pt013.MNC_HN_NO = txtHN.Text.Trim();
                pt013.MNC_HN_YR = "";
                pt013.MNC_PRE_NO = PRENO;
                pt013.MNC_REW_PRI = txtCreditLimit.Text.Trim();
                pt013.MNC_RUN_NO = txtRewId.Text.Trim();
                pt013.MNC_FN_TYP_CD = bc.bcDB.finM02DB.getPaidCode(txtPaidCode.Text.Trim());
                pt013.COM_CD = bc.bcDB.pm24DB.selectCustByName1(txtInsur.Text.Trim());
                pt013.RES_MAS = bc.bcDB.pm24DB.selectCustByName1(txtPttComp.Text.Trim());
                pt013.DIA_DSC = txtName.Text.Trim();
                pt013.MNC_CUR_PAD = txtCurPaid.Text.Trim().Replace(",","");
            }
            catch (Exception ex)
            {
                chk = false;
            }
            return chk;
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlLimitCredit();
            flagNew = true;
            txtPaidCode.Value = PAD;
            txtInsur.Value = INSURNAME;
            bc.setC1Combo(cboLimitCredit, "");
            txtDocNo.Value = "";
            txtDocDate.Value = "";
        }
        private void setControl()
        {
            txtHN.Value = HN;
            lbPttNameT.Text = PTTNAME;
            txtVsSymptom.Value = SYMPTOMS;
            txtPttComp.Value = COMPNAME;
            txtPttInsur.Value = INSURNAME;
            txtPttRemark.Value = "";
            txtCurPaid.Value = "";
            txtPaidCode.Value = PAD;
            txtVsPaid.Value = PAD;//แก้ไขสิทธิ
            txtVN.Value = VN;
            bc.setC1Combo(cboVsDept,bc.bcDB.pm32DB.selectSECNOOPDBySecName( DEPTNAME));
            lbVN.Text = VN;
            lbVsDate.Text = bc.datetoShow(VSDATE);
            txtInsur.Value = INSURNAME;
        }
        private void setControlRew()
        {
            pt013 = bc.bcDB.pt013DB.selectByPreno(HN, VSDATE, PRENO);
            lbLimitCreditOld.Text = "";
            rbDocDateOld.Text = pt013.MNC_DOC_DAT;
            rbDocNoOld.Text = pt013.MNC_DOC_NO;
            Double.TryParse(pt013.MNC_REW_PRI, out Double limitcredit);
            Double.TryParse(pt013.MNC_SUM_PAD, out Double limituse);
            txtAmt.Value = limitcredit - limituse;
            txtCreditLimit.Value = pt013.MNC_REW_PRI;
            txtDocDate.Value = pt013.MNC_DOC_DAT;
            txtDocNo.Value = pt013.MNC_DOC_NO;
            txtCardNo.Value = "";
            txtRewId.Value = pt013.MNC_RUN_NO;
            txtCurPaid.Value = pt013.MNC_CUR_PAD;
            txtRemark.Value = VSREMARK;
            txtName.Value = "";
            txtPaidCode.Value = pt013.MNC_RUN_NO.Length<=0 ? PAD : bc.bcDB.finM02DB.getPaidName(pt013.MNC_FN_TYP_CD);
            txtName.Value = pt013.DIA_DSC;
            setGrfLimit(pt013.MNC_DOC_NO, pt013.MNC_DOC_DAT);
            String paid = "";
            paid = bc.bcDB.fint01DB.SelectPADByPreno(HN, VSDATE, PRENO, bc.bcDB.finM02DB.getPaidCode(PAD));
            float.TryParse(paid, out float paid1);
            float.TryParse(txtCreditLimit.Text, out float credit);
            txtCurPaid.Value = paid;
            txtAmt.Value = (credit - paid1).ToString("#,###.00");
            bc.setC1Combo(cboLimitCredit, "");
            rb1.Text = pt013.MNC_DOC_NO;
            rbSbMessage.Text = pt013.MNC_DOC_DAT;
            if (pt013.MNC_DOC_NO.Length > 0)
            {
                lbLimitCreditOld.Text = "มีวงเงิน "+ pt013.DIA_DSC+" "+ pt013.MNC_REW_PRI;
            }
        }
        private void clearControlLimitCredit()
        {
            txtAmt.Value = "";
            txtCreditLimit.Value = "";
            txtInsur.Value = "";
            txtCardNo.Value = "";
            txtRewId.Value = "";
            txtCurPaid.Value = "";
            txtRemark.Value = "";
            txtName.Value = "";
            txtDocNo.Value = "";
            txtPaidCode.Value = "";
        }
        private void initGrfLimit()
        {
            grfLimit = new C1FlexGrid();
            grfLimit.Font = fEdit;
            grfLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLimit.Location = new System.Drawing.Point(0, 0);
            grfLimit.Rows.Count = 1;
            grfLimit.Cols.Count = 9;
            grfLimit.Cols[colGrfLimitDesc].DataType = typeof(String);
            grfLimit.Cols[colGrfLimitCredit].DataType = typeof(String);
            grfLimit.Cols[colGrfLimitVsDate].DataType = typeof(String);
            grfLimit.Cols[colGrfLimitpad].DataType = typeof(String);

            grfLimit.Cols[colGrfLimitDesc].TextAlign = TextAlignEnum.LeftCenter;
            grfLimit.Cols[colGrfLimitCredit].TextAlign = TextAlignEnum.RightCenter;
            grfLimit.Cols[colGrfLimitVsDate].TextAlign = TextAlignEnum.CenterCenter;
            grfLimit.Cols[colGrfLimitpad].TextAlign = TextAlignEnum.RightCenter;

            grfLimit.Cols[colGrfLimitDesc].Width = 200;
            grfLimit.Cols[colGrfLimitCredit].Width = 120;
            grfLimit.Cols[colGrfLimitVsDate].Width = 100;
            grfLimit.Cols[colGrfLimitpad].Width = 120;

            grfLimit.ShowCursor = true;
            grfLimit.Cols[colGrfLimitDesc].Caption = "รายการ";
            grfLimit.Cols[colGrfLimitCredit].Caption = "วงเงิน";
            grfLimit.Cols[colGrfLimitVsDate].Caption = "วันที่มารักษา";
            grfLimit.Cols[colGrfLimitpad].Caption = "ยอดใช้";

            grfLimit.Cols[colGrfLimitDesc].Visible = true;
            grfLimit.Cols[colGrfLimitCredit].Visible = true;
            grfLimit.Cols[colGrfLimitVsDate].Visible = true;
            grfLimit.Cols[colGrfLimitpad].Visible = true;
            grfLimit.Cols[colGrfLimitRunNo].Visible = false;
            grfLimit.Cols[colGrfLimitRunDate].Visible = false;
            grfLimit.Cols[colGrfLimitDocNo].Visible = false;
            grfLimit.Cols[colGrfLimitDocDate].Visible = false;

            grfLimit.Cols[colGrfLimitDesc].AllowEditing = false;
            grfLimit.Cols[colGrfLimitCredit].AllowEditing = false;
            grfLimit.Cols[colGrfLimitVsDate].AllowEditing = false;
            grfLimit.Cols[colGrfLimitpad].AllowEditing = false;

            panel4.Controls.Add(grfLimit);
            theme1.SetTheme(grfLimit, "Office2010Blue");
        }
        private void setGrfLimit(String docno, String docdate)
        {
            //if (pageLoad) return;
            DataTable dtvs = new DataTable();
            rb1.Text = docno;
            rbSbMessage.Text = docdate;
            dtvs = bc.bcDB.pt013DB.SelectByLimitNo(txtHN.Text.Trim(), docno, docdate);
            double total = 0;
            grfLimit.Rows.Count = 1; grfLimit.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfLimit.Rows[i];
                float.TryParse(row1["MNC_CUR_PAD"].ToString(), out float paid1);
                float.TryParse(row1["MNC_REW_PRI"].ToString(), out float credit);
                rowa[colGrfLimitDesc] = row1["MNC_DIA_DSC"].ToString();
                rowa[colGrfLimitCredit] = credit.ToString("#,###.00");
                rowa[colGrfLimitVsDate] = bc.datetoShow1(row1["MNC_DATE"].ToString());
                rowa[colGrfLimitpad] = paid1.ToString("#,###.00");
                rowa[colGrfLimitRunNo] = row1["MNC_RUN_NO"].ToString();
                rowa[colGrfLimitRunDate] = row1["MNC_RUN_DAT"].ToString();
                rowa[colGrfLimitDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colGrfLimitDocDate] = row1["MNC_DOC_DAT"].ToString();
                total += paid1;
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void initGrfStatus()
        {
            grfStatus = new C1FlexGrid();
            grfStatus.Font = fEdit;
            grfStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            grfStatus.Location = new System.Drawing.Point(0, 0);
            grfStatus.Rows.Count = 1;
            grfStatus.Cols.Count = 7;
            grfStatus.Cols[colGrfActNo].DataType = typeof(String);
            grfStatus.Cols[colGrfActDesc].DataType = typeof(String);
            grfStatus.Cols[colGrfActNoDate].DataType = typeof(String);
            grfStatus.Cols[colGrfActTime].DataType = typeof(String);
            grfStatus.Cols[colGrfActNoDeptName].DataType = typeof(String);

            grfStatus.Cols[colGrfActNo].TextAlign = TextAlignEnum.CenterCenter;
            grfStatus.Cols[colGrfActDesc].TextAlign = TextAlignEnum.LeftCenter;
            grfStatus.Cols[colGrfActNoDate].TextAlign = TextAlignEnum.CenterCenter;
            grfStatus.Cols[colGrfActTime].TextAlign = TextAlignEnum.CenterCenter;
            grfStatus.Cols[colGrfActNoDeptName].TextAlign = TextAlignEnum.CenterCenter;
            
            grfStatus.Cols[colGrfActNo].Width = 70;
            grfStatus.Cols[colGrfActDesc].Width = 200;
            grfStatus.Cols[colGrfActNoDate].Width = 100;
            grfStatus.Cols[colGrfActTime].Width = 70;
            grfStatus.Cols[colGrfActNoDeptName].Width = 90;
            grfStatus.Cols[colGrfActUserName].Width = 140;

            grfStatus.ShowCursor = true;
            grfStatus.Cols[colGrfActNo].Caption = "code";
            grfStatus.Cols[colGrfActDesc].Caption = "description";
            grfStatus.Cols[colGrfActNoDate].Caption = "Date";
            grfStatus.Cols[colGrfActTime].Caption = "Time";
            grfStatus.Cols[colGrfActNoDeptName].Caption = "แผนก";

            grfStatus.Cols[colGrfActNo].Visible = true;
            grfStatus.Cols[colGrfActDesc].Visible = true;
            grfStatus.Cols[colGrfActNoDate].Visible = true;
            grfStatus.Cols[colGrfActTime].Visible = true;
            grfStatus.Cols[colGrfActNoDeptName].Visible = true;

            grfStatus.Cols[colGrfActNo].AllowEditing = false;
            grfStatus.Cols[colGrfActDesc].AllowEditing = false;
            grfStatus.Cols[colGrfActNoDate].AllowEditing = false;
            grfStatus.Cols[colGrfActTime].AllowEditing = false;
            grfStatus.Cols[colGrfActNoDeptName].AllowEditing = false;
            
            panel3.Controls.Add(grfStatus);
            theme1.SetTheme(grfStatus, bc.iniC.themeApp);
        }
        private void setGrfStatus(String preno, String vsdate)
        {
            if (pageLoad) return;
            DataTable dtvs = new DataTable();
            
            dtvs = bc.bcDB.pt03DB.SelectByVsDate(txtHN.Text.Trim(), PRENO, VSDATE);
            
            grfStatus.Rows.Count = 1; grfStatus.Rows.Count = dtvs.Rows.Count+1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfStatus.Rows[i];
                rowa[colGrfActNo] = row1["MNC_ACT_NO"].ToString();
                rowa[colGrfActDesc] = row1["MNC_ACT_DSC"].ToString();
                rowa[colGrfActNoDate] = bc.datetoShow1( row1["MNC_STP_DATE"].ToString());
                rowa[colGrfActTime] = bc.showTime(row1["MNC_STP_TIME"].ToString());
                rowa[colGrfActNoDeptName] = bc.bcDB.pm32DB.getDeptNameOPD(row1["MNC_SEC_NO"].ToString());
                rowa[colGrfActUserName] = row1["MNC_USR_FULL"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void FrmReceptionStatusVisit_Load(object sender, EventArgs e)
        {
            sp1.HeaderHeight = 0;
            
            autoPaid = bc.bcDB.finM02DB.getlPaid1();
            txtCurPaid.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCurPaid.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCurPaid.AutoCompleteCustomSource = autoPaid;

            autoComp = bc.bcDB.pm24DB.getlPaid1(false);
            txtInsur.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtInsur.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtInsur.AutoCompleteCustomSource = autoComp;

            AutoCompleteStringCollection autoInsur = new AutoCompleteStringCollection();
            autoInsur = bc.bcDB.pm24DB.setAutoInsur(false);
            txtPttInsur.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPttInsur.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPttInsur.AutoCompleteCustomSource = autoInsur;

            
            
            txtPttComp.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPttComp.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPttComp.AutoCompleteCustomSource = autoComp;

            AutoCompleteStringCollection autoPaid1 = new AutoCompleteStringCollection();
            autoPaid1 = bc.bcDB.finM02DB.getlPaid();
            txtVsPaid.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtVsPaid.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtVsPaid.AutoCompleteCustomSource = autoPaid1;

            setGrfStatus(PRENO, VSDATE);
        }
    }
}
