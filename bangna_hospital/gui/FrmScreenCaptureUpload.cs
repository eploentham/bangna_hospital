using bangna_hospital.control;
using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScreenCaptureUpload : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        String filename = "", hn="", pttname="";
        public FrmScreenCaptureUpload(BangnaControl bc, String filename, String hn, String pttname)
        {
            InitializeComponent();
            this.bc = bc;
            this.filename = filename;
            this.hn = hn;
            this.pttname = pttname;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            picWait.Hide();

            //bc.bcDB.dgsDB.setCboBsp(cboDgs, "");
            bc.bcDB.dgssDB.setCboBspDeptUS(cboDgs, "");
            txtHn.Value = hn;
            lbName.Text = pttname;

            btnUpload.Click += BtnUpload_Click;
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //name = filename.Replace("*", "");
            String dgssid = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
            if (dgssid.Length <= 0)
            {
                MessageBox.Show("ไม่ได้เลือก กลุ่มเอกสาร", "");
                return;
            }
            picWait.Show();
            string ext = Path.GetExtension(filename);
            String dgssname = "",  vn = "", an = "";
            //dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            DocScan dsc = new DocScan();
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
            dsc.hn = txtHn.Text;
            dsc.vn = "";
            dsc.an = "";
            dsc.visit_date = "";
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = dgssid;
            dsc.pre_no = "";
            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "O";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            String re = bc.bcDB.dscDB.insertDocScan(dsc, bc.userId);

            long chk = 0;
            if(long.TryParse(re, out chk))
            {
                dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + re + ext;
                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);

                FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                //MessageBox.Show("111", "");
                //ftp.createDirectory(txtHn.Text);
                ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-"));
                //MessageBox.Show("222", "");
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //MessageBox.Show("333", "");

                ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename);
                File.Delete(filename);
                this.Dispose();
            }
        }

        private void FrmScreenCaptureUpload_Load(object sender, EventArgs e)
        {
            theme1.SetTheme(this, bc.iniC.themeApplication);
            theme1.SetTheme(label1, bc.iniC.themeApplication);
            theme1.SetTheme(txtHn, bc.iniC.themeApplication);
            theme1.SetTheme(label2, bc.iniC.themeApplication);
            theme1.SetTheme(lbName, bc.iniC.themeApplication);
            theme1.SetTheme(cboDgs, bc.iniC.themeApplication);
            sB1.Text = "hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP;
        }
    }
}
