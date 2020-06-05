using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.control
{
    public class BangnaControl
    {
        public InitConfig iniC;
        private IniFile iniF;
        public ConnectDB conn;

        public String theme = "", userId = "", hn="", vn="", preno="", appName = "";
        public Color cTxtFocus;
        public Staff user;
        public Staff sStf, cStf;
        public int grdViewFontSize = 0, imggridscanwidth=0;

        public BangnaHospitalDB bcDB;

        public Patient sPtt;
        public Boolean ftpUsePassive = false, ftpUsePassiveLabOut = false;
        public int grfScanWidth = 0, imgScanWidth = 0, txtSearchHnLenghtStart=0, timerCheckLabOut=0, tabLabOutImageHeight = 0, tabLabOutImageWidth = 0;

        public BangnaControl()
        {
            initConfig();
        }
        private void initConfig()
        {
            //MessageBox.Show("h1111n ", "");
            try
            {
                appName = System.AppDomain.CurrentDomain.FriendlyName;
                appName = appName.ToLower().Replace(".exe", "");
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\" + appName + ".ini"))
                {
                    appName = Environment.CurrentDirectory + "\\" + appName + ".ini";
                }
                else
                {
                    appName = Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini";
                }
                if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("bangna_hospital_scan_capture"))
                {
                    appName = "c:\\bangna_hospital.ini";
                }
                //MessageBox.Show("h1111n ", "");
                iniF = new IniFile(appName);
                iniC = new InitConfig();
                cTxtFocus = ColorTranslator.FromHtml(iniC.txtFocus);
                user = new Staff();
                sPtt = new Patient();
                sStf = new Staff();
                cStf = new Staff();

                GetConfig();
                conn = new ConnectDB(iniC);
                bcDB = new BangnaHospitalDB(conn);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "BangnaControl initConfig ");
                MessageBox.Show("error "+ex.Message, "");
            }
            
        }
        public void getInit()
        {
            //bcDB.sexDB.getlSex();
            //cop = ivfDB.copDB.selectByCode1("001");
        }
        public void GetConfig()
        {
            //MessageBox.Show("hn " , "");
            iniC.hostDB = iniF.getIni("connection", "hostDB");
            iniC.nameDB = iniF.getIni("connection", "nameDB");
            iniC.userDB = iniF.getIni("connection", "userDB");
            iniC.passDB = iniF.getIni("connection", "passDB");
            iniC.portDB = iniF.getIni("connection", "portDB");

            iniC.hostDBMainHIS = iniF.getIni("connection", "hostDBMainHIS");
            iniC.nameDBMainHIS = iniF.getIni("connection", "nameDBMainHIS");
            iniC.userDBMainHIS = iniF.getIni("connection", "userDBMainHIS");
            iniC.passDBMainHIS = iniF.getIni("connection", "passDBMainHIS");
            iniC.portDBMainHIS = iniF.getIni("connection", "portDBMainHIS");

            iniC.hostDBPACs = iniF.getIni("connection", "hostDBPACs");
            iniC.nameDBPACs = iniF.getIni("connection", "nameDBPACs");
            iniC.userDBPACs = iniF.getIni("connection", "userDBPACs");
            iniC.passDBPACs = iniF.getIni("connection", "passDBPACs");
            iniC.portDBPACs = iniF.getIni("connection", "portDBPACs");

            iniC.hostDBLabOut = iniF.getIni("connection", "hostDBLabOut");
            iniC.nameDBLabOut = iniF.getIni("connection", "nameDBLabOut");
            iniC.userDBLabOut = iniF.getIni("connection", "userDBLabOut");
            iniC.passDBLabOut = iniF.getIni("connection", "passDBLabOut");
            iniC.portDBLabOut = iniF.getIni("connection", "portDBLabOut");

            iniC.hostFTP = iniF.getIni("ftp", "hostFTP");
            iniC.userFTP = iniF.getIni("ftp", "userFTP");
            iniC.passFTP = iniF.getIni("ftp", "passFTP");
            iniC.portFTP = iniF.getIni("ftp", "portFTP");
            iniC.folderFTP = iniF.getIni("ftp", "folderFTP");
            iniC.usePassiveFTP = iniF.getIni("ftp", "usePassiveFTP");
            iniC.ProxyHost = iniF.getIni("ftp", "ProxyHost");
            iniC.ProxyPort = iniF.getIni("ftp", "ProxyPort");
            iniC.ProxyProxyType = iniF.getIni("ftp", "ProxyProxyType");

            iniC.hostFTPLabOut = iniF.getIni("ftp", "hostFTPLabOut");
            iniC.userFTPLabOut = iniF.getIni("ftp", "userFTPLabOut");
            iniC.passFTPLabOut = iniF.getIni("ftp", "passFTPLabOut");
            iniC.portFTPLabOut = iniF.getIni("ftp", "portFTPLabOut");
            iniC.folderFTPLabOut = iniF.getIni("ftp", "folderFTPLabOut");
            iniC.usePassiveFTPLabOut = iniF.getIni("ftp", "usePassiveFTPLabOut");

            iniC.hostFTPLabOutMedica = iniF.getIni("ftp", "hostFTPLabOut_MADICA");
            iniC.userFTPLabOutMedica = iniF.getIni("ftp", "userFTPLabOut_MADICA");
            iniC.passFTPLabOutMedica = iniF.getIni("ftp", "passFTPLabOut_MADICA");
            iniC.portFTPLabOutMedica = iniF.getIni("ftp", "portFTPLabOut_MADICA");
            iniC.folderFTPLabOutMedica = iniF.getIni("ftp", "folderFTPLabOut_MADICA");
            iniC.usePassiveFTPLabOutMedica = iniF.getIni("ftp", "usePassiveFTPLabOut_MADICA");

            iniC.grdViewFontSize = iniF.getIni("app", "grdViewFontSize");
            iniC.grdViewFontName = iniF.getIni("app", "grdViewFontName");

            iniC.txtFocus = iniF.getIni("app", "txtFocus");
            iniC.grfRowColor = iniF.getIni("app", "grfRowColor");
            iniC.statusAppDonor = iniF.getIni("app", "statusAppDonor");
            iniC.themeApplication = iniF.getIni("app", "themeApplication");
            iniC.themeDonor = iniF.getIni("app", "themeDonor");
            iniC.printerSticker = iniF.getIni("app", "printerSticker");
            iniC.timerImgScanNew = iniF.getIni("app", "timerImgScanNew");
            iniC.pathImageScan = iniF.getIni("app", "pathImageScan");
            iniC.imggridscanwidth = iniF.getIni("app", "imggridscanwidth");
            iniC.hostname = iniF.getIni("app", "hostname");
            iniC.printerA4 = iniF.getIni("app", "printerA4");
            iniC.programLoad = iniF.getIni("app", "programLoad");
            iniC.labOutOpenFileDialog = iniF.getIni("app", "labOutOpenFileDialog");
            iniC.windows = iniF.getIni("app", "windows");
            iniC.grfScanWidth = iniF.getIni("app", "grfScanWidth");
            iniC.imgScanWidth = iniF.getIni("app", "imgScanWidth");
            iniC.pathScanStaffNote = iniF.getIni("app", "pathScanStaffNote");
            iniC.pathScreenCaptureUpload = iniF.getIni("app", "pathScreenCaptureUpload");
            iniC.pathIniFile = iniF.getIni("app", "pathIniFile");
            iniC.statusShowPrintDialog = iniF.getIni("app", "statusShowPrintDialog");
            iniC.txtSearchHnLenghtStart = iniF.getIni("app", "txtSearchHnLenghtStart");
            iniC.pathLabOutReceiveInnoTech = iniF.getIni("app", "pathLabOutReceiveInnoTech");
            iniC.pathLabOutBackupInnoTech = iniF.getIni("app", "pathLabOutBackupInnoTech");
            iniC.timerCheckLabOut = iniF.getIni("app", "timerCheckLabOut");
            iniC.pathTempScanAdd = iniF.getIni("app", "pathTempScanAdd");
            iniC.themeApp = iniF.getIni("app", "themeApp");
            iniC.station = iniF.getIni("app", "station");
            iniC.pathLabOutReceiveRIA = iniF.getIni("app", "pathLabOutReceiveRIA");
            iniC.pathLabOutBackupRIA = iniF.getIni("app", "pathLabOutBackupRIA");
            iniC.pathLabOutBackupRIAZipExtract = iniF.getIni("app", "pathLabOutBackupRIAZipExtract");
            iniC.pathLabOutBackupManual = iniF.getIni("app", "pathLabOutBackupManual");
            iniC.pathDownloadFile = iniF.getIni("app", "pathDownloadFile");
            iniC.pathScreenCaptureSend = iniF.getIni("app", "pathScreenCaptureSend");
            iniC.pacsServerIP = iniF.getIni("app", "pacsServerIP");
            iniC.pacsServerPort = iniF.getIni("app", "pacsServerPort");
            iniC.tabLabOutImageHeight = iniF.getIni("app", "tabLabOutImageHeight");
            iniC.tabLabOutImageWidth = iniF.getIni("app", "tabLabOutImageWidth");
            iniC.statusShowLabOutFrmLabOutReceiveView = iniF.getIni("app", "statusShowLabOutFrmLabOutReceiveView");

            iniC.themeApplication = iniC.themeApplication == null ? "Office2007Blue" : iniC.themeApplication.Equals("") ? "Office2007Blue" : iniC.themeApplication;
            iniC.timerImgScanNew = iniC.timerImgScanNew == null ? "2" : iniC.timerImgScanNew.Equals("") ? "0" : iniC.timerImgScanNew;
            iniC.pathImageScan = iniC.pathImageScan == null ? "d:\\images" : iniC.pathImageScan.Equals("") ? "d:\\images" : iniC.pathImageScan;
            iniC.imggridscanwidth = iniC.imggridscanwidth == null ? "380" : iniC.imggridscanwidth.Equals("") ? "380" : iniC.imggridscanwidth;
            iniC.themeApp = iniC.themeApp == null ? "Office2007Blue" : iniC.themeApp.Equals("") ? "Office2007Blue" : iniC.themeApp;
            iniC.pathScreenCaptureSend = iniC.pathScreenCaptureSend == null ? "C:\\capture" : iniC.pathScreenCaptureSend.Equals("") ? "C:\\capture" : iniC.pathScreenCaptureSend;

            //iniC.pathImgScanNew = iniC.pathImgScanNew == null ? "d:\\images" : iniC.pathImgScanNew.Equals("") ? "d:\\images" : iniC.pathImgScanNew;
            iniC.folderFTP = iniC.folderFTP == null ? "images_medical_record" : iniC.folderFTP.Equals("") ? "images_medical_record" : iniC.folderFTP;
            iniC.grdViewFontName = iniC.grdViewFontName.Equals("") ? "Microsoft Sans Serif" : iniC.grdViewFontName;
            iniC.hostname = iniC.hostname == null ? "โรงพยาบาล" : iniC.hostname.Equals("") ? "โรงพยาบาล" : iniC.hostname;
            iniC.usePassiveFTP = iniC.usePassiveFTP == null ? "false" : iniC.usePassiveFTP.Equals("") ? "false" : iniC.usePassiveFTP;
            iniC.usePassiveFTPLabOut = iniC.usePassiveFTPLabOut == null ? "false" : iniC.usePassiveFTPLabOut.Equals("") ? "false" : iniC.usePassiveFTPLabOut;
            iniC.labOutOpenFileDialog = iniC.labOutOpenFileDialog == null ? "" : iniC.labOutOpenFileDialog.Equals("") ? "" : iniC.labOutOpenFileDialog;
            iniC.windows = iniC.windows == null ? "" : iniC.windows.Equals("") ? "" : iniC.windows;
            iniC.imgScanWidth = iniC.imgScanWidth == null ? "380" : iniC.imgScanWidth.Equals("") ? "380" : iniC.imgScanWidth;
            iniC.pathScanStaffNote = iniC.pathScanStaffNote == null ? "172.25.10.5" : iniC.pathScanStaffNote.Equals("") ? "172.25.10.5" : iniC.pathScanStaffNote;
            iniC.ProxyProxyType = iniC.ProxyProxyType == null ? "0" : iniC.ProxyProxyType.Equals("") ? "0" : iniC.ProxyProxyType;
            iniC.ProxyPort = iniC.ProxyPort == null ? "0" : iniC.ProxyPort.Equals("") ? "0" : iniC.ProxyPort;
            iniC.pathIniFile = iniC.pathIniFile == null ? "" : iniC.pathIniFile.Equals("") ? "" : iniC.pathIniFile;
            iniC.statusShowPrintDialog = iniC.statusShowPrintDialog == null ? "0" : iniC.statusShowPrintDialog.Equals("") ? "0" : iniC.statusShowPrintDialog;
            iniC.timerCheckLabOut = iniC.timerCheckLabOut == null ? "0" : iniC.timerCheckLabOut.Equals("") ? "0" : iniC.timerCheckLabOut;
            iniC.station = iniC.station == null ? "0" : iniC.station.Equals("") ? "0" : iniC.station;
            iniC.pathDownloadFile = iniC.pathDownloadFile == null ? "" : iniC.pathDownloadFile.Equals("") ? "" : iniC.pathDownloadFile;
            iniC.pacsServerIP = iniC.pacsServerIP == null ? "" : iniC.pacsServerIP.Equals("") ? "" : iniC.pacsServerIP;
            iniC.pacsServerPort = iniC.pacsServerPort == null ? "" : iniC.pacsServerPort.Equals("") ? "" : iniC.pacsServerPort;
            iniC.tabLabOutImageHeight = iniC.tabLabOutImageHeight == null ? "842" : iniC.tabLabOutImageHeight.Equals("") ? "842" : iniC.tabLabOutImageHeight;
            iniC.tabLabOutImageWidth = iniC.tabLabOutImageWidth == null ? "595" : iniC.tabLabOutImageWidth.Equals("") ? "595" : iniC.tabLabOutImageWidth;
            iniC.statusShowLabOutFrmLabOutReceiveView = iniC.statusShowLabOutFrmLabOutReceiveView == null ? "windows10" : iniC.statusShowLabOutFrmLabOutReceiveView.Equals("") ? "windows10" : iniC.statusShowLabOutFrmLabOutReceiveView;

            int.TryParse(iniC.grdViewFontSize, out grdViewFontSize);
            int.TryParse(iniC.imggridscanwidth, out imggridscanwidth);
            Boolean.TryParse(iniC.usePassiveFTP, out ftpUsePassive);
            Boolean.TryParse(iniC.usePassiveFTPLabOut, out ftpUsePassiveLabOut);
            int.TryParse(iniC.grfScanWidth, out grfScanWidth);
            int.TryParse(iniC.timerCheckLabOut, out timerCheckLabOut);

            int.TryParse(iniC.imggridscanwidth, out imggridscanwidth);
            int.TryParse(iniC.imgScanWidth, out imgScanWidth);
            int.TryParse(iniC.txtSearchHnLenghtStart, out txtSearchHnLenghtStart);
            int.TryParse(iniC.tabLabOutImageHeight, out tabLabOutImageHeight);
            int.TryParse(iniC.tabLabOutImageWidth, out tabLabOutImageWidth);
        }
        public void setC1Combo(C1ComboBox c, String data)
        {
            if (c.Items.Count == 0) return;
            c.SelectedIndex = c.SelectedItem == null ? 0 : c.SelectedIndex;
            c.SelectedIndex = 0;
            foreach (ComboBoxItem item in c.Items)
            {
                if (item.Value.Equals(data))
                {
                    c.SelectedItem = item;
                    break;
                }
            }
        }
        public Image RotateImage(Image img)
        {
            var bmp = new Bitmap(img);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.White);
                gfx.DrawImage(img, 0, 0, img.Width, img.Height);
            }

            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            //bmp.Dispose();
            return bmp;
        }
        public Stream ToStream(Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
        public String datetoShow(Object dt)
        {
            DateTime dt1 = new DateTime();
            //MySqlDateTime dtm = new MySqlDateTime();
            String re = "";
            if (iniC.windows.Equals("windowsxp"))
            {
                if (DateTime.TryParse(dt.ToString(), out dt1))
                {
                    if (dt1.Year > 2500)
                    {
                        re = dt1.ToString("dd-MM") + "-" + (dt1.Year - 543).ToString();
                    }
                    else if (dt1.Year < 1500)
                    {
                        re = dt1.ToString("dd-MM")+ "-"+ (dt1.Year + 543).ToString();
                    }
                    else
                    {
                        re = dt1.ToString("MM-dd") + "-" + dt1.Year.ToString();
                    }
                }
                //re = dt.ToString();
            }
            else
            {
                if (dt != null)
                {
                    if (DateTime.TryParse(dt.ToString(), out dt1))
                    {
                        re = dt1.ToString("dd-MM-yyyy");
                    }
                }
            }
            return re;
        }
        public String datetoShow1(String dt)
        {
            DateTime dt1 = new DateTime();
            //MySqlDateTime dtm = new MySqlDateTime();
            String re = "", year1 = "", mm = "", dd = "";
            if (iniC.windows.Equals("windowsxp"))
            {
                if (dt.Length >= 10)
                {
                    year1 = dt.Substring(0, 4);
                    mm = dt.Substring(5, 2);
                    dd = dt.Substring(8, 2);
                    re = dd + "-" + mm + "-" + year1;
                }
                else
                {
                    re = "";
                }
                //re = dt.ToString();
            }
            else
            {
                if (dt != null)
                {
                    if (DateTime.TryParse(dt.ToString(), out dt1))
                    {
                        re = dt1.ToString("dd-MM-yyyy");
                    }
                }
            }
            return re;
        }
        public String datetoDB(String dt)
        {
            DateTime dt1 = new DateTime();
            String re = "", year1="",mm="",dd="";
            int year = 0, mon=0, day=0;
            //new LogWriter("d", "datetoDB 01" );
            if (iniC.windows.Equals("windowsxp"))
            {
                //new LogWriter("d", "datetoDB 02 iniC.windowsxp ");
                //if (DateTime.TryParse(dt, out dt1))
                //{
                //    //new LogWriter("d", "datetoDB 02 iniC.windowsxp DateTime.TryParse(dt, out dt1) true dt1.Year"+ dt1.Year);
                //    if (dt1.Year > 2500)
                //    {
                //        re = (dt1.Year - 543).ToString() + "-" + dt1.ToString("MM-dd");
                //    }
                //    else if (dt1.Year < 1500)
                //    {
                //        re = (dt1.Year + 543).ToString() + "-" + dt1.ToString("MM-dd");
                //    }
                //    else
                //    {
                //        re = dt1.Year.ToString() + "-" + dt1.ToString("MM-dd");
                //    }                    
                //}
                //else
                //{
                    //new LogWriter("d", "datetoDB 02 iniC.windowsxp DateTime.TryParse(dt, out dt1) false ");
                    if (dt.Length >= 10)
                    {
                        year1 = dt.Substring(6, 4);
                        mm = dt.Substring(3, 2);
                        dd = dt.Substring(0, 2);
                        int.TryParse(mm, out mon);
                        if (mon > 12)
                        {
                            mm = dt.Substring(0, 2);
                            dd = dt.Substring(3, 2);
                            int.TryParse(mm, out mon);
                        }
                        //new LogWriter("d", "datetoDB year1 " + year1);
                        if (int.TryParse(year1, out year))
                        {
                            //new LogWriter("d", "datetoDB year1 int.TryParse(year1, out year) " + year);
                            if (year <= 1500)
                            {
                                year = year + 543;
                            }
                            else if (year >= 2500)
                            {
                                year = year - 543;
                            }
                        }
                        else
                        {
                            //new LogWriter("d", "datetoDB year1 else int.TryParse(year1, out year) ");
                        }
                        re = year.ToString() + "-" + mm + "-" + dd;
                    }
                    else
                    {

                    }
                //}
                //re = dt1.ToString("yyyy-MM-dd");
            }
            else
            {
                //new LogWriter("d", "datetoDB 03 iniC.windows 10 ");
                if (dt != null)
                {
                    if (!dt.Equals(""))
                    {
                        // Thread แบบนี้ ทำให้ โปรแกรม ที่ไปลงที Xtrim ไม่เอา date ผิด
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us")
                        {
                            DateTimeFormat =
                            {
                                DateSeparator = "-"
                            }
                        };
                        if (DateTime.TryParse(dt, out dt1))
                        {
                            
                            if (dt1.Year > 2500)
                            {
                                re = (dt1.Year -543).ToString() + "-" + dt1.ToString("MM-dd");
                            }
                            else
                            {
                                re = dt1.Year.ToString() + "-" + dt1.ToString("MM-dd");
                            }
                        }
                        else
                        {
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH")
                            {
                                DateTimeFormat =
                            {
                                DateSeparator = "-"
                            }
                            };
                            if (DateTime.TryParse(dt, out dt1))
                            {
                                re = dt1.ToString("yyyy-MM-dd");
                            }
                        }
                        //dt1 = DateTime.Parse(dt.ToString());

                    }
                }
            }
            
            return re;
        }
        public String getMonth(String monthId)
        {
            if (monthId == "01")
            {
                return "มกราคม";
            }
            else if (monthId == "02")
            {
                return "กุมภาพันธ์";
            }
            else if (monthId == "03")
            {
                return "มีนาคม";
            }
            else if (monthId == "04")
            {
                return "เมษายน";
            }
            else if (monthId == "05")
            {
                return "พฤษภาคม";
            }
            else if (monthId == "06")
            {
                return "มิถุนายน";
            }
            else if (monthId == "07")
            {
                return "กรกฎาคม";
            }
            else if (monthId == "08")
            {
                return "สิงหาคม";
            }
            else if (monthId == "09")
            {
                return "กันยายน";
            }
            else if (monthId == "10")
            {
                return "ตุลาคม";
            }
            else if (monthId == "11")
            {
                return "พฤศจิกายน";
            }
            else if (monthId == "12")
            {
                return "ธันวาคม";
            }
            else
            {
                return "";
            }
        }
        public ComboBox setCboMonth(ComboBox c)
        {
            c.Items.Clear();
            var items = new[]{
                new{Text = "มกราคม", Value="01"},
                new{Text = "กุมภาพันธ์", Value="02"},
                new{Text = "มีนาคม", Value="03"},
                new{Text = "เมษายน", Value="04"},
                new{Text = "พฤษภาคม", Value="05"},
                new{Text = "มิถุนายน", Value="06"},
                new{Text = "กรกฎาคม", Value="07"},
                new{Text = "สิงหาคม", Value="08"},
                new{Text = "กันยายน", Value="09"},
                new{Text = "ตุลาคม", Value="10"},
                new{Text = "พฤศจิกายน", Value="11"},
                new{Text = "ธันวาคม", Value="12"}
            };
            c.DataSource = items;
            c.DisplayMember = "Text";

            c.ValueMember = "Value";
            c.SelectedIndex = c.FindStringExact(getMonth(System.DateTime.Now.Month.ToString("00")));
            return c;
        }
        public void setCboMOdality(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectAll();
            int i = 0;
            
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CR";
            item.Text = "Computed Radiography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CT";
            item.Text = "Computed Tomography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "MR";
            item.Text = "Magnetic Resonance";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "NR";
            item.Text = "Nuclear Medicine";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "UA";
            item.Text = "Ultrasound";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "OT";
            item.Text = "Other";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "BI";
            item.Text = "Biomagnetic Imaging";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CD";
            item.Text = "Color Flow Doppler";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "DD";
            item.Text = "Duplex Doppler";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "DG";
            item.Text = "Diaphanography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "ES";
            item.Text = "Endoscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "LS";
            item.Text = "Laser Surface Scan";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PT";
            item.Text = "Positron Emission Tomography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RG";
            item.Text = "Radiographic Imaging";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "ST";
            item.Text = "Single-photon Emission Computed Tomography (SPECT)";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "TG";
            item.Text = "Thermography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "XA";
            item.Text = "X-Ray Angiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RF";
            item.Text = "Radio Fluoroscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTIMAGE";
            item.Text = "Radiotherapy Image";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTDOSE";
            item.Text = "Radiotherapy Dose";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTSTRUCT";
            item.Text = "RadioTherapy Structure Set";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTPLAN";
            item.Text = "Radiotherapy Plan";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTRECORD";
            item.Text = "Radiotherapy Treatment Record";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "HC";
            item.Text = "Hard Copy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "DX";
            item.Text = "Digital Radiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "MG";
            item.Text = "Mammography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "IO";
            item.Text = "Intra-oral Radiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PX";
            item.Text = "Panoramic X-Ray";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "GM";
            item.Text = "General Microscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SM";
            item.Text = "Slide Microscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "XC";
            item.Text = "External-camera Photography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PR";
            item.Text = "Presentation State";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "AU";
            item.Text = "Audio";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "ECG";
            item.Text = "Electrocardiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "EPS";
            item.Text = "Cardiac Electrophysiology";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "HD";
            item.Text = "Hemodynamic Waveform";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SR";
            item.Text = "Structured Report Document";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "IVUS";
            item.Text = "Intravascular Ultrasound";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "OP";
            item.Text = "Ophthalmic Photography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SMR";
            item.Text = "Stereometric Relationship";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SC";
            item.Text = "Secondary Capture";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SD";
            item.Text = "Scanned Document";
            c.Items.Add(item);
        }
        public ComboBox setCboYear(ComboBox c)
        {
            c.Items.Clear();
            c.Items.Add(System.DateTime.Now.Year + 543);
            c.Items.Add(System.DateTime.Now.Year + 543 - 1);
            c.Items.Add(System.DateTime.Now.Year + 543 - 2);
            c.SelectedIndex = c.FindStringExact(String.Concat(System.DateTime.Now.Year + 543));
            return c;
        }
        public ComboBox setCboPeriod(ComboBox c)
        {
            c.Items.Clear();
            c.Items.Add(1);
            c.Items.Add(2);

            c.SelectedIndex = 0;
            return c;
        }
        public String shortPaidName(String name)
        {
            if (name == "ประกันสังคม (บ.1)")
            {
                return "ปกส(บ.1)";
            }
            else if (name == "ประกันสังคม (บ.2)")
            {
                return "ปกส(บ.2)";
            }
            else if (name == "ประกันสังคม (บ.5)")
            {
                return "ปกส(บ.5)";
            }
            else if (name == "ประกันสังคมอิสระ (บ.1)")
            {
                return "ปกต(บ.1)";
            }
            else if (name == "ประกันสังคมอิสระ (บ.5)")
            {
                return "ปกต(บ.5)";
            }
            else if (name == "ตรวจสุขภาพ (เงินสด)")
            {
                return "ตส(เงินสด)";
            }
            else if (name == "ตรวจสุขภาพ (บริษัท)")
            {
                return "ตส(บริษัท)";
            }
            else if (name == "ตรวจสุขภาพ (PACKAGE)")
            {
                return "ตส(PACKAGE)";
            }
            else if (name == "ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ")
            {
                return "ลูกหนี้(ปากน้ำ)";
            }
            else if (name == "ลูกหนี้บางนา 1")
            {
                return "ลูกหนี้(บ.1)";
            }
            else if (name == "บริษัทประกัน")
            {
                return "บ.ประกัน";
            }
            else if (name == "ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปู่เจ้า")
            {
                return "ลูกหนี้(ปู่เจ้า)";
            }
            else
            {
                return name;
            }
        }
        public String dateDBtoShowShort(String dt)
        {
            if (dt != "")
            {
                if (Int16.Parse(dt.Substring(0, 4)) < 1957)
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543);
                }
                else
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543).Substring(2);
                }

            }
            else
            {
                return dt;
            }
        }
        public String FormatTime(String t)
        {
            String aa = "";
            aa = "0000" + t;
            if (aa.Length >= 4)
            {
                aa = aa.Substring(aa.Length - 4, 2) + ":" + aa.Substring(aa.Length - 2);
            }
            return aa;
        }
        public String dateDBtoShowShort1(String dt)
        {
            if (dt != "")
            {
                if (Int16.Parse(dt.Substring(0, 4)) < 1957)
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543);
                }
                else
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543).Substring(2);
                }

            }
            else
            {
                return dt;
            }
        }
        public Size MeasureString(Control c)
        {
            return TextRenderer.MeasureText(c.Text, c.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.SingleLine | TextFormatFlags.NoClipping | TextFormatFlags.PreserveGraphicsClipping);
        }
        
        public Bitmap ResizeImage(Image image, int width, int height)
        {            
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public Bitmap ResizeImagetoA4(Image image)
        {
            /*
             * 
             */
            float tgtWidthMM = 210;  //A4 paper size
            float tgtHeightMM = 297;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            float srcWidthPx = image.Width;
            float srcHeightPx = image.Height;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            var destRect = new Rectangle(0, 0, 210, 297);
            var destImage = new Bitmap(210, 297);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public Bitmap ResizeImagetoA4Lan(Image image)
        {
            /*
             * 
             */
            Image aaa = RotateImage(image);
            float tgtWidthMM = 297;  //A4 paper size
            float tgtHeightMM = 210;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            //float srcWidthPx = aaa.Width;
            //float srcHeightPx = aaa.Height;
            float srcWidthPx = aaa.Height;
            float srcHeightPx = aaa.Width;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            var destRect = new Rectangle(0, 0, 210, 297);
            var destImage = new Bitmap(210, 297);
            destImage.SetResolution(aaa.HorizontalResolution, aaa.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(aaa, destRect, 0, 0, aaa.Width, aaa.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public bool ExploreFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }
            //Clean up file path so it can be navigated OK
            filePath = System.IO.Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }
        public void serverPACsInfinittStart(TcpListener tcpServerListener, String ipaddress, String port)
        {
            try
            {
                IPAddress ipad = IPAddress.Parse(ipaddress);
                tcpServerListener = new TcpListener(ipad, int.Parse(port));

                tcpServerListener.Start();

                Socket socket = tcpServerListener.AcceptSocket();
                byte[] b = new byte[100];
                int k = socket.Receive(b);

            }
            catch(Exception ex)
            {

            }
        }
        public PACsMSH genPAsMSH(String reqdept, String MessageType)
        {
            PACsMSH msh = new PACsMSH();
            msh.FieldSeparator = "";
            msh.EncodingCharacters = "^~\\&";
            msh.SendingApplication = "HIS";
            msh.SendingFacility = reqdept;
            msh.ReceivingApplication = "ThaiGL";
            msh.ReceivingFacility = "";
            msh.DateTimeOfMessage = DateTime.Now.Year+DateTime.Now.ToString("MMddHHmmss");
            msh.Security = "";
            //msh.MessageType = "ADT^A08";
            msh.MessageType = MessageType;
            msh.MessageControlID = "331950";
            msh.ProcessingID = "P";
            msh.VersionID = "2.3";
            msh.SequenceNumber = "";
            msh.ContinuationPointer = "";
            msh.AcceptAcknowledgementType = "";
            msh.ApplicationAcknowledgementType = "";
            msh.CountryCode = "";
            msh.CharacterSet = "";
            msh.PrincipalLanguageOfMessage = "";
            msh.AlternateCharacterSetHandlingScheme = "UNICODE UTF-8";
            
            return msh;
        }
        public PACsEVN genPACsEVN()
        {
            PACsEVN evn = new PACsEVN();
            evn.EventTypeCode = "";
            evn.RecordedDateTime = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            evn.DateTimePlannedEvent = "";
            evn.EventReasonCode = "";
            evn.OperatorID = "";
            evn.EventOccurred = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");

            return evn;
        }
        public PACsPID genPACsPID(String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation)
        {
            PACsPID pid = new PACsPID();
            pid = new PACsPID();
            pid.SetID = "";
            pid.PatientID = "";
            pid.PatientIdentifierList = hn;
            pid.AlternatePatientID = "";
            pid.PatientName = pttprefix+"^"+ pttfirstname+"^^^"+ pttlastame;
            pid.MothersMaidenName = "";
            pid.DateTimeOfBirth = dob;
            pid.Sex = sex;
            pid.PatientAlias = "";
            pid.Race = "";
            pid.PatientAddress = "";
            pid.CountyCode = "";
            pid.HomePhoneNumber = "";
            pid.BusinessPhoneNumber = "";
            pid.PrimaryLanguage = "";
            pid.MaritalStatus = "";
            pid.Religion = "";
            pid.PatientAccountNumber = "";
            pid.SSNNumber = "";
            pid.DriversLicenseNumber = "";
            pid.MothersIdentifier = "";
            pid.EthnicGroup = "";
            pid.BirthPlace = "";
            pid.MultipleBirthIndicator = "";
            pid.BirthOrder = "";
            pid.Citizenship = "";
            pid.VeteransMilitaryStatus = "";
            pid.Nationality = nation;
            pid.PatientDeathDateAndTime = "";
            pid.PatientDeathIndicator = "";

            return pid;
        }
        public PACsPV1 genPV1(String opdtype, String depcode, String depname)
        {
            PACsPV1 pv1 = new PACsPV1();
            pv1.SetID = "";
            pv1.PatientClass = opdtype;
            pv1.AssignedPatientLocation = depcode+"^"+ depname;
            pv1.AdmissionType = "";
            pv1.PreadmitNumber = "";
            pv1.PriorPatientLocation = "";
            pv1.AttendingDoctor = "";
            pv1.ReferringDoctor = "";
            pv1.ConsultingDoctor = "";
            pv1.HospitalService = "";
            pv1.TemporaryLocation = "";
            pv1.PreadmitTestIndicator = "";
            pv1.ReadmissionIndicator = "";
            pv1.AdmitSource = "";
            pv1.AmbulatoryStatus = "";
            pv1.VIPIndicator = "";
            pv1.AdmittingDoctor = "";
            pv1.PatientType = "";
            pv1.VisitNumber = "";
            pv1.FinancialClass = "";
            pv1.ChargePriceIndicator = "";
            pv1.CourtesyCode = "";
            pv1.CreditRating = "";
            pv1.ContractCode = "";
            pv1.ContractEffectiveDate = "";
            pv1.ContractAmount = "";
            pv1.ContractPeriod = "";
            pv1.InterestCode = "";
            pv1.TransferToBadDebtCode = "";
            pv1.TransferToBadDebtDate = "";
            pv1.BadDebtAgencyCode = "";
            pv1.BadDebtTransferAmount = "";
            pv1.BadDebtRecoveryAmount = "";
            pv1.DeleteAccountIndicator = "";
            pv1.DeleteAccountDate = "";
            pv1.DischargeDisposition = "";
            pv1.DischargedToLocation = "";
            pv1.DietType = "";
            pv1.ServicingFacility = "";
            pv1.BedStatus = "";
            pv1.AccountStatus = "";
            pv1.PendingLocation = "";
            pv1.PriorTemporaryLocation = "";
            pv1.AdmitDateTime = "";
            pv1.DischargeDateTime = "";
            pv1.CurrentPatientBalance = "";
            pv1.TotalCharges = "";
            pv1.TotalAdjustments = "";
            pv1.TotalPayments = "";
            pv1.AlternateVisitID = "";
            pv1.VisitIndicator = "";
            pv1.OtherHealthcareProvider = "";

            return pv1;
        }
        public String PACsADT(PACsMSH msh, PACsEVN evn, PACsPID pid, PACsPV1 pv1)
        {
            String txt = "", MSH="", separate="|", EVN="", PID="", PV1="";
            txt = "\x0b";

            MSH = "MSH" + separate + msh.EncodingCharacters + separate + msh.SendingApplication + separate + msh.SendingFacility + separate + msh.ReceivingApplication
                + separate + msh.ReceivingFacility + separate + msh.DateTimeOfMessage + separate + msh.Security + separate + msh.MessageType
                + separate + msh.MessageControlID + separate + msh.ProcessingID + separate + msh.VersionID + separate + msh.SequenceNumber
                + separate + msh.ContinuationPointer + separate + msh.AcceptAcknowledgementType + separate + msh.ApplicationAcknowledgementType + separate + msh.CountryCode
                + separate + msh.CharacterSet + separate + msh.PrincipalLanguageOfMessage + separate + msh.AlternateCharacterSetHandlingScheme;

            EVN = "EVN" + separate + evn.EventTypeCode + separate + evn.RecordedDateTime + separate + evn.DateTimePlannedEvent + separate + evn.EventReasonCode
                + separate + evn.OperatorID + separate + evn.EventOccurred;

            PID = "PID" + separate + pid.SetID + separate + pid.PatientID + separate + pid.PatientIdentifierList + separate + pid.AlternatePatientID
                + pid.PatientName + separate + pid.MothersMaidenName + separate + pid.DateTimeOfBirth + separate + pid.Sex
                + pid.PatientAlias + separate + pid.Race + separate + pid.PatientAddress + separate + pid.CountyCode
                + pid.HomePhoneNumber + separate + pid.BusinessPhoneNumber + separate + pid.PrimaryLanguage + separate + pid.MaritalStatus
                + pid.Religion + separate + pid.PatientAccountNumber + separate + pid.SSNNumber + separate + pid.DriversLicenseNumber
                + pid.MothersIdentifier + separate + pid.EthnicGroup + separate + pid.BirthPlace + separate + pid.MultipleBirthIndicator
                + pid.BirthOrder + separate + pid.Citizenship + separate + pid.VeteransMilitaryStatus + separate + pid.Nationality
                + pid.PatientDeathDateAndTime + separate + pid.PatientDeathIndicator;

            PV1 = "PV1" + separate + pv1.SetID + separate + pv1.PatientClass + separate + pv1.AssignedPatientLocation + separate + pv1.AdmissionType
                + separate + pv1.PreadmitNumber + separate + pv1.PriorPatientLocation + separate + pv1.AttendingDoctor + separate + pv1.ReferringDoctor
                + separate + pv1.ConsultingDoctor + separate + pv1.HospitalService + separate + pv1.TemporaryLocation + separate + pv1.PreadmitTestIndicator
                + separate + pv1.ReadmissionIndicator + separate + pv1.AdmitSource + separate + pv1.AmbulatoryStatus + separate + pv1.VIPIndicator
                + separate + pv1.AdmittingDoctor + separate + pv1.PatientType + separate + pv1.VisitNumber + separate + pv1.FinancialClass
                + separate + pv1.ChargePriceIndicator + separate + pv1.CourtesyCode + separate + pv1.CreditRating + separate + pv1.ContractCode
                + separate + pv1.ContractEffectiveDate + separate + pv1.ContractAmount + separate + pv1.ContractPeriod + separate + pv1.InterestCode
                + separate + pv1.TransferToBadDebtCode + separate + pv1.TransferToBadDebtDate + separate + pv1.BadDebtAgencyCode + separate + pv1.BadDebtTransferAmount
                + separate + pv1.BadDebtRecoveryAmount + separate + pv1.DeleteAccountIndicator + separate + pv1.DeleteAccountDate + separate + pv1.DischargeDisposition
                + separate + pv1.DischargedToLocation + separate + pv1.DietType + separate + pv1.ServicingFacility + separate + pv1.BedStatus
                + separate + pv1.AccountStatus + separate + pv1.PendingLocation + separate + pv1.PriorTemporaryLocation + separate + pv1.AdmitDateTime
                + separate + pv1.DischargeDateTime + separate + pv1.CurrentPatientBalance + separate + pv1.TotalCharges + separate + pv1.TotalAdjustments
                + separate + pv1.TotalPayments + separate + pv1.AlternateVisitID + separate + pv1.VisitIndicator + separate + pv1.OtherHealthcareProvider;
            txt = txt + MSH + Environment.NewLine + EVN + Environment.NewLine + PID + Environment.NewLine + PV1;
            return txt;
        }
        public String genADT(String reqdept, String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation, String opdtype, String depcode, String depname)
        {
            String txt = "";
            PACsMSH msh = new PACsMSH();
            PACsPID pid = new PACsPID();
            PACsEVN evn = new PACsEVN();
            PACsPV1 pv1 = new PACsPV1();
            msh = genPAsMSH(reqdept, "ADT^A08");
            pid = genPACsPID(hn, pttprefix, pttfirstname, pttlastame, dob, sex, nation);
            pv1 = genPV1(opdtype, depcode, depname);
            evn = genPACsEVN();

            txt = PACsADT(msh, evn, pid, pv1);
            return txt;
        }
        public PACsORC genORC(String xrayyear, String reqno, String xraycode, String userid, String username)
        {
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            PACsORC orc = new PACsORC();
            orc.OrderControl = "NW";
            orc.PlacerOrderNumber = xrayyear+ reqno+ code;
            orc.FillerOrderNumber = "";
            orc.PlacerGroupNumber = "";
            orc.OrderStatus = "SC";
            orc.ResponseFlag = "";
            orc.QuantityTiming = "";
            orc.Parent = "";

            orc.DateTimeOfTransaction = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            orc.EnteredBy = userid+"^"+ username;
            orc.VerifiedBy = "";
            orc.OrderingProvider = userid + "^" + username;
            orc.EnterersLocation = "";
            orc.CallBackPhoneNumber = "";
            orc.OrderEffectiveDateTime = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            orc.OrderControlCodeReason = "";

            orc.EnteringOrganization = "";
            orc.EnteringDevice = "";
            orc.ActionBy = "";
            orc.AdvancedBeneficiaryNoticeCode = "";
            orc.OrderingFacilityName = "";
            orc.OrderingFacilityAddress = "";
            orc.OrderingFacilityPhoneNumber = "";
            orc.OrderingProviderAddress = "";
            return orc;
        }
        public PACsOBR genOBR(String xrayyear, String reqno, String xraycode,String xrayname, String xraytype, String userid, String username)
        {
            PACsOBR obr = new PACsOBR();
            obr.SetID = "";
            obr.PlacerOrderNumber = "";
            obr.FillerOrderNumber = "";
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            obr.UniversalServiceID = code + "^"+ xrayname;
            obr.Priority = "";
            obr.RequestedDateTime = "";
            obr.ObservationDateTime = "";
            obr.ObservationEndDateTime = "";

            obr.CollectionVolume = "";
            obr.CollectorIdentifier = "";
            obr.SpecimenActionCode = "";
            obr.DangerCode = "";
            obr.RelevantClinicalInfo = "";
            obr.SpecimenReceivedDateTime = "";
            obr.SpecimenSource = "";
            obr.OrderingProvider = userid + "^" + username;

            obr.OrderCallbackPhoneNumber = "";
            obr.PlacerField1 = xrayyear + reqno + code;
            obr.PlacerField2 = "RP"+ xrayyear + reqno + code;
            obr.FillerField1 = "SS" + xrayyear + reqno + code;
            obr.FillerField2 = "";
            obr.ResultsRptStatusChngDateTime = "";
            obr.ChargeToPractice = "";
            obr.DiagnosticServSectID = "CR";
            obr.ResultStatus = xraytype;
            obr.ParentResult = "";
            obr.QuantityTiming = "";
            obr.ResultCopiesTo = "";

            obr.ParentNumber = "";
            obr.TransportationMode = "";
            obr.ReasonForStudy = "";
            obr.PrincipalResultInterpreter = "";
            obr.AssistantResultInterpreter = "";
            obr.Technician = "";
            obr.Transcriptionist = "";
            obr.ScheduledDateTime = "";
            obr.NumberOfSampleContainers = "";
            obr.TransportLogisticsOfCollectedSample = "";
            obr.CollectorsComment = "";
            obr.TransportArrangementResponsibility = "";
            obr.TransportArranged = "";
            obr.EscortRequired = "A";
            obr.PlannedPatientTransportComment = "";
            obr.ProcedureCode = "";
            obr.ProcedureCodeModifier = code + "^" + xrayname;
            return obr;
        }
        public PACsZDS genZDS(String xrayyear, String reqno, String xraycode, String modality)
        {
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            PACsZDS zds = new PACsZDS();
            zds.ZDS_Field1 = "1.2.410.2000010.66.101." + xrayyear + reqno + code;
            zds.ZDS_Field2 ="";
            zds.ZDS_Field3 ="";
            zds.ZDS_Field4 ="";
            zds.ZDS_Field5 ="";
            zds.ZDS_Field6 ="";
            zds.ZDS_Field7 ="";
            zds.ZDS_Field8 ="";
            zds.ZDS_Field9 ="";
            zds.ZDS_Field10 = "";

            return zds;
        }
        public String PACsORM(PACsMSH msh, PACsPID pid, PACsPV1 pv1, PACsORC orc, PACsOBR obr, PACsZDS zds, String comment)
        {
            String txt = "", MSH = "", separate = "|", PID = "", PV1 = "", ORC = "", OBR = "", ZDS = "", NTE="";
            txt = "\x0b";

            MSH = "MSH" + separate + msh.EncodingCharacters + separate + msh.SendingApplication + separate + msh.SendingFacility + separate + msh.ReceivingApplication
                + separate + msh.ReceivingFacility + separate + msh.DateTimeOfMessage + separate + msh.Security + separate + msh.MessageType
                + separate + msh.MessageControlID + separate + msh.ProcessingID + separate + msh.VersionID + separate + msh.SequenceNumber
                + separate + msh.ContinuationPointer + separate + msh.AcceptAcknowledgementType + separate + msh.ApplicationAcknowledgementType + separate + msh.CountryCode
                + separate + msh.CharacterSet + separate + msh.PrincipalLanguageOfMessage + separate + msh.AlternateCharacterSetHandlingScheme;
            
            PID = "PID" + separate + pid.SetID + separate + pid.PatientID + separate + pid.PatientIdentifierList + separate + pid.AlternatePatientID
                + pid.PatientName + separate + pid.MothersMaidenName + separate + pid.DateTimeOfBirth + separate + pid.Sex
                + pid.PatientAlias + separate + pid.Race + separate + pid.PatientAddress + separate + pid.CountyCode
                + pid.HomePhoneNumber + separate + pid.BusinessPhoneNumber + separate + pid.PrimaryLanguage + separate + pid.MaritalStatus
                + pid.Religion + separate + pid.PatientAccountNumber + separate + pid.SSNNumber + separate + pid.DriversLicenseNumber
                + pid.MothersIdentifier + separate + pid.EthnicGroup + separate + pid.BirthPlace + separate + pid.MultipleBirthIndicator
                + pid.BirthOrder + separate + pid.Citizenship + separate + pid.VeteransMilitaryStatus + separate + pid.Nationality
                + pid.PatientDeathDateAndTime + separate + pid.PatientDeathIndicator;

            PV1 = "PV1" + separate + pv1.SetID + separate + pv1.PatientClass + separate + pv1.AssignedPatientLocation + separate + pv1.AdmissionType
                + separate + pv1.PreadmitNumber + separate + pv1.PriorPatientLocation + separate + pv1.AttendingDoctor + separate + pv1.ReferringDoctor
                + separate + pv1.ConsultingDoctor + separate + pv1.HospitalService + separate + pv1.TemporaryLocation + separate + pv1.PreadmitTestIndicator
                + separate + pv1.ReadmissionIndicator + separate + pv1.AdmitSource + separate + pv1.AmbulatoryStatus + separate + pv1.VIPIndicator
                + separate + pv1.AdmittingDoctor + separate + pv1.PatientType + separate + pv1.VisitNumber + separate + pv1.FinancialClass
                + separate + pv1.ChargePriceIndicator + separate + pv1.CourtesyCode + separate + pv1.CreditRating + separate + pv1.ContractCode
                + separate + pv1.ContractEffectiveDate + separate + pv1.ContractAmount + separate + pv1.ContractPeriod + separate + pv1.InterestCode
                + separate + pv1.TransferToBadDebtCode + separate + pv1.TransferToBadDebtDate + separate + pv1.BadDebtAgencyCode + separate + pv1.BadDebtTransferAmount
                + separate + pv1.BadDebtRecoveryAmount + separate + pv1.DeleteAccountIndicator + separate + pv1.DeleteAccountDate + separate + pv1.DischargeDisposition
                + separate + pv1.DischargedToLocation + separate + pv1.DietType + separate + pv1.ServicingFacility + separate + pv1.BedStatus
                + separate + pv1.AccountStatus + separate + pv1.PendingLocation + separate + pv1.PriorTemporaryLocation + separate + pv1.AdmitDateTime
                + separate + pv1.DischargeDateTime + separate + pv1.CurrentPatientBalance + separate + pv1.TotalCharges + separate + pv1.TotalAdjustments
                + separate + pv1.TotalPayments + separate + pv1.AlternateVisitID + separate + pv1.VisitIndicator + separate + pv1.OtherHealthcareProvider;

            ORC = "ORC" + separate + orc.OrderControl + separate + orc.PlacerOrderNumber + separate + orc.FillerOrderNumber + separate + orc.PlacerGroupNumber + separate
                + orc.OrderStatus + separate + orc.ResponseFlag + separate + orc.QuantityTiming + separate + orc.Parent + separate
                + orc.DateTimeOfTransaction + separate + orc.EnteredBy + separate + orc.VerifiedBy + separate + orc.OrderingProvider + separate
                + orc.EnterersLocation + separate + orc.CallBackPhoneNumber + separate + orc.OrderEffectiveDateTime + separate + orc.OrderControlCodeReason + separate
                + orc.EnteringOrganization + separate + orc.EnteringDevice + separate + orc.ActionBy + separate + orc.AdvancedBeneficiaryNoticeCode + separate
                + orc.OrderingFacilityName + separate + orc.OrderingFacilityAddress + separate + orc.OrderingFacilityPhoneNumber + separate + orc.OrderingProviderAddress;

            OBR = "OBR" + separate + obr.SetID + separate + obr.PlacerOrderNumber + separate + obr.FillerOrderNumber + separate + obr.UniversalServiceID + separate
                + obr.Priority + separate + obr.RequestedDateTime + separate + obr.ObservationDateTime + separate + obr.ObservationEndDateTime + separate
                + obr.CollectionVolume + separate + obr.CollectorIdentifier + separate + obr.SpecimenActionCode + separate + obr.DangerCode + separate
                + obr.RelevantClinicalInfo + separate + obr.SpecimenReceivedDateTime + separate + obr.SpecimenSource + separate + obr.OrderingProvider + separate
                + obr.OrderCallbackPhoneNumber + separate + obr.PlacerField1 + separate + obr.PlacerField2 + separate + obr.FillerField1 + separate
                + obr.FillerField2 + separate + obr.ResultsRptStatusChngDateTime + separate + obr.ChargeToPractice + separate + obr.DiagnosticServSectID + separate
                + obr.ResultStatus + separate + obr.ParentResult + separate + obr.QuantityTiming + separate + obr.ResultCopiesTo + separate
                + obr.ParentNumber + separate + obr.TransportationMode + separate + obr.ReasonForStudy + separate + obr.PrincipalResultInterpreter + separate
                + obr.AssistantResultInterpreter + separate + obr.Technician + separate + obr.Transcriptionist + separate + obr.ScheduledDateTime + separate
                + obr.NumberOfSampleContainers + separate + obr.TransportLogisticsOfCollectedSample + separate + obr.CollectorsComment + separate + obr.TransportArrangementResponsibility + separate
                + obr.TransportArranged + separate + obr.EscortRequired + separate + obr.PlannedPatientTransportComment + separate + obr.ProcedureCode
                + obr.ProcedureCodeModifier;

            ZDS = "ZDS" + separate + zds.ZDS_Field1 + separate + zds.ZDS_Field2 + separate + zds.ZDS_Field3 + separate + zds.ZDS_Field4 + separate
                + zds.ZDS_Field5 + separate + zds.ZDS_Field6 + separate + zds.ZDS_Field7 + separate + zds.ZDS_Field8 + separate
                + zds.ZDS_Field9 + separate + zds.ZDS_Field10;
            NTE = "NTE" + separate +"1"+ separate+ comment;
            txt = txt + MSH + Environment.NewLine + PID + Environment.NewLine + PV1 + Environment.NewLine + ORC + Environment.NewLine + OBR + Environment.NewLine + ZDS + Environment.NewLine+ NTE+ "\x0c";
            return txt;
        }
        public String genORM(String reqdept, String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation
            , String xrayyear, String reqno, String xraycode, String xrayname, String xraytype, String userid, String username, String modality
            , String opdtype, String depcode, String depname, String comment)
        {
            String txt = "";
            PACsMSH msh = new PACsMSH();
            PACsPID pid = new PACsPID();
            PACsPV1 pv1 = new PACsPV1();

            PACsORC orc = new PACsORC();
            PACsOBR obr = new PACsOBR();
            PACsZDS zds = new PACsZDS();
            msh = genPAsMSH(reqdept, "ORM^O01");
            pid = genPACsPID(hn, pttprefix, pttfirstname, pttlastame, dob, sex, nation);
            pv1 = genPV1(opdtype, depcode, depname);
            orc = genORC(xrayyear, reqno, xraycode, userid, username);
            obr = genOBR(xrayyear, reqno, xraycode, xrayname, xraytype, userid, username);
            zds = genZDS(xrayyear, reqno, xraycode, modality);
            txt = PACsORM(msh, pid, pv1, orc, obr, zds, comment);
            return txt;
        }
    }
}
