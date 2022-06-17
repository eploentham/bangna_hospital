using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Excel;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.C1Tile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmReception : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient ptt;
        C1FlexGrid grfSrc, grfPttComp, grfPttVs, grfPttApm, grfRptReport, grfRptData;
        C1ThemeController theme1;

        Label lbLoading;

        int colgrfSrcHn = 1, colgrfSrcFullNameT = 2, colgrfSrcPID = 3, colgrfSrcDOB = 4, colgrfSrcPttid=5;
        int celgrfPttCompCode = 1, colgrfPttCompNameT = 2, colgrfPttCompid = 3;
        int colgrfPttVsVsDate = 1, colgrfPttVsHn=2, colgrfPttVsPaid=3, colgrfPttVsPreno=4, colgrfPttVsDept=5, colgrfPttVsStatusOPD=6, colgrfPttVsSymptom=7;
        int colgrfPttApmVsDate = 1, colgrfPttApmApmDate = 2, colgrfPttApmDept = 3, colgrfPttApmNote = 4;
        int colgrfRptReportCode = 1, colgrfRptReportName = 2;
        int colgrfRptDatadailydeptDate = 1,colgrfRptDatadailydeptTime = 2, colgrfRptQueno=3,colgrfRptDatadailydeptHn = 4, colgrfRptDatadailydeptName = 5, colgrfRptDatadailydeptMobile = 6, colgrfRptDatadailyDrugSet=7, colgrfRptDatadailyXray = 8, colgrfRptDatadailyAuthen = 9, colgrfRptDatadailyPicKYC = 10, colgrfRptDatadailyPicFoodsdaily = 11;
        String rptCode = "";
        Boolean pageLoad = false;
        Image imgCorr, imgTran;
        C1TileControl TileFoods;
        PanelElement peOrd;
        ImageElement ieOrd;
        TextElement teOrd;
        
        public FrmReception(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            theme1 = new C1ThemeController();
            imgCorr = Resources.red_checkmark_png_16;
            imgTran = Resources.DeleteTable_small;

            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);

            bc.bcDB.pm02DB.setCboPrefixT(cboPttPrefixT, "");
            bc.bcDB.pm02DB.setCboPrefixE(cboPttPrefixE, "");
            bc.bcDB.pm07DB.setCboTumbon(cboPttIDTambon,"", "");
            bc.bcDB.pm07DB.setCboTumbon(cboPttCurTambon,"", "");
            bc.bcDB.pm07DB.setCboTumbon(cboPttRefTambon,"", "");
            bc.bcDB.pm08DB.setCboAmphur(cboPttIDAmphur, "", "");
            bc.bcDB.pm08DB.setCboAmphur(cboPttCurAmphur, "", "");
            bc.bcDB.pm08DB.setCboAmphur(cboPttRefAmphur, "", "");
            bc.bcDB.pm09DB.setCboProvince(cboPttIDProv, "");
            bc.bcDB.pm09DB.setCboProvince(cboPttCurProv, "");
            bc.bcDB.pm09DB.setCboProvince(cboPttRefProv, "");

            bc.bcDB.pm03DB.setCboEdu(cboPttEdu, "");
            bc.bcDB.pm04DB.setCboNation(cboPttNat, "");
            bc.bcDB.pm05DB.setCboRel(cboPttRel, "");
            if (bc.iniC.statusStation.Equals("OPD"))
            {
                bc.bcDB.pttDB.setCboDeptOPD(cboRptDept, bc.iniC.station);
            }
            else
            {
                bc.bcDB.pttDB.setCboDeptIPD(cboRptDept, bc.iniC.station);
            }
            bc.bcDB.pm09DB.setCboProvince(cboPttIDProv, "");
            bc.bcDB.pm09DB.setCboProvince(cboPttCurProv, "");
            bc.bcDB.pm09DB.setCboProvince(cboPttRefProv, "");

            txtRptDateStart.Value = DateTime.Now;
            txtRptDateEnd.Value = DateTime.Now;
            //bc.bcDB.pm09DB.setCboProvince(cboPttRefProv, "");
            //bc.bcDB.pm09DB.setCboProvince(cboPttRefProv, "");

            setEvent();
            setCboTheme();

            setTheme(bc.iniC.themeApp);
            initGrfSrc();
            initGrfPttComp();
            initGrfPttVs();
            initGrfPttApm();
            initGrfRptReport();
            initGrfRptData();
            initTileImg();

            int nres = 0;
            byte[] _lic = String2Byte(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\RDNIDLib.DLD");
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
            ListCardReader();
            
            pageLoad = false;
        }
        private void setEvent()
        {
            btnSrcNew.Click += BtnSrcNew_Click;
            txtSrcHn.KeyUp += TxtSrcHn_KeyUp;
            cboPttIDTambon.SelectedItemChanged += CboPttIDTambon_SelectedItemChanged;
            cboPttIDTambon.KeyUp += CboPttIDTambon_KeyUp;
            txtPttCompCode.KeyUp += TxtPttCompCode_KeyUp;
            cboPttCurTambon.SelectedItemChanged += CboPttCurTambon_SelectedItemChanged;
            cboPttCurTambon.KeyUp += CboPttCurTambon_KeyUp;
            cboPttRefTambon.SelectedItemChanged += CboPttRefTambon_SelectedItemChanged;
            cboPttRefTambon.KeyUp += CboPttRefTambon_KeyUp;

            cboPttPrefixT.DropDownClosed += CboPttPrefixT_DropDownClosed;
            txtPttNameT.KeyUp += TxtPttNameT_KeyUp;
            txtPttSurNameT.KeyUp += TxtPttSurNameT_KeyUp;
            txtPttNameE.KeyUp += TxtPttNameE_KeyUp;
            txtPttSurNameE.KeyUp += TxtPttSurNameE_KeyUp;
            txtPttPID.KeyUp += TxtPttPID_KeyUp;
            txtPttSsn.KeyUp += TxtPttSsn_KeyUp;
            txtPttPassport.KeyUp += TxtPttPassport_KeyUp;
            txtPttMobile1.KeyUp += TxtPttMobile1_KeyUp;
            txtPttMobile2.KeyUp += TxtPttMobile2_KeyUp;
            txtPttEmail.KeyUp += TxtPttEmail_KeyUp;
            txtPttIDHomeNo.KeyUp += TxtPttIDHomeNo_KeyUp;
            txtPttIDMoo.KeyUp += TxtPttIDMoo_KeyUp;
            txtPttIDSoi.KeyUp += TxtPttIDSoi_KeyUp;
            txtPttIDRoad.KeyUp += TxtPttIDRoad_KeyUp;
            txtPttDOB.DropDownClosed += TxtPttDOB_DropDownClosed;
            cboPttIDTambon.DropDownClosed += CboPttIDTambon_DropDownClosed;
            cboPttCurTambon.DropDownClosed += CboPttCurTambon_DropDownClosed;
            cboPttRefTambon.DropDownClosed += CboPttRefTambon_DropDownClosed;
            txtPttCurHomeNo.KeyUp += TxtPttCurHomeNo_KeyUp;
            txtPttCurMoo.KeyUp += TxtPttCurMoo_KeyUp;
            txtPttCurSoi.KeyUp += TxtPttCurSoi_KeyUp;
            txtPttCurRoad.KeyUp += TxtPttCurRoad_KeyUp;
            btnRptOk.Click += BtnRptOk_Click;
            btnRptExcel.Click += BtnRptExcel_Click;
            txtPttRefHomeNo.KeyUp += TxtPttRefHomeNo_KeyUp;
            txtPttRefMoo.KeyUp += TxtPttRefMoo_KeyUp;
            txtPttRefSoi.KeyUp += TxtPttRefSoi_KeyUp;
            txtPttRefRoad.KeyUp += TxtPttRefRoad_KeyUp;
            txtPttRefContact1Name.KeyUp += TxtPttRefContact1Name_KeyUp;
            txtPttRefContact1Mobile.KeyUp += TxtPttRefContact1Mobile_KeyUp;
            txtPttRefContact2Name.KeyUp += TxtPttRefContact2Name_KeyUp;
            txtPttRefContact2Mobile.KeyUp += TxtPttRefContact2Mobile_KeyUp;
            txtPttCompCode.KeyUp += TxtPttCompCode_KeyUp1;
            txtPttInsur.KeyUp += TxtPttInsur_KeyUp;
            cboPttNat.DropDownClosed += CboPttNat_DropDownClosed;
            cboPttRel.DropDownClosed += CboPttRel_DropDownClosed;
            cboPttEdu.DropDownClosed += CboPttEdu_DropDownClosed;
            cboPttMarri.DropDownClosed += CboPttMarri_DropDownClosed;
            txtPttAttchNote.KeyUp += TxtPttAttchNote_KeyUp;
            txtPttRemark1.KeyUp += TxtPttRemark1_KeyUp;
            btnPttCardRead.Click += BtnPttCardRead_Click;
            btnPttLIC.Click += BtnPttLIC_Click;
            btnPttIdCopyto.Click += BtnPttIdCopyto_Click;
            btnPttCurCopyto.Click += BtnPttCurCopyto_Click;
            txtPttHn.KeyUp += TxtPttHn_KeyUp;
            cboTheme.SelectedIndexChanged += CboTheme_SelectedIndexChanged;

            btnPttSave.Click += BtnPttSave_Click;
            btnDocSearch.Click += BtnDocSearch_Click;


        }

        private void BtnDocSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setTiltImg(txtDocHn.Text.Trim());
        }

        private void setPattient()
        {
            DateTime dob = new DateTime();
            DateTime.TryParse(txtPttDOB.Text, out dob);
            if (dob.Year < 1900)
            {
                dob = dob.AddYears(543);
            }
            ptt = new Patient();
            ptt.MNC_HN_NO = txtPttHn.Text.Trim();
            ptt.passport = txtPttPassport.Text.Trim();
            ptt.MNC_HN_YR = "";
            ptt.MNC_PFIX_CDT = "01";
            ptt.MNC_PFIX_CDE = "";
            ptt.MNC_FNAME_T = txtPttNameT.Text.Trim(); ;
            ptt.MNC_LNAME_T = txtPttSurNameT.Text.Trim();
            ptt.MNC_FNAME_E = txtPttNameE.Text.Trim();
            ptt.MNC_LNAME_E = txtPttSurNameE.Text.Trim();
            ptt.MNC_AGE = "";
            ptt.MNC_BDAY = dob.Year.ToString()+"-"+ dob.ToString("MM-dd");
            ptt.MNC_ID_NO = txtPttPID.Text.Trim();
            ptt.MNC_SS_NO = txtPttSsn.Text.Trim();
            ptt.MNC_SEX = "";
            ptt.MNC_FULL_ADD = "";
            ptt.MNC_STAMP_DAT = "";
            ptt.MNC_STAMP_TIM = "";

            ptt.MNC_CUR_ADD = "";
            ptt.MNC_CUR_TUM = "";
            ptt.MNC_CUR_AMP = "";
            ptt.MNC_CUR_CHW = "";
            ptt.MNC_CUR_POC = "";
            ptt.MNC_CUR_TEL = txtPttMobile1.Text.Trim();
            ptt.MNC_DOM_ADD = "";
            ptt.MNC_DOM_TUM = "";
            ptt.MNC_DOM_AMP = "";
            ptt.MNC_DOM_CHW = "";
            ptt.MNC_DOM_POC = "";
            ptt.MNC_DOM_TEL = "";

            ptt.MNC_REF_NAME = "";
            ptt.MNC_REF_ADD = "";
            ptt.MNC_REF_TUM = "";
            ptt.MNC_REF_AMP = "";
            ptt.MNC_REF_CHW = "";
            ptt.MNC_REF_POC = "";
            ptt.MNC_REF_TEL = "";

            ptt.MNC_CUR_MOO = "";
            ptt.MNC_DOM_MOO = "";
            ptt.MNC_REF_MOO = "";
            ptt.MNC_CUR_SOI = "";
            ptt.MNC_DOM_SOI = "";
            ptt.MNC_REF_SOI = "";
            ptt.MNC_FULL_ADD = "";
            ptt.MNC_COM_CD = "";
            ptt.MNC_COM_CD2 = "";
        }
        private void initTileImg()
        {
            TileFoods = new C1TileControl();
            TileFoods.Orientation = LayoutOrientation.Vertical;

            peOrd = new C1.Win.C1Tile.PanelElement();
            ieOrd = new C1.Win.C1Tile.ImageElement();
            peOrd.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            peOrd.Children.Add(ieOrd);
            TileFoods.DefaultTemplate.Elements.Add(peOrd);
            TileFoods.Dock = DockStyle.Fill;

            //TileFoods[i].Templates.Add(this.tempFlickr);
            //TileFoods = new C1TileControl();
            //TileFoods[i].Name = "tile" + i;
            //TileFoods[i].Dock = DockStyle.Fill;
            //TileFoods[i].BackColor = tilecolor;     // tile color
                                                    //pnOrder.Controls.Add(TileFoods);                    
            TileFoods.ScrollOffset = 0;
            TileFoods.SurfaceContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            TileFoods.Padding = new System.Windows.Forms.Padding(0);
            TileFoods.GroupPadding = new System.Windows.Forms.Padding(10);

            pnDocView.Controls.Add(TileFoods);
        }
        private void setTiltImg(String hn)
        {
            DataTable dtrow = new DataTable();
            //lFoot = new List<FoodsTopping>();
            TileFoods.BeginUpdate();
            dtrow = bc.bcDB.dscDB.selectStatus4ByHn(hn);
            //lFoot = mposC.mposDB.footpDB.getlFooSpecByFooId(fooId);
            //lFoos = lfooC1;
            Group gr1 = new Group();
            TileFoods.Groups.Add(gr1);
            TileCollection tiles = TileFoods.Groups[0].Tiles;
            tiles.Clear(true);
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
            //foreach (DataRow drow in dt.Rows)
            foreach (DataRow foos in dtrow.Rows)
            {
                var tile = new Tile();
                tile.HorizontalSize = 5;
                tile.VerticalSize = 4;
                //tile.Template = tempSpec;
                tile.Text = foos["row_no"].ToString();
                //tile.Text1 = "ราคา " + foo1.foods_price;
                tile.Tag = foos;
                tile.Name = foos["doc_scan_id"].ToString();
                //tile.Click += TileTopping_Click;
                tile.Image = null;
                try
                {
                    //tile.Image = null;
                    MemoryStream stream = new MemoryStream();
                    Image loadedImage = null, resizedImage;
                    if (foos["image_path"].ToString().Equals("")) continue;
                    stream = ftp.download(foos[bc.bcDB.dscDB.dsc.folder_ftp].ToString() + "//" + foos["image_path"].ToString());
                    if (stream.Length == 0) continue;
                    loadedImage = new Bitmap(stream);
                    int newWidth = 540;
                    resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / loadedImage.Width, null, IntPtr.Zero);
                    tile.Image = resizedImage;
                    
                    tiles.Add(tile);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("" + ex.Message, "showImg");
                }
                //foos.statusUs = "";
            }
            TileFoods.EndUpdate();
        }
        private void BtnPttSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setPattient();
            String re = bc.bcDB.pttDB.insertPatientStep1(ptt);
            long chk = 0;
            if(long.TryParse(re, out chk))
            {
                lfSbStatus.Text = "update OK";
            }
        }
        private void CboTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            setTheme(cboTheme.Text);
            pageLoad = false;
        }

        private void TxtPttHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void BtnPttCurCopyto_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            pageLoad = true;
            txtPttRefHomeNo.Value = txtPttIDHomeNo.Text.Trim();
            txtPttRefMoo.Value = txtPttIDMoo.Text.Trim();
            txtPttRefSoi.Value = txtPttIDSoi.Text.Trim();
            txtPttRefRoad.Value = txtPttIDRoad.Text.Trim();
            txtPttRefPostcode.Value = txtPttIDPostcode.Text.Trim();
            bc.cloneComboBox(cboPttIDTambon,ref cboPttRefTambon);
            bc.cloneComboBox(cboPttIDAmphur,ref cboPttRefAmphur);
            //bc.cloneComboBox(cboPttIDProv,ref cboPttRefProv);
            cboPttRefProv.SelectedIndex = cboPttIDProv.SelectedIndex;
            pageLoad = false;
            hideLbLoading();
        }

        private void BtnPttIdCopyto_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            pageLoad = true;
            txtPttCurHomeNo.Value = txtPttIDHomeNo.Text.Trim();
            txtPttCurMoo.Value = txtPttIDMoo.Text.Trim();
            txtPttCurSoi.Value = txtPttIDSoi.Text.Trim();
            txtPttCurRoad.Value = txtPttIDRoad.Text.Trim();
            txtPttCurPostcode.Value = txtPttIDPostcode.Text.Trim();
            bc.cloneComboBox(cboPttIDTambon,ref cboPttCurTambon);
            bc.cloneComboBox(cboPttIDAmphur,ref cboPttCurAmphur);
            //bc.cloneComboBox(cboPttIDProv,ref cboPttCurProv);
            cboPttIDProv.SelectedIndex = cboPttCurProv.SelectedIndex;
            pageLoad = false;
            hideLbLoading();
        }

        private void BtnPttLIC_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            byte[] _lic = String2Byte(StartupPath + "\\RDNIDLib.DLD");
            int res = RDNID.updateLicenseFileRD(_lic);
            if (res != 0)
            {
                string s = string.Format("Error : {0}", res);
                MessageBox.Show(s);
            }
        }

        private void BtnPttCardRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ReadCard();
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByPID(txtPttPID.Text.Trim(),"pid");
            setControl(ptt,"smartcard");
        }
        private void setControl(Patient ptt, String flag)
        {
            txtPttHn.Value = ptt.MNC_HN_NO;
            if (!flag.Equals("smartcard"))
            {
                txtPttNameT.Value = ptt.MNC_FNAME_T;
                txtPttSurNameT.Value = ptt.MNC_LNAME_T;
                txtPttNameE.Value = ptt.MNC_FNAME_E;
                txtPttSurNameE.Value = ptt.MNC_LNAME_E;
                DateTime dtime = new DateTime();
                DateTime.TryParse(ptt.MNC_BDAY, out dtime);
                txtPttDOB.Value = dtime;
                txtPttPID.Value = ptt.MNC_ID_NO;
                bc.setC1Combo(cboPttPrefixT, ptt.MNC_PFIX_CDT);
                txtPttIDHomeNo.Value = ptt.MNC_DOM_ADD;
                txtPttIDSoi.Value = ptt.MNC_DOM_SOI;
                //txtPttIDRoad.Value = ptt.MNC_DOM_;
            }
            txtPttSsn.Value = ptt.MNC_SS_NO;
            txtPttPassport.Value = ptt.passport;
            
        }
        protected int ReadCard()
        {
            //clearPatient();
            pageLoad = true;
            showLbLoading();
            //if (bc.iniC.statusSmartCardNoDatabase.Equals("1"))
            //{
            //    bc.bcDB.insertLogPage(bc.userId, this.Name, "ReadCard", "read card start");
            //}
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
                String day = "", month = "", year = "", prefix = "", provid = "", provincename1 = "", amprid = "", districtid = "", poc = "", dob="";
                string[] fields = NIDData.Split('#');

                txtPttPID.Text = NIDNum;                             // or use m_txtID.Text = fields[(int)NID_FIELD.NID_Number];

                String fullname = fields[(int)NID_FIELD.TITLE_T] + " " +
                                    fields[(int)NID_FIELD.NAME_T] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_T] + " " +
                                    fields[(int)NID_FIELD.SURNAME_T];
                txtPttNameT.Value = fields[(int)NID_FIELD.NAME_T].Trim();
                txtPttSurNameT.Value = fields[(int)NID_FIELD.SURNAME_T].Trim();
                txtPttNameE.Value = fields[(int)NID_FIELD.NAME_E].Trim();
                txtPttSurNameE.Value = fields[(int)NID_FIELD.SURNAME_E].Trim();

                //m_txtBrithDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);
                string _yyyy = "",_mm = "", _dd = "";

                String addr = fields[(int)NID_FIELD.HOME_NO] + " " + fields[(int)NID_FIELD.MOO] + " " + fields[(int)NID_FIELD.TROK] + " " + fields[(int)NID_FIELD.SOI] + " " + fields[(int)NID_FIELD.ROAD] + " " + fields[(int)NID_FIELD.TUMBON] + " " +fields[(int)NID_FIELD.AMPHOE] + " " + fields[(int)NID_FIELD.PROVINCE];

                txtPttIDHomeNo.Value = fields[(int)NID_FIELD.HOME_NO].Trim();
                txtPttIDMoo.Value = fields[(int)NID_FIELD.MOO].Trim().Replace("หมู่ที่", "").Trim();
                txtPttIDSoi.Value = fields[(int)NID_FIELD.SOI].Trim();
                txtPttIDRoad.Value = fields[(int)NID_FIELD.ROAD].Trim();
                //txtPttIDHomeNo.Value = fields[(int)NID_FIELD.HOME_NO].Trim();

                prefix = bc.bcDB.pttDB.selectProfixId(fields[(int)NID_FIELD.TITLE_T].Trim());
                provincename1 = fields[(int)NID_FIELD.PROVINCE].Trim().Replace("จังหวัด", "");
                provid = bc.bcDB.pttDB.selectProvinceId(provincename1);
                amprid = bc.bcDB.pttDB.selectAmphurId(provid, fields[(int)NID_FIELD.AMPHOE].Trim().Replace("อำเภอ", ""));
                districtid = bc.bcDB.pttDB.selectDistrictId(provid, amprid, fields[(int)NID_FIELD.TUMBON].Trim().Replace("ตำบล", ""));
                poc = bc.bcDB.pttDB.selectPOCId(provid, amprid, districtid);

                bc.setC1Combo(cboPttIDProv, provid);
                bc.bcDB.pm08DB.setCboAmphurByProvCode(cboPttIDAmphur, provid, amprid);
                bc.bcDB.pm07DB.setCboTambonByAmphrCode(cboPttIDTambon, amprid, districtid);
                txtPttIDPostcode.Value = poc;

                cboPttSex.Text = fields[(int)NID_FIELD.GENDER].Trim() == "1" ? "M" : "F";
                //m_txtIssueDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.ISSUE_DATE]);
                //m_txtExpiryDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.EXPIRY_DATE]);
                //if ("99999999" == m_txtExpiryDate.Text)
                //    m_txtExpiryDate.Text = "99999999 ตลอดชีพ";
                //m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];

                try
                {
                    _yyyy = fields[(int)NID_FIELD.BIRTH_DATE].Substring(0, 4);
                    _mm = fields[(int)NID_FIELD.BIRTH_DATE].Substring(4, 2);
                    _dd = fields[(int)NID_FIELD.BIRTH_DATE].Substring(6, 2);
                    dob = _yyyy + "-" + _mm + "-" + _dd;
                    DateTime dtime = new DateTime();
                    DateTime.TryParse(dob, out dtime);
                    txtPttDOB.Value = dtime;
                }
                catch (Exception ex)
                {
                    //sep.SetError(fields[(int)NID_FIELD.BIRTH_DATE], "");
                    lfSbMessage.Text = fields[(int)NID_FIELD.BIRTH_DATE]+" "+ex.Message;
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
                //m_picPhoto.Image = img;
                Bitmap MyImage = new Bitmap(img, m_picPhoto.Width - 2, m_picPhoto.Height - 2);
                m_picPhoto.Image = (Image)MyImage;
            }

            RDNID.disconnectCardRD(obj);
            RDNID.deselectReaderRD(obj);
            hideLbLoading();
            pageLoad = true;
            return 0;
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
        public IntPtr selectReader(String reader)
        {
            IntPtr mCard = (IntPtr)0;
            byte[] _reader = String2Byte(reader);
            IntPtr res = (IntPtr)RDNID.selectReaderRD(_reader);
            if ((Int64)res > 0)
                mCard = (IntPtr)res;
            return mCard;
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
            catch(Exception ex)
            {

            }
        }
        private void TxtPttRemark1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //cboPttNat.Focus();
                txtPttRemark2.SelectAll();
                txtPttRemark2.Focus();
            }
        }

        private void TxtPttAttchNote_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //cboPttNat.Focus();
                txtPttRemark1.SelectAll();
                txtPttRemark1.Focus();
            }
        }

        private void CboPttMarri_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttRace.Focus();
        }

        private void CboPttEdu_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttMarri.Focus();
        }

        private void CboPttRel_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttEdu.Focus();
        }

        private void CboPttNat_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttRel.Focus();
        }

        private void TxtPttInsur_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //cboPttNat.Focus();
                txtPttAttchNote.SelectAll();
                txtPttAttchNote.Focus();
            }
        }

        private void TxtPttCompCode_KeyUp1(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttInsur.SelectAll();
                txtPttInsur.Focus();
            }
        }

        private void TxtPttRefContact2Mobile_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttCompCode.SelectAll();
                txtPttCompCode.Focus();
            }
        }

        private void TxtPttRefContact2Name_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttRefContact2Mobile.SelectAll();
                txtPttRefContact2Mobile.Focus();
            }
        }

        private void TxtPttRefContact1Mobile_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttRefContact2Name.SelectAll();
                txtPttRefContact2Name.Focus();
            }
        }

        private void TxtPttRefContact1Name_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttRefContact1Mobile.SelectAll();
                txtPttRefContact1Mobile.Focus();
            }
        }

        private void CboPttRefTambon_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtPttRefContact1Name.SelectAll();
            txtPttRefContact1Name.Focus();
        }

        private void TxtPttRefRoad_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                cboPttRefTambon.Focus();
            }
        }

        private void TxtPttRefSoi_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttRefRoad.SelectAll();
                txtPttRefRoad.Focus();
            }
        }

        private void TxtPttRefMoo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttRefSoi.SelectAll();
                txtPttRefSoi.Focus();
            }
        }

        private void TxtPttRefHomeNo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttRefMoo.SelectAll();
                txtPttRefMoo.Focus();
            }
        }

        private void CboPttCurTambon_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtPttRefHomeNo.SelectAll();
            txtPttRefHomeNo.Focus();
        }

        private void BtnRptExcel_Click(object sender, EventArgs e)
        {
            DateTime datestart = new DateTime();
            DateTime dateend = new DateTime();
            DateTime.TryParse(txtRptDateStart.Text, out datestart);
            DateTime.TryParse(txtRptDateEnd.Text, out dateend);
            //throw new NotImplementedException();
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "xls";
            dlg.FileName = "*.xls";
            dlg.Filter = "Excel Files | *.xls";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            C1XLBook _book = new C1XLBook();
            XLSheet sheet = _book.Sheets.Add(datestart.ToString("dd-MM-yyyy") + "-" + dateend.ToString("dd-MM-yyyy"));
            bc.SaveSheet(grfRptData, sheet, _book, false);
            //ic.SaveSheet(grfCld, sheet, _book, false);
            //}

            // save selected sheet index
            _book.Sheets.SelectedIndex = 1;

            // save the book
            _book.Save(dlg.FileName);

            //String filename = "";
            //filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\doc-nurse-dotm\\กอร์ดอน-pop.docm";
            if (File.Exists(dlg.FileName))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = dlg.FileName,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            else
            {
                MessageBox.Show("File not found", "");
            }
        }

        private void BtnRptOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
            if (rptCode.Length <= 0)
            {
                lfSbMessage.Text = rptCode+" no select report";
                return;
            }
            if (rptCode.Equals("reportdailydept"))
            {
                setGrfRptReportDailyDept();
            }
            else if (rptCode.Equals("reportdailydeptHI"))
            {
                setGrfRptReportDailyDeptHi();
            }
            else if (rptCode.Equals("reportdailydeptHIATK"))
            {
                setGrfRptReportDailyDeptHiATK();
            }
        }
        private void setGrfRptReportDailyDept()
        {
            DateTime datestart = new DateTime();
            DateTime dateend = new DateTime();
            DataTable dt = new DataTable();
            String deptid = "";
            try
            {
                showLbLoading();
                deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(((ComboBoxItem)cboRptDept.SelectedItem).Value);
                DateTime.TryParse(txtRptDateStart.Text, out datestart);
                DateTime.TryParse(txtRptDateEnd.Text, out dateend);
                lfSbMessage.Text = datestart.ToString("yyyy-MM-dd") + " " + dateend.ToString("yyyy-MM-dd") + " " + ((ComboBoxItem)cboRptDept.SelectedItem).Value;
                dt = bc.bcDB.vsDB.selectPttHiinDept(deptid, ((ComboBoxItem)cboRptDept.SelectedItem).Value, datestart.ToString("yyyy-MM-dd"), dateend.ToString("yyyy-MM-dd"));
                int i = 1, j = 1;
                grfRptData.Cols.Count = 12;
                grfRptData.Rows.Count = 1;
                grfRptData.Cols[colgrfRptDatadailydeptDate].Width = 110;
                grfRptData.Cols[colgrfRptDatadailydeptTime].Width = 50;
                grfRptData.Cols[colgrfRptDatadailydeptHn].Width = 90;
                grfRptData.Cols[colgrfRptDatadailydeptName].Width = 280;
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Width = 120;
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyAuthen].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptQueno].Width = 50;
                
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyXray].Width = 50;

                grfRptData.Cols[colgrfRptDatadailydeptDate].Caption = "date";
                grfRptData.Cols[colgrfRptDatadailydeptTime].Caption = "time";
                grfRptData.Cols[colgrfRptDatadailydeptHn].Caption = "hn";
                grfRptData.Cols[colgrfRptDatadailydeptName].Caption = "name";
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Caption = "mobile";
                //grfRptData.Cols[colgrfRptDatadailyDrugSet].Caption = "set";
                //grfRptData.Cols[colgrfRptDatadailyAuthen].Caption = "authen";
                //grfRptData.Cols[colgrfRptDatadailyPicFoods].Caption = "Foods";
                //grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Caption = "Foods1";
                //grfRptData.Cols[colgrfRptDatadailyXray].Caption = "xray";

                grfRptData.Rows.Count = dt.Rows.Count + 1;
                //Column colAuthen = grfRptData.Cols[colgrfRptDatadailyAuthen];
                //colAuthen.DataType = typeof(Image);
                //Column colPic = grfRptData.Cols[colgrfRptDatadailyPic];
                //colPic.DataType = typeof(Image);
                //pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row1 in dt.Rows)
                {
                    //pB1.Value++;
                    Row rowa = grfRptData.Rows[i];
                    rowa[colgrfRptDatadailydeptDate] = row1["MNC_date"].ToString();
                    rowa[colgrfRptDatadailydeptTime] = row1["MNC_time"].ToString();
                    rowa[colgrfRptDatadailydeptHn] = row1["mnc_hn_no"].ToString();
                    rowa[colgrfRptDatadailydeptName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                    rowa[colgrfRptDatadailydeptMobile] = row1["mnc_cur_tel"].ToString();
                    //rowa[colgrfSrcPttid] = "";
                    //row1[colgrfRptDatadailyAuthen] = row1["status_authen"] != null ? row1["status_authen"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;
                    //row1[colgrfRptDatadailyPic] = row1["status_pic_kyc"] != null ?  row1["status_pic_kyc"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;
                    //row1[colgrfRptDatadailyAuthen] = "";
                    //row1[colgrfRptDatadailyPic] = "";
                    rowa[colgrfRptQueno] = row1["queue_seq"].ToString();
                    rowa[0] = i.ToString();
                    i++;
                }
            }
            catch(Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmReception setGrfRptReportDailyDept ");
            }
            finally
            {
                hideLbLoading();
            }
        }
        private void setGrfRptReportDailyDeptHi()
        {
            DateTime datestart = new DateTime();
            DateTime dateend = new DateTime();
            DataTable dt = new DataTable();
            String deptid = "";
            try
            {
                showLbLoading();
                deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(((ComboBoxItem)cboRptDept.SelectedItem).Value);
                DateTime.TryParse(txtRptDateStart.Text, out datestart);
                DateTime.TryParse(txtRptDateEnd.Text, out dateend);
                lfSbMessage.Text = datestart.ToString("yyyy-MM-dd") + " " + dateend.ToString("yyyy-MM-dd") + " " + ((ComboBoxItem)cboRptDept.SelectedItem).Value;
                dt = bc.bcDB.vsDB.selectPttHiinDept(deptid, ((ComboBoxItem)cboRptDept.SelectedItem).Value, datestart.ToString("yyyy-MM-dd"), dateend.ToString("yyyy-MM-dd"));
                int i = 1, j = 1;
                long chkxray = 0;
                grfRptData.Cols.Count = 12;
                grfRptData.Rows.Count = 1;
                grfRptData.Cols[colgrfRptDatadailydeptDate].Width = 110;
                grfRptData.Cols[colgrfRptDatadailydeptTime].Width = 50;
                grfRptData.Cols[colgrfRptDatadailydeptHn].Width = 90;
                grfRptData.Cols[colgrfRptDatadailydeptName].Width = 280;
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Width = 120;
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyAuthen].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptQueno].Width = 50;

                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyXray].Width = 50;

                grfRptData.Cols[colgrfRptDatadailydeptDate].Caption = "date";
                grfRptData.Cols[colgrfRptDatadailydeptTime].Caption = "time";
                grfRptData.Cols[colgrfRptDatadailydeptHn].Caption = "hn";
                grfRptData.Cols[colgrfRptDatadailydeptName].Caption = "name";
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Caption = "mobile";
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Caption = "set";
                grfRptData.Cols[colgrfRptDatadailyAuthen].Caption = "Foods";
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Caption = "KYC";
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Caption = "Foods1";
                grfRptData.Cols[colgrfRptDatadailyXray].Caption = "xray";

                grfRptData.Rows.Count = dt.Rows.Count + 1;
                Column colAuthen = grfRptData.Cols[colgrfRptDatadailyAuthen];
                colAuthen.DataType = typeof(Image);
                Column colPic = grfRptData.Cols[colgrfRptDatadailyPicKYC];
                colPic.DataType = typeof(Image);
                Column colPic1 = grfRptData.Cols[colgrfRptDatadailyXray];
                colPic1.DataType = typeof(Image);
                //pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row1 in dt.Rows)
                {
                    //pB1.Value++;
                    Row rowa = grfRptData.Rows[i];
                    rowa[colgrfRptDatadailydeptDate] = row1["MNC_date"].ToString();
                    rowa[colgrfRptDatadailydeptTime] = row1["MNC_time"].ToString();
                    rowa[colgrfRptDatadailydeptHn] = row1["mnc_hn_no"].ToString();
                    rowa[colgrfRptDatadailydeptName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                    rowa[colgrfRptDatadailydeptMobile] = row1["mnc_cur_tel"].ToString();
                    //rowa[colgrfSrcPttid] = "";
                    rowa[colgrfRptDatadailyAuthen] = row1["status_authen"] != null ? row1["status_authen"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;        //รูปถ่ายอาหาร รับอาหาร
                    rowa[colgrfRptDatadailyPicKYC] = row1["status_pic_kyc"] != null ? row1["status_pic_kyc"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;         //รูปถ่ายคนไข้ KYC
                    //long.TryParse(row1["req_no_xray"].ToString(), out chkxray);
                    rowa[colgrfRptDatadailyXray] = long.TryParse(row1["req_no_xray"].ToString(), out chkxray) ? imgCorr : imgTran;
                    rowa[colgrfRptDatadailyDrugSet] = row1["drug_set"].ToString();
                    rowa[colgrfRptQueno] = row1["queue_seq"].ToString();
                    rowa[0] = i.ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmReception setGrfRptReportDailyDept ");
            }
            finally
            {
                hideLbLoading();
            }
        }
        private void setGrfRptReportDailyDeptHiATK()
        {
            DateTime datestart = new DateTime();
            DateTime dateend = new DateTime();
            DataTable dt = new DataTable();
            String deptid = "";
            try
            {
                showLbLoading();
                deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(((ComboBoxItem)cboRptDept.SelectedItem).Value);
                DateTime.TryParse(txtRptDateStart.Text, out datestart);
                DateTime.TryParse(txtRptDateEnd.Text, out dateend);
                lfSbMessage.Text = datestart.ToString("yyyy-MM-dd") + " " + dateend.ToString("yyyy-MM-dd") + " " + ((ComboBoxItem)cboRptDept.SelectedItem).Value;
                dt = bc.bcDB.vsDB.selectPttHiinDeptPaidCode(deptid, ((ComboBoxItem)cboRptDept.SelectedItem).Value, datestart.ToString("yyyy-MM-dd"), dateend.ToString("yyyy-MM-dd"), txtPaidCode.Text.Trim());
                int i = 1, j = 1;
                long chkxray = 0;
                grfRptData.Cols.Count = 12;
                grfRptData.Rows.Count = 1;
                grfRptData.Cols[colgrfRptDatadailydeptDate].Width = 110;
                grfRptData.Cols[colgrfRptDatadailydeptTime].Width = 50;
                grfRptData.Cols[colgrfRptDatadailydeptHn].Width = 90;
                grfRptData.Cols[colgrfRptDatadailydeptName].Width = 280;
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Width = 120;
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyAuthen].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptQueno].Width = 50;


                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyXray].Width = 50;

                grfRptData.Cols[colgrfRptDatadailydeptDate].Caption = "date";
                grfRptData.Cols[colgrfRptDatadailydeptTime].Caption = "time";
                grfRptData.Cols[colgrfRptDatadailydeptHn].Caption = "hn";
                grfRptData.Cols[colgrfRptDatadailydeptName].Caption = "name";
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Caption = "mobile";
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Caption = "set";
                grfRptData.Cols[colgrfRptDatadailyAuthen].Caption = "Foods";
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Caption = "KYC";
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Caption = "Foods1";
                grfRptData.Cols[colgrfRptDatadailyXray].Caption = "xray";

                grfRptData.Rows.Count = dt.Rows.Count + 1;
                Column colAuthen = grfRptData.Cols[colgrfRptDatadailyAuthen];
                colAuthen.DataType = typeof(Image);
                Column colPic = grfRptData.Cols[colgrfRptDatadailyPicKYC];
                colPic.DataType = typeof(Image);
                Column colPic1 = grfRptData.Cols[colgrfRptDatadailyXray];
                colPic1.DataType = typeof(Image);
                //pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row1 in dt.Rows)
                {
                    //pB1.Value++;
                    Row rowa = grfRptData.Rows[i];
                    rowa[colgrfRptDatadailydeptDate] = row1["MNC_date"].ToString();
                    rowa[colgrfRptDatadailydeptTime] = row1["MNC_time"].ToString();
                    rowa[colgrfRptDatadailydeptHn] = row1["mnc_hn_no"].ToString();
                    rowa[colgrfRptDatadailydeptName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                    rowa[colgrfRptDatadailydeptMobile] = row1["mnc_cur_tel"].ToString();
                    //rowa[colgrfSrcPttid] = "";
                    //rowa[colgrfRptDatadailyAuthen] = row1["status_authen"] != null ? row1["status_authen"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;        //รูปถ่ายอาหาร รับอาหาร
                    //rowa[colgrfRptDatadailyPicKYC] = row1["status_pic_kyc"] != null ? row1["status_pic_kyc"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;         //รูปถ่ายคนไข้ KYC
                    //long.TryParse(row1["req_no_xray"].ToString(), out chkxray);
                    //rowa[colgrfRptDatadailyXray] = long.TryParse(row1["req_no_xray"].ToString(), out chkxray) ? imgCorr : imgTran;
                    //rowa[colgrfRptDatadailyDrugSet] = row1["drug_set"].ToString();
                    //rowa[colgrfRptQueno] = row1["queue_seq"].ToString();
                    rowa[0] = i.ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmReception setGrfRptReportDailyDept ");
            }
            finally
            {
                hideLbLoading();
            }
        }
        private void TxtPttCurRoad_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                cboPttCurTambon.Focus();
            }
        }

        private void TxtPttCurSoi_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttCurRoad.SelectAll();
                txtPttCurRoad.Focus();
            }
        }

        private void TxtPttCurMoo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttCurSoi.SelectAll();
                txtPttCurSoi.Focus();
            }
        }

        private void TxtPttCurHomeNo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttCurMoo.SelectAll();
                txtPttCurMoo.Focus();
            }
        }

        private void CboPttIDTambon_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtPttCurHomeNo.SelectAll();
            txtPttCurHomeNo.Focus();
        }

        private void TxtPttDOB_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttPrefixT.Focus();
        }

        private void TxtPttIDRoad_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                cboPttIDTambon.Focus();
            }
        }

        private void TxtPttIDSoi_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttIDRoad.SelectAll();
                txtPttIDRoad.Focus();
            }
        }

        private void TxtPttIDMoo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttIDSoi.SelectAll();
                txtPttIDSoi.Focus();
            }
        }

        private void TxtPttIDHomeNo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttIDMoo.SelectAll();
                txtPttIDMoo.Focus();
            }
        }

        private void TxtPttEmail_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttIDHomeNo.SelectAll();
                txtPttIDHomeNo.Focus();
            }
        }

        private void TxtPttMobile2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttEmail.SelectAll();
                txtPttEmail.Focus();
            }
        }

        private void TxtPttMobile1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttMobile2.SelectAll();
                txtPttMobile2.Focus();
            }
        }

        private void TxtPttPassport_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttMobile1.SelectAll();
                txtPttMobile1.Focus();
            }
        }

        private void TxtPttSsn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttPassport.SelectAll();
                txtPttPassport.Focus();
            }
        }

        private void TxtPttPID_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttSsn.SelectAll();
                txtPttSsn.Focus();
            }
        }

        private void TxtPttSurNameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttPID.SelectAll();
                txtPttPID.Focus();
            }
        }

        private void TxtPttNameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttSurNameE.SelectAll();
                txtPttSurNameE.Focus();
            }
        }

        private void TxtPttSurNameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttNameE.SelectAll();
                txtPttNameE.Focus();
            }
        }

        private void CboPttPrefixT_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtPttNameT.SelectAll();
            txtPttNameT.Focus();
        }

        private void TxtPttNameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttSurNameT.SelectAll();
                txtPttSurNameT.Focus();
            }
        }

        private void TxtPttCompCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {

            }
            else if (txtPttCompCode.Text.Trim().Length > 3)
            {

            }
        }
        private void CboPttRefTambon_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if (e.KeyCode == Keys.Enter)
            {
                cboPttRefAmphur.Focus();
            }
            else if (((C1ComboBox)sender).Text.Trim().Length > 3)
            {
                pageLoad = true;
                lfSbMessage.Text = ((C1ComboBox)sender).Text;
                bc.bcDB.pm07DB.setCboTumbonName(((C1ComboBox)sender), ((C1ComboBox)sender).Text);
                ((C1ComboBox)sender).CloseDropDown();
                ((C1ComboBox)sender).OpenDropDown();
                pageLoad = false;
            }
        }

        private void CboPttRefTambon_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;

            if (((C1ComboBox)sender).Text.Trim().Length > 2)
            {
                String code = "", name = "";
                code = ((ComboBoxItem)((C1ComboBox)sender).SelectedItem).Value;
                name = ((ComboBoxItem)((C1ComboBox)sender).SelectedItem).Text;
                lfSbMessage.Text = name;
                if (code.Length >= 6)
                {
                    code = code.Substring(0, 4) + "00";
                    bc.bcDB.pm08DB.setCboAmphurByAmphurCode(cboPttRefAmphur, code, code);
                    if (cboPttRefAmphur.Items.Count == 1)
                    {
                        cboPttRefAmphur.SelectedIndex = 1;
                    }
                    code = code.Substring(0, 2) + "0000";
                    bc.bcDB.pm09DB.setCboProvByProvCode(cboPttRefProv, code, code);
                    if (cboPttRefProv.Items.Count == 1)
                    {
                        cboPttRefProv.SelectedIndex = 1;
                    }
                    //txtPttCurHomeNo.SelectAll();
                    //txtPttCurHomeNo.Focus();
                    cboPttRefTambon.CloseDropDown();
                }
            }
        }
        private void CboPttCurTambon_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if (e.KeyCode == Keys.Enter)
            {
                cboPttCurAmphur.Focus();
            }
            else if (((C1ComboBox)sender).Text.Trim().Length > 3)
            {
                pageLoad = true;
                lfSbMessage.Text = ((C1ComboBox)sender).Text;
                bc.bcDB.pm07DB.setCboTumbonName(((C1ComboBox)sender), ((C1ComboBox)sender).Text);
                ((C1ComboBox)sender).CloseDropDown();
                ((C1ComboBox)sender).OpenDropDown();
                pageLoad = false;
            }
        }

        private void CboPttCurTambon_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;

            if (cboPttCurTambon.Text.Trim().Length > 2)
            {
                String code = "", name = "";
                code = ((ComboBoxItem)((C1ComboBox)sender).SelectedItem).Value;
                name = ((ComboBoxItem)((C1ComboBox)sender).SelectedItem).Text;
                lfSbMessage.Text = name;
                if (code.Length >= 6)
                {
                    code = code.Substring(0, 4) + "00";
                    bc.bcDB.pm08DB.setCboAmphurByAmphurCode(cboPttCurAmphur, code, code);
                    if (cboPttCurAmphur.Items.Count == 1)
                    {
                        cboPttCurAmphur.SelectedIndex = 1;
                    }
                    code = code.Substring(0, 2) + "0000";
                    bc.bcDB.pm09DB.setCboProvByProvCode(cboPttCurProv, code, code);
                    if (cboPttCurProv.Items.Count == 1)
                    {
                        cboPttCurProv.SelectedIndex = 1;
                    }
                    //txtPttCurHomeNo.SelectAll();
                    //txtPttCurHomeNo.Focus();
                    cboPttCurTambon.CloseDropDown();
                }
            }
        }
        private void CboPttIDTambon_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if(e.KeyCode == Keys.Enter)
            {
                txtPttCurHomeNo.SelectAll();
                txtPttCurHomeNo.Focus();
            }
            else if (cboPttIDTambon.Text.Trim().Length > 3)
            {
                pageLoad = true;
                lfSbMessage.Text = cboPttIDTambon.Text;
                bc.bcDB.pm07DB.setCboTumbonName(cboPttIDTambon, cboPttIDTambon.Text);
                cboPttIDTambon.CloseDropDown();
                cboPttIDTambon.OpenDropDown();
                pageLoad = false;
            }
        }

        private void CboPttIDTambon_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;

            if (cboPttIDTambon.Text.Trim().Length > 2)
            {
                String code = "", name = "";
                code = ((ComboBoxItem)cboPttIDTambon.SelectedItem).Value;
                name = ((ComboBoxItem)cboPttIDTambon.SelectedItem).Text;
                lfSbMessage.Text = name;
                if (code.Length >= 6)
                {
                    code = code.Substring(0, 4)+"00";
                    bc.bcDB.pm08DB.setCboAmphurByAmphurCode(cboPttIDAmphur, code, code);
                    if (cboPttIDAmphur.Items.Count == 1)
                    {
                        cboPttIDAmphur.SelectedIndex = 1;
                    }
                    code = code.Substring(0, 2) + "0000";
                    bc.bcDB.pm09DB.setCboProvByProvCode(cboPttIDProv, code, code);
                    if (cboPttIDProv.Items.Count == 1)
                    {
                        cboPttIDProv.SelectedIndex = 1;
                    }
                    //txtPttCurHomeNo.SelectAll();
                    //txtPttCurHomeNo.Focus();
                    cboPttIDTambon.CloseDropDown();
                }
            }
        }

        private void TxtSrcHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {

            }
            else if (txtSrcHn.Text.Length>3)
            {
                showLbLoading();
                setGrfSrc();
                hideLbLoading();
            }
            else
            {

            }
        }

        private void BtnSrcNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setCboTheme()
        {
            cboTheme.Items.Clear();
            String[] themes = C1ThemeController.GetThemes();
            foreach (String txt in themes)
            {
                cboTheme.Items.Add(txt);
            }
            cboTheme.SelectedText = "Office2010Red";
            
        }
        private void setTheme(String theme)
        {
            theme1.SetTheme(this,theme);//pnSrcTop
            //theme1.SetTheme(groupBox1, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox2, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox3, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox5, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox6, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox4, bc.iniC.themeApp);
            //theme1.SetTheme(pnPttComp, bc.iniC.themeApp);
            //theme1.SetTheme(groupBox6, bc.iniC.themeApp);c1StatusBar1
            theme1.SetTheme(c1SplitContainer2, theme);
            theme1.SetTheme(c1StatusBar1, bc.iniC.themeApp);
            //foreach (Control c in this.Controls)
            //{
            //    if(c is C1TextBox)
            //    {
            //        theme1.SetTheme(c, bc.iniC.themeApp);
            //    }
            //}
        }
        private void setControlTabPtt()
        {
            

        }
        private void clearControl()
        {
            txtPttHn.Value = "";
            txtPttDOB.Value = "";
            cboPttPrefixT.Text = "";
            txtPttNameT.Value = "";
            txtPttSurNameT.Value = "";
            txtPttAge.Value = "";
            cboPttPrefixE.Text = "";
            txtPttNameE.Value = "";
            txtPttSurNameE.Value = "";
            txtPttPID.Value = "";
            txtPttSsn.Value = "";
            txtPttPassport.Value = "";
            txtPttMobile1.Value = "";
            txtPttMobile2.Value = "";
            txtPttEmail.Value = "";
            txtPttIDHomeNo.Value = "";
            txtPttIDMoo.Value = "";
            txtPttIDSoi.Value = "";
            txtPttIDRoad.Value = "";
            cboPttIDTambon.Text = "";
            cboPttIDAmphur.Text = "";
            cboPttIDProv.Text = "";
            txtPttIDPostcode.Value = "";
            txtPttCurHomeNo.Value = "";
            txtPttCurMoo.Value = "";
            txtPttCurSoi.Value = "";
            txtPttCurRoad.Value = "";
            cboPttCurTambon.Text = "";
            cboPttCurAmphur.Text = "";
            cboPttCurProv.Text = "";
            txtPttCurPostcode.Value = "";
            txtPttRefHomeNo.Value = "";
            txtPttRefMoo.Value = "";
            txtPttRefSoi.Value = "";
            txtPttRefRoad.Value = "";
            cboPttRefTambon.Text = "";
            cboPttRefAmphur.Text = "";
            cboPttRefProv.Text = "";
            txtPttRefPostcode.Value = "";
            txtPttRefContact1Name.Value = "";
            txtPttRefContact2Name.Value = "";
            txtPttRefContact1Mobile.Value = "";
            txtPttRefContact2Mobile.Value = "";
            cboPttRefContact1Relate.Text = "";
            cboPttRefContact2Relate.Text = "";
            txtPttCompCode.Value = "";
            lbPttCompNameT.Text = "";
            cboPttNat.Text = "";
            cboPttRace.Text = "";
            cboPttEdu.Text = "";
            cboPttMarri.Text = "";
            txtPttAttchNote.Value = "";
            txtPttRemark1.Value = "";
            txtPttRemark2.Value = "";
            grfPttComp.Rows.Count = 1;
        }
        private void initGrfPttApm()
        {
            grfPttApm = new C1FlexGrid();
            grfPttApm.Font = fEdit;
            grfPttApm.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPttApm.Location = new System.Drawing.Point(0, 0);
            grfPttApm.Rows.Count = 1;
            grfPttApm.Cols.Count = 5;
            
            grfPttApm.Cols[colgrfPttApmVsDate].Width = 100;
            grfPttApm.Cols[colgrfPttApmApmDate].Width = 300;
            grfPttApm.Cols[colgrfPttApmDept].Width = 150;
            grfPttApm.Cols[colgrfPttApmNote].Width = 150;
            grfPttApm.ShowCursor = true;
            grfPttApm.Cols[colgrfPttApmVsDate].Caption = "code";
            grfPttApm.Cols[colgrfPttApmApmDate].Caption = "name";
            grfPttApm.Cols[colgrfPttApmDept].Caption = "id";
            grfPttApm.Cols[colgrfPttApmNote].Caption = "id";

            grfPttApm.Cols[colgrfPttApmVsDate].Visible = true;
            grfPttApm.Cols[colgrfPttApmApmDate].Visible = true;
            grfPttApm.Cols[colgrfPttApmDept].Visible = true;
            grfPttApm.Cols[colgrfPttApmNote].Visible = true;

            grfPttApm.Cols[colgrfPttApmVsDate].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmApmDate].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmDept].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmNote].AllowEditing = false;
            //grfPttComp.AllowFiltering = true;            
            //grfSrc.AutoSizeCols();
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnPttApm.Controls.Add(grfPttApm);
            theme1.SetTheme(grfPttApm, bc.iniC.themeApp);
        }
        private void initGrfPttVs()
        {
            grfPttVs = new C1FlexGrid();
            grfPttVs.Font = fEdit;
            grfPttVs.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPttVs.Location = new System.Drawing.Point(0, 0);
            grfPttVs.Rows.Count = 1;
            grfPttVs.Cols.Count = 8;
            
            grfPttVs.Cols[colgrfPttVsVsDate].Width = 100;
            grfPttVs.Cols[colgrfPttVsHn].Width = 300;
            grfPttVs.Cols[colgrfPttVsPreno].Width = 150;
            grfPttVs.Cols[colgrfPttVsDept].Width = 150;
            grfPttVs.Cols[colgrfPttVsStatusOPD].Width = 150;
            grfPttVs.Cols[colgrfPttVsSymptom].Width = 150;
            grfPttVs.ShowCursor = true;
            grfPttVs.Cols[colgrfPttVsVsDate].Caption = "code";
            grfPttVs.Cols[colgrfPttVsHn].Caption = "name";
            grfPttVs.Cols[colgrfPttVsPreno].Caption = "id";
            grfPttVs.Cols[colgrfPttVsDept].Caption = "id";
            grfPttVs.Cols[colgrfPttVsStatusOPD].Caption = "id";
            grfPttVs.Cols[colgrfPttVsSymptom].Caption = "id";

            grfPttVs.Cols[colgrfPttVsVsDate].Visible = true;
            grfPttVs.Cols[colgrfPttVsHn].Visible = true;
            grfPttVs.Cols[colgrfPttVsPreno].Visible = false;
            grfPttVs.Cols[colgrfPttVsDept].Visible = false;
            grfPttVs.Cols[colgrfPttVsStatusOPD].Visible = false;
            grfPttVs.Cols[colgrfPttVsSymptom].Visible = false;

            grfPttVs.Cols[colgrfPttVsVsDate].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsHn].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsPreno].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsDept].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsStatusOPD].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsSymptom].AllowEditing = false;
            //grfPttComp.AllowFiltering = true;            
            //grfSrc.AutoSizeCols();
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnPttVs.Controls.Add(grfPttVs);
            theme1.SetTheme(grfPttVs, bc.iniC.themeApp);
        }
        private void initGrfPttComp()
        {
            grfPttComp = new C1FlexGrid();
            grfPttComp.Font = fEdit;
            grfPttComp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPttComp.Location = new System.Drawing.Point(0, 0);
            grfPttComp.Rows.Count = 1;
            grfPttComp.Cols.Count = 4;
            grfPttComp.DoubleClick += GrfPttComp_DoubleClick;
            grfPttComp.Cols[celgrfPttCompCode].Width = 100;
            grfPttComp.Cols[colgrfPttCompNameT].Width = 300;
            grfPttComp.Cols[colgrfPttCompid].Width = 150;
            grfPttComp.ShowCursor = true;
            grfPttComp.Cols[celgrfPttCompCode].Caption = "code";
            grfPttComp.Cols[colgrfPttCompNameT].Caption = "name";
            grfPttComp.Cols[colgrfPttCompid].Caption = "id";

            grfPttComp.Cols[celgrfPttCompCode].Visible = true;
            grfPttComp.Cols[colgrfPttCompNameT].Visible = true;
            grfPttComp.Cols[colgrfPttCompid].Visible = false;

            grfPttComp.Cols[celgrfPttCompCode].AllowEditing = false;
            grfPttComp.Cols[colgrfPttCompNameT].AllowEditing = false;
            //grfPttComp.AllowFiltering = true;            
            //grfSrc.AutoSizeCols();
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnPttComp.Controls.Add(grfPttComp);
            theme1.SetTheme(grfPttComp, bc.iniC.themeApp);
        }

        private void GrfPttComp_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfRptData()
        {
            grfRptData = new C1FlexGrid();
            grfRptData.Font = fEdit;
            grfRptData.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRptData.Location = new System.Drawing.Point(0, 0);
            grfRptData.Rows.Count = 1;
            pnRptData.Controls.Add(grfRptData);
            theme1.SetTheme(grfRptData, bc.iniC.themeApp);
        }
        private void initGrfRptReport()
        {
            grfRptReport = new C1FlexGrid();
            grfRptReport.Font = fEdit;
            grfRptReport.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRptReport.Location = new System.Drawing.Point(0, 0);
            grfRptReport.Rows.Count = 4;
            grfRptReport.Cols.Count = 3;
            grfRptReport.Cols[colgrfRptReportCode].Width = 100;
            grfRptReport.Cols[colgrfRptReportName].Width = 400;
            //grfRptReport.Cols[colgrfPttCompid].Width = 150;
            grfRptReport.ShowCursor = true;
            grfRptReport.Cols[colgrfRptReportCode].Caption = "code";
            grfRptReport.Cols[colgrfRptReportName].Caption = "name";
            //grfRptReport.Cols[colgrfPttCompid].Caption = "id";

            grfRptReport.Cols[colgrfRptReportCode].Visible = false;
            grfRptReport.Cols[colgrfRptReportName].Visible = true;
            //grfRptReport.Cols[colgrfPttCompid].Visible = false;

            grfRptReport.Cols[colgrfRptReportCode].AllowEditing = false;
            grfRptReport.Cols[colgrfRptReportName].AllowEditing = false;
            grfRptReport.AllowSorting = AllowSortingEnum.None;

            grfRptReport.Rows[1][colgrfRptReportCode] = "reportdailydept";
            grfRptReport.Rows[1][colgrfPttCompNameT] = "รายงานยอดคนไข้ ตามแผนก";

            grfRptReport.Rows[2][colgrfRptReportCode] = "reportdailydeptHI";
            grfRptReport.Rows[2][colgrfPttCompNameT] = "รายงานยอดคนไข้ ตามแผนก Home Isolate";

            grfRptReport.Rows[3][colgrfRptReportCode] = "reportdailydeptHIATK";
            grfRptReport.Rows[3][colgrfPttCompNameT] = "รายงานยอดคนไข้ ตามแผนก Home Isolate ATK Screening";

            grfRptReport.Click += GrfRptReport_Click;

            pnRptReport.Controls.Add(grfRptReport);
            theme1.SetTheme(grfRptReport, bc.iniC.themeApp);
        }

        private void GrfRptReport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfRptReport.Row <= 0) return;
            if (grfRptReport.Col <= 0) return;
            rptCode = grfRptReport[grfRptReport.Row, colgrfRptReportCode]!= null ? grfRptReport[grfRptReport.Row,colgrfRptReportCode].ToString():"";
            lfSbMessage.Text = "report "+rptCode ;
        }

        private void initGrfSrc()
        {
            grfSrc = new C1FlexGrid();
            grfSrc.Font = fEdit;
            grfSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrc.Location = new System.Drawing.Point(0, 0);
            grfSrc.Rows.Count = 1;
            grfSrc.Cols.Count = 6;
            grfSrc.Cols[colgrfSrcHn].Width = 100;
            grfSrc.Cols[colgrfSrcFullNameT].Width = 300;
            grfSrc.Cols[colgrfSrcPID].Width = 150;
            grfSrc.Cols[colgrfSrcDOB].Width = 100;
            grfSrc.Cols[colgrfSrcPttid].Width = 60;
            grfSrc.ShowCursor = true;
            grfSrc.Cols[colgrfSrcHn].Caption = "hn";
            grfSrc.Cols[colgrfSrcFullNameT].Caption = "full name";
            grfSrc.Cols[colgrfSrcPID].Caption = "PID";
            grfSrc.Cols[colgrfSrcDOB].Caption = "DOB";
            grfSrc.Cols[colgrfSrcPttid].Caption = "";

            grfSrc.Cols[colgrfSrcHn].Visible = true;
            grfSrc.Cols[colgrfSrcFullNameT].Visible = true;
            grfSrc.Cols[colgrfSrcPID].Visible = true;
            grfSrc.Cols[colgrfSrcDOB].Visible = true;
            grfSrc.Cols[colgrfSrcPttid].Visible = false;

            grfSrc.Cols[colgrfSrcHn].AllowEditing = false;
            grfSrc.Cols[colgrfSrcFullNameT].AllowEditing = false;
            grfSrc.Cols[colgrfSrcPID].AllowEditing = false;
            grfSrc.Cols[colgrfSrcDOB].AllowEditing = false;
            grfSrc.AllowFiltering = true;

            grfSrc.DoubleClick += GrfSrc_DoubleClick;
            //grfSrc.AutoSizeCols();
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnSrcGrf.Controls.Add(grfSrc);
            theme1.SetTheme(grfSrc, bc.iniC.themeApp);
        }

        private void GrfSrc_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSrc.Row <= 0) return;
            if (grfSrc.Col <= 0) return;

            String hn = "";
            hn = grfSrc[grfSrc.Row, colgrfSrcHn] != null ? grfSrc[grfSrc.Row, colgrfSrcHn].ToString():"";
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
            if (ptt.Hn.Length > 5)
            {
                setControlTabPateint(ptt);

                tC.SelectedTab = tabPtt;
            }
            else
            {
                lfSbStatus.Text = "";
                lfSbMessage.Text = "";
            }
        }
        private void setControlTabPateint(Patient ptt)
        {
            showLbLoading();
            DateTime dob = new DateTime();
            DateTime.TryParse(ptt.MNC_BDAY, out dob);
            dob = dob.AddYears(543);
            txtPttHn.Value = ptt.Hn;
            txtPttDOB.Value = dob;
            txtPttNameT.Value = ptt.MNC_FNAME_T;
            txtPttSurNameT.Value = ptt.MNC_LNAME_T;
            txtPttNameE.Value = ptt.MNC_FNAME_E;
            txtPttSurNameE.Value = ptt.MNC_LNAME_E;

            txtPttPID.Value = ptt.MNC_ID_NO;
            txtPttSsn.Value = ptt.MNC_SS_NO;
            txtPttPassport.Value = ptt.passport;
            txtPttMobile1.Value = ptt.MNC_CUR_TEL;
            txtPttEmail.Value = ptt.MNC_DOM_TEL;
            txtPttAge.Value = ptt.AgeStringOK1();
            
            txtDocHn.Value = ptt.MNC_HN_NO;
            lbDocFullname.Text = ptt.Name;

            setGrfPttVs();
            hideLbLoading();
        }
        private void setGrfPttVs()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.vsDB.selectVisitByHn(txtPttHn.Text.Trim());
            grfPttVs.Rows.Count = 1;
            grfPttVs.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfPttVs.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfPttVs.Rows[i];
                rowa[colgrfPttVsVsDate] = row1["mnc_date"].ToString();
                rowa[colgrfPttVsHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfPttVsPaid] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfPttVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfPttVsDept] = "";
                rowa[colgrfPttVsStatusOPD] = row1["mnc_sts_flg"].ToString();
                //rowa[colgrfPttVsSymptom] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void setGrfSrc()
        {
            showLbLoading();
            DataTable dt = new DataTable();
            dt = bc.bcDB.pttDB.selectPatinetBySearch(txtSrcHn.Text.Trim());
            
            int i = 1, j = 1, row = grfSrc.Rows.Count;
            
            grfSrc.Rows.Count = 1;
            grfSrc.Rows.Count = dt.Rows.Count+1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSrc.Rows[i];
                rowa[colgrfSrcHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfSrcFullNameT] = row1["pttfullname"].ToString();
                rowa[colgrfSrcPID] = row1["mnc_id_no"].ToString();
                rowa[colgrfSrcDOB] = row1["MNC_bday"].ToString();
                rowa[colgrfSrcPttid] = "";
                rowa[0] = i.ToString();
                i++;
            }
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            //menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            //grfVs.ContextMenu = menuGw;
            hideLbLoading();
        }
        private void showLbLoading()
        {
            lbLoading.Show();
            lbLoading.BringToFront();
            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();
            Application.DoEvents();
        }
        private void FrmReception_Load(object sender, EventArgs e)
        {
            lfSbLastUpdate.Text = "Update 2565-03-12";
            tC.SelectedTab = tabSrc;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            CboTheme_SelectedIndexChanged(null, null);
            //groupBox5.Top = this.Height - groupBox5.Height;
        }
    }
}
