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
        //FileSystemWatcher watcher;
        //ListBox listBox1;
        System.Windows.Forms.Timer timer;
        Boolean chkFile = false;
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

            timer = new System.Windows.Forms.Timer();
            timer.Enabled = true;
            timer.Interval = bc.timerCheckLabOut*1000;
            timer.Tick += Timer_Tick;
            
            //watch();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            getFileinFolder();
            if (chkFile)
            {
                uploadFiletoServer();
            }
        }

        //private void watch()
        //{
        //    watcher = new FileSystemWatcher();
        //    watcher.Path = bc.iniC.pathLabOutReceive;
        //    watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
        //                           | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        //    watcher.Filter = "*.*";
        //    watcher.Changed += Watcher_Changed;
        //    watcher.Created += Watcher_Created;
        //    watcher.EnableRaisingEvents = true;
        //}
        private void getFileinFolder()
        {
            //if (listBox1 != null) listBox1 = null;
            //listBox1 = new ListBox();
            //listBox1.Dock = DockStyle.Fill;

            //panel1.Controls.Add(listBox1);
            listBox1.Items.Clear();
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceive);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                //str = str + ", " + file.Name;
                listBox1.BeginUpdate();
                listBox1.Items.Add(file.Name);
                listBox1.EndUpdate();
                chkFile = true;
                //listBoxSetText(file.Name);
                //listBox1.Invoke(() => listBox1.Items.Add(file.Name));
            }
        }
        private void uploadFiletoServer()
        {
            timer.Stop();
            listBox3.Items.Clear();
            Application.DoEvents();
            Thread.Sleep(1000);

            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            String dgssid = "";
            dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            foreach (String filename in filePaths)
            {
                listBox3.Items.Add("พบ file "+ filename);
                int year2 = 0;
                String yy = "", mm = "", dd = "", reqid = "", vn="", filename1="", filename2="", year1="";
                filename1 = Path.GetFileName(filename);
                string ext = Path.GetExtension(filename1);
                filename2 = filename1.Replace(".pdf", "");
                if (filename2.Replace(".pdf","").Length < 10)
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่ถูก FORMAT");
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackup))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackup);
                    }
                    Thread.Sleep(200);
                    File.Move(filename, bc.iniC.pathLabOutBackup + "\\err_" + filename2 + "_" + datetick + ext);
                    Thread.Sleep(1000);
                    continue;
                }
                reqid = filename2.Substring(filename2.Length - 3);
                yy = filename2.Substring(filename2.Length - 5, 2);
                mm = filename2.Substring(filename2.Length - 7, 2);
                dd = filename2.Substring(filename2.Length - 9, 2);
                year1 = "20" + yy;
                int.TryParse(year1, out year2);
                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.SelectHnLabOut(reqid, year1 + "-" + mm + "-"+ dd);
                if(dt.Rows.Count <= 0)
                {
                    listBox3.Items.Add("Filename ไม่พบข้อมูล HIS " + filename);
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่พบข้อมูล HIS");
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackup))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackup);
                    }
                    Thread.Sleep(200);
                    File.Move(filename, bc.iniC.pathLabOutBackup + "\\err_" + filename2 + "_" + datetick + ext);
                    Thread.Sleep(1000);
                    continue;
                }
                listBox3.Items.Add("พบข้อมูล HIS " + dt.Rows[0]["mnc_hn_no"].ToString());
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = dgss.doc_group_id;
                dsc.hn = dt.Rows[0]["mnc_hn_no"].ToString();
                dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                dsc.an = "";
                dsc.visit_date = "";
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = dgssid;
                dsc.pre_no = dt.Rows[0]["mnc_pre_no"].ToString();
                dsc.an = "";
                //DateTime dt = new DateTime();

                //    dsc.an_date = (DateTime.TryParse(txtAnDate.Text, out dt)) ? bc.datetoDB(txtAnDate.Text) : "";
                //    if (dsc.an_date.Equals("1-01-01"))
                //    {
                //        dsc.an_date = "";
                //    }
                dsc.folder_ftp = bc.iniC.folderFTP;
                //    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_version = "2";
                String re = bc.bcDB.dscDB.insertDocScan(dsc, bc.userId);
                listBox3.Items.Add("ได้เลขที่ " + re);
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString() + "//" + dt.Rows[0]["mnc_hn_no"].ToString() + "_" + re + ext;
                //    if (chkIPD.Checked)
                //    {
                //        vn = txtAN.Text.Replace("/", "_").Replace("(", "_").Replace(")", "");
                //    }
                //    else
                //    {
                vn = dsc.vn.Replace("/", "_").Replace("(", "_").Replace(")", "");
                dsc.ml_fm = "FM-LAB-999";
                //    }
                //    //dsc.image_path = txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;       //-1
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1
                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                listBox3.Items.Add("updateImagepath " + dsc.image_path);
                //    //MessageBox.Show("111", "");

                ftp.createDirectory(bc.iniC.folderFTP + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-"));       // สร้าง Folder HN
                //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn);
                //    //MessageBox.Show("222", "");
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //    //MessageBox.Show("333", "");
                Thread.Sleep(200);
                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                {
                    listBox3.Items.Add("FTP upload success " );
                    Thread.Sleep(1000);
                    String datetick = "";
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackup))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackup);
                    }
                    Thread.Sleep(200);
                    File.Move(filename, bc.iniC.pathLabOutBackup + "\\" + filename2 + "_" + datetick + ext);
                    Thread.Sleep(1000);
                    listBox2.BeginUpdate();
                    listBox2.Items.Add(bc.iniC.folderFTP + "//" + dsc.image_path);
                    listBox2.EndUpdate();
                }
                else
                {
                    listBox3.Items.Add("FTP upload success ");
                    new LogWriter("e", "FTP upload no success");
                }
            }
            timer.Start();
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
