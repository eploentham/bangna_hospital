using bangna_hospital.control;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
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
    public partial class FrmBillLabCheck : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;

        Color bg, fc;
        Font ff, ffB;
        int colID = 1, colName = 2;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        List<String> lItm;
        C1FlexGrid grf2;

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
            //theme1.SetTheme(sB, "BeigeOne");
            bc.setCboMonth(cboMonth);
            bc.setCboYear(cboYear);
            bc.setCboPeriod(cboPeriod);
            pic1.Image = Resources.Navigate_right;
            pic2.Image = Resources.Navigate_right;
            pic21.Image = Resources.Navigate_left;

            btnCheck.Click += BtnCheck_Click;
            bthPath.Click += BthPath_Click;
            btnOpen.Click += BtnOpen_Click;
            btnCheck2.Click += BtnCheck2_Click;
            btnCheck.Enabled = false;
            label6.Text = "";
            initGrf();
        }

        private void BtnCheck2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pB1.Show();
            pB1.Minimum = 0;
            pB1.Maximum = lItm.Count;
            foreach (String str in lItm)
            {
                try
                {
                    String[] itm = str.Split('|');
                    String col1 = "", date = "", hn = "", name = "", fntype="",col6="",col7="", labcode="", labdate1 = "",labdate = "", labdateOld="";
                    col1 = itm[0];
                    date = itm[1];
                    hn = itm[2];
                    name = itm[3];
                    fntype = itm[4];
                    col6 = itm[5];
                    col7 = itm[6];
                    labcode = itm[7];
                    labdate1 = (int.Parse(date.Substring(6)) - 543) + "-" + date.Substring(3, 2) + "-" + date.Substring(0, 2);
                    String sql = "";
                    sql = "select lab_t05.MNC_req_no,LAB_T01.MNC_PRE_NO " +
                        "from PATIENT_T01 " +
                        "inner join LAB_T01 on LAB_T01.mnc_hn_no = PATIENT_T01.mnc_hn_no " +
                        "and LAB_T01.mnc_pre_no = PATIENT_T01.mnc_pre_no " +
                        "and LAB_T01.mnc_date = PATIENT_T01.MNC_DATE " +
                        "inner join LAB_T05 on lab_t05.MNC_REQ_YR = lab_t01.MNC_REQ_YR " +
                        "and lab_t05.MNC_REQ_no = lab_t01.MNC_REQ_no " +
                        "and lab_t05.MNC_REQ_dat = lab_t01.MNC_REQ_dat " +
                        "where lab_t05.MNC_REQ_DAT >= '" + labdate1 + "' " +
                        "and lab_t05.MNC_REQ_DAT <= '" + labdate1 + "' " +
                        //"and patient_t01.MNC_STS = 'f' " +
                        //"and LAB_T01.MNC_REQ_STS = 'Q' " +
                        "and LAB_T01.mnc_hn_no ='" + hn + "' " +
                        "and lab_t05.mnc_lb_cd ='" + labcode + "'";
                    pB1.Value++;
                }
                catch (Exception ex)
                {

                }
            }
            pB1.Hide();
            //pB1.Show();
            //Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            //excelapp.Visible = false;
            ////String visitDate = "", visitTime = "", err = "", err1 = "", pharName = "";

            //Microsoft.Office.Interop.Excel._Workbook workbook = excelapp.Workbooks.Open(txtPath.Text);
            //Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
            //Microsoft.Office.Interop.Excel.Range xlRange = worksheet.UsedRange;
            //int rowCount = xlRange.Rows.Count;
            //int rowWork = 0, rowErr = 0, cntOK = 0, row1 = 9;
            //pB1.Minimum = 0;
            //pB1.Maximum = rowCount;
            //////worksheet.Cells[0, 0] = "patient name";
            //String hnOld = "", hn1 = "", flagBr = "", labdateOld = "", err = "", labcodeOld = "", price31 = "";
            //flagBr = chkBn1.Checked ? "1" : chkBn2.Checked ? "2" : chkBn5.Checked ? "5" : "";
            //ConnectDB conn = new ConnectDB("mainhis", flagBr);
            //conn.connMainHIS5.Open();
            //Config1 cf = new Config1();
            //Decimal noFind = 0, price3 = 0;
            //try
            //{
            //    DataTable dt = new DataTable();
            //    for (int i = 1; i < rowCount; i++)
            //    {
            //        try
            //        {
            //            if (i == 1) continue;
            //            String hn = worksheet.Cells[i, 3].Value != null ? worksheet.Cells[i, 3].Value.ToString() : "";
            //            String labdate = worksheet.Cells[i, 2].Value != null ? worksheet.Cells[i, 2].Value.ToString() : "";
            //            String labcode = worksheet.Cells[i, 6].Value != null ? worksheet.Cells[i, 6].Value.ToString() : "";
            //            price31 = worksheet.Cells[i, 10].Value != null ? worksheet.Cells[i, 10].Value.ToString() : "";
            //            if (hn.Equals(""))
            //            {
            //                hn = hnOld;
            //            }
            //            else
            //            {
            //                hnOld = hn;
            //            }
            //            if (labdate.Equals(""))
            //            {
            //                labdate = labdateOld;
            //            }
            //            else
            //            {
            //                labdateOld = labdate;
            //            }
            //            String labdate1 = "";
            //            if (labdate.Length < 10) continue;
            //            if (labcode.Equals(""))
            //            {
            //                labcode = labcodeOld;
            //            }
            //            else
            //            {
            //                labcodeOld = labcode;
            //            }
            //            //labdate1 = cf.datetoDB(labdate);
            //            labdate1 = (int.Parse(labdate.Substring(6)) - 543) + "-" + labdate.Substring(3, 2) + "-" + labdate.Substring(0, 2);

            //            String sql = "";
            //            sql = "select lab_t05.MNC_req_no,LAB_T01.MNC_PRE_NO " +
            //                "from PATIENT_T01 " +
            //                "inner join LAB_T01 on LAB_T01.mnc_hn_no = PATIENT_T01.mnc_hn_no " +
            //                "and LAB_T01.mnc_pre_no = PATIENT_T01.mnc_pre_no " +
            //                "and LAB_T01.mnc_date = PATIENT_T01.MNC_DATE " +
            //                "inner join LAB_T05 on lab_t05.MNC_REQ_YR = lab_t01.MNC_REQ_YR " +
            //                "and lab_t05.MNC_REQ_no = lab_t01.MNC_REQ_no " +
            //                "and lab_t05.MNC_REQ_dat = lab_t01.MNC_REQ_dat " +
            //                "where lab_t05.MNC_REQ_DAT >= '" + labdate1 + "' " +
            //                "and lab_t05.MNC_REQ_DAT <= '" + labdate1 + "' " +
            //                //"and patient_t01.MNC_STS = 'f' " +
            //                //"and LAB_T01.MNC_REQ_STS = 'Q' " +
            //                "and LAB_T01.mnc_hn_no ='" + hn + "' " +
            //                "and lab_t05.mnc_lb_cd ='" + labcode + "'";
            //            dt.Clear();
            //            dt = conn.selectDataNoClose(sql);
            //            if (dt.Rows.Count > 0)
            //            {
            //                cntOK++;
            //                worksheet.Cells[i, 16] = "ok";
            //                worksheet.Cells[i, 17] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //                worksheet.Cells[i, 18] = "MNC_req_no " + dt.Rows[0]["MNC_req_no"].ToString() + "; MNC_PRE_NO " + dt.Rows[0]["MNC_PRE_NO"].ToString();
            //            }
            //            else
            //            {
            //                //sql = "";
            //                row1++;
            //                worksheet.Cells[row1, 15] = "row " + i + "; hn " + hn + "; labdate1 " + labdate1 + "; labcode " + labcode + " price3 " + price31;
            //                worksheet.Cells[i, 16] = sql;
            //                //price31 = worksheet.Cells[i, 10].Value != null ? worksheet.Cells[i, 10].Value.toString() : 0;
            //                noFind += Decimal.Parse(price31);
            //            }

            //            pB1.Value = i;
            //            rowWork++;
            //        }
            //        catch (Exception ex)
            //        {
            //            rowErr++;
            //            //MessageBox.Show("Error " + ex.Message + "\n row " + i, "error " + err);
            //        }


            //    }
            //    worksheet.Cells[2, 15] = "year month period " + cboYear.Text + " " + cboMonth.Text + " " + cboPeriod.Text;
            //    worksheet.Cells[3, 15] = "row work " + rowWork;
            //    worksheet.Cells[4, 15] = "row error " + rowErr;
            //    worksheet.Cells[5, 15] = "row Excel " + rowCount;
            //    worksheet.Cells[6, 15] = "search find " + cntOK;
            //    worksheet.Cells[7, 15] = "search no find " + (rowCount - cntOK);
            //    worksheet.Cells[8, 15] = "amount no find " + noFind;

            //    GC.Collect();
            //    GC.WaitForPendingFinalizers();
            //    workbook.Save();
            //    workbook.Close();
            //    excelapp.Quit();
            //    conn.connMainHIS5.Close();
            //}
            //catch (Exception ex)
            //{
            //    err = ex.Message;
            //}

            //MessageBox.Show("Check Data เรียบร้อย ", "");

        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int i = 0;
            const Int32 BufferSize = 4096;
            using (var fileStream = File.OpenRead(txtPath.Text))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                lItm.Clear();
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (i == 0)
                    {
                        String[] line1 = line.Split('|');
                        String hn = "", date="", year="", month="", date1="";
                        int day = 0;
                        hn = line1[2];
                        date = line1[1];
                        year = date.Substring(date.Length - 4);
                        month = date.Substring(3,2);
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
                    listBox1.Items.Add(line);
                    lItm.Add(line);
                    i++;
                }
                btnCheck.Enabled = true;
                label6.Text = "จำนวนข้อมูล "+i;
            }
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
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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

            panel7.Controls.Add(this.grf2);

            C1Theme theme = C1ThemeController.GetThemeByName("Office2013Red", false);
            C1ThemeController.ApplyThemeToObject(grf2, theme);
        }
        private void FrmBillLabCheck_Load(object sender, EventArgs e)
        {
            tC.SelectedTab = tab1;
            tC.ShowTabs = false;
        }
    }
}
