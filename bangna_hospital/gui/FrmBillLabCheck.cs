using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.Properties;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmBillLabCheck : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;

        Color bg, fc;
        Font ff, ffB;
        int colID = 1, colName = 2;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        List<String> lItm, lItmC, lItmE, lPaid;
        List<int> lPaidCntErr, lPaidCnt;
        C1FlexGrid grf2, grf11;
        DataTable dtChk, dtChkGrp;
        public FrmBillLabCheck(BangnaControl x)
        {
            InitializeComponent();
            bc = x;
            initConfig();
        }
        private void initConfig()
        {
            pB1.Hide();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            C1ThemeController.ApplicationTheme = bc.iniC.themeApplication;
            theme1.Theme = C1ThemeController.ApplicationTheme;
            dtChk = new DataTable();
            dtChkGrp = new DataTable();
            addColumn(dtChk);
            //dtAll.Columns.Add("row1", typeof(int));

            //theme1.SetTheme(sB, "BeigeOne");
            bc.setCboMonth(cboMonth);
            bc.setCboYear(cboYear);
            bc.setCboPeriod(cboPeriod);
            pic1.Image = Resources.Navigate_right;
            pic2.Image = Resources.Navigate_right;
            //pic3.Image = Resources.Navigate_right;
            pic21.Image = Resources.Navigate_left;
            pic31.Image = Resources.Navigate_left;

            btnCheck.Click += BtnCheck_Click;
            bthPath.Click += BthPath_Click;
            btnOpen.Click += BtnOpen_Click;
            btnCheck2.Click += BtnCheck2_Click;
            pic1.Click += Pic1_Click;
            pic2.Click += Pic2_Click;
            pic31.Click += Pic31_Click;
            pic21.Click += Pic21_Click;
            btnTab3.Click += BtnTab3_Click;
            btnPrint.Click += BtnPrint_Click;
            btnCheck.Enabled = false;
            btnOpen.Enabled = false;
            btnCheck2.Enabled = false;
            btnTab3.Enabled = false;
            panel8.Hide();
            label6.Text = "";
            initGrf();
            initGrf11();
            String chk = "", printerDefault = "";
            try
            {
                PrinterSettings settings = new PrinterSettings();
                int i = 0;
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    settings.PrinterName = printer;
                    cboPrinter.Items.Insert(i, printer);
                    if (settings.IsDefaultPrinter)
                        printerDefault = printer;
                    i++;
                }
                PrinterSettings settings1 = new PrinterSettings();
                //settings1.PrinterName = ;
            }
            catch (Exception ex)
            {
                chk = ex.Message.ToString();
            }
        }
        private void addColumn(DataTable dt)
        {
            dt.Columns.Add("row1", typeof(int));
            dt.Columns.Add("lab_code", typeof(String));
            dt.Columns.Add("lab_name", typeof(String));
            dt.Columns.Add("qty", typeof(int));
            dt.Columns.Add("price", typeof(String));
            dt.Columns.Add("net_price", typeof(String));
            dt.Columns.Add("amount", typeof(String));
            dt.Columns.Add("paidtype", typeof(String));
            dt.Columns.Add("status_chk", typeof(String));
            dt.Columns.Add("ptt_name", typeof(String));
            dt.Columns.Add("date", typeof(String));
            dt.Columns.Add("hn", typeof(String));
            dt.Columns.Add("col1", typeof(String));
            
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = cboPrinter.Text;
            document.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            //This is where you set the printer in your case you could use "EPSON USB"

            //or whatever it is called on your machine, by Default it will choose the default printer

            
            //document.ShowDialog();
            document.Print();
        }

        private void BtnTab3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tC.SelectedTab = tab3;
        }

        private void Pic21_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tC.SelectedTab = tab1;
        }

        private void Pic31_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tC.SelectedTab = tab2;
        }

        private void Pic2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tC.SelectedTab = tab3;
        }

        private void Pic1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tC.SelectedTab = tab2;
        }
        private void BtnCheck2_Click(object sender, EventArgs e)
        {
            int cnt = 0, cntErr=0;
            Decimal sum = 0, price1=0, net=0, minus=0;
            //throw new NotImplementedException();
            pB2.Show();
            pB2.Minimum = 0;
            pB2.Maximum = lItm.Count;
            ConnectDB conn;
            conn = new ConnectDB(bc.iniC);
            lItmC = new List<string>();
            lItmE = new List<string>();
            label11.Text = chkBn1.Checked ? "bangna1" : chkBn2.Checked ? "bangna2" : chkBn5.Checked ? "bangna5" : "";
            label12.Text = cboYear.Text;
            label13.Text = cboMonth.Text;
            label14.Text = cboPeriod.Text;
            Application.DoEvents();
            //foreach (String str in lItm)
            foreach (DataRow str in dtChk.Rows)
            {
                try
                {
                    //String[] itm = str.Split('|');
                    //String[] itm = str.Split('|');
                    String col1 = "", date = "", hn = "", name = "", fntype="",col6="", labname = "", labcode="", labdate1 = "",labdate = "", labdateOld="", price="";
                    //col1 = itm[0];
                    //date = itm[1];
                    //hn = itm[2];
                    //name = itm[3];
                    //fntype = itm[4];
                    //col6 = itm[5];
                    //price = itm[6];
                    //labcode = itm[7];
                    //labname = itm[8];

                    col1 = str["col1"].ToString();
                    date = str["date"].ToString();
                    hn = str["hn"].ToString();
                    name = str["ptt_name"].ToString();
                    fntype = str["paidtype"].ToString();
                    col6 = str["net_price"].ToString();
                    price = str["price"].ToString();
                    labcode = str["lab_code"].ToString();
                    labname = str["lab_name"].ToString();

                    labdate1 = (int.Parse(date.Substring(6)) - 543) + "-" + date.Substring(3, 2) + "-" + date.Substring(0, 2);
                    price1 = 0;
                    if (Decimal.TryParse(price, out price1))
                    {
                        sum += price1;
                    }
                    String sql = "";
                    DataTable dt = new DataTable();
                    //sql = "select lab_t05.MNC_req_no,LAB_T01.MNC_PRE_NO " +
                    //    "from PATIENT_T01 " +
                    //    "inner join LAB_T01 on LAB_T01.mnc_hn_no = PATIENT_T01.mnc_hn_no " +
                    //    "and LAB_T01.mnc_pre_no = PATIENT_T01.mnc_pre_no " +
                    //    "and LAB_T01.mnc_date = PATIENT_T01.MNC_DATE " +
                    //    "inner join LAB_T05 on lab_t05.MNC_REQ_YR = lab_t01.MNC_REQ_YR " +
                    //    "and lab_t05.MNC_REQ_no = lab_t01.MNC_REQ_no " +
                    //    "and lab_t05.MNC_REQ_dat = lab_t01.MNC_REQ_dat " +
                    //    "where lab_t05.MNC_REQ_DAT >= '" + labdate1 + "' " +
                    //    "and lab_t05.MNC_REQ_DAT <= '" + labdate1 + "' " +
                    //    //"and patient_t01.MNC_STS = 'f' " +
                    //    //"and LAB_T01.MNC_REQ_STS = 'Q' " +
                    //    "and LAB_T01.mnc_hn_no ='" + hn + "' " +
                    //    "and lab_t05.mnc_lb_cd ='" + labcode + "'";
                    sql = "select lab_t02.MNC_req_no,LAB_T01.MNC_PRE_NO " +
                        "from PATIENT_T01 " +
                        "inner join LAB_T01 on LAB_T01.mnc_hn_no = PATIENT_T01.mnc_hn_no " +
                        "and LAB_T01.mnc_pre_no = PATIENT_T01.mnc_pre_no " +
                        "and LAB_T01.mnc_date = PATIENT_T01.MNC_DATE " +
                        "inner join lab_t02 on lab_t02.MNC_REQ_YR = lab_t01.MNC_REQ_YR " +
                        "and lab_t02.MNC_REQ_no = lab_t01.MNC_REQ_no " +
                        "and lab_t02.MNC_REQ_dat = lab_t01.MNC_REQ_dat " +
                        "where lab_t02.MNC_REQ_DAT >= '" + labdate1 + "' " +
                        "and lab_t02.MNC_REQ_DAT <= '" + labdate1 + "' " +
                        //"and patient_t01.MNC_STS = 'f' " +
                        //"and LAB_T01.MNC_REQ_STS = 'Q' " +
                        "and LAB_T01.mnc_hn_no ='" + hn + "' " +
                        "and lab_t02.mnc_lb_cd ='" + labcode + "'";
                    dt = conn.selectData(conn.connMainHIS, sql);
                    if (dt.Rows.Count > 0)
                    {
                        //itm[8] = dt.Rows[0]["MNC_PRE_NO"].ToString();
                        //itm[9] = dt.Rows[0]["MNC_req_no"].ToString();
                        str["status_chk"] = "1";
                        listBox2.Items.Add(date+" "+ hn+" "+ name+" "+ labcode+" "+ labname);
                        lItmC.Add(col1+"|"+ date+"|"+hn+"|"+ name+"|"+ fntype+"|"+ col6+"|"+ price+"|"+ labcode+"|"+ labname+"|"+ dt.Rows[0]["MNC_PRE_NO"].ToString() + "|"+ dt.Rows[0]["MNC_req_no"].ToString());
                        cnt++;
                        net += price1;
                        if((cnt % 100)== 0)
                        {
                            Application.DoEvents();
                        }
                    }
                    else
                    {
                        cntErr++;
                        minus += price1;
                        listBox3.Items.Add(date + " " + hn + " " + name + " " + labcode + " " + labname);
                        lItmC.Add(col1 + "|" + date + "|" + hn + "|" + name + "|" + fntype + "|" + col6 + "|" + price + "|" + labcode + "|" + labname + "|0|0|-"+"|-");
                        lItmE.Add(col1 + "|" + date + "|" + hn + "|" + name + "|" + fntype + "|" + col6 + "|" + price + "|" + labcode + "|" + labname + "|0|0|-" + "|-");
                        int cnt1 = 0;
                        foreach (String paid in lPaid)
                        {
                            if (paid.Equals(fntype))
                            {
                                //int cnt2 = 0;
                                lPaidCntErr[cnt1]++;
                                //int.TryParse(lPaidCnt[cnt1],out cnt2);
                            }
                            cnt1++;
                        }
                        //itm[8] = dt.Rows[0]["MNC_PRE_NO"].ToString();
                        //itm[9] = dt.Rows[0]["MNC_req_no"].ToString();
                        //listBox2.Items.Add(itm.ToString());
                    }
                    pB2.Value++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ex"+ex.Message, "");
                }
            }
            //Control ctn = this.GetControl("listBoxSum3");
            ListBox listsum = new ListBox();
            foreach (Control ctl in this.Controls)
            {
                if((ctl is C1DockingTab) && (ctl.Name.Equals("tC")))
                {
                    foreach (Control ctl1 in ctl.Controls)
                    {
                        if ((ctl1 is C1DockingTabPage) && (ctl1.Name.Equals("tab3")))
                        {                            
                            foreach (Control ctl2 in ctl1.Controls)
                            {
                                if ((ctl2 is Panel) && (ctl2.Name.Equals("panel3")))
                                {
                                    foreach (Control ctl3 in ctl2.Controls)
                                    {
                                        if ((ctl3 is C1DockingTab) && (ctl3.Name.Equals("tC3")))
                                        {
                                            foreach (Control ctl4 in ctl3.Controls)
                                            {
                                                if ((ctl4 is C1DockingTabPage) && (ctl4.Name.Equals("tabsum")))
                                                {
                                                    foreach (Control ctl5 in ctl4.Controls)
                                                    {
                                                        if ((ctl5 is ListBox) && (ctl5.Name.Equals("listBoxSum3")))
                                                        {
                                                            listsum = (ListBox)ctl5;
                                                            listsum.Items.Clear();
                                                            int i = 0;
                                                            foreach (String txt in lPaid)
                                                            {
                                                                listsum.Items.Add(txt + "  จำนวน " + lPaidCnt[i] + " ไม่พบ จำนวน " + lPaidCntErr[i]);
                                                                i++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //listsum.Items.Clear();
            //int i = 0;
            //foreach (String txt in lPaid)
            //{
            //    listsum.Items.Add(txt + "  จำนวน " + lPaidCnt[i]+" ไม่พบ จำนวน "+ lPaidCntErr[i]);
            //    i++;
            //}

            pB1.Hide();
            label18.Text = lItm.Count.ToString();
            label17.Text = cnt.ToString();
            label16.Text = cntErr.ToString();
            label15.Text = sum.ToString("#,###.00");
            label23.Text = net.ToString("#,###.00");
            label26.Text = minus.ToString("#,###.00");
            btnTab3.Enabled = true;
            pB2.Hide();
            tC.SelectedTab = tab3;
            setGrf();
            //DataTable dt11 = new DataTable();
            dtChkGrp = GroupBy("paidtype", "paidtype", dtChk);
            setGrf11(dtChkGrp);
            if (dtChkGrp.Rows.Count > 0)
            {
                foreach (DataRow row in dtChkGrp.Rows)
                {
                    C1DockingTabPage tab1 = new C1DockingTabPage();
                    tab1.Text = row["paidtype"].ToString();
                    tC3.TabPages.Add(tab1);
                    C1FlexGrid grf = new C1FlexGrid();
                    grf.Font = fEdit;
                    grf.Dock = System.Windows.Forms.DockStyle.Fill;
                    grf.Location = new System.Drawing.Point(0, 0);
                    tab1.Controls.Add(grf);
                    DataRow[] result = dtChk.Select("paidtype = '"+ row["paidtype"].ToString() + "'");
                    DataTable dt1 = new DataTable();
                    addColumn(dt1);
                    foreach(DataRow row1 in result)
                    {
                        DataRow row2 = dt1.NewRow();
                        ///row2 = row1;
                        row2["row1"] = int.Parse(row1["row1"].ToString())+1;
                        row2["lab_code"] = row1["lab_code"];
                        row2["lab_name"] = row1["lab_name"];
                        row2["qty"] = row1["qty"];
                        row2["price"] = row1["price"];
                        row2["net_price"] = row1["net_price"];
                        row2["amount"] = row1["amount"];
                        row2["paidtype"] = row1["paidtype"];
                        row2["status_chk"] = row1["status_chk"];
                        row2["ptt_name"] = row1["ptt_name"];
                        row2["date"] = row1["date"];
                        row2["hn"] = row1["hn"];
                        row2["col1"] = row1["col1"];
                        dt1.Rows.Add(row2);
                    }
                    DataTable dt2 = new DataTable();
                    dt2 = GroupBy1("lab_code", "lab_code", dt1);
                    foreach (DataRow row3 in dt2.Rows)
                    {
                        DataRow[] result1 = dtChk.Select("lab_code = '"+ row3 ["lab_code"].ToString()+ "'");
                        if (result.Length > 0)
                        {
                            row3["lab_name"] = result1[0]["lab_name"].ToString();
                        }
                        Console.WriteLine("{0}, {1}", row[0], row[1]);
                    }
                    //dt1.ad
                    grf.DataSource = dt2;
                    grf.Cols["price"].Visible = false;
                    grf.Cols["net_price"].Visible = false;
                    grf.Cols["amount"].Visible = false;
                    grf.Cols["lab_code"].Width = 100;
                    grf.Cols["lab_name"].Width = 300;
                    C1Theme theme = C1ThemeController.GetThemeByName("Office2013Red", false);
                    C1ThemeController.ApplyThemeToObject(grf, theme);
                }
            }
            //pB1.Show();
        }
        public DataTable GroupBy(string i_sGroupByColumn, string i_sAggregateColumn, DataTable i_dSourceTable)
        {

            DataView dv = new DataView(i_dSourceTable);

            //getting distinct values for group column
            DataTable dtGroup = dv.ToTable(true, new string[] { i_sGroupByColumn });

            //adding column for the row count
            dtGroup.Columns.Add("Count", typeof(int));

            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtGroup.Rows)
            {
                dr["Count"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
            }

            //returning grouped/counted result
            return dtGroup;
        }
        public DataTable GroupBy1(string i_sGroupByColumn, string i_sAggregateColumn, DataTable i_dSourceTable)
        {

            DataView dv = new DataView(i_dSourceTable);

            //getting distinct values for group column
            DataTable dtGroup = dv.ToTable(true, new string[] { i_sGroupByColumn });

            //adding column for the row count
            dtGroup.Columns.Add("lab_name", typeof(String));
            dtGroup.Columns.Add("Count", typeof(int));
            dtGroup.Columns.Add("price", typeof(String));
            dtGroup.Columns.Add("net_price", typeof(String));
            dtGroup.Columns.Add("amount", typeof(String));

            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtGroup.Rows)
            {
                dr["Count"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
            }

            //returning grouped/counted result
            return dtGroup;
        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int i = 0;
            Boolean findPaid = false;
            lItm = new List<String>();
            lPaid = new List<String>();
            lPaidCntErr = new List<int>();
            lPaidCnt = new List<int>();
            const Int32 BufferSize = 4096;
            dtChk.Clear();
            using (var fileStream = File.OpenRead(txtPath.Text))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                lItm.Clear();
                String line;
                try
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        findPaid = false;
                        String[] line1 = line.Split('|');
                        if (line1.Length <= 1) continue;
                        if (i == 17939)
                        {
                            String sql = "";
                        }
                        String hn = "", date = "", year = "", month = "", date1 = "", paidtype = "", labcode = "", name = "", fntype = "", price = "", labname = "", netprice = "", col1 = "";
                        if (i == 0)
                        {
                            int day = 0;
                            hn = line1[2];
                            date = line1[1];
                            year = date.Substring(date.Length - 4);
                            month = date.Substring(3, 2);
                            date1 = date.Substring(0, 2);
                            int.TryParse(date1, out day);
                            if (hn.Substring(0, 1).Equals("1"))
                            {
                                chkBn1.Checked = true;
                                chkBn2.Checked = false;
                                chkBn5.Checked = false;
                            }
                            else if (hn.Substring(0, 1).Equals("2"))
                            {
                                chkBn1.Checked = false;
                                chkBn2.Checked = true;
                                chkBn5.Checked = false;
                            }
                            else if (hn.Substring(0, 1).Equals("5"))
                            {
                                chkBn1.Checked = false;
                                chkBn2.Checked = false;
                                chkBn5.Checked = true;
                            }
                            cboYear.Text = year;
                            cboMonth.Text = bc.getMonth(month);
                            if (day <= 15)
                            {
                                cboPeriod.Text = "1";
                            }
                            else
                            {
                                cboPeriod.Text = "2";
                            }
                        }
                        col1 = line1[0];
                        hn = line1[2];
                        date = line1[1];
                        name = line1[3];
                        paidtype = line1[4];
                        price = line1[5];
                        labcode = line1[7];
                        labname = line1[8];
                        netprice = line1[6];
                        foreach (String paid in lPaid)
                        {
                            if (paid.Equals(paidtype))
                            {
                                findPaid = true;
                            }
                        }
                        if (!findPaid)
                        {
                            lPaid.Add(paidtype);
                            lPaidCntErr.Add(0);
                            lPaidCnt.Add(0);
                        }
                        DataRow row1 = dtChk.NewRow();
                        row1["row1"] = i;
                        row1["lab_code"] = labcode;
                        row1["lab_name"] = labname;
                        row1["qty"] = 1;
                        row1["price"] = price;
                        row1["net_price"] = netprice;
                        row1["amount"] = labcode;
                        row1["paidtype"] = paidtype;
                        row1["status_chk"] = "0";
                        row1["ptt_name"] = name;
                        row1["date"] = date;
                        row1["hn"] = hn;
                        row1["col1"] = col1;
                        //dtChk.Columns.Add("lab_name", typeof(String));
                        //dtChk.Columns.Add("qty", typeof(int));
                        //dtChk.Columns.Add("price", typeof(String));
                        //dtChk.Columns.Add("net_price", typeof(String));
                        //dtChk.Columns.Add("amount", typeof(String));
                        //dtChk.Rows.InsertAt(row1, i);
                        dtChk.Rows.Add(row1);
                        listBox1.Items.Add(line + "|0|0");
                        lItm.Add(line + "|0|0");
                        i++;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("error i = "+i+" "+ex.Message, "");
                }
                
                btnCheck.Enabled = true;
                label6.Text = "จำนวนข้อมูล "+i;
            }
            if (lPaid.Count > 0)
            {
                C1DockingTabPage tabsum = new C1DockingTabPage();
                C1DockingTabPage tabsum1 = new C1DockingTabPage();
                tabsum.Text = "สรุป";
                tabsum.Name = "tabsum";
                tabsum1.Text = "สรุป";
                tabsum1.Name = "tabsum1";
                tC1.TabPages.Add(tabsum1);
                tC3.TabPages.Add(tabsum);
                ListBox listsum = new ListBox();
                listsum.BackColor = System.Drawing.Color.White;
                listsum.Dock = System.Windows.Forms.DockStyle.Fill;
                listsum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
                listsum.FormattingEnabled = true;
                listsum.Name = "listBoxSum3";
                theme1.SetTheme(listsum, "(default)");
                ListBox listsum1 = new ListBox();
                listsum1.BackColor = System.Drawing.Color.White;
                listsum1.Dock = System.Windows.Forms.DockStyle.Fill;
                listsum1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
                listsum1.FormattingEnabled = true;
                listsum1.Name = "listBoxSum1";
                theme1.SetTheme(listsum1, "(default)");

                tabsum.Controls.Add(listsum);
                tabsum1.Controls.Add(listsum1);
                int ii = 0;
                foreach (String paid in lPaid)
                {
                    int cnt = 0;                    
                    //C1DockingTabPage tab = new C1DockingTabPage();
                    C1DockingTabPage tab1 = new C1DockingTabPage();
                    //tab.Text = paid;
                    tab1.Text = paid;
                    tC1.TabPages.Add(tab1);
                    //tC3.TabPages.Add(tab);
                    //ListBox list = new ListBox();
                    //list.BackColor = System.Drawing.Color.White;
                    //list.Dock = System.Windows.Forms.DockStyle.Fill;
                    //list.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
                    //list.FormattingEnabled = true;
                    //list.Name = "listBox1"+ paid;
                    //theme1.SetTheme(list, "(default)");
                    ListBox list1 = new ListBox();
                    list1.BackColor = System.Drawing.Color.White;
                    list1.Dock = System.Windows.Forms.DockStyle.Fill;
                    list1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
                    list1.FormattingEnabled = true;
                    list1.Name = "listBox1" + paid;
                    theme1.SetTheme(list1, "(default)");

                    //tab.Controls.Add(list);
                    tab1.Controls.Add(list1);
                    foreach (String txt in lItm)
                    {
                        String[] line1 = txt.Split('|');
                        String hn = "", date = "", year = "", month = "", date1 = "", paidtype = "";
                        paidtype = line1[4];
                        if (paid.Equals(paidtype))
                        {
                            //findPaid = true;
                            //list.Items.Add(txt);
                            list1.Items.Add(txt);
                            cnt++;
                        }
                    }
                    lPaidCnt[ii] = cnt;
                    listsum.Items.Add(paid+"  จำนวน "+ cnt);
                    listsum1.Items.Add(paid + "  จำนวน " + cnt);
                    ii++;
                }
            }
            panel8.Show();
        }

        private void BthPath_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog res = new OpenFileDialog();
            res.Filter = "Excel Files|*.xls;";
            res.Filter = "Text Files|*.txt;";
            if (res.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                txtPath.Text = res.FileName;
                //Do something
            }
            
            btnCheck.Enabled = false;
            btnOpen.Enabled = true;
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            label11.Text = chkBn1.Checked ? "bangna1" : chkBn2.Checked ? "bangna2" : chkBn5.Checked ? "bangna5" : "";
            label12.Text = cboYear.Text;
            label13.Text = cboMonth.Text;
            label14.Text = cboPeriod.Text;
            btnCheck2.Enabled = true;
            tC.SelectedTab = tab2;
        }
        private void initGrf()
        {
            grf2 = new C1FlexGrid();
            grf2.Font = fEdit;
            grf2.Dock = System.Windows.Forms.DockStyle.Fill;
            grf2.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfPosi);

            //grf2.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grfPosi_AfterRowColChange);
            //grf2.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfPosi_CellButtonClick);
            //grf2.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfPosi_CellChanged);

            panel10.Controls.Add(this.grf2);

            C1Theme theme = C1ThemeController.GetThemeByName("Office2013Red", false);
            C1ThemeController.ApplyThemeToObject(grf2, theme);
        }
        private void initGrf11()
        {
            grf11 = new C1FlexGrid();
            grf11.Font = fEdit;
            grf11.Dock = System.Windows.Forms.DockStyle.Fill;
            grf11.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfPosi);

            //grf2.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grfPosi_AfterRowColChange);
            //grf2.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfPosi_CellButtonClick);
            //grf2.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfPosi_CellChanged);

            panel11.Controls.Add(this.grf11);

            C1Theme theme = C1ThemeController.GetThemeByName("Office2013Red", false);
            C1ThemeController.ApplyThemeToObject(grf11, theme);
        }
        private void setGrf11(DataTable dt11)
        {
            grf11.Clear();
            //grf11.Rows.Count = 1;
            //grf11.Cols.Count = 4;
            //DataTable dt = ic.ivfDB.noteDB.selectByPttId(ptt.t_patient_id);

            grf11.DataSource = dt11;

            grf11.ShowCursor = true;

            grf2.Cols[1].Caption = "Note";
        }
        private void setGrf()
        {
            grf2.Clear();
            grf2.Rows.Count = 1;
            grf2.Cols.Count = 4;
            //DataTable dt = ic.ivfDB.noteDB.selectByPttId(ptt.t_patient_id);

            grf2.DataSource = dtChk;

            //grf2.Cols[colNoteId].Width = 250;
            //grf2.Cols[colNote].Width = 600;

            grf2.ShowCursor = true;

            grf2.Cols[1].Caption = "Note";

            int i = 1;
            //foreach (DataRow row in dt.Rows)
            //{
            //    grfNote[i, colNoteId] = row[ic.ivfDB.noteDB.note.note_id].ToString();
            //    grfNote[i, colNote] = row[ic.ivfDB.noteDB.note.note_1].ToString();
            //    grfNote[i, colNoteStatusAll] = row[ic.ivfDB.noteDB.note.status_all].ToString();
            //    i++;
            //}
            //grfNote.Cols[colNoteId].Visible = false;
            //grfNote.Cols[colNoteStatusAll].Visible = false;
            grf2.Cols[0].AllowEditing = false;
            grf2.Cols[1].AllowEditing = false;
            grf2.Cols[2].AllowEditing = false;
            grf2.Cols[3].AllowEditing = false;
            grf2.Cols[4].AllowEditing = false;
            grf2.Cols[5].AllowEditing = false;
            grf2.Cols[6].AllowEditing = false;
            grf2.Cols[7].AllowEditing = false;
            grf2.Cols[8].AllowEditing = false;

            theme1.SetTheme(grf2, "Office2016DarkGray");
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            string line = null;
            Size proposedSize = new Size(100, 100);  //maximum size you would ever want to allow
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?

            // Calculate the number of lines per page.
            linesPerPage = e.MarginBounds.Height / fEdit.GetHeight(e.Graphics);

            count++;
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = bc.iniC.hostname;
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);

            count++;
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = "ใบตรวจสอบ ข้อมูล LAB ";
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);

            count++;
            String date = "";
            date = System.DateTime.Now.Year + 543 + DateTime.Now.ToString("-MM-dd");
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = "วันที่ ตรวจ "+ date;
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);

            count++;
            
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = "ข้อมูลที่ใช้ตรวจ สาขา" + label11.Text + "  ปี "+ label12.Text +" เดือน "+ label13.Text +" งวด "+ label14.Text;
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);

            count++;
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = "จำนวนข้อมูลทั้งหมด " + label18.Text + " รายการ";
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);

            count++;
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = "จำนวนข้อมูล ที่ตรวจพบ " + label17.Text + " รายการ     มูลค่า วางบิล " + label15.Text;
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);

            count++;
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = "จำนวนข้อมูล ที่ตรวจพบ " + label17.Text + " รายการ    มูลค่า ตรวจพบ " + label23.Text;
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);

            count++;
            int ii = 0;
            foreach (String txt in lPaid)
            {
                count++;
                yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
                //line = "จำนวนข้อมูล ที่ตรวจไม่พบ " + label16.Text + " รายการ    มูลค่า ตรวจพบ " + label26.Text;
                line = txt + "  จำนวน " + lPaidCnt[ii] + " ไม่พบ จำนวน " + lPaidCntErr[ii];
                textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
                xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
                yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
                                                                    //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
                e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);
                //listsum.Items.Add(txt + "  จำนวน " + lPaidCnt[ii] + " ไม่พบ จำนวน " + lPaidCntErr[ii]);
                ii++;
            }

            count++;
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = "จำนวนข้อมูล ที่ตรวจไม่พบ " + label16.Text + " รายการ    มูลค่า ตรวจพบ " + label26.Text;
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);


            count++; count++; count++;
            yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));
            line = "จำนวนข้อมูล ที่ตรวจไม่พบ " + label16.Text + " รายการ    มูลค่า ตรวจพบ " + label26.Text;
            textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, xOffset, yPos, new StringFormat());
            e.Graphics.DrawString(line, fEdit, Brushes.Black, leftMargin, yPos, flags);
            
            int page = 50, i=0;
            foreach (String txt in lItmE)
            {
                String[] itm = txt.Split('|');
                String col1 = "", date1 = "", hn = "", name = "", fntype = "", col6 = "", labname = "", labcode = "", labdate1 = "", labdate = "", labdateOld = "", price = "";
                col1 = itm[0];
                date1 = itm[1];
                hn = itm[2];
                name = itm[3];
                fntype = itm[4];
                col6 = itm[5];
                price = itm[6];
                labcode = itm[7];
                labname = itm[8];
                count++;
                yPos = topMargin + (count * fEdit.GetHeight(e.Graphics));                
                textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);
                xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
                yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
                e.Graphics.DrawString(date1 + " " + hn + " " + name + " " + labcode + " " + labname, fEdit, Brushes.Black, leftMargin, yPos, flags);
                i++;

                if (i >= page)
                {
                    e.HasMorePages = true;
                    i = 0;
                }
                else
                    e.HasMorePages = false;
            }
            //line = null;
        }

        private void FrmBillLabCheck_Load(object sender, EventArgs e)
        {
            tC.SelectedTab = tab1;
            tC.ShowTabs = false;

            this.Text = "07-12-21 " + bc.iniC.hostDB + " " + bc.iniC.nameDB;
        }
    }
}
