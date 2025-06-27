using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.C1Zip;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmSSO : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfSupra, grfRpt, grfSSOPimp, grfItems, grfSSOP, grfBillI, grfDisp, grfDispI, grfOPDx, grfOpServ, grfMakeTextSearch, grfMakeText;
        Patient PTT;
        C1ThemeController theme1;
        int colgrfSupraVsDate = 1, colgrfSupraPreno = 2, colgrfSupraSymptoms = 3, colgrfSupraVNAN=4, colgrfSupraStatusAdmit=5;
        int colgrfSSOPimpchk = 1, colgrfSSOPimpVsDate = 2, colgrfSSOPimpPreno = 3, colgrfSSOPimpHn = 4, colgrfSSOPimpName = 5, colgrfSSOPimpSymptoms = 6, colgrfSSOPimpPaidName = 7, colgrfSSOPimppid = 8, colgrfSSOPimpVsTime = 9;
        int colgrfSSOPhn=1, colgrfSSOPname=2, colgrfSSOPvsdate=3, colgrfSSOPpreno = 4, colgrfSSOPid = 5, colgrfSSOPSymptoms=6;
        int colgrfBillIid=1, colgrfBillIsvdate = 2, colgrfBillIlccode = 3, colgrfBillIdesc = 4;
        int colgrfDispid = 1, colgrfDispprescdt = 2, colgrfDispprescb = 3, colgrfDispitemcnt = 4;
        int colgrfDispIid = 1, colgrfDispIdrgid = 2, colgrfDispIdfscode = 3, colgrfDispIdfstext = 4;
        int colgrfOPServid = 1, colgrfOPServsvid = 2, colgrfOPServcodeset = 3, colgrfOPServtypein = 4;
        int colgrfOPDxid = 1, colgrfOPDxsl = 2, colgrfOPDxcodeset = 3, colgrfOPDxcode = 4;
        int colgrfMakeTextSearchchk = 1,colgrfMakeTextSearchid=2, colgrfMakeTextSearchname = 3, colgrfMakeTextSearchhn = 4, colgrfMakeTextSearchvsdate = 5;
        
        Label lbLoading;
        Boolean pageLoad = false;
        public FrmSSO(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            theme1 = new C1ThemeController();

            initFont();
            initLoading();
            initCombobox();
            initGrf();

            PTT = new Patient();
            initEvent();
        }
        private void initEvent()
        {
            txtSupraNewHn.KeyUp += TxtSupraNewHn_KeyUp;
            btnSuprNewSave.Click += BtnSuprNewSave_Click;
            btnSSOPSearch.Click += BtnSSOPSearch_Click;
            btnSSOPimport.Click += BtnSSOPimport_Click;
            tcSSOP.SelectedTabChanged += TcSSOP_SelectedTabChanged;
            btnSSOPSave.Click += BtnSSOPSave_Click;
            btnSSOPProcBillItemsGet.Click += BtnSSOPProcBillItemsGet_Click;
            btnSSOPProcBillItemsSave.Click += BtnSSOPProcBillItemsSave_Click;
            btnSSOPProcDispensingSave.Click += BtnSSOPProcDispensingSave_Click;
            btnSSOPProcDispenseditemSave.Click += BtnSSOPProcDispenseditemSave_Click;
            btnSSOPProcOPServicesSave.Click += BtnSSOPProcOPServicesSave_Click;
            btnSSOPProcOPDxSave.Click += BtnSSOPProcOPDxSave_Click;
            txtSSOPgentextSearch.KeyUp += TxtSSOPgentextSearch_KeyUp;
            btnSSOPgentextgentext.Click += BtnSSOPgentextgentext_Click;
            btnSSOPGen.Click += BtnSSOPGen_Click;
            btnSSOPVoid.Click += BtnSSOPVoid_Click;
            btnSSOPProcBillItemsNew.Click += BtnSSOPProcBillItemsNew_Click;
            btnSSOPProcDispensingNew.Click += BtnSSOPProcDispensingNew_Click;
            btnSSOPProcDispenseditemNew.Click += BtnSSOPProcDispenseditemNew_Click;
            btnSSOPProcOPServicesNew.Click += BtnSSOPProcOPServicesNew_Click;
            btnSSOPProcOPDxNew.Click += BtnSSOPProcOPDxNew_Click;
            btnSSOPItemVoid.Click += BtnSSOPItemVoid_Click;
            btnSSOPDispVoid.Click += BtnSSOPDispVoid_Click;
            btnSSOPDispItemVoid.Click += BtnSSOPDispItemVoid_Click;
            btnSSOPOPserVoid.Click += BtnSSOPOPserVoid_Click;
            btnSSOPOPDxVoid.Click += BtnSSOPOPDxVoid_Click;
            txtSSOPgrfsearch.KeyUp += TxtSSOPgrfsearch_KeyUp;
        }

        private void initGrf()
        {
            //throw new NotImplementedException();
            initGrfSupra();             initGrfSSOP();              initGrfSSOPimp();               initGrfItems();
            initGrfBillItems();         initGrfDisp();              initGrfDispItems();             initGrfOpServ();
            initGrfOPDx();              initGrfMakeTextSearch();    initGrfMakeText();
            
        }
        private void initCombobox()
        {
            bc.setCboSSOPCompletion(cboSSOPOpServicescompletion, "");
            bc.setCboSSOPTFlag(cboSSOPProcTFlag, "");
            bc.setCboSSOPPayPlan(cboSSOPProcPayPlan, "");
            bc.setCboSSOPOtherPayPlan(cboSSOPProcOPayPlan, "");
            bc.setCboSSOPBillmuad(cboSSOPBillItemsbillmuad, "");
            bc.setCboSSOPClaimCat(cboSSOPBillItemsClaimcat, "");
            bc.setCboSSOPBenefitplan(cboSSOPDispBenefitplan, "");
            bc.setCboSSOPDispeStat(cboSSOPDispensingdispe, "");
            bc.setCboSSOPPrdCat(cboSSOPDispenseditemprdcat, "");
            bc.setCboSSOPClaimcont(cboSSOPDispenseditemclaimcont, "");
            bc.setCboSSOPClaimCat(cboSSOPDispenseditemclaimcat, "");
            bc.setCboSSOPPrdSeCode(cboSSOPDispenseditemprdsecode, "");
            bc.setCboSSOPClass(cboSSOPOPServicesclass, "");
            bc.setCboSSOPCareAccount(cboSSOPOpServicescareaccount, "");
            bc.setCboSSOPTypeIn(cboSSOPOpServicestypein, "");
            bc.setCboSSOPTypeOut(cboSSOPOpServicestypeout, "");
            bc.setCboSSOPCodeSet(cboSSOPOPServicescodeset, "");
            bc.setCboSSOPCompletion(cboSSOPOpServicescompletion, "");
            bc.setCboSSOPTypeServ(cboSSOPOpServicestypeserv, "");
            bc.setCboSSOPClinic(cboSSOPOPServicesclinic, "");
            bc.setCboSSOPClaimCat(cboSSOPOpServicesclaimcat, "");
            bc.setCboSSOPClass(cboSSOPOPDxclass, "");
            bc.setCboSSOPOPDxCodeSet(cboSSOPOPDxcodeset, "");
            bc.setCboSSOPSL(cboSSOPOPDxsl, "");
            bc.bcDB.pttDB.setCboDeptOPD(cboSSOPProcStation, bc.iniC.station);
        }
        private void TxtSSOPgrfsearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(txtSSOPgrfsearch.Text.Length> 2)
            {
                grfSSOPimp.ApplySearch(txtSSOPgrfsearch.Text.Trim(), true, true, false);
            }
        }
        private void BtnSSOPOPDxVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPODDB.voidOPDx(txtSSOPOPDxid.Text.Trim());
        }

        private void BtnSSOPOPserVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPOSDB.voidOPService(txtSSOPOpServicesid.Text.Trim());
        }

        private void BtnSSOPDispItemVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPDIDB.voidDispensedItem(txtSSOPDispenseditemid.Text.Trim());
        }

        private void BtnSSOPDispVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPDDB.voidDispensing(txtSSOPDispID.Text.Trim());
        }

        private void BtnSSOPItemVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.ssopBillItemsDB.voidBillItem(txtSSOPBillItemsID.Text.Trim());
        }

        private void BtnSSOPProcOPDxNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlOPDx();
        }

        private void BtnSSOPProcOPServicesNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlOpServ();
        }

        private void BtnSSOPProcDispenseditemNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlDispI();
        }

        private void BtnSSOPProcDispensingNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlDispsing();
        }

        private void BtnSSOPProcBillItemsNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlBillItems();
        }

        private void BtnSSOPVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPBillTranDB.updateStatusVoid(txtSSOPBilltransID.Text.Trim());
            setGrfSSOP();
        }

        private void BtnSSOPGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPBillTranDB.updateStatusProcessMakeText(txtSSOPBilltransID.Text.Trim());
            foreach(Row rowa in grfSSOP.Rows)
            {
                if (rowa[colgrfSSOPid] != null && rowa[colgrfSSOPid].ToString().Equals(txtSSOPBilltransID.Text.Trim()))
                {
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
                }
            }
        }

        private void BtnSSOPgentextgentext_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genSSOPXML();
        }
        private String genSSOPXML()
        {
            List<SSOPBillTran> lbillt = new List<SSOPBillTran>();
            List<SSOPBillItems> lbillitems = new List<SSOPBillItems>();
            List<SSOPDispensing> ldisp = new List<SSOPDispensing>();
            List<SSOPDispenseditem> ldispitems = new List<SSOPDispenseditem>();
            List<SSOPOpservices> lopserv = new List<SSOPOpservices>();
            List<SSOPOpdx> lopdx = new List<SSOPOpdx>();
            String hcode =bc.iniC.ssoid,Hmain = "", sessionNo = "999999", pathFile = "";
            if (grfMakeText.Rows.Count > 0)
            {
                foreach (Row rowa in grfMakeText.Rows)
                {
                    if (rowa[colgrfMakeTextSearchchk] != null && rowa[colgrfMakeTextSearchchk].ToString().Equals("True"))
                    {
                        String id = rowa[colgrfMakeTextSearchid] != null ? rowa[colgrfMakeTextSearchid].ToString() : "";
                        SSOPBillTran billt = bc.bcDB.sSOPBillTranDB.selectByPk(id);
                        if (billt != null) { lbillt.Add(billt); }
                        List<SSOPBillItems> litems = bc.bcDB.ssopBillItemsDB.selectByBTransId(id);
                        if(litems != null && litems.Count > 0)
                        {
                            foreach (SSOPBillItems item in litems)                              {                                lbillitems.Add(item);              }
                        }
                        List<SSOPDispensing> ldisp1 = bc.bcDB.sSOPDDB.selectByBTransId(id);
                        if (ldisp1 != null && ldisp1.Count > 0)
                        {
                            foreach (SSOPDispensing item in ldisp1)                             {                                ldisp.Add(item);                   }
                        }
                        List<SSOPDispenseditem> ldispitems1 = bc.bcDB.sSOPDIDB.selectByBTransId(id);
                        if (ldispitems1 != null && ldispitems1.Count > 0)
                        {
                            foreach (SSOPDispenseditem item in ldispitems1)                     {                                ldispitems.Add(item);              }
                        }
                        List<SSOPOpservices> lopserv1 = bc.bcDB.sSOPOSDB.selectByBTransId(id);
                        if (lopserv1 != null && lopserv1.Count > 0)
                        {
                            foreach (SSOPOpservices item in lopserv1)                           {                                lopserv.Add(item);                 }
                        }
                        List<SSOPOpdx> lopdx1 = bc.bcDB.sSOPODDB.selectByBTransId(id);
                        if (lopdx1 != null && lopdx1.Count > 0)
                        {
                            foreach (SSOPOpdx item in lopdx1)                                   {                                lopdx.Add(item);                   }
                        }
                    }
                }
            }
            if (lbillt.Count <= 0)            {                MessageBox.Show("ไม่พบข้อมูล ", "");                return "";            }
            Hmain = bc.iniC.ssoid;
            sessionNo = "0000"+bc.bcDB.sSOPBillTranDB.selectMaxSessionID();
            sessionNo = sessionNo.Substring(sessionNo.Length - 4);
            pathFile = bc.iniC.ssopXmlPath + "\\" + sessionNo;
            txtSSOPgentextSessionid.Value = sessionNo;
            txtSSOPgentextHname.Value = bc.iniC.hostname;
            txtSSOPgentextHCODE.Value = hcode;
            if (!Directory.Exists(pathFile))            {                Directory.CreateDirectory(pathFile);            }
            String date1 = DateTime.Now.Year.ToString()+ DateTime.Now.ToString("MMdd");
            String senddate = DateTime.Now.Year.ToString() + DateTime.Now.ToString("-MM-dd")+"T" + DateTime.Now.ToString("HH:mm:ss");
            String filenamebillt = pathFile + "\\" + "BILLTRAN" + date1 + ".txt";
            String filenamedisp = pathFile + "\\" + "BILLDISP" + date1 + ".txt";
            String filenameopserv = pathFile + "\\" + "OPServices" + date1 + ".txt";
            SSOPxmlFile ssopxmlFile = new SSOPxmlFile();
            ssopxmlFile.genBillTrans(hcode, bc.iniC.hostname, senddate, sessionNo, lbillt, lbillitems, filenamebillt);
            ssopxmlFile.genDispensing(hcode, bc.iniC.hostname, senddate, sessionNo, ldisp, ldispitems, filenamedisp);
            ssopxmlFile.genOpService(hcode, bc.iniC.hostname, senddate, sessionNo, lopserv, lopdx, filenameopserv);
            foreach(SSOPBillTran billtran in lbillt)
            {
                String re = bc.bcDB.sSOPBillTranDB.updateStatusProcessMakeTextSend(billtran.billtran_id);
            }
            try
            {
                String fileZipName = "";
                C1ZipFile zip = new C1ZipFile(); //iniC.ssoid
                //fileZipName = Hmain+"AIPN"+ sessionNo;
                senddate = senddate.Replace("-", "").Replace(":", "");
                senddate = senddate.Replace("T", "-");
                fileZipName = bc.iniC.ssoid + "_SSOPBIL_" + sessionNo+"_01_"+ senddate;
                zip.Create(pathFile + "\\" + fileZipName + ".zip");
                foreach (String filename in Directory.GetFiles(pathFile))
                {
                    if (filename.IndexOf("-data") > 0)                  {                        continue;                    }
                    if (filename.IndexOf("-utf8") > 0)                  {                        continue;                    }
                    if (filename.IndexOf(".zip") > 0)                   {                        continue;                    }
                    if (filename.IndexOf("_temp.txt") > 0) { continue; }
                    zip.Entries.Add(filename);
                }
                zip.Close();
                Process.Start("explorer.exe", pathFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
            return sessionNo;
        }
        private void TxtSSOPgentextSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //setGrfSSOP(txtSSOPgentextSearch.Text.Trim());
                setGrfMakeTextSearch(txtSSOPgentextSearch.Text.Trim());
            }
        }

        private void BtnSSOPProcOPDxSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPOpdx sSOPOpdx = setSSOPOPDx();
            String re = bc.bcDB.sSOPODDB.insertData(sSOPOpdx);
        }
        private SSOPOpdx setSSOPOPDx()
        {
            SSOPOpdx sSOPOpdx = new SSOPOpdx();
            sSOPOpdx.opdx_id = txtSSOPOPDxid.Text.Trim();
            sSOPOpdx.class1 = cboSSOPOPDxclass.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPDxclass.SelectedItem).Value : "";
            sSOPOpdx.svid = txtSSOPOPDxsvid.Text.Trim();
            sSOPOpdx.sl = cboSSOPOPDxsl.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPDxsl.SelectedItem).Value : "";
            sSOPOpdx.codeset = cboSSOPOPDxcodeset.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPDxcodeset.SelectedItem).Value : "";
            sSOPOpdx.code = txtSSOPOPDxcode.Text.Trim();
            sSOPOpdx.desc1 = txtSSOPOPDxdesc1.Text.Trim();
            sSOPOpdx.billtrans_id = "";
            sSOPOpdx.active = "1";
            return sSOPOpdx;
        }
        private void BtnSSOPProcOPServicesSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPOpservices sSOPOpservices = setSSOPOpServices();
            String re = bc.bcDB.sSOPOSDB.insertData(sSOPOpservices);
        }
        private SSOPOpservices setSSOPOpServices()
        {
            SSOPOpservices sSOPOpservices = new SSOPOpservices();
            sSOPOpservices.opservices_id = txtSSOPOpServicesid.Text.Trim();
            sSOPOpservices.billtrans_id = "";
            sSOPOpservices.svid = txtSSOPOpServicessvid.Text.Trim();
            sSOPOpservices.class1 = cboSSOPOPServicesclass.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPServicesclass.SelectedItem).Value : "";
            sSOPOpservices.invno = txtSSOPOpServicesinvno.Text.Trim();
            sSOPOpservices.hcode = "";
            sSOPOpservices.hn = txtSSOPProcHN.Text.Trim();
            sSOPOpservices.pid = txtSSOPProcPID.Text.Trim();
            sSOPOpservices.careaccount = cboSSOPOpServicescareaccount.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicescareaccount.SelectedItem).Value : "";
            sSOPOpservices.typeserv = cboSSOPOpServicestypeserv.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicestypeserv.SelectedItem).Value : "";
            sSOPOpservices.typein = cboSSOPOpServicestypein.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicestypein.SelectedItem).Value : "";
            sSOPOpservices.typeout = cboSSOPOpServicestypeout.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicestypeout.SelectedItem).Value : "";
            sSOPOpservices.dtappoint = bc.datetoDBCultureInfo(txtSSOPOPServicesdtappoint.Text.Trim());
            sSOPOpservices.svpid = txtSSOPOPServicessvpid.Text.Trim();
            sSOPOpservices.clinic = cboSSOPOPServicesclinic.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPServicesclinic.SelectedItem).Value : "";
            sSOPOpservices.begdt = bc.datetoDBCultureInfo(txtSSOPOPServicesbegdt.Text.Trim());
            sSOPOpservices.enddt = bc.datetoDBCultureInfo(txtSSOPOPServicesenddt.Text.Trim());
            sSOPOpservices.lccode = txtSSOPOpServiceslccode.Text.Trim();
            sSOPOpservices.codeset = cboSSOPOPServicescodeset.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPServicescodeset.SelectedItem).Value : "";
            sSOPOpservices.stdcode = txtSSOPOPServicestdcode.Text.Trim();
            sSOPOpservices.svcharge = txtSSOPOPServicessvcharge.Text.Trim();
            sSOPOpservices.completion = cboSSOPOpServicescompletion.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicescompletion.SelectedItem).Value : "";
            sSOPOpservices.svtxcode = txtSSOPOpServicessvtxcode.Text.Trim();
            sSOPOpservices.claimcat = cboSSOPOpServicesclaimcat.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicesclaimcat.SelectedItem).Value : "";
            return sSOPOpservices;
        }
        private void BtnSSOPProcDispenseditemSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPDispenseditem sSOPDispenseditem = setSSOPDispenseditem();
            String re = bc.bcDB.sSOPDIDB.insertData(sSOPDispenseditem);
        }
        private SSOPDispenseditem setSSOPDispenseditem()
        {
            SSOPDispenseditem dispitem = new SSOPDispenseditem();
            dispitem.dispenseditem_id = txtSSOPDispenseditemid.Text.Trim();             dispitem.billtrans_id = "";                                         dispitem.dispid = "";
            dispitem.prdcat = cboSSOPDispenseditemprdcat.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispenseditemprdcat.SelectedItem).Value : "";
            dispitem.hospdrgid = txtSSOPDispenseditemhospdrgid.Text.Trim();             dispitem.drgid = txtSSOPDispenseditemdrgid.Text.Trim();             dispitem.dfscode = txtSSOPDispenseditemdfscode.Text.Trim();
            dispitem.dfstext = txtSSOPDispenseditemdfstext.Text.Trim();                 dispitem.packsize = txtSSOPDispenseditempacksize.Text.Trim();       dispitem.sigcode = txtSSOPDispenseditemsigcode.Text.Trim();
            dispitem.sigtext = txtSSOPDispenseditemsigtext.Text.Trim();                 dispitem.quantity = txtSSOPDispenseditemquantity.Text.Trim();       dispitem.unitprice = txtSSOPDispenseditemunitprice.Text.Trim();
            dispitem.chargeamt = txtSSOPDispenseditemchargeamt.Text.Trim();             dispitem.reimbprice = txtSSOPDispenseditemreimbprice.Text.Trim();    dispitem.reimbamt = txtSSOPDispenseditemreimbamt.Text.Trim();
            dispitem.prdsecode = cboSSOPDispenseditemprdsecode.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispenseditemprdsecode.SelectedItem).Value : "";
            dispitem.claimcont = cboSSOPDispenseditemclaimcont.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispenseditemclaimcont.SelectedItem).Value : "";
            dispitem.claimcat = cboSSOPDispenseditemclaimcat.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispenseditemclaimcat.SelectedItem).Value : "";
            dispitem.multidisp = txtSSOPDispenseditemmultidisp.Text.Trim();             dispitem.supplyfor = txtSSOPDispenseditemsupplyfor.Text.Trim();
            return dispitem;
        }
        private void BtnSSOPProcDispensingSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPDispensing sSOPDispensing = setSSOPDispensing();
            String re = bc.bcDB.sSOPDDB.insertData(sSOPDispensing);
        }
        private SSOPDispensing setSSOPDispensing()
        {
            SSOPDispensing disp = new SSOPDispensing();
            disp.billdisp_id = txtSSOPDispID.Text.Trim();                                   disp.billtrans_id = "";                                     disp.hn = txtSSOPProcHN.Text.Trim();
            disp.pid = txtSSOPProcPID.Text.Trim();                                          disp.invno = txtSSOPDispensinginvno.Text.Trim();            disp.prescdt = bc.datetoDBCultureInfo(txtSSOPDispPrescdt.Text.Trim());
            disp.dispdt = bc.datetoDBCultureInfo(txtSSOPDispDispdt.Text.Trim());            disp.prescb = txtSSOPDispPrescb.Text.Trim();                disp.itemcnt = txtSSOPDispItemcnt.Text.Trim();
            disp.chargeamt = txtSSOPDispChargeamt.Text.Trim();                              disp.claimamt = txtSSOPDispClaimamt.Text.Trim();            disp.paid = txtSSOPDispPaid.Text.Trim();
            disp.otherpay = txtSSOPDispOtherpay.Text.Trim();
            disp.reimburser = txtSSOPDispReimburser.Text.Trim();
            disp.benefitplan = cboSSOPDispBenefitplan.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispBenefitplan.SelectedItem).Value : "";
            disp.dispestat = cboSSOPDispensingdispe.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispensingdispe.SelectedItem).Value : "";
            disp.svid = txtSSOPDispensingsvid.Text.Trim();                                  disp.daycover = txtSSOPDispensingdaycover.Text.Trim();      disp.dispid = txtSSOPDispensingdispid.Text.Trim();
            disp.providerid = txtSSOPDispensingproiderid.Text.Trim();

            return disp;
        }
        private void BtnSSOPProcBillItemsSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPBillItems sSOPBillItems = setSSOPBillItems();
            String re = bc.bcDB.ssopBillItemsDB.insertData(sSOPBillItems);
        }
        private SSOPBillItems setSSOPBillItems()
        {
            SSOPBillItems sSOPBillItems = new SSOPBillItems();
            sSOPBillItems.billitems_id = txtSSOPBillItemsID.Text.Trim();
            sSOPBillItems.billtrans_id = "";
            sSOPBillItems.billmuad = cboSSOPBillItemsbillmuad.SelectedItem != null ? ((ComboBoxItem)cboSSOPBillItemsbillmuad.SelectedItem).Value : "";
            sSOPBillItems.claimcat = cboSSOPBillItemsClaimcat.SelectedItem != null ? ((ComboBoxItem)cboSSOPBillItemsClaimcat.SelectedItem).Value : "";
            sSOPBillItems.invno = txtSSOPBillItemsInvno.Text.Trim();
            sSOPBillItems.svdate = bc.datetoDBCultureInfo(txtSSOPBillItemssvdate.Text.Trim());
            sSOPBillItems.lccode = txtSSOPBillItemsLccode.Text.Trim();
            sSOPBillItems.stdcode = txtSSOPBillItemsstdcode.Text.Trim();
            sSOPBillItems.desc1 = txtSSOPBillItemsDesc1.Text.Trim();
            sSOPBillItems.qty = txtSSOPBillItemsQty.Text.Trim();
            sSOPBillItems.up1 = txtSSOPBillItemsUp1.Text.Trim();
            sSOPBillItems.chargeamt = txtSSOPBillItemsChargeamt.Text.Trim();
            sSOPBillItems.claimup = txtSSOPBillItemsClaimup.Text.Trim();
            sSOPBillItems.claimamount = txtSSOPBillItemsClaimamount.Text.Trim();
            sSOPBillItems.svrefid = txtSSOPBillItemssvrefid.Text.Trim();
            return sSOPBillItems;
        }
        private void BtnSSOPProcBillItemsGet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void BtnSSOPSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPBillTran sSOPBillTran = setSSOPprocess();
            String re = bc.bcDB.sSOPBillTranDB.insertData(sSOPBillTran);
            if (int.Parse(re) > 0)            {                lfSbMessage.Text = "update ok";            }
            else            {                MessageBox.Show("insert error", "");            }
        }

        private void TcSSOP_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tcSSOP.SelectedTab == tabSSOPprocess)            {                setGrfSSOP();            }
            else if(tcSSOP.SelectedTab == tabGenText) { setGrfMakeText(); }
            else { }
        }
        private void BtnSSOPimport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            foreach(Row arow in grfItems.Rows)
            {
                String hn = arow[colgrfSSOPimpHn] != null ? arow[colgrfSSOPimpHn].ToString() : "";
                String preno = arow[colgrfSSOPimpPreno] != null ? arow[colgrfSSOPimpPreno].ToString() : "";
                String vsdate = arow[colgrfSSOPimpVsDate] != null ? arow[colgrfSSOPimpVsDate].ToString() : "";
                String name = arow[colgrfSSOPimpName] != null ? arow[colgrfSSOPimpName].ToString() : "";
                String pid = arow[colgrfSSOPimppid] != null ? arow[colgrfSSOPimppid].ToString() : "";
                String vstime = arow[colgrfSSOPimpVsTime] != null ? arow[colgrfSSOPimpVsTime].ToString() : "";
                if (int.TryParse(hn, out int i))
                {
                    DataTable dt = bc.bcDB.fint01DB.SelectByPreno(hn, vsdate, preno);
                    DataTable dtbillitems = bc.bcDB.fint01DB.SelectBillGroupSS1Bypreno(hn, vsdate, preno);
                    if(dtbillitems.Rows.Count <= 0){            MessageBox.Show("ไม่พบรายการ", "");              return;             }
                    String invno = "", amount="0", paid="0", secno="", docno="", docdate="", paidcode="";
                    if (dt.Rows.Count > 0)
                    {
                        String no = "000000"+ dt.Rows[0]["MNC_DOC_NO"].ToString();
                        no = no.Substring(no.Length - 6, 6);
                        String yr = "0000"+dt.Rows[0]["MNC_DOC_YR"].ToString();
                        yr = yr.Substring(2, 2);
                        invno = yr + no;//MNC_SUM_PRI
                        amount = dt.Rows[0]["MNC_SUM_PRI"].ToString();          paid = dt.Rows[0]["MNC_PAY_CASH"].ToString();       secno = dt.Rows[0]["MNC_SEC_NO"].ToString();
                        docno = dt.Rows[0]["MNC_DOC_NO"].ToString();            docdate = dt.Rows[0]["MNC_DOC_DAT"].ToString();
                        paidcode = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                    }
                    secno = "147";
                    paid = "0.00";
                    
                    SSOPBillTran ssopbilltran = setSSOPBillTran(secno, vsdate+"T"+ vstime, invno, "", hn, preno, amount, paid
                        , pid, name, paidcode, "80", paid, "");
                    //setGrfSSOP(hn, preno, vsdate);
                    String rebillt = bc.bcDB.sSOPBillTranDB.insertData(ssopbilltran);
                    if (long.Parse(rebillt) > 0)
                    {
                        String svid = "", class1="EC";
                        // insert bill items
                        foreach (DataRow rowb in dtbillitems.Rows)
                        {
                            String ss1 = "00"+rowb["mnc_grp_ss1"].ToString();               ss1 = ss1.Substring(ss1.Length - 2, 2);
                            String svid1 = "000"+rowb["MNC_FN_NO"].ToString();              svid1 = svid1.Substring(svid1.Length - 3, 3);
                            String svid2 = "000" + rowb["MNC_FN_CD"].ToString();            svid2 = svid2.Substring(svid2.Length - 3, 3);
                            svid = ss1 + svid2 + svid1;
                            if (ss1.Equals("04"))         //drug  เอามาเป็นรายการสั่งยา
                            {
                                String reqdrugold = "", otherpay="0.00", reimburser="HP";
                                float amt = 0;
                                DataTable dtdrug = bc.bcDB.pharT06DB.selectBypreno(hn, preno, vsdate);
                                if (dtdrug.Rows.Count > 0)
                                {
                                    foreach (DataRow rowd in dtdrug.Rows)
                                    {
                                        SSOPBillItems p = new SSOPBillItems();
                                        p.billtrans_id = rebillt;                                   p.billitems_id = "";                            p.billmuad = ss1;
                                        p.claimcat = "OP1";                                         p.invno = invno;                                p.svdate = rowd["MNC_CFR_DAT"].ToString();
                                        p.lccode = rowd["MNC_PH_CD"].ToString();                    p.stdcode = "";                                 p.desc1 = rowd["MNC_PH_TN"].ToString();
                                        p.qty = rowd["MNC_PH_QTY_PAID"].ToString();
                                        p.up1 = rowd["MNC_PH_PRI"].ToString();
                                        p.chargeamt = rowd["MNC_PH_PRI"].ToString();
                                        p.claimup = rowd["MNC_PH_PRI"].ToString();
                                        p.claimamount = rowd["MNC_PH_PRI"].ToString();
                                        p.svrefid = invno;
                                        p.active = "1";
                                        p.billmuad = "3";
                                        String re1 = bc.bcDB.ssopBillItemsDB.insertData(p);
                                        String reqdrug = rowd["MNC_CFR_NO"].ToString() + "." + rowd["MNC_CFR_YR"].ToString();
                                        if(!reqdrugold.Equals(reqdrug))
                                        {
                                            reqdrugold = reqdrug;
                                            float.TryParse(rowd["MNC_PH_PRI"].ToString(), out float price);
                                            amt += price;
                                            SSOPDispensing disp = new SSOPDispensing();
                                            disp.dispid = reqdrug;                              disp.billdisp_id = "";                                  disp.billtrans_id = rebillt;
                                            disp.hn = hn;                                       disp.pid = pid;                                         disp.invno = invno;
                                            disp.prescdt = rowd["MNC_CFR_DAT"].ToString();      disp.dispdt = rowd["MNC_CFR_DAT"].ToString();           disp.prescb = "ว"+rowd["MNC_DOT_CD"].ToString();
                                            disp.itemcnt = dtdrug.Rows.Count.ToString();        disp.chargeamt = amt.ToString("#,###.00");              disp.claimamt = amt.ToString("#,###.00");
                                            disp.paid = amt.ToString("#,###.00");               disp.otherpay = otherpay;                               disp.reimburser = reimburser;
                                            disp.benefitplan = "";                              disp.dispestat = "1";                                   disp.svid = svid;
                                            disp.daycover = "7D";                               disp.providerid = bc.iniC.ssoid;
                                            String re3 = bc.bcDB.sSOPDDB.insertData(disp);
                                        }
                                        SSOPDispenseditem ditem = new SSOPDispenseditem();
                                        ditem.dispid = reqdrug;                                     ditem.prdcat = "1";                                     ditem.hospdrgid = rowd["MNC_PH_CD"].ToString();
                                        ditem.drgid = rowd["tmt_code"].ToString();                  ditem.dfscode = rowd["MNC_PH_DIR_CD"].ToString();       ditem.dfstext = rowd["MNC_PH_DIR_DSC"].ToString();
                                        ditem.packsize = rowd["MNC_PH_UNT_CD"].ToString();          ditem.sigcode = rowd["MNC_PH_TIM_CD"].ToString();       ditem.sigtext = rowd["MNC_PH_TIM_DSC"].ToString();
                                        ditem.quantity = rowd["MNC_PH_QTY_PAID"].ToString();        ditem.unitprice = rowd["MNC_PH_PRI"].ToString();        ditem.chargeamt = rowd["MNC_PH_PRI"].ToString();
                                        ditem.reimbamt = rowd["MNC_PH_PRI"].ToString();             ditem.reimbprice = rowd["MNC_PH_PRI"].ToString();       ditem.prdsecode = "0";
                                        ditem.claimcont = "OD";                                     ditem.claimcat = "OP1";                                 ditem.multidisp = "11";
                                        ditem.supplyfor = "7D";                                     ditem.billtrans_id = rebillt;
                                        ditem.sigtext = ditem.sigtext.Length<= 0 ? "/" : ditem.sigtext;
                                        ditem.dfstext = ditem.dfstext.Length <= 0 ? "/" : ditem.dfstext;
                                        String re2 = bc.bcDB.sSOPDIDB.insertData(ditem);
                                    }
                                }
                            }
                            else if(ss1.Equals("17")) //รายการตรวจ
                            {
                                SSOPOpservices p = new SSOPOpservices();
                                p.opservices_id = "";                               p.svid = svid;                              p.class1 = class1;
                                p.invno = invno;                                    p.hcode = bc.iniC.ssoid;                               p.hn = hn;
                                p.pid = pid;                                        p.careaccount = "1";                        p.typeserv = "01";
                                p.typein = "1";                                     p.typeout = "1";                            p.dtappoint = "";
                                p.svpid = "ว"+rowb["MNC_DOT_CD_DF"].ToString();   //  Doctor
                                p.clinic = "01";                                      p.begdt = docdate;                          p.enddt = docdate;
                                p.lccode = svid;                                    p.codeset = "";                             p.stdcode = "";
                                p.svcharge = rowb["MNC_FN_PAD"].ToString();         p.completion = "";                          p.svtxcode = "";
                                p.claimcat = "OP1";                                 p.active = "1";                             p.billtrans_id = rebillt;
                                String re1 = bc.bcDB.sSOPOSDB.insertData(p);
                            }
                            else
                            {
                                SSOPBillItems p = new SSOPBillItems();
                                p.billtrans_id = rebillt;                           p.billitems_id = "";                            p.billmuad = rowb["mnc_grp_ss1"].ToString();
                                p.claimcat = "OP1";                                 p.invno = invno;                                p.svdate = rowb["MNC_FN_DAT"].ToString();
                                p.lccode = rowb["MNC_FN_CD"].ToString();            p.stdcode = "";                                 p.desc1 = rowb["MNC_FN_DSCT"].ToString();
                                p.qty = "1";                                        p.up1 = rowb["MNC_FN_PAD"].ToString();          p.chargeamt = rowb["MNC_FN_PAD"].ToString();
                                p.claimup = rowb["MNC_FN_PAD"].ToString();          p.claimamount = rowb["MNC_FN_PAD"].ToString();  p.svrefid = "";
                                p.active = "1";
                                String re1 = bc.bcDB.ssopBillItemsDB.insertData(p);
                            }
                        }
                        // end insert bill items
                        // insert OPDx
                        DataTable dtopdx = bc.bcDB.pt09DB.SelectBypreno(hn, preno, vsdate);
                        if (dtopdx.Rows.Count > 0)
                        {
                            foreach (DataRow rowd in dtopdx.Rows)
                            {
                                SSOPOpdx p = new SSOPOpdx();
                                p.opdx_id = "";                                     p.svid = svid;                                  p.class1 = class1;
                                p.sl = rowd["MNC_DIA_FLG"].ToString();              p.codeset = rowd["MNC_DIA_CD"].ToString();      p.code = rowd["MNC_DIA_CD"].ToString();
                                p.desc1 = "";                                       p.billtrans_id = rebillt;
                                String re1 = bc.bcDB.sSOPODDB.insertData(p);
                            }
                        }
                        //MessageBox.Show("insert ok", "");
                        //bc.bcDB.sSOPBillTranDB.updateData(ssopbilltran);
                        //bc.bcDB.sSOPBillTranDB.deleteData(ssopbilltran);
                    }
                    else
                    {
                        MessageBox.Show("insert error", "");
                    }
                }
            }
            grfItems.Rows.Count = 1;
            hideLbLoading();
        }
        private void initFont()
        {
            fEdit = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt7 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Regular);
            famt7B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new System.Drawing.Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new System.Drawing.Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            fEditS = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);

        }
        private SSOPBillTran setSSOPBillTran(string station, string dtran, string invno, string billno, string hn, String preno, string amount, string paid, string pid, string name, string paidcode, string payplan, string claimamt, string otherpay)
        {
            SSOPBillTran sSOPBillTran = new SSOPBillTran();
            sSOPBillTran.hcode = bc.iniC.ssoid; 
            sSOPBillTran.hmain = paidcode.Equals("44")?"11592": paidcode.Equals("45")? "11772" : paidcode.Equals("46")?"24036": paidcode.Equals("47")? "11592" : paidcode.Equals("48")? "11772" : paidcode.Equals("49")? "24036" : "24036";
            sSOPBillTran.billtran_id = "";            sSOPBillTran.station = station ?? "";
            sSOPBillTran.authcode = "";            sSOPBillTran.dtran = dtran ?? "";
            sSOPBillTran.invno = invno ?? "";
            sSOPBillTran.billno = billno ?? "";            sSOPBillTran.hn = hn ?? "";
            sSOPBillTran.memberno = "";            sSOPBillTran.amount = amount ?? "";
            sSOPBillTran.paid = paid ?? "";            sSOPBillTran.vercode = "";
            sSOPBillTran.tflag = "A";/*Fix Aขอเบิก*/            sSOPBillTran.pid = pid ?? "";
            sSOPBillTran.name = name ?? "";
            sSOPBillTran.payplan = payplan ?? "";            sSOPBillTran.claimamt = claimamt ?? "";
            sSOPBillTran.otherpayplan = "";            sSOPBillTran.otherpay = otherpay ?? "";
            sSOPBillTran.active = "1";            sSOPBillTran.preno = preno;
            return sSOPBillTran;
        }
        private void BtnSSOPSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfSSOPimp();
        }
        private void BtnSuprNewSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        
        private void initGrfMakeText()
        {
            grfMakeText = new C1FlexGrid();
            grfMakeText.Font = fEdit;
            grfMakeText.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMakeText.Location = new System.Drawing.Point(0, 0);
            grfMakeText.Rows.Count = 1;
            grfMakeText.Cols.Count = 6;

            grfMakeText.Cols[colgrfMakeTextSearchid].Width = 90;
            grfMakeText.Cols[colgrfMakeTextSearchname].Width = 300;
            grfMakeText.Cols[colgrfMakeTextSearchhn].Width = 100;
            grfMakeText.Cols[colgrfMakeTextSearchvsdate].Width = 100;
            grfMakeText.Cols[colgrfMakeTextSearchchk].Width = 50;
            grfMakeText.Cols[colgrfMakeTextSearchchk].DataType = typeof(bool);
            grfMakeText.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfMakeText.Cols[colgrfMakeTextSearchname].Caption = "name";
            grfMakeText.Cols[colgrfMakeTextSearchhn].Caption = "hn";
            grfMakeText.Cols[colgrfMakeTextSearchvsdate].Caption = "visitdate";
            grfMakeText.Cols[colgrfMakeTextSearchid].Visible = false;
            grfMakeText.Cols[colgrfMakeTextSearchname].AllowEditing = false;
            grfMakeText.Cols[colgrfMakeTextSearchhn].AllowEditing = false;
            grfMakeText.Cols[colgrfMakeTextSearchvsdate].AllowEditing = false;

            grfMakeText.DoubleClick += GrfMakeText_DoubleClick;
            pnSSOPGenTextMaketext.Controls.Add(grfMakeText);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfMakeTextSearch, bc.iniC.themegrfIpd);
        }
        private void GrfMakeText_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMakeText.Row < 1) return;
            String id = grfMakeText[grfMakeText.Row, colgrfMakeTextSearchid].ToString();
            String re = bc.bcDB.sSOPBillTranDB.updateStatusProcessMakeTextPrepare(id);
            grfMakeText.Rows.Remove(grfMakeText.Row);
        }
        private void setGrfMakeText()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPBillTranDB.selectBystatusprocess("1");
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfMakeText.Rows.Count = 1;
            grfMakeText.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfMakeText.Rows[i];
                rowa[colgrfMakeTextSearchid] = row1["billtran_id"].ToString();
                rowa[colgrfMakeTextSearchname] = row1["name"].ToString();
                rowa[colgrfMakeTextSearchhn] = row1["hn"].ToString();
                rowa[colgrfMakeTextSearchvsdate] = row1["dtran"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfMakeTextSearch()
        {
            grfMakeTextSearch = new C1FlexGrid();
            grfMakeTextSearch.Font = fEdit;
            grfMakeTextSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMakeTextSearch.Location = new System.Drawing.Point(0, 0);
            grfMakeTextSearch.Rows.Count = 1;
            grfMakeTextSearch.Cols.Count = 6;

            grfMakeTextSearch.Cols[colgrfMakeTextSearchid].Width = 90;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchname].Width = 300;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchhn].Width = 100;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchvsdate].Width = 100;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchchk].Width = 50;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchchk].DataType = typeof(bool);
            grfMakeTextSearch.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchname].Caption = "name";
            grfMakeTextSearch.Cols[colgrfMakeTextSearchhn].Caption = "hn";
            grfMakeTextSearch.Cols[colgrfMakeTextSearchvsdate].Caption = "visitdate";
            grfMakeTextSearch.Cols[colgrfMakeTextSearchid].Visible = false;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchname].AllowEditing = false;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchhn].AllowEditing = false;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchvsdate].AllowEditing = false;

            grfMakeTextSearch.DoubleClick += GrfMakeTextSearch_DoubleClick;
            pnGenTextSearch.Controls.Add(grfMakeTextSearch);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfMakeTextSearch, bc.iniC.themegrfIpd);
        }

        private void GrfMakeTextSearch_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMakeTextSearch.Row < 1) return;
            
            Row rowa = grfMakeText.Rows.Add();
            rowa[colgrfMakeTextSearchchk] = true;
            rowa[colgrfMakeTextSearchid] = grfMakeTextSearch[grfMakeTextSearch.Row, colgrfMakeTextSearchid].ToString();
            rowa[colgrfMakeTextSearchname] = grfMakeTextSearch[grfMakeTextSearch.Row, colgrfMakeTextSearchname].ToString();
            rowa[colgrfMakeTextSearchhn] = grfMakeTextSearch[grfMakeTextSearch.Row, colgrfMakeTextSearchhn].ToString();
            rowa[colgrfMakeTextSearchvsdate] = grfMakeTextSearch[grfMakeTextSearch.Row, colgrfMakeTextSearchvsdate].ToString();
            
        }

        private void setGrfMakeTextSearch(String search)
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPBillTranDB.selectBySearch(search);
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfMakeTextSearch.Rows.Count = 1;
            grfMakeTextSearch.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfMakeTextSearch.Rows[i];
                rowa[colgrfMakeTextSearchid] = row1["billtran_id"].ToString();
                rowa[colgrfMakeTextSearchname] = row1["name"].ToString();
                rowa[colgrfMakeTextSearchhn] = row1["hn"].ToString();
                rowa[colgrfMakeTextSearchvsdate] = row1["dtran"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfOPDx()
        {
            grfOPDx = new C1FlexGrid();
            grfOPDx.Font = fEdit;
            grfOPDx.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPDx.Location = new System.Drawing.Point(0, 0);
            grfOPDx.Rows.Count = 1;
            grfOPDx.Cols.Count = 5;

            grfOPDx.Cols[colgrfOPDxid].Width = 90;
            grfOPDx.Cols[colgrfOPDxsl].Width = 60;
            grfOPDx.Cols[colgrfOPDxcodeset].Width = 100;
            grfOPDx .Cols[colgrfOPDxcode].Width = 300;

            grfOPDx.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOPDx .Cols[colgrfOPDxsl].Caption = "SL";
            grfOPDx.Cols[colgrfOPDxcodeset].Caption = "codeset";
            grfOPDx .Cols[colgrfOPDxcode].Caption = "code";
            grfOPDx .Cols[colgrfOPDxid].Visible = false;
            grfOPDx.Cols[colgrfOPDxsl].AllowEditing = false;
            grfOPDx.Cols[colgrfOPDxcodeset].AllowEditing = false;
            grfOPDx.Cols[colgrfOPDxcode].AllowEditing = false;

            grfOPDx.Click += GrfOPDx_Click;
            pnOPDx.Controls.Add(grfOPDx);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfOPDx, bc.iniC.themegrfIpd);
        }
        private void GrfOPDx_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfOPDx.Row < 1) return;
            String opdxid = grfOPDx[grfOPDx.Row, colgrfOPDxid] != null ? grfOPDx[grfOPDx.Row, colgrfOPDxid].ToString() : "";
            setControlOPDx(opdxid);
        }
        private void setControlOPDx(String opdxid)
        {
            clearControlOPDx();
            SSOPOpdx item = bc.bcDB.sSOPODDB.selectByPk(opdxid);
            if (item == null) return;
            txtSSOPOPDxid.Value = item.opdx_id;
            txtSSOPOPDxsvid.Value = item.svid;
            txtSSOPOPDxcode.Value = item.code;
            txtSSOPOPDxdesc1.Value = item.desc1;
            txtSSOPOPDxsvid.Value = item.svid;
            bc.setC1Combo(cboSSOPOPDxcodeset, item.codeset);
            bc.setC1Combo(cboSSOPOPDxsl, item.sl);
            bc.setC1Combo(cboSSOPOPDxclass, item.class1);
        }
        private void clearControlOPDx()
        {
            txtSSOPOPDxid.Value = "";
            txtSSOPOPDxsvid.Value = "";
            txtSSOPOPDxcode.Value = "";
            txtSSOPOPDxdesc1.Value = "";
            txtSSOPOPDxsvid.Value = "";
            cboSSOPOPDxcodeset.SelectedIndex = -1;
            cboSSOPOPDxsl.SelectedIndex = -1;
            cboSSOPOPDxclass.SelectedIndex = -1;
        }
        private void setGrfOPDx()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPODDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfOPDx.Rows.Count = 1;
            grfOPDx.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfOPDx.Rows[i];
                rowa[colgrfOPDxid] = row1["opdx_id"].ToString();
                rowa[colgrfOPDxsl] = row1["sl"].ToString();
                rowa[colgrfOPDxcodeset] = row1["codeset"].ToString();
                rowa[colgrfOPDxcode] = row1["code"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfOpServ()
        {
            grfOpServ = new C1FlexGrid();
            grfOpServ.Font = fEdit;
            grfOpServ.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOpServ.Location = new System.Drawing.Point(0, 0);
            grfOpServ.Rows.Count = 1;
            grfOpServ.Cols.Count = 5;

            grfOpServ.Cols[colgrfOPServid].Width = 90;
            grfOpServ.Cols[colgrfOPServsvid].Width = 100;
            grfOpServ.Cols[colgrfOPServcodeset].Width = 100;
            grfOpServ.Cols[colgrfOPServtypein].Width = 300;

            grfOpServ.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOpServ.Cols[colgrfOPServsvid].Caption = "svid";
            grfOpServ.Cols[colgrfOPServcodeset].Caption = "codeset";
            grfOpServ.Cols[colgrfOPServtypein].Caption = "typein";
            grfOpServ.Cols[colgrfOPServid].Visible = false;
            grfOpServ.Cols[colgrfOPServsvid].AllowEditing = false;
            grfOpServ.Cols[colgrfOPServcodeset].AllowEditing = false;
            grfOpServ.Cols[colgrfOPServtypein].AllowEditing = false;

            grfOpServ.Click += GrfOpServ_Click;
            pnOpServ.Controls.Add(grfOpServ);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfOpServ, bc.iniC.themegrfIpd);
        }

        private void GrfOpServ_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfOpServ.Row < 1) return;
            String opservid = grfOpServ[grfOpServ.Row, colgrfOPServid] != null ? grfOpServ[grfOpServ.Row, colgrfOPServid].ToString() : "";
            setControlOpServ(opservid);
        }
        private void setControlOpServ(String opservid)
        {
            clearControlOpServ();
            SSOPOpservices item = bc.bcDB.sSOPOSDB.selectByPk(opservid);
            if (item == null) return;
            txtSSOPOpServicesid.Value = item.opservices_id;
            txtSSOPOpServicesinvno.Value = item.invno;
            txtSSOPOpServicessvid.Value = item.svid;
            txtSSOPOPServicesdtappoint.Value = item.dtappoint;
            txtSSOPOPServicessvpid.Value = item.svpid;
            txtSSOPOPServicestdcode.Value = item.stdcode;
            //txtSSOPOPServicesdtappoint.Value = item.dtappoint;
            txtSSOPOPServicesbegdt.Value = item.begdt;
            txtSSOPOPServicesenddt.Value = item.enddt;
            txtSSOPOpServiceslccode.Value = item.lccode;
            txtSSOPOpServicessvtxcode.Value = item.svtxcode;
            bc.setC1Combo(cboSSOPOPServicesclass, item.class1);
            bc.setC1Combo(cboSSOPOpServicescareaccount, item.careaccount);
            bc.setC1Combo(cboSSOPOpServicestypeserv, item.typeserv);
            bc.setC1Combo(cboSSOPOpServicestypein, item.typein);
            bc.setC1Combo(cboSSOPOpServicestypeout, item.typeout);
            bc.setC1Combo(cboSSOPOpServicescompletion, item.completion);
            bc.setC1Combo(cboSSOPOPServicesclinic, item.clinic);
            bc.setC1Combo(cboSSOPOpServicesclaimcat, item.claimcat);
            bc.setC1Combo(cboSSOPOPServicescodeset, item.codeset);
        }
        private void clearControlOpServ()
        {
            txtSSOPOpServicesid.Value = "";
            txtSSOPOpServicesinvno.Value = "";
            txtSSOPOpServicessvid.Value = "";
            txtSSOPOPServicesdtappoint.Value = "";
            txtSSOPOPServicessvpid.Value = "";
            txtSSOPOPServicestdcode.Value = "";
            //txtSSOPOPServicesdtappoint.Value = item.dtappoint;
            txtSSOPOPServicesbegdt.Value = "";
            txtSSOPOPServicesenddt.Value = "";
            txtSSOPOpServiceslccode.Value = "";
            txtSSOPOpServicessvtxcode.Value = "";
            cboSSOPOPServicesclass.SelectedIndex = -1;
            cboSSOPOpServicescareaccount.SelectedIndex = -1;
            cboSSOPOpServicestypeserv.SelectedIndex = -1;
            cboSSOPOpServicestypein.SelectedIndex = -1;
            cboSSOPOpServicestypeout.SelectedIndex = -1;
            cboSSOPOpServicescompletion.SelectedIndex = -1;
            cboSSOPOPServicesclinic.SelectedIndex = -1;
            cboSSOPOpServicesclaimcat.SelectedIndex = -1;
            cboSSOPOPServicescodeset.SelectedIndex = -1;
        }
        private void setGrfOpServ()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPOSDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfOpServ.Rows.Count = 1;
            grfOpServ.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfOpServ.Rows[i];
                rowa[colgrfOPServid] = row1["opservices_id"].ToString();
                rowa[colgrfOPServsvid] = row1["svid"].ToString();
                rowa[colgrfOPServcodeset] = row1["codeset"].ToString();
                rowa[colgrfOPServtypein] = row1["typein"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfDispItems()
        {
            grfDispI = new C1FlexGrid();
            grfDispI.Font = fEdit;
            grfDispI.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDispI.Location = new System.Drawing.Point(0, 0);
            grfDispI.Rows.Count = 1;
            grfDispI.Cols.Count = 5;

            grfDispI.Cols[colgrfDispIid].Width = 90;
            grfDispI.Cols[colgrfDispIdrgid].Width = 100;
            grfDispI.Cols[colgrfDispIdfscode].Width = 100;
            grfDispI.Cols[colgrfDispIdfstext].Width = 500;

            grfDispI.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfDispI.Cols[colgrfDispIdrgid].Caption = "drgid";
            grfDispI.Cols[colgrfDispIdfscode].Caption = "dfscode";
            grfDispI.Cols[colgrfDispIdfstext].Caption = "dfstext";
            grfDispI.Cols[colgrfDispIid].Visible = false;
            grfDispI.Cols[colgrfDispIdrgid].AllowEditing = false;
            grfDispI.Cols[colgrfDispIdfscode].AllowEditing = false;
            grfDispI.Cols[colgrfDispIdfstext].AllowEditing = false;

            grfDispI.Click += GrfDispI_Click;
            pnDispI.Controls.Add(grfDispI);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfDispI, bc.iniC.themegrfOpd);
        }

        private void GrfDispI_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDispI.Row < 1) return;
            String dispIid = grfDispI[grfDispI.Row, colgrfDispIid] != null ? grfDispI[grfDispI.Row, colgrfDispIid].ToString() : "";
            setControlDispI(dispIid);
        }
        private void setControlDispI(String dispiid)
        {
            clearControlDispI();
            SSOPDispenseditem item = bc.bcDB.sSOPDIDB.selectByPk(dispiid);
            if (item == null) return;
            txtSSOPDispenseditemid.Value = item.dispenseditem_id;
            txtSSOPDispenseditemdispid.Value = item.dispid;
            txtSSOPDispenseditemhospdrgid.Value = item.hospdrgid;
            txtSSOPDispenseditemdrgid.Value = item.drgid;
            txtSSOPDispenseditemdfscode.Value = item.dfscode;
            txtSSOPDispenseditemdfstext.Value = item.dfstext;
            txtSSOPDispenseditempacksize.Value = item.packsize;
            txtSSOPDispenseditemsigcode.Value = item.sigcode;
            txtSSOPDispenseditemsigtext.Value = item.sigtext;
            txtSSOPDispenseditemquantity.Value = item.quantity;
            txtSSOPDispenseditemunitprice.Value = item.unitprice;
            txtSSOPDispenseditemchargeamt.Value = item.chargeamt;
            txtSSOPDispenseditemreimbamt.Value = item.reimbamt;
            txtSSOPDispenseditemreimbprice.Value = item.reimbprice;
            txtSSOPDispenseditemsupplyfor.Value = item.supplyfor;
            txtSSOPDispenseditemmultidisp.Value = item.multidisp;
            //txtSSOPDispenseditemhospdrgid.Value = item.claimcont;
            bc.setC1Combo(cboSSOPDispenseditemclaimcont, item.claimcont);
            bc.setC1Combo(cboSSOPDispenseditemclaimcat, item.claimcat);
            bc.setC1Combo(cboSSOPDispenseditemprdcat, item.prdcat);
            bc.setC1Combo(cboSSOPDispenseditemprdsecode, item.prdsecode);
        }
        private void clearControlDispI()
        {
            txtSSOPDispenseditemid.Value = "";            txtSSOPDispenseditemdispid.Value = "";
            txtSSOPDispenseditemhospdrgid.Value = "";            txtSSOPDispenseditemdrgid.Value = "";
            txtSSOPDispenseditemdfscode.Value = "";            txtSSOPDispenseditemdfstext.Value = "";
            txtSSOPDispenseditempacksize.Value = "";            txtSSOPDispenseditemsigcode.Value = "";
            txtSSOPDispenseditemsigtext.Value = "";            txtSSOPDispenseditemquantity.Value = "";
            txtSSOPDispenseditemunitprice.Value = "";            txtSSOPDispenseditemchargeamt.Value = "";
            txtSSOPDispenseditemreimbamt.Value = "";            txtSSOPDispenseditemreimbprice.Value = "";
            txtSSOPDispenseditemsupplyfor.Value = "";            txtSSOPDispenseditemmultidisp.Value = "";
        }
        private void setGrfDispItems()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPDIDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfDispI.Rows.Count = 1;
            grfDispI.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDispI.Rows[i];
                rowa[colgrfDispIid] = row1["dispenseditem_id"].ToString();
                rowa[colgrfDispIdrgid] = row1["drgid"].ToString();
                rowa[colgrfDispIdfscode] = row1["dfscode"].ToString();
                rowa[colgrfDispIdfstext] = row1["dfstext"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfDisp()
        {
            grfDisp = new C1FlexGrid();
            grfDisp.Font = fEdit;
            grfDisp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDisp.Location = new System.Drawing.Point(0, 0);
            grfDisp.Rows.Count = 1;
            grfDisp.Cols.Count = 5;

            grfDisp.Cols[colgrfDispid].Width = 90;
            grfDisp.Cols[colgrfDispprescdt].Width = 100;
            grfDisp.Cols[colgrfDispprescb].Width = 100;
            grfDisp.Cols[colgrfDispitemcnt].Width = 100;

            grfDisp.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfDisp.Cols[colgrfDispprescdt].Caption = "prescdt";
            grfDisp.Cols[colgrfDispprescb].Caption = "prescb";
            grfDisp.Cols[colgrfDispitemcnt].Caption = "itemcnt";
            grfDisp.Cols[colgrfDispid].Visible = false;
            grfDisp.Cols[colgrfDispprescdt].AllowEditing = false;
            grfDisp.Cols[colgrfDispprescb].AllowEditing = false;
            grfDisp.Cols[colgrfDispitemcnt].AllowEditing = false;

            grfDisp.Click += GrfDisp_Click;
            pnDisp.Controls.Add(grfDisp);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfDisp, bc.iniC.themegrfIpd);
        }

        private void GrfDisp_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDisp.Row < 1) return;
            String dispId = grfDisp[grfDisp.Row, colgrfDispid] != null ? grfDisp[grfDisp.Row, colgrfDispid].ToString() : "";
            setControlDispsing(dispId);
        }
        private void setGrfDisp()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPDDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfDisp.Rows.Count = 1;
            grfDisp.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDisp.Rows[i];
                rowa[colgrfDispid] = row1["billdisp_id"].ToString();
                rowa[colgrfDispprescdt] = row1["prescdt"].ToString();
                rowa[colgrfDispprescb] = row1["prescb"].ToString();
                rowa[colgrfDispitemcnt] = row1["itemcnt"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void setControlDispsing(String dispId)
        {
            clearControlDispsing();
            SSOPDispensing item = bc.bcDB.sSOPDDB.selectByPk(dispId);
            if (item != null)
            {
                txtSSOPDispID.Value = item.billdisp_id;
                txtSSOPDispPrescdt.Value = item.prescdt;
                txtSSOPDispDispdt.Value = item.dispdt;
                txtSSOPDispPrescb.Value = item.prescb;
                txtSSOPDispItemcnt.Value = item.itemcnt;
                txtSSOPDispChargeamt.Value = item.chargeamt;
                txtSSOPDispClaimamt.Value = item.claimamt;
                txtSSOPDispPaid.Value = item.paid;
                txtSSOPDispOtherpay.Value = item.otherpay;
                txtSSOPDispReimburser.Value = item.reimburser;
                txtSSOPDispensinginvno.Value = item.invno;
                txtSSOPDispensingdispid.Value = item.dispid;
                txtSSOPDispensingdaycover.Value = item.daycover;
                txtSSOPDispensingsvid.Value = item.svid;
                txtSSOPDispensingproiderid.Value = item.providerid;
                //txtSSOPDispensingdaycover.Value = item.claimamt;

                bc.setC1Combo(cboSSOPDispBenefitplan, item.benefitplan);
                bc.setC1Combo(cboSSOPDispensingdispe, item.dispestat);
            }
        }
        private void clearControlDispsing()
        {
            txtSSOPDispID.Value = "";            txtSSOPDispPrescdt.Value = "";
            txtSSOPDispDispdt.Value = "";            txtSSOPDispPrescb.Value = "";
            txtSSOPDispItemcnt.Value = "";            txtSSOPDispChargeamt.Value = "";
            txtSSOPDispClaimamt.Value = "";            txtSSOPDispPaid.Value = "";
            txtSSOPDispOtherpay.Value = "";            txtSSOPDispReimburser.Value = "";
            txtSSOPDispensinginvno.Value = "";            txtSSOPDispensingdispid.Value = "";
            txtSSOPDispensingdaycover.Value = "";            txtSSOPDispensingsvid.Value = "";
            txtSSOPDispensingproiderid.Value = "";            bc.setC1Combo(cboSSOPDispBenefitplan, "");
            bc.setC1Combo(cboSSOPDispensingdispe, "");
        }
        private void initGrfBillItems()
        {
            grfBillI = new C1FlexGrid();
            grfBillI.Font = fEdit;
            grfBillI.Dock = System.Windows.Forms.DockStyle.Fill;
            grfBillI.Location = new System.Drawing.Point(0, 0);
            grfBillI.Rows.Count = 1;
            grfBillI.Cols.Count = 5;

            grfBillI.Cols[colgrfBillIid].Width = 90;
            grfBillI.Cols[colgrfBillIsvdate].Width = 100;
            grfBillI.Cols[colgrfBillIlccode].Width = 100;
            grfBillI.Cols[colgrfBillIdesc].Width = 400;

            grfBillI.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfBillI.Cols[colgrfBillIsvdate].Caption = "visitdate";
            grfBillI.Cols[colgrfBillIlccode].Caption = "lccode";
            grfBillI.Cols[colgrfBillIdesc].Caption = "desc1";
            grfBillI.Cols[colgrfBillIlccode].DataType = typeof(string);
            grfBillI.Cols[colgrfBillIid].Visible = false;
            grfBillI.Cols[colgrfBillIsvdate].AllowEditing = false;
            grfBillI.Cols[colgrfBillIlccode].AllowEditing = false;
            grfBillI.Cols[colgrfBillIdesc].AllowEditing = false;

            grfBillI.Click += GrfBillI_Click;
            pnBillI.Controls.Add(grfBillI);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfBillI, bc.iniC.themeApp);
        }

        private void GrfBillI_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfBillI.Row < 1) return;
            String billiid = grfBillI[grfBillI.Row, colgrfBillIid] != null ? grfBillI[grfBillI.Row, colgrfBillIid].ToString() : "";
            setControlBillItems(billiid);
        }
        private void setGrfBillI()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.ssopBillItemsDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfBillI.Rows.Count = 1;
            grfBillI.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfBillI.Rows[i];
                rowa[colgrfBillIid] = row1["billitems_id"].ToString();
                rowa[colgrfBillIsvdate] = row1["svdate"].ToString();
                rowa[colgrfBillIlccode] = row1["lccode"].ToString();
                rowa[colgrfBillIdesc] = row1["desc1"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void setControlBillItems(String billiid)
        {
            clearControlBillItems();
            SSOPBillItems item = bc.bcDB.ssopBillItemsDB.selectByPk(billiid);
            if (item != null)
            {
                txtSSOPBillItemsID.Value = item.billitems_id;
                txtSSOPBillItemssvdate.Value = item.svdate;
                txtSSOPBillItemssvrefid.Value = item.svrefid;
                txtSSOPBillItemsLccode.Value = item.lccode;
                txtSSOPBillItemsDesc1.Value = item.desc1;
                bc.setC1Combo(cboSSOPBillItemsClaimcat, item.claimcat);
                bc.setC1Combo(cboSSOPBillItemsbillmuad, item.billmuad);
                txtSSOPBillItemsQty.Value = item.qty;
                txtSSOPBillItemsUp1.Value = item.up1;
                txtSSOPBillItemsChargeamt.Value = item.chargeamt;
                txtSSOPBillItemsClaimup.Value = item.claimup;
                txtSSOPBillItemsClaimamount.Value = item.claimamount;
                txtSSOPBillItemsInvno.Value = item.invno;
                txtSSOPBillItemsstdcode.Value = item.stdcode;
            }

        }
        private void clearControlBillItems()
        {
            txtSSOPBillItemsID.Value = "";            txtSSOPBillItemssvdate.Value = "";
            txtSSOPBillItemssvrefid.Value = "";            txtSSOPBillItemsLccode.Value = "";
            txtSSOPBillItemsstdcode.Value = "";            txtSSOPBillItemsDesc1.Value = "";
            bc.setC1Combo(cboSSOPBillItemsClaimcat, "");            bc.setC1Combo(cboSSOPBillItemsbillmuad, "");
            txtSSOPBillItemsQty.Value = "";            txtSSOPBillItemsUp1.Value = "";
            txtSSOPBillItemsChargeamt.Value = "";            txtSSOPBillItemsClaimup.Value = "";
            txtSSOPBillItemsClaimamount.Value = "";            txtSSOPBillItemsInvno.Value = "";
        }
        private void initGrfSSOP()
        {
            grfSSOP = new C1FlexGrid();
            grfSSOP.Font = fEdit;
            grfSSOP.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSSOP.Location = new System.Drawing.Point(0, 0);
            grfSSOP.Rows.Count = 1;
            grfSSOP.Cols.Count = 7;

            grfSSOP.Cols[colgrfSSOPhn].Width = 90;
            grfSSOP.Cols[colgrfSSOPname].Width = 300;
            grfSSOP.Cols[colgrfSSOPvsdate].Width = 100;
            grfSSOP.Cols[colgrfSSOPpreno].Width = 70;
            grfSSOP.Cols[colgrfSSOPid].Width = 150;

            grfSSOP.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfSSOP.Cols[colgrfSSOPhn].Caption = "hn";
            grfSSOP.Cols[colgrfSSOPname].Caption = "name"; // ชื่อหัวคอลัมน์
            grfSSOP.Cols[colgrfSSOPvsdate].Caption = "date";
            grfSSOP.Cols[colgrfSSOPpreno].Caption = "preno";
            grfSSOP.Cols[colgrfSSOPid].Caption = "id";
            grfSSOP.Cols[colgrfSSOPSymptoms].Caption = "อาการ";

            grfSSOP.Cols[colgrfSSOPid].Visible = false;
            grfSSOP.Cols[colgrfSSOPhn].AllowEditing = false;
            grfSSOP.Cols[colgrfSSOPname].AllowEditing = false;
            grfSSOP.Cols[colgrfSSOPvsdate].AllowEditing = false;
            grfSSOP.Cols[colgrfSSOPpreno].AllowEditing = false;
            grfSSOP.Cols[colgrfSSOPSymptoms].AllowEditing = false;

            grfSSOP.Click += GrfSSOP_Click;
            pnSSOP.Controls.Add(grfSSOP);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfSSOP, bc.iniC.themegrfIpd);
        }

        private void GrfSSOP_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSSOP.Row < 1) return;
            String billtranid = grfSSOP[grfSSOP.Row, colgrfSSOPid] != null ? grfSSOP[grfSSOP.Row, colgrfSSOPid].ToString() : "";
            setControlSSOPprocess(billtranid);
        }
        private void setGrfSSOP()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPBillTranDB.selectBystatusprocess("0");
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfSSOP.Rows.Count = 1;
            grfSSOP.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSSOP.Rows[i];
                rowa[colgrfSSOPhn] = row1["hn"].ToString();
                rowa[colgrfSSOPname] = row1["name"].ToString();
                rowa[colgrfSSOPvsdate] = row1["dtran"].ToString();
                rowa[colgrfSSOPpreno] = row1["preno"].ToString();
                rowa[colgrfSSOPid] = row1["billtran_id"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void setControlSSOPprocess(String billtranid)
        {
            clearControlBillItems();
            clearControlDispsing();
            clearControlDispI();
            clearControlOpServ();
            clearControlOPDx();
            SSOPBillTran sSOPBillTran = bc.bcDB.sSOPBillTranDB.selectByPk(billtranid);
            DataTable dtbitems = bc.bcDB.ssopBillItemsDB.selectByBilltransId(billtranid);
            DataTable dtdisp = bc.bcDB.sSOPDDB.selectByBilltransId(billtranid);
            DataTable dtdispitems = bc.bcDB.sSOPDIDB.selectByBilltransId(billtranid);
            DataTable dtopdx = bc.bcDB.sSOPODDB.selectByBilltransId(billtranid);
            DataTable dtOpservices = bc.bcDB.sSOPOSDB.selectByBilltransId(billtranid);
            if (sSOPBillTran != null)
            {
                txtSSOPProcHN.Value = sSOPBillTran.hn;
                lbSSOPProcName.Text = sSOPBillTran.name;
                txtSSOPProcVsDate.Value = sSOPBillTran.dtran;
                txtSSOPProcPID.Value = sSOPBillTran.pid;
                txtSSOPProcmemberno.Value = sSOPBillTran.memberno;
                txtSSOPProcVercode.Value = sSOPBillTran.vercode;
                txtSSOPProcPaid.Value = sSOPBillTran.paid;
                txtSSOPProcInvno.Value = sSOPBillTran.invno;
                txtSSOPProcBillno.Value = sSOPBillTran.billno;
                txtSSOPProcClaimAmt.Value = sSOPBillTran.claimamt;

                txtSSOPProcAmount.Value = sSOPBillTran.amount;
                txtSSOPProcHCODE.Value = sSOPBillTran.hcode;
                txtSSOPProcOtherPay.Value = sSOPBillTran.otherpay;
                bc.setC1Combo(cboSSOPProcTFlag, sSOPBillTran.tflag);
                bc.setC1Combo(cboSSOPProcOPayPlan, sSOPBillTran.otherpayplan);
                bc.setC1Combo(cboSSOPProcPayPlan, sSOPBillTran.payplan);
                bc.setC1Combo(cboSSOPProcStation, sSOPBillTran.station);
                txtSSOPProcHMain.Value = sSOPBillTran.hmain;
                txtSSOPBilltransID.Value = sSOPBillTran.billtran_id;
                //setGrfSSOPimp(sSOPBillTran.hn, sSOPBillTran.preno, sSOPBillTran.dtran);
                setGrfBillI();
                setGrfDisp();
                setGrfDispItems();
                setGrfOpServ();
                setGrfOPDx();
            }
        }
        private SSOPBillTran setSSOPprocess()
        {
            SSOPBillTran sSOPBillTran = new SSOPBillTran();

            sSOPBillTran.hn = txtSSOPProcHN.Text;
            //lbSSOPProcName.Text = sSOPBillTran.name;
            sSOPBillTran.dtran = bc.datetoDBCultureInfo(txtSSOPProcVsDate.Text);
            sSOPBillTran.memberno = txtSSOPProcmemberno.Text;
            sSOPBillTran.paid = txtSSOPProcPaid.Text;
            sSOPBillTran.billno = txtSSOPProcBillno.Text;
            sSOPBillTran.claimamt = txtSSOPProcClaimAmt.Text;
            sSOPBillTran.vercode = txtSSOPProcVercode.Text;
            sSOPBillTran.paid = txtSSOPProcPaid.Text;
            sSOPBillTran.invno = txtSSOPProcInvno.Text;
            sSOPBillTran.pid = txtSSOPProcPID.Text.Trim();

            sSOPBillTran.amount = txtSSOPProcAmount.Text;
            sSOPBillTran.hcode = txtSSOPProcHCODE.Text;
            sSOPBillTran.otherpay = txtSSOPProcOtherPay.Text;
            sSOPBillTran.tflag = cboSSOPProcTFlag.SelectedItem == null ? "" : ((ComboBoxItem)cboSSOPProcTFlag.SelectedItem).Value.ToString();
            sSOPBillTran.otherpayplan = cboSSOPProcOPayPlan.SelectedItem == null ? "" : ((ComboBoxItem)cboSSOPProcOPayPlan.SelectedItem).Value.ToString();
            sSOPBillTran.payplan = cboSSOPProcPayPlan.SelectedItem == null ? "" : ((ComboBoxItem)cboSSOPProcPayPlan.SelectedItem).Value.ToString();
            sSOPBillTran.station = cboSSOPProcStation.SelectedItem == null ? "" : ((ComboBoxItem)cboSSOPProcStation.SelectedItem).Value.ToString();
            sSOPBillTran.hmain = txtSSOPProcHMain.Text;
            sSOPBillTran.billtran_id = txtSSOPBilltransID.Text;
            //setGrfSSOPimp(sSOPBillTran.hn, sSOPBillTran.preno, sSOPBillTran.dtran);
            return sSOPBillTran;
        }
        private void initGrfItems()
        {
            grfItems = new C1FlexGrid();
            grfItems.Font = fEdit;
            grfItems.Dock = System.Windows.Forms.DockStyle.Fill;
            grfItems.Location = new System.Drawing.Point(0, 0);
            grfItems.Rows.Count = 1;
            grfItems.Cols.Count = 10;

            grfItems.Cols[colgrfSSOPimpVsDate].Width = 90;
            grfItems.Cols[colgrfSSOPimpPreno].Width = 60;
            grfItems.Cols[colgrfSSOPimpHn].Width = 100;
            grfItems.Cols[colgrfSSOPimpSymptoms].Width = 300;
            grfItems.Cols[colgrfSSOPimpName].Width = 150;
            grfItems.Cols[colgrfSSOPimpPaidName].Width = 380;
            grfItems.Cols[colgrfSSOPimpchk].Width = 80;

            grfItems.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfItems.Cols[colgrfSSOPimpchk].DataType = typeof(bool);
            grfItems.Cols[colgrfSSOPimpchk].Caption = "Select"; // ชื่อหัวคอลัมน์
            grfItems.Cols[colgrfSSOPimpchk].TextAlign = TextAlignEnum.LeftCenter;
            grfItems.Cols[colgrfSSOPimpVsDate].Caption = "visitdate";
            grfItems.Cols[colgrfSSOPimpPreno].Caption = "preno";
            grfItems.Cols[colgrfSSOPimpHn].Caption = "HN";
            grfItems.Cols[colgrfSSOPimpchk].Visible = false;
            grfItems.Cols[colgrfSSOPimpVsDate].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpVsTime].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpPreno].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpHn].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpName].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpSymptoms].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpPaidName].AllowEditing = false;

            grfItems.Click += GrfItems_Click;
            grfItems.DoubleClick += GrfItems_DoubleClick;
            pnSSOPItems.Controls.Add(grfItems);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfItems, bc.iniC.themegrfIpd);
        }

        private void GrfItems_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfItems_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfSSOPimp()
        {
            grfSSOPimp = new C1FlexGrid();
            grfSSOPimp.Font = fEdit;
            grfSSOPimp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSSOPimp.Location = new System.Drawing.Point(0, 0);
            grfSSOPimp.Rows.Count = 1;
            grfSSOPimp.Cols.Count = 10;

            grfSSOPimp.Cols[colgrfSSOPimpVsDate].Width = 90;
            grfSSOPimp.Cols[colgrfSSOPimpPreno].Width = 60;
            grfSSOPimp.Cols[colgrfSSOPimpHn].Width = 100;
            grfSSOPimp.Cols[colgrfSSOPimpSymptoms].Width = 300;
            grfSSOPimp.Cols[colgrfSSOPimpName].Width = 150;
            grfSSOPimp.Cols[colgrfSSOPimpPaidName].Width = 380;
            grfSSOPimp.Cols[colgrfSSOPimpchk].Width = 80;

            grfSSOPimp.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfSSOPimp.Cols[colgrfSSOPimpchk].DataType = typeof(bool);
            grfSSOPimp.Cols[colgrfSSOPimpchk].Caption = "Select"; // ชื่อหัวคอลัมน์
            grfSSOPimp.Cols[colgrfSSOPimpchk].TextAlign = TextAlignEnum.LeftCenter;
            grfSSOPimp.Cols[colgrfSSOPimpVsDate].Caption = "visitdate";
            grfSSOPimp.Cols[colgrfSSOPimpPreno].Caption = "preno";
            grfSSOPimp.Cols[colgrfSSOPimpHn].Caption = "HN";
            //grfSSOP.Cols[colgrfSSOPchk].Visible = false;
            grfSSOPimp.Cols[colgrfSSOPimpVsDate].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpVsTime].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpPreno].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpHn].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpName].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpSymptoms].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpPaidName].AllowEditing = false;

            grfSSOPimp.Click += GrfSSOPimp_Click;
            grfSSOPimp.DoubleClick += GrfSSOPimp_DoubleClick;
            pnSSOPimp.Controls.Add(grfSSOPimp);
            grfSSOPimp.SelectionMode = SelectionModeEnum.Row;
            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfSSOPimp, bc.iniC.themegrfIpd);
        }

        private void GrfSSOPimp_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSSOPimp.Row < 1) return;
            if (!grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpchk].Equals("True"))
            {
                Row rowa = grfItems.Rows.Add();
                rowa[colgrfSSOPimpchk] = true;
                rowa[colgrfSSOPimpVsDate] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpVsDate].ToString();
                rowa[colgrfSSOPimpPreno] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpPreno].ToString();
                rowa[colgrfSSOPimpHn] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpHn].ToString();
                rowa[colgrfSSOPimpSymptoms] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpSymptoms].ToString();
                rowa[colgrfSSOPimpName] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpName].ToString();
                rowa[colgrfSSOPimpPaidName] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpPaidName].ToString();
                rowa[colgrfSSOPimppid] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimppid].ToString();
                rowa[colgrfSSOPimpVsTime] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpVsTime].ToString();
            }
        }

        private void GrfSSOPimp_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSSOPimp.Row < 1) return;
            String hn = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpHn] != null ? grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpHn].ToString():"";
            String preno = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpPreno] != null ? grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpPreno].ToString() : "";
            String vsdate = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpVsDate] != null ? grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpVsDate].ToString() : "";
            //setGrfSSOP(hn, preno, vsdate);
        }
        private void setGrfSSOPimp()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectPtt(txtSSOPhn.Text.Trim(), txtSSOPPaidcode.Text.Trim(), bc.datetoDBCultureInfo(txtSSOPDateStart.Text),bc.datetoDBCultureInfo(txtSSOPDateend.Text));
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfSSOPimp.Rows.Count = 1;
            grfSSOPimp.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSSOPimp.Rows[i];
                rowa[colgrfSSOPimpchk] = false;
                rowa[colgrfSSOPimpVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfSSOPimpPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfSSOPimpHn] = row1["mnc_hn_no"].ToString();
                rowa[colgrfSSOPimpSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfSSOPimpName] = row1["pttfullname"].ToString();
                rowa[colgrfSSOPimpPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfSSOPimppid] = row1["mnc_id_no"].ToString();
                rowa[colgrfSSOPimpVsTime] = bc.showTime( row1["MNC_TIME"].ToString());
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void TxtSupraNewHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setControlSupra(txtSupraNewHn.Text);
            }
        }
        private void setControlSupra(String hn)
        {
            lbPttAttachNote.Text = "";
            lbVN.Text = "";
            PTT = bc.bcDB.pttDB.selectPatinetByHn(hn);
            if (PTT == null) return;
            txtSupraNewHn.Value = PTT.MNC_HN_NO;
            lbSupraNewPttName.Text = PTT.Name;
            lbPttAge.Text = PTT.AgeStringOK1DOT();
            lbPttAttachNote.Text = PTT.MNC_ATT_NOTE;
            setGrfSupra(hn);
        }
        private void initGrfSupra()
        {
            grfSupra = new C1FlexGrid();
            grfSupra.Font = fEdit;
            grfSupra.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSupra.Location = new System.Drawing.Point(0, 0);
            grfSupra.Rows.Count = 1;
            grfSupra.Cols.Count = 11;

            grfSupra.Cols[colgrfSupraVsDate].Width = 90;
            grfSupra.Cols[colgrfSupraPreno].Width = 60;
            grfSupra.Cols[colgrfSupraSymptoms].Width = 200;
            grfSupra.Cols[colgrfSupraVNAN].Width = 100;
            grfSupra.Cols[colgrfSupraStatusAdmit].Width = 60;
            
            grfSupra.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfSupra.Cols[colgrfSupraVsDate].Caption = "visitdate";
            grfSupra.Cols[colgrfSupraPreno].Caption = "preno";
            grfSupra.Cols[colgrfSupraSymptoms].Caption = "Symptoms";
            grfSupra.Cols[colgrfSupraVNAN].Caption = "VNAN";
            grfSupra.Cols[colgrfSupraStatusAdmit].Caption = "status";

            grfSupra.Cols[colgrfSupraVsDate].AllowEditing = false;
            grfSupra.Cols[colgrfSupraPreno].AllowEditing = false;
            grfSupra.Cols[colgrfSupraSymptoms].AllowEditing = false;
            grfSupra.Cols[colgrfSupraVNAN].AllowEditing = false;
            grfSupra.Cols[colgrfSupraStatusAdmit].AllowEditing = false;

            grfSupra.AfterRowColChange += GrfSupra_AfterRowColChange;

            pnSupra.Controls.Add(grfSupra);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfSupra, bc.iniC.themegrfIpd);
        }

        private void GrfSupra_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfSupra(String hn)
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dtvs = bc.bcDB.vsDB.selectVisitByHn(hn);
            int i = 1, j = 1, row = grfSupra.Rows.Count;

            grfSupra.Rows.Count = 1;
            grfSupra.Rows.Count = dtvs.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSupra.Rows[i];

                rowa[colgrfSupraVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfSupraPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfSupraStatusAdmit] = row1["MNC_AN_NO"].ToString().Equals("0") ? "O" : "I";
                rowa[colgrfSupraSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfSupraVNAN] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["MNC_VN_NO"].ToString(): row1["MNC_AN_NO"].ToString();

                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initLoading()
        {
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
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
        private void FrmSSO_Load(object sender, EventArgs e)
        {
            this.Text = "last update 2025-06-25";
            scSupraNew.HeaderHeight = 0;
            spSSOPget.HeaderHeight = 0;
            c1SplitContainer1.HeaderHeight = 0;
            scGenText.HeaderHeight = 0;
            tcSSOP.SelectedTab = tabSSOPimp;
            tabSSOPDetails.SelectedTab = tabSSOPBillItems;
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
        }
    }
}