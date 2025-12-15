using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmLab : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient ptt;
        C1FlexGrid grfReq, grfResVs, grfResReq, grfRes, grfGrp, grfSubGrp, grfLab, grfLabRes;
        C1ThemeController theme1;
        Boolean pageLoad = false;
        Timer timeOperList;

        String PRENO = "", VSDATE = "", HN = "",DEPTNO = "", ERRLINE="", ANPRNLAB="", REQNOPRNLAB="", ANNO="", anCNT="", VNNO="";
        int colgrfReqReqNo=1,colgrfReqReqDate = 2, colgrfReqReqTime = 3,colgrfReqHn = 4, colgrfReqFullNameT = 5, colgrfReqAge = 6, colgrfReqSex=7, colgrfReqPaidName = 8, colgrfReqDtrNameT=9, colgrfReqDeptName=10, colgrfReqPreno = 11, colgrfReqVsDate = 12;
        int colgrfVsTime=1,colgrfVsHn = 2, colgrfVsFullNameT = 3, colgrfVsAge = 4, colgrfVsSex = 5, colgrfVsDeptName = 6, colgrfVsDtrNameT=7, colgrfVsPreno=8, colgrfVsVsdate=9, colgrfVsVnno=10, colgrfVsVsFlag=11;
        int colgrfReslabcode = 1, colgrfReslabnamet = 2, colgrfReslabsubnamet = 3, colgrfResValue = 4, colgrfResStd = 5, colgrfResUnit = 6;
        int colgrfGrpCode = 1, colgrfGrpName = 2, colgrfGrpSort1 = 3;
        int colgrfSubGrpCode = 1, colgrfSubGrpName = 2;
        int colgrfLablabcode = 1, colgrfLablabname = 2, colgrfLabGrpname = 3, colgrfLabSubGrpName = 4, colgrfLabSpcname = 5, colgrfLabPrice1 = 6, colgrfLabPrice2 = 7, colgrfLabPrice3 = 8, colgrfLabSchAct = 9, colgrfLabYearLimit = 10, colgrfLabSupervisor = 11;
        int colgrfLabRescode=1, colgrfLabResname=2, colgrfLabResMin=3, colgrfLabResMax=4, colgrfLabResgrpname=5, colgrfLabResUnitname=6, colgrfLabResValname = 7, colgrfLabResValDefault=8;
        int rowindexgrfResReq = 0, rowindexgrfGrp=0, rowindexgrfLabRes = 0;
        DataTable DTRES = new DataTable();

        Label lbLoading;
        public FrmLab(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            
            initControl();
            setEvent();

            pageLoad = false;
        }
        private void initControl()
        {
            initFont();
            theme1 = new C1ThemeController();
            ptt = new Patient();
            stt = new C1SuperTooltip();
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
            timeOperList = new Timer();
            timeOperList.Interval = bc.timerImgScanNew*1000*60;
            timeOperList.Enabled = false;
            initGrfReq();
            initGrfResVs();
            initGrfResReq();
            initGrfRes();
            initGrfGrp();
            setGrfGrp();
            initGrfSubGrp();
            initGrfLab();
            initGrfLlabRes();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String labcode = txtCode.Text;
            String labname = txtName.Text;
            String price1 = txtPrice1.Text;
            String price2 = txtPrice2.Text;
            String price3 = txtPrice3.Text;
            String limit = txtLimit.Text;
            String supervisor = chkSupervisor.Checked ? "1" : "0";
            String re = bc.bcDB.labM01DB.updateControlLab(labcode, limit, supervisor);
            if(int.Parse(re) > 0)
            {
                lfSbMessage.Text = "บันทึกข้อมูลสำเร็จ";
                String grpcode = grfGrp[grfGrp.Row, colgrfGrpCode].ToString();
                setGrfLab(grpcode);
            }
            else
            {
                lfSbMessage.Text = "บันทึกข้อมูลไม่สำเร็จ";
            }
        }

        private void clearControlMaster()
        {
            txtCode.Value = "";
            txtName.Value = "";
            txtPrice1.Value = "";
            txtPrice2.Value = "";
            txtPrice3.Value = "";
            txtLimit.Value = "";
            chkSupervisor.Checked = false;
        }
        private void setControlLab()
        {
            if (pageLoad) return;
            try
            {
                showLbLoading();
                clearControlMaster();
                txtCode.Value = grfLab[grfLab.Row, colgrfLablabcode].ToString();
                txtName.Value = grfLab[grfLab.Row, colgrfLablabname].ToString();
                txtPrice1.Value = grfLab[grfLab.Row, colgrfLabPrice1].ToString();
                txtPrice2.Value = grfLab[grfLab.Row, colgrfLabPrice2].ToString();
                txtPrice3.Value = grfLab[grfLab.Row, colgrfLabPrice3].ToString();
                txtLimit.Value = grfLab[grfLab.Row, colgrfLabYearLimit].ToString();
                chkSupervisor.Checked = grfLab[grfLab.Row, colgrfLabSupervisor].ToString().Equals("1") ? true : false;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmLab setControlLab " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmLab setControlLab  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }
        private void initFont()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 20, FontStyle.Bold);
        }
        private void setEvent()
        {
            timeOperList.Tick += TimeOperList_Tick;
            TCMain.SelectedTabChanged += TCMain_SelectedTabChanged;
            tabRes.SelectedTabChanged += TabRes_SelectedTabChanged;
            txtSBSearchDate.DropDownClosed += TxtSBSearchDate_DropDownClosed;
            btnSave.Click += BtnSave_Click;
            // Restrict txtLimit to numeric input only (digits and optional single decimal point)
            // txtLimit is a C1 input control in the designer; attach KeyPress and Leave handlers
            txtLimit.KeyPress += TxtLimit_KeyPress;
            txtLimit.Leave += TxtLimit_Leave;
        }
        // --- New handlers to lock txtLimit to numeric input ---
        private void TxtLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (backspace, delete, arrows), digits and one decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // If '.' entered, ensure there's not already a '.' in the text
            if (e.KeyChar == '.')
            {
                var ctl = sender as Control;
                string current = ctl?.Text ?? txtLimit.Value?.ToString() ?? "";
                if (current.Contains('.')) e.Handled = true;
            }
        }

        private void TxtLimit_Leave(object sender, EventArgs e)
        {
            // Sanitize pasted/entered text on leave: keep only digits and at most one decimal point
            var raw = txtLimit.Text?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(raw)) return;

            // Keep digits and dots
            var cleanedChars = raw.Where(c => char.IsDigit(c) || c == '.').ToArray();
            var cleaned = new string(cleanedChars);

            // If multiple dots, keep first
            int firstDot = cleaned.IndexOf('.');
            if (firstDot >= 0)
            {
                // remove subsequent dots
                var sb = new StringBuilder();
                bool dotSeen = false;
                foreach (var c in cleaned)
                {
                    if (c == '.')
                    {
                        if (!dotSeen) { sb.Append(c); dotSeen = true; }
                        // else skip
                    }
                    else sb.Append(c);
                }
                cleaned = sb.ToString();
            }

            // Final numeric validation: if not parseable, clear or keep digits-only
            if (!decimal.TryParse(cleaned, out _))
            {
                var digitsOnly = new string(cleaned.Where(char.IsDigit).ToArray());
                txtLimit.Value = digitsOnly;
            }
            else
            {
                txtLimit.Value = cleaned;
            }
        }
        // --- end new handlers ---
        private void TxtSBSearchDate_DropDownClosed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfResVs();
        }

        private void printLabResult(String reqdate, String reqno)
        {
            Patient ptt = new Patient();            DataTable dtRes = new DataTable();            DataTable dtReq = new DataTable();            String[] deptno = bc.iniC.station.Split(',');
            ERRLINE = "";            ERRLINE = "01";            String depname = "";
            dtRes = bc.bcDB.labT05DB.selectResultbyReqNo(reqdate, reqno);
            if (dtRes.Rows.Count > 0)
            {
                ERRLINE = "02";
                ptt = bc.bcDB.pttDB.selectPatinetByHn(HN);
                dtRes.Columns.Add("patient_name", typeof(String));                  dtRes.Columns.Add("patient_hn", typeof(String));
                dtRes.Columns.Add("patient_age", typeof(String));                   dtRes.Columns.Add("request_no", typeof(String));
                dtRes.Columns.Add("patient_vn", typeof(String));                    dtRes.Columns.Add("doctor", typeof(String));
                dtRes.Columns.Add("result_date", typeof(String));                   dtRes.Columns.Add("print_date", typeof(String));
                dtRes.Columns.Add("patient_dep", typeof(String));                   dtRes.Columns.Add("patient_company", typeof(String));
                dtRes.Columns.Add("patient_type", typeof(String));                  dtRes.Columns.Add("sort1", typeof(String));
                ERRLINE = "03";
                foreach (DataRow drow in dtRes.Rows)
                {
                    drow["patient_age"] = ptt.AgeStringOK1();
                    depname = dtRes.Rows[0]["MNC_REQ_DEP"].ToString();
                    drow["patient_name"] = ptt.Name;
                    drow["patient_hn"] = ptt.Hn;
                    drow["patient_company"] = "";           //ไม่ต้องพิมพ์ ชื่อบริษัท ลองดู
                    ERRLINE = "031";
                    drow["patient_vn"] = drow["MNC_AN_NO"].ToString().Equals("0") ? VNNO : drow["MNC_AN_NO"].ToString() + "." + drow["MNC_AN_YR"].ToString();
                    ERRLINE = "0310";
                    //new LogWriter("d", "FrmLab printLabResult hn " + HN + " reqdate " + reqdate + " REQNOPRNLAB " + reqno);
                    
                    //new LogWriter("d", "FrmLab printLabResult drow['mnc_lb_res'] " + drow["mnc_lb_res"] + " drow['mnc_lb_res'].ToString() " + drow["mnc_lb_res"].ToString());
                    ERRLINE = "032";
                    drow["patient_type"] = dtRes.Rows[0]["MNC_FN_TYP_DSC"].ToString();
                    drow["request_no"] = drow["MNC_REQ_NO"].ToString() + "/" + bc.datetoShow(drow["mnc_req_dat"].ToString());
                    ERRLINE = "033";
                    drow["doctor"] = dtRes.Rows[0]["dtr_name"].ToString() + "[" + dtRes.Rows[0]["mnc_dot_cd"].ToString() + "]";
                    ERRLINE = "04";
                    drow["result_date"] = bc.datetoShow(dtRes.Rows[0]["mnc_req_dat"].ToString());
                    drow["print_date"] = bc.datetoShow(dtRes.Rows[0]["MNC_STAMP_DAT"].ToString()) + " " + bc.FormatTime(dtRes.Rows[0]["MNC_RESULT_TIM"].ToString());
                    drow["user_lab"] = drow["user_lab"].ToString() + " [ทน." + drow["MNC_USR_NAME_result"].ToString() + "]";            //ซ้าย
                    drow["user_report"] = drow["user_report"].ToString() + " [ทน." + drow["MNC_USR_NAME_report"].ToString() + "]";      //กลาง
                    drow["user_check"] = drow["user_check"].ToString() + " [ทน." + drow["MNC_USR_NAME_approve"].ToString() + "]";       //ขวา
                    drow["patient_dep"] = depname.Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                        depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
                    ERRLINE = "05";
                    drow["mnc_lb_dsc"] = drow["MNC_LB_DSC"].ToString();
                    drow["mnc_lb_grp_cd"] = drow["MNC_LB_TYP_DSC"].ToString();
                    drow["hostname"] = bc.iniC.hostname;
                    //if (drow["MNC_RES_VALUE"].ToString().Equals("-")) drow["MNC_RES_UNT"] = "";
                    drow["MNC_RES_UNT"] = drow["MNC_RES_UNT"].ToString().Replace("0.00-0.00", "").Replace("0.00 - 0.00", "").Replace("0.00", "");
                    drow["sort1"] = drow["mnc_req_dat"].ToString().Replace("-", "").Replace("-", "") + drow["MNC_REQ_NO"].ToString();
                    //drow["MNC_RES"]=ชื่อlabลูก
                    //drow["mnc_lb_res"]REFERRENCE RANGE
                    //drow["mnc_lb_res"] = drow["mnc_lb_res"].ToString().Replace(drow["MNC_RES"].ToString(), "");      //lab ต้องการให้แสดงค่าตัวเลข
                    drow["mnc_lb_res"] = bc.fixLabRef(drow["MNC_LB_CD"].ToString(), drow["MNC_RES"].ToString().Trim(), drow["mnc_lb_res"].ToString().Trim(), drow["MNC_RES"].ToString());      // REFERRENCE RANGE
                    drow["MNC_RES_UNT"] = bc.fixLabUnit(drow["MNC_LB_CD"].ToString(), drow["MNC_RES"].ToString().Trim(), drow["MNC_RES_UNT"].ToString().Trim(), drow["mnc_lb_res"].ToString());
                    if (drow["mnc_lb_res"].Equals("0 - 0"))
                    {
                        drow["mnc_lb_res"] = drow["MNC_RES_UNT"].ToString();
                        drow["MNC_RES_UNT"] = "";
                    }
                    
                    ERRLINE = "06";
                }
                DTRES = dtRes;
                
                System.IO.FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\lab_result_4.rdlx");
                PageReport definition = new PageReport(rptPath);
                PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);
                runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);
                runtime.LocateDataSource += Runtime_LocateDataSource;
                arvMain.LoadDocument(runtime);
                pnResRpt.Show();
                new LogWriter("d", "FrmScreenCapture printLabResult  " + ptt.Hn + " an " + ANPRNLAB);
            }
                
            dtRes.Dispose();
            dtReq.Dispose();
        }

        private void Runtime_LocateDataSource(object sender, LocateDataSourceEventArgs args)
        {
            //throw new NotImplementedException();
            args.Data = DTRES;
        }

        private void TabRes_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if (tabRes.SelectedTab == tabResReport) { setGrfResVs();  /*  tabResReport รายงานผล */}
        }

        private void TCMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (TCMain.SelectedTab == tabResult) { setGrfResVs();  /*  tabresult */}
        }

        private void TimeOperList_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfReqLab();          //tabReq
        }

        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt; Application.DoEvents();
        }
        private void showLbLoading()
        {
            lbLoading.Show(); lbLoading.BringToFront(); Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide(); Application.DoEvents();
        }
        private void initGrfLlabRes()
        {
            grfLabRes = new C1FlexGrid();
            grfLabRes.Font = fEdit;
            grfLabRes.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLabRes.Location = new System.Drawing.Point(0, 0);
            grfLabRes.Rows.Count = 1;
            grfLabRes.Cols.Count = 13;
            grfLabRes.Cols[colgrfLabRescode].Width = 70;
            grfLabRes.Cols[colgrfLabResname].Width = 250;
            grfLabRes.Cols[colgrfLabResMin].Width = 80;
            grfLabRes.Cols[colgrfLabResMax].Width = 80;
            grfLabRes.Cols[colgrfLabResgrpname].Width = 150;
            grfLabRes.Cols[colgrfLabResUnitname].Width = 150;
            grfLabRes.Cols[colgrfLabResValname].Width = 150;
            grfLabRes.Cols[colgrfLabResValDefault].Width = 150;

            grfLabRes.ShowCursor = true;
            grfLabRes.Cols[colgrfLabRescode].Caption = "code";
            grfLabRes.Cols[colgrfLabResname].Caption = "Res Name";
            grfLabRes.Cols[colgrfLabResMin].Caption = "MIN";
            grfLabRes.Cols[colgrfLabResMax].Caption = "MAX";
            grfLabRes.Cols[colgrfLabResgrpname].Caption = "res grp";
            grfLabRes.Cols[colgrfLabResUnitname].Caption = "unit";
            grfLabRes.Cols[colgrfLabResValname].Caption = "type value";
            grfLabRes.Cols[colgrfLabResValDefault].Caption = "default value";

            grfLabRes.Cols[colgrfLabRescode].DataType = typeof(String);
            grfLabRes.Cols[colgrfLabResname].DataType = typeof(String);
            grfLabRes.Cols[colgrfLabResMin].DataType = typeof(String);
            grfLabRes.Cols[colgrfLabResMax].DataType = typeof(String);
            grfLabRes.Cols[colgrfLabResgrpname].DataType = typeof(String);
            grfLabRes.Cols[colgrfLabResUnitname].DataType = typeof(String);
            grfLabRes.Cols[colgrfLabResValname].DataType = typeof(String);
            grfLabRes.Cols[colgrfLabResValDefault].DataType = typeof(String);

            grfLabRes.Cols[colgrfLabRescode].TextAlign = TextAlignEnum.CenterCenter;
            grfLabRes.Cols[colgrfLabResname].TextAlign = TextAlignEnum.LeftCenter;
            grfLabRes.Cols[colgrfLabResMin].TextAlign = TextAlignEnum.LeftCenter;
            grfLabRes.Cols[colgrfLabResMax].TextAlign = TextAlignEnum.LeftCenter;
            grfLabRes.Cols[colgrfLabResgrpname].TextAlign = TextAlignEnum.LeftCenter;
            grfLabRes.Cols[colgrfLabResUnitname].TextAlign = TextAlignEnum.LeftCenter;
            grfLabRes.Cols[colgrfLabResValname].TextAlign = TextAlignEnum.LeftCenter;
            grfLabRes.Cols[colgrfLabResValDefault].TextAlign = TextAlignEnum.LeftCenter;

            //grfGrp.Cols[colgrfReqVsDate].Visible = false;

            grfLabRes.Cols[colgrfLabRescode].AllowEditing = false;
            grfLabRes.Cols[colgrfLabResname].AllowEditing = false;
            grfLabRes.Cols[colgrfLabResMin].AllowEditing = false;
            grfLabRes.Cols[colgrfLabResMax].AllowEditing = false;
            grfLabRes.Cols[colgrfLabResgrpname].AllowEditing = false;
            grfLabRes.Cols[colgrfLabResUnitname].AllowEditing = false;
            grfLabRes.Cols[colgrfLabResValname].AllowEditing = false;
            grfLabRes.Cols[colgrfLabResValDefault].AllowEditing = false;

            pnLabResList.Controls.Add(grfLabRes);
            theme1.SetTheme(grfLabRes, bc.iniC.themeApp);
        }
        private void setGrfLabRes(String labcode)
        {
            //if (pageLoad) return;

            pageLoad = true;
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.labM04DB.SelectAllBylabcode(labcode);
            grfLabRes.Rows.Count = 1;
            grfLabRes.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfLabRes.Rows[i];
                rowa[colgrfLabRescode] = row1["MNC_LB_RES_CD"].ToString();
                rowa[colgrfLabResname] = row1["MNC_LB_RES"].ToString();
                rowa[colgrfLabResMin] = row1["MNC_LB_RES_MIN"].ToString();
                rowa[colgrfLabResMax] = row1["MNC_LB_RES_MAX"].ToString();
                rowa[colgrfLabResgrpname] = row1["MNC_RES_GRP_DSC"].ToString();

                rowa[colgrfLabResUnitname] = row1["MNC_RES_UNT"].ToString();
                //rowa[colgrfLabResValname] = row1["MNC_RES_VALUE"].ToString();
                rowa[colgrfLabResValDefault] = row1["MNC_KW_DSC"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            pageLoad = false;
        }
        private void initGrfLab()
        {
            grfLab = new C1FlexGrid();
            grfLab.Font = fEdit;
            grfLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLab.Location = new System.Drawing.Point(0, 0);
            grfLab.Rows.Count = 1;
            grfLab.Cols.Count = 13;
            grfLab.Cols[colgrfLablabcode].Width = 70;
            grfLab.Cols[colgrfLablabname].Width = 250;
            grfLab.Cols[colgrfLabGrpname].Width = 50;
            grfLab.Cols[colgrfLabSubGrpName].Width = 50;
            grfLab.Cols[colgrfLabSpcname].Width = 50;
            grfLab.Cols[colgrfLabPrice1].Width = 50;
            grfLab.Cols[colgrfLabPrice2].Width = 50;
            grfLab.Cols[colgrfLabPrice3].Width = 50;
            grfLab.Cols[colgrfLabSchAct].Width = 80;

            grfLab.ShowCursor = true;
            grfLab.Cols[colgrfLablabcode].Caption = "code";
            grfLab.Cols[colgrfLablabname].Caption = "Name";
            grfLab.Cols[colgrfLabGrpname].Caption = "group";
            grfLab.Cols[colgrfLabSubGrpName].Caption = "sub group";
            grfLab.Cols[colgrfLabSpcname].Caption = "specimen";
            grfLab.Cols[colgrfLabPrice1].Caption = "price1";
            grfLab.Cols[colgrfLabPrice2].Caption = "price2";
            grfLab.Cols[colgrfLabPrice3].Caption = "price3";
            grfLab.Cols[colgrfLabSchAct].Caption = "ระยะเวลา";

            grfLab.Cols[colgrfLablabcode].DataType = typeof(String);
            grfLab.Cols[colgrfLablabname].DataType = typeof(String);
            grfLab.Cols[colgrfLabGrpname].DataType = typeof(String);
            grfLab.Cols[colgrfLabSubGrpName].DataType = typeof(String);
            grfLab.Cols[colgrfLabSpcname].DataType = typeof(String);
            grfLab.Cols[colgrfLabPrice1].DataType = typeof(String);
            grfLab.Cols[colgrfLabPrice2].DataType = typeof(String);
            grfLab.Cols[colgrfLabPrice3].DataType = typeof(String);
            grfLab.Cols[colgrfLabSchAct].DataType = typeof(String);

            grfLab.Cols[colgrfLablabcode].TextAlign = TextAlignEnum.CenterCenter;
            grfLab.Cols[colgrfLablabname].TextAlign = TextAlignEnum.LeftCenter;
            grfLab.Cols[colgrfLabGrpname].TextAlign = TextAlignEnum.CenterCenter;
            grfLab.Cols[colgrfLabSubGrpName].TextAlign = TextAlignEnum.CenterCenter;
            grfLab.Cols[colgrfLabSpcname].TextAlign = TextAlignEnum.CenterCenter;
            grfLab.Cols[colgrfLabPrice1].TextAlign = TextAlignEnum.CenterCenter;
            grfLab.Cols[colgrfLabPrice2].TextAlign = TextAlignEnum.CenterCenter;
            grfLab.Cols[colgrfLabPrice3].TextAlign = TextAlignEnum.CenterCenter;
            grfLab.Cols[colgrfLabSchAct].TextAlign = TextAlignEnum.CenterCenter;

            //grfGrp.Cols[colgrfReqVsDate].Visible = false;

            grfLab.Cols[colgrfLablabcode].AllowEditing = false;
            grfLab.Cols[colgrfLablabname].AllowEditing = false;
            grfLab.Cols[colgrfLabGrpname].AllowEditing = false;
            grfLab.Cols[colgrfLabSubGrpName].AllowEditing = false;
            grfLab.Cols[colgrfLabSpcname].AllowEditing = false;
            grfLab.Cols[colgrfLabPrice1].AllowEditing = false;
            grfLab.Cols[colgrfLabPrice2].AllowEditing = false;
            grfLab.Cols[colgrfLabPrice3].AllowEditing = false;
            grfLab.Cols[colgrfLabSchAct].AllowEditing = false;

            grfLab.AfterRowColChange += GrfLab_AfterRowColChange;

            pnLabList.Controls.Add(grfLab);
            theme1.SetTheme(grfLab, bc.iniC.themeApp);
        }

        private void GrfLab_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                if (rowindexgrfLabRes != ((C1FlexGrid)(sender)).Row) { rowindexgrfLabRes = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                String labcode = grfLab[grfLab.Row, colgrfLablabcode].ToString();
                setGrfLabRes(labcode);
                setControlLab();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmLab GrfLab_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmLab GrfLab_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }

        private void setGrfLab(String grpcode)
        {
            //if (pageLoad) return;

            pageLoad = true;
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.labM01DB.SelectAllByGroup(grpcode);
            grfLab.Rows.Count = 1;
            grfLab.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfLab.Rows[i];
                rowa[colgrfLablabcode] = row1["MNC_LB_CD"].ToString();
                rowa[colgrfLablabname] = row1["MNC_LB_DSC"].ToString();
                rowa[colgrfLabGrpname] = "";
                rowa[colgrfLabSubGrpName] = "";
                rowa[colgrfLabSpcname] = row1["MNC_SPC_DSC"].ToString();

                rowa[colgrfLabPrice1] = row1["mnc_lb_pri01"].ToString();
                rowa[colgrfLabPrice2] = row1["mnc_lb_pri02"].ToString();
                rowa[colgrfLabPrice3] = row1["mnc_lb_pri03"].ToString();
                rowa[colgrfLabSchAct] = row1["MNC_SCH_ACT"].ToString();
                rowa[colgrfLabYearLimit] = row1["control_year"].ToString();
                rowa[colgrfLabSupervisor] = row1["control_supervisor"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            pageLoad = false;
        }
        private void initGrfSubGrp()
        {
            grfSubGrp = new C1FlexGrid();
            grfSubGrp.Font = fEdit;
            grfSubGrp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSubGrp.Location = new System.Drawing.Point(0, 0);
            grfSubGrp.Rows.Count = 1;
            grfSubGrp.Cols.Count = 13;
            grfSubGrp.Cols[colgrfSubGrpCode].Width = 70;
            grfSubGrp.Cols[colgrfSubGrpName].Width = 250;

            grfSubGrp.ShowCursor = true;
            grfSubGrp.Cols[colgrfSubGrpCode].Caption = "code";
            grfSubGrp.Cols[colgrfSubGrpName].Caption = "SubName";
            
            grfSubGrp.Cols[colgrfSubGrpCode].DataType = typeof(String);
            grfSubGrp.Cols[colgrfSubGrpName].DataType = typeof(String);
            
            grfSubGrp.Cols[colgrfSubGrpCode].TextAlign = TextAlignEnum.CenterCenter;
            grfSubGrp.Cols[colgrfSubGrpName].TextAlign = TextAlignEnum.LeftCenter;
            
            //grfGrp.Cols[colgrfReqVsDate].Visible = false;

            grfSubGrp.Cols[colgrfSubGrpCode].AllowEditing = false;
            grfSubGrp.Cols[colgrfSubGrpName].AllowEditing = false;
            
            pnSubGrpLists.Controls.Add(grfSubGrp);
            theme1.SetTheme(grfSubGrp, bc.iniC.themeApp);
        }
        private void setGrfSubGrp(String grpcode)
        {
            //if (pageLoad) return;

            pageLoad = true;
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.labm07DB.selectByGrpcode(grpcode);
            grfSubGrp.Rows.Count = 1;
            grfSubGrp.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfSubGrp.Rows[i];
                rowa[colgrfSubGrpCode] = row1["MNC_LB_TYP_CD"].ToString();
                rowa[colgrfSubGrpName] = row1["MNC_LB_TYP_DSC"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            pageLoad = false;
        }
        private void initGrfGrp()
        {
            grfGrp = new C1FlexGrid();
            grfGrp.Font = fEdit;
            grfGrp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfGrp.Location = new System.Drawing.Point(0, 0);
            grfGrp.Rows.Count = 1;
            grfGrp.Cols.Count = 13;
            grfGrp.Cols[colgrfGrpCode].Width = 70;
            grfGrp.Cols[colgrfGrpName].Width = 250;
            grfGrp.Cols[colgrfGrpSort1].Width = 50;
            
            grfGrp.ShowCursor = true;
            grfGrp.Cols[colgrfGrpCode].Caption = "code";
            grfGrp.Cols[colgrfGrpName].Caption = "Name";
            grfGrp.Cols[colgrfGrpSort1].Caption = "sort";            

            grfGrp.Cols[colgrfGrpCode].DataType = typeof(String);
            grfGrp.Cols[colgrfGrpName].DataType = typeof(String);
            grfGrp.Cols[colgrfGrpSort1].DataType = typeof(String);
            
            grfGrp.Cols[colgrfGrpCode].TextAlign = TextAlignEnum.CenterCenter;
            grfGrp.Cols[colgrfGrpName].TextAlign = TextAlignEnum.LeftCenter;
            grfGrp.Cols[colgrfGrpSort1].TextAlign = TextAlignEnum.CenterCenter;

            //grfGrp.Cols[colgrfReqVsDate].Visible = false;

            grfGrp.Cols[colgrfGrpCode].AllowEditing = false;
            grfGrp.Cols[colgrfGrpName].AllowEditing = false;
            grfGrp.Cols[colgrfGrpSort1].AllowEditing = false;
            grfGrp.AfterRowColChange += GrfGrp_AfterRowColChange;

            pnGrpList.Controls.Add(grfGrp);
            theme1.SetTheme(grfGrp, bc.iniC.themeApp);
        }
        private void GrfGrp_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                if (rowindexgrfGrp != ((C1FlexGrid)(sender)).Row) { rowindexgrfGrp = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                String grpcode = grfGrp[grfGrp.Row, colgrfGrpCode].ToString();
                setGrfSubGrp(grpcode);
                setGrfLab(grpcode);
                clearControlMaster();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmLab GrfGrp_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmLab GrfGrp_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }
        private void setGrfGrp()
        {
            //if (pageLoad) return;

            pageLoad = true;
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.labm06DB.selectAll();
            grfGrp.Rows.Count = 1;
            grfGrp.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfGrp.Rows[i];
                rowa[colgrfGrpCode] = row1["MNC_LB_GRP_CD"].ToString();
                rowa[colgrfGrpName] = row1["MNC_LB_GRP_DSC"].ToString();
                rowa[colgrfGrpSort1] = row1["MNC_ORDER"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            pageLoad = false;
        }
        private void initGrfRes()
        {
            grfRes = new C1FlexGrid();
            grfRes.Font = fEdit;
            grfRes.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRes.Location = new System.Drawing.Point(0, 0);
            grfRes.Rows.Count = 1;
            grfRes.Cols.Count = 7;
            grfRes.Cols[colgrfReslabcode].Width = 70;
            grfRes.Cols[colgrfReslabnamet].Width = 120;
            grfRes.Cols[colgrfReslabsubnamet].Width = 120;
            grfRes.Cols[colgrfResValue].Width = 120;
            grfRes.Cols[colgrfResStd].Width = 120;
            grfRes.Cols[colgrfResUnit].Width = 60;

            grfRes.ShowCursor = true;
            grfRes.Cols[colgrfReslabcode].Caption = "code";
            grfRes.Cols[colgrfReslabnamet].Caption = "Name";
            grfRes.Cols[colgrfReslabsubnamet].Caption = "name";
            grfRes.Cols[colgrfResValue].Caption = "value";
            grfRes.Cols[colgrfResStd].Caption = "std";
            grfRes.Cols[colgrfResUnit].Caption = "unit";

            grfRes.Cols[colgrfReslabcode].DataType = typeof(String);
            grfRes.Cols[colgrfReslabnamet].DataType = typeof(String);
            grfRes.Cols[colgrfReslabsubnamet].DataType = typeof(String);
            grfRes.Cols[colgrfResValue].DataType = typeof(String);
            grfRes.Cols[colgrfResStd].DataType = typeof(String);
            grfRes.Cols[colgrfResUnit].DataType = typeof(String);

            grfRes.Cols[colgrfReslabcode].TextAlign = TextAlignEnum.CenterCenter;
            grfRes.Cols[colgrfReslabnamet].TextAlign = TextAlignEnum.LeftCenter;
            grfRes.Cols[colgrfReslabsubnamet].TextAlign = TextAlignEnum.LeftCenter;
            grfRes.Cols[colgrfResValue].TextAlign = TextAlignEnum.LeftCenter;
            grfRes.Cols[colgrfResStd].TextAlign = TextAlignEnum.CenterCenter;
            grfRes.Cols[colgrfResUnit].TextAlign = TextAlignEnum.CenterCenter;

            //grfRes.Cols[colgrfVsPreno].Visible = false;

            grfRes.Cols[colgrfReslabcode].AllowEditing = false;
            grfRes.Cols[colgrfReslabnamet].AllowEditing = false;
            grfRes.Cols[colgrfReslabsubnamet].AllowEditing = false;
            grfRes.Cols[colgrfResStd].AllowEditing = false;
            grfRes.Cols[colgrfResUnit].AllowEditing = false;
            grfRes.Cols[colgrfResValue].AllowEditing = false;

            grfRes.AfterRowColChange += GrfRes_AfterRowColChange;
            //grfCheckUPList.AllowFiltering = true;

            pnRes.Controls.Add(grfRes);
            theme1.SetTheme(grfRes, bc.iniC.themeApp);
        }

        private void GrfRes_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            //if (e.NewRange.r1 < 0) return;

            //if (e.NewRange.Data == null) return;
            //if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            //try
            //{
            //    if (rowindexgrfResReq != ((C1FlexGrid)(sender)).Row) { rowindexgrfResReq = ((C1FlexGrid)(sender)).Row; }
            //    else { return; }
            //    showLbLoading();
            //    HN = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfResv] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqHn].ToString() : "";
            //    String reqno = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqReqNo] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqReqNo].ToString() : "";
            //    String reqdate = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqReqDate] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqReqDate].ToString() : "";
            //    setGrfRes(reqdate, reqno);
            //    printLabResult(reqdate, reqno);
            //}
            //catch (Exception ex)
            //{
            //    new LogWriter("e", "FrmLab GrfResReq_AfterRowColChange " + ex.Message);
            //    bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmLab GrfResReq_AfterRowColChange  ", ex.Message);
            //    lfSbMessage.Text = ex.Message;
            //}
            //finally
            //{
            //    //frmFlash.Dispose();
            //    hideLbLoading();
            //}
        }
        private void setGrfRes(String reqdate, String reqno)
        {
            //if (pageLoad) return;

            //pageLoad = true;
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.labT05DB.selectResultbyReqNo(reqdate, reqno);
            grfRes.Rows.Count = 1;
            grfRes.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfRes.Rows[i];
                rowa[colgrfReslabcode] = row1["MNC_LB_CD"].ToString();
                rowa[colgrfReslabnamet] = row1["mnc_lb_dsc"].ToString();
                rowa[colgrfReslabsubnamet] = row1["MNC_RES"].ToString();
                rowa[colgrfResValue] = row1["MNC_RES_VALUE"].ToString();
                rowa[colgrfResStd] = row1["mnc_lb_res"].ToString();
                rowa[colgrfResUnit] = row1["MNC_RES_UNT"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            //pageLoad = false;
        }
        private void initGrfResReq()
        {
            grfResReq = new C1FlexGrid();
            grfResReq.Font = fEdit;
            grfResReq.Dock = System.Windows.Forms.DockStyle.Fill;
            grfResReq.Location = new System.Drawing.Point(0, 0);
            grfResReq.Rows.Count = 1;
            grfResReq.Cols.Count = 13;
            grfResReq.Cols[colgrfReqReqNo].Width = 60;
            grfResReq.Cols[colgrfReqReqTime].Width = 70;
            grfResReq.Cols[colgrfReqHn].Width = 80;
            grfResReq.Cols[colgrfReqFullNameT].Width = 250;
            grfResReq.Cols[colgrfReqAge].Width = 70;
            grfResReq.Cols[colgrfReqSex].Width = 60;
            grfResReq.Cols[colgrfReqPaidName].Width = 150;
            grfResReq.Cols[colgrfReqDtrNameT].Width = 200;
            grfResReq.Cols[colgrfReqDeptName].Width = 120;
            grfResReq.ShowCursor = true;
            grfResReq.Cols[colgrfReqReqNo].Caption = "no";
            grfResReq.Cols[colgrfReqReqTime].Caption = "time";
            grfResReq.Cols[colgrfReqHn].Caption = "HN";
            grfResReq.Cols[colgrfReqFullNameT].Caption = "ชื่อ-นามสกุล";
            grfResReq.Cols[colgrfReqAge].Caption = "age";
            grfResReq.Cols[colgrfReqSex].Caption = "sex";
            grfResReq.Cols[colgrfReqPaidName].Caption = "สิทธิ";
            grfResReq.Cols[colgrfReqDtrNameT].Caption = "แพทย์";
            grfResReq.Cols[colgrfReqDeptName].Caption = "dept";

            grfResReq.Cols[colgrfReqReqNo].DataType = typeof(String);
            grfResReq.Cols[colgrfReqReqTime].DataType = typeof(String);
            grfResReq.Cols[colgrfReqHn].DataType = typeof(String);
            grfResReq.Cols[colgrfReqFullNameT].DataType = typeof(String);
            grfResReq.Cols[colgrfReqAge].DataType = typeof(String);
            grfResReq.Cols[colgrfReqSex].DataType = typeof(String);
            grfResReq.Cols[colgrfReqPaidName].DataType = typeof(String);
            grfResReq.Cols[colgrfReqDtrNameT].DataType = typeof(String);
            grfResReq.Cols[colgrfReqDeptName].DataType = typeof(String);

            grfResReq.Cols[colgrfReqReqNo].TextAlign = TextAlignEnum.CenterCenter;
            grfResReq.Cols[colgrfReqReqTime].TextAlign = TextAlignEnum.CenterCenter;
            grfResReq.Cols[colgrfReqHn].TextAlign = TextAlignEnum.LeftCenter;
            grfResReq.Cols[colgrfReqFullNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfResReq.Cols[colgrfReqAge].TextAlign = TextAlignEnum.CenterCenter;
            grfResReq.Cols[colgrfReqSex].TextAlign = TextAlignEnum.CenterCenter;
            grfResReq.Cols[colgrfReqPaidName].TextAlign = TextAlignEnum.LeftCenter;
            grfResReq.Cols[colgrfReqDtrNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfResReq.Cols[colgrfReqDeptName].TextAlign = TextAlignEnum.LeftCenter;

            grfResReq.Cols[colgrfReqVsDate].Visible = false;
            grfResReq.Cols[colgrfReqPreno].Visible = false;
            grfResReq.Cols[colgrfReqReqDate].Visible = false;

            grfResReq.Cols[colgrfReqReqNo].AllowEditing = false;
            grfResReq.Cols[colgrfReqReqTime].AllowEditing = false;
            grfResReq.Cols[colgrfReqHn].AllowEditing = false;
            grfResReq.Cols[colgrfReqFullNameT].AllowEditing = false;
            grfResReq.Cols[colgrfReqAge].AllowEditing = false;
            grfResReq.Cols[colgrfReqSex].AllowEditing = false;
            grfResReq.Cols[colgrfReqPaidName].AllowEditing = false;
            grfResReq.Cols[colgrfReqDtrNameT].AllowEditing = false;
            grfResReq.Cols[colgrfReqDeptName].AllowEditing = false;
            grfResReq.SelectionMode = SelectionModeEnum.Cell;
            grfResReq.AfterRowColChange += GrfResReq_AfterRowColChange;
            //grfCheckUPList.AllowFiltering = true;

            panel3.Controls.Add(grfResReq);
            theme1.SetTheme(grfResReq, bc.iniC.themeApp);
        }
        private void GrfResReq_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                if (rowindexgrfResReq != ((C1FlexGrid)(sender)).Row) { rowindexgrfResReq = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                showLbLoading();
                HN = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqHn] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqHn].ToString() : "";
                String reqno = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqReqNo] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqReqNo].ToString() : "";
                String reqdate = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqReqDate] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqReqDate].ToString() : "";
                //VNNO = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReq] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfReqHn].ToString() : "";
                setGrfRes(reqdate, reqno);
                printLabResult(reqdate, reqno);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmLab GrfResReq_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmLab GrfResReq_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }
        private void setGrfResReqLab(String hn, String vsdate, String preno, String anno, String flagOPD)
        {
            //if (pageLoad) return;
            //pageLoad = true;
            timeOperList.Stop();
            DataTable dtvs = new DataTable();
            if (flagOPD.Equals("O"))
            {
                dtvs = bc.bcDB.labT01DB.selectReqOPDByDate(hn, vsdate, preno);
            }
            else
            {
                String[] an = anno.Split('.');
                dtvs = bc.bcDB.labT01DB.selectReqIPDByDate(hn, an[0], an[1]);
            }
            
            grfResReq.Rows.Count = 1;
            grfResReq.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfResReq.Rows[i];
                rowa[colgrfReqReqTime] = bc.showTime(row1["MNC_REQ_TIM"].ToString());
                rowa[colgrfReqHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfReqReqNo] = row1["MNC_REQ_NO"].ToString();
                rowa[colgrfReqFullNameT] = row1["pttfullnamet"].ToString();
                rowa[colgrfReqReqDate] = row1["MNC_REQ_DAT"].ToString();
                rowa[colgrfReqDeptName] = bc.bcDB.pm32DB.getDeptNameOPD1(bc.showTime(row1["MNC_REQ_DEP"].ToString()));
                rowa[colgrfReqDtrNameT] = row1["MNC_USR_FULL"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            timeOperList.Start();
            //pageLoad = false;
        }
        private void initGrfResVs()
        {
            grfResVs = new C1FlexGrid();
            grfResVs.Font = fEdit;
            grfResVs.Dock = System.Windows.Forms.DockStyle.Fill;
            grfResVs.Location = new System.Drawing.Point(0, 0);
            grfResVs.Rows.Count = 1;
            grfResVs.Cols.Count = 12;
            grfResVs.Cols[colgrfVsTime].Width = 60;
            grfResVs.Cols[colgrfVsHn].Width = 80;
            grfResVs.Cols[colgrfVsFullNameT].Width = 250;
            grfResVs.Cols[colgrfVsAge].Width = 70;
            grfResVs.Cols[colgrfVsSex].Width = 60;
            grfResVs.Cols[colgrfVsDeptName].Width = 120;
            grfResVs.Cols[colgrfVsDtrNameT].Width = 150;
            grfResVs.Cols[colgrfVsVsFlag].Width = 30;
            grfResVs.Cols[colgrfVsVnno].Width = 70;

            grfResVs.ShowCursor = true;
            grfResVs.Cols[colgrfVsTime].Caption = "time1";
            grfResVs.Cols[colgrfVsHn].Caption = "HN";
            grfResVs.Cols[colgrfVsFullNameT].Caption = "ชื่อ-นามสกุล";
            grfResVs.Cols[colgrfVsAge].Caption = "age";
            grfResVs.Cols[colgrfVsSex].Caption = "sex";
            grfResVs.Cols[colgrfVsDeptName].Caption = "dept";
            grfResVs.Cols[colgrfVsDtrNameT].Caption = "dtrname";
            grfResVs.Cols[colgrfVsVsFlag].Caption = "";
            grfResVs.Cols[colgrfVsVnno].Caption = "VN/AN";

            grfResVs.Cols[colgrfVsTime].DataType = typeof(String);
            grfResVs.Cols[colgrfVsHn].DataType = typeof(String);
            grfResVs.Cols[colgrfVsFullNameT].DataType = typeof(String);
            grfResVs.Cols[colgrfVsAge].DataType = typeof(String);
            grfResVs.Cols[colgrfVsSex].DataType = typeof(String);
            grfResVs.Cols[colgrfVsDeptName].DataType = typeof(String);
            grfResVs.Cols[colgrfVsDtrNameT].DataType = typeof(String);

            grfResVs.Cols[colgrfVsTime].TextAlign = TextAlignEnum.CenterCenter;
            grfResVs.Cols[colgrfVsHn].TextAlign = TextAlignEnum.LeftCenter;
            grfResVs.Cols[colgrfVsFullNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfResVs.Cols[colgrfVsAge].TextAlign = TextAlignEnum.CenterCenter;
            grfResVs.Cols[colgrfVsSex].TextAlign = TextAlignEnum.CenterCenter;
            grfResVs.Cols[colgrfVsDeptName].TextAlign = TextAlignEnum.LeftCenter;
            grfResVs.Cols[colgrfVsDtrNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfResVs.Cols[colgrfVsVnno].TextAlign = TextAlignEnum.LeftCenter;

            grfResVs.Cols[colgrfVsPreno].Visible = false;
            grfResVs.Cols[colgrfVsVsdate].Visible = false;
            //grfResVs.Cols[colgrfVsVnno].Visible = false;
            //grfResVs.Cols[colgrfVsVsFlag].Visible = false;

            grfResVs.Cols[colgrfVsTime].AllowEditing = false;
            grfResVs.Cols[colgrfVsHn].AllowEditing = false;
            grfResVs.Cols[colgrfVsFullNameT].AllowEditing = false;
            grfResVs.Cols[colgrfVsAge].AllowEditing = false;
            grfResVs.Cols[colgrfVsSex].AllowEditing = false;
            grfResVs.Cols[colgrfVsDeptName].AllowEditing = false;
            grfResVs.Cols[colgrfVsDtrNameT].AllowEditing = false;
            grfResVs.Cols[colgrfVsVnno].AllowEditing = false;

            grfResVs.AfterRowColChange += GrfResVs_AfterRowColChange;
            //grfCheckUPList.AllowFiltering = true;

            pnVs.Controls.Add(grfResVs);
            theme1.SetTheme(grfResVs, "Office2010Red");
        }
        private void GrfResVs_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                //if (rowindexgrfResReq != ((C1FlexGrid)(sender)).Row) { rowindexgrfResReq = ((C1FlexGrid)(sender)).Row; }
                //else { return; }
                String flagopd = "";
                showLbLoading();
                HN = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsHn] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsHn].ToString() : "";
                PRENO = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsPreno] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsPreno].ToString() : "";
                VSDATE = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsVsdate] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsVsdate].ToString() : "";
                VNNO = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsVnno] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsVnno].ToString() : "";
                flagopd = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsVsFlag] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colgrfVsVsFlag].ToString() : "";
                setGrfResReqLab(HN, VSDATE, PRENO, VNNO, flagopd);
                //grfResReq.Rows.Count = 1;
                grfRes.Rows.Count = 1;
                grfResReq.Select(1, 2);
                grfResReq.Select(1, 1);
                grfResReq.RowSel = 1;
                rowindexgrfResReq = 0;
                
                //pnResRpt.Hide();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmLab GrfResVs_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmLab GrfResVs_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }
        private void setGrfResVs()
        {
            if (pageLoad) return;
            pageLoad = true;
            showLbLoading();
            timeOperList.Stop();
            DataTable dtvs = new DataTable();
            String reqdate = "";
            DateTime.TryParse(txtSBSearchDate.Text, out DateTime reqdate1);
            if (reqdate1.Year > 2500) reqdate1 = reqdate1.AddYears(reqdate1.Year-543);
            dtvs = bc.bcDB.labT01DB.selectVisitReqLabByDate(reqdate1.Year.ToString()+"-"+ reqdate1.ToString("MM-dd"));  //ควรดึงจาก visit ก่อน
            Patient ptt = new Patient();
            grfResVs.Rows.Count = 1;
            grfResVs.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfResVs.Rows[i];
                ptt.patient_birthday = row1["MNC_BDAY"].ToString();
                rowa[colgrfVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
                rowa[colgrfVsHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfVsFullNameT] = row1["pttfullnamet"].ToString();
                rowa[colgrfVsAge] = ptt.AgeStringOK1DOT();
                rowa[colgrfVsSex] = row1["MNC_SEX"].ToString();

                rowa[colgrfVsDtrNameT] = row1["dtrnamet"].ToString();
                //rowa[colgrfVsDeptName] = bc.bcDB.pm32DB.getDeptNameOPD1(row1["MNC_DEP_NO"].ToString());
                rowa[colgrfVsPreno] = row1["MNC_PRE_NO"].ToString();
                rowa[colgrfVsVsdate] = row1["MNC_DATE"].ToString();
                //rowa[colgrfVsVnno] = row1["MNC_VN_NO"].ToString()+"."+ row1["MNC_VN_SEQ"].ToString()+"."+ row1["MNC_VN_SUM"].ToString();

                rowa[colgrfVsVsFlag] = row1["MNC_PAT_FLAG"].ToString();

                rowa[0] = i.ToString();
                rowa[colgrfVsDeptName] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["dept_opd"].ToString() : row1["dept_ipd"].ToString();
                rowa[colgrfVsVnno] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString() : row1["MNC_AN_NO"].ToString() + "." + row1["MNC_AN_YR"].ToString();
                if (!row1["MNC_AN_NO"].ToString().Equals("0")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor); }
                i++;
            }
            timeOperList.Start();
            pageLoad = false;
            hideLbLoading();
        }
        private void initGrfReq()
        {
            grfReq = new C1FlexGrid();
            grfReq.Font = fEdit;
            grfReq.Dock = System.Windows.Forms.DockStyle.Fill;
            grfReq.Location = new System.Drawing.Point(0, 0);
            grfReq.Rows.Count = 1;
            grfReq.Cols.Count = 13;
            grfReq.Cols[colgrfReqReqNo].Width = 50;
            grfReq.Cols[colgrfReqReqTime].Width = 60;
            grfReq.Cols[colgrfReqHn].Width = 75;
            grfReq.Cols[colgrfReqFullNameT].Width = 250;
            grfReq.Cols[colgrfReqAge].Width = 70;
            grfReq.Cols[colgrfReqSex].Width = 50;
            grfReq.Cols[colgrfReqPaidName].Width = 150;
            grfReq.Cols[colgrfReqDtrNameT].Width = 200;
            grfReq.Cols[colgrfReqDeptName].Width = 120;
            grfReq.ShowCursor = true;
            grfReq.Cols[colgrfReqReqNo].Caption = "no";
            grfReq.Cols[colgrfReqReqTime].Caption = "time";
            grfReq.Cols[colgrfReqHn].Caption = "HN";
            grfReq.Cols[colgrfReqFullNameT].Caption = "ชื่อ-นามสกุล";
            grfReq.Cols[colgrfReqAge].Caption = "age";
            grfReq.Cols[colgrfReqSex].Caption = "sex";
            grfReq.Cols[colgrfReqPaidName].Caption = "สิทธิ";
            grfReq.Cols[colgrfReqDtrNameT].Caption = "แพทย์";
            grfReq.Cols[colgrfReqDeptName].Caption = "dept";
            
            grfReq.Cols[colgrfReqReqNo].DataType = typeof(String);
            grfReq.Cols[colgrfReqReqTime].DataType = typeof(String);
            grfReq.Cols[colgrfReqHn].DataType = typeof(String);
            grfReq.Cols[colgrfReqFullNameT].DataType = typeof(String);
            grfReq.Cols[colgrfReqAge].DataType = typeof(String);
            grfReq.Cols[colgrfReqSex].DataType = typeof(String);
            grfReq.Cols[colgrfReqPaidName].DataType = typeof(String);
            grfReq.Cols[colgrfReqDtrNameT].DataType = typeof(String);
            grfReq.Cols[colgrfReqDeptName].DataType = typeof(String);

            grfReq.Cols[colgrfReqReqNo].TextAlign = TextAlignEnum.CenterCenter;
            grfReq.Cols[colgrfReqReqTime].TextAlign = TextAlignEnum.CenterCenter;
            grfReq.Cols[colgrfReqHn].TextAlign = TextAlignEnum.LeftCenter;
            grfReq.Cols[colgrfReqFullNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfReq.Cols[colgrfReqAge].TextAlign = TextAlignEnum.CenterCenter;
            grfReq.Cols[colgrfReqSex].TextAlign = TextAlignEnum.CenterCenter;
            grfReq.Cols[colgrfReqPaidName].TextAlign = TextAlignEnum.LeftCenter;
            grfReq.Cols[colgrfReqDtrNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfReq.Cols[colgrfReqDeptName].TextAlign = TextAlignEnum.LeftCenter;

            grfReq.Cols[colgrfReqVsDate].Visible = false;
            grfReq.Cols[colgrfReqPreno].Visible = false;
            grfReq.Cols[colgrfReqReqDate].Visible = false;

            grfReq.Cols[colgrfReqReqNo].AllowEditing = false;
            grfReq.Cols[colgrfReqReqTime].AllowEditing = false;
            grfReq.Cols[colgrfReqHn].AllowEditing = false;
            grfReq.Cols[colgrfReqFullNameT].AllowEditing = false;
            grfReq.Cols[colgrfReqAge].AllowEditing = false;
            grfReq.Cols[colgrfReqSex].AllowEditing = false;
            grfReq.Cols[colgrfReqPaidName].AllowEditing = false;
            grfReq.Cols[colgrfReqDtrNameT].AllowEditing = false;
            grfReq.Cols[colgrfReqDeptName].AllowEditing = false;

            grfReq.AfterRowColChange += GrfReq_AfterRowColChange;
            //grfCheckUPList.AllowFiltering = true;

            panel1.Controls.Add(grfReq);
            theme1.SetTheme(grfReq, bc.iniC.themeApp);
        }

        private void GrfReq_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfReqLab()
        {
            if (pageLoad) return;
            pageLoad = true;
            timeOperList.Stop();
            DataTable dtvs = new DataTable();
            
            dtvs = bc.bcDB.labT01DB.selectReqOPDByNoStatus();

            grfReq.Rows.Count = 1;
            grfReq.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            Patient ptt = new Patient();
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfReq.Rows[i];
                ptt.patient_birthday = row1["MNC_BDAY"].ToString();
                
                rowa[colgrfReqReqDate] = row1["MNC_REQ_DAT"].ToString();
                rowa[colgrfReqReqTime] = bc.showTime(row1["MNC_REQ_TIM"].ToString());
                rowa[colgrfReqHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfReqFullNameT] = row1["pttfullnamet"].ToString();
                rowa[colgrfReqAge] = ptt.AgeStringOK1DOT();

                rowa[colgrfReqSex] = row1["MNC_SEX"].ToString();
                rowa[colgrfReqPaidName] = bc.getPaidShortName(row1["MNC_FN_TYP_DSC"].ToString());
                rowa[colgrfReqDtrNameT] = row1["dtrname"].ToString();
                rowa[colgrfReqDeptName] = bc.bcDB.pm32DB.getDeptNameOPD1(row1["MNC_REQ_DEP"].ToString());
                rowa[colgrfReqPreno] = row1["MNC_PRE_NO"].ToString();

                rowa[colgrfReqVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfReqReqNo] = row1["MNC_REQ_NO"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            timeOperList.Start();
            pageLoad = false;
        }
        private void FrmLab_Load(object sender, EventArgs e)
        {
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            scRes.HeaderHeight = 0;
            scReq.HeaderHeight = 0;
            scMake.HeaderHeight = 0;
            c1SplitContainer2.HeaderHeight = 0;
            this.Text = "Last Update 2024-07-02-1";
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String stationname = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);
            lfSbStation.Text = DEPTNO + "[" + bc.iniC.station + "]" + stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            lfsb1.Text = "timer "+bc.timerImgScanNew.ToString();
            txtSBSearchDate.Value = DateTime.Now;
         //pnLabAdd.Height= 400;
            pnGrpList.Height = 370;
            pnSubGrpLists.Height = 370;
            timeOperList.Enabled = true;
            timeOperList.Start();
            setGrfReqLab();     //tabReq
            tcMaster.SelectedTab = tabMasGrp;
            pnLabList.Height = 300;
            pnLabResList.Height = 350;
            c1SplitterPanel7.SizeRatio = 70;
        }
    }
}
