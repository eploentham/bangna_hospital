using bangna_hospital.control;
using bangna_hospital.object1;
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
    public partial class FrmAttachNote : Form
    {
        BangnaControl BC;
        Patient PTT;
        public FrmAttachNote(BangnaControl bc, Patient ptt)
        {
            InitializeComponent();
            this.BC = bc;
            this.PTT = ptt;
            initConfig();
        }
        private void initConfig()
        {
            txtHN.Value = PTT.MNC_HN_NO;
            lbPttNameT.Text = PTT.Name;
            txtAttachNote.Value = PTT.MNC_ATT_NOTE;
            btnOk.Click += BtnOk_Click; ;
            setControl();
        }
        private void setControl()
        {
            txtHN.Value = PTT.MNC_HN_NO;
            lbPttNameT.Text = PTT.Name;
            txtAttachNote.Value = PTT.MNC_ATT_NOTE;
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmPasswordConfirm frm = new FrmPasswordConfirm(BC);
            frm.ShowDialog();
            frm.Dispose();
            if (BC.USERCONFIRMID.Length <= 0)
            {
                //lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            AttachNote an = new AttachNote();
            an.attach_note = txtAttachNote.Text;
            String re = BC.bcDB.attachNoteDB.insertAttachNote(an);
        }

        private void FrmAttachNote_Load(object sender, EventArgs e)
        {

        }
    }
}
