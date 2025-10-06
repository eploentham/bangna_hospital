using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Ribbon;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
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

namespace bangna_hospital.gui
{
    public partial class UCVitalsign : BaseUserControl
    {
        BangnaControl BC;
        String PRENO = "", VSDATE = "", HN = "", DTRCODE = "", StatusFormUs = "", TXT = "", STATUSFORMUS = "";
        C1FlexGrid grfApmOrder;
        C1ThemeController theme1;
        Patient PTT;
        Visit VS;
        Boolean isLoaded = false;
        RibbonLabel lfSbMessage;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        public UCVitalsign(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt, Visit vs, String statusformus, ref RibbonLabel lfSbMessage)
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
            //setControl(dtrcode, hn, vsdate, preno, ptt, statusformus);
        }
        private void initConfig()
        {
            this.SuspendLayout();
            isLoaded = true;
            stt = new C1SuperTooltip();
            initFont(BC);
            if(VS==null)            {                VS = BC.bcDB.vsDB.selectbyPreno(HN, VSDATE,PRENO);            }
            setControlTabOperVital(VS);
            this.Load += UCVitalsign_Load;
            txtOperBmi.MouseHover += TxtOperBmi_MouseHover;
            txtOperTemp.MouseHover += TxtOperTemp_MouseHover;
            isLoaded = false;
        }

        private void TxtOperTemp_MouseHover(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //stt.SetToolTip(txtOperTemp, "<p><b>สีฟ้า - ต่ำกว่า 35 องศาเซลเซียส</b></p><p><b>สีเขียว - 35.0 - 37.5 องศาเซลเซียส</b></p><p><b>สีเหลือง - 37.6 - 38.0 องศาเซลเซียส</b></p><p><b>สีส้ม - 38.1 - 39.0 องศาเซลเซียส</b></p><p><b>สีแดง - มากกว่า 39.0 องศาเซลเซียส</b></p>");
            stt.SetToolTip(txtOperTemp,
                "<p><span style='color:#3B82F6;'>สีฟ้า</span> - ต่ำกว่า 35 องศาเซลเซียส</p>" +
                "<p><span style='color:#10B981;'>สีขาว</span> - 35.0 - 37.5 องศาเซลเซียส</p>" +
                "<p><span style='color:#F59E0B;'>สีเหลือง</span> - 37.6 - 38.0 องศาเซลเซียส</p>" +
                "<p><span style='color:#F97316;'>สีส้ม</span> - 38.1 - 39.0 องศาเซลเซียส</p>" +
                "<p><span style='color:#EF4444;'>สีแดง</span> - มากกว่า 39.0 องศาเซลเซียส</p>");
        }
        private void TxtOperBmi_MouseHover(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            stt.SetToolTip(txtOperBmi, "<p><span style='color:#3B82F6;'>สีฟ้า</span> - น้ำหนักน้อย / ผอม(BMI < 18.5)</p><p><span style='color:#10B981;'>สีขาว</span> - น้ำหนักปกติ / ดี(BMI 18.5 - 22.9)</p><p><b><span style='color:#F59E0B;'>สีเหลือง</span> - น้ำหนักเกิน(BMI 23.0 - 24.9)</b></p><p><b><span style='color:#F97316;'>สีส้ม</span> - อ้วนระดับ 1(BMI 25.0 - 29.9)</b></p><p><b><span style='color:#EF4444;'>สีแดง</span> - อ้วนระดับ 2 ขึ้นไป(BMI ≥ 30.0)</b></p>");
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
        private void setControlTabOperVital(Visit vs)
        {
            //txtOperHN.Value = vs.HN;
            //lbOperPttNameT.Text = vs.PatientName;
            Color b = new Color();
            Color f = new Color();
            float temp = float.Parse(vs.temp);
            b = txtOperTemp.BackColor;
            if(temp < 35) { b = ColorTranslator.FromHtml("#3B82F6"); txtOperTemp.BackColor = b; }
            else if ((temp >= 35) && (temp <= 37.5)) {/* b = ColorTranslator.FromHtml("#10B981");*/ txtOperTemp.BackColor = b; }
            else if ((temp > 37.5) && (temp <= 38)) { b = ColorTranslator.FromHtml("#F59E0B"); txtOperTemp.BackColor = b; }
            else if ((temp > 38) && (temp <= 39)) { b = ColorTranslator.FromHtml("#F97316"); txtOperTemp.BackColor = b; }
            else if (temp > 39) { b = ColorTranslator.FromHtml("#EF4444"); txtOperTemp.BackColor = b; }
            txtOperTemp.Value = vs.temp;
            txtOperHrate.Value = vs.ratios;
            txtOperRrate.Value = vs.breath;
            //txtOperAbo.Value = vs.temp;       txtOperBp1L
            //BP    เกณฑ์ปกติ (< 120/80) ใช้เหมือนกันทุกคน ไม่ว่าจะหนักหรือเบา
            //อ้วนระดับ 2 (Obese Class II)
            String[] bp = vs.bp1l.Split('/');
            b = txtOperBp1L.BackColor;
            if (bp.Length == 2)
            {
                try
                {
                    if ((int.Parse(bp[0])>120) && (int.Parse(bp[1]) > 80)) { b = ColorTranslator.FromHtml("#FCD34D"); txtOperBp1L.BackColor = b; }
                }catch(Exception ex) { }
            }
            //O2
            //Sat  95-100 %                         #10B981
            //ต่ำกว่า 95 % ให้ส่งตรวจ ABG                   #F59E0B
            // 90-94 %  Mild Hypoxemia              #F97316
            //  75-89 %  Moderate Hypoxemia         #EF4444
            //  < 75 %  Severe Hypoxemia
            b = txtOperO2.BackColor;
            if (vs.o2_sat != "")
            {
                try
                {
                    if (int.Parse(vs.o2_sat) >= 95) { b = ColorTranslator.FromHtml("#10B981"); txtOperO2.BackColor = b; }
                    else if ((int.Parse(vs.o2_sat) < 95) && (int.Parse(vs.o2_sat) >= 90)) { b = ColorTranslator.FromHtml("#F59E0B"); txtOperO2.BackColor = b; }
                    else if ((int.Parse(vs.o2_sat) < 90) && (int.Parse(vs.o2_sat) >= 75)) { b = ColorTranslator.FromHtml("#F97316"); txtOperO2.BackColor = b; }
                    else if (int.Parse(vs.o2_sat) < 75) { b = ColorTranslator.FromHtml("#EF4444"); txtOperO2.BackColor = b; }
                }
                catch (Exception ex) { }
            }
            //Pain Score(คะแนนความเจ็บปวด)
            //0 = No pain (ไม่มีความเจ็บปวด)               #10B981
            //1-3 = Mild pain (ความเจ็บปวดเล็กน้อย)        #F59E0B
            //4-6 = Moderate pain (ความเจ็บปวดปานกลาง)   #F97316
            //7-10 = Severe pain (ความเจ็บปวดรุนแรง)      #EF4444
            b = txtOperPain.BackColor;
            if (vs.pain_score != "")
            {
                try
                {
                    if (int.Parse(vs.pain_score) == 0) { b = ColorTranslator.FromHtml("#10B981"); txtOperPain.BackColor = b; }
                    else if ((int.Parse(vs.pain_score) >= 1) && (int.Parse(vs.pain_score) <= 3)) { b = ColorTranslator.FromHtml("#F59E0B"); txtOperPain.BackColor = b; }
                    else if ((int.Parse(vs.pain_score) >= 4) && (int.Parse(vs.pain_score) <= 6)) { b = ColorTranslator.FromHtml("#F97316"); txtOperPain.BackColor = b; }
                    else if ((int.Parse(vs.pain_score) >= 7) && (int.Parse(vs.pain_score) <= 10)) { b = ColorTranslator.FromHtml("#EF4444"); txtOperPain.BackColor = b; }
                }
                catch (Exception ex) { }
            }
            txtOperRh.Value = "";
            txtOperBp1L.Value = vs.bp1l;
            txtOperBp1R.Value = vs.bp1r;
            txtOperBp2L.Value = vs.bp2l;
            txtOperBp2R.Value = vs.bp2r;
            txtOperAbc.Value = vs.abc;
            txtOperWt.Value = vs.weight;
            txtOperHt.Value = vs.high;
            txtOperHc.Value = vs.hc;
            txtOperCc.Value = vs.cc;
            txtOperCcin.Value = vs.ccin;
            txtOperCcex.Value = vs.ccex;
            txtOperO2.Value = vs.o2_sat;
            txtOperPain.Text = vs.pain_score;
            lbSymptoms.Text = vs.symptom.Replace("\r\n", ",");
            lbSymptoms.Text = lbSymptoms.Text.Replace(",,,", "");
            lbSymptoms.Text = lbSymptoms.Text.Replace(",,", "");
            txtOperAbo.Value = "";
            
            txtOperBmi.Value = BC.calBMI(vs.weight, vs.high,ref b,ref f);
            txtOperBmi.BackColor = b;
            txtOperBmi.ForeColor = f;
            //สีฟ้า / น้ำเงินอ่อน - น้ำหนักน้อย / ผอม(BMI < 18.5)   #3B82F6
            //สีเขียว - น้ำหนักปกติ / ดี(BMI 18.5 - 22.9)         #10B981
            //สีเหลือง - น้ำหนักเกิน(BMI 23.0 - 24.9)            #F59E0B
            //สีส้ม - อ้วนระดับ 1(BMI 25.0 - 29.9)              #F97316
            //สีแดง - อ้วนระดับ 2 ขึ้นไป(BMI ≥ 30.0)             #EF4444     #FCA5A5 
        }
        private void clearControlTabOperVital()
        {
            //txtOperHN.Value = "";
            //lbOperPttNameT.Text = "";
            txtOperTemp.Value = "";
            txtOperHrate.Value = "";
            txtOperRrate.Value = "";
            txtOperAbo.Value = "";
            txtOperRh.Value = "";
            txtOperBp1L.Value = "";
            txtOperBp1R.Value = "";
            txtOperBp2L.Value = "";
            txtOperBp2R.Value = "";
            txtOperAbc.Value = "";
            txtOperWt.Value = "";
            txtOperHt.Value = "";
            txtOperHc.Value = "";
            txtOperCc.Value = "";
            txtOperCcin.Value = "";
            txtOperCcex.Value = "";
            //txtOperDtr.Value = "";
            //lbOperDtrName.Text = "";
            lbSymptoms.Text = "";
            txtOperBmi.Value = "";
        }
        private void UCVitalsign_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.ResumeLayout();
        }
    }
}
