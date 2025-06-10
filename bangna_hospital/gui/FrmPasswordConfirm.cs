using C1.Win.C1Input;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bangna_hospital.control;
using C1.Win.C1SuperTooltip;
using bangna_hospital.object1;
using bangna_hospital.Properties;

namespace bangna_hospital.gui
{
    public partial class FrmPasswordConfirm:Form
    {
        BangnaControl bc;

        Font fEdit, fEditB;

        Color bg, fc;
        Font ff, ffB;

        Label label1 ;
        Label label2 ;
        C1TextBox txtPassword = new C1.Win.C1Input.C1TextBox();
        C1ThemeController theme1;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        public FrmPasswordConfirm(BangnaControl bc)
        {
            this.bc = bc;
            initControl();
        }
        private void initControl()
        {
            this.Width = 250;
            this.Height = 120;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            txtPassword = new C1.Win.C1Input.C1TextBox();
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            theme1.Theme = "Office2013Red";


            //C1ThemeController.ApplicationTheme = ic.iniC.themeApplication;
            theme1.Theme = C1ThemeController.ApplicationTheme;
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();

            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            label1.Location = new System.Drawing.Point(10, 20);
            //label1.Size = new System.Drawing.Size(68, 16);
            label1.TabIndex = 0;
            label1.Text = "รหัสผ่าน :";

            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            label2.Location = new System.Drawing.Point(10, label1.Top+30);
            //label1.Size = new System.Drawing.Size(68, 16);
            label2.TabIndex = 1;
            label2.Text = "-";

            txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtPassword.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            txtPassword.Location = new System.Drawing.Point(80, 20);
            txtPassword.Name = "txtContFNameE";
            txtPassword.Size = new System.Drawing.Size(100, 20);
            txtPassword.TabIndex = 84;
            txtPassword.Tag = null;
            txtPassword.PasswordChar = '*';
            txtPassword.KeyUp += new KeyEventHandler(this.txtPassword_KeyUp);
            theme1.SetTheme(this.txtPassword, "(default)");

            this.StartPosition = FormStartPosition.CenterParent;

            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(txtPassword);
        }
        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf = new Staff();
                stf = bc.bcDB.stfDB.selectByPasswordConfirm1(txtPassword.Text.Trim());
                if ((stf.staff_id !=null)&&(!stf.staff_id.Equals("")))
                {
                    label2.Text = stf.staff_fname_t + " " + stf.staff_lname_t;
                    bc.cStf = stf;
                    bc.USERCONFIRMID = stf.username;
                    //label2.Refresh();
                    //System.Threading.Thread.Sleep(2000);
                    Close();
                    //this.Hide();
                    //this.Dispose();
                }
                else
                {
                    MessageBox.Show("Password " + txtPassword.Text, "");
                }
            }
        }
    }
}
