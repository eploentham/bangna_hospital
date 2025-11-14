using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Ribbon;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class UCDrugEdit : UserControl
    {
        BangnaControl BC;
        String DOCCODE = "", CFRNO = "", CFRYEAR = "", DRUGCODE = "", USERID = "", TXT = "", STATUSFORMUS = "";
        C1ThemeController theme1;
        Patient PTT;
        Visit VS;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        RibbonLabel lfSbMessage;
        FrmPharmacy frmPharmacy;
        DataTable DTDRUG;
        public UCDrugEdit(BangnaControl bc, String drugcode, String cfryear, String cfrno, String doccode, String userid, ref RibbonLabel lfSbMessage)
        {
            InitializeComponent();
            this.BC = bc;
            this.DRUGCODE = drugcode;
            this.CFRYEAR = cfryear;
            this.CFRNO = cfrno;
            this.DOCCODE = doccode;
            this.USERID = userid;
            this.lfSbMessage = lfSbMessage;
            initConfig();
        }
        public UCDrugEdit(BangnaControl bc, ref RibbonLabel lfSbMessage, FrmPharmacy frmPharmacy)
        {
            InitializeComponent();
            this.BC = bc;
            this.lfSbMessage = lfSbMessage;
            this.frmPharmacy = frmPharmacy;
            initConfig();
        }
        private void initConfig()
        {
            theme1 = new C1ThemeController();
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            // lock fixed size      
            this.AutoSize = false;  
            this.MinimumSize = new Size(439, 221);
            this.MaximumSize = new Size(439, 221);
            // optional: prevent resizing by parent layout events
            this.Resize += (s, e) => { if (this.Width != 439 || this.Height != 221) this.Size = new Size(439, 221); };
            btnSave.Click += BtnSave_Click;
            btnPrint.Click += BtnPrint_Click;
            chkLangEng.Click += ChkLangEng_Click;
            chkLangThai.Click += ChkLangThai_Click;
            setControl();
        }
        private void ChkLangThai_Click(object sender, EventArgs e)
        {
            setControl(DTDRUG,chkLangThai.Name);
        }
        private void ChkLangEng_Click(object sender, EventArgs e)
        {
            setControl(DTDRUG, chkLangEng.Name);
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtstcdrug = BC.bcDB.pharT06DB.selectByCFRNoDrug3(CFRYEAR, CFRNO, DOCCODE, DRUGCODE);
            frmPharmacy.setReportStricker(ref dtstcdrug, false, "thai", "stricker");     //ทำแบบนี้เพราะ ต้องการให้พิมพ์ต่อกัน  ถ้าพิมพ์ธรรมดา มันมีโอกาส สลับกัน
            System.Threading.Thread.Sleep(1000);                            //ทำแบบนี้เพราะ ต้องการให้พิมพ์ต่อกัน  ถ้าพิมพ์ธรรมดา มันมีโอกาส สลับกัน
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            FrmPasswordConfirm frm = new FrmPasswordConfirm(BC);
            frm.ShowDialog(this);
            frm.Dispose();
            if (BC.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String using1 = txtUsing.Text.Trim();
            String freq = txtFrequency.Text.Trim();
            String indica = txtIndication.Text.Trim();
            String precau = txtPrecautions.Text.Trim();
            String interac = txtInteraction.Text.Trim();
            String properties = txtProperties.Text.Trim();
            String result = BC.bcDB.pharT06DB.updateUsingFreqIndicau(using1, freq, indica, precau, interac, properties, DOCCODE, CFRYEAR, CFRNO, DRUGCODE, BC.USERCONFIRMID);
            if (result.Equals("1"))
            {
                lfSbMessage.Text = "บันทึกข้อมูลสำเร็จ";
                DataTable dt = new DataTable();
                dt = BC.bcDB.pharT06DB.selectByCFRNoDrug3(CFRYEAR, CFRNO, DOCCODE, "");
                frmPharmacy.setGrfDispDrugOPD(dt);
            }
            else
            {
                lfSbMessage.Text = "บันทึกข้อมูลไม่สำเร็จ";
            }
        }
        private void setControl()
        {
            setControl(CFRYEAR, CFRNO, DOCCODE, DRUGCODE);
        }
        public void setControl(String drugcode, String cfryear, String cfrno, String doccode)
        {
            DTDRUG = BC.bcDB.pharT06DB.selectByCFRNoDrug3(cfryear, cfrno, doccode, drugcode);
            if (DTDRUG.Rows.Count > 0)
            {
                setControl(DTDRUG,"");
            }
        }
        public void setControl(DataTable dtdreug, String flagclick)
        {
            clearControl();
            DTDRUG = dtdreug;
            //DTDRUG.AcceptChanges();
            setControl1(flagclick);
        }
        public void setControl1(String flagclick)
        {
            clearControl();
            if (DTDRUG.Rows.Count > 0)
            {
                //DTDRUG = dtdreug;
                if(flagclick.Length<=0)
                {
                    chkLangEng.Checked = DTDRUG.Rows[0]["nation"].ToString().Equals("01") ? false : true;
                    chkLangThai.Checked = DTDRUG.Rows[0]["nation"].ToString().Equals("01") ? true : false;
                }
                CFRYEAR = DTDRUG.Rows[0]["MNC_CFR_YR"].ToString();
                CFRNO = DTDRUG.Rows[0]["MNC_CFR_NO"].ToString();
                DOCCODE = DTDRUG.Rows[0]["MNC_DOC_CD"].ToString();
                DRUGCODE = DTDRUG.Rows[0]["MNC_PH_CD"].ToString();
                String using1 = DTDRUG.Rows[0]["using1"].ToString();           //จริงๆคือ Using วิธีใช้ pm04.mnc_ph_dir_dsc
                String freq1 = DTDRUG.Rows[0]["frequency"].ToString();         //ความถี่  pm21.MNC_PH_FRE_DSC
                String indica1 = DTDRUG.Rows[0]["indication"].ToString();      //Indication ข้อบ่งชี้  pm22.MNC_PH_TIM_DSC
                String interaction1 = DTDRUG.Rows[0]["interaction"].ToString();
                String precau1 = DTDRUG.Rows[0]["precautions"].ToString();     //Precautions จริงๆคือ คำเตือน pm11.MNC_PH_CAU_dsc
                String properties1 = DTDRUG.Rows[0]["properties"].ToString();  //สรรพคุณ
                if (chkLangThai.Checked)
                {
                    using1 = using1.Trim().Length > 0 ? using1 : DTDRUG.Rows[0]["drug_using"] != DBNull.Value ? DTDRUG.Rows[0]["drug_using"].ToString() : "";   //drug_using
                    indica1 = indica1.Trim().Length > 0 ? indica1 : DTDRUG.Rows[0]["drug_ind"] != DBNull.Value ? DTDRUG.Rows[0]["drug_ind"].ToString() : "";    //drug_ind
                    freq1 = freq1.Trim().Length > 0 ? freq1 : DTDRUG.Rows[0]["drug_freq"] != DBNull.Value ? DTDRUG.Rows[0]["drug_freq"].ToString() : "";        //drug_frequency
                    properties1 = properties1.Trim().Length > 0 ? properties1 : DTDRUG.Rows[0]["drug_properties"] != DBNull.Value ? DTDRUG.Rows[0]["drug_properties"].ToString() : "";   //drug_frequency
                    precau1 = precau1.Trim().Length > 0 ? precau1 : DTDRUG.Rows[0]["drug_cau"] != DBNull.Value ? DTDRUG.Rows[0]["drug_cau"].ToString() : "";   //drug_frequency
                }
                else
                {
                    using1 = using1.Trim().Length > 0 ? using1 : DTDRUG.Rows[0]["drug_using_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_using_e"].ToString() : "";   //drug_using
                    indica1 = indica1.Trim().Length > 0 ? indica1 : DTDRUG.Rows[0]["drug_ind_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_ind_e"].ToString() : "";    //drug_ind
                    freq1 = freq1.Trim().Length > 0 ? freq1 : DTDRUG.Rows[0]["drug_freq_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_freq_e"].ToString() : "";        //drug_frequency
                    properties1 = properties1.Trim().Length > 0 ? properties1 : DTDRUG.Rows[0]["drug_properties_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_properties_e"].ToString() : "";   //drug_frequency
                    precau1 = precau1.Trim().Length > 0 ? precau1 : DTDRUG.Rows[0]["drug_cau_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_cau_e"].ToString() : "";    //drug_frequency
                }

                using1 = (using1.Length > 0 && freq1.Length > 0) ? using1.Replace(freq1, "").Trim() : "";
                if (using1.LastIndexOf("/") > 0) { using1 = using1.Substring(0, using1.LastIndexOf("/")); }
                freq1 = freq1.Length > 0 ? freq1.Replace("/", "").Trim() : "";
                indica1 = indica1.Length > 0 ? indica1.Replace("/", "").Trim() : "";
                precau1 = precau1.Length > 0 ? precau1.Replace("/", "").Trim() : "";
                properties1 = properties1.Length > 0 ? properties1.Replace("/", "").Trim() : "";
                txtItemCode.Value = DTDRUG.Rows[0]["MNC_PH_CD"].ToString();
                txtUsing.Value = using1;
                txtGeneric.Value = DTDRUG.Rows[0]["generic"].ToString();
                txtFrequency.Value = freq1;
                txtIndication.Value = indica1;
                txtPrecautions.Value = precau1;
                txtProperties.Value = properties1;
                txtInteraction.Value = interaction1;

            }
        }
        public void clearControl()
        {
            txtItemCode.Value = "";
            txtUsing.Value = "";
            txtGeneric.Value = "";
            txtFrequency.Value = "";
            txtIndication.Value = "";
            txtInteraction.Value = "";
            txtPrecautions.Value = "";
            txtProperties.Value = "";
        }
    }
}
