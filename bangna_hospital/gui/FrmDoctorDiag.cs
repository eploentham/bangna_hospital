using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmDoctorDiag : Form
    {
        BangnaControl bc;
        String title = "", filename="", hn="", vsdate="", preno="", status="";
        Font fEdit, fEditB, fEditBig;
        Patient ptt;

        public FrmDoctorDiag(BangnaControl bc, String title, String hn)
        {
            InitializeComponent();
            this.bc = bc;
            this.title = title;
            this.hn = hn;
            //this.vsdate = vsdate;
            //this.preno = preno;
            
            status = title;
            
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinet(hn);

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("Template 1", new EventHandler(ContextMenu_Template1));
            richTextBox1.ContextMenu = menuGw;

            rbnSave.Click += RbnSave_Click;
            rbnBullet.Click += RbnBullet_Click;
            rbnPaint.Click += RbnPaint_Click;
        }

        private void RbnPaint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (File.Exists(@"white_300_200.jpg"))
            {
                if (!Directory.Exists("temp_med"))
                {
                    Directory.CreateDirectory("temp_med");
                }
                String datetick = "", filename="";
                datetick = DateTime.Now.Ticks.ToString();
                filename = "temp_med\\" + datetick + ".jpg";
                File.Copy(@"white_300_200.jpg",  filename);
                var filePath = @filename;
                ProcessStartInfo Info = new ProcessStartInfo()
                {
                    FileName = "mspaint.exe",
                    WindowStyle = ProcessWindowStyle.Maximized,
                    Arguments = filePath
                };
                Process procPaint = Process.Start(Info);
                procPaint.WaitForExit();
                setImagePaint(filename);
                this.filename = filename;
            }
            
            //richTextBox1.SelectedText = "aaaaa";
        }
        private void setImagePaint(String filename)
        {
            Clipboard.SetImage(Image.FromFile(filename));
            richTextBox1.Paste();
        }
        private void RbnBullet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //richTextBox1.SelectedText = richTextBox1.BulletIndent;
            //richTextBox1.Text = "welcome to author code";
            richTextBox1.SelectionIndent = 20;
            richTextBox1.BulletIndent = 10;
            richTextBox1.SelectionBullet = true;
        }

        private void RbnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            bc.cStf.staff_id = "";
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog(this);
            if (!bc.cStf.staff_id.Equals(""))
            {
                String filename = "", ext="";
                if (!Directory.Exists("medical"))
                {
                    Directory.CreateDirectory("medical");
                }
                filename = "medical\\"+hn + "_" +bc.vsdate + "_" + bc.preno + "_" + status + ".rtf";
                SaveDocumentMedicalExamination(filename);
                Application.DoEvents();
                Thread.Sleep(200);
                if (File.Exists(filename))
                {
                    ext = Path.GetExtension(filename);
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                    DocScan dsc = new DocScan();
                    dsc.active = "1";
                    dsc.doc_scan_id = "";
                    dsc.doc_group_id = "";
                    dsc.hn = bc.hn;
                    //dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                    dsc.an = "";
                    
                    dsc.visit_date = bc.vsdate;
                    dsc.host_ftp = bc.iniC.hostFTP;
                    //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                    dsc.image_path = "";
                    dsc.doc_group_sub_id = "";
                    dsc.pre_no = bc.preno;
                    
                    dsc.folder_ftp = bc.iniC.folderFTP;
                    //    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
                    dsc.row_no = "1";
                    dsc.row_cnt = "1";
                    dsc.status_version = "2";
                    dsc.req_id = "";
                    dsc.date_req = "";
                    dsc.status_ipd = "0";
                    //dsc.ml_fm = "FM-LAB-999";
                    if (status.Equals("cc"))
                    {
                        dsc.ml_fm = "FM-MED-900";       //
                    }
                    else if (status.Equals("me"))
                    {
                        dsc.ml_fm = "FM-MED-901";       //
                    }
                    else if (status.Equals("diag"))
                    {
                        dsc.ml_fm = "FM-MED-902";       //
                    }
                    bc.bcDB.dscDB.voidDocScanByStatusMedicalExamination(hn, dsc.ml_fm, bc.vsdate, bc.preno, bc.cStf.staff_id);
                    dsc.patient_fullname = ptt.Name;
                    dsc.status_record = "5";        // status medical diagnose
                    dsc.comp_labout_id = "";
                    String re = bc.bcDB.dscDB.insertMedicalExamination(dsc, bc.userId);
                    dsc.image_path = hn + "//" + status +"_"+ bc.hn + "_" + bc.vsdate +"_"+ bc.preno +"_"+ re + ext;
                    String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                    ftp.createDirectory(bc.iniC.folderFTP + "//" + hn.Replace("/", "-"));       // สร้าง Folder HN
                    ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                    Thread.Sleep(200);
                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                    {

                    }
                }
            }
        }
        private void SaveDocumentMedicalExamination(String filename)
        {
            //string filePathName = this.documentPath + '\\' + this.documentName;
            try
            {
                String PathName = "medical";
                if (File.Exists(PathName+""+ filename))
                {
                    File.Delete(PathName + "" + filename);
                    System.Threading.Thread.Sleep(200);
                }
                //foreach(String rthline in richTextBox1.Lines)
                //{
                    
                //}
                richTextBox1.SaveFile(filename, RichTextBoxStreamType.RichText);
                //bc.savePicOPUtoServer(txtIdOld.Text, documentName, filePathName);
            }
            catch (Exception ex)
            {

            }
        }
        private void ContextMenu_Template1(object sender, System.EventArgs e)
        {
            Clipboard.SetImage(Image.FromFile("d:\\other\\test.jpg"));

            richTextBox1.Paste();
        }
        private void loadDoctorDiag()
        {
            String mlfm = "";
            DocScan docCC = new DocScan();
            //DocScan docME = new DocScan();
            //DocScan docDiag = new DocScan();
            if (status.Equals("cc"))
            {
                mlfm = "FM-MED-900";
            }
            else if (title.Equals("me"))
            {
                mlfm = "FM-MED-901";
            }
            else if (title.Equals("diag"))
            {
                mlfm = "FM-MED-902";
            }
            docCC = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, mlfm, bc.vsdate, bc.preno);
            //docME = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, "FM-MED-901", vsdate, preno);
            //docDiag = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, "FM-MED-902", vsdate, preno);
            MemoryStream streamCC, streamME, streamDiag;
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);

            streamCC = ftp.download(bc.iniC.folderFTP + "//" + docCC.image_path);
            Thread.Sleep(200);
            //streamME = ftp.download(bc.iniC.folderFTP + "//" + docME.image_path);
            //Thread.Sleep(200);
            //streamDiag = ftp.download(bc.iniC.folderFTP + "//" + docDiag.image_path);
            //Thread.Sleep(200);
            richTextBox1.Text = "";
            streamCC.Position = 0;
            if (streamCC.Length > 0)
            {
                richTextBox1.LoadFile(streamCC, RichTextBoxStreamType.RichText);
            }
        }
        private void FrmDoctorDiag_Load(object sender, EventArgs e)
        {
            this.Text = "Medical Examination";
            //richTextBox1.Text = "Medical Examination";
            //ribbonGroup1.Text = "";
            if (title.Equals("cc"))
            {
                ribbonLabel1.Text = "Cheif Compliant";
            }
            else if (title.Equals("me"))
            {
                ribbonLabel1.Text = "Medical Examination";
            }
            else if (title.Equals("diag"))
            {
                ribbonLabel1.Text = "Diagnose";
            }
            //richTextBox1.LoadFile("progressnote.rtf");
            loadDoctorDiag();
        }
    }
}
