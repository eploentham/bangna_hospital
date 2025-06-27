using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
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
    public partial class FrmLabCompare1 : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, fEditBRed;
        C1ThemeController theme1;
        Label lbLoading;
        Boolean pageLoad = false;
        Image imgCorr, imgTran;
        Patient ptt;
        C1FlexGrid grfLab, grfResult;
        String HN = "", PRENO = "", VSDATE = "", AN = "", statusIPD="", LABNAME="";
        List<String> lLccode;
        DateTime datedischarge;
        C1DockingTab tcDtr;
        int colLabHn = 1, colLablccode = 2, colLabLabName = 3, colLabSubName = 4, colLabRange = 5, colLabUnitName = 6, colLabSubcode = 7, colLabReqNO=8, colLabReqDate=9;
        public FrmLabCompare1(BangnaControl bc, String hn, String vsdate, String preno, String an)
        {
            InitializeComponent();
            this.bc = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.AN = an;
            statusIPD = (AN.Length > 0) ? "IPD":"OPD";
            initConfig();
        }
        public FrmLabCompare1(BangnaControl bc, String hn, String vsdate, String preno, String an, String labname)
        {
            InitializeComponent();
            this.bc = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.AN = an;
            this.LABNAME = labname;
            statusIPD = (AN.Length > 0) ? "IPD" : "OPD";
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
            //fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            theme1 = new C1ThemeController();

            ptt = new Patient();
            lLccode = new List<string>();

            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);

            bc.bcDB.labT02DB.setCboLabNamebyHN(cboLab, HN);
            bc.setC1ComboByName(cboLab, LABNAME);
            initGrfLab(0);
            initCboSeries();
            initTabDtr();
            setControl();
            //initGrf();
            cboLab.SelectedItemChanged += CboLab_SelectedItemChanged;
            cboSeries.SelectedItemChanged += CboSeries_SelectedItemChanged;
            btnOk.Click += BtnOk_Click;
            btnLabAdd.Click += BtnLabAdd_Click;
            pageLoad = false;
        }

        private void BtnLabAdd_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (cboLab.SelectedItem == null) return;
            String lccode = cboLab.SelectedItem != null ? ((ComboBoxItem)(cboLab.SelectedItem)).Value.ToString() : "";
            String lcname = cboLab.SelectedItem != null ? cboLab.SelectedItem.ToString() : "";
            Boolean chk = false;
            foreach(C1DockingTabPage tab in tcDtr.TabPages)
            {
                String tabname = tab.Name.Replace("tab","");
                if (tabname.Equals(lcname))
                {
                    chk = true;
                }
            }
            if (!chk)
            {
                datedischarge = DateTime.Now;
                setGrfLab(datedischarge);
            }
        }

        private void initTabDtr()
        {
            tcDtr = new C1DockingTab();
            tcDtr.Dock = System.Windows.Forms.DockStyle.Fill;
            tcDtr.Location = new System.Drawing.Point(0, 266);
            tcDtr.Name = "tcDtr";
            tcDtr.Size = new System.Drawing.Size(669, 200);
            tcDtr.TabIndex = 0;
            tcDtr.TabsSpacing = 5;
            panel2.Controls.Add(tcDtr);

            LabM01 lab = new LabM01();
            lab = bc.bcDB.labM01DB.SelectByName(LABNAME);
            panelResult.Hide();
            //C1DockingTabPage tabLab = new C1DockingTabPage();
            //tabLab.Location = new System.Drawing.Point(1, 24);
            ////tabScan.Name = "c1DockingTabPage1";
            //tabLab.Size = new System.Drawing.Size(667, 175);
            //tabLab.TabIndex = 0;
            //tabLab.Text = LABNAME;
            //tabLab.Name = "tab"+ lab.MNC_LB_DSC;
            //tabLab.Controls.Add(panelResult);
            //panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            //tcDtr.Controls.Add(tabLab);
        }
        private void CboSeries_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            //setCbo();
            pageLoad = false;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            datedischarge = DateTime.Now;
            //setGrfLab(datedischarge, tcDtr);
        }

        private void CboLab_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            //String lccode = cboLab.SelectedItem != null ? cboLab.SelectedItem.ToString() : "1100000099";
            //setCbo();
            pageLoad = false;
        }
        private void setCbo()
        {
            showLbLoading();
            try
            {
                grfLab.Hide();
                String lccode = cboLab.SelectedItem != null ? ((ComboBoxItem)cboLab.SelectedItem).Value : "";
                if (lLccode.Count == 0)
                {
                    lLccode.Add(lccode);
                    //addGrdLab(lccode);
                }
                foreach (String chk in lLccode)
                {
                    if (!chk.Equals(lccode))
                    {
                        lLccode.Add(lccode);
                        //addGrdLab(lccode);
                        break;
                    }
                }
                datedischarge = DateTime.Now;
                //setGrfLab(datedischarge);
                grfLab.Show();
            }
            catch(Exception ex)
            {
                String aaa = "";
                new LogWriter("e", "FrmLabCompare setCbo "+ex.Message);
            }
            hideLbLoading();
        }
        private void addGrdLab(String lccode)
        {
            grfLab.Rows.Count = lLccode.Count+1;
            grfLab[grfLab.Rows.Count-1, colLablccode] = lccode;
        }
        private void initGrfLab(int icol)
        {
            grfLab = new C1FlexGrid();
            grfLab.Font = fEdit;
            grfLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLab.Location = new System.Drawing.Point(0, 0);
            
            //panelLab.Controls.Clear();

            //panelLab.Controls.Add(grfLab);

            grfLab.Rows.Count = 1;
            //grfLab.Cols.Count = 10 + icol;
            grfLab.Cols.Count = 10;
            grfLab.Cols[colLabHn].Caption = "HN";
            grfLab.Cols[colLablccode].Caption = "code";
            grfLab.Cols[colLabLabName].Caption = "name";
            grfLab.Cols[colLabSubName].Caption = "lab name";
            grfLab.Cols[colLabRange].Caption = "Range";
            grfLab.Cols[colLabUnitName].Caption = "Unit";
            grfLab.Cols[colLabSubcode].Caption = "lab sub";

            grfLab.Cols[colLabHn].Width = 90;
            grfLab.Cols[colLablccode].Width = 250;
            grfLab.Cols[colLabLabName].Width = 210;
            grfLab.Cols[colLabSubName].Width = 200;
            grfLab.Cols[colLabRange].Width = 140;
            grfLab.Cols[colLabUnitName].Width = 120;
            

            grfLab.Cols[colLabLabName].AllowEditing = false;
            grfLab.Cols[colLabSubName].AllowEditing = false;
            grfLab.Cols[colLabRange].AllowEditing = false;
            grfLab.Cols[colLabUnitName].AllowEditing = false;

            grfLab.Cols[colLabHn].Visible = false;
            grfLab.Cols[colLablccode].Visible = false;
            grfLab.Cols[colLabSubcode].Visible = false;
            grfLab.Cols[colLabReqNO].Visible = false;
            grfLab.Cols[colLabReqDate].Visible = false;

            grfLab.Cols[colLabLabName].AllowSorting = false;
            grfLab.Cols[colLabUnitName].AllowSorting = false;
            grfLab.Cols[colLabRange].AllowSorting = false;
            grfLab.Cols[colLabSubName].AllowSorting = false;
            //grfLab.Click += GrfHn_Click;
            theme1.SetTheme(grfLab, "MacBlue");
        }
        private void setGrfLab(DateTime datedis)
        {
            DataTable dtlab = new DataTable();
            DataTable dtdate = new DataTable();
            String datestart = "", dateend = "";
            DateTime dateend1 = datedis;
            DateTime datestart1 = datedis;
            String seriesid = "";
            dateend = dateend1.Year + "-" + dateend1.ToString("MM-dd");
            seriesid = cboSeries.SelectedItem != null ? ((ComboBoxItem)cboSeries.SelectedItem).Value : "";
            if (seriesid.Length > 0)
            {
                if (seriesid.Equals("00"))
                {
                    datestart1 = dateend1;
                }
                else if (seriesid.Equals("01"))
                {
                    datestart1 = datestart1.AddDays(-3);
                }
                else if (seriesid.Equals("02"))
                {
                    datestart1 = datestart1.AddDays(-7);
                }
                else if (seriesid.Equals("03"))
                {
                    datestart1 = datestart1.AddDays(-14);
                }
                else if (seriesid.Equals("04"))
                {
                    datestart1 = datestart1.AddDays(-30);
                }
                else if (seriesid.Equals("05"))
                {
                    datestart1 = datestart1.AddDays(-365);
                }
                else if (seriesid.Equals("99"))
                {
                    datestart1 = datestart1.AddDays(-730);
                }
            }
            else
            {
                datestart1 = DateTime.Now;
            }
            datestart = datestart1.Year + "-" + datestart1.ToString("MM-dd");
            //dtlab = bc.bcDB.labT02DB.selectLabSubNamebyHN(HN, lLccode, datestart, dateend);
            String lccode = cboLab.SelectedItem != null ? ((ComboBoxItem)cboLab.SelectedItem).Value : "";
            String lcname = cboLab.SelectedItem != null ? ((ComboBoxItem)cboLab.SelectedItem).Text : "";
            lLccode.Clear();
            lLccode.Add(lccode);
            dtdate = bc.bcDB.labT02DB.selectLabSubNameDatebyHN(HN, lLccode, datestart, dateend);
            lf1.Text = lcname+" "+dtdate.Rows.Count + " รายการ";
            //initGrfLab(dtdate.Rows.Count);
            //grfLab.Rows.Count = 1;
            C1DockingTabPage tab = new C1DockingTabPage();
            tab.Name = "tab" + lcname;
            tab.Text = lcname;
            tcDtr.TabPages.Add(tab);
            tcDtr.CloseBox = CloseBoxPositionEnum.AllPages;
            tcDtr.TabsShowFocusCues = true;
            //tcDtr.Controls.Add(tab);

            C1FlexGrid grfResult = new C1FlexGrid();
            grfResult.Font = fEdit;
            grfResult.Dock = System.Windows.Forms.DockStyle.Fill;
            grfResult.Location = new System.Drawing.Point(0, 0);
            grfResult.Name = "grf"+lccode;
            grfResult.Rows.Count = 1;
            grfResult.Cols.Count = dtdate.Rows.Count+4; //บวกชื่อ lab sub name
            Panel panelResult = new Panel();
            panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            panelResult.Controls.Add(grfResult);
            tab.Controls.Add(panelResult);
            //grfLab.Rows.Count = dtlab.Rows.Count + 1;
            //grfLab.Cols.Add(dtdate.Rows.Count);
            CellStyle cells0 = grfResult.Cols[2].StyleNew;
            cells0.BackColor = ColorTranslator.FromHtml("#FFCACC");
            CellStyle cells1 = grfResult.Cols[3].StyleNew;
            cells1.BackColor = ColorTranslator.FromHtml("#DBC4F0");
            int i = 0, icol=4;
            foreach(DataRow dcol in dtdate.Rows)
            {
                grfResult.Cols[0].Visible = true;
                grfResult.Cols[icol].Visible = true;
                grfResult.Cols[icol].Caption = dcol["MNC_REQ_DAT"].ToString()+"["+ dcol["MNC_REQ_NO"].ToString()+"]";
                grfResult.Cols[icol].Width = 125;
                grfResult.Cols[icol].AllowSorting = false;
                grfResult.Cols[icol].TextAlign = TextAlignEnum.RightCenter;
                CellStyle cells = grfResult.Cols[icol].StyleNew;
                if (icol == 4) cells.BackColor = ColorTranslator.FromHtml("#88AB8E");
                else if (icol == 5) cells.BackColor = ColorTranslator.FromHtml("#AFC8AD");
                else if (icol == 6) cells.BackColor = ColorTranslator.FromHtml("#EEE7DA");
                else if (icol == 7) cells.BackColor = ColorTranslator.FromHtml("#F2F1EB");
                else if (icol == 8) cells.BackColor = ColorTranslator.FromHtml("#C3E2C2");
                else if (icol == 9) cells.BackColor = ColorTranslator.FromHtml("#EAECCC");
                else if (icol == 10) cells.BackColor = ColorTranslator.FromHtml("#DBCC95");
                else if (icol == 11) cells.BackColor = ColorTranslator.FromHtml("#CD8D7A");

                else if (icol == 12) cells.BackColor = ColorTranslator.FromHtml("#FFC5C5");
                else if (icol == 13) cells.BackColor = ColorTranslator.FromHtml("#FFEBD8");
                else if (icol == 14) cells.BackColor = ColorTranslator.FromHtml("#C7DCA7");
                else if (icol == 15) cells.BackColor = ColorTranslator.FromHtml("#89B9AD");

                else if (icol == 16) cells.BackColor = ColorTranslator.FromHtml("#DADDB1");
                else if (icol == 17) cells.BackColor = ColorTranslator.FromHtml("#B3A492");
                else if (icol == 18) cells.BackColor = ColorTranslator.FromHtml("#BFB29E");
                else if (icol == 19) cells.BackColor = ColorTranslator.FromHtml("#D6C7AE");

                else if (icol == 20) cells.BackColor = ColorTranslator.FromHtml("#FAF3F0");
                else if (icol == 21) cells.BackColor = ColorTranslator.FromHtml("#D4E2D4");
                else if (icol == 22) cells.BackColor = ColorTranslator.FromHtml("#FFCACC");
                else if (icol == 23) cells.BackColor = ColorTranslator.FromHtml("#DBC4F0");
                //grfResult.Cols[0] = icol;
                icol++;
            }
            ////grfLab.Redraw;
            //grfLab.Refresh();
            //foreach (DataRow drow in dtdate.Rows)
            //{ 
            //    i++;
            //    grfResult[i, 1] = drow["MNC_LB_CD"].ToString();
            //    grfResult[i, 2] = drow["MNC_RES"].ToString();
            //}
            //1. เอาข้อมูลจาก dtdate มาวน reqdate กับ reqno และ hn
            //2. แล้วมาวนใน grfLab อีกที lccode  ใช้แบบนี้ไม่ได้ เพราะ มี male female การแสดงผล  และถ้าใน table lab_t05 มีข้อมูล แต่ใน grf ไม่มีข้อมูล จะทำให้เกิด bug
            String labname = "", labnameold = "", reqno1 = "", reqnoold = "";
            //grfResult.Cols[colLabSubName].Visible = true;
            for (int jjj = 4; jjj < icol; jjj++)
            {
                String reqno = "", reqdate="";
                reqno = grfResult.Cols[jjj].Caption;
                reqno = reqno.Replace("]","");
                String[] req = reqno.Split('[');
                if (req.Length > 1)
                {
                    String err = "00";
                    try
                    {
                        reqno = req[1];
                        reqdate = req[0];
                        DataTable dtres = new DataTable();
                        dtres = bc.bcDB.labT02DB.selectLabResultbyHNReqNo(HN, lLccode, reqdate, reqno);//ได้ result ต้องเอาลงให้ครบ ถ้าค้นไม่เจอ ก็ เพิ่ม record
                        //String labname = "", labnameold = "", reqno1 = "", reqnoold = "";
                        foreach (DataRow rowres in dtres.Rows)
                        {
                            err = "00";
                            Boolean chk = false;
                            for (int rowindex = 1; rowindex < grfResult.Rows.Count; rowindex++)//ค้นใน grf ว่ามี ข้อมูลไหม ถ้าไม่เจอ ต้องเพิ่ม
                            {       // ต้องเอาชื่อlab sub เพราะ ใน table lab_t05 field mnc_lb_res_cd เป็น running
                                    //grfResult[rowindex, 1] = rowres["MNC_RES"].ToString();
                                if (rowres["MNC_RES"].ToString().Trim().Equals("Target cell"))
                                {
                                    String aaa = "";
                                }
                                if (rowres["MNC_RES"].ToString().Trim().Equals(grfResult[rowindex, 1].ToString()))
                                {
                                    err = "01 rowindex " + rowindex+ " grfResult.Rows.Count " + grfResult.Rows.Count;
                                    //grfLab[colindex, colLabSubName] = "";
                                    //grfResult[colindex, colLabUnitName] = rowres["MNC_RES_UNT"].ToString();
                                    //grfResult[colindex, colLabRange] = rowres["MNC_RES_MIN"].ToString() + " - " + rowres["MNC_RES_MAX"].ToString();
                                    grfResult[rowindex, jjj] = rowres["MNC_RES_VALUE"].ToString();
                                    try
                                    {
                                        float min = 0, max = 0, val = 0;
                                        float.TryParse(rowres["MNC_RES_MIN"].ToString(), out min);
                                        float.TryParse(rowres["MNC_RES_MAX"].ToString(), out max);
                                        float.TryParse(rowres["MNC_RES_VALUE"].ToString(), out val);
                                        if ((val < min) || (val > max))
                                        {
                                            var cell = grfResult[rowindex, jjj];
                                            var rng = grfResult.GetCellRange(rowindex, jjj);
                                            var newStyle = grfResult.Styles.Add(null);
                                            newStyle.MergeWith(rng.StyleDisplay);
                                            newStyle.Font = fEditB;
                                            newStyle.ForeColor = Color.Red;
                                            rng.Style = newStyle;
                                        }
                                        if (rowres["MNC_STS"].ToString().Trim().ToLower().Equals("abnormal"))
                                        {
                                            var cell = grfResult[rowindex, jjj];
                                            var rng = grfResult.GetCellRange(rowindex, jjj);
                                            var newStyle = grfResult.Styles.Add(null);
                                            newStyle.MergeWith(rng.StyleDisplay);
                                            newStyle.Font = fEditB;
                                            newStyle.ForeColor = Color.Red;
                                            rng.Style = newStyle;
                                            //rowres.SetColumnError(jjj, "error");
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    chk = true;
                                }
                            }
                            if (!chk)
                            {
                                //Row rownew = grfLab.Rows.Add();
                                try
                                {
                                    Row resultnew = grfResult.Rows.Add();
                                    //rownew[0] = grfLab.Rows.Count-1;
                                    labname = rowres["MNC_LB_DSC"].ToString().Trim();
                                    reqno1 = rowres["mnc_req_no"].ToString().Trim();
                                    //if (!labname.Equals(labnameold) || !reqno1.Equals(reqnoold))
                                    if (!labname.Equals(labnameold))
                                    {
                                        labnameold = labname;
                                        reqnoold = reqno1;
                                        //rownew[colLabLabName] = labname;
                                    }
                                    else
                                    {
                                        //rownew[colLabLabName] = "";
                                    }
                                    //rownew[colLabHn] = txtHn.Text.Trim();
                                    //rownew[colLablccode] = rowres["MNC_LB_CD"].ToString();
                                    //rownew[colLabSubName] = rowres["MNC_RES"].ToString().Trim();
                                    //rownew[colLabUnitName] = rowres["MNC_RES_UNT"].ToString().Trim();
                                    //rownew[colLabRange] = rowres["MNC_RES_MIN"].ToString() + " - " + rowres["MNC_RES_MAX"].ToString();
                                    //rownew[colLabSubcode] = rowres["MNC_LB_RES_CD"].ToString();
                                    resultnew[1] = rowres["MNC_RES"].ToString();
                                    resultnew[jjj] = rowres["MNC_RES_VALUE"].ToString();
                                    resultnew[2] = rowres["MNC_RES_UNT"].ToString();
                                    resultnew[3] = rowres["MNC_RES_MIN"].ToString() + " - " + rowres["MNC_RES_MAX"].ToString();
                                    float min = 0, max = 0, val = 0;
                                    float.TryParse(rowres["MNC_RES_MIN"].ToString(), out min);
                                    float.TryParse(rowres["MNC_RES_MAX"].ToString(), out max);
                                    float.TryParse(rowres["MNC_RES_VALUE"].ToString(), out val);
                                    if ((val < min) || (val > max))
                                    {
                                        //var cell = rownew[jjj];
                                        var rng = grfResult.GetCellRange(grfResult.Rows.Count - 1, jjj);
                                        var newStyle = grfResult.Styles.Add(null);
                                        newStyle.MergeWith(rng.StyleDisplay);
                                        newStyle.Font = fEditB;
                                        newStyle.ForeColor = Color.Red;
                                        rng.Style = newStyle;
                                    }
                                    if (rowres["MNC_STS"].ToString().Trim().ToLower().Equals("abnormal"))
                                    {
                                        //var cell = rownew[jjj];
                                        var rng = grfResult.GetCellRange(grfResult.Rows.Count - 1, jjj);
                                        var newStyle = grfResult.Styles.Add(null);
                                        newStyle.MergeWith(rng.StyleDisplay);
                                        newStyle.Font = fEditB;
                                        newStyle.ForeColor = Color.Red;
                                        rng.Style = newStyle;
                                        //rowres.SetColumnError(jjj, "error");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    new LogWriter("e", "FrmLabCompare setGrfLab selectLabResultbyHNReqNo " + err + " " + ex.Message);
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        new LogWriter("e", "FrmLabCompare setGrfLab selectLabResultbyHNReqNo "+ err+" " + ex.Message);
                    }
                    
                }
            }
        }
        public void initCboSeries()
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            cboSeries.Items.Clear();
            cboSeries.Items.Add(item1);
            item1 = new ComboBoxItem();
            item1.Text = "ย้อนหลัง 3 วัน";
            item1.Value = "01";
            cboSeries.Items.Add(item1);
            item1 = new ComboBoxItem();
            item1.Text = "ย้อนหลัง 1 สัปดาห์";
            item1.Value = "02";
            cboSeries.Items.Add(item1);
            item1 = new ComboBoxItem();
            item1.Text = "ย้อนหลัง 2 สัปดาห์";
            item1.Value = "03";
            cboSeries.Items.Add(item1);
            item1 = new ComboBoxItem();
            item1.Text = "ย้อนหลัง 1 เดือน";
            item1.Value = "04";
            cboSeries.Items.Add(item1);
            item1 = new ComboBoxItem();
            item1.Text = "ย้อนหลัง 1 ปี";
            item1.Value = "05";
            cboSeries.Items.Add(item1);
            item1 = new ComboBoxItem();
            item1.Text = "ย้อนหลัง 2 ปี";
            item1.Value = "99";
            cboSeries.Items.Add(item1);

            cboSeries.SelectedIndex = 6;
        }
        private void setControl()
        {
            ptt = bc.bcDB.pttDB.selectPatinetByHn(HN);
            txtHn.Text = HN;
            txtNameT.Text = ptt.Name;
            txtAges.Text = ptt.AgeStringOK1DOT();
            txtDOB.Text = ptt.patient_birthday;
            txtSex.Text = ptt.MNC_SEX;

            String allergy = "", chronic="";
            DataTable dt = new DataTable();
            DataTable dtchronic = new DataTable();
            dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
            dtchronic = bc.bcDB.vsDB.SelectChronicByPID(ptt.idcard);
            foreach (DataRow row in dt.Rows)
            {
                allergy += row["MNC_ph_tn"].ToString() + " " + row["MNC_ph_memo"].ToString() + ", ";
            }
            lbDrugAllergy.Text = "";
            if (allergy.Length > 0)
            {
                lbDrugAllergy.Text = allergy;
            }
            else
            {
                lbDrugAllergy.Text = "ไม่มีข้อมูล การแพ้ยา ";
            }
            foreach (DataRow row in dtchronic.Rows)
            {
                chronic += row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString() + ",";
            }
            lbChronic1.Text = chronic;
            //setCbo();
            datedischarge = DateTime.Now;
            setGrfLab(datedischarge);
        }
        private void addLccode()
        {

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
        private void FrmLabCompare_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 2023-11-29 ";
        }
    }
}
