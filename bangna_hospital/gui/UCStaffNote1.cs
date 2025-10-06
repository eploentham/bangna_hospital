using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1Ribbon;
using C1.Win.C1SplitContainer;
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
    public partial class UCStaffNote1 : BaseUserControl
    {
        BangnaControl BC;
        String PRENO = "", HN = "", VSDATE = "", DTRCODE = "", StatusFormUs = "", TXT = "", STATUSFORMUS = "";
        Patient PTT;
        Visit VS;
        Boolean isLoaded = false;
        RibbonLabel lfSbMessage;
        C1PictureBox picLeft, picRight;
        public UCStaffNote1(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt, Visit vs, String statusformus, ref RibbonLabel lfSbMessage)
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
        }
        private void initConfig()
        {
            isLoaded = true;
            initFont(BC);
            initControl();
            this.Load += UCStaffNote_Load;

            isLoaded = false;
        }
        private void initControl()
        {
            picLeft = new C1PictureBox();
            picRight = new C1PictureBox();
            picLeft.Dock = DockStyle.Fill;
            picLeft.SizeMode = PictureBoxSizeMode.StretchImage;
            picRight.Dock = DockStyle.Fill;
            picRight.SizeMode = PictureBoxSizeMode.StretchImage;
            picLeft.Image = null;
            picRight.Image = null;
            picRight.BorderStyle = BorderStyle.Fixed3D;
            pnRight.Controls.Add(picRight);
            pnLeft.Controls.Add(picLeft);
            pnRight.BackColor = Color.FromArgb(240, 240, 240);
            //pnLeft.SizeRatio = 60;
            //spRight.BackColor = Color.Red;
            //picL.Image = (Image)imgl.Clone();
        }
        private void clearControl()
        {

        }
        public void setControl(String dtrcode, String hn, String vsdate, String preno, Patient ptt, String statusformus)
        {
            this.SuspendLayout();
            DTRCODE = dtrcode;
            HN = hn;
            VSDATE = vsdate;
            PRENO = preno;
            PTT = ptt;
            StatusFormUs = statusformus;
            setControl();
            this.ResumeLayout();
        }
        public void setControl()
        {
            this.SuspendLayout();
            String file = "", dd = "", mm = "", yy = "", err = "", preno1 = "";
            try
            {
                err = "00";
                picLeft.Image = null;
                picRight.Image = null;
                int chk = 0;
                err = "01";
                dd = VSDATE.Substring(VSDATE.Length - 2);
                mm = VSDATE.Substring(5, 2);
                yy = VSDATE.Substring(0, 4);
                err = "02";
                int.TryParse(yy, out chk);
                if (chk > 2500)
                    chk -= 543;
                file = "\\\\" + BC.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                preno1 = "000000" + PRENO;
                err = "03";
                preno1 = preno1.Substring(preno1.Length - 6);
                err = "04";
                //new LogWriter("e", "FrmScanView1 setStaffNote file  " + file + preno1+" hn "+txtHn.Text+" yy "+yy);
                picLeft.Image = Image.FromFile(file + preno1 + "S.JPG");
                picRight.Image = Image.FromFile(file + preno1 + "R.JPG");
                picRight.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = err + " setControl " + ex.Message;
                new LogWriter("e", this.Name + " setControl " + ex.Message);
                BC.bcDB.insertLogPage(BC.userId, this.Name, "setControl", ex.Message);
            }
            this.ResumeLayout();
        }
        private void UCStaffNote_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControl();
        }
    }
}
