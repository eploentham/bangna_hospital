using bangna_hospital.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmOPD2CheckUPMain : Form
    {
        BangnaControl bc;
        public FrmOPD2CheckUPMain()
        {
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            bc = new BangnaControl();
            btnPrintOPD21TrueStar.Click += BtnPrintOPD21TrueStar_Click;
            btnPrintLicenseDriver.Click += BtnPrintLicenseDriver_Click;
        }

        private void BtnPrintLicenseDriver_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmOPDLicenseDriver frm = new FrmOPDLicenseDriver(bc);
            frm.ShowDialog();
        }

        private void BtnPrintOPD21TrueStar_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmOPD21CheckUP frm = new FrmOPD21CheckUP(bc, "truestar");
            frm.ShowDialog();
        }

        private void btnPrintOPD2_Click(object sender, EventArgs e)
        {
            FrmOPD2CheckUP frm = new FrmOPD2CheckUP(bc,"");
            frm.ShowDialog();
        }

        private void btnPrintOPD21_Click(object sender, EventArgs e)
        {
            FrmOPD21CheckUP frm = new FrmOPD21CheckUP(bc,"");
            frm.ShowDialog();
        }

        private void FrmOPD2CheckUPMain_Load(object sender, EventArgs e)
        {

        }
    }
}
