using bangna_hospital.control;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmPrintCri : Form
    {
        BangnaControl bc;

        C1FlexGrid grfHn;
        Font fEdit, fEditB;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmPrintCri(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {

        }
        private void FrmPrintCri_Load(object sender, EventArgs e)
        {

        }
    }
}
