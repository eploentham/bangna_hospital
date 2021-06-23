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
        Font fEdit, fEditB, fEdit3B, fEdit5B;

        //RDNIDWRAPPER.RDNID mRDNIDWRAPPER = new RDNIDWRAPPER.RDNID();
        string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        String fname = "", lname = "", PID="", dob="", addrno="", districtname="", amphurname="", provincename="", sex="", prefixname="", moo="", trok="" ,soi="";
        String fnamee = "", lnamee = "" ,road="", address="";
        List<String> lSmartCard;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Boolean flagInsert = false;
        Patient ptt;
        public FrmSmartCard(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            string fileName = StartupPath + "\\RDNIDLib.DLD";
            //fileName = @"D:\source\bangna\NIDCardPlusCS\NIDCardPlusCS\bin\Debug\RDNIDLib.DLD";
            if (System.IO.File.Exists(fileName) == false)
            {
                MessageBox.Show("RDNIDLib.DLD not found");
            }
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();

            ptt = new Patient();

            btnPatientNew.Click += BtnPatientNew_Click;
            txtPaidType.KeyUp += TxtPaidType_KeyUp;
            m_txtID.KeyUp += M_txtID_KeyUp;

            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = String.Format("R&D NID Card Plus C# {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

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

            //m_lblDLDInfo.Text = aByteToString(Licinfo);

            byte[] Softinfo = new byte[1024];
            RDNID.getSoftwareInfoRD(Softinfo);
            //m_lblSoftwareInfo.Text = aByteToString(Softinfo);
            lSmartCard = new List<string>();
            ListCardReader();
        }

        private void M_txtID_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                ptt = bc.bcDB.pttDB.selectPatinetByPID(m_txtID.Text.Trim());
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
                    ptt = bc.bcDB.pttDB.selectPatinet(m_txtID.Text.Trim());
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
                lbPaidName.Text = "";
                String paidname = "";
                paidname = bc.bcDB.finM02DB.SelectpaidTypeName(txtPaidType.Text.Trim());
                lbPaidName.Text = paidname;
            }
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
            //ptt.mnc_ref

            String re = bc.bcDB.pttDB.insert(ptt);
            long chk = 0;
            if(long.TryParse(re, out chk))
            {
                flagInsert = true;
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByPID(m_txtID.Text.Trim());
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
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient error");
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
            txtRefName.Text = "";
            cboRefType.Text = "";
            txtRefMobile.Text = "";
            txtRefAddrNo.Text = "";
            txtRefMoo.Text = "";
            txtRefSoi.Text = "";
            txtRefRoad.Text = "";
            cboAttactNote.Text = "";
            txtPaidType.Text = "";
            lbPaidName.Text = "...";
            ptt = new Patient();
            btnPatientNew.Enabled = false;
            picPtt.Image = null;
            Application.DoEvents();
        }
        protected int ReadCard()
        {
            clearPatient();
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



                    ptt = bc.bcDB.pttDB.selectPatinetByPID(PID);
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
                        btnPatientNew.Enabled = false;
                        btnPatientNew.BackColor = Color.Yellow;
                    }
                    else
                    {
                        btnPatientNew.Enabled = true;
                        btnPatientNew.BackColor = Color.Green;
                        DateTime dob1 = new DateTime();
                        DateTime.TryParse(dob, out dob1);

                        ptt.MNC_HN_NO = "";
                        ptt.MNC_HN_YR = "";
                        ptt.MNC_PFIX_CDT = prefix;
                        ptt.MNC_PFIX_CDE = "";
                        ptt.MNC_FNAME_T = fname;
                        ptt.MNC_LNAME_T = lname;
                        ptt.MNC_FNAME_E = fnamee;
                        ptt.MNC_LNAME_E = lnamee;
                        ptt.MNC_AGE = "";
                        ptt.MNC_BDAY = dob;
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
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "ReadCard", "read card end");
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
        private void FrmSmartCard_Load(object sender, EventArgs e)
        {
            if (bc.iniC.statusSmartCardNoDatabase.Equals("1"))
            {
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmSmartCard_Load", "FrmSmartCard_Load");
            }
            this.Text += " Update 2021-06-23";
            btnPatientNew.Enabled = false;
            tC1.SelectedTab = tabPttNew;
            tCBn1.SelectedTab = tabBn1Ptt;
        }
    }
}
