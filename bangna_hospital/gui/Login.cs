using bangna_hospital.Properties;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using bangna_hospital.Properties;
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
    public partial class Login : Form
    {
        public String LogonSuccessful = "";
        Staff stf;

        Font fEdit, fEditB;
        Color bg, fc;
        Font ff, ffB;

        BangnaControl bc;
        FrmSplash splash;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        StaffDB stfDB;
        Boolean chkOK=false;
        public Login(BangnaControl bc, FrmSplash splash)
        {
            InitializeComponent();
            this.bc = bc;
            this.splash = splash;
            initConfig();
            //new LogWriter("e", "Login InitializeComponent Start Form");
        }
        public Login(ref BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            //this.splash = splash;
            initConfig();
            //new LogWriter("e", "Login InitializeComponent Start Form");
        }
        private void initConfig()
        {
            theme1.Theme = bc.iniC.themeApplication;
            txtUserName.KeyUp += TxtUserName_KeyUp;
            txtUserName.LostFocus += TxtUserName_LostFocus;
            txtUserName.Enter += TxtUserName_Enter;
            txtPassword.KeyUp += TxtPassword_KeyUp;
            txtPassword.Enter += TxtUserName_Enter;
            btnOk.Click += BtnOk_Click;
            bg = txtUserName.BackColor;
            fc = txtUserName.ForeColor;

            //theme1.SetTheme(panel1, "Office2013Red");
            theme1.SetTheme(btnOk, bc.iniC.themeApplication);
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            stfDB = new StaffDB(bc.conn);
            foreach (Control con in panel1.Controls)
            {
                theme1.SetTheme(con, bc.iniC.themeApplication);
            }
            theme1.SetTheme(txtUserName, bc.iniC.themeDonor);
            theme1.SetTheme(txtPassword, bc.iniC.themeDonor);
            //stt.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.Gold;
            //stt.
        }

        //private void TxtPassword_Enter(object sender, EventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    C1TextBox a = (C1TextBox)sender;
        //    a.BackColor = ic.cTxtFocus;
        //}

        private void TxtUserName_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1TextBox a = (C1TextBox)sender;
            a.BackColor = bc.cTxtFocus;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //new LogWriter("e", "Login BtnOk_Click " );
            stf = stfDB.selectByLogin(txtUserName.Text, txtPassword.Text);
            //stf.fullname = "111";
            //MessageBox.Show("BtnOk_Click 11111", "");
            if (stf.fullname.Length > 0)
            {
                bc.userId = stf.staff_id;
                bc.user = stf;
                bc.conn.user = stf;
                LogonSuccessful = "1";
                this.Dispose();
            }
            else
            {
                //stt.Show("ไม่พบ username หรือ password", txtPassword);
                sep.SetError(txtPassword, "333");
                LogonSuccessful = "0";
            }
        }
        private void TxtUserName_LostFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1TextBox a = (C1TextBox)sender;
            a.BackColor = bg;
            //a.ForeColor = fc;
            //a.Font = new Font(ff, FontStyle.Regular);
            if(!chkOK) chkLogin();
        }
        private void TxtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.Focus();
            }
        }
        private void TxtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                chkLogin();
            }
        }
        private void chkLogin()
        {
            Staff stf1 = new Staff();
            if (txtUserName.Text.Length <= 0) return;
            stf1 = stfDB.selectByUsername(txtUserName.Text);
            if (stf1.fullname.Length > 0)
            {
                btnOk.Image = Resources.Accept_Male_User24;
                stt.Show("<p><b>สวัสดี</b></p>คุณ " + stf1.fullname + "<br> กรุณา ป้อนรหัสผ่าน", txtUserName);
                chkOK = true;
                //stt.SetToolTip(txtUserName, "<p><b>สวัสดี</b></p>คุณ " + stf1.staff_fname_t + " " + stf1.staff_lname_t + "<br> กรุณา ป้อนรหัสผ่าน");
                sep.Clear();
                txtPassword.Focus();
            }
            else
            {
                btnOk.Image = Resources.Male_User_Warning24;
                //stt.Show("ไม่พบ username ", txtUserName);
                //stt.SetToolTip(txtUserName, "ไม่พบ username ");
                sep.SetError(txtUserName, "333");
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {
            //if (ic.iniC.statusAppDonor.Equals("1"))
            //{
            //theme1.SetTheme(this, bc.theme);
            //theme1.SetTheme(panel1, bc.theme);
            //foreach (Control c in panel1.Controls)
            //{
            //    theme1.SetTheme(c,bc.theme);
            //}
            bg = txtUserName.BackColor;
            fc = txtUserName.ForeColor;
            this.Text = "Last Update 2020-03-06 Format Date " + System.DateTime.Now.ToString("dd-MM-yyyy") + "hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP;
            //}
            //else
            //{
            //    theme1.SetTheme(this, "Office2007Blue");
            //    theme1.SetTheme(panel1, "Office2007Blue");
            //    foreach (Control c in panel1.Controls)
            //    {
            //        theme1.SetTheme(c, "Office2007Blue");
            //    }
            //}
        }
    }
}
