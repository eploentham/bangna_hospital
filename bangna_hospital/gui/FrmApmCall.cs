using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmApmCall : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        PatientT07 APM;
        C1ThemeController theme1;

        Boolean pageLoad = false;
        public FrmApmCall(BangnaControl bc, Patient ptt, PatientT07 apm)
        {
            this.bc = bc;
            this.PTT = ptt;
            this.APM = apm;
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
            theme1 = new C1ThemeController();

            btnApmCallSave.Click += BtnApmCallSave_Click;
            chkApmCall1.Click += ChkApmCall1_Click;
            chkApmCall2.Click += ChkApmCall2_Click;
            chkApmCall3.Click += ChkApmCall3_Click;
            chkApmCall4.Click += ChkApmCall4_Click;
            chkApmCall5.Click += ChkApmCall5_Click;

            setControl();
            pageLoad = false;
        }

        private void ChkApmCall5_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ChkApmCallClick(false, false, false, false, true);
        }

        private void ChkApmCall4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ChkApmCallClick(false, false, false, true, false);
        }

        private void ChkApmCall3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ChkApmCallClick(false, false, true, false, false);
        }

        private void ChkApmCall2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ChkApmCallClick(false, true, false, false, false);
        }
        private void ChkApmCall1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ChkApmCallClick(true, false, false,false, false);
        }
        private void ChkApmCallClick(Boolean chk1, Boolean chk2, Boolean chk3, Boolean chk4, Boolean chk5)
        {
            if (chk1) { chkApmCall1.Checked = true; chkApmCall2.Checked = false; chkApmCall3.Checked = false; chkApmCall4.Checked = false; chkApmCall5.Checked = false; }
            else if (chk2) { chkApmCall1.Checked = false; chkApmCall2.Checked = true; chkApmCall3.Checked = false; chkApmCall4.Checked = false; chkApmCall5.Checked = false; }
            else if (chk3) { chkApmCall1.Checked = false; chkApmCall2.Checked = false; chkApmCall3.Checked = true; chkApmCall4.Checked = false; chkApmCall5.Checked = false; }
            else if (chk4) { chkApmCall1.Checked = false; chkApmCall2.Checked = false; chkApmCall3.Checked = false; chkApmCall4.Checked = true; chkApmCall5.Checked = false; }
            else if (chk5) { chkApmCall1.Checked = false; chkApmCall2.Checked = false; chkApmCall3.Checked = false; chkApmCall4.Checked = false; chkApmCall5.Checked = true; }
        }
        private void BtnApmCallSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String chk = "";
            if (chkApmCall1.Checked) chk = "1";
            else if (chkApmCall2.Checked) chk = "2";
            else if (chkApmCall3.Checked) chk = "3";
            else if (chkApmCall4.Checked) chk = "4";
            else if (chkApmCall5.Checked) chk = "5";
            String re = bc.bcDB.pt07DB.updateRemarkCall(APM.MNC_DOC_YR, APM.MNC_DOC_NO, txtApmCallRemark.Text.Trim(), chk);
            if (int.TryParse(re, out int chk1))
            {
                lfstMessage.Text = "update OK";
                this.Dispose();
            }
            else
            {
                lfstMessage.Text = re;
            }
        }

        private void setControl()
        {
            lbPttNameT.Text = PTT.Name;
            txtHN.Value = PTT.Hn;
            txtPttMobile1.Value = PTT.MNC_CUR_TEL;
            //txtPttMobile2.Value = PTT.p;
            txtPttRemark1.Value = PTT.remark1;
            txtPttRemark2.Value = PTT.remark2;
            txtApmCallNote.Value = APM.MNC_APP_DSC;
            txtApmCallOrder.Value = APM.MNC_REM_MEMO;

            txtApmCallApmDate.Value = APM.MNC_APP_DAT +" "+APM.MNC_APP_TIM;
            txtApmCallApmDept.Value = APM.MNC_SEC_NO;
            if (APM.status_remark_call.Equals("โทรเรียบร้อย รับสาย บุคคลอื่นเป็นคนรับ"))
            {
                ChkApmCallClick(true, false, false, false, false);
            }else if (APM.status_remark_call.Equals("โทรเรียบร้อย ไม่รับสาย"))
            {
                ChkApmCallClick(false, true, false, false, false);
            }
            else if (APM.status_remark_call.Equals("โทรไม่ติด สายไม่ว่าง"))
            {
                ChkApmCallClick(false, false, true, false, false);
            }
            else if (APM.status_remark_call.Equals("โทรเรียบร้อย รับสาย แจ้งคนไข้ ครบถ้วน"))
            {
                ChkApmCallClick(false, false, false, true, false);
            }
            else if (APM.status_remark_call.Equals("ไม่สามารถโทรได้ ไม่มีเบอร์โทร"))
            {
                ChkApmCallClick(false, false, false, false, true);
            }
        }
        private void FrmApmCall_Load(object sender, EventArgs e)
        {

        }
    }
}
