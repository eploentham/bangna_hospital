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
        String filename = "", hn="", pttname="", vn="", flagVn="", CERTID="";
        DocScan dsc;
        Boolean AUTOUPLOAD;
        public FrmScreenCaptureUpload(BangnaControl bc, String filename, String hn, String pttname, String vn, String flagVn)
        {
            InitializeComponent();
            this.bc = bc;
            this.filename = filename;
            this.hn = hn;
            this.pttname = pttname;
            this.vn = vn;
            this.flagVn = flagVn;
            initConfig();
        }
        public FrmScreenCaptureUpload(BangnaControl bc, String filename, String hn, String pttname, String vn, String flagVn, DocScan dsc)
        {
            InitializeComponent();
            this.bc = bc;
            this.filename = filename;
            this.hn = hn;
            this.pttname = pttname;
            this.vn = vn;
            this.flagVn = flagVn;
            this.dsc = dsc;
            initConfig();
        }
        public FrmScreenCaptureUpload(BangnaControl bc, String filename, String hn, String pttname, String vn, String flagVn, DocScan dsc,String certid, Boolean autoUpload)
        {
            InitializeComponent();
            this.bc = bc;
            this.filename = filename;
            this.hn = hn;
            this.pttname = pttname;
            this.vn = vn;
            this.flagVn = flagVn;
            this.dsc = dsc;
            this.CERTID = certid;
            this.AUTOUPLOAD = autoUpload;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            picWait.Hide();

            //bc.bcDB.dgsDB.setCboBsp(cboDgs, "");
            bc.bcDB.dgssDB.setCboBspDeptUS(cboDgs, "");
            bc.setC1Combo(cboDgs, bc.iniC.statusScreenCaptureUploadDoc);
            txtHn.Value = hn;
            lbName.Text = pttname;
            lbVn.Text = flagVn;
            txtVn.Value = vn;
            if (dsc != null && dsc.doc_scan_id.Length > 0) 
            {
                txtFM.Value = dsc.ml_fm;
            }
            btnUpload.Click += BtnUpload_Click;

            toolStripStatusLabel1.Text = CERTID;
        }
        private void UploadMertMed()
        {
            String dgssid = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
            if (dgssid.Length <= 0)
            {
                dgssid = bc.iniC.statusScreenCaptureUploadDoc;
                //MessageBox.Show("ไม่ได้เลือก กลุ่มเอกสาร", "");
                //return;
            }
            picWait.Show();
            string ext = Path.GetExtension(filename);
            String dgssname = "", vn = "", an = "";
            //dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            //DocGroupSubScan dgss = new DocGroupSubScan();
            //dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            DocScan dsc = new DocScan();
            //new LogWriter("d", "BtnUpload_Click dsc.vn " + dsc.vn + " dsc.an " + dsc.an);
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
            dsc.hn = txtHn.Text;
            
            //return;
            dsc.visit_date = "";
            dsc.pre_no = "";
            dsc.ml_fm = txtFM.Text.Trim();
            if (this.dsc != null && this.dsc.doc_scan_id.Length > 0)
            {
                dsc.visit_date = this.dsc.visit_date;
                dsc.pre_no = this.dsc.pre_no;
                bc.bcDB.dscDB.voidDocScanCertMed(this.dsc.doc_scan_id, "screencaptureupload");
            }
            if (lbVn.Text.ToUpper().IndexOf("VN") >= 0)
            {
                //MessageBox.Show("lbVn.Text.Trim().Equals", lbVn.Text.Trim());
                dsc.an = "";
                dsc.vn = txtVn.Text.Trim();
            }
            else
            {
                //MessageBox.Show("else lbVn.Text.Trim().Equals", lbVn.Text.Length.ToString());
                dsc.vn = "";
                dsc.an = txtVn.Text.Trim();
            }
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = dgssid;

            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "O";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_ml = "2";

            String re = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);

            sB11.Text = " filename " + filename + " bc.iniC.folderFTP " + bc.iniC.folderFTP + "//" + dsc.image_path;
            long chk = 0;
            if (long.TryParse(re, out chk))
            {
                //dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1
                dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + re + ext;
                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                if (CERTID.Length > 0) { String re2 = bc.bcDB.mcertiDB.updateDocScanIdScanUploadByPk("555" + CERTID, re); }
                //MessageBox.Show("filename" + filename + "\n bc.iniC.folderFTP " + bc.iniC.folderFTP + "//" + dsc.image_path, "");
                //FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                //MessageBox.Show("HN "+ txtHn.Text.Replace("/", "-"), "");
                //ftp.createDirectory(txtHn.Text);
                ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-"));
                //MessageBox.Show("222", "");
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //MessageBox.Show("333", "");

                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                {
                    File.Delete(filename);
                    System.Threading.Thread.Sleep(200);
                    this.Dispose();
                }
            }
        }
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //name = filename.Replace("*", "");
            UploadMertMed();
        }

        private void FrmScreenCaptureUpload_Load(object sender, EventArgs e)
        {
            theme1.SetTheme(this, bc.iniC.themeApplication);
            theme1.SetTheme(label1, bc.iniC.themeApplication);
            theme1.SetTheme(txtHn, bc.iniC.themeApplication);
            theme1.SetTheme(label2, bc.iniC.themeApplication);
            theme1.SetTheme(lbName, bc.iniC.themeApplication);
            theme1.SetTheme(cboDgs, bc.iniC.themeApplication);
            theme1.SetTheme(sB11, bc.iniC.themeApplication);
            theme1.SetTheme(lbVn, bc.iniC.themeApplication);
            theme1.SetTheme(txtVn, bc.iniC.themeApplication);
            theme1.SetTheme(txtFM, bc.iniC.themeApplication);
            theme1.SetTheme(label3, bc.iniC.themeApplication);
            sB11.Text = "hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP+" ini path "+bc.appName;
            if(AUTOUPLOAD) UploadMertMed();
        }
    }
}
