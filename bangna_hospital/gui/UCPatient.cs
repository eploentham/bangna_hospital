using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Ribbon;
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

namespace bangna_hospital.gui
{
    public partial class UCPatient : BaseUserControl
    {
        BangnaControl BC;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        String PRENO = "", HN="", VSDATE = "", DTRCODE = "", StatusFormUs = "", TXT = "", STATUSFORMUS = "";
        C1FlexGrid grfApmOrder;
        C1ThemeController theme1;
        Patient PTT;
        Visit VS;
        Boolean isLoaded = false;
        RibbonLabel lfSbMessage;
        public UCPatient(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt, Visit vs, String statusformus, ref RibbonLabel lfSbMessage)
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
            InitializeComponent();
            initConfig();
            //setControl(dtrcode, hn, vsdate, preno, ptt, statusformus);
        }
        private void initConfig()
        {
            isLoaded = true;
            initFont(BC);
            this.Load += UCPatient_Load;
            clearControl();
            isLoaded = false;
        }
        private void clearControl()
        {
            lbHN.Text = "";
            lbAttachNote.Text = "";
            lbPttFinNote.Text = "";
            lboperAge.Text = "";
            m_picPhoto.Image = null;
        }
        public void setControl(String dtrcode, String hn, String vsdate, String preno, Patient ptt, String statusformus)
        {
            DTRCODE = dtrcode;
            HN = hn;
            VSDATE = vsdate;
            PRENO = preno;
            PTT = ptt;
            StatusFormUs = statusformus;
        }
        public void setControlShow(String dtrcode, String hn, String vsdate, String preno, Patient ptt, String statusformus)
        {
            DTRCODE = dtrcode;
            HN = hn;
            VSDATE = vsdate;
            PRENO = preno;
            PTT = ptt;
            StatusFormUs = statusformus;
            setControl();
        }
        public void setControl()
        {
            this.SuspendLayout();
            lbHN.Text = PTT.Name+" ["+ HN+"]";
            lbAttachNote.Text = "Attach Note:"+PTT.MNC_ATT_NOTE;
            Size textSize = TextRenderer.MeasureText(lbAttachNote.Text, lbAttachNote.Font);
            lbPttFinNote.Top =textSize.Height + lbAttachNote.Top + 5;
            lbPttFinNote.Text = "Finance Note:"+PTT.MNC_FIN_NOTE;
            textSize = TextRenderer.MeasureText(lbPttFinNote.Text, lbPttFinNote.Font);
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            label1.Left = lbPttFinNote.Left;
            label2.Left = lbPttFinNote.Left;
            label3.Left = lbPttFinNote.Left;
            label1.Top = textSize.Height + lbPttFinNote.Top + 5;
            label2.Top = (TextRenderer.MeasureText(label1.Text, label1.Font)).Height + label1.Top + 5;
            label3.Top = (TextRenderer.MeasureText(label2.Text, label2.Font)).Height + label2.Top + 5;
            lboperAge.Text = PTT.AgeStringShort();
            m_picPhoto.Image = null;
            label1.Text = "ประวัติ  สูบบุหรี่ ...";
            label2.Text = "ประวัติ  ดื่มเหล้า ...";
            label3.Text = "ประวัติ  ใช้สารเสพติด ...";
            this.ResumeLayout();
            //this.Invalidate();
            this.Update();
        }
        private void UCPatient_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControl();
        }
    }
}
