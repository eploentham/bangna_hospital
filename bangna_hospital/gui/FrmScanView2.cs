using bangna_hospital.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital
{
    public partial class FrmScanView2 : Form
    {
        BangnaControl bc;
        String dsc_id = "", hn = "";
        public FrmScanView2(BangnaControl bc, String hn)
        {
            InitializeComponent();
            this.bc = bc;
            this.hn = hn;
        }

        private void FrmScanView2_Load(object sender, EventArgs e)
        {

        }
    }
}
