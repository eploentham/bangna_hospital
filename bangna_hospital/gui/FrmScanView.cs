using bangna_hospital.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScanView : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        String hn = "", vn = "", name = "", filename = "";

        public FrmScanView(BangnaControl bc, String hn, String vn, String name, String filename)
        {
            InitializeComponent();
            this.bc = bc;
            this.hn = hn;
            this.vn = vn;
            this.name = name;
            this.filename = filename;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            theme1.Theme = bc.iniC.themeApplication;
            btnSave.Click += BtnSave_Click;
            //theme1.SetTheme(sb1, "BeigeOne");

            //sb1.Text = "aaaaaaaaaa";

            txtHn.Value = hn;
            txtVN.Value = vn;
            txtNameFeMale.Value = name;

            if (File.Exists(@filename))
            {
                Image img = Image.FromFile(filename);
                pic1.Image = img;
            }            
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void FrmScanView_Load(object sender, EventArgs e)
        {

        }
    }
}
