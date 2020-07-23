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
    public partial class FrmDoctorDiag : Form
    {
        BangnaControl bc;
        String title = "";
        Font fEdit, fEditB, fEditBig;
        public FrmDoctorDiag(BangnaControl bc, String title)
        {
            InitializeComponent();
            this.bc = bc;
            this.title = title;
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("Template 1", new EventHandler(ContextMenu_Template1));
            richTextBox1.ContextMenu = menuGw;
        }
        private void ContextMenu_Template1(object sender, System.EventArgs e)
        {

        }
        private void FrmDoctorDiag_Load(object sender, EventArgs e)
        {
            this.Text = "Medical Examination";
            richTextBox1.Text = "Medical Examination";
            ribbonGroup1.Text = "";
            ribbonLabel1.Text = title;
            //ribbonLabel1.Ribbon.Font = fEdit;
        }
    }
}
