using C1.Win.C1Command;
using bangna_hospital.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class MainMenu : Form
    {
        BangnaControl bc;
        Login login;

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        Image CloseImage;
        
        Boolean flagExit = false;

        public MainMenu(BangnaControl bc, FrmSplash splash)
        {
            this.bc = bc;
            InitializeComponent();
            login = new Login(bc, splash);
            login.ShowDialog(this);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                bc.bcDB = new objdb.BangnaHospitalDB(bc.conn);
                bc.getInit();
            }).Start();
            splash.Dispose();
            if (login.LogonSuccessful.Equals("1"))
            {
                initConfig();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */
                    
                }).Start();
            }
            else
            {
                Application.Exit();
            }
        }
        private void initConfig()
        {
            theme1.Theme = bc.iniC.themeApplication;

            this.FormClosing += MainMenu4_FormClosing;
            menuExit.Click += MenuExit_Click;
            menuScan.Click += MenuScan_Click;
            menuDocGroup.Click += MenuDocGroup_Click;
            menuScanView.Click += MenuScanView_Click;
            menuDocGroupSub.Click += MenuDocGroupSub_Click;
            menuPrint.Click += MenuPrint_Click;
            menuNurseScanView.Click += MenuNurseScanView_Click;
            menuScanChk.Click += MenuScanChk_Click1;
        }

        private void MenuScanChk_Click1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmScanCheck frm = new FrmScanCheck(bc, this);
            frm.FormBorderStyle = FormBorderStyle.None;
            AddNewTab(frm, menuScanChk.Text + " ");
        }

        private void MenuNurseScanView_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmNurseScanView frm = new FrmNurseScanView(bc);
            frm.FormBorderStyle = FormBorderStyle.None;
            AddNewTab(frm, menuScanView.Text + " ");
        }

        private void MenuPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void MenuDocGroupSub_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDocGroupSub frm = new FrmDocGroupSub(bc);
            frm.FormBorderStyle = FormBorderStyle.None;
            AddNewTab(frm, menuDocGroupSub.Text + " ");
        }

        private void MenuScanView_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmScanView1 frm = new FrmScanView1(bc);
            frm.FormBorderStyle = FormBorderStyle.None;
            AddNewTab(frm, menuScanView.Text + " ");
        }

        private void MenuDocGroup_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDocGroupScan frm = new FrmDocGroupScan(bc);
            frm.FormBorderStyle = FormBorderStyle.None;
            AddNewTab(frm, menuDocGroup.Text + " ");
        }

        private void MenuScan_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmScanAdd frm = new FrmScanAdd(bc, this);
            frm.FormBorderStyle = FormBorderStyle.None;
            AddNewTab(frm, menuScan.Text + " ");
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            appExit();
        }
        private void MainMenu4_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!flagExit)
                {
                    if (MessageBox.Show("ต้องการออกจากโปรแกรม3", "ออกจากโปรแกรม", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        //Close();
                        //return true;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                //appExit();
            }
            else
            {
                e.Cancel = true;
            }
        }
        private Boolean appExit()
        {
            flagExit = true;
            if (MessageBox.Show("ต้องการออกจากโปรแกรม2", "ออกจากโปรแกรม menu", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        public C1DockingTabPage AddNewTab(Form frm, String label)
        {
            //foreach (Control y in tC1.Controls)
            //{
            //    if (y is C1DockingTabPage)
            //    {
            //        if (y.Text.Equals("Import JOB"))
            //        {
            //            if (label.Equals("Import JOB"))
            //            {
            //                tC1.SelectedTab = (C1DockingTabPage)y;
            //                return null;
            //            }
            //        }
            //    }
            //}
            C1DockingTabPage tab = new C1DockingTabPage();
            tab.SuspendLayout();
            frm.TopLevel = false;
            tab.Width = tCC1.Width - 10;
            tab.Height = tCC1.Height - 35;

            frm.Parent = tab;
            frm.Dock = DockStyle.Fill;
            frm.Width = tab.Width;
            frm.Height = tab.Height;
            tab.Text = label;
            //foreach (Control x in frm.Controls)
            //{
            //    if (x is DataGridView)
            //    {
            //        //x.Dock = DockStyle.Fill;
            //    }
            //}
            //tab.BackColor = System.Drawing.ColorTranslator.FromHtml("#1E1E1E");
            frm.Visible = true;

            tC1.TabPages.Add(tab);

            //frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);
            frm.Location = new Point(0, 0);
            tab.ResumeLayout();
            tab.Refresh();
            tab.Text = label;
            if (bc.iniC.statusAppDonor.Equals("1"))
            {
                theme1.SetTheme(tC1, bc.iniC.themeDonor);
            }
            else
            {
                theme1.SetTheme(tC1, "Office2007Blue");
            }
            tC1.SelectedTab = tab;
            //theme1.SetTheme(tC1, "Office2010Blue");
            //theme1.SetTheme(tC1, "Office2010Green");
            return tab;
        }
private void MenuScanChk_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                appExit();
                //if (MessageBox.Show("ต้องการออกจากโปรแกรม1", "ออกจากโปรแกรม", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                //{
                //    Close();
                //    return true;
                //}
            }
            else
            {
                //keyData
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.Text = "สวัสดี คุณ "+bc.user.staff_fname_t +" "+bc.user.staff_lname_t+" Update 2020-01-30 " + "hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP;
            //sb1.Text = "Last Update 2019-12-24 " + "hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP;
            //theme1.SetTheme(this, ic.theme);
            theme1.SetTheme(this, bc.theme);
            theme1.SetTheme(menuStrip1, bc.theme);
            theme1.SetTheme(tC1, bc.theme);
            //menuMedicalRecord.Visible = false;
            //menuNurse.Visible = false;
            //menuLab.Visible = false;
            //menuPharmacy.Visible = false;
            //menuInit.Visible = false;
            
            //else
            //{

            //    //foreach (Control c in tC1.Controls)
            //    //{
            //    //    if (c is C1DockingTab) continue;
            //    //    if (c is C1CommandDock) continue;
            //    //    if (c is C1DockingTabPage) continue;
            //    //    theme1.SetTheme(c, "Office2007Blue");
            //    //}
            //    menuRecept.Visible = true;
            //    menuNurse.Visible = true;
            //    menuLab.Visible = true;
            //    menuInit.Visible = true;
            //}

        }
    }
}
