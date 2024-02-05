using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using static CSJ2K.j2k.codestream.HeaderInfo;

namespace bangna_hospital.gui
{
    public partial class PasswordForm : Form
    {
        BangnaControl bc;
        public PasswordForm(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            tbPassword.KeyUp += TbPassword_KeyUp;
        }

        private void TbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                chkLogin();
                this.Dispose();
            }
        }
        private Boolean chkLogin()
        {
            Staff stf1 = new Staff();
            if (tbPassword.Text.Length <= 0) return false;
            stf1 = bc.bcDB.stfDB.selectByPasswordConfirm1(tbPassword.Text.Trim());
            if (stf1.fullname.Length > 0)
            {
                btnOk.Image = Resources.Accept_Male_User24;
                lbName.Text = "คุณ " + stf1.fullname;
                bc.USERCONFIRMID = stf1.username;
                return true;
            }
            return false;
        }
        private void PasswordForm_Load(object sender, EventArgs e)
        {
            tbPassword.Focus();
        }
    }
}
