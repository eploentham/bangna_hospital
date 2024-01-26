using bangna_hospital.control;

using bangna_hospital.object1;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScanViewEdit : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        String hn = "", vn = "", name = "", filename = "", visitDate="", dscid = "";
        
        MemoryStream stream;
        Image img1=null;
        DocScan dgs;
        Boolean pageLoad = false;
        public FrmScanViewEdit(BangnaControl bc, String hn, String vn, String name, Image img, String dscid, String statusOPD)
        {
            InitializeComponent();
            this.bc = bc;
            this.hn = hn;
            this.vn = vn;
            this.name = name;
            this.img1 = img;
            this.dscid = dscid;
            //visitDate = visitdate;
            initConfig();
        }

        private void initConfig()
        {
            pageLoad = true;
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            theme.Theme = bc.iniC.themeApplication;
            btnRotate.Click += BtnRotate_Click;
            bc.bcDB.dgsDB.setCboDgs(cboDgs, "");

            btnSave.Click += BtnSave_Click;
            btnAnalyze.Click += BtnAnalyze_Click;
            this.FormClosing += FrmScanNewView_FormClosing;
            txtFmCode.KeyUp += TxtFmCode_KeyUp;
            btnSaveFmCode.Click += BtnSaveFmCode_Click;
            cboDgs.SelectedIndexChanged += CboDgs_SelectedIndexChanged;
            chkVoid.Click += ChkVoid_Click;
            btnVoid.Click += BtnVoid_Click;
            btnGenSort.Click += BtnGenSort_Click;
            btnRotate.Click += BtnRotate_Click;
            btnSaveRotate.Click += BtnSaveRotate_Click;
            
            //theme1.SetTheme(sb1, "BeigeOne");

            //sb1.Text = "aaaaaaaaaa";

            setControl();
            pageLoad = false;
        }
        private void BtnSaveRotate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Stream stream = new MemoryStream();
            pic1.Image.Save(stream, ImageFormat.Jpeg);
            
            DocScan dsc = new DocScan();
            dsc = bc.bcDB.dscDB.castDocScan(dgs);
            dsc.doc_scan_id = "";
            string ext = Path.GetExtension(dgs.image_path);

            String re = bc.bcDB.dscDB.insertDocScan(dsc, "");
            dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn.Replace("/", "_").Replace("(", "_").Replace(")", "") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn.Replace("/", "_").Replace("(", "_").Replace(")", "") + "-" + re + ext;         //+1
            int chk = 0;
            if(int.TryParse(re, out chk))
            {
                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                stream.Seek(0, SeekOrigin.Begin);
                //MessageBox.Show(bc.iniC.folderFTP + "//" + dsc.image_path, "");
                Boolean chk1 = ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, stream);

                bc.bcDB.dscDB.voidDocScan2(dgs.doc_scan_id, "");
                MessageBox.Show("save file Rotate to seerver success", "");
            }
        }

        private void BtnGenSort_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = this.bc.bcDB.dscDB.selectByAnSortID(((Control)this.txtHn).Text, ((Control)this.txtAn).Text);
            if (dataTable2.Rows.Count <= 0)
                return;
            long result1 = 0;
            if (long.TryParse(dataTable2.Rows[0][this.bc.bcDB.dscDB.dsc.sort1].ToString(), out result1))
            {
                int num = 0;
                foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
                {
                    ++num;
                    int result2 = 0;
                    if (!int.TryParse(this.bc.bcDB.dscDB.updateSort(row[this.bc.bcDB.dscDB.dsc.doc_scan_id].ToString(), num.ToString()), out result2));
                }
                DocScan docScan1 = new DocScan();
                DocScan docScan2 = this.bc.bcDB.dscDB.selectByPk(((Control)this.txtID).Text);
                txtSort.Value = docScan2.sort1;
                txtSort.Value = docScan2.sort1;
            }
        }

        private void BtnVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการ ยกเลิกรายการ ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                String re = "";
                int chk = 0;
                re = bc.bcDB.dscDB.voidDocScan2(txtID.Text, txtFmCode.Text.Trim());
                if (int.TryParse(re, out chk))
                {
                    MessageBox.Show("ยกเลิกรายการ เรียบร้อย", "");
                }
            }
        }

        private void ChkVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkVoid.Checked)
            {
                btnVoid.Show();
            }
            else
            {
                btnVoid.Hide();
            }
        }

        private void CboDgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String dgsid = "";
            dgsid = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
            if (dgsid.Length > 0)
            {
                cboDgss.Text = "";
                bc.bcDB.dgssDB.setCboDGSS(cboDgss, dgsid, "");
            }
        }

        private void setControl()
        {
            string str = "";
            try
            {
                str = "00";
                dgs = bc.bcDB.dscDB.selectByPk(dscid);
                bc.bcDB.dgssDB.setCboDGSS(cboDgss, "");
                //bc.bcDB.dgsDB.setCboDgs(cboDgs, "");
                str = "01";
                txtID.Value = dgs.doc_scan_id;
                txtHn.Value = hn;
                txtVN.Value = dgs.vn;
                txtAn.Value = dgs.an;
                txtPttName.Value = name;
                txtVisitDate.Value = visitDate;
                txtSort.Value = dgs.sort1;
                txtFmCode.Value = dgs.ml_fm;
                //bc.setC1Combo(cboDgs, dgs);
                txtSortMax.Value = dgs.row_cnt;
                //txtSortMax.Value = dgs.row_cnt;
                //DocGroupFM docGroupFm1 = new DocGroupFM();
                //DocGroupFM docGroupFm2 = bc.bcDB.dfmDB.selectByFMCode(this.dgs.ml_fm);
                //if (docGroupFm2.fm_id.Length > 0)
                //{

                //}
                bc.setC1Combo(cboDgs, dgs.doc_group_id);
                bc.bcDB.dgssDB.setCboDGSS(cboDgss, dgs.doc_group_id, "");
                bc.setC1Combo(cboDgss, dgs.doc_group_sub_id);
                str = "02";
                pic1.Image = img1;
                //btnSaveFmCode.Hide();
                btnVoid.Hide();
            }
            catch (Exception ex)
            {
                LogWriter logWriter = new LogWriter("e", "FrmScanViewEdit setControl err " + str + " " + ex.Message);
            }
            
        }
        private void BtnSaveFmCode_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            int chk = 0;
            re = bc.bcDB.dscDB.updateFMCode(txtID.Text.Trim(), txtFmCode.Text.Trim());
            if (int.TryParse(re, out chk))
            {
                String dgsid = cboDgs.SelectedItem == null ? "" : ((ComboBoxItem)cboDgs.SelectedItem).Value;
                String dgssid = cboDgss.SelectedItem == null ? "" : ((ComboBoxItem)cboDgss.SelectedItem).Value;
                re = bc.bcDB.dscDB.updateGrpSubGrp(txtID.Text.Trim(), dgsid, dgssid);
                MessageBox.Show("แก้ไขข้อมูล FM-CODE เรียบร้อย", "");
            }
        }
        private void TxtFmCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                pageLoad = false;
                DocGroupFM dfm = new DocGroupFM();
                dfm = bc.bcDB.dfmDB.selectByFMCode(txtFmCode.Text.Trim());
                if ((dfm.doc_group_id != null) && (dfm.doc_group_id.Length > 0))
                {
                    bc.setC1Combo(cboDgs, dfm.doc_group_id);
                    bc.bcDB.dgssDB.setCboDGSS(cboDgss, dfm.doc_group_id, "");
                    bc.setC1Combo(cboDgss, dfm.doc_group_sub_id);
                    btnSaveFmCode.Show();
                }
                else
                {
                    bc.setC1Combo(cboDgs, "");
                    bc.setC1Combo(cboDgss, "");
                }
                pageLoad = true;
            }
            else
            {
                btnSaveFmCode.Hide();
            }
        }
        private void FrmScanNewView_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException();
            //if (formsOCREngine != null && formsOCREngine.IsStarted)
            //    formsOCREngine.Shutdown();
        }
        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //Startup();
        }
        private void BtnRotate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String dgs = "", id = "";            
            try
            {
                filename = filename.Substring(filename.IndexOf('*') + 1);
                //Image img=null;
                //img.Save(stream, ImageFormat.Jpeg);
                //resizedImage = bc.RotateImage(img);
                img1 = bc.RotateImage(img1);
                //img.Dispose();
                //if (File.Exists(filename))
                //{
                //    File.Delete(filename);
                //}
                //img1.Save(filename);
                //Bitmap bmp;
                //bmp = (Bitmap)img1;
                //bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                //Image img2 = Image.FromFile(filename);
                pic1.Image = img1;
                //pnImg.Width = img1.
            }
            catch (Exception ex)
            {
                dgs = ex.Message;
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            int chk = 0;
            re = bc.bcDB.dscDB.updateSort(txtID.Text, txtSort1.Text.Trim());
            if(int.TryParse(re, out chk))
            {
                MessageBox.Show("แก้ไขข้อมูล ลำดับ เรียบร้อย", "");
            }

        }
        private void FrmScanViewEdit_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            theme.SetTheme(pn, bc.iniC.themeApp);
            foreach(Control c in pn.Controls)
            {
                c.Font = fEdit;
            }
            //Screen.PrimaryScreen.Bounds.Width.ToString();
            //pn.Width = 340;
            //pic1.Size = img1.Size;
            pic1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
