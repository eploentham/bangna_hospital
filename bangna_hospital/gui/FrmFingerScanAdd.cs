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
using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using SecuGen.FDxSDKPro.Windows;

namespace bangna_hospital.gui
{
    public partial class FrmFingerScanAdd : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, famtB14, fque, fqueB, famtB30;

        private bool m_useAnsiTemplate = false; // true;  
        private SGFingerPrintManager m_FPM;
        private Int32 m_ImageWidth;
        private Int32 m_ImageHeight;
        private Int32 m_Dpi;
        private SGFPMSecurityLevel m_SecurityLevel;

        private Byte[] m_RegMin1;
        private Byte[] m_RegMin2;
        private Byte[] m_VrfMin;
        private Byte[] m_StoredTemplate;
        private bool m_DeviceOpened;
        private System.Windows.Forms.RadioButton[] m_RadioButton;
        String hn = "", fullname = "";
        public FrmFingerScanAdd(BangnaControl bc,String hn, string fullname)
        {
            InitializeComponent();
            this.bc = bc;
            this.hn = hn;
            this.fullname = fullname;
            initConfig();
        }
        private void initConfig()
        {
            OpenDeviceBtn.Click += OpenDeviceBtn_Click;
            BtnCapture1.Click += BtnCapture1_Click;
            BtnCapture2.Click += BtnCapture2_Click;
            BtnRegister.Click += BtnRegister_Click;
            txtHn.Text = hn;
            lbFullName.Text = fullname;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            bool matched = false;
            Int32 err = 0;
            String error = "";
            err = m_FPM.MatchTemplate(m_RegMin1, m_RegMin2, m_SecurityLevel, ref matched);

            if ((err == (Int32)SGFPMError.ERROR_NONE))
            {
                error = "00";
                if (matched)
                {
                    // Save template after merging two template - m_FetBuf1, m_FetBuf2
                    Byte[] merged_template;
                    Int32 buf_size = 0;
                    error = "01";
                    if (m_useAnsiTemplate)
                    {
                        m_FPM.GetTemplateSizeAfterMerge(m_RegMin1, m_RegMin2, ref buf_size);
                        merged_template = new Byte[buf_size];
                        m_FPM.MergeAnsiTemplate(m_RegMin1, m_RegMin2, merged_template);
                    }
                    else
                    {
                        m_FPM.GetIsoTemplateSizeAfterMerge(m_RegMin1, m_RegMin2, ref buf_size);
                        merged_template = new Byte[buf_size];
                        m_FPM.MergeIsoTemplate(m_RegMin1, m_RegMin2, merged_template);
                    }
                    error = "02";
                    if (m_StoredTemplate == null)
                    {
                        m_StoredTemplate = new Byte[buf_size];
                        merged_template.CopyTo(m_StoredTemplate, 0);
                    }
                    else
                    {
                        Int32 new_size = 0;

                        if (m_useAnsiTemplate)
                        {
                            err = m_FPM.GetTemplateSizeAfterMerge(m_StoredTemplate, merged_template, ref new_size);
                        }
                        else
                        {
                            err = m_FPM.GetIsoTemplateSizeAfterMerge(m_StoredTemplate, merged_template, ref new_size);
                        }

                        Byte[] new_enroll_template = new Byte[new_size];

                        if (m_useAnsiTemplate)
                        {
                            err = m_FPM.MergeAnsiTemplate(merged_template, m_StoredTemplate, new_enroll_template);
                        }
                        else
                        {
                            err = m_FPM.MergeIsoTemplate(merged_template, m_StoredTemplate, new_enroll_template);
                        }

                        m_StoredTemplate = new Byte[new_size];

                        new_enroll_template.CopyTo(m_StoredTemplate, 0);
                    }
                    error = "03";
                    // Let's display ANSI/ISO template Info
                    for (int i = 0; i < m_RadioButton.Length; i++)
                        m_RadioButton[i].Checked = false;
                    error = "04";
                    if (m_useAnsiTemplate)
                    {
                        SGFPMANSITemplateInfo sample_info = new SGFPMANSITemplateInfo();
                        err = m_FPM.GetAnsiTemplateInfo(m_StoredTemplate, sample_info);
                        for (int i = 0; i < sample_info.TotalSamples; i++)
                            m_RadioButton[(Int32)sample_info.SampleInfo[i].FingerNumber].Checked = true;
                    }
                    else
                    {
                        SGFPMISOTemplateInfo sample_info = new SGFPMISOTemplateInfo();
                        err = m_FPM.GetIsoTemplateInfo(m_StoredTemplate, sample_info);
                        for (int i = 0; i < sample_info.TotalSamples; i++)
                            m_RadioButton[(Int32)sample_info.SampleInfo[i].FingerNumber].Checked = true;
                    }
                    String re1 = "1";
                    //stt.Show("<p><b>success</b></p> <br> ส่งข้อมูล เข้าระบบโรงพยาบาลเรียบร้อย", btnPatientNew, 10);
                    TFingerScanDB tfsdb = new TFingerScanDB(bc.conn);
                    long chk = 0;
                    re1 = tfsdb.insertFingerScan(txtHn.Text.Trim(), comboBoxSelFinger.SelectedIndex.ToString(), m_RegMin1);
                    if (long.TryParse(re1, out chk))
                    {
                        
                    }
                    StatusBar.Text = "Template registration success";
                }
                else
                    StatusBar.Text = "Template registration failed " + error;
            }
            else
                StatusBar.Text = "MatchTemplate() Error: " + err;
        }

        private void BtnCapture2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Byte[] fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            Int32 error = (Int32)SGFPMError.ERROR_NONE;
            Int32 img_qlty = 0;

            if (m_DeviceOpened)
                error = m_FPM.GetImage(fp_image);
            else
                error = GetImageFromFile(fp_image);

            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
            progressBar_R2.Value = img_qlty;

            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                DrawImage(fp_image, pictureBoxR2);

                SGFPMFingerInfo finger_info = new SGFPMFingerInfo();
                finger_info.FingerNumber = (SGFPMFingerPosition)comboBoxSelFinger.SelectedIndex;
                finger_info.ImageQuality = (Int16)img_qlty;
                finger_info.ImpressionType = (Int16)SGFPMImpressionType.IMPTYPE_LP;
                finger_info.ViewNumber = 1;

                error = m_FPM.CreateTemplate(finger_info, fp_image, m_RegMin2);

                if (error == (Int32)SGFPMError.ERROR_NONE)
                    StatusBar.Text = "Second image is captured";
                else
                    StatusBar.Text = "GetMinutiae() Error : " + error;
            }
            else
                StatusBar.Text = "GetImage() Error : " + error;
        }

        private void BtnCapture1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Byte[] fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            Int32 error = (Int32)SGFPMError.ERROR_NONE;
            Int32 img_qlty = 0;

            if (m_DeviceOpened)
                error = m_FPM.GetImage(fp_image);
            else
                error = GetImageFromFile(fp_image);

            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
                progressBar_R1.Value = img_qlty;

                DrawImage(fp_image, pictureBoxR1);

                SGFPMFingerInfo finger_info = new SGFPMFingerInfo();
                finger_info.FingerNumber = (SGFPMFingerPosition)comboBoxSelFinger.SelectedIndex;
                finger_info.ImageQuality = (Int16)img_qlty;
                finger_info.ImpressionType = (Int16)SGFPMImpressionType.IMPTYPE_LP;
                finger_info.ViewNumber = 1;

                // CreateTemplate
                error = m_FPM.CreateTemplate(finger_info, fp_image, m_RegMin1);

                if (error == (Int32)SGFPMError.ERROR_NONE)
                    StatusBar.Text = "First image is captured";
                else
                    StatusBar.Text = "GetMinutiae() Error : " + error;
            }
            else
                StatusBar.Text = "GetImage() Error : " + error;
        }
        private void initDevice()
        {
            Int32 error;
            SGFPMDeviceName device_name = SGFPMDeviceName.DEV_UNKNOWN;
            Int32 device_id = (Int32)SGFPMPortAddr.USB_AUTO_DETECT;

            m_DeviceOpened = false;

            // Get device name
            if (comboBoxDeviceName.Text == "USB FDU02")
                device_name = SGFPMDeviceName.DEV_FDU02;
            else if (comboBoxDeviceName.Text == "USB FDU03")
                device_name = SGFPMDeviceName.DEV_FDU03;
            else if (comboBoxDeviceName.Text == "USB FDU04")
                device_name = SGFPMDeviceName.DEV_FDU04;
            else if (comboBoxDeviceName.Text == "USB FDU05")
                device_name = SGFPMDeviceName.DEV_FDU05;

            else if (comboBoxDeviceName.Text == "USB FDU07(U10)")
                device_name = SGFPMDeviceName.DEV_FDU07;
            else if (comboBoxDeviceName.Text == "USB FDU07A(U10A)")
                device_name = SGFPMDeviceName.DEV_FDU07A;

            else if (comboBoxDeviceName.Text == "USB FDU08(U20A)")
                device_name = SGFPMDeviceName.DEV_FDU08;
            else if (comboBoxDeviceName.Text == "USB FDU08P(U20AP)")
                device_name = SGFPMDeviceName.DEV_FDU08P;

            else if (comboBoxDeviceName.Text == "Auto Selection")
                device_name = SGFPMDeviceName.DEV_AUTO;

            if (device_name != SGFPMDeviceName.DEV_UNKNOWN)
            {
                error = m_FPM.Init(device_name);

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    m_FPM.CloseDevice();
                    error = m_FPM.OpenDevice(device_id);
                }

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
                    m_FPM.GetDeviceInfo(pInfo);
                    m_ImageWidth = pInfo.ImageWidth;
                    m_ImageHeight = pInfo.ImageHeight;
                }
            }
            else
                error = m_FPM.InitEx(m_ImageWidth, m_ImageHeight, m_Dpi);

            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                EnableButtons(true);
                StatusBar.Text = "Initialization Success";
            }
            else
            {
                EnableButtons(false);
                StatusBar.Text = "Init() Error " + error;
                return;
            }

            if (m_useAnsiTemplate)
            {
                // Set template format to ANSI 378
                error = m_FPM.SetTemplateFormat(SGFPMTemplateFormat.ANSI378);
            }
            else
            {
                // Set template format to ISO 19794-2
                error = m_FPM.SetTemplateFormat(SGFPMTemplateFormat.ISO19794);
            }

            // Get Max template size
            Int32 max_template_size = 0;
            error = m_FPM.GetMaxTemplateSize(ref max_template_size);

            m_RegMin1 = new Byte[max_template_size];
            m_RegMin2 = new Byte[max_template_size];
            m_VrfMin = new Byte[max_template_size];

            // OpenDevice if device is selected
            if (device_name != SGFPMDeviceName.DEV_UNKNOWN)
            {
                error = m_FPM.OpenDevice(device_id);
                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    m_DeviceOpened = true;
                }
                else
                {
                    StatusBar.Text = "OpenDevice() Error : " + error;
                    EnableButtons(false);
                }
            }
        }
        private void OpenDeviceBtn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            initDevice();
        }

        private void EnableButtons(bool enable)
        {
            BtnCapture1.Enabled = enable;
            BtnCapture2.Enabled = enable;
            //BtnCapture3.Enabled = enable;
            BtnRegister.Enabled = enable;
            //BtnVerify.Enabled = enable;
        }


        /////////////////////////////////////
        private void DrawImage(Byte[] imgData, PictureBox picBox)
        {
            int colorval;
            Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight);
            picBox.Image = (Image)bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorval = (int)imgData[(j * m_ImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            picBox.Refresh();
        }

        /////////////////////////////////////
        private Int32 GetImageFromFile(Byte[] data)
        {
            OpenFileDialog open_dlg;
            open_dlg = new OpenFileDialog();

            open_dlg.Title = "Image raw file dialog";
            open_dlg.Filter = "Image raw files (*.raw)|*.raw";

            if (open_dlg.ShowDialog() == DialogResult.OK)
            {
                FileStream inStream = File.OpenRead(open_dlg.FileName);

                BinaryReader br = new BinaryReader(inStream);

                Byte[] local_data = new Byte[data.Length];
                local_data = br.ReadBytes(data.Length);
                Array.Copy(local_data, data, data.Length);

                br.Close();
                return (Int32)SGFPMError.ERROR_NONE;
            }
            return (Int32)SGFPMError.ERROR_FUNCTION_FAILED;
        }
        private void FrmFingerScanAdd_Load(object sender, EventArgs e)
        {
            EnableButtons(false);
            // Init Button
            m_RadioButton = new RadioButton[11];
            m_RadioButton[0] = FPPosUnknown;
            m_RadioButton[1] = FPPosRightThumb;
            m_RadioButton[2] = FPPosRightIndex;
            m_RadioButton[3] = FPPosRightMiddle;
            m_RadioButton[4] = FPPosRightRing;
            m_RadioButton[5] = FPPosRightLittle;
            m_RadioButton[6] = FPPosLeftThumb;
            m_RadioButton[7] = FPPosLeftIndex;
            m_RadioButton[8] = FPPosLeftMiddle;
            m_RadioButton[9] = FPPosLeftRing;
            m_RadioButton[10] = FPPosLeftLittle;

            comboBoxDeviceName.SelectedIndex = 0;
            comboBoxSelFinger.SelectedIndex = 0;

            m_SecurityLevel = SGFPMSecurityLevel.NORMAL;
            m_StoredTemplate = null;
            m_ImageWidth = 260;
            m_ImageHeight = 300;
            m_Dpi = 500;
            m_FPM = new SGFingerPrintManager();

            StatusBar.Text = "Click Init Button";

            if (m_useAnsiTemplate)
            {
                this.Text = "Matching Test: ANSI 378 Format";
            }
            else
            {
                this.Text = "Matching Test: ISO 19794-2 Format";
            }
            initDevice();
            comboBoxSelFinger.SelectedIndex = 1;
        }
    }
}
