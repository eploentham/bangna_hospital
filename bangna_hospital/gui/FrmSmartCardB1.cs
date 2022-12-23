using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmSmartCardB1 : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, famtB14, fque, fqueB, famtB30;
        static public int grdViewFontSize = 0, printBillTextFoodsSize = 0;
        Boolean statusStickerIPD = false, pageLoad = false, chkStkDfPrint = false;
        List<String> lSmartCard;

        Patient ptt;
        Queue queue, queueHi;
        String StartupPath = "";
        String fname = "", lname = "", PID = "", dob = "", addrno = "", districtname = "", amphurname = "", provincename = "", sex = "", prefixname = "", moo = "", trok = "", soi = "";
        String fnamee = "", lnamee = "", road = "", address = "", webcamname = "";

        RDNID mRDNIDWRAPPER;

        C1ThemeController theme;
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
        public FrmSmartCardB1(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            String err = "";
            try
            {
                mRDNIDWRAPPER = new RDNID();
                pageLoad = true;
                StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

                fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
                fEditS1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);
                fEdit = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
                fEditB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 3, FontStyle.Bold);
                fEdit5B = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Bold);
                famt = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
                famtB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
                famtB14 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
                famtB30 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
                ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
                fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
                fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
                fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
                ptt = new Patient();
                queue = new Queue();

                theme = new C1ThemeController();

                setControlCardReader();
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmSmartCardB1 initConfig err " + err + " " + ex.Message);
            }

            btnFingerScan.Click += BtnFingerScan_Click;
            btnReadCard.Click += BtnReadCard_Click;
            btnPatientNew.Click += BtnPatientNew_Click;
        }

        private void BtnPatientNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

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
            //ptt.mnc_ref

            //String re = bc.bcDB.pttDB.insert(ptt);
            String re = bc.bcDB.pttDB.insertPatient(ptt);
            long chk = 0;
            if (long.TryParse(re, out chk))
            {
                //MessageBox.Show("111111", "");
                
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByPID(m_txtID.Text.Trim(), "pid");
                if (ptt.Hn.Length > 0)
                {
                    lbPttName.Text = ptt.Name;
                    txtHn.Text = ptt.MNC_HN_NO;
                    
                    //bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient success");
                    chk = 0;
                    //Image img = picPtt.Image;
                    //var bmp = (Bitmap)img;
                    //byte[] arr;
                    //ImageConverter converter = new ImageConverter();
                    ////arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    //arr = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

                    MemoryStream ms = new MemoryStream();
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
                        btnPatientNew.Enabled = false;
                        btnPatientNew.BackColor = Color.Yellow;
                    }
                    else
                    {
                        bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient image error");
                    }
                }
            }
            else
            {
                MessageBox.Show("error " + re, "");
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPatientNew_Click", "insert patient error " + re);
            }
        }

        private void BtnReadCard_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            m_txtID.Text = "";
            m_txtFullNameT.Text = "";
            m_txtFullNameE.Text = "";
            m_txtAddress.Text = "";
            m_txtBrithDate.Text = "";
            m_txtGender.Text = "";
            m_txtIssueDate.Text = "";
            m_txtExpiryDate.Text = "";
            //m_txtIssueNum.Text = "";
            m_picPhoto.Image = null;

            ReadCard();
        }
        protected int ReadCard()
        {
            clearPatient();
            statusStickerIPD = false;
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
                MessageBox.Show(m);

                RDNID.disconnectCardRD(obj);
                RDNID.deselectReaderRD(obj);
                return nInsertCard;
            }

            //BindDataToScreen();
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
                string[] fields = NIDData.Split('#');

                m_txtID.Text = NIDNum;                             // or use m_txtID.Text = fields[(int)NID_FIELD.NID_Number];

                String fullname = fields[(int)NID_FIELD.TITLE_T] + " " +
                                    fields[(int)NID_FIELD.NAME_T] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_T] + " " +
                                    fields[(int)NID_FIELD.SURNAME_T];
                m_txtFullNameT.Text = fullname.Trim();

                fullname = fields[(int)NID_FIELD.TITLE_E] + " " +
                                    fields[(int)NID_FIELD.NAME_E] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_E] + " " +
                                    fields[(int)NID_FIELD.SURNAME_E];
                m_txtFullNameE.Text = fullname.Trim();

                m_txtBrithDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);
                String trok = "", moocard="", soi="", roadcard="";
                moocard = fields[(int)NID_FIELD.MOO].Length > 0 ? fields[(int)NID_FIELD.MOO].Trim() + "   " : "";
                trok = fields[(int)NID_FIELD.TROK].Length > 0 ? fields[(int)NID_FIELD.TROK].Trim() + "   " : "";
                soi = fields[(int)NID_FIELD.SOI].Length > 0 ? fields[(int)NID_FIELD.SOI].Trim() + "   " : "";
                roadcard = fields[(int)NID_FIELD.ROAD].Length > 0 ? fields[(int)NID_FIELD.ROAD].Trim() + "   " : "";
                m_txtAddress.Text = fields[(int)NID_FIELD.HOME_NO].Trim() + "   " +
                                        moocard +
                                        trok +
                                        soi +
                                        roadcard +
                                        fields[(int)NID_FIELD.TUMBON].Trim() + "   " +
                                        fields[(int)NID_FIELD.AMPHOE].Trim() + "   " +
                                        fields[(int)NID_FIELD.PROVINCE].Trim() + "   "
                                        ;
                m_txtAddress.Text = m_txtAddress.Text.Trim();
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
                //m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];

                String day = "", month = "", year = "", prefix = "", provid = "", provincename1 = "", amprid = "", districtid = "", poc = "";
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
                    dob = _yyyy + "-" + _mm + "-" + _dd;
                }
                catch (Exception ex)
                {
                    //sep.SetError(m_txtBrithDate, "");
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

                    ptt = bc.bcDB.pttDB.selectPatinetByPID(PID, "pid");
                    if (ptt.Hn.Length > 0)
                    {
                        PrakunM01 prak = new PrakunM01();
                        prak = bc.bcDB.prakM01DB.selectByPID(ptt.MNC_ID_NO);
                        if (prak.PrakanCode.Length > 0)
                        {
                            label1.Text = prak.PrakanCode.Equals("2210028") ? "สิทธิ บางนา 1" : prak.PrakanCode.Equals("2211006") ? "สิทธิ บางนา 2" : prak.PrakanCode.Equals("2211041") ? "สิทธิ บางนา 5" : "สิทธิ  ที่อื่น";
                        }
                        else
                        {
                            label1.Text = prak.PrakanCode;
                        }
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
                        
                        txtAddress.Text = ptt.MNC_CUR_ADD + " หมู่ที่ " + ptt.MNC_CUR_MOO + " " + ptt.MNC_CUR_SOI;
                        txtProvince.Text = bc.bcDB.pttDB.selectProvinceName(ptt.MNC_CUR_CHW);
                        txtAmphur.Text = bc.bcDB.pttDB.selectAmphurName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP);
                        txtDistrict.Text = bc.bcDB.pttDB.selectDistrictName(ptt.MNC_CUR_CHW, ptt.MNC_CUR_AMP, ptt.MNC_CUR_TUM);
                        txtMobile.Text = ptt.MNC_CUR_TEL;
                        txtPttAge.Text = ptt.AgeStringOK().Replace("Years", "Y").Replace("Months", "M").Replace("Days", "D");

                        

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
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        btnPatientNew.Enabled = true;
                        btnPatientNew.BackColor = Color.Green;
                        
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
                        
                        btnPatientNew.Enabled = true;
                        btnPatientNew.BackColor = Color.Green;
                        btnVisit.Enabled = true;
                        btnVisit.BackColor = Color.Green;
                        
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
                    btnPatientNew.Enabled = true;
                    //bc.bcDB.insertLogPage(bc.userId, this.Name, "ReadCard", "read card end dob" + dob);
                }
            }

            byte[] NIDPicture = new byte[1024 * 5];
            int imgsize = NIDPicture.Length;
            res = RDNID.getNIDPhotoRD(obj, NIDPicture, out imgsize);
            if (res != DefineConstants.NID_SUCCESS)
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
                Bitmap MyImage = new Bitmap(img, m_picPhoto.Width - 2, m_picPhoto.Height - 2);
                m_picPhoto.Image = (Image)MyImage;
            }

            RDNID.disconnectCardRD(obj);
            RDNID.deselectReaderRD(obj);

            return 0;
        }
        private void clearPatient()
        {
            pageLoad = true;
            
            lbPttName.Text = "";
            txtHn.Text = "";
            
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
            txtPaidType.Text = "01";
            lbPaidName.Text = "...";
            ptt = new Patient();
            queue = new Queue();
            btnPatientNew.Enabled = false;
            
            txtPreno.Text = "";
            txtVN.Text = "";
            label1.Text = "";
            
            
            pageLoad = false;
            Application.DoEvents();
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
        private void setControlCardReader()
        {
            String fileName = StartupPath + "\\RDNIDLib.DLD";
            //fileName = @"D:\source\bangna\NIDCardPlusCS\NIDCardPlusCS\bin\Debug\RDNIDLib.DLD";
            if (System.IO.File.Exists(fileName) == false)
            {
                MessageBox.Show("RDNIDLib.DLD not found");
            }
            try
            {
                txtPaidType.Text = bc.iniC.paidcode;
                
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
            catch (Exception ex)
            {
                //An attempt was made to load a program with an incorrect format. (Exception from HRESULT: 0x8007000B)
                //ถ้า error แบบนี้  ต้องเปลี่ยน Property Platform target เป็น x86
                new LogWriter("e", "FrmSmartCardB1 setControlCardReader  " + ex.Message);
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
        private void BtnFingerScan_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmFingerScanAdd frm = new FrmFingerScanAdd(bc,txtHn.Text.Trim(), m_txtFullNameT.Text.Trim());
            frm.ShowDialog(this);
        }

        private void FrmSmartCardB1_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 2022-09-29";
            c1DockingTab1.SelectedTab = tabPtt;
            btnPatientNew.Enabled = false;
            btnPatientNew.BackColor = Color.Green;
        }
    }
}
