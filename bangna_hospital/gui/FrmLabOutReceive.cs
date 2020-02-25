using bangna_hospital.control;
using bangna_hospital.object1;
using Ionic.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        Boolean chkFileInnoTech = false, chkFileRIA = false;
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
            //getFileinFolderInnoTech();
            getFileinFolderRIA();
            if (chkFileInnoTech)
            {
                uploadFiletoServerInnoTech();
            }
            if (chkFileRIA)
            {
                uploadFiletoServerRIA();
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
        private void getFileinFolderRIA()
        {
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceiveRIA);//Assuming Test is your Folder
            //DirectoryInfo[] dirs = d.GetDirectories();
            //foreach (FileInfo diNext in d.FullName)
            //{
            //Console.WriteLine("The number of files in {0} is {1}", diNext, diNext.GetFiles().Length);
            FileInfo[] Files = d.GetFiles("*.zip"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                //str = str + ", " + file.Name;
                listBox3.BeginUpdate();     //listBox1
                listBox3.Items.Add(file.FullName);     //listBox1
                listBox3.EndUpdate();     //listBox1
                chkFileRIA = true;
            }
            //}
        }
        private void uploadFiletoServerRIA()
        {
            timer.Stop();
            listBox2.Items.Clear();     //listBox3
            Application.DoEvents();
            Thread.Sleep(1000);
            if (!Directory.Exists(bc.iniC.pathLabOutBackupRIAZipExtract))
            {
                Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIAZipExtract);
            }
            if (!Directory.Exists(bc.iniC.pathLabOutBackupRIA))
            {
                Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIA);
            }
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            //String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            List<String> filePaths = new List<String>();
            DirectoryInfo dir = new DirectoryInfo(bc.iniC.pathLabOutReceiveRIA);
            FileInfo[] Files = dir.GetFiles("*.zip"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                //Console.WriteLine("The number of files in {0} is {1}", diNext, diNext.GetFiles().Length);
                string str = "";
                filePaths.Add(file.FullName);
            }
            String dgssid = "";
            dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            foreach (String zipFilename in filePaths)
            {
                listBox2.Items.Add("พบ file " + zipFilename);
                Application.DoEvents();
                int year2 = 0;
                String yy = "", mm = "", dd = "", reqid = "", vn = "", datetick1 = "", pathbackup = "", year1 = "", ext = "",pathname = "", tmp = "", filename2="", hn="", filenamePDF="";
                var fileStreamJson = new MemoryStream();
                var fileStreamPDF = new MemoryStream();
                //Json

                datetick1 = DateTime.Now.Ticks.ToString();
                pathbackup = bc.iniC.pathLabOutBackupRIA + "\\" + datetick1;
                if (!Directory.Exists(pathbackup))
                {
                    Directory.CreateDirectory(pathbackup);
                }
                //using (var zip = ZipFile.Read(filename))
                //{
                //    foreach (var entry in zip)
                //    {
                //        ext = Path.GetExtension(entry.FileName);
                //        if (ext.ToLower().Equals(".json"))
                //        {
                //            entry.Extract(fileStreamJson);
                //            fileStreamJson.Seek(0, SeekOrigin.Begin);
                //            StreamReader reader = new StreamReader(fileStreamJson, System.Text.Encoding.UTF8, true);
                //            using (JsonTextReader readerJson = new JsonTextReader(reader))
                //            {
                //                //JObject o2 = (JObject)JToken.ReadFrom(readerJson);
                //                //RIA_Patient account = JsonConvert.DeserializeObject<RIA_Patient>(readerJson);
                //                string jsonData1 = @"{'idcard': '', 'hn': '', title:'', fname:'', lname:'', sex:'', dob:''}";
                //                JsonSerializer serializer = new JsonSerializer();
                //                var movie2 = serializer.Deserialize(readerJson);
                //                JObject o2 = (JObject)JToken.ReadFrom(readerJson);
                //                dynamic obj = o2["patient"];
                //                //dynamic obj = JsonConvert.DeserializeObject(movie2.ToString());
                //                //var jsonData = JObject.Parse(movie2.ToString());
                //                //RIA_Patient account = (RIA_Patient)jsonData[0]["hn"].ToString();
                //                //RIA_Patient rptt = new RIA_Patient();
                //                //rptt = (RIA_Patient)movie2[0];
                //            }
                //        }
                //        else if (ext.ToLower().Equals(".pdf"))
                //        {
                //            entry.Extract(fileStreamPDF);
                //            fileStreamPDF.Seek(0, SeekOrigin.Begin);

                //        }
                //    }
                //}
                RIA_Patient aaa = new RIA_Patient();
                ZipFile zf = new ZipFile(zipFilename);
                zf.ExtractAll(pathbackup);
                DirectoryInfo dZip = new DirectoryInfo(pathbackup);
                FileInfo[] filesZip = dZip.GetFiles("*.*");
                foreach (FileInfo file in filesZip)
                {
                    ext = Path.GetExtension(file.FullName);
                    if (ext.ToLower().Equals(".json"))
                    {
                        using (StreamReader file1 = File.OpenText(file.FullName))
                        using (JsonTextReader reader = new JsonTextReader(file1))
                        {
                            JObject o2 = (JObject)JToken.ReadFrom(reader);
                            
                            dynamic objPtt = o2["patient"];
                            dynamic objOrd = o2["orderdetail"];
                            aaa.hn = objPtt.hn;
                            aaa.idcard = objPtt.idcard;
                            aaa.fname = objPtt.fname;
                            aaa.lname = objPtt.lname;
                            aaa.ref_no = objOrd.ref_no;
                            aaa.order_number = objOrd.order_number;
                            aaa.ln = objOrd.ln;
                            aaa.hn_customer = objOrd.hn_customer;
                            aaa.status = objOrd.status;
                            aaa.comment_order = objOrd.comment_order;
                            aaa.comment_patient = objOrd.comment_patient;
                            aaa.ward_customer = objOrd.ward_customer;
                            aaa.doctor = objOrd.doctor;
                            aaa.time_register = objOrd.time_register;
                            aaa.ward_customer = objOrd.ward_customer;
                            filename2 = aaa.ref_no;
                            //aaa = (RIA_Patient)o2["patient"];
                            //IList<JToken> results = o2["patient"]["fname"].Children().ToList();
                        }
                    }
                    else if (ext.ToLower().Equals(".pdf"))
                    {
                        filenamePDF = file.FullName;
                    }
                }
                filesZip = null;
                if (filename2.Length <= 0)
                {
                    MessageBox.Show("aaaaaa", "");
                    return;
                }
                dZip = null;
                zf.Dispose();
                reqid = filename2.Substring(filename2.Length - 3);
                yy = filename2.Substring(filename2.Length - 5, 2);
                mm = filename2.Substring(filename2.Length - 7, 2);
                dd = filename2.Substring(filename2.Length - 9, 2);
                year1 = "20" + yy;
                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.SelectHnLabOut(reqid, year1 + "-" + mm + "-" + dd);
                if (dt.Rows.Count <= 0)
                {
                    listBox2.Items.Add("Filename ไม่พบข้อมูล HIS " + zipFilename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่พบข้อมูล HIS");
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupRIA))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIA);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupRIA + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIA + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(zipFilename, bc.iniC.pathLabOutBackupRIA + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(zipFilename, bc.iniC.pathLabOutBackupRIA + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("พบข้อมูล HIS " + dt.Rows[0]["mnc_hn_no"].ToString());
                Application.DoEvents();
                if (!dt.Rows[0]["mnc_hn_no"].ToString().Equals(aaa.hn_customer))
                {
                    MessageBox.Show("aaaaaa", "");
                }
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = dgss.doc_group_id;
                dsc.hn = dt.Rows[0]["mnc_hn_no"].ToString();
                dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                dsc.an = dt.Rows[0]["MNC_AN_NO"].ToString() + "/" + dt.Rows[0]["MNC_AN_YR"].ToString();
                if (dsc.an.Equals("/"))
                {
                    dsc.an = "";
                }
                dsc.visit_date = "";
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = dgssid;
                dsc.pre_no = dt.Rows[0]["mnc_pre_no"].ToString();
                //dsc.an = "";
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
                dsc.req_id = dt.Rows[0]["mnc_req_no"].ToString();
                DateTime dtt = new DateTime();
                if (DateTime.TryParse(dt.Rows[0]["mnc_req_dat"].ToString(), out dtt))
                {
                    dsc.date_req = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
                }
                else
                {
                    dsc.date_req = "";
                }
                if (dsc.an.Length > 0)
                {
                    dsc.status_ipd = "1";
                }
                else
                {
                    dsc.status_ipd = "0";
                }
                //dsc.ml_fm = "FM-LAB-999";
                
                dsc.ml_fm = "FM-LAB-996";       //RIA
                
                dsc.patient_fullname = dt.Rows[0]["mnc_patname"].ToString();
                dsc.status_record = "2";
                String re = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                if (re.Length <= 0)
                {
                    listBox2.Items.Add("ไม่ได้เลขที่ " + zipFilename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "ไม่ได้เลขที่ " + zipFilename);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(zipFilename, pathbackup + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(zipFilename, pathbackup + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("ได้เลขที่ " + re);
                Application.DoEvents();
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString() + "//" + dt.Rows[0]["mnc_hn_no"].ToString() + "_" + re + ext;
                //    if (chkIPD.Checked)
                //    {
                //        vn = txtAN.Text.Replace("/", "_").Replace("(", "_").Replace(")", "");
                //    }
                //    else
                //    {
                vn = dsc.vn.Replace("/", "_").Replace("(", "_").Replace(")", "");
                
                dsc.ml_fm = "FM-LAB-996";       //RIA
                
                //    }
                //    //dsc.image_path = txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;       //-1
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1                

                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                listBox2.Items.Add("updateImagepath " + dsc.image_path);
                Application.DoEvents();
                //    //MessageBox.Show("111", "");

                ftp.createDirectory(bc.iniC.folderFTP + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-"));       // สร้าง Folder HN
                //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn);
                //    //MessageBox.Show("222", "");
                Thread.Sleep(200);
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //    //MessageBox.Show("333", "");
                Thread.Sleep(200);
                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, zipFilename))
                {
                    Thread.Sleep(200);
                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filenamePDF))
                    {
                        listBox2.Items.Add("FTP upload success ");
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        String datetick = "";
                        datetick = DateTime.Now.Ticks.ToString();
                        //if (!Directory.Exists(bc.iniC.pathLabOutBackupRIA))
                        //{
                        //    Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIA);
                        //}
                        //if (pathname.Length > 0)
                        //{
                        //    if (!Directory.Exists(bc.iniC.pathLabOutBackupRIA + "\\" + pathname))
                        //    {
                        //        Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIA + "\\" + pathname);
                        //    }
                        //}
                        Thread.Sleep(1000);
                        if (File.Exists(zipFilename))
                        {
                            //dZip.MoveTo("");
                            try
                            {
                                File.Move(zipFilename, pathbackup + "\\" + filename2 + "_" + datetick + ".zip");
                            }
                            catch (Exception ex)
                            {
                                String aaaa = "";
                            }
                        }
                    }
                    //else
                    //{
                    //    File.Move(zipFilename, pathbackup + "\\" + filename2 + "_" + datetick + ".zip");
                    //}
                    listBox1.BeginUpdate();     //listBox2
                    listBox1.Items.Add(zipFilename + " -> " + bc.iniC.hostFTP + "//" + bc.iniC.folderFTP + "//" + dsc.image_path);     //listBox2
                    listBox1.EndUpdate();     //listBox2
                    Application.DoEvents();
                    Thread.Sleep(1000 * 60);
                }
                else
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            timer.Start();
        }
        private void getFileinFolderInnoTech()
        {
            //if (listBox1 != null) listBox1 = null;
            //listBox1 = new ListBox();
            //listBox1.Dock = DockStyle.Fill;

            //panel1.Controls.Add(listBox1);
            listBox3.Items.Clear();     //listBox1
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceiveInnoTech);//Assuming Test is your Folder
            DirectoryInfo[] dirs = d.GetDirectories();
            foreach (DirectoryInfo diNext in dirs)
            {
                Console.WriteLine("The number of files in {0} is {1}", diNext, diNext.GetFiles().Length);
                FileInfo[] Files = diNext.GetFiles("*.*"); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    //str = str + ", " + file.Name;
                    listBox3.BeginUpdate();     //listBox1
                    listBox3.Items.Add(file.FullName);     //listBox1
                    listBox3.EndUpdate();     //listBox1
                    chkFileInnoTech = true;
                    //listBoxSetText(file.Name);
                    //listBox1.Invoke(() => listBox1.Items.Add(file.Name));
                }
            }
        }
        private void uploadFiletoServerInnoTech()
        {
            timer.Stop();
            listBox2.Items.Clear();     //listBox3
            Application.DoEvents();
            Thread.Sleep(1000);

            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            //String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            List<String> filePaths = new List<String>();
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceiveInnoTech);//Assuming Test is your Folder
            DirectoryInfo[] dirs = d.GetDirectories();
            foreach (DirectoryInfo diNext in dirs)
            {
                Console.WriteLine("The number of files in {0} is {1}", diNext, diNext.GetFiles().Length);
                FileInfo[] Files = diNext.GetFiles("*.*"); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    filePaths.Add(file.FullName);
                }
            }
            String dgssid = "";
            dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            foreach (String filename in filePaths)
            {
                listBox2.Items.Add("พบ file "+ filename);
                Application.DoEvents();
                int year2 = 0;
                String yy = "", mm = "", dd = "", reqid = "", vn="", filename1="", filename2="", year1="", ext="";
                String pathname = "",tmp="";
                tmp = bc.iniC.pathLabOutReceiveInnoTech.Replace("\\\\", "\\");
                filename1 = Path.GetFileName(filename);
                pathname = filename.Replace(filename1, "").Replace(tmp, "").Replace("\\", "");
                pathname = pathname.Replace("\\", "");
                String[] txt = filename1.Split('_');
                if (txt.Length > 1)
                {
                    filename2 = txt[0];
                }
                else
                {
                    filename2 = filename1.Replace(".pdf", "");
                }
                ext = Path.GetExtension(filename1);
                
                if (filename2.Replace(".pdf","").Length < 10)
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่ถูก FORMAT");
                    listBox2.Items.Add("Filename ไม่ถูก FORMAT " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech+"\\"+pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                if (filename2.Length <= 10)
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ชื่อ File สั้นไป "+ filename);
                    listBox2.Items.Add("Filename ชื่อ File สั้นไป " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                DateTime dtt1 = new DateTime();
                int.TryParse(year1, out year2);
                yy = filename2.Substring(filename2.Length - 5, 2);
                mm = filename2.Substring(filename2.Length - 7, 2);
                dd = filename2.Substring(filename2.Length - 9, 2);
                year1 = "20" + yy;
                if (!DateTime.TryParse(year1 + "-"+mm+"-"+dd, out dtt1))
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ชื่อ File ไม่สามารถหา date ได้ " + filename);
                    listBox2.Items.Add("Filename ชื่อ File ไม่สามารถหา date ได้ " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                reqid = filename2.Substring(filename2.Length - 3);
                
                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.SelectHnLabOut(reqid, year1 + "-" + mm + "-"+ dd);
                if(dt.Rows.Count <= 0)
                {
                    listBox2.Items.Add("Filename ไม่พบข้อมูล HIS " + filename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่พบข้อมูล HIS");
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("พบข้อมูล HIS " + dt.Rows[0]["mnc_hn_no"].ToString());
                Application.DoEvents();
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = dgss.doc_group_id;
                dsc.hn = dt.Rows[0]["mnc_hn_no"].ToString();
                dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                dsc.an = dt.Rows[0]["MNC_AN_NO"].ToString() + "/" + dt.Rows[0]["MNC_AN_YR"].ToString();
                if (dsc.an.Equals("/"))
                {
                    dsc.an = "";
                }
                dsc.visit_date = "";
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = dgssid;
                dsc.pre_no = dt.Rows[0]["mnc_pre_no"].ToString();
                //dsc.an = "";
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
                dsc.req_id = dt.Rows[0]["mnc_req_no"].ToString();
                DateTime dtt = new DateTime();
                if (DateTime.TryParse(dt.Rows[0]["mnc_req_dat"].ToString(), out dtt))
                {
                    dsc.date_req = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
                }
                else
                {
                    dsc.date_req = "";
                }
                if (dsc.an.Length > 0)
                {
                    dsc.status_ipd = "1";
                }
                else
                {
                    dsc.status_ipd = "0";
                }
                //dsc.ml_fm = "FM-LAB-999";
                if (pathname.Equals("ClinicalReport"))
                {
                    dsc.ml_fm = "FM-LAB-998";
                }
                else
                {
                    dsc.ml_fm = "FM-LAB-997";       //PathoReport
                }
                dsc.patient_fullname = dt.Rows[0]["mnc_patname"].ToString();
                dsc.status_record = "2";
                String re = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                if (re.Length <= 0)
                {
                    listBox2.Items.Add("ไม่ได้เลขที่ " + filename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "ไม่ได้เลขที่ " + filename);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("ได้เลขที่ " + re);
                Application.DoEvents();
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString() + "//" + dt.Rows[0]["mnc_hn_no"].ToString() + "_" + re + ext;
                //    if (chkIPD.Checked)
                //    {
                //        vn = txtAN.Text.Replace("/", "_").Replace("(", "_").Replace(")", "");
                //    }
                //    else
                //    {
                vn = dsc.vn.Replace("/", "_").Replace("(", "_").Replace(")", "");
                if (pathname.Equals("ClinicalReport"))
                {
                    dsc.ml_fm = "FM-LAB-998";
                }
                else 
                {
                    dsc.ml_fm = "FM-LAB-997";       //PathoReport
                }
                //    }
                //    //dsc.image_path = txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;       //-1
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1                
                
                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                listBox2.Items.Add("updateImagepath " + dsc.image_path);
                Application.DoEvents();
                //    //MessageBox.Show("111", "");

                ftp.createDirectory(bc.iniC.folderFTP + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-"));       // สร้าง Folder HN
                //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn);
                //    //MessageBox.Show("222", "");
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //    //MessageBox.Show("333", "");
                Thread.Sleep(200);
                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                {
                    listBox2.Items.Add("FTP upload success " );
                    Application.DoEvents();
                    Thread.Sleep(1000);
                    String datetick = "";
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(1000);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + filename2 + "_" + datetick + ext);
                    }
                    listBox1.BeginUpdate();     //listBox2
                    listBox1.Items.Add(filename + " -> " + bc.iniC.hostFTP + "//" + bc.iniC.folderFTP + "//" + dsc.image_path);     //listBox2
                    listBox1.EndUpdate();     //listBox2
                    Application.DoEvents();
                    Thread.Sleep(1000*60);
                }
                else
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            timer.Start();
        }
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            //throw new NotImplementedException();
            //getFileinFolder();
            uploadFiletoServerInnoTech();
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            //throw new NotImplementedException();
            getFileinFolderInnoTech();
        }

        private void FrmLabOutReceive_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 2020-02-12";
        }
    }
}