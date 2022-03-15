using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using bangna_hospital.control;
using bangna_hospital.object1;
using System.Globalization;
using C1.Win.C1SuperTooltip;
using System.Drawing.Printing;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using System.Diagnostics;
using AForge.Video.DirectShow;

namespace bangna_hospital.gui
//NID Card CS.net
//V1.0.0.2081 For TDK Series DLL V2.21.7D+, support DLD
//V1.0.0.2082 fix bug
//....
//....
//....
//V1.0.0.2091 Code clearing
//V1.0.0.2092 Correct program name
{
    public partial class FrmSmartCard : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, famtB14, fque, fqueB;
        static public int grdViewFontSize = 0,printBillTextFoodsSize=0;

        RDNID mRDNIDWRAPPER = new RDNID();
        string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        String fname = "", lname = "", PID="", dob="", addrno="", districtname="", amphurname="", provincename="", sex="", prefixname="", moo="", trok="" ,soi="";
        String fnamee = "", lnamee = "" ,road="", address="", webcamname = "";
        List<String> lSmartCard;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Boolean flagInsert = false;
        Patient ptt;
        Queue queue;
        LabM01 lab;
        Boolean statusStickerIPD = false, pageLoad = false, chkStkDfPrint = false;
        C1FlexGrid grfAdmit, grfHn;
        int colAdmitAn = 1, colAdmitDate = 2, colAdmitWard = 3;
        int colHn = 1, colName = 2, colDate = 3, colAdmit=4, coladdate=5;
        DateTime dtstart1 = new DateTime();
        DateTime dtstart2 = new DateTime();
        String dtrcode = "", dtrname = "", admittime = "", drugset="";
        FilterInfoCollection webcanDevice;
        Bitmap img;
        Image image1;
        Form frmMultiSrc;
        PictureBox picMultiSrc, picKyc;
        Panel pn;
        Boolean flagShow = false;
        public FrmSmartCard(BangnaControl bc)
        {
            InitializeComponent();
            //new LogWriter("d", "FrmSmartCard FrmSmartCard  00");
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize-2, FontStyle.Regular);
            fEditS1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);
            fEdit = new Font(bc.iniC.pdfFontName,bc.pdfFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 3, FontStyle.Bold);
            famt = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famtB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);

            string fileName = StartupPath + "\\RDNIDLib.DLD";
            //fileName = @"D:\source\bangna\NIDCardPlusCS\NIDCardPlusCS\bin\Debug\RDNIDLib.DLD";
            if (System.IO.File.Exists(fileName) == false)
            {
                MessageBox.Show("RDNIDLib.DLD not found");
            }
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            //new LogWriter("d", "FrmSmartCard initConfig  00");
            ptt = new Patient();
            queue = new Queue();
            DateTime vsdate = new DateTime();
            DateTime.TryParse(DateTime.Now.ToString(), out vsdate);
            String datestart = "";
            datestart = vsdate.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            txtVsdate.Text = datestart;
            //new LogWriter("d", "FrmSmartCard initConfig  000");
            bc.bcDB.queueTypeDB.setCboDgs(cboQueue, "");
            //new LogWriter("d", "FrmSmartCard initConfig  001");
            bc.setCboNation(cboPPNat);
            bc.setCboProvince(cboProv);
            bc.bcDB.pttDB.setCboDeptIPD(cboStkWard, bc.iniC.station);

            //new LogWriter("d", "FrmSmartCard initConfig  01");
            initGrfAdmit();
            initGrfHn();

            btnPatientNew.Click += BtnPatientNew_Click;
            txtPaidType.KeyUp += TxtPaidType_KeyUp;
            m_txtID.KeyUp += M_txtID_KeyUp;
            btnVisit.Click += BtnVisit_Click;
            txtSymptom.KeyUp += TxtSymptom_KeyUp;
            txtDept.KeyUp += TxtDept_KeyUp;
            btnPrnStaffNote.Click += BtnPrnStaffNote_Click;
            txtDtrId.KeyUp += TxtDtrId_KeyUp;
            txtMobile.KeyUp += TxtMobile_KeyUp;
            btnPrnLetter.Click += BtnPrnLetter_Click;
            btnPrnSticker.Click += BtnPrnSticker_Click;
            txtTaxId.KeyUp += TxtTaxId_KeyUp;
            btnReqLab.Click += BtnReqLab_Click;
            btnReceipt.Click += BtnReceipt_Click;
            btnPttUpdate.Click += BtnPttUpdate_Click;
            btnPrnQue.Click += BtnPrnQue_Click;
            label60.DoubleClick += Label60_DoubleClick;
            txtPPID.KeyUp += TxtPPID_KeyUp;
            txtPPPassport.KeyUp += TxtPPPassport_KeyUp;
            txtPPFirstnameT.KeyUp += TxtPPFirstnameT_KeyUp;
            txtPPLastnameT.KeyUp += TxtPPLastnameT_KeyUp;
            txtPPMiddlenameT.KeyUp += TxtPPMiddlenameT_KeyUp;
            txtPPFirstnameE.KeyUp += TxtPPFirstnameE_KeyUp;
            txtPPMiddlenameE.KeyUp += TxtPPMiddlenameE_KeyUp;
            txtPPLastnameE.KeyUp += TxtPPLastnameE_KeyUp;
            txtNatCode.KeyUp += TxtNatCode_KeyUp;
            txtOccupation.KeyUp += TxtOccupation_KeyUp;
            txtCurAddress.KeyUp += TxtCurAddress_KeyUp;
            txtCurMoo.KeyUp += TxtCurMoo_KeyUp;
            txtCurSoi.KeyUp += TxtCurSoi_KeyUp;
            txtCurRoad.KeyUp += TxtCurRoad_KeyUp;

            btnPatientNewF.Click += BtnPatientNewF_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            label46.DoubleClick += Label46_DoubleClick;
            ChkSE640.Click += ChkSE640_Click;
            chkSE629.Click += ChkSE629_Click;
            chkSE184.Click += ChkSE184_Click;
            cboPPNat.SelectedItemChanged += CboPPNat_SelectedItemChanged;
            cboProv.SelectedItemChanged += CboProv_SelectedItemChanged;
            cboDistrict.SelectedItemChanged += CboDistrict_SelectedItemChanged;
            chkPCR1500.Click += ChkPCR1500_Click;
            chkCentralPark.Click += ChkCentralPark_Click;
            chkATKFree.Click += ChkATKFree_Click;
            txtStkHn.KeyUp += TxtStkHn_KeyUp;
            btnStkPrint.Click += BtnStkPrint_Click;
            cboStkWard.SelectedIndexChanged += CboStkWard_SelectedIndexChanged;
            txtStkDtrCode.KeyUp += TxtStkDtrCode_KeyUp;
            chkStkTemp1.Click += ChkStkTemp1_Click;
            chkStkTemp2.Click += ChkStkTemp2_Click;
            chkStkDf.Click += ChkStkDf_Click;
            chkStkEx1.Click += ChkStkEx1_Click;
            txtStkDtr1Code.KeyUp += TxtStkDtr1Code_KeyUp;
            chkHI.Click += ChkHI_Click;
            btnCapScreen.Click += BtnCapScreen_Click;
            btnScr2.Click += BtnScr2_Click;

            btnWebCamOn.Click += BtnWebCamOn_Click;
            btnCapture.Click += BtnCapture_Click;
            btnSavePic.Click += BtnSavePic_Click;
            this.FormClosing += FrmSmartCard_FormClosing;

            btnReqDrug1.Click += BtnReqDrug1_Click;
            btnReqDrug2.Click += BtnReqDrug2_Click;
            btnReqDrug3.Click += BtnReqDrug3_Click;
            btnReqDrug4.Click += BtnReqDrug4_Click;
            btnReqXray.Click += BtnReqXray_Click;
            //new LogWriter("d", "FrmSmartCard initConfig  02");
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = String.Format("R&D NID Card Plus C# {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            try
            {
                frmMultiSrc = new Form();
                frmMultiSrc.WindowState = FormWindowState.Maximized;
                picMultiSrc = new PictureBox();
                frmMultiSrc.Controls.Add(picMultiSrc);
                pn = new Panel();
                pn.Dock = DockStyle.Bottom;
                picKyc = new PictureBox();
                picKyc.Dock = DockStyle.Fill;
                picKyc.Image = Resources.kyc;
                picKyc.Width = picKyc.Image.Width;
                picKyc.Height = picKyc.Image.Height;
                pn.Controls.Add(picKyc);
                frmMultiSrc.Controls.Add(pn);
                picMultiSrc.Dock = DockStyle.None;
                picMultiSrc.BorderStyle = BorderStyle.Fixed3D;
                picMultiSrc.Image = picPttCap.Image;
                picMultiSrc.Width = 700;
                picMultiSrc.Height = 700;
                lblfMessage.Text = Screen.AllScreens.Length.ToString();
                int i = 0;
                foreach (Screen screen in Screen.AllScreens)
                {
                    if (i == 0)
                    {
                        lblfscreen1.Text = screen.Primary.ToString()+" "+ screen.DeviceName+" "+screen.WorkingArea.Location;
                    }
                    else if (i == 1)
                    {
                        lblfscreen2.Text = screen.Primary.ToString() + " " + screen.DeviceName + " " + screen.WorkingArea.Location;
                    }
                    if (!screen.Primary)
                    {
                        frmMultiSrc.Location = Screen.AllScreens[1].WorkingArea.Location;
                    }
                    i++;
                }

                webcanDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                bc.video = new VideoCaptureDevice();
                foreach (FilterInfo device in webcanDevice)
                {
                    webcamname = device.Name;
                    //MessageBox.Show("device.Name "+ device.Name, "");
                    //video.NewFrame += Video_NewFrame;
                }

                int nres = 0;
                byte[] _lic = String2Byte(fileName);
                nres = RDNID.openNIDLibRD(_lic);
                if (nres != 0)
                {
                    String m;
                    m = String.Format(" error no {0} ", nres);
                    MessageBox.Show(m);
                }
                byte[] Licinfo = new byte[1024];

                RDNID.getLicenseInfoRD(Licinfo);
                //m_lblDLDInfo.Text = aByteToString(Licinfo);

                byte[] Softinfo = new byte[1024];
                RDNID.getSoftwareInfoRD(Softinfo);
                //m_lblSoftwareInfo.Text = aByteToString(Softinfo);
                lSmartCard = new List<string>();
                ListCardReader();
            }
            catch(Exception ex)
            {
                //An attempt was made to load a program with an incorrect format. (Exception from HRESULT: 0x8007000B)
                //ถ้า error แบบนี้  ต้องเปลี่ยน Property Platform target เป็น x86
                new LogWriter("e", "FrmSmartCard initConfig  "+ex.Message);
            }
            setControl();
            pageLoad = false;
        }

        private void BtnReqXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String reXray = "";
            XrayT01 xrayT01 = new XrayT01();
            xrayT01 = setXrayT01(ptt.Hn, ptt.hnyr, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), txtDtrId.Text.Trim());
            reXray = bc.bcDB.xrayT01DB.insertXrayT01(xrayT01);
            int chk = 0;
            if (int.TryParse(reXray, out chk))
            {
                txtReqNoXray.Text = reXray;
            }
        }
        private XrayT01 setXrayT01(String hn, string hnyear, String vsDate, String preno, String dtrcode)
        {
            XrayT01 xrayT01 = new XrayT01();
            xrayT01.MNC_REQ_YR = (DateTime.Now.Year + 543).ToString();
            xrayT01.MNC_REQ_NO = "";
            xrayT01.MNC_REQ_DAT = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
            xrayT01.MNC_REQ_DEP = "";
            xrayT01.MNC_REQ_STS = "";
            xrayT01.MNC_REQ_TIM = "";
            xrayT01.MNC_HN_YR = hnyear;
            xrayT01.MNC_HN_NO = hn;
            xrayT01.MNC_AN_YR = "";
            xrayT01.MNC_AN_NO = "";
            xrayT01.MNC_PRE_NO = preno;
            xrayT01.MNC_DATE = vsDate;
            xrayT01.MNC_TIME = "";
            xrayT01.MNC_DOT_CD = dtrcode;
            xrayT01.MNC_WD_NO = "";
            xrayT01.MNC_RM_NAM = "";
            xrayT01.MNC_BD_NO = "";
            xrayT01.MNC_FN_TYP_CD = "";
            xrayT01.MNC_COM_CD = "";
            xrayT01.MNC_REM = "";
            xrayT01.MNC_XR_STS = "";
            xrayT01.MNC_CAL_NO = "";
            xrayT01.MNC_EMPR_CD = bc.user.username;
            xrayT01.MNC_EMPC_CD = bc.user.username;
            xrayT01.MNC_ORD_DOT = "";
            xrayT01.MNC_CFM_DOT = "";
            xrayT01.MNC_DOC_YR = "";
            xrayT01.MNC_DOC_NO = "";
            xrayT01.MNC_DOC_DAT = "";
            xrayT01.MNC_DOC_CD = "";
            xrayT01.MNC_STAMP_DAT = "";
            xrayT01.MNC_STAMP_TIM = "";
            xrayT01.MNC_CANCEL_STS = "";
            xrayT01.MNC_PAC_CD = "";
            xrayT01.MNC_PAC_TYP = "";
            xrayT01.status_pacs = "";
            return xrayT01;
        }
        private void BtnReqDrug4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //setOrderDrug("b");
        }

        private void BtnReqDrug3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setOrderDrug("d");
        }

        private void BtnReqDrug2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setOrderDrug("k");
        }

        private void BtnReqDrug1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setOrderDrug("b");
        }
        private void setOrderDrug(String flagdrug)
        {
            String sql = "", re = "", preno = "", vsdate = "", labcode = "";
            long chk = 0;
            DateTime datechk = new DateTime();
            sep.Clear();

            if (ptt.MNC_HN_NO.Length < 5)
            {
                sep.SetError(m_txtID, "");
                MessageBox.Show("MNC_HN_NO", "");
                return;
            }
            if (txtDept.Text.Length <= 0)
            {
                sep.SetError(txtDept, "");
                MessageBox.Show("txtDept", "");
                return;
            }
            if (txtPreno.Text.Length <= 0)
            {
                sep.SetError(txtDept, "");
                MessageBox.Show("txtPreno", "");
                return;
            }
            if (txtDtrId.Text.Length <= 0)
            {
                sep.SetError(txtDtrId, "");
                MessageBox.Show("กรุณาป้อน ว.แพทย์", "");
                return;
            }
            //new LogWriter("d", "BtnReqLab_Click ");
            //MessageBox.Show("6666666", "");
            if (DateTime.TryParse(txtVsdate.Text.Trim(), out datechk))
            {
                //MessageBox.Show("7777777", "");
                //new LogWriter("d", "BtnReqLab_Click 1");
                if (flagdrug.Equals("b"))
                {
                    drugset = "b";
                    re = bc.bcDB.pharT01DB.insertPharmacyOPDDrugB(ptt.MNC_HN_YR, txtHn.Text.Trim(), txtVsdate.Text.Trim(), txtPreno.Text.Trim(), txtDtrId.Text.Trim(), "1618");
                }
                else if (flagdrug.Equals("k"))
                {
                    drugset = "k";
                    re = bc.bcDB.pharT01DB.insertPharmacyOPDDrugK(ptt.MNC_HN_YR, ptt.MNC_HN_NO, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), labcode, "1618");
                }
                else if (flagdrug.Equals("d"))
                {
                    drugset = "d";
                    re = bc.bcDB.pharT01DB.insertPharmacyOPDDrugD(ptt.MNC_HN_YR, ptt.MNC_HN_NO, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), labcode, "1618");
                }
                if (long.TryParse(re, out chk))
                {
                    stt.SetToolTip(btnReqLab, "ออก ORder drug เรียบร้อย");
                    txtreqno.Text = re;
                    //btnReqLab.Enabled = false;
                    //btnReqLab.BackColor = Color.Yellow;
                    //printLabReqNo();
                    genImgStaffNoteHI();
                    bc.bcDB.vsDB.updateStatusCloseVisit(txtHn.Text.Trim(), ptt.MNC_HN_YR, txtPreno.Text.Trim(), txtVsdate.Text.Trim());
                }
            }
            else
            {
                MessageBox.Show("88888888", "");
                sep.SetError(txtVsdate, "");
                return;
            }
        }
        private void BtnScr2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (frmMultiSrc.IsDisposed)
            {
                frmMultiSrc = new Form();
                frmMultiSrc.WindowState = FormWindowState.Maximized;
                picMultiSrc = new PictureBox();
                frmMultiSrc.Controls.Add(picMultiSrc);
                Panel pn = new Panel();
                pn.Dock = DockStyle.Bottom;
                picKyc = new PictureBox();
                picKyc.Dock = DockStyle.Fill;
                picKyc.Image = Resources.kyc;
                picKyc.Width = picKyc.Image.Width;
                picKyc.Height = picKyc.Image.Height;
                pn.Controls.Add(picKyc);
                frmMultiSrc.Controls.Add(pn);
                picMultiSrc.Dock = DockStyle.None;
                picMultiSrc.BorderStyle = BorderStyle.Fixed3D;
                picMultiSrc.Image = picPttCap.Image;
                picMultiSrc.Width = picMultiSrc.Image.Width;
                picMultiSrc.Height = picMultiSrc.Image.Height;
            }
            frmMultiSrc.Hide();
            frmMultiSrc.WindowState = FormWindowState.Minimized;
            frmMultiSrc.Show();
            frmMultiSrc.WindowState = FormWindowState.Maximized;
            frmMultiSrc.StartPosition = FormStartPosition.CenterScreen;
            lblfStatus.Text = "Screen.AllScreens.Length " + Screen.AllScreens.Length;
            if (Screen.AllScreens.Length > 1)
            {
                lblfStatus.Text = "Screen.AllScreens.Length " + Screen.AllScreens.Length+" OK";
                frmMultiSrc.Location = Screen.AllScreens[1].WorkingArea.Location;
            }
        }

        private void BtnCapScreen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if (Screen.AllScreens.Length > 1)
            //{
            if (frmMultiSrc.IsDisposed)
            {
                frmMultiSrc = new Form();
                frmMultiSrc.WindowState = FormWindowState.Maximized;
                picMultiSrc = new PictureBox();
                frmMultiSrc.Controls.Add(picMultiSrc);
                picMultiSrc.Dock = DockStyle.Fill;
                picMultiSrc.BorderStyle = BorderStyle.Fixed3D;
                picMultiSrc.Image = picPttCap.Image;
            }
            lblfStatus.Text = "Screen.AllScreens.Length " + Screen.AllScreens.Length + " OK";
            frmMultiSrc.Location = Screen.AllScreens[1].WorkingArea.Location;
            //}
            frmMultiSrc.Hide();
            Application.DoEvents();
            frmMultiSrc.Show();
            frmMultiSrc.WindowState = FormWindowState.Minimized;
            frmMultiSrc.Location = new Point(Screen.AllScreens[1].Bounds.Left, Screen.AllScreens[1].Bounds.Top);
            frmMultiSrc.Hide();
            Application.DoEvents();
            frmMultiSrc.Show();
            Application.DoEvents();
            frmMultiSrc.WindowState = FormWindowState.Maximized;
            frmMultiSrc.Show();
        }

        private void FrmSmartCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException();
            if (bc.video != null)
            {
                if (bc.video.IsRunning)
                {
                    bc.video.Stop();
                }
                bc.video = null;
            }
            if(frmMultiSrc != null)
            {
                frmMultiSrc.Dispose();
            }
        }

        private void TakePic()
        {
            //myPlayer.SoundLocation = appPath + "\\camera.wav";
            //myPlayer.Play();
            //listView1.Items.Clear();
            //imageList1.Images.Clear();
            if (img == null) return;
            image1 = (Image)img.Clone();
            if (image1 == null)
            {
            }
            else
            {
                bc.video.Stop();
                btnSavePic.Enabled = true;
                //img = (Bitmap)eventArgs.Frame.Clone();
                //picPtt.Image = img;
            }

            image1 = null;
            //loadimages();
        }
        private void BtnSavePic_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String folder = bc.iniC.pathImageScan+"\\";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            folder += txtHn.Text+"_"+ DateTime.Now.Ticks.ToString()+".jpg";
            image1 = picPttCap.Image;
            //image1.Save(@"temppic.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            if (txtHn.Text.Length > 0)
            {
                picPttCap.Image.Save(folder, System.Drawing.Imaging.ImageFormat.Jpeg);

                String dgssid = "1200000024";
                if (dgssid.Length <= 0)
                {
                    MessageBox.Show("ไม่ได้เลือก กลุ่มเอกสาร", "");
                    return;
                }
                //picWait.Show();
                string ext = Path.GetExtension(folder);
                String dgssname = "", vn = "", an = "";
                //dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
                DocGroupSubScan dgss = new DocGroupSubScan();
                dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
                DocScan dsc = new DocScan();
                //new LogWriter("d", "BtnUpload_Click dsc.vn " + dsc.vn + " dsc.an " + dsc.an);
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = "1100000007";
                dsc.hn = txtHn.Text;
                
                dsc.vn = "";
                dsc.an = txtVN.Text.Trim();
                
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
                dsc.status_ml = "2";
                dsc.ml_fm = "";
                String re = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);

                //sB11.Text = " filename " + filename + " bc.iniC.folderFTP " + bc.iniC.folderFTP + "//" + dsc.image_path;
                long chk = 0;
                if (long.TryParse(re, out chk))
                {
                    //dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1
                    dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + re + ext;
                    String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                    //MessageBox.Show("filename" + filename + "\n bc.iniC.folderFTP " + bc.iniC.folderFTP + "//" + dsc.image_path, "");
                    //FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                    //MessageBox.Show("HN "+ txtHn.Text.Replace("/", "-"), "");
                    //ftp.createDirectory(txtHn.Text);
                    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-"));
                    //MessageBox.Show("222", "");
                    ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                    //MessageBox.Show("333", "");

                    ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, folder);
                    //File.Delete(filename);
                    System.Threading.Thread.Sleep(1000);
                    //this.Dispose();
                    lblfStatus.Text = "FTP success";
                    lblfMessage.Text = bc.iniC.hostFTP+" "+ folder;
                }
            }
            else
            {
                MessageBox.Show("ไม่พบ HN คนไข้", "");
            }
        }

        private void BtnCapture_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TakePic();
        }

        private void BtnWebCamOn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtHn.Text.Length > 0)
            {
                //Patient ptt = new Patient();
                //ptt = bc.bcDB.pttDB.selectPatinetByHn(txtHn.Text);
                try
                {
                    bc.video = new VideoCaptureDevice(webcanDevice[0].MonikerString);
                    bc.video.NewFrame += Video_NewFrame;
                    bc.video.Start();

                    //ic.posiID = "-";
                    btnCapture.Enabled = true;
                    btnSavePic.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorry there is no camera Found\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Sorry No HN", "");
            }
        }
        private void Video_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //throw new NotImplementedException();
            img = (Bitmap)eventArgs.Frame.Clone();
            picPttCap.Image = img;
            if(picMultiSrc != null)
            {
                picMultiSrc.Image = img;
                picMultiSrc.Width = img.Width;
                picMultiSrc.Height = img.Height;

                if (flagShow) return;
                pn.Top = picMultiSrc.Height + 10;
                pn.Height = Screen.AllScreens[1].WorkingArea.Height - picMultiSrc.Height - 10;
                picKyc.SizeMode = PictureBoxSizeMode.StretchImage;
                flagShow = true;
            }
        }
        private void TxtStkDtr1Code_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbStkDtr1Name.Text = bc.selectDoctorName(txtStkDtrCode.Text.Trim());
            }
        }

        private void ChkStkEx1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setStkQty();
        }

        private void ChkStkDf_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setStkQty();
        }

        private void ChkStkTemp2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setStkQty();
        }

        private void ChkStkTemp1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setStkQty();
        }
        private void setStkQty()
        {
            if (chkStkTemp1.Checked)
            {
                txtStkNum.Text = "20";
            }
            else if (chkStkTemp2.Checked)
            {
                txtStkNum.Text = "20";
            }
            else if (chkStkDf.Checked)
            {
                txtStkNum.Text = "2";
            }
            else if (chkStkEx1.Checked)
            {
                txtStkNum.Text = "1";
            }
        }
        private void TxtStkDtrCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbStkDtrName.Text = bc.selectDoctorName(txtStkDtrCode.Text.Trim());
            }
        }
        private void CboStkWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String wardid = "";
            wardid = ((ComboBoxItem)cboStkWard.SelectedItem).Value;
            setGrfHn(wardid);
        }
        private void BtnStkPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printTabStkSticker();
        }
        private void printTabStkSticker()
        {
            PrintDocument document = new PrintDocument();
            if (chkStkDf.Checked)
            {
                chkStkDfPrint = false;
                dtstart1 = (DateTime)txtStkDateStart.Value;
                dtstart2 = dtstart1.AddDays(4);
                document.PrinterSettings.PrinterName = bc.iniC.printerA4;
                PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.PaperSize = new PaperSize("A4", 827, 1169);
                document.DefaultPageSettings = pg;
                document.PrintPage += Document_PrintPage_tabStkDf;
                document.DefaultPageSettings.Landscape = false;
                //document.DefaultPageSettings.PaperSize.RawKind = PaperSize("A4");
                int num = 0;
                if (int.TryParse(txtStkNum.Text, out num))
                {
                    for(int i = 0; i < num; i++)
                    {
                        document.PrinterSettings.Copies = short.Parse("1");
                        document.Print();
                        chkStkDfPrint = true;
                    }
                }
                else
                {
                    sep.SetError(txtStickerNum, "");
                }
                chkStkDfPrint = true;
            }
            else if (chkStkEx1.Checked)
            {
                document.PrinterSettings.PrinterName = bc.iniC.printerA4;
                PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.PaperSize = new PaperSize("A4", 827, 1169);
                document.DefaultPageSettings = pg;
                
                document.PrintPage += Document_PrintPage_tabStkEx1;
                document.DefaultPageSettings.Landscape = false;
                int num = 0;
                if (int.TryParse("1", out num))
                {
                    document.PrinterSettings.Copies = short.Parse(num.ToString());
                    document.Print();
                    //}
                }
                else
                {
                    sep.SetError(txtStickerNum, "");
                }
            }
            else if (chkStkFoodsOrder.Checked)
            {

            }
            else if (chkStkKodon.Checked)
            {
                String filename = "";
                filename = Path.GetDirectoryName(Application.ExecutablePath)+ "\\doc-nurse-dotm\\กอร์ดอน-pop.docm";
                if (File.Exists(filename))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = filename,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("File not found", "");
                }
            }
            else if (chkStkNurseNote.Checked)
            {
                String filename = "";
                filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\doc-nurse-dotm\\กอร์ดอน-pop.docm";
                if (File.Exists(filename))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = filename,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("File not found", "");
                }
            }
            else
            {
                document.PrinterSettings.PrinterName = bc.iniC.printerSticker;
                document.PrintPage += Document_PrintPage_tabStkSticker;
                document.DefaultPageSettings.Landscape = false;
                int num = 0;
                if (int.TryParse(txtStkNum.Text.Trim(), out num))
                {
                    document.PrinterSettings.Copies = short.Parse(num.ToString());
                    //for(int i = 0; i < num; i++)
                    //{
                    document.Print();
                    //}
                }
                else
                {
                    sep.SetError(txtStickerNum, "");
                }
            }
            
        }
        private void Document_PrintPage_tabStkEx1(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "",txt="";
            float yPos = 10, ydate=0, gapline=35;
            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            date = DateTime.Now.ToString("dd/MM/yyyy");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);
            DateTime dtstart = new DateTime();

            //logo
            Image logo;
            logo = Resources.LOGO_BW_tran;
            float newWidth = logo.Width * 100 / logo.HorizontalResolution;
            float newHeight = logo.Height * 100 / logo.VerticalResolution;

            float widthFactor = 6.8F;
            float heightFactor = 6.8F;
            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    widthFactor = 1;
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                    //newWidth = newWidth / 1.2;
                    //newHeight = newHeight / 1.2;
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }
            RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            e.Graphics.DrawImage(logo, recf);
            yPos += 5;
            e.Graphics.DrawString("โรงพยาบาล บางนา5", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos += 10;
            e.Graphics.DrawString("Bangna5 General Hospital", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos = 45;
            e.Graphics.DrawString("ใบคิดค่าใช้จ่าย", famt, Brushes.Black, 400, yPos, flags);
            //e.Graphics.DrawRectangle(blackPen, 430, yPos, 15, 15);
            yPos += 35;
            e.Graphics.DrawString("ชื่อ-นามสกุล " +  txtStkNameT.Text + " [" +txtStkHn.Text + "] ward " + txtStkWard.Text + " วันที่รับ " + txtStkDateStart.Text + " " + admittime, fEdit, Brushes.Black, 40, yPos, flags);
            yPos += 30;
            //e.Graphics.DrawString("ward ห้อง "+txtStkWard.Text, fEdit, Brushes.Black, 40, yPos, flags);
            e.Graphics.DrawString("แพทย์ผู้รับ " + lbStkDtr1Name.Text + " [" + txtStkDtr1Code.Text+"]", fEdit, Brushes.Black, 40, yPos, flags);
            //e.Graphics.DrawString("แพทย์ผู้รับ ", fEdit, Brushes.Black, 30, yPos, flags);
            //e.Graphics.DrawString("วันที่รับ "+ txtStkDateStart.Text+" "+ admittime, fEdit, Brushes.Black, 350, yPos, flags);
            e.Graphics.DrawString("แพทย์ผู้ดูแล "+lbStkDtrName.Text+" ["+txtStkDtrCode.Text+"]", fEdit, Brushes.Black, 400, yPos, flags);

            yPos += 25;
            float yy = yPos, rectanW = 560;
            e.Graphics.DrawRectangle(blackPen, 30, yPos, 750, rectanW);
            yy += gapline;
            ydate = yPos;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("รายการ ", fEdit, Brushes.Black, 60, ydate, flags);
            e.Graphics.DrawString("ค่าห้อง ", fEdit, Brushes.Black, 40, yy, flags);
            txt = " "+txtStkWard.Text.ToLower().Replace("ward","W").Replace("covid-", "co");
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 160, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 230, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 300, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 370, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 440, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 510, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 580, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 650, yy, flags);
            //e.Graphics.DrawString(txt, fEdit, Brushes.Black, 720, yy, flags);

            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("ค่าอาหาร ", fEdit, Brushes.Black, 40, yy, flags);
            txt = " 3";
            //e.Graphics.DrawString(txt, fEdit, Brushes.Black, 170, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 230, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 300, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 370, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 440, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 510, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 580, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 650, yy, flags);
            //e.Graphics.DrawString(txt, fEdit, Brushes.Black, 720, yy, flags);

            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("ค่ายา ", fEdit, Brushes.Black, 40, yy, flags);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("ค่าLAB ", fEdit, Brushes.Black, 40, yy, flags);
            txt = " 1";
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 170, yy, flags);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("ค่าXray ", fEdit, Brushes.Black, 40, yy, flags);
            txt = " 1";
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 170, yy, flags);
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 440, yy, flags);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("ค่าทำแผล ", fEdit, Brushes.Black, 40, yy, flags);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("ค่าพ่นยา, Oxygen ", fEdit, Brushes.Black, 40, yy, flags);
            txt = " 1";
            e.Graphics.DrawString(txt, fEdit, Brushes.Black, 170, yy, flags);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("ค่าแพทย์ ", fEdit, Brushes.Black, 40, yy, flags);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            e.Graphics.DrawString("อื่นๆ ", fEdit, Brushes.Black, 40, yy, flags);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            if (chkStkOther1.Checked)
            {
                txt = " "+txtStkOther1Name.Text.Trim();
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 40, yy, flags);
                txt = " " + txtStkOther1Qty.Text.Trim();
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 170, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 230, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 300, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 370, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 440, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 510, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 580, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 650, yy, flags);
                //e.Graphics.DrawString(txt, fEdit, Brushes.Black, 720, yy, flags);
            }
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            if (chkStkOther2.Checked)
            {
                txt = " " + txtStkOther2Name.Text.Trim();
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 40, yy, flags);
                txt = " " + txtStkOther1Qty.Text.Trim();
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 170, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 230, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 300, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 370, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 440, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 510, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 580, yy, flags);
                e.Graphics.DrawString(txt, fEdit, Brushes.Black, 650, yy, flags);
                //e.Graphics.DrawString(txt, fEdit, Brushes.Black, 720, yy, flags);
            }
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            yy += gapline;
            e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            //yy += gapline;
            //e.Graphics.DrawLine(blackPen, 30, yy, 780, yy);
            DateTime dt11 = new DateTime();
            DateTime.TryParse(txtStkDateStart.Text, out dt11);
            float adjrectanW = 135;
            e.Graphics.DrawLine(blackPen, 150, yPos, 150, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("วันที่ ", fEdit, Brushes.Black, 170, ydate, flags);
            e.Graphics.DrawString(dt11.ToString("dd/MM"), fEdit, Brushes.Black, 170, ydate+13, flags);
            e.Graphics.DrawLine(blackPen, 220, yPos, 220, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("วันที่ ", fEdit, Brushes.Black, 230, ydate, flags);
            dt11 = dt11.AddDays(1);
            e.Graphics.DrawString(dt11.ToString("dd/MM"), fEdit, Brushes.Black, 230, ydate + 13, flags);
            e.Graphics.DrawLine(blackPen, 290, yPos, 290, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("วันที่ ", fEdit, Brushes.Black, 300, ydate, flags);
            dt11 = dt11.AddDays(1);
            e.Graphics.DrawString(dt11.ToString("dd/MM"), fEdit, Brushes.Black, 300, ydate + 13, flags);
            e.Graphics.DrawLine(blackPen, 360, yPos, 360, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("วันที่ ", fEdit, Brushes.Black, 370, ydate, flags);
            dt11 = dt11.AddDays(1);
            e.Graphics.DrawString(dt11.ToString("dd/MM"), fEdit, Brushes.Black, 370, ydate + 13, flags);
            e.Graphics.DrawLine(blackPen, 430, yPos, 430, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("วันที่ ", fEdit, Brushes.Black, 440, ydate, flags);
            dt11 = dt11.AddDays(1);
            e.Graphics.DrawString(dt11.ToString("dd/MM"), fEdit, Brushes.Black, 440, ydate + 13, flags);
            e.Graphics.DrawLine(blackPen, 500, yPos, 500, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("วันที่ ", fEdit, Brushes.Black, 510, ydate, flags);
            dt11 = dt11.AddDays(1);
            e.Graphics.DrawString(dt11.ToString("dd/MM"), fEdit, Brushes.Black, 510, ydate + 13, flags);
            e.Graphics.DrawLine(blackPen, 570, yPos, 570, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("วันที่ ", fEdit, Brushes.Black, 580, ydate, flags);
            dt11 = dt11.AddDays(1);
            e.Graphics.DrawString(dt11.ToString("dd/MM"), fEdit, Brushes.Black, 580, ydate + 13, flags);
            e.Graphics.DrawLine(blackPen, 640, yPos, 640, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("วันที่ ", fEdit, Brushes.Black, 650, ydate, flags);
            dt11 = dt11.AddDays(1);
            e.Graphics.DrawString(dt11.ToString("dd/MM"), fEdit, Brushes.Black, 650, ydate + 13, flags);
            e.Graphics.DrawLine(blackPen, 710, yPos, 710, rectanW + adjrectanW);       //เส้นตั้ง
            e.Graphics.DrawString("รวม ", fEdit, Brushes.Black, 720, ydate, flags);

            yPos += (rectanW+30);
            e.Graphics.DrawString("ADMISSION NOTE ", fEditB, Brushes.Black, 400, yPos, flags);
            yPos += gapline;
            e.Graphics.DrawString("ประเภทผู้ป่วย ", fEdit, Brushes.Black, 40, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 200, yPos, 15, 15);
            e.Graphics.DrawString("/", famtB14, Brushes.Black, 195, yPos-23, flags);
            e.Graphics.DrawString("ADMIT ", fEdit, Brushes.Black, 220, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 300, yPos, 15, 15);
            e.Graphics.DrawString("OBS ", fEdit, Brushes.Black, 320, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 400, yPos, 15, 15);
            e.Graphics.DrawString("ฝากนอน ", fEdit, Brushes.Black, 420, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 550, yPos, 15, 15);
            e.Graphics.DrawString("ห้องเตียง ........................ ", fEdit, Brushes.Black, 570, yPos, flags);
            yPos += gapline;
            e.Graphics.DrawString("ประะภทห้องที่ต้องการ ", fEdit, Brushes.Black, 40, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 200, yPos, 15, 15);
            e.Graphics.DrawString("ห้องรวม ", fEdit, Brushes.Black, 220, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 300, yPos, 15, 15);
            e.Graphics.DrawString("ห้องคู่ ", fEdit, Brushes.Black, 320, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 400, yPos, 15, 15);
            e.Graphics.DrawString("ห้องพิเศษเดี่ยว ", fEdit, Brushes.Black, 420, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 550, yPos, 15, 15);
            e.Graphics.DrawString("ห้อง VIP ", fEdit, Brushes.Black, 570, yPos, flags);
            yPos += gapline;
            e.Graphics.DrawString("สิทธิการรักษา "+txtStkPaidName.Text.Trim(), fEdit, Brushes.Black, 40, yPos, flags);
            yPos += gapline;
            e.Graphics.DrawString("อัตราประเมิน(จำนวนเงิน) ..................................................................................... ", fEdit, Brushes.Black, 40, yPos, flags);
            yPos += gapline;
            e.Graphics.DrawString("เอกสารที่เกี่ยวข้อง ", fEdit, Brushes.Black, 40, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 200, yPos, 15, 15);
            e.Graphics.DrawString("ครบ ", fEdit, Brushes.Black, 220, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 280, yPos, 15, 15);
            e.Graphics.DrawString("ไม่ครบ ระบุ ........................................................ ", fEdit, Brushes.Black, 300, yPos, flags);
            yPos += gapline;
            e.Graphics.DrawString("หมายเหตุ ", fEdit, Brushes.Black, 40, yPos, flags);
            yPos += gapline;
            e.Graphics.DrawString("ลงชื่อ ................................................. เจ้าหน้าที่การเงิน ", fEdit, Brushes.Black, 40, yPos, flags);
            //yPos += gapline;
            e.Graphics.DrawString("ลงชื่อ ................................................. เจ้าหน้าที่ประเมิน ", fEdit, Brushes.Black, 460, yPos, flags);
            
            yPos += gapline;
            e.Graphics.DrawString("เวลาที่ผู้ป่วยกลับบ้าน .......................... ", fEdit, Brushes.Black, 40, yPos, flags);
            //yPos += gapline;
            e.Graphics.DrawString("เวลาที่รับกลับบ้าน ........................... ", fEdit, Brushes.Black, 460, yPos, flags);
            yPos += gapline;
            yPos += gapline;
            yPos += gapline;
            e.Graphics.DrawString("FM-NUR-036(00-01/09/53) (1/1) ", fEdit, Brushes.Black, 40, yPos, flags);
        }
        private void Document_PrintPage_tabStkDf(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "";
            float yPos = 10;
            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            date = DateTime.Now.ToString("dd/MM/yyyy");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);
            DateTime dtstart = new DateTime();

            //logo
            Image logo;
            logo = Resources.LOGO_BW_tran;
            float newWidth = logo.Width * 100 / logo.HorizontalResolution;
            float newHeight = logo.Height * 100 / logo.VerticalResolution;

            float widthFactor = 6.8F;
            float heightFactor = 6.8F;
            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    widthFactor = 1;
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                    //newWidth = newWidth / 1.2;
                    //newHeight = newHeight / 1.2;
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }

            RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            e.Graphics.DrawImage(logo, recf);
            yPos += 5;
            e.Graphics.DrawString("โรงพยาบาล บางนา5", fEditS, Brushes.Black, recf.Width+20, yPos, flags);
            yPos += 10;
            e.Graphics.DrawString("Bangna5 General Hospital", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos = 45;
            e.Graphics.DrawString("แบบฟอร์มเยี่ยมไข้ / หัตถการของแพทย์", fEdit, Brushes.Black, 160, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 430, yPos, 15, 15);
            e.Graphics.DrawString("เยี่ยมผู้ป่วย Admit", fEdit, Brushes.Black, 445, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 560, yPos, 15, 15);
            e.Graphics.DrawString("ทำหัตถการ .......................................", fEdit, Brushes.Black, 580, yPos, flags);
            yPos += 35;
            e.Graphics.DrawString("ชื่อ-นามสกุล "+ txtStkHn.Text+" " + txtStkNameT.Text+" "+txtStkWard.Text, fEdit, Brushes.Black, 30, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 480, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 545, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 620, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 710, yPos, 15, 15);
            e.Graphics.DrawString("ประเภท", fEdit, Brushes.Black, 420, yPos, flags);
            e.Graphics.DrawString("ปกส.", fEdit, Brushes.Black, 505, yPos, flags);
            e.Graphics.DrawString("กองทุน", fEdit, Brushes.Black, 570, yPos, flags);
            e.Graphics.DrawString("บริษัท", fEdit, Brushes.Black, 650, yPos, flags);
            e.Graphics.DrawString("ทั่วไป", fEdit, Brushes.Black, 730, yPos, flags);

            yPos += 25;
            float yy = yPos, rectanW=730;
            e.Graphics.DrawRectangle(blackPen, 30, yPos, rectanW, 160);
            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW+30, yPos);
            //e.Graphics.DrawString(dtstart.ToString("dd/MM/yyyy"), fEdit, Brushes.Black, 35, yPos+10, flags);
            if (chkStkDfPrint)
            {
                dtstart = dtstart2;
            }
            else
            {
                dtstart = dtstart1;
            }
            e.Graphics.DrawString(dtstart.ToString("dd/MM/yyyy"), fEdit, Brushes.Black, 35, yPos + 10, flags);
            chkStkDfPrint = true;
            e.Graphics.DrawString(txtStkDtrCode.Text+" "+lbStkDtrName.Text, fEdit, Brushes.Black, 220, yPos + 10, flags);

            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);
            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);

            e.Graphics.DrawLine(blackPen, 120, yy, 120, 265);       //เส้นตั้ง
            e.Graphics.DrawLine(blackPen, 200, yy, 200, 265);
            e.Graphics.DrawLine(blackPen, 500, yy, 500, 265);
            e.Graphics.DrawLine(blackPen, 620, yy, 620, 265);
            e.Graphics.DrawString("วัน/เดือน/ปี", fEdit, Brushes.Black, 35, yy+10, flags);
            e.Graphics.DrawString("เวลา", fEdit, Brushes.Black, 135, yy + 10, flags);
            e.Graphics.DrawString("ชื่อแพทย์", fEdit, Brushes.Black, 300, yy + 10, flags);
            e.Graphics.DrawString("จำนวนเงิน", fEdit, Brushes.Black, 520, yy + 10, flags);
            e.Graphics.DrawString("หมายเหตุ", fEdit, Brushes.Black, 630, yy + 10, flags);

            //ใบที่2
            yPos += 15;
            yPos += 40;
            recf = new RectangleF(15, yPos, (int)newWidth, (int)newHeight);
            e.Graphics.DrawImage(logo, recf);
            yPos += 5;
            e.Graphics.DrawString("โรงพยาบาล บางนา5", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos += 10;
            e.Graphics.DrawString("Bangna5 General Hospital", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos += 25;
            e.Graphics.DrawString("แบบฟอร์มเยี่ยมไข้ / หัตถการของแพทย์", fEdit, Brushes.Black, 160, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 430, yPos, 15, 15);
            e.Graphics.DrawString("เยี่ยมผู้ป่วย Admit", fEdit, Brushes.Black, 445, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 560, yPos, 15, 15);
            e.Graphics.DrawString("ทำหัตถการ .......................................", fEdit, Brushes.Black, 580, yPos, flags);
            yPos += 35;
            e.Graphics.DrawString("ชื่อ-นามสกุล " + txtStkHn.Text + " " + txtStkNameT.Text + " " + txtStkWard.Text, fEdit, Brushes.Black, 30, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 480, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 545, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 620, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 710, yPos, 15, 15);
            e.Graphics.DrawString("ประเภท", fEdit, Brushes.Black, 420, yPos, flags);
            e.Graphics.DrawString("ปกส.", fEdit, Brushes.Black, 505, yPos, flags);
            e.Graphics.DrawString("กองทุน", fEdit, Brushes.Black, 570, yPos, flags);
            e.Graphics.DrawString("บริษัท", fEdit, Brushes.Black, 650, yPos, flags);
            e.Graphics.DrawString("ทั่วไป", fEdit, Brushes.Black, 730, yPos, flags);

            yPos += 25;
            yy = yPos;
            e.Graphics.DrawRectangle(blackPen, 30, yPos, rectanW, 160);
            yPos += 40;
            dtstart = dtstart.AddDays(1);
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);
            e.Graphics.DrawString(dtstart.ToString("dd/MM/yyyy"), fEdit, Brushes.Black, 35, yPos + 10, flags);
            e.Graphics.DrawString(txtStkDtrCode.Text + " " + lbStkDtrName.Text, fEdit, Brushes.Black, 220, yPos + 10, flags);

            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);
            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);

            e.Graphics.DrawLine(blackPen, 120, yy, 120, yy + 160);       //เส้นตั้ง
            e.Graphics.DrawLine(blackPen, 200, yy, 200, yy + 160);
            e.Graphics.DrawLine(blackPen, 500, yy, 500, yy + 160);
            e.Graphics.DrawLine(blackPen, 620, yy, 620, yy + 160);
            e.Graphics.DrawString("วัน/เดือน/ปี", fEdit, Brushes.Black, 35, yy + 10, flags);
            e.Graphics.DrawString("เวลา", fEdit, Brushes.Black, 135, yy + 10, flags);
            e.Graphics.DrawString("ชื่อแพทย์", fEdit, Brushes.Black, 300, yy + 10, flags);
            e.Graphics.DrawString("จำนวนเงิน", fEdit, Brushes.Black, 520, yy + 10, flags);
            e.Graphics.DrawString("หมายเหตุ", fEdit, Brushes.Black, 630, yy + 10, flags);

            //ใบที่3
            yPos += 15;
            yPos += 40;
            recf = new RectangleF(15, yPos, (int)newWidth, (int)newHeight);
            e.Graphics.DrawImage(logo, recf);
            yPos += 5;
            e.Graphics.DrawString("โรงพยาบาล บางนา5", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos += 10;
            e.Graphics.DrawString("Bangna5 General Hospital", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos += 25;
            e.Graphics.DrawString("แบบฟอร์มเยี่ยมไข้ / หัตถการของแพทย์", fEdit, Brushes.Black, 160, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 430, yPos, 15, 15);
            e.Graphics.DrawString("เยี่ยมผู้ป่วย Admit", fEdit, Brushes.Black, 445, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 560, yPos, 15, 15);
            e.Graphics.DrawString("ทำหัตถการ .......................................", fEdit, Brushes.Black, 580, yPos, flags);
            yPos += 35;
            e.Graphics.DrawString("ชื่อ-นามสกุล " + txtStkHn.Text + " " + txtStkNameT.Text + " " + txtStkWard.Text, fEdit, Brushes.Black, 30, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 480, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 545, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 620, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 710, yPos, 15, 15);
            e.Graphics.DrawString("ประเภท", fEdit, Brushes.Black, 420, yPos, flags);
            e.Graphics.DrawString("ปกส.", fEdit, Brushes.Black, 505, yPos, flags);
            e.Graphics.DrawString("กองทุน", fEdit, Brushes.Black, 570, yPos, flags);
            e.Graphics.DrawString("บริษัท", fEdit, Brushes.Black, 650, yPos, flags);
            e.Graphics.DrawString("ทั่วไป", fEdit, Brushes.Black, 730, yPos, flags);

            yPos += 25;
            yy = yPos;
            e.Graphics.DrawRectangle(blackPen, 30, yPos, rectanW, 160);
            yPos += 40;
            dtstart = dtstart.AddDays(1);
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);
            e.Graphics.DrawString(dtstart.ToString("dd/MM/yyyy"), fEdit, Brushes.Black, 35, yPos + 10, flags);
            e.Graphics.DrawString(txtStkDtrCode.Text + " " + lbStkDtrName.Text, fEdit, Brushes.Black, 220, yPos + 10, flags);

            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);
            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);

            e.Graphics.DrawLine(blackPen, 120, yy, 120, yy + 160);       //เส้นตั้ง
            e.Graphics.DrawLine(blackPen, 200, yy, 200, yy + 160);
            e.Graphics.DrawLine(blackPen, 500, yy, 500, yy + 160);
            e.Graphics.DrawLine(blackPen, 620, yy, 620, yy + 160);
            e.Graphics.DrawString("วัน/เดือน/ปี", fEdit, Brushes.Black, 35, yy + 10, flags);
            e.Graphics.DrawString("เวลา", fEdit, Brushes.Black, 135, yy + 10, flags);
            e.Graphics.DrawString("ชื่อแพทย์", fEdit, Brushes.Black, 300, yy + 10, flags);
            e.Graphics.DrawString("จำนวนเงิน", fEdit, Brushes.Black, 520, yy + 10, flags);
            e.Graphics.DrawString("หมายเหตุ", fEdit, Brushes.Black, 630, yy + 10, flags);

            //ใบที่4
            yPos += 15;
            yPos += 40;
            recf = new RectangleF(15, yPos, (int)newWidth, (int)newHeight);
            e.Graphics.DrawImage(logo, recf);
            yPos += 5;
            e.Graphics.DrawString("โรงพยาบาล บางนา5", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos += 10;
            e.Graphics.DrawString("Bangna5 General Hospital", fEditS, Brushes.Black, recf.Width + 20, yPos, flags);
            yPos += 25;
            e.Graphics.DrawString("แบบฟอร์มเยี่ยมไข้ / หัตถการของแพทย์", fEdit, Brushes.Black, 160, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 430, yPos, 15, 15);
            e.Graphics.DrawString("เยี่ยมผู้ป่วย Admit", fEdit, Brushes.Black, 445, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 560, yPos, 15, 15);
            e.Graphics.DrawString("ทำหัตถการ .......................................", fEdit, Brushes.Black, 580, yPos, flags);
            yPos += 35;
            e.Graphics.DrawString("ชื่อ-นามสกุล " + txtStkHn.Text + " " + txtStkNameT.Text + " " + txtStkWard.Text, fEdit, Brushes.Black, 30, yPos, flags);
            e.Graphics.DrawRectangle(blackPen, 480, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 545, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 620, yPos, 15, 15);
            e.Graphics.DrawRectangle(blackPen, 710, yPos, 15, 15);
            e.Graphics.DrawString("ประเภท", fEdit, Brushes.Black, 420, yPos, flags);
            e.Graphics.DrawString("ปกส.", fEdit, Brushes.Black, 505, yPos, flags);
            e.Graphics.DrawString("กองทุน", fEdit, Brushes.Black, 570, yPos, flags);
            e.Graphics.DrawString("บริษัท", fEdit, Brushes.Black, 650, yPos, flags);
            e.Graphics.DrawString("ทั่วไป", fEdit, Brushes.Black, 730, yPos, flags);

            yPos += 25;
            yy = yPos;
            e.Graphics.DrawRectangle(blackPen, 30, yPos, rectanW, 160);
            yPos += 40;
            dtstart = dtstart.AddDays(1);
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);
            e.Graphics.DrawString(dtstart.ToString("dd/MM/yyyy"), fEdit, Brushes.Black, 35, yPos + 10, flags);
            e.Graphics.DrawString(txtStkDtrCode.Text + " " + lbStkDtrName.Text, fEdit, Brushes.Black, 220, yPos + 10, flags);

            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);
            yPos += 40;
            e.Graphics.DrawLine(blackPen, 30, yPos, rectanW + 30, yPos);

            e.Graphics.DrawLine(blackPen, 120, yy, 120, yy + 160);       //เส้นตั้ง
            e.Graphics.DrawLine(blackPen, 200, yy, 200, yy + 160);
            e.Graphics.DrawLine(blackPen, 500, yy, 500, yy + 160);
            e.Graphics.DrawLine(blackPen, 620, yy, 620, yy + 160);
            e.Graphics.DrawString("วัน/เดือน/ปี", fEdit, Brushes.Black, 35, yy + 10, flags);
            e.Graphics.DrawString("เวลา", fEdit, Brushes.Black, 135, yy + 10, flags);
            e.Graphics.DrawString("ชื่อแพทย์", fEdit, Brushes.Black, 300, yy + 10, flags);
            e.Graphics.DrawString("จำนวนเงิน", fEdit, Brushes.Black, 520, yy + 10, flags);
            e.Graphics.DrawString("หมายเหตุ", fEdit, Brushes.Black, 630, yy + 10, flags);

            yPos += 40;
            e.Graphics.DrawString("FM-NUR-056 (00-01/09/53) (1/1)", fEdit, Brushes.Black, 30, yPos, flags);
        }
        private void Document_PrintPage_tabStkSticker(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }
            if (chkStkTemp2.Checked)
            {
                line = "H.N. " + ptt.MNC_HN_NO + " " + txtStkAN.Text;
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 5, flags);
                line = ptt.Name;
                e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 25, flags);
                //line = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString() + " " + dt.Rows[0]["MNC_RM_NAM"].ToString() + " " + dt.Rows[0]["MNC_BD_NO"].ToString();//MNC_AN_NO
                line = txtStkDOB.Text;
                e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 45, flags);

                line = "H.N. " + ptt.MNC_HN_NO + " " + txtStkAN.Text;
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 65, flags);
                line = ptt.Name;
                e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 85, flags);
                //line = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString() + " " + dt.Rows[0]["MNC_RM_NAM"].ToString() + " " + dt.Rows[0]["MNC_BD_NO"].ToString();
                line = txtStkDOB.Text;
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 100, flags);
            }
            else if (chkStkTemp1.Checked)
            {
                line = "H.N. " + ptt.MNC_HN_NO + " " + txtStkAN.Text;
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 5, flags);
                line = ptt.Name;
                e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 25, flags);
                //line = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString() + " " + dt.Rows[0]["MNC_RM_NAM"].ToString() + " " + dt.Rows[0]["MNC_BD_NO"].ToString();//MNC_AN_NO
                line = txtStkDOB.Text;
                line = line.Replace("เดือน", "ด");
                line = line.Replace("วัน", "ว");
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 45, flags);
                line = txtStkWard.Text;
                e.Graphics.DrawString(line, fEditS, Brushes.Black, 115, yPos + 45, flags);

                line = "H.N. " + ptt.MNC_HN_NO + " " + txtStkAN.Text;
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 65, flags);
                line = ptt.Name;
                e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 85, flags);
                //line = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString() + " " + dt.Rows[0]["MNC_RM_NAM"].ToString() + " " + dt.Rows[0]["MNC_BD_NO"].ToString();
                line = txtStkDOB.Text;
                line = line.Replace("เดือน", "ด");
                line = line.Replace("วัน", "ว");
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 100, flags);

                line = txtStkWard.Text;
                e.Graphics.DrawString(line, fEditS, Brushes.Black, 115, yPos + 100, flags);
            }
        }
        private void TxtStkHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setTxtStkHn();
            }
        }
        private void clearControl()
        {
            //txtStkHn.Text = "";
            txtStkDOB.Text = "";
            txtStkNameE.Text = "";
            txtStkNameT.Text = "";
            txtStkAN.Text = "";
            txtStkWard.Text = "";
        }
        private void setTxtStkHn()
        {
            clearControl();
            DataTable dt = new DataTable();
            ptt = bc.bcDB.pttDB.selectPatinetByHn(txtStkHn.Text.Trim());
            dt = bc.bcDB.vsDB.selectPttinWard1(txtStkHn.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtStkAN.Text = dt.Rows[0]["MNC_AN_NO"].ToString() + "." + dt.Rows[0]["MNC_AN_yr"].ToString();
                txtStkWard.Text = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString();
                DateTime dt1 = new DateTime();
                if (DateTime.TryParse(dt.Rows[0]["MNC_AD_DATE"].ToString(), out dt1))
                {
                    if (dt1.Year < 2000)
                    {
                        dt1 = dt1.AddYears(543);
                    }
                    txtStkDateStart.Value = dt1;
                }//MNC_MD_DEP_DSC
                txtStkDtr1Code.Text = dt.Rows[0]["MNC_DOT_CD_s"].ToString();
                lbStkDtr1Name.Text = bc.selectDoctorName(txtStkDtr1Code.Text);
                //txtStkDtrCode.Text = dt.Rows[0]["MNC_DOT_CD_R"].ToString();
                //lbStkDtrName.Text = dt.Rows[0]["MNC_PFIX_DSC"].ToString() + " " + dt.Rows[0]["MNC_DOT_FNAME"].ToString()+" "+ dt.Rows[0]["MNC_DOT_LNAME"].ToString();
                admittime = dt.Rows[0]["MNC_AD_time"].ToString();
                txtStkPaidName.Text = dt.Rows[0]["MNC_FN_TYP_DSC"].ToString() + " [" + dt.Rows[0]["MNC_FN_TYP_CD"].ToString()+"]";
                setGrfAdmit(dt);
            }
            
            txtStkNameT.Text = ptt.Name;
            //txtStkDOB.Text = ptt.AgeStringShort();
            txtStkDOB.Text = bc.iniC.windows.Equals("windowsxp") ? ptt.AgeStringShortEngWindowsXP() : ptt.AgeStringOK();
            txtStkDOB.Text = txtStkDOB.Text.Replace("Years", "ปี");
            txtStkDOB.Text = txtStkDOB.Text.Replace("Year", "ปี");
            txtStkDOB.Text = txtStkDOB.Text.Replace("Months", "เดือน");
            txtStkDOB.Text = txtStkDOB.Text.Replace("Month", "เดือน");
            txtStkDOB.Text = txtStkDOB.Text.Replace("Days", "วัน");
            txtStkDOB.Text = txtStkDOB.Text.Replace("Day", "วัน");
        }
        private void ChkATKFree_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSymptom();
        }
        private void ChkHI_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSymptom();
        }
        private void initGrfAdmit()
        {
            grfAdmit = new C1FlexGrid();
            grfAdmit.Font = fEdit;
            grfAdmit.Dock = System.Windows.Forms.DockStyle.Fill;
            grfAdmit.Location = new System.Drawing.Point(0, 0);
            grfAdmit.Rows.Count = 1;

            panel3.Controls.Add(grfAdmit);

            grfAdmit.Rows.Count = 1;
            grfAdmit.Cols.Count = 4;
            grfAdmit.Cols[colAdmitAn].Caption = "AN";
            grfAdmit.Cols[colAdmitDate].Caption = "admit Date";
            grfAdmit.Cols[colAdmitWard].Caption = "Ward";

            grfAdmit.Cols[colAdmitAn].Width = 100;
            grfAdmit.Cols[colAdmitDate].Width = 100;
            grfAdmit.Cols[colAdmitWard].Width = 100;

            grfAdmit.Cols[colAdmitAn].AllowEditing = false;
            grfAdmit.Cols[colAdmitDate].AllowEditing = false;
            grfAdmit.Cols[colAdmitWard].AllowEditing = false;

            grfAdmit.Click += GrfAdmit_Click;

            //theme1.SetTheme(grfAdmit, bc.iniC.themeApp);
        }
        private void setGrfAdmit(DataTable dt)
        {
            grfAdmit.Rows.Count = 1;
            grfAdmit.Rows.Count = dt.Rows.Count + 1;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfAdmit[i, colAdmitAn] = row1["MNC_AN_NO"].ToString()+"."+ row1["MNC_AN_yr"].ToString();
                grfAdmit[i, colAdmitDate] = row1["MNC_AD_DATE"].ToString();
                grfAdmit[i, colAdmitWard] = row1["MNC_MD_DEP_DSC"].ToString();
                grfAdmit[i, 0] = i;
                if (i%2==0)
                {
                    grfAdmit.Rows[i].StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
                }
            }
        }
        private void GrfAdmit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfHn()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);
            grfHn.Rows.Count = 1;

            panel5.Controls.Add(grfHn);

            grfHn.Rows.Count = 1;
            grfHn.Cols.Count = 6;
            grfHn.Cols[colHn].Caption = "HN";
            grfHn.Cols[colName].Caption = "Name";
            grfHn.Cols[colDate].Caption = "date";
            grfHn.Cols[colAdmit].Caption = "cal";
            grfHn.Cols[0].Width = 30;
            grfHn.Cols[colHn].Width = 70;
            grfHn.Cols[colName].Width = 200;
            grfHn.Cols[colDate].Width = 70;
            grfHn.Cols[colAdmit].Width = 40;

            grfHn.Cols[colHn].AllowEditing = false;
            grfHn.Cols[colName].AllowEditing = false;
            grfHn.Cols[colDate].AllowEditing = false;
            grfHn.Cols[colAdmit].AllowEditing = false;
            grfHn.Cols[coladdate].AllowEditing = false;

            grfHn.Click += GrfHn_Click;
            grfHn.SelectionMode = SelectionModeEnum.Row;
            grfHn.AllowFiltering = true;
            
            //theme1.SetTheme(grfAdmit, bc.iniC.themeApp);
        }
        private void setGrfHn(String wardid)
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pttDB.selectPatientinWardIPD(wardid);
            grfHn.Rows.Count = 1;
            grfHn.Rows.Count = dt.Rows.Count + 1;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfHn[i, colHn] = row1["MNC_HN_NO"].ToString();
                grfHn[i, colName] = row1["MNC_PFIX_DSC"].ToString() +" "+ row1["MNC_FNAME_T"].ToString() +" "+ row1["MNC_LNAME_T"].ToString();
                grfHn[i, colDate] = bc.datetoShowShort(row1["MNC_AD_DATE"].ToString());
                grfHn[i, colAdmit] = row1["day1"].ToString();
                grfHn[i, coladdate] = row1["MNC_AD_DATE"].ToString();
                grfHn[i, 0] = i;
                if (i % 2 == 0)
                {
                    grfHn.Rows[i].StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
                }
            }
        }
        private void GrfHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHn.Row <= 0) return;
            if (grfHn.Col <= 0) return;
            String hn = "", addate="";
            hn = grfHn[grfHn.Row, colHn].ToString();
            addate = grfHn[grfHn.Row, coladdate].ToString();
            txtStkHn.Text = hn;
            DateTime dt = new DateTime();
            if(DateTime.TryParse(addate, out dt))
            {
                txtStkDateStart.Value = dt;
            }
            setTxtStkHn();
        }

        private void ChkCentralPark_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSymptom();
        }

        private void ChkPCR1500_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSymptom();
        }

        private void TxtCurRoad_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            cboProv.Focus();
        }

        private void TxtCurSoi_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCurRoad.SelectAll();
                txtCurRoad.Focus();
            }
        }

        private void TxtCurMoo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCurSoi.SelectAll();
                txtCurSoi.Focus();
            }
        }

        private void TxtCurAddress_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCurMoo.SelectAll();
                txtCurMoo.Focus();
            }
        }

        private void TxtOccupation_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtNatCode.SelectAll();
                txtNatCode.Focus();
            }
        }

        private void CboDistrict_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!pageLoad)
            {
                bc.setCboSubDistrict(cboSubDistrict, cboDistrict.SelectedItem == null ? "" : ((ComboBoxItem)cboDistrict.SelectedItem).Value);
            }
        }

        private void CboProv_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!pageLoad)
            {
                bc.setCboDistrict(cboDistrict, cboProv.SelectedItem == null ? "" : ((ComboBoxItem)cboProv.SelectedItem).Value);
            }
        }

        private void CboPPNat_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!pageLoad)
            {
                txtNatCode.Text = cboPPNat.SelectedItem == null ? "" : ((ComboBoxItem)cboPPNat.SelectedItem).Value;
            }
        }

        private void ChkSE184_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSymptom();
        }

        private void ChkSE629_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSymptom();
        }

        private void ChkSE640_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSymptom();
        }
        private void setSymptom()
        {
            txtSymptom.Text = ChkSE640.Checked ? "ตรวจ COVID ATK Rapid Test" : chkSE629.Checked ? "ตรวจ COVID RT-PCR" : chkPCR1500.Checked ? "ตรวจ COVID RT-PCR 1500" : chkCentralPark.Checked ? "ตรวจ COVID RT-PCR Central Park" : chkATKFree.Checked ? "ตรวจ COVID ATK" : "ตรวจ COVID RT-PCR ไป จีน";
            lab = chkSE184.Checked ? bc.bcDB.labM01DB.SelectByPk("SE184") : chkSE629.Checked ? bc.bcDB.labM01DB.SelectByPk("SE629") : chkPCR1500.Checked ? bc.bcDB.labM01DB.SelectByPk("SE629") : chkCentralPark.Checked ? bc.bcDB.labM01DB.SelectByPk("SE629") : chkATKFree.Checked ? bc.bcDB.labM01DB.SelectByPk("SE640") : bc.bcDB.labM01DB.SelectByPk("SE640");
            if (chkCentralPark.Checked)
            {
                txtPaidType.Text = "57";
                settxtPaid();
                txtDept.Text = "199";
                lbDeptName.Text = bc.bcDB.pttDB.selectDeptOPD(txtDept.Text.Trim());
                txtRemark.Focus();
                txtRemark.SelectAll();
            }
            else if (chkPCR1500.Checked)
            {
                lab.price = "1500";
                txtDept.Text = "199";
                lbDeptName.Text = bc.bcDB.pttDB.selectDeptOPD(txtDept.Text.Trim());
                txtRemark.Focus();
                txtRemark.SelectAll();
            }
            else if (chkATKFree.Checked)
            {
                lab.price = "900";
                txtPaidType.Text = "52";
                settxtPaid();
                txtDept.Text = "403";
                lbDeptName.Text = bc.bcDB.pttDB.selectDeptOPD(txtDept.Text.Trim());
                txtRemark.Focus();
                txtRemark.SelectAll();
            }
            else if (chkHI.Checked)
            {
                lab.price = "900";
                txtPaidType.Text = "46";
                settxtPaid();
                txtDept.Text = "145";
                lbDeptName.Text = bc.bcDB.pttDB.selectDeptOPD(txtDept.Text.Trim());
                txtRemark.Focus();
                txtRemark.SelectAll();
                txtSymptom.Text = "Home Isolate";
            }
            else
            {
                txtPaidType.Text = "02";
                settxtPaid();
                txtDept.Text = "199";
                lbDeptName.Text = bc.bcDB.pttDB.selectDeptOPD(txtDept.Text.Trim());
                txtRemark.Focus();
                txtRemark.SelectAll();
            }
        }
        private void Label46_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genImgStaffNote();
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                ptt = bc.bcDB.pttDB.selectPatinetByHn(txtHn.Text.Trim());
                m_txtFullNameT.Text = ptt.Name;
                m_txtID.Text = ptt.MNC_ID_NO;
                lbPttName.Text = ptt.Name;
                statusStickerIPD = true;
            }
        }

        private void BtnPatientNewF_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (flagInsert)
            {
                stt.Show("<p><b>success</b></p> <br> กรุณา กดปุ่ม clear screen", btnPatientNew);
                return;
            }
            if (txtPaidType.Text.Length <= 0)
            {
                sep.SetError(txtPaidType, "กรุณาป้อน สิทธิ");
                return;
            }
            bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNewF_Click", "start");

            ptt.MNC_CUR_TEL = txtMobile.Text.Trim();
            ptt.MNC_DOM_TEL = txtMobile.Text.Trim();
            ptt.MNC_ATT_NOTE = cboAttactNote.Text.Trim();
            ptt.MNC_FN_TYP_CD = txtPaidType.Text.Trim();
            ptt.MNC_REF_NAME = txtRefName.Text.Trim();
            ptt.MNC_REF_REL = cboRefType.Text.Trim();
            ptt.MNC_REF_ADD = txtRefAddrNo.Text.Trim();
            ptt.MNC_REF_MOO = txtRefMoo.Text.Trim();
            ptt.MNC_REF_SOI = txtRefSoi.Text.Trim();

            ptt.MNC_HN_NO = "";
            ptt.MNC_HN_YR = "";
            ptt.MNC_PFIX_CDT = "I1";
            ptt.MNC_PFIX_CDE = "";
            ptt.MNC_FNAME_T = txtPPFirstnameT.Text.Trim();
            ptt.MNC_LNAME_T = txtPPLastnameT.Text.Trim();
            ptt.MNC_FNAME_E = txtPPFirstnameE.Text.Trim();
            ptt.MNC_LNAME_E = txtPPLastnameE.Text.Trim();
            ptt.MNC_AGE = "";
            ptt.MNC_BDAY = "";
            ptt.MNC_ID_NO = txtPPID.Text.Trim();
            ptt.MNC_SS_NO = txtPPID.Text.Trim();
            ptt.MNC_SEX = sex;
            ptt.MNC_FULL_ADD = "";
            ptt.MNC_STAMP_DAT = "";
            ptt.MNC_STAMP_TIM = "";

            ptt.MNC_NAT_CD = txtNatCode.Text.Trim();
            ptt.MNC_CUR_ADD = "";
            ptt.MNC_CUR_TUM = "";
            ptt.MNC_CUR_AMP = "";
            ptt.MNC_CUR_CHW = "";
            ptt.MNC_CUR_POC = "";
            ptt.MNC_CUR_TEL = txtMobile.Text.Trim();
            ptt.MNC_DOM_ADD = "";
            ptt.MNC_DOM_TUM = "";
            ptt.MNC_DOM_AMP = "";
            ptt.MNC_DOM_CHW = "";
            ptt.MNC_DOM_POC = "";
            ptt.MNC_DOM_TEL = txtMobile.Text.Trim();
            ptt.MNC_REF_NAME = txtRefName.Text.Trim();
            ptt.MNC_REF_REL = "";
            //ptt.MNC_REF_ADD = "";
            ptt.MNC_REF_TUM = "";
            ptt.MNC_REF_AMP = "";
            ptt.MNC_REF_CHW = "";
            ptt.MNC_REF_POC = "";
            ptt.MNC_REF_TEL = txtMobile.Text.Trim();
            ptt.MNC_CUR_MOO = "";
            ptt.MNC_DOM_MOO = "";
            ptt.MNC_REF_MOO = txtRefMoo.Text.Trim();
            ptt.MNC_REF_SOI = txtRefSoi.Text.Trim();
            //ptt.MNC_REF_roa = "";
            ptt.MNC_REG_DAT = "";
            ptt.MNC_REG_TIM = "";
            ptt.MNC_CUR_SOI = "";
            ptt.MNC_DOM_SOI = "";
            ptt.MNC_REF_SOI = "";
            ptt.MNC_ATT_NOTE = cboAttactNote.Text.Trim();
            ptt.MNC_FN_TYP_CD = "";
            ptt.passport = txtPPPassport.Text.Trim();
            ptt.MNC_NAT_CD = txtNatCode.Text.Trim();
            ptt.MNC_CUR_ADD = txtRefAddrNo.Text.Trim()+" "+ txtRefMoo.Text.Trim()+" "+ txtRefSoi.Text.Trim()+" "+ txtRefRoad.Text.Trim();
            ptt.MNC_DOM_ADD = ptt.MNC_CUR_ADD;
            //ptt.MNC_REF_ADD = ptt.MNC_CUR_ADD;
            ptt.MNC_CUR_ADD = txtCurAddress.Text.Trim();
            ptt.MNC_CUR_MOO = txtCurMoo.Text.Trim();
            ptt.MNC_CUR_SOI = txtCurSoi.Text.Trim();
            ptt.MNC_CUR_ROAD = txtCurRoad.Text.Trim();
            ptt.MNC_CUR_CHW = cboProv.SelectedItem == null ? "" : ((ComboBoxItem)cboProv.SelectedItem).Value;
            ptt.MNC_CUR_AMP = cboDistrict.SelectedItem == null ? "" : ((ComboBoxItem)cboDistrict.SelectedItem).Value;
            ptt.MNC_CUR_TUM = cboSubDistrict.SelectedItem == null ? "" : ((ComboBoxItem)cboSubDistrict.SelectedItem).Value;

            ptt.MNC_SEX = m_txtGender.Text.Trim();
            ptt.MNC_FN_TYP_CD = txtPaidType.Text.Trim();
            String chkdate = "";
            DateTime chkdate1 = new DateTime();

            chkdate = ptt.patient_birthday.Length <= 0 ? m_txtBrithDate.Text.Trim() : ptt.patient_birthday;
            DateTime chkdate2 = new DateTime();
            if (!DateTime.TryParse(chkdate, out chkdate2))
            {
                String year = "", mon = "", day = "";
                String[] chk3 = chkdate.Split('-');
                if (chk3.Length >= 2)
                {
                    day = chk3[0].Trim();
                    mon = chk3[1].Trim();
                    year = chk3[2].Trim();
                }

                day = day.Length > 0 ? day : "01";
                mon = mon.Length > 0 ? mon : "01";
                year = year.Length > 0 ? year : "2500";
                int year1 = 0;
                int.TryParse(year, out year1);
                year1 -= 543;
                ptt.patient_birthday = year1.ToString() + "-" + mon + "-" + day;
                ptt.MNC_BDAY = ptt.patient_birthday;
            }
            else
            {
                ptt.patient_birthday = chkdate;
                ptt.MNC_BDAY = ptt.patient_birthday;
            }
            String re = bc.bcDB.pttDB.insertPatient(ptt);
            long chk = 0;
            if (long.TryParse(re, out chk))
            {
                //MessageBox.Show("111111", "");
                flagInsert = true;
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByPID(txtPPID.Text.Trim(), "pid");
                if (ptt.Hn.Length > 0)
                {
                    lbPttName.Text = ptt.Name;
                    txtHn.Text = ptt.MNC_HN_NO;
                    txtDOB.Text = ptt.patient_birthday;
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient success");
                    
                    
                }
            }
            else
            {
                MessageBox.Show("error " + re, "");
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient error " + re);
            }
        }

        private void TxtNatCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCurAddress.SelectAll();
                txtCurAddress.Focus();
                bc.setC1Combo(cboPPNat, txtNatCode.Text.Trim());
            }
        }

        private void TxtPPLastnameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOccupation.SelectAll();
                txtOccupation.Focus();
            }
        }

        private void TxtPPMiddlenameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPPLastnameE.SelectAll();
                txtPPLastnameE.Focus();
            }
        }

        private void TxtPPFirstnameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPPMiddlenameE.SelectAll();
                txtPPMiddlenameE.Focus();
            }
        }

        private void TxtPPMiddlenameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPPLastnameT.SelectAll();
                txtPPLastnameT.Focus();
            }
        }

        private void TxtPPLastnameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPPFirstnameE.SelectAll();
                txtPPFirstnameE.Focus();
            }
        }

        private void TxtPPFirstnameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPPMiddlenameT.SelectAll();
                txtPPMiddlenameT.Focus();
            }
        }
        private void TxtPPID_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                clearPatient();
                btnReqLab.Enabled = false;
                btnReqLab.BackColor = Color.Yellow;

                PID = txtPPID.Text.Trim();
                ptt = bc.bcDB.pttDB.selectPatinetByPID(PID, "pid");
                setPatientPP(ptt);

                txtPPPassport.SelectAll();
                txtPPPassport.Focus();
            }
        }
        private void TxtPPPassport_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                clearPatient();
                btnReqLab.Enabled = false;
                btnReqLab.BackColor = Color.Yellow;

                PID = txtPPID.Text.Trim();
                ptt = bc.bcDB.pttDB.selectPatinetByPID(PID, "passport");
                setPatientPP(ptt);

                txtPPFirstnameT.SelectAll();
                txtPPFirstnameT.Focus();
            }
        }
        private void setPatientPP(Patient ptt)
        {

            if (ptt.Hn.Length > 0)
            {
                DateTime dob = new DateTime();
                DateTime.TryParse(ptt.patient_birthday, out dob);
                if (dob.Year > 2500)
                {
                    dob = dob.AddYears(-543);
                }
                else if (dob.Year < 2000)
                {
                    dob = dob.AddYears(543);
                }
                txtHn.Text = ptt.Hn;
                txtPPFirstnameT.Text = ptt.MNC_FNAME_T;
                txtPPMiddlenameT.Text = "";
                txtPPLastnameT.Text = ptt.MNC_LNAME_T;
                txtPPFirstnameE.Text = ptt.MNC_FNAME_E;
                txtPPMiddlenameE.Text = "";
                txtPPLastnameE.Text = ptt.MNC_LNAME_E;

                lbPttName.Text = ptt.Name;
                txtDOB.Text = dob.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
                txtAddress.Text = ptt.MNC_CUR_ADD + " หมู่ที่ " + ptt.MNC_CUR_MOO + " " + ptt.MNC_CUR_SOI;
                txtProvince.Text = bc.bcDB.pttDB.selectProvinceName(ptt.MNC_CUR_CHW);
                txtAmphur.Text = bc.bcDB.pttDB.selectAmphurName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP);
                txtDistrict.Text = bc.bcDB.pttDB.selectDistrictName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP, ptt.MNC_CUR_TUM);
                //txtPPAddrNo.Text = ptt.MNC_CUR_ADD + " หมู่ที่ " + ptt.MNC_CUR_MOO + " " + ptt.MNC_CUR_SOI;


                DataTable dtpic = new DataTable();
                dtpic = bc.bcDB.pttDB.selectPatientPic(ptt.Hn);
                if (dtpic.Rows.Count > 0)
                {
                    try
                    {
                        byte[] photo_aray;
                        photo_aray = (byte[])dtpic.Rows[0]["MNC_PAT_IMG"];
                        MemoryStream ms = new MemoryStream(photo_aray);
                        ms.Position = 0;
                        picPtt.Image = Image.FromStream(ms);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                btnPatientNew.Enabled = true;
                btnPatientNew.BackColor = Color.Green;
                btnReqLab.Enabled = false;
                btnReqLab.BackColor = Color.Yellow;
                btnVisit.Enabled = true;
                btnVisit.BackColor = Color.Green;
            }
            else
            {
                fname = "";
                lname = "";
                prefixname = "";
                fnamee = "";
                lnamee = "";

                addrno = "";
                moo = "";
                trok = "";
                districtname = "";
                amphurname = "";
                provincename = "";
                //districtname = fields[(int)NID_FIELD.TUMBON].Trim();
                soi = "";
                road = "";
                address = addrno.Trim() + " " + moo.Trim() + " " + trok.Trim() + " " + soi.Trim() + " " + " " + road.Trim() + " ";

                //prefix = bc.bcDB.pttDB.selectProfixId(prefixname.Trim());
                //provincename1 = provincename.Replace("จังหวัด", "");
                //provid = bc.bcDB.pttDB.selectProvinceId(provincename1);
                //amprid = bc.bcDB.pttDB.selectAmphurId(provid, amphurname.Replace("อำเภอ", ""));
                //districtid = bc.bcDB.pttDB.selectDistrictId(provid, amprid, districtname.Replace("ตำบล", ""));
                //poc = bc.bcDB.pttDB.selectPOCId(provid, amprid, districtid);

                DateTime dob1 = new DateTime();
                DateTime.TryParse(dob, out dob1);
                //MessageBox.Show("dob " + dob, "");
                if (dob1.Year > 2500)
                {
                    dob1 = dob1.AddYears(-543);
                    //MessageBox.Show("if (dob1.Year > 2500) ", "");
                }
                //else if (dob1.Year < 2000)
                //{
                //    dob1 = dob1.AddYears(543);
                //    MessageBox.Show("else if (dob1.Year < 2000) ", "");
                //}
                btnPatientNew.Enabled = true;
                btnPatientNew.BackColor = Color.Green;
                btnVisit.Enabled = true;
                btnVisit.BackColor = Color.Green;
                btnReqLab.Enabled = false;
                btnReqLab.BackColor = Color.Yellow;
                //DateTime dob1 = new DateTime();
                //DateTime.TryParse(dob, out dob1);
                String datestart = "";
                datestart = dob1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                //MessageBox.Show("datestart "+datestart, "");
                ptt.MNC_HN_NO = "";
                ptt.MNC_HN_YR = "";
                ptt.MNC_PFIX_CDT = "";
                ptt.MNC_PFIX_CDE = "";
                ptt.MNC_FNAME_T = fname;
                ptt.MNC_LNAME_T = lname;
                ptt.MNC_FNAME_E = fnamee;
                ptt.MNC_LNAME_E = lnamee;
                ptt.MNC_AGE = "";
                ptt.MNC_BDAY = datestart;
                ptt.MNC_ID_NO = PID;
                ptt.MNC_SS_NO = PID;
                ptt.MNC_SEX = sex;
                ptt.MNC_FULL_ADD = "";
                ptt.MNC_STAMP_DAT = "";
                ptt.MNC_STAMP_TIM = "";

                ptt.MNC_NAT_CD = "";
                ptt.MNC_CUR_ADD = addrno;
                ptt.MNC_CUR_TUM = "";
                ptt.MNC_CUR_AMP = "";
                ptt.MNC_CUR_CHW = "";
                ptt.MNC_CUR_POC = "";
                ptt.MNC_CUR_TEL = txtMobile.Text.Trim();
                ptt.MNC_DOM_ADD = addrno;
                ptt.MNC_DOM_TUM = "";
                ptt.MNC_DOM_AMP = "";
                ptt.MNC_DOM_CHW = "";
                ptt.MNC_DOM_POC = "";
                ptt.MNC_DOM_TEL = txtMobile.Text.Trim();
                ptt.MNC_REF_NAME = txtRefName.Text.Trim();
                ptt.MNC_REF_REL = "";
                ptt.MNC_REF_ADD = txtRefAddrNo.Text.Trim();
                ptt.MNC_REF_TUM = "";
                ptt.MNC_REF_AMP = "";
                ptt.MNC_REF_CHW = "";
                ptt.MNC_REF_POC = "";
                ptt.MNC_REF_TEL = txtMobile.Text.Trim();
                ptt.MNC_CUR_MOO = moo.Trim();
                ptt.MNC_DOM_MOO = moo.Trim();
                ptt.MNC_REF_MOO = txtRefMoo.Text.Trim();
                ptt.MNC_REF_SOI = txtRefSoi.Text.Trim();
                //ptt.MNC_REF_roa = "";
                ptt.MNC_REG_DAT = "";
                ptt.MNC_REG_TIM = "";
                ptt.MNC_CUR_SOI = trok.Trim() + " " + soi.Trim();
                ptt.MNC_DOM_SOI = trok.Trim() + " " + soi.Trim();
                ptt.MNC_REF_SOI = "";
                ptt.MNC_ATT_NOTE = cboAttactNote.Text.Trim();
                ptt.MNC_FN_TYP_CD = "";
            }
        }
        

        private void Label60_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printLabReqNo();
        }

        private void BtnPrnQue_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printQueue();
        }

        private void BtnPttUpdate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void BtnReceipt_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void BtnReqLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String sql = "", re = "", preno = "", vsdate="", labcode="";
            long chk = 0;
            DateTime datechk = new DateTime();
            sep.Clear();

            if (ptt.MNC_HN_NO.Length < 5)
            {
                sep.SetError(m_txtID, "");
                MessageBox.Show("MNC_HN_NO", "");
                return;
            }
            if (txtDept.Text.Length <= 0)
            {
                sep.SetError(txtDept, "");
                MessageBox.Show("txtDept", "");
                return;
            }
            if (txtPreno.Text.Length <= 0)
            {
                sep.SetError(txtDept, "");
                MessageBox.Show("txtPreno", "");
                return;
            }
            if (txtDtrId.Text.Length <= 0)
            {
                sep.SetError(txtDtrId, "");
                MessageBox.Show("กรุณาป้อน ว.แพทย์", "");
                return;
            }
            if (!chkSE184.Checked && !chkSE629.Checked && !ChkSE640.Checked && !chkPCR1500.Checked)
            {
                sep.SetError(panel2, "");
                MessageBox.Show("กรุณาเลือก รายการ LAB", "");
                return;
            }
            else
            {
                //labcode = chkSE629.Checked ? "SE629" : chkSE184.Checked ? "SE184": chkPCR1500.Checked ? "SE629" : "SE640";    // lab เปลี่ยนรหัส RT-PCR ใหม่ เพราะเปลี่ยนน้ำยา
                labcode = chkSE629.Checked ? "SE649" : chkSE184.Checked ? "SE650" : chkPCR1500.Checked ? "SE649" : "SE640";
            }
            //new LogWriter("d", "BtnReqLab_Click ");
            //MessageBox.Show("6666666", "");
            if (DateTime.TryParse(txtVsdate.Text.Trim(), out datechk))
            {
                //MessageBox.Show("7777777", "");
                //new LogWriter("d", "BtnReqLab_Click 1");
                if (chkPCR1500.Checked)
                {
                    re = bc.bcDB.labT01DB.insertCOVID1500(ptt.MNC_HN_NO, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), labcode, "1618");
                }
                else
                {
                    re = bc.bcDB.labT01DB.insertCOVID(ptt.MNC_HN_NO, txtVsdate.Text.Trim(), txtPreno.Text.Trim(), labcode, "1618");
                }
                
                if (long.TryParse(re, out chk))
                {
                    stt.SetToolTip(btnReqLab, "ออก ORder Lab เรียบร้อย");
                    txtreqno.Text = re;
                    //btnReqLab.Enabled = false;
                    btnReqLab.BackColor = Color.Yellow;
                    printLabReqNo();
                    genImgStaffNote();
                    bc.bcDB.vsDB.updateStatusCloseVisit(txtHn.Text.Trim(), ptt.MNC_HN_YR, txtPreno.Text.Trim(), txtVsdate.Text.Trim());
                }
            }
            else
            {
                MessageBox.Show("88888888", "");
                sep.SetError(txtVsdate, "");
                return;
            }
        }
        private void setControl()
        {
            String deptname = "", paidname="", dtrname="";
            deptname = bc.bcDB.pttDB.selectDeptOPD("199");
            //lbPaidName.Text = deptname;
            lbDeptName.Text = deptname;
            //lbDtrName.Text = "";
            lbPaidName.Text = bc.bcDB.finM02DB.SelectpaidTypeName(txtPaidType.Text.Trim());
            lbDtrName.Text = bc.selectDoctorName(txtDtrId.Text.Trim());
            btnReqLab.Enabled = false;
            btnReqLab.BackColor = Color.Yellow;
            String quetype = "", que="";
            quetype = cboQueue.SelectedItem == null ? "" : ((ComboBoxItem)cboQueue.SelectedItem).Value;
            queue = bc.bcDB.queueDB.selectQueueNext(txtVsdate.Text, quetype);
            txtQue.Text = queue.queue_seq;
            txtStkNum.Text = bc.iniC.stickerPrintNumber;
            txtStkFontName.Text = bc.iniC.pdfFontName;
            txtStkFontSize.Text = bc.iniC.pdfFontSize;
            txtStkPrintName.Text = bc.iniC.printerSticker;

            lab = chkSE184.Checked ? bc.bcDB.labM01DB.SelectByPk("SE184") : chkSE629.Checked ? bc.bcDB.labM01DB.SelectByPk("SE629") : bc.bcDB.labM01DB.SelectByPk("SE640");
            setGrfHn(bc.iniC.station);
            txtStkDtrCode.Text = "24738";
            lbStkDtrName.Text = bc.selectDoctorName(txtStkDtrCode.Text.Trim());
            txtStkDateStart.Value = DateTime.Today;
            txtStkDateEnd.Value = DateTime.Today;
            setSymptom();
        }
        private void TxtTaxId_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                

            }
        }

        private void BtnPrnSticker_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printSticker();
        }

        private void BtnPrnLetter_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printLetter();
        }

        private void TxtMobile_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                cboAttactNote.Focus();
            }
        }

        private void TxtDtrId_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbDtrName.Text = bc.selectDoctorName(txtDtrId.Text.Trim());
            }
        }

        private void BtnPrnStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printStaffNote();
        }

        private void TxtDept_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbDeptName.Text = bc.bcDB.pttDB.selectDeptOPD(txtDept.Text.Trim());
                txtRemark.Focus();
                txtRemark.SelectAll();
            }
        }

        private void TxtSymptom_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtDept.Focus();
                txtDept.SelectAll();
            }
        }

        private void BtnVisit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String sql = "", re="", preno="";
            long chk = 0;
            sep.Clear();
            if (ptt.MNC_HN_NO.Length < 5)
            {
                sep.SetError(m_txtID, "");
                return;
            }
            if (txtDept.Text.Length <= 0)
            {
                sep.SetError(txtDept, "");
                return;
            }
            if (chkHI.Checked)
            {
                re = bc.bcDB.vsDB.insertVisitHI(ptt.MNC_HN_NO, txtPaidType.Text.Trim(), txtSymptom.Text.Trim(), txtDept.Text.Trim(), txtRemark.Text.Trim(), txtDtrId.Text.Trim(), "1618");
            }
            else
            {
                re = bc.bcDB.vsDB.insertVisit(ptt.MNC_HN_NO, txtPaidType.Text.Trim(), txtSymptom.Text.Trim(), txtDept.Text.Trim(), txtRemark.Text.Trim(), txtDtrId.Text.Trim(), "1618");
            }
            
            if(long.TryParse(re, out chk))
            {
                stt.SetToolTip(btnVisit, "ออก VISIT เรียบร้อย");
                preno = re;
                Visit vs = new Visit();
                vs = bc.bcDB.vsDB.selectVisit(ptt.MNC_HN_NO, txtVsdate.Text.Trim(), preno);
                txtPreno.Text = vs.preno;
                txtVN.Text = vs.vn + "." + vs.vnseq + "." + vs.vnsum;
                queue.queue_seq = txtQue.Text.Trim();
                new LogWriter("d", "BtnVisit_Click queue.queue_id " + queue.queue_id);
                bc.bcDB.queueDB.insertQueue(queue, "");
                printQueue();
            }
            btnVisit.Enabled = false;
            btnVisit.BackColor = Color.Yellow;
            btnReqLab.Enabled = true;
            btnReqLab.BackColor = Color.Green;
        }
        private void printLetter()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerLeter;
            document.PrintPage += Document_PrintPage_Letter;
            document.DefaultPageSettings.Landscape = true;

            document.Print();
        }
        private void Document_PrintPage_Letter(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 120;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }

            line = "H.N. " + ptt.MNC_HN_NO;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, avg , yPos + 5, flags);

            line = "Name " + ptt.Name;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, avg , yPos + 25, flags);

            line = "PID " + m_txtID.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, avg , yPos + 45, flags);

            line = "Mobile " + txtMobile.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, avg, yPos + 65, flags);
        }
        private void printQueue()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerQueue;
            document.PrintPage += Document_PrintPage_Queue;
            document.DefaultPageSettings.Landscape = false;
            
            document.Print();
                
        }
        private void Document_PrintPage_Queue(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }
            String quetype = cboQueue.SelectedItem == null ? "" : ((ComboBoxItem)cboQueue.SelectedItem).Text;
            line = quetype;
            e.Graphics.DrawString(line, fque, Brushes.Black, 15, yPos + 5, flags);

            line = "H.N. " + ptt.MNC_HN_NO;
            e.Graphics.DrawString(line, fqueB, Brushes.Black, 15, yPos + 25, flags);

            //line = "Name " + ptt.Name;
            line = ptt.Name;
            e.Graphics.DrawString(line, fque, Brushes.Black, 5, yPos + 65, flags);

            line = txtQue.Text.Trim();
            e.Graphics.DrawString(line, ftotal, Brushes.Black, 5, yPos + 65, flags);

            line = "  " ;
            e.Graphics.DrawString(line, ftotal, Brushes.Black, 15, yPos + 85, flags);

            line = "  ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.Left);
            e.Graphics.DrawString(line, ftotal, Brushes.Black, 15, yPos + 105, flags);

            line = "print time  "+ date;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.Left);
            //e.Graphics.DrawString(line, famtB, Brushes.Black, 15, yPos + 185, flags);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, 10, yPos + 185, flags);

            Image qrcodeLineBng5 = Resources.qrcode_657bkkyq;
            float newWidth = qrcodeLineBng5.Width * 100 / qrcodeLineBng5.HorizontalResolution;
            float newHeight = qrcodeLineBng5.Height * 100 / qrcodeLineBng5.VerticalResolution;

            float widthFactor = newWidth / e.MarginBounds.Width;
            float heightFactor = newHeight / e.MarginBounds.Height;

            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    widthFactor = 1;
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                    newWidth = newWidth / float.Parse("4.2");
                    newHeight = newHeight / float.Parse("4.2");
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }
            //e.Graphics.DrawImage(img, 10, 20, (int)newWidth, (int)newHeight);@657bkkyq

            line = "add line @bangna5 โรงพยาบาล บางนา5";
            e.Graphics.DrawString(line, fEditS, Brushes.Black, 2, yPos + 200, flags);
            line = "สอบถามผลตรวจ covid";
            e.Graphics.DrawString(line, fEditS, Brushes.Black, 10, yPos + 220, flags);
            line = "พิมพ์ covid เว้นวรรค เลขที่บัตรประชาชน";
            e.Graphics.DrawString(line, fEditS, Brushes.Black, 10, yPos + 240, flags);
            line = "ตัวอย่าง   covid  123456789012";
            e.Graphics.DrawString(line, fEditS, Brushes.Black, 10, yPos + 260, flags);

            e.Graphics.DrawImage(qrcodeLineBng5,15,290,(int)newWidth, (int)newHeight);
            //line = "Mobile " + txtMobile.Text.Trim();
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 + 25, yPos + 65, flags);
        }
        private void printSticker()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerSticker;
            document.PrintPage += Document_PrintPage_Sticker;
            document.DefaultPageSettings.Landscape = false;
            int num = 0;
            if(int.TryParse(txtStickerNum.Text.Trim(), out num))
            {
                document.PrinterSettings.Copies = short.Parse(num.ToString());
                //for(int i = 0; i < num; i++)
                //{
                document.Print();
                //}
            }
            else
            {
                sep.SetError(txtStickerNum, "");
            }
        }
        private void Document_PrintPage_Sticker(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }
            if (statusStickerIPD)
            {
                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.selectPttinWard1(txtHn.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    line = "H.N. " + ptt.MNC_HN_NO + " " + dt.Rows[0]["MNC_AN_NO"].ToString() + "." + dt.Rows[0]["MNC_AN_yr"].ToString();
                    e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 5, flags);
                    line = ptt.Name;
                    e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 25, flags);
                    //line = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString() + " " + dt.Rows[0]["MNC_RM_NAM"].ToString() + " " + dt.Rows[0]["MNC_BD_NO"].ToString();//MNC_AN_NO
                    line = ptt.AgeString();
                    e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 45, flags);

                    line = "H.N. " + ptt.MNC_HN_NO + " " + dt.Rows[0]["MNC_AN_NO"].ToString() + "." + dt.Rows[0]["MNC_AN_yr"].ToString();
                    e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 65, flags);
                    line = ptt.Name;
                    e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 85, flags);
                    //line = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString() + " " + dt.Rows[0]["MNC_RM_NAM"].ToString() + " " + dt.Rows[0]["MNC_BD_NO"].ToString();
                    line = ptt.AgeString();
                    e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 105, flags);
                }
            }
            else
            {
                line = "H.N. " + ptt.MNC_HN_NO + " " + ptt.AgeStringShort1();
                //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 5, flags);

                line = ptt.Name;
                //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEditS, Brushes.Black, 15, yPos + 25, flags);
                line = "PID " + m_txtID.Text.Trim();
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 45, flags);
            }
            statusStickerIPD = false;
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);

            //e.Graphics.DrawString(line, fEdit, Brushes.Black, 15, yPos + 45, flags);

        }
        private void printLabReqNo()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA5;
            document.PrintPage += Document_PrintPage_LabReqNo;
            document.DefaultPageSettings.Landscape = false;

            document.Print();
        }
        private void Document_PrintPage_LabReqNo(object sender, PrintPageEventArgs e){
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, gap = 6,gapLine = 25, colName = 0, col2 = 25, col3 = 350, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 620, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 35;
                col3 = 300;
                col4 = 630;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }
            
            line = "โรงพยาบาล บางนา5 " ;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, famtB, Brushes.Black, col2, yPos + 5, flags);

            line = "ชื่อผู้ตรวจ " + ptt.Name;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 5, flags);
            line = "อายุ " + ptt.AgeStringShort1();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 5, flags);

            yPos += gapLine;
            line = "HN " + ptt.MNC_HN_NO;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos, flags);
            line = "VN " + txtVN.Text;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos, flags);
            line = "page 1/1 ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 + textSize.Width + 5, yPos, flags);

            yPos += gapLine;
            line = "queue " + txtQue.Text.Trim();
            e.Graphics.DrawString(line, famtB, Brushes.Black, col2, yPos - 20, flags);
            line = "req no " + txtreqno.Text+" "+txtVsdate.Text;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos, flags);
            line = "req date " + DateTime.Now.ToString("dd/MM/yyyy HH:MM");
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos, flags);

            yPos += gapLine;
            line = "doctor " + txtDtrId.Text+" " + lbDtrName.Text;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos, flags);
            line = "visit date " + DateTime.Now.ToString("dd/MM/yyyy HH:MM");
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos, flags);

            yPos += gapLine;
            //line = "queue " + txtQue.Text.Trim();
            //e.Graphics.DrawString(line, famtB, Brushes.Black, col2, yPos-20, flags);
            line = "บริษัท " + txtCompName.Text;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos, flags);
            line = "print date " + DateTime.Now.ToString("dd/MM/yyyy HH:MM");
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos, flags);
            //yPos += gapLine;
            line = "ใบสั่งทำการ LAB ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, famtB, Brushes.Black, col2, yPos, flags);

            yPos += gapLine;
            line = "............................................................................................................................................................................................. ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos -5, flags);
            line = "STAT";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2+20, yPos + 15, flags);
            line = "DESCRIPTION";
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 15, flags);
            line = "PRICE";
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3+300, yPos + 15, flags);
            line = "............................................................................................................................................................................................. ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 30, flags);

            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;

            line = lab.MNC_LB_DSC;
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2+100, yPos, flags);
            double price2 = 0;
            double.TryParse(lab.price, out price2);
            line = price2.ToString("#,###.00");
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 + 300, yPos, flags);

            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            yPos += gapLine;
            line = "recorder   admin    ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos , flags);

            line = "............................................................................................................................................................................................. ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos - 15, flags);
            line = "............................................................................................................................................................................................. ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 10, flags);

            yPos += gapLine;
            line = "FM-NUR-212 (00-08/04/59) (1/1)";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos , flags);
        }
        private void printStaffNote()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerStaffNote;
            if (chkHI.Checked)
            {
                document.PrintPage += Document_PrintPage_StaffNote_HI;
                document.DefaultPageSettings.Landscape = true;
            }
            else
            {
                document.PrintPage += Document_PrintPage_StaffNote;
                document.DefaultPageSettings.Landscape = true;
            }
            

            document.Print();
        }
        private void Document_PrintPage_StaffNote_HI(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }

            line = "5";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fPrn, Brushes.Black, xOffset, yPos, new StringFormat());leftMargin
            e.Graphics.DrawString(line, famtB, Brushes.Black, col3, yPos, flags);
            e.Graphics.DrawString(line, famtB, Brushes.Black, col4, yPos, flags);
            line = "H.N. " + ptt.MNC_HN_NO + "     " + txtVN.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 + 25, yPos + 5, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 + 30, yPos + 5, flags);

            line = "ชื่อ " + ptt.Name;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 + 20, yPos + 20, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 + 10, yPos + 20, flags);
            line = "เลขที่บัตร " + ptt.MNC_ID_NO;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 40, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 40, flags);
            line = lbPaidName.Text;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 40, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 40, flags);

            line = txtCompName.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 40, flags);

            line = "โรคประจำตัว        ไม่มี";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 60, flags);
            rec = new Rectangle(col2int + 75, 72, recx, recy);
            e.Graphics.DrawRectangle(blackPen, rec);

            line = "อายุ " + ptt.AgeStringShort1() + " [" + bc.datetoShow(ptt.MNC_BDAY) + "]";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 60, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 60, flags);
            //line = lbPaidName.Text.Trim();
            //textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);

            line = "มีโรค ระบุ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 70, yPos + 80, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 67 - recx, 92, recx, recy));

            line = "วันที่เวลา " + date;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 80, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 80, flags);

            line = "โรคเรื้อรัง";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 100, flags);
            line = "ชื่อแพทย์ " + txtDtrId.Text.Trim() + " " + lbDtrName.Text;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 100, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 - 50, yPos + 120, flags);

            line = "DR Time.                               ปิดใบยา";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 120, flags);

            line = "อาการเบื้องต้น " + txtSymptom.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 120, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 100, flags);

            line = "Temp";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 140, flags);

            line = "H.Rate";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 80, yPos + 140, flags);
            line = "R.Rate";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 160, yPos + 140, flags);
            line = "BP1";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 240, yPos + 140, flags);
            line = "Time :";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 300, yPos + 140, flags);
            line = "BP2 ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 380, yPos + 140, flags);
            line = "Time :";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 440, yPos + 140, flags);

            line = "Wt.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 160, flags);
            line = "Ht.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 80, yPos + 160, flags);
            line = "BMI.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 100, yPos + 160, flags);
            line = "CC.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 180, yPos + 160, flags);
            line = "CC.IN";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 240, yPos + 160, flags);
            line = "CC.EX";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 300, yPos + 160, flags);
            line = "Ab.C";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 400, yPos + 160, flags);
            line = "H.C.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 460, yPos + 160, flags);

            line = "Precaution (Med) _________________________________________ ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = "แพ้ยา/อาหาร/อื่นๆ         ไม่มี";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 180, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 180, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 123 - recx, yPosint + 180, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx, yPosint + 180, recx, recy));
            line = "มี ระบุอาการ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 20, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, yPosint + 200, recx, recy));
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 15, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 15 - recx, yPosint + 200, recx, recy));

            //line = "อาการเบื้อต้น  "+ txtSymptom.Text;
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 10, yPos + 220, flags);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = "O2 Sat __________        Pain Score __________";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 220, flags);

            line = txtRemark.Text + " " + lbDeptName.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 40, yPos + 260, flags);

            line = "Medication                       No Medication";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 50, yPos + 280, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 30 - recx - 5, yPosint + 280, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx + 60, yPosint + 280, recx, recy));

            line = txtSymptom.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col3 + 40, yPos + 315, flags);
            
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 40, yPos + 330, flags);

            line = "อาการ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 360, flags);
            line = "เอกซเรย์ปอด";
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 40, yPos + 360, flags);

            line = "สัมผัสผู้ป่วย ชื่อ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 430, flags);
            line = "Set ";
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 40, yPos + 430, flags);

            line = "สัมผัสล่าสุด";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 475, flags);
            line = "เครื่องวัดออกซิเจน ปลายนิ้ว    1";
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 40, yPos + 475, flags);
            line = "ปรอทวัดอุณหภูมิ     1";
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 40, yPos + 515, flags);

            line = "คำแนะนำ       การออกกำลังกาย               การรับประทานอาหารที่ถูกสัดส่วน";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 620, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 75 - recx, yPosint + 620, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 210 - recx, yPosint + 620, recx, recy));

            line = "การตรวจสุขภาพประจำปี          การพบแพทย์เฉพาะทาง       อื่นๆ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 640, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 35 - recx, yPosint + 640, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 190 - recx, yPosint + 640, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 3350 - recx, yPosint + 640, recx, recy));

            line = "ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 660, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 170 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 215 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 345 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 385 - recx, yPosint + 660, recx, recy));

            line = "ชื่อผู้รับ _____________________________";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 680, flags);

            line = "Health Education :";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 730, flags);

            line = "ลงชื่อพยาบาล: _____________________________________";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 750, flags);

            line = "FM-REC-002 (00 10/09/53)(1/1)";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 770, flags);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col40, yPos + 770, flags);
        }
        private void Document_PrintPage_StaffNote(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col3=250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4=820, col40=620;
            int count = 0, recx=15, recy=15, col2int=0, yPosint=0, col40int=0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }

            line = "5";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fPrn, Brushes.Black, xOffset, yPos, new StringFormat());leftMargin
            e.Graphics.DrawString(line, famtB, Brushes.Black, col3, yPos, flags);
            e.Graphics.DrawString(line, famtB, Brushes.Black, col4, yPos, flags);
            line = "H.N. "+ptt.MNC_HN_NO+"     "+txtVN.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3+25, yPos+5, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4+30, yPos+5, flags);

            line = "ชื่อ " + ptt.Name;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3+20, yPos+20, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4+10, yPos + 20, flags);
            line = "เลขที่บัตร " + ptt.MNC_ID_NO;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 , yPos + 40, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 , yPos + 40, flags);
            line = lbPaidName.Text;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 40, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 40, flags);

            line = txtCompName.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 40, flags);

            line = "โรคประจำตัว        ไม่มี";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 60, flags);
            rec = new Rectangle(col2int + 75, 72, recx, recy);
            e.Graphics.DrawRectangle(blackPen, rec);

            line = "อายุ " + ptt.AgeStringShort1() +" ["+bc.datetoShow(ptt.MNC_BDAY)+"]";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 , yPos + 60, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 60, flags);
            //line = lbPaidName.Text.Trim();
            //textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);

            line = "มีโรค ระบุ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2+70, yPos + 80, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 67 - recx, 92, recx, recy));

            line = "วันที่เวลา " + date;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 , yPos + 80, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 80, flags);

            line = "โรคเรื้อรัง";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 100, flags);
            line = "ชื่อแพทย์ " + txtDtrId.Text.Trim()+" "+lbDtrName.Text;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 , yPos + 100, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 - 50, yPos + 120, flags);

            line = "DR Time.                               ปิดใบยา";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 120, flags);

            line = "อาการเบื้องต้น " + txtSymptom.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 120, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 100, flags);

            line = "Temp";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 140, flags);

            line = "H.Rate";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 80, yPos + 140, flags);
            line = "R.Rate";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 160, yPos + 140, flags);
            line = "BP1";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 240, yPos + 140, flags);
            line = "Time :";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 300, yPos + 140, flags);
            line = "BP2 ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 380, yPos + 140, flags);
            line = "Time :";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 440, yPos + 140, flags);

            line = "Wt.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 160, flags);
            line = "Ht.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2+80, yPos + 160, flags);
            line = "BMI.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 100, yPos + 160, flags);
            line = "CC.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 180, yPos + 160, flags);
            line = "CC.IN";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 240, yPos + 160, flags);
            line = "CC.EX";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 300, yPos + 160, flags);
            line = "Ab.C";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 400, yPos + 160, flags);
            line = "H.C.";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 460, yPos + 160, flags);

            line = "Precaution (Med) _________________________________________ ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = "แพ้ยา/อาหาร/อื่นๆ         ไม่มี";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 , yPos + 180, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 180, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 123 - recx, yPosint + 180, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx, yPosint + 180, recx, recy));
            line = "มี ระบุอาการ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 +20, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, yPosint + 200, recx, recy));
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 15, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 15 - recx, yPosint + 200, recx, recy));

            //line = "อาการเบื้อต้น  "+ txtSymptom.Text;
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 10, yPos + 220, flags);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = "O2 Sat __________        Pain Score __________";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 , yPos + 220, flags);

            line = txtRemark.Text+" "+lbDeptName.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 40, yPos + 260, flags);

            line = "Medication                       No Medication";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40+50, yPos + 280, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 30 - recx -5, yPosint + 280, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx +60, yPosint + 280, recx, recy));

            line = txtSymptom.Text.Trim();
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col3 + 40, yPos + 315, flags);

            if (chkSE629.Checked)
            {
                //line = "RT-PCR COVID-19  2700.00";
                line = lab.MNC_LB_DSC + " " + lab.price;
            }
            else if (chkSE184.Checked)
            {
                //line = "RT-PCR COVID-19  2700.00";
                line = lab.MNC_LB_DSC + " จีน " + lab.price;
            }
            else if (chkPCR1500.Checked)
            {
                //line = "RT-PCR COVID-19  2700.00";
                line = lab.MNC_LB_DSC + " 1500 ";
            }
            else
            {
                line = lab.MNC_LB_DSC+" "+lab.price;
            }
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col40 + 40, yPos + 330, flags);

            line = "อาการ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 360, flags);

            line = "สัมผัสผู้ป่วย ชื่อ";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 430, flags);

            line = "สัมผัสล่าสุด";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 475, flags);


            line = "คำแนะนำ       การออกกำลังกาย               การรับประทานอาหารที่ถูกสัดส่วน" ;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 , yPos + 620, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 75 - recx, yPosint + 620, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 210 - recx, yPosint + 620, recx, recy));

            line = "การตรวจสุขภาพประจำปี          การพบแพทย์เฉพาะทาง       อื่นๆ" ;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 +40, yPos + 640, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 35 - recx, yPosint + 640, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 190 - recx, yPosint + 640, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 3350 - recx, yPosint + 640, recx, recy));

            line = "ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________" ;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 +40, yPos + 660, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 170 - recx, yPosint + 660 , recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 215 - recx, yPosint + 660 , recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 345 - recx, yPosint + 660 , recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 385 - recx, yPosint + 660 , recx, recy));

            line = "ชื่อผู้รับ _____________________________";
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 , yPos + 680, flags);

            line = "Health Education :" ;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 , yPos + 730, flags);

            line = "ลงชื่อพยาบาล: _____________________________________" ;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 , yPos + 750, flags);

            line = "FM-REC-002 (00 10/09/53)(1/1)" ;
            textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 , yPos + 770, flags);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col40, yPos + 770, flags);
        }

        private void M_txtID_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                ptt = bc.bcDB.pttDB.selectPatinetByPID(m_txtID.Text.Trim(),"pid");
                if (ptt.Hn.Length > 0)
                {
                    DateTime dob = new DateTime();
                    DateTime.TryParse(ptt.patient_birthday, out dob);
                    if (dob.Year > 2500)
                    {
                        dob = dob.AddYears(-543);
                    }
                    else if (dob.Year < 2000)
                    {
                        dob = dob.AddYears(543);
                    }
                    txtHn.Text = ptt.Hn;
                    lbPttName.Text = ptt.Name;
                    txtDOB.Text = dob.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
                    txtAddress.Text = ptt.MNC_CUR_ADD + " หมู่ที่ " + ptt.MNC_CUR_MOO + " " + ptt.MNC_CUR_SOI;
                    txtProvince.Text = bc.bcDB.pttDB.selectProvinceName(ptt.MNC_CUR_CHW);
                    txtAmphur.Text = bc.bcDB.pttDB.selectAmphurName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP);
                    txtDistrict.Text = bc.bcDB.pttDB.selectDistrictName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP, ptt.MNC_CUR_TUM);
                    DataTable dtpic = new DataTable();
                    dtpic = bc.bcDB.pttDB.selectPatientPic(ptt.Hn);
                    if (dtpic.Rows.Count > 0)
                    {
                        try
                        {
                            byte[] photo_aray;
                            photo_aray = (byte[])dtpic.Rows[0]["MNC_PAT_IMG"];
                            MemoryStream ms = new MemoryStream(photo_aray);
                            ms.Position = 0;
                            picPtt.Image = Image.FromStream(ms);
                            if (ImageFormat.Jpeg.Equals(picPtt.Image.RawFormat))
                            {
                                // JPEG
                            }
                            else if (ImageFormat.Png.Equals(picPtt.Image.RawFormat))
                            {
                                // PNG
                            }
                            else if (ImageFormat.Gif.Equals(picPtt.Image.RawFormat))
                            {
                                // GIF
                            }
                            else if (ImageFormat.Bmp.Equals(picPtt.Image.RawFormat))
                            {
                                // 
                            }
                            else if (ImageFormat.Tiff.Equals(picPtt.Image.RawFormat))
                            {
                                // 
                            }
                            else if (ImageFormat.Exif.Equals(picPtt.Image.RawFormat))
                            {
                                // 
                            }
                            else if (ImageFormat.Emf.Equals(picPtt.Image.RawFormat))
                            {
                                // GIF
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    btnPatientNew.Enabled = false;
                    btnPatientNew.BackColor = Color.Yellow;
                }
                else
                {
                    ptt = bc.bcDB.pttDB.selectPatient(m_txtID.Text.Trim());
                    if (ptt.Hn.Length > 0)
                    {
                        DateTime dob = new DateTime();
                        DateTime.TryParse(ptt.patient_birthday, out dob);
                        if (dob.Year > 2500)
                        {
                            dob = dob.AddYears(-543);
                        }
                        else if (dob.Year < 2000)
                        {
                            dob = dob.AddYears(543);
                        }
                        txtHn.Text = ptt.Hn;
                        lbPttName.Text = ptt.Name;
                        txtDOB.Text = dob.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
                        txtAddress.Text = ptt.MNC_CUR_ADD + " หมู่ที่ " + ptt.MNC_CUR_MOO + " " + ptt.MNC_CUR_SOI;
                        txtProvince.Text = bc.bcDB.pttDB.selectProvinceName(ptt.MNC_CUR_CHW);
                        txtAmphur.Text = bc.bcDB.pttDB.selectAmphurName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP);
                        txtDistrict.Text = bc.bcDB.pttDB.selectDistrictName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP, ptt.MNC_CUR_TUM);
                        DataTable dtpic = new DataTable();
                        dtpic = bc.bcDB.pttDB.selectPatientPic(ptt.Hn);
                        if (dtpic.Rows.Count > 0)
                        {
                            try
                            {
                                byte[] photo_aray;
                                photo_aray = (byte[])dtpic.Rows[0]["MNC_PAT_IMG"];
                                MemoryStream ms = new MemoryStream(photo_aray);
                                ms.Position = 0;
                                picPtt.Image = Image.FromStream(ms);
                                if (ImageFormat.Jpeg.Equals(picPtt.Image.RawFormat))
                                {
                                    // JPEG
                                }
                                else if (ImageFormat.Png.Equals(picPtt.Image.RawFormat))
                                {
                                    // PNG
                                }
                                else if (ImageFormat.Gif.Equals(picPtt.Image.RawFormat))
                                {
                                    // GIF
                                }
                                else if (ImageFormat.Bmp.Equals(picPtt.Image.RawFormat))
                                {
                                    // 
                                }
                                else if (ImageFormat.Tiff.Equals(picPtt.Image.RawFormat))
                                {
                                    // 
                                }
                                else if (ImageFormat.Exif.Equals(picPtt.Image.RawFormat))
                                {
                                    // 
                                }
                                else if (ImageFormat.Emf.Equals(picPtt.Image.RawFormat))
                                {
                                    // GIF
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        btnPatientNew.Enabled = false;
                        btnPatientNew.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void TxtPaidType_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            sep.Clear();
            if (e.KeyCode== Keys.Enter)
            {
                settxtPaid();
            }
        }
        private void settxtPaid()
        {
            lbPaidName.Text = "";
            String paidname = "";
            paidname = bc.bcDB.finM02DB.SelectpaidTypeName(txtPaidType.Text.Trim());
            lbPaidName.Text = paidname;
            txtPaidType.SelectAll();
            txtSymptom.Focus();
        }
        private void BtnPatientNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (flagInsert)
            {
                stt.Show("<p><b>success</b></p> <br> กรุณา กดปุ่ม clear screen", btnPatientNew);
                return;
            }
            if (txtPaidType.Text.Length <= 0)
            {
                sep.SetError(txtPaidType, "กรุณาป้อน สิทธิ");
                return;
            }
            bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "start");
            //Patient ptt = new Patient();

            ptt.MNC_CUR_TEL = txtMobile.Text.Trim();
            ptt.MNC_DOM_TEL = txtMobile.Text.Trim();
            ptt.MNC_ATT_NOTE = cboAttactNote.Text.Trim();
            ptt.MNC_FN_TYP_CD = txtPaidType.Text.Trim();
            ptt.MNC_REF_NAME = txtRefName.Text.Trim();
            ptt.MNC_REF_REL = cboRefType.Text.Trim();
            ptt.MNC_REF_ADD = txtRefAddrNo.Text.Trim();
            ptt.MNC_REF_MOO = txtRefMoo.Text.Trim();
            ptt.MNC_REF_SOI = txtRefSoi.Text.Trim();

            String chkdate = "";
            DateTime chkdate1 = new DateTime();

            chkdate = ptt.patient_birthday.Length<=0 ? m_txtBrithDate.Text.Trim() : ptt.patient_birthday;
            DateTime chkdate2 = new DateTime();
            if(!DateTime.TryParse(chkdate, out chkdate2))
            {
                String year = "", mon="", day="";
                String[] chk3 = chkdate.Split('-');
                if (chk3.Length >= 2)
                {
                    day = chk3[0].Trim();
                    mon = chk3[1].Trim();
                    year = chk3[2].Trim();
                }

                day = day.Length>0 ? day : "01";
                mon = mon.Length > 0 ? mon : "01";
                year = year.Length > 0 ? year : "2500";
                int year1 = 0;
                int.TryParse(year, out year1);
                year1 -= 543;
                ptt.patient_birthday = year1.ToString() + "-" + mon + "-" + day;
                ptt.MNC_BDAY = ptt.patient_birthday;
            }
            //ptt.mnc_ref

            //String re = bc.bcDB.pttDB.insert(ptt);
            String re = bc.bcDB.pttDB.insertPatient(ptt);
            long chk = 0;
            if(long.TryParse(re, out chk))
            {
                //MessageBox.Show("111111", "");
                flagInsert = true;
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByPID(m_txtID.Text.Trim(), "pid");
                if (ptt.Hn.Length > 0)
                {
                    lbPttName.Text = ptt.Name;
                    txtHn.Text = ptt.MNC_HN_NO;
                    txtDOB.Text = ptt.patient_birthday;
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient success");
                    chk = 0;
                    //Image img = picPtt.Image;
                    //var bmp = (Bitmap)img;
                    //byte[] arr;
                    //ImageConverter converter = new ImageConverter();
                    ////arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    //arr = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

                    MemoryStream ms = new MemoryStream();
                    picPtt.Image.Save(ms, ImageFormat.Bmp);
                    byte[] photo_aray = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);
                    //img = Image.FromStream(ms);
                    String re1 = "1";
                    
                    //stt.Show("<p><b>success</b></p> <br> ส่งข้อมูล เข้าระบบโรงพยาบาลเรียบร้อย", btnPatientNew, 10);
                    re1 = bc.bcDB.pttDB.insertPatientImage(ptt.MNC_HN_NO, ptt.MNC_HN_YR, photo_aray);     //      insert ได้เรียบร้อย แต่ใน โปรแกรม คุณbee มีerror เรื่องรูป
                    if (long.TryParse(re1, out chk))
                    {
                        bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient image success");
                        stt.Show("<p><b>success</b></p> <br> ส่งข้อมูล เข้าระบบโรงพยาบาลเรียบร้อย", btnPatientNew,10);
                        btnPatientNew.Enabled = false;
                        btnPatientNew.BackColor = Color.Yellow;
                    }
                    else
                    {
                        stt.Show("<p><b>success image erro</b></p> <br> insert patient success image error  .", btnPatientNew);
                        bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient image error");
                    }
                }
            }
            else
            {
                MessageBox.Show("error "+re, "");
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient error "+re);
            }
        }

        public IntPtr selectReader(String reader)
        {
            IntPtr mCard = (IntPtr)0;
            byte[] _reader = String2Byte(reader);
            IntPtr res = (IntPtr)RDNID.selectReaderRD(_reader);
            if ((Int64)res > 0)
                mCard = (IntPtr)res;
            return mCard;
        }

        private void ListCardReader()
        {
            try
            {
                byte[] szReaders = new byte[1024 * 2];
                int size = szReaders.Length;
                int numreader = RDNID.getReaderListRD(szReaders, size);
                if (numreader <= 0)
                    return;
                String s = aByteToString(szReaders);
                String[] readlist = s.Split(';');
                if (readlist != null)
                {
                    for (int i = 0; i < readlist.Length; i++)
                        m_ListReaderCard.Items.Add(readlist[i]);
                    m_ListReaderCard.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        static string aByteToString(byte[] b)
        {
            Encoding ut = Encoding.GetEncoding(874); // 874 for Thai langauge
            int i;
            for (i = 0; b[i] != 0; i++) ;

            string s = ut.GetString(b);
            s = s.Substring(0, i);
            return s;
        }

        static byte[] String2Byte(string s)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.GetEncoding(874);
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte array.
            byte[] unicodeBytes = unicode.GetBytes(s);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            return asciiBytes;
        }

        String _yyyymmdd_(String d)
        {
            string s = "";
            string _yyyy = d.Substring(0, 4);
            string _mm = d.Substring(4, 2);
            string _dd = d.Substring(6, 2);


            string[] mm = { "", "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
            string _tm = "-";
            if (_mm == "00")
            {
                _tm = "-";
            }
            else if (_mm == "99")
            {
                _tm = "-";
            }
            else
            {                
                _tm = mm[int.Parse(_mm)];
            }
            if (_yyyy == "0000")
                _yyyy = "-";

            if (_dd == "00")
                _dd = "-";

            s = _dd + " " + _tm + " " + _yyyy;
            return s;
        }


        enum NID_FIELD
        {
            NID_Number,   //1234567890123#

            TITLE_T,    //Thai title#
            NAME_T,     //Thai name#
            MIDNAME_T,  //Thai mid name#
            SURNAME_T,  //Thai surname#

            TITLE_E,    //Eng title#
            NAME_E,     //Eng name#
            MIDNAME_E,  //Eng mid name#
            SURNAME_E,  //Eng surname#

            HOME_NO,    //12/34#
            MOO,        //10#
            TROK,       //ตรอกxxx#
            SOI,        //ซอยxxx#
            ROAD,       //ถนนxxx#
            TUMBON,     //ตำบลxxx#
            AMPHOE,     //อำเภอxxx#
            PROVINCE,   //จังหวัดxxx#

            GENDER,     //1#			//1=male,2=female

            BIRTH_DATE, //25200131#	    //YYYYMMDD 
            ISSUE_PLACE,//xxxxxxx#      //
            ISSUE_DATE, //25580131#     //YYYYMMDD 
            EXPIRY_DATE,//25680130      //YYYYMMDD 
            ISSUE_NUM,  //12345678901234 //14-Char
            END            
        };
        private void ListCardReader1()
        {
            byte[] szReaders = new byte[1024 * 2];
            int size = szReaders.Length;
            int numreader = RDNID.getReaderListRD(szReaders, size);
            if (numreader <= 0)
                return;
            String s = aByteToString(szReaders);
            String[] readlist = s.Split(';');
            if (readlist != null)
            {
                for (int i = 0; i < readlist.Length; i++)
                    //m_ListReaderCard.Items.Add(readlist[i]);
                    lSmartCard.Add(readlist[i]);
                //m_ListReaderCard.SelectedIndex = 0;
            }
        }
        public IntPtr selectReader1(String reader)
        {
            IntPtr mCard = (IntPtr)0;
            byte[] _reader = String2Byte(reader);
            IntPtr res = (IntPtr)RDNID.selectReaderRD(_reader);
            if ((Int64)res > 0)
                mCard = (IntPtr)res;
            return mCard;
        }
        protected int ReadCard1()
        {
            try
            {
                string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                string fileName = StartupPath + "\\RDNIDLib.DLD";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("RDNIDLib.DLD not found");
                }
                byte[] _lic = String2Byte(fileName);
                int nres = 0;
                nres = RDNID.openNIDLibRD(_lic);
                if (nres != 0)
                {
                    String m;
                    m = String.Format(" error no {0} ", nres);
                    MessageBox.Show(m);
                }
                byte[] Licinfo = new byte[1024];
                RDNID.getLicenseInfoRD(Licinfo);
                byte[] Softinfo = new byte[1024];
                RDNID.getSoftwareInfoRD(Softinfo);
                ListCardReader();
                if (lSmartCard == null) return 0;
                if (lSmartCard.Count <= 0) return 0;
                String strTerminal = lSmartCard[0];
                //byte[] Licinfo = new byte[1024];
                //RDNID.getLicenseInfoRD(Licinfo);
                //m_lblDLXInfo.Text = aByteToString(Licinfo);
                //String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);
                //_CardReaderTFK2700 = ic.ListCardReader();
                //String strTerminal = _CardReaderTFK2700;
                IntPtr obj = selectReader(strTerminal);

                Int32 nInsertCard = 0;
                nInsertCard = RDNID.connectCardRD(obj);
                if (nInsertCard != 0)
                {
                    String m;
                    m = String.Format(" error no {0} ", nInsertCard);
                    MessageBox.Show(m);

                    RDNID.disconnectCardRD(obj);
                    RDNID.deselectReaderRD(obj);
                    return nInsertCard;
                }

                byte[] id = new byte[30];
                int res = RDNID.getNIDNumberRD(obj, id);
                if (res != DefineConstants.NID_SUCCESS)
                    return res;
                String NIDNum = aByteToString(id);

                byte[] data = new byte[1024];
                res = RDNID.getNIDTextRD(obj, data, data.Length);
                if (res != DefineConstants.NID_SUCCESS)
                    return res;

                String NIDData = aByteToString(data);
                if (NIDData == "")
                {
                    MessageBox.Show("Read Text error");
                }
                else
                {
                    
                }

                RDNID.disconnectCardRD(obj);
                RDNID.deselectReaderRD(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ReadCard " + ex.Message, "");
            }

            return 0;
        }
        private void clearPatient()
        {
            flagInsert = false;
            fname = "";
            lname = "";
            PID = "";
            dob = "";
            addrno = "";
            districtname = "";
            amphurname = "";
            provincename = "";
            sex = "";
            prefixname = "";
            moo = "";
            trok = "";
            fnamee = "";
            lnamee = "";
            soi = "";
            road = "";
            address = "";
            lbPttName.Text = "";
            txtHn.Text = "";
            txtDOB.Text = "";
            txtAddress.Text = "";
            txtDistrict.Text = "";
            txtAmphur.Text = "";
            txtProvince.Text = "";
            txtMobile.Text = "";
            txtRefName.Text = "ผู้ป่วยจำไม่ได้";
            cboRefType.Text = "";
            txtRefMobile.Text = "";
            txtRefAddrNo.Text = "";
            txtRefMoo.Text = "";
            txtRefSoi.Text = "";
            txtRefRoad.Text = "";
            cboAttactNote.Text = "";
            txtPaidType.Text = "02";
            lbPaidName.Text = "...";
            ptt = new Patient();
            queue = new Queue();
            btnPatientNew.Enabled = false;
            picPtt.Image = null;
            txtPreno.Text = "";
            txtVN.Text = "";
            setControl();
            Application.DoEvents();
        }
        protected int ReadCard()
        {
            clearPatient();
            statusStickerIPD = false;
            btnReqLab.Enabled = false;
            btnReqLab.BackColor = Color.Yellow;
            if (bc.iniC.statusSmartCardNoDatabase.Equals("1"))
            {
                bc.bcDB.insertLogPage(bc.userId, this.Name, "ReadCard", "read card start");
            }
            String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);

            IntPtr obj = selectReader(strTerminal);

            Int32 nInsertCard = 0;
            nInsertCard = RDNID.connectCardRD(obj);
            if (nInsertCard != 0)
            {
                String m;
                m = String.Format(" error no {0} ", nInsertCard);
                MessageBox.Show( m );

                RDNID.disconnectCardRD(obj);
                RDNID.deselectReaderRD(obj);
                return nInsertCard;
            }

 //BindDataToScreen();
            byte[] id = new byte[30];
            int res = RDNID.getNIDNumberRD(obj, id);
            if ( res!= DefineConstants.NID_SUCCESS)
                return res;
            String NIDNum = aByteToString(id);
           
            byte[] data = new byte[1024];
            res = RDNID.getNIDTextRD(obj, data, data.Length);
            if ( res != DefineConstants.NID_SUCCESS)
                return res;

            String NIDData = aByteToString(data);
            if (NIDData == "")
            {
                MessageBox.Show("Read Text error");
            }
            else
            {
                string[] fields = NIDData.Split('#');

                m_txtID.Text = NIDNum;                             // or use m_txtID.Text = fields[(int)NID_FIELD.NID_Number];

                String fullname = fields[(int)NID_FIELD.TITLE_T] + " " +
                                    fields[(int)NID_FIELD.NAME_T] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_T] + " " +
                                    fields[(int)NID_FIELD.SURNAME_T];
                m_txtFullNameT.Text = fullname;

                fullname = fields[(int)NID_FIELD.TITLE_E] + " " +
                                    fields[(int)NID_FIELD.NAME_E] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_E] + " " +
                                    fields[(int)NID_FIELD.SURNAME_E];
                m_txtFullNameE.Text = fullname;

                m_txtBrithDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);

                m_txtAddress.Text = fields[(int)NID_FIELD.HOME_NO] + "   " +
                                        fields[(int)NID_FIELD.MOO] + "   " +
                                        fields[(int)NID_FIELD.TROK] + "   " +
                                        fields[(int)NID_FIELD.SOI] + "   " +
                                        fields[(int)NID_FIELD.ROAD] + "   " +
                                        fields[(int)NID_FIELD.TUMBON] + "   " +
                                        fields[(int)NID_FIELD.AMPHOE] + "   " +
                                        fields[(int)NID_FIELD.PROVINCE] + "   "
                                        ;
                if (fields[(int)NID_FIELD.GENDER].Trim() == "1")
                {
                    m_txtGender.Text = "ชาย";
                    sex = "M";
                }
                else
                {
                    m_txtGender.Text = "หญิง";
                    sex = "F";
                }
                m_txtIssueDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.ISSUE_DATE]);
                m_txtExpiryDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.EXPIRY_DATE]);
                if ("99999999" == m_txtExpiryDate.Text)
                    m_txtExpiryDate.Text = "99999999 ตลอดชีพ";
                m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];

                String day = "", month = "", year = "", prefix="", provid="",provincename1="", amprid = "", districtid="", poc="";
                int year1 = 0;
                fname = fields[(int)NID_FIELD.NAME_T].Trim();
                lname = fields[(int)NID_FIELD.SURNAME_T].Trim();
                prefixname = fields[(int)NID_FIELD.TITLE_T].Trim();
                fnamee = fields[(int)NID_FIELD.NAME_E].Trim();
                lnamee = fields[(int)NID_FIELD.SURNAME_E].Trim();
                PID = m_txtID.Text.Trim();
                try
                {
                    string _yyyy = fields[(int)NID_FIELD.BIRTH_DATE].Substring(0, 4);
                    string _mm = fields[(int)NID_FIELD.BIRTH_DATE].Substring(4, 2);
                    string _dd = fields[(int)NID_FIELD.BIRTH_DATE].Substring(6, 2);
                    //dob = m_txtBrithDate.Text.Trim();
                    //if (dob.Length > 2)
                    //{
                    //    day = dob.Substring(0, 2);
                    //}
                    //if (dob.Length > 4)
                    //{
                    //    year = dob.Substring(dob.Length-4);
                    //    int.TryParse(year, out year1);
                    //    year1 -= 543;
                    //}
                    //month = (dob.Replace(day, "").Replace(year, "")).Trim();
                    //month = bc.getMonth1(month);
                    dob = _yyyy + "-" + _mm + "-" + _dd;
                }
                catch(Exception ex)
                {
                    sep.SetError(m_txtBrithDate, "");
                }

                if (bc.iniC.statusSmartCardNoDatabase.Equals("0"))
                {
                    addrno = fields[(int)NID_FIELD.HOME_NO].Trim();
                    moo = fields[(int)NID_FIELD.MOO].Trim();
                    trok = fields[(int)NID_FIELD.TROK].Trim();
                    districtname = fields[(int)NID_FIELD.TUMBON].Trim();
                    amphurname = fields[(int)NID_FIELD.AMPHOE].Trim();
                    provincename = fields[(int)NID_FIELD.PROVINCE].Trim();
                    //districtname = fields[(int)NID_FIELD.TUMBON].Trim();
                    soi = fields[(int)NID_FIELD.SOI].Trim();
                    road = fields[(int)NID_FIELD.ROAD].Trim();
                    address = addrno.Trim() + " " + moo.Trim() + " " + trok.Trim() + " " + soi.Trim() + " " + " " + road.Trim() + " ";

                    prefix = bc.bcDB.pttDB.selectProfixId(prefixname.Trim());
                    provincename1 = provincename.Replace("จังหวัด", "");
                    provid = bc.bcDB.pttDB.selectProvinceId(provincename1);
                    amprid = bc.bcDB.pttDB.selectAmphurId(provid, amphurname.Replace("อำเภอ", ""));
                    districtid = bc.bcDB.pttDB.selectDistrictId(provid, amprid, districtname.Replace("ตำบล", ""));
                    poc = bc.bcDB.pttDB.selectPOCId(provid, amprid, districtid);

                    ptt = bc.bcDB.pttDB.selectPatinetByPID(PID,"pid");
                    if (ptt.Hn.Length > 0)
                    {
                        DateTime dob = new DateTime();
                        DateTime.TryParse(ptt.patient_birthday, out dob);
                        if (dob.Year > 2500)
                        {
                            dob = dob.AddYears(-543);
                        }
                        else if (dob.Year < 2000)
                        {
                            dob = dob.AddYears(543);
                        }
                        txtHn.Text = ptt.Hn;
                        lbPttName.Text = ptt.Name;
                        txtDOB.Text = dob.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
                        txtAddress.Text = ptt.MNC_CUR_ADD + " หมู่ที่ " + ptt.MNC_CUR_MOO + " " + ptt.MNC_CUR_SOI;
                        txtProvince.Text = bc.bcDB.pttDB.selectProvinceName(ptt.MNC_CUR_CHW);
                        txtAmphur.Text = bc.bcDB.pttDB.selectAmphurName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP);
                        txtDistrict.Text = bc.bcDB.pttDB.selectDistrictName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP, ptt.MNC_CUR_TUM);
                        DataTable dtpic = new DataTable();
                        dtpic = bc.bcDB.pttDB.selectPatientPic(ptt.Hn);
                        if (dtpic.Rows.Count > 0)
                        {
                            try
                            {
                                byte[] photo_aray;
                                photo_aray = (byte[])dtpic.Rows[0]["MNC_PAT_IMG"];
                                MemoryStream ms = new MemoryStream(photo_aray);
                                ms.Position = 0;
                                picPtt.Image = Image.FromStream(ms);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        btnPatientNew.Enabled = true;
                        btnPatientNew.BackColor = Color.Green;
                        btnReqLab.Enabled = false;
                        btnReqLab.BackColor = Color.Yellow;
                        btnVisit.Enabled = true;
                        btnVisit.BackColor = Color.Green;
                        //แก้ไขชื่อ ให้เอา จาก smart card มาเลย
                        ptt.MNC_FNAME_T = fname;
                        ptt.MNC_LNAME_T = lname;
                        ptt.MNC_FNAME_E = fnamee;
                        ptt.MNC_LNAME_E = lnamee;
                    }
                    else
                    {
                        DateTime dob1 = new DateTime();
                        DateTime.TryParse(dob, out dob1);
                        //MessageBox.Show("dob " + dob, "");
                        if (dob1.Year > 2500)
                        {
                            dob1 = dob1.AddYears(-543);
                            //MessageBox.Show("if (dob1.Year > 2500) ", "");
                        }
                        //else if (dob1.Year < 2000)
                        //{
                        //    dob1 = dob1.AddYears(543);
                        //    MessageBox.Show("else if (dob1.Year < 2000) ", "");
                        //}
                        btnPatientNew.Enabled = true;
                        btnPatientNew.BackColor = Color.Green;
                        btnVisit.Enabled = true;
                        btnVisit.BackColor = Color.Green;
                        btnReqLab.Enabled = false;
                        btnReqLab.BackColor = Color.Yellow;
                        //DateTime dob1 = new DateTime();
                        //DateTime.TryParse(dob, out dob1);
                        String datestart = "";
                        datestart = dob1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        //MessageBox.Show("datestart "+datestart, "");
                        ptt.MNC_HN_NO = "";
                        ptt.MNC_HN_YR = "";
                        ptt.MNC_PFIX_CDT = prefix;
                        ptt.MNC_PFIX_CDE = "";
                        ptt.MNC_FNAME_T = fname;
                        ptt.MNC_LNAME_T = lname;
                        ptt.MNC_FNAME_E = fnamee;
                        ptt.MNC_LNAME_E = lnamee;
                        ptt.MNC_AGE = "";
                        ptt.MNC_BDAY = datestart;
                        ptt.MNC_ID_NO = PID;
                        ptt.MNC_SS_NO = PID;
                        ptt.MNC_SEX = sex;
                        ptt.MNC_FULL_ADD = "";
                        ptt.MNC_STAMP_DAT = "";
                        ptt.MNC_STAMP_TIM = "";

                        ptt.MNC_NAT_CD = "";
                        ptt.MNC_CUR_ADD = addrno;
                        ptt.MNC_CUR_TUM = districtid;
                        ptt.MNC_CUR_AMP = amprid;
                        ptt.MNC_CUR_CHW = provid;
                        ptt.MNC_CUR_POC = poc;
                        ptt.MNC_CUR_TEL = txtMobile.Text.Trim();
                        ptt.MNC_DOM_ADD = addrno;
                        ptt.MNC_DOM_TUM = districtid;
                        ptt.MNC_DOM_AMP = amprid;
                        ptt.MNC_DOM_CHW = provid;
                        ptt.MNC_DOM_POC = poc;
                        ptt.MNC_DOM_TEL = txtMobile.Text.Trim();
                        ptt.MNC_REF_NAME = txtRefName.Text.Trim();
                        ptt.MNC_REF_REL = "";
                        ptt.MNC_REF_ADD = txtRefAddrNo.Text.Trim();
                        ptt.MNC_REF_TUM = "";
                        ptt.MNC_REF_AMP = "";
                        ptt.MNC_REF_CHW = "";
                        ptt.MNC_REF_POC = "";
                        ptt.MNC_REF_TEL = txtMobile.Text.Trim();
                        ptt.MNC_CUR_MOO = moo.Trim();
                        ptt.MNC_DOM_MOO = moo.Trim();
                        ptt.MNC_REF_MOO = txtRefMoo.Text.Trim();
                        ptt.MNC_REF_SOI = txtRefSoi.Text.Trim();
                        //ptt.MNC_REF_roa = "";
                        ptt.MNC_REG_DAT = "";
                        ptt.MNC_REG_TIM = "";
                        ptt.MNC_CUR_SOI = trok.Trim() + " " + soi.Trim();
                        ptt.MNC_DOM_SOI = trok.Trim() + " " + soi.Trim();
                        ptt.MNC_REF_SOI = "";
                        ptt.MNC_ATT_NOTE = cboAttactNote.Text.Trim();
                        ptt.MNC_FN_TYP_CD = "";
                    }
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "ReadCard", "read card end dob"+ dob);
                }
            }

            byte[] NIDPicture = new byte[1024 * 5];
            int imgsize = NIDPicture.Length;
            res=RDNID.getNIDPhotoRD(obj, NIDPicture, out imgsize);
            if (res!= DefineConstants.NID_SUCCESS)
                return res;

            byte[] byteImage = NIDPicture;
            if (byteImage == null)
            {
                MessageBox.Show("Read Photo error");
            }
            else
            {
                //m_picPhoto
                Image img = Image.FromStream(new MemoryStream(byteImage));               
                picPtt.Image = img;
                Bitmap MyImage = new Bitmap(img, m_picPhoto.Width-2, m_picPhoto.Height-2);
                m_picPhoto.Image = (Image)MyImage;
            }

            RDNID.disconnectCardRD(obj);
            RDNID.deselectReaderRD(obj);

            return 0;
     }

        private void OnReadCardClick(object sender, EventArgs e)
        {
            m_txtID.Text = "";
            m_txtFullNameT.Text = "";
            m_txtFullNameE.Text = "";
            m_txtAddress.Text = "";
            m_txtBrithDate.Text = "";
            m_txtGender.Text = "";
            m_txtIssueDate.Text = "";            
            m_txtExpiryDate.Text = "";
            m_txtIssueNum.Text = "";
            m_picPhoto.Image = null;
            
            ReadCard();
        }
        private void btnloadLIC_Click(object sender, EventArgs e)
        {
            byte[] _lic = String2Byte(StartupPath + "\\RDNIDLib.DLD");
            int res = RDNID.updateLicenseFileRD(_lic);           
            if (res != 0)
            {
                string s = string.Format("Error : {0}", res);
                MessageBox.Show(s);
            }
        }

        private void btngetReaderID_Click(object sender, EventArgs e)
        {
            m_txtReaderID.ResetText();
            this.Refresh();
            String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);

            IntPtr obj = selectReader(strTerminal);

            byte[] Rid = new byte[16];
            int res = RDNID.getRidDD(obj, Rid);
            if (res > 0)
            {
                m_txtReaderID.Text = BitConverter.ToString(Rid);
            }
            else
            {
                m_txtReaderID.Text = String.Format("{0}", res);
                MessageBox.Show(String.Format("{0}", res));
            }
            //m_txtGetRidRY.Text = mRDNIDWRAPPER.getRidDD();
            RDNID.deselectReaderRD(obj); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_txtID.Text = "";
            m_txtFullNameT.Text = "";
            m_txtFullNameE.Text = "";
            m_txtAddress.Text = "";
            m_txtBrithDate.Text = "";
            m_txtGender.Text = "";
            m_txtIssueDate.Text = "";
            m_txtExpiryDate.Text = "";
            m_txtIssueNum.Text = "";
            m_txtReaderID.Text = "";
            m_picPhoto.Image = null;
            button2.Image = null;
        }

        private void m_ListReaderCard_Click(object sender, EventArgs e)
        {
            m_ListReaderCard.ResetText();
            m_ListReaderCard.Items.Clear();
            ListCardReader();
        }
        public static string GetCurrentExecutingDirectory(System.Reflection.Assembly assembly)
        {
            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
        private void genImgStaffNote()
        {
            String err = "";
            float mmpi = 25.4f;
            int dpi = 150, line1Len = 965, x2Right = 1740;

            err = "00";
            Image imgLogo = Resources.LOGO_Green_Transparent;
            int newHeight = 50, yPos = 0, gapLine = 45, col2=470, col3 = 980, col31=460, recx = 15, recy = 15, col2int = 0, col4=0, col40=0, col40int=0, yPosint=0, col5=980, col6=1340;
            String line = "", date = "";
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps

            Image resizedImageLogo = imgLogo.GetThumbnailImage((newHeight * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
            //Bitmap imgA4 = new Bitmap((int)(210 / mmpi * dpi), (int)(297 / mmpi * dpi));        //Port
            Bitmap imgA4 = new Bitmap((int)(297 / mmpi * dpi), (int)(210 / mmpi * dpi));        //lang

            Pen penGreen3 = new Pen(Color.FromArgb(26, 173, 79), 3);
            Pen penBlue3 = new Pen(Color.FromArgb(79, 111, 108), 3);
            Pen blackPen = new Pen(Color.Black, 1);
            Pen penBorder = penBlue3;
            SolidBrush BrushBlack = new SolidBrush(Color.Black);
            SolidBrush brushBule = new SolidBrush(Color.Blue);
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
                //resizedImageLogo = imgLogo.GetThumbnailImage(((newHeight+3000) * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
                //resizedImageLogo = imgLogo.GetThumbnailImage((5 * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
                int newWidth = bc.imgScanWidth;
                newWidth = 8000;
                resizedImageLogo = imgLogo.GetThumbnailImage(newWidth, (newWidth * imgLogo.Height) / imgLogo.Width, null, IntPtr.Zero);
            }
            col2 = 105;
            col3 = 300;
            col4 = 1000;
            col40 = 650;
            yPos = 15;
            col6 = 1340;
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            imgA4.SetResolution(dpi, dpi);
            err = "01";
            using (Graphics gfx = Graphics.FromImage(imgA4))
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                gfx.FillRectangle(brush, 0, 0, imgA4.Width, imgA4.Height);
                //gfx.DrawImage(resizedImageLogo, 120, 40);
                //gfx.DrawImage(resizedImageLogo, 1000, 40);
                line = "โรงพยาบาล บางนา5";
                gfx.DrawString(line, famtB, brushBule, 195, 35, flags);
                gfx.DrawString(line, famtB, brushBule, 1070, 35, flags);
                //gfx.DrawRectangle(penBorder, 3, 3, imgA4.Width - 9, imgA4.Height - 9);            // Border
                line = "H.N. " + ptt.MNC_HN_NO + "     " + txtVN.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 35, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 35, flags);
                line = "ชื่อ " + ptt.Name;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 60, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 60, flags);
                line = "เลขที่บัตร " + ptt.MNC_ID_NO;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 85, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, 1340, 85, flags);
                line = lbPaidName.Text;
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 115, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 115, flags);

                line = txtCompName.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 140, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 140, flags);
                line = "โรคประจำตัว        ไม่มี";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 140, flags);
                rec = new Rectangle(col2int + 125, 145, recx, recy);
                gfx.DrawRectangle(blackPen, rec);
                line = "อายุ " + ptt.AgeStringShort1() + " [" + bc.datetoShow(ptt.MNC_BDAY) + "]";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 140, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 140, flags);

                line = "มีโรค ระบุ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 35, 170, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 35 - recx -10, 180, recx, recy));
                line = "วันที่เวลา " + date;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 170, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 170, flags);

                line = "โรคเรื้อรัง";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 200, flags);
                line = "ชื่อแพทย์ " + txtDtrId.Text.Trim() + " " + lbDtrName.Text;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 200, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 200, flags);

                line = "DR Time.                               ปิดใบยา";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 230, flags);
                line = "อาการเบื้องต้น " + txtSymptom.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 230, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 230, flags);

                line = "Temp";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, 260, flags);

                line = "H.Rate";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 80, 260, flags);
                line = "R.Rate";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 160, 260, flags);
                line = "BP1";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 240, 260, flags);
                line = "Time :";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 300, 260, flags);
                line = "BP2 ";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 380, 260, flags);
                line = "Time :";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 440, 260, flags);

                line = "Wt.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, 290, flags);
                line = "Ht.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 80, 290, flags);
                line = "BMI.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 100, 290, flags);
                line = "CC.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 180, 290, flags);
                line = "CC.IN";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 240, 290, flags);
                line = "CC.EX";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 300, 290, flags);
                line = "Ab.C";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 400, 290, flags);
                line = "H.C.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 460, 290, flags);

                line = "Precaution (Med) _________________________________________ ";
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 290, flags);
                err = "02";
                line = "แพ้ยา/อาหาร/อื่นๆ         ไม่มี";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 320, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 320, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 180 , 330, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col4 - recx + 200, 330, recx, recy));
                line = "มี ระบุอาการ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + recx + 10, 350, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, 360, recx, recy));
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + recx+10, 350, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col4, 360, recx, recy));
                line = "O2 Sat __________        Pain Score __________";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 350, flags);

                gfx.DrawRectangle(penBorder, 100, 20, 865, 1200);// Border
                gfx.DrawRectangle(penBorder, col5, 20, 760, 1200);// Border

                yPos = 390;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //1
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawLine(penBorder, 1550, yPos, 1550, 1020);        //

                //gfx.DrawString("",)
                line = "Staff Note";
                gfx.DrawString(line, famtB, brushBule, 380, yPos, flags);
                line = "Rx";
                gfx.DrawString(line, famtB, brushBule, 980, yPos , flags);
                line = "ใบสั่งยา";
                gfx.DrawString(line, famtB, brushBule, 1280, yPos , flags);
                line = "จำนวนหน่วย";
                gfx.DrawString(line, famtB, brushBule, 1550, yPos, flags);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                
                gfx.DrawLine(penBorder, col3, yPos, x2Right, yPos);

                err = "03";
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("CHIEF COMPLIANT", fEditB, brushBule, 110, yPos, flags);
                line = txtRemark.Text + " " + lbDeptName.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + 100, yPos -30, flags);
                line = "Medication                       No Medication";
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + 100, yPos, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col4 + 80 - recx - 5, yPos+10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col4 + 300 - recx , yPos+10, recx, recy));
                line = txtSymptom.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, 110+100, yPos +50, flags);
                if (chkPCR1500.Checked)
                {
                    line = "RT-PCR COVID-19  1500.00";
                }
                else
                {
                    line = "RT-PCR COVID-19  2700.00";
                }
                
                gfx.DrawString(line, fEdit, Brushes.Black, col4+100, yPos + 50, flags);

                line = "อาการ";
                gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 175, flags);

                line = "สัมผัสผู้ป่วย ชื่อ";
                gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 230, flags);

                line = "สัมผัสล่าสุด";
                gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 275, flags);
                line = "คำแนะนำ       การออกกำลังกาย               การรับประทานอาหารที่ถูกสัดส่วน";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, yPos + 320, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 105, yPos + 325, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 310, yPos + 325, recx, recy));
                
                line = "การตรวจสุขภาพประจำปี          การพบแพทย์เฉพาะทาง       อื่นๆ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 365, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 35 - recx, yPos + 375, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 280 - recx, yPos + 375, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 510 - recx, yPos + 375, recx, recy));

                err = "04";
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("PYSICAL EXAM", fEditB, brushBule, 110, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //10
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("DIAGNOSIS", fEditB, brushBule, 110, yPos, flags);
                gfx.DrawString("คำแนะนำ", fEditB, brushBule, 680, yPos, flags);
                
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //15
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("รหัสโรค", fEditB, brushBule, 110, yPos, flags);
                gfx.DrawString("FOLLOW UP", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("ATTENDIND PHYSICAN", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);

                //yPos += gapLine;
                err = "05";
                line = "ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos , flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 240 - recx, yPos+10  , recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 305 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 410 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 515 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 590 - recx, yPos + 10, recx, recy));

                line = "Health Education :";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, imgA4.Height - 90, flags);
                line = "ชื่อผู้รับ _____________________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2+500, imgA4.Height - 90, flags);

                line = "ลงชื่อพยาบาล: _____________________________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, imgA4.Height - 60, flags);
                line = "FM-REC-002 (00 10/09/53)(1/1)";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, imgA4.Height - 30, flags);
                gfx.DrawString(line, fEditS, Brushes.Black, col6, imgA4.Height-30, flags);
                //gfx.DrawString("",)
            }
            //imgA4.Save("aaaa.jpg");
            try
            {
                err = "06";
                Rectangle rectL = new Rectangle(0, 0, 980, imgA4.Height);
                //Bitmap imgL = new Bitmap(980, imgA4.Height);
                Bitmap imgL = new Bitmap(630, imgA4.Width - 960);
                Graphics gfxL = Graphics.FromImage(imgL);
                gfxL.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfxL.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfxL.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfxL.DrawImage(imgA4, 0, 0, rectL, GraphicsUnit.Pixel);

                String filenameS = "";
                filenameS = "000000" + txtPreno.Text.Trim();
                filenameS = filenameS.Substring(filenameS.Length - 6);

                Rectangle rectR = new Rectangle(975, 0, imgA4.Width - 960, imgA4.Height);
                //Bitmap imgR = new Bitmap(imgA4.Width - 975, imgA4.Height);
                Bitmap imgR = new Bitmap(502, imgA4.Width - 960);
                Graphics gfxR = Graphics.FromImage(imgR);
                gfxR.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfxR.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfxR.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfxR.DrawImage(imgA4, 0, 0, rectR, GraphicsUnit.Pixel);
                String filenameR = "", path = "\\\\172.25.10.5\\image\\OPD\\", year = DateTime.Now.ToString("yyyy"), mon = DateTime.Now.ToString("MM"), day = DateTime.Now.ToString("dd");
                year = txtVsdate.Text.Substring(0, 4);
                path += year + "\\" + mon + "\\" + day + "\\";
                
                filenameR = "000000" + txtPreno.Text.Trim();
                filenameR = filenameR.Substring(filenameR.Length - 6);
                err = "07";
                new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameS);
                imgL.Save(path + filenameS + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgL.Save(filenameS + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                err = "08";
                new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameR);
                imgR.Save(path + filenameR + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgR.Save(filenameR + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "genImgStaffNote err"+err+" error " + ex.Message);
                MessageBox.Show(""+ ex.Message, "");
            }
        }
        private void genImgStaffNoteHI()
        {
            String err = "";
            float mmpi = 25.4f;
            int dpi = 150, line1Len = 965, x2Right = 1740;

            err = "00";
            Image imgLogo = Resources.LOGO_Green_Transparent;
            int newHeight = 50, yPos = 0, gapLine = 45, col2 = 470, col3 = 980, col31 = 460, recx = 15, recy = 15, col2int = 0, col4 = 0, col40 = 0, col40int = 0, yPosint = 0, col5 = 980, col6 = 1340;
            String line = "", date = "";
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps

            Image resizedImageLogo = imgLogo.GetThumbnailImage((newHeight * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
            //Bitmap imgA4 = new Bitmap((int)(210 / mmpi * dpi), (int)(297 / mmpi * dpi));        //Port
            Bitmap imgA4 = new Bitmap((int)(297 / mmpi * dpi), (int)(210 / mmpi * dpi));        //lang

            Pen penGreen3 = new Pen(Color.FromArgb(26, 173, 79), 3);
            Pen penBlue3 = new Pen(Color.FromArgb(79, 111, 108), 3);
            Pen blackPen = new Pen(Color.Black, 1);
            Pen penBorder = penBlue3;
            SolidBrush BrushBlack = new SolidBrush(Color.Black);
            SolidBrush brushBule = new SolidBrush(Color.Blue);
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
                //resizedImageLogo = imgLogo.GetThumbnailImage(((newHeight+3000) * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
                //resizedImageLogo = imgLogo.GetThumbnailImage((5 * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
                int newWidth = bc.imgScanWidth;
                newWidth = 8000;
                resizedImageLogo = imgLogo.GetThumbnailImage(newWidth, (newWidth * imgLogo.Height) / imgLogo.Width, null, IntPtr.Zero);
            }
            col2 = 105;
            col3 = 300;
            col4 = 1000;
            col40 = 650;
            yPos = 15;
            col6 = 1340;
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            imgA4.SetResolution(dpi, dpi);
            err = "01";
            
            using (Graphics gfx = Graphics.FromImage(imgA4))
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                String line1 = "";
                gfx.FillRectangle(brush, 0, 0, imgA4.Width, imgA4.Height);
                //gfx.DrawImage(resizedImageLogo, 120, 40);
                //gfx.DrawImage(resizedImageLogo, 1000, 40);
                line = "โรงพยาบาล บางนา5";
                gfx.DrawString(line, famtB, brushBule, 195, 35, flags);
                gfx.DrawString(line, famtB, brushBule, 1070, 35, flags);
                //gfx.DrawRectangle(penBorder, 3, 3, imgA4.Width - 9, imgA4.Height - 9);            // Border
                line = "H.N. " + ptt.MNC_HN_NO + "     " + txtVN.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 35, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 35, flags);
                line = "ชื่อ " + ptt.Name;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 60, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 60, flags);
                line = "เลขที่บัตร " + ptt.MNC_ID_NO;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 85, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, 1340, 85, flags);
                line = lbPaidName.Text;
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 115, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 115, flags);

                line = txtCompName.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 140, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 140, flags);
                line = "โรคประจำตัว        ไม่มี";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 140, flags);
                rec = new Rectangle(col2int + 125, 145, recx, recy);
                gfx.DrawRectangle(blackPen, rec);
                line = "อายุ " + ptt.AgeStringShort1() + " [" + bc.datetoShow(ptt.MNC_BDAY) + "]";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 140, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 140, flags);

                line = "มีโรค ระบุ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 35, 170, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 35 - recx - 10, 180, recx, recy));
                line = "วันที่เวลา " + date;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 170, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 170, flags);

                line = "โรคเรื้อรัง";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 200, flags);
                line = "ชื่อแพทย์ " + txtDtrId.Text.Trim() + " " + lbDtrName.Text;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 200, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 200, flags);

                line = "DR Time.                               ปิดใบยา";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 230, flags);
                line = "อาการเบื้องต้น " + txtSymptom.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 230, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 230, flags);

                line = "Temp";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, 260, flags);

                line = "H.Rate";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 80, 260, flags);
                line = "R.Rate";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 160, 260, flags);
                line = "BP1";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 240, 260, flags);
                line = "Time :";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 300, 260, flags);
                line = "BP2 ";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 380, 260, flags);
                line = "Time :";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 440, 260, flags);

                line = "Wt.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, 290, flags);
                line = "Ht.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 80, 290, flags);
                line = "BMI.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 100, 290, flags);
                line = "CC.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 180, 290, flags);
                line = "CC.IN";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 240, 290, flags);
                line = "CC.EX";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 300, 290, flags);
                line = "Ab.C";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 400, 290, flags);
                line = "H.C.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 460, 290, flags);

                line = "Precaution (Med) _________________________________________ ";
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 290, flags);
                err = "02";
                line = "แพ้ยา/อาหาร/อื่นๆ         ไม่มี";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 320, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 320, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 180, 330, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col4 - recx + 200, 330, recx, recy));
                line = "มี ระบุอาการ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + recx + 10, 350, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, 360, recx, recy));
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + recx + 10, 350, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col4, 360, recx, recy));
                line = "O2 Sat __________        Pain Score __________";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 350, flags);

                gfx.DrawRectangle(penBorder, 100, 20, 865, 1200);// Border
                gfx.DrawRectangle(penBorder, col5, 20, 760, 1200);// Border

                yPos = 390;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //1
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawLine(penBorder, 1550, yPos, 1550, 1020);        //

                //gfx.DrawString("",)
                line = "Staff Note";
                gfx.DrawString(line, famtB, brushBule, 380, yPos, flags);
                line = "Rx";
                gfx.DrawString(line, famtB, brushBule, 980, yPos, flags);
                line = "ใบสั่งยา";
                gfx.DrawString(line, famtB, brushBule, 1280, yPos, flags);
                line = "จำนวนหน่วย";
                gfx.DrawString(line, famtB, brushBule, 1550, yPos, flags);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);

                gfx.DrawLine(penBorder, col3, yPos, x2Right, yPos);

                err = "03";
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("CHIEF COMPLIANT", fEditB, brushBule, 110, yPos, flags);
                line = txtRemark.Text + " " + lbDeptName.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + 100, yPos - 30, flags);
                line = "Medication                       No Medication";
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + 100, yPos, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col4 + 80 - recx - 5, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col4 + 300 - recx, yPos + 10, recx, recy));
                line = txtSymptom.Text.Trim();
                gfx.DrawString(line, fEdit, Brushes.Black, 110 + 100, yPos + 50, flags);
                gfx.DrawString(line, fEditB, Brushes.Black, col40 + 140, yPos + 50, flags);
                //btnSavePic
                line = "อาการ";
                gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 175, flags);
                line = "เอกซเรย์ปอด";
                gfx.DrawString(line, fEditB, Brushes.Black, col40 + 140, yPos + 175, flags);

                line = "สัมผัสผู้ป่วย ชื่อ";
                gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 230, flags);
                line = "Set ";
                if (chkHI.Checked)
                {
                    line = "Set " + drugset.ToUpper();
                }
                gfx.DrawString(line, fEditB, Brushes.Black, col40 + 140, yPos + 230, flags);

                line = "สัมผัสล่าสุด";
                gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 275, flags);
                line = "เครื่องวัดออกซิเจน ปลายนิ้ว    1";
                gfx.DrawString(line, fEditB, Brushes.Black, col40 + 140, yPos + 275, flags);
                line = "ปรอทวัดอุณหภูมิ     1";
                gfx.DrawString(line, fEditB, Brushes.Black, col40 + 140, yPos + 305, flags);

                line = "คำแนะนำ       การออกกำลังกาย               การรับประทานอาหารที่ถูกสัดส่วน";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, yPos + 320, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 105, yPos + 325, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 310, yPos + 325, recx, recy));

                line = "การตรวจสุขภาพประจำปี          การพบแพทย์เฉพาะทาง       อื่นๆ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 365, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 35 - recx, yPos + 375, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 280 - recx, yPos + 375, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 510 - recx, yPos + 375, recx, recy));

                err = "04";
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("PYSICAL EXAM", fEditB, brushBule, 110, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //10
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("DIAGNOSIS", fEditB, brushBule, 110, yPos, flags);
                gfx.DrawString("คำแนะนำ", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //15
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("รหัสโรค", fEditB, brushBule, 110, yPos, flags);
                gfx.DrawString("FOLLOW UP", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("ATTENDIND PHYSICAN", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);

                //yPos += gapLine;
                err = "05";
                line = "ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 240 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 305 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 410 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 515 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 590 - recx, yPos + 10, recx, recy));

                line = "Health Education :";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, imgA4.Height - 90, flags);
                line = "ชื่อผู้รับ _____________________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 500, imgA4.Height - 90, flags);

                line = "ลงชื่อพยาบาล: _____________________________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, imgA4.Height - 60, flags);
                line = "FM-REC-002 (00 10/09/53)(1/1)";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, imgA4.Height - 30, flags);
                gfx.DrawString(line, fEditS, Brushes.Black, col6, imgA4.Height - 30, flags);
                //gfx.DrawString("",)
            }
            //imgA4.Save("aaaa.jpg");
            try
            {
                err = "06";
                Rectangle rectL = new Rectangle(0, 0, 980, imgA4.Height);
                //Bitmap imgL = new Bitmap(980, imgA4.Height);
                Bitmap imgL = new Bitmap(630, imgA4.Width - 960);
                Graphics gfxL = Graphics.FromImage(imgL);
                gfxL.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfxL.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfxL.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfxL.DrawImage(imgA4, 0, 0, rectL, GraphicsUnit.Pixel);

                String filenameS = "";
                filenameS = "000000" + txtPreno.Text.Trim();
                filenameS = filenameS.Substring(filenameS.Length - 6);

                Rectangle rectR = new Rectangle(975, 0, imgA4.Width - 960, imgA4.Height);
                //Bitmap imgR = new Bitmap(imgA4.Width - 975, imgA4.Height);
                Bitmap imgR = new Bitmap(502, imgA4.Width - 960);
                Graphics gfxR = Graphics.FromImage(imgR);
                gfxR.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfxR.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfxR.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfxR.DrawImage(imgA4, 0, 0, rectR, GraphicsUnit.Pixel);
                String filenameR = "", path = "\\\\172.25.10.5\\image\\OPD\\", year = DateTime.Now.ToString("yyyy"), mon = DateTime.Now.ToString("MM"), day = DateTime.Now.ToString("dd");
                year = txtVsdate.Text.Substring(0, 4);
                path += year + "\\" + mon + "\\" + day + "\\";

                filenameR = "000000" + txtPreno.Text.Trim();
                filenameR = filenameR.Substring(filenameR.Length - 6);
                err = "07";
                new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameS);
                imgL.Save(path + filenameS + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgL.Save(filenameS + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                err = "08";
                new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameR);
                imgR.Save(path + filenameR + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgR.Save(filenameR + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "genImgStaffNote err" + err + " error " + ex.Message);
                MessageBox.Show("" + ex.Message, "");
            }
        }
        private void FrmSmartCard_Load(object sender, EventArgs e)
        {
            if (bc.iniC.statusSmartCardNoDatabase.Equals("1"))
            {
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmSmartCard_Load", "FrmSmartCard_Load");
            }
            this.Text += " Update 2022-03-14";
            btnPatientNew.Enabled = false;
            if (bc.iniC.FrmSmartCardTabDefault.Equals("1"))
            {
                tC1.SelectedTab = tabPttNew;
            }
            else if (bc.iniC.FrmSmartCardTabDefault.Equals("2"))
            {
                tC1.SelectedTab = tabPttNew;
            }
            if (bc.iniC.FrmSmartCardTabDefault.Equals("3"))
            {
                tC1.SelectedTab = tabSticker;
            }
            else
            {
                tC1.SelectedTab = tabPttNew;
            }
            tCBn1.SelectedTab = tabBn1Ptt;
            txtStkHn.Focus();
        }
    }
}
