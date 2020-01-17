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
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmLabOutReceive : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        FileSystemWatcher watcher;
        //ListBox listBox1;

        public FrmLabOutReceive(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            getFileinFolder();
            watch();
        }
        private void watch()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = bc.iniC.pathLabOutReceive;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Created;
            watcher.EnableRaisingEvents = true;
        }
        private void getFileinFolder()
        {
            //if (listBox1 != null) listBox1 = null;
            //listBox1 = new ListBox();
            //listBox1.Dock = DockStyle.Fill;

            //panel1.Controls.Add(listBox1);
            //listBox1.Items.Clear();
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceive);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                //str = str + ", " + file.Name;
                listBox1.BeginUpdate();
                listBox1.Items.Add(file.Name);
                listBox1.EndUpdate();
                //listBoxSetText(file.Name);
                //listBox1.Invoke(() => listBox1.Items.Add(file.Name));
            }
        }
        private void uploadFiletoServer()
        {
            Application.DoEvents();
            Thread.Sleep(5000);

            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);

            //foreach(String filename in filePaths)
            //{
            //    String dgssid = "";
            //    dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            //    DocGroupSubScan dgss = new DocGroupSubScan();
            //    dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            //    DocScan dsc = new DocScan();
            //    dsc.active = "1";
            //    dsc.doc_scan_id = "";
            //    dsc.doc_group_id = dgss.doc_group_id;
            //    dsc.hn = txtHn.Text;
            //    dsc.vn = txtVN.Text;
            //    dsc.an = txtAN.Text;
            //    dsc.visit_date = bc.datetoDB(txtVisitDate.Text);
            //    dsc.host_ftp = bc.iniC.hostFTP;
            //    //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            //    dsc.image_path = "";
            //    dsc.doc_group_sub_id = dgssid;
            //    dsc.pre_no = txtPreNo.Text;
            //    dsc.an = txtAN.Text;
            //    DateTime dt = new DateTime();

            //    dsc.an_date = (DateTime.TryParse(txtAnDate.Text, out dt)) ? bc.datetoDB(txtAnDate.Text) : "";
            //    if (dsc.an_date.Equals("1-01-01"))
            //    {
            //        dsc.an_date = "";
            //    }
            //    dsc.folder_ftp = bc.iniC.folderFTP;
            //    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
            //    dsc.row_no = i.ToString();
            //    dsc.row_cnt = array1.Count.ToString();
            //    dsc.status_version = "2";
            //    String re = bc.bcDB.dscDB.insertDocScan(dsc, bc.userId);
            //    //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + re + ext;
            //    if (chkIPD.Checked)
            //    {
            //        vn = txtAN.Text.Replace("/", "_").Replace("(", "_").Replace(")", "");
            //    }
            //    else
            //    {
            //        vn = txtVN.Text.Replace("/", "_").Replace("(", "_").Replace(")", "");
            //    }
            //    //dsc.image_path = txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;       //-1
            //    dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1
            //    String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);

            //    //MessageBox.Show("111", "");
            //    //ftp.createDirectory(txtHn.Text);
            //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-"));       // สร้าง Folder HN
            //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn);
            //    //MessageBox.Show("222", "");
            //    ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
            //    //MessageBox.Show("333", "");
            //    ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, name);
            //}
        }
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            //throw new NotImplementedException();
            //getFileinFolder();
            uploadFiletoServer();
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            //throw new NotImplementedException();
            getFileinFolder();
        }

        private void FrmLabOutReceive_Load(object sender, EventArgs e)
        {

        }
    }
}
