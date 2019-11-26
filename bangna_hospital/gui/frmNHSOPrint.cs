using bangna_hospital.control;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class frmNHSOPrint : Form
    {
        BangnaControl bc;
        C1FlexGrid grfVs;
        Font fEdit, fEditB;
        C1DockingTabPage tabVs;
        int colHn = 1, colName = 2, colDate = 3, colVn = 4, colPreno = 5, colAn=6, colPaid=7, colDoctor=8, colPdf=9, colDob=10, colDateDisc=11, colWeight=12, colID=13, colChk=14;
        int colGrfPhTn = 1, colGrfQty = 2, colGrfPrice = 3, colGrfAmt = 4, colGrfDate = 5;
        public frmNHSOPrint(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            theme1.SetTheme(tC, bc.iniC.themeApplication);
            theme1.SetTheme(groupBox1, bc.iniC.themeApplication);
            foreach (Control con in groupBox1.Controls)
            {
                theme1.SetTheme(con, bc.iniC.themeApplication);
            }
            btnSearch.Click += BtnSearch_Click;
            txtAn.KeyUp += TxtAn_KeyUp;

            initGrf();
        }

        private void TxtAn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                setGrfVs();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfVs();
        }

        private void GrfVs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            setControlGrf();
        }
        private void setControlGrf()
        {
            String id = "", vn = "", an = "", preno = "", vsdate = "", hn = "", name = "", dateDisc="", chk="";
            if (grfVs.Row <= 0) return;
            if (grfVs.Col <= 0) return;

            id = grfVs[grfVs.Row, colID] != null ? grfVs[grfVs.Row, colID].ToString() : "";
            vn = grfVs[grfVs.Row, colVn] != null ? grfVs[grfVs.Row, colVn].ToString() : "";
            an = grfVs[grfVs.Row, colAn] != null ? grfVs[grfVs.Row, colAn].ToString() : "";
            preno = grfVs[grfVs.Row, colPreno] != null ? grfVs[grfVs.Row, colPreno].ToString() : "";
            vsdate = grfVs[grfVs.Row, colDate] != null ? grfVs[grfVs.Row, colDate].ToString() : "";
            hn = grfVs[grfVs.Row, colHn] != null ? grfVs[grfVs.Row, colHn].ToString() : "";
            name = grfVs[grfVs.Row, colName] != null ? grfVs[grfVs.Row, colName].ToString() : "";
            dateDisc = grfVs[grfVs.Row, colDateDisc] != null ? grfVs[grfVs.Row, colDateDisc].ToString() : "";
            chk = grfVs[grfVs.Row, colChk] != null ? grfVs[grfVs.Row, colChk].ToString() : "";

            tabVs = new C1DockingTabPage();
            tabVs.Location = new System.Drawing.Point(1, 24);
            tabVs.Name = "c1DockingTabPage1";
            tabVs.Size = new System.Drawing.Size(667, 175);
            tabVs.TabIndex = 0;
            tabVs.Text = name;
            tabVs.Name = "tabVs_" + vn;
            tC.Controls.Add(tabVs);
            Panel pn = new Panel();
            pn.Dock = DockStyle.Fill;
            pn.Name = "pn_" + vn;
            tabVs.Controls.Add(pn);
            C1FlexGrid grf = new C1FlexGrid();
            grf.Dock = DockStyle.Fill;
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            //grf.Rows[0].Visible = false;
            //grf.Cols[0].Visible = false;
            grf.Rows.Count = 1;
            grf.Name = "grf_" + vn;
            pn.Controls.Add(grf);
            theme1.SetTheme(grf, bc.iniC.themeApplication);
            setGrf(hn, vn, preno, dateDisc, chk, an, grf);
        }
        private void setGrf(String hn, String vn, String preno,String dsDate, String chk, String an, C1FlexGrid grf)
        {
            
            String dsDate1 = "", dsTime = "", dsDate2="";
            dsDate1 = bc.bcDB.vsDB.selectDSDateAN(hn, vn, preno);
            String[] aa = dsDate.Split(',');
            if (aa.Length > 1)
            {
                dsDate2 = aa[0];
                //an = aa[1];
            }
            String[] bb = dsDate1.Split('*');
            if (bb.Length > 1)
            {
                dsDate2 = bb[0];
                dsTime = bb[1];
            }
            DataTable dt, dtt12;
            if (chk.Equals(""))
            {
                dt = bc.bcDB.vsDB.selectNHSOPrintHN("", hn, preno, vn);
            }
            else
            {
                dt = bc.bcDB.vsDB.selectNHSOPrintHNAll("", hn, preno, vn);
            }
            DateTime dtEnd = new DateTime();
            DateTime dtStart = new DateTime();
            String datestart = "", dateend = "", time3 = "";
            String[] an1 = an.Split('/');
            if (an1.Length >= 1)
            {
                dtt12 = bc.bcDB.vsDB.selectPatientOR(hn, preno, an1[0]);
                if (dtt12.Rows.Count > 0)
                {
                    datestart = dtt12.Rows[0]["MNC_OR_DATE_S"].ToString();
                    if (datestart.Length >= 10)
                    {
                        datestart = datestart.Substring(0, 10);
                        //dateend = datestart;
                        String time1 = "0" + dtt12.Rows[0]["MNC_OR_TIME_S"].ToString();
                        if (time1.Length > 2)
                        {
                            String time2 = time1.Substring(time1.Length - 2, 2);
                            //String time3 = "";
                            String time4 = time1.Substring(time1.Length - 4, 2);
                            time3 = " " + time4 + ":" + time2;

                            if (DateTime.TryParse(datestart + time3, out dtStart))
                            {
                                dtEnd = dtStart;
                                String time5 = "";
                                time5 = dtt12.Rows[0]["MNC_OR_HOUR"].ToString();
                                int cnt = 0;
                                if (int.TryParse(time5, out cnt))
                                {
                                    int cnt1 = 0, cnt2 = 0, cnt0 = 0;
                                    cnt1 = cnt / 60;
                                    cnt2 = cnt % 60;
                                    dtEnd = dtEnd.AddHours(cnt1);
                                    dtEnd = dtEnd.AddMinutes(cnt2);
                                    dateend = dtEnd.ToString("HH:mm");
                                }
                            }
                        }
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                grf.Rows.Count = dt.Rows.Count + 1;
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    grf[i, 0] = (i + 1);
                    grf[i, colGrfPhTn] = dt.Rows[i-1]["MNC_PH_TN"].ToString() + " [" + dt.Rows[i - 1]["MNC_PH_cd"].ToString() + "]";
                    grf[i, colGrfQty] = dt.Rows[i - 1]["qty"].ToString();
                    grf[i, colGrfPrice] = dt.Rows[i - 1]["MNC_PH_PRI01"].ToString();
                    grf[i, colGrfAmt] = dt.Rows[i - 1]["amt"].ToString();
                    //grf[i, colHn] = dt.Rows[i]["amt"].ToString();
                    grf[i, colGrfDate] = bc.dateDBtoShowShort(bc.datetoDB(dt.Rows[i - 1]["MNC_CFG_DAT"].ToString())) + " " + time3 + "-" + dateend;
                }
            }
        }
        private void initGrf()
        {
            grfVs = new C1FlexGrid();
            grfVs.Font = fEdit;
            grfVs.Dock = System.Windows.Forms.DockStyle.Fill;
            grfVs.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);
            grfVs.Rows.Count = 1;
            grfVs.MouseDoubleClick += GrfVs_MouseDoubleClick;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            panel1.Controls.Add(grfVs);
            //grfVs.Rows[0].Visible = false;
            //grfVs.Cols[0].Visible = false;
            theme1.SetTheme(grfVs, bc.iniC.themeApplication);

        }
        private void setGrfVs()
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            //pB1.Visible = true;
            String sql = "", datestart = "", dateend = "";
            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 12);
            DataTable dt = new DataTable();

            grfVs.Cols.Count = 15;
            //grfVs.Rows.cl
            grfVs.Rows.Count = 1;

            grfVs.Cols[colHn].Width = 80;
            grfVs.Cols[colName].Width = 300;
            grfVs.Cols[colDate].Width = 110;
            grfVs.Cols[colVn].Width = 65;
            grfVs.Cols[colPreno].Width = 50;
            grfVs.Cols[colAn].Width = 80;

            grfVs.Cols[colHn].Caption = "HN";
            grfVs.Cols[colName].Caption = "Name";
            grfVs.Cols[colDate].Caption = "Date";
            grfVs.Cols[colDateDisc].Caption = "Date Disc";
            grfVs.Cols[colVn].Caption = "Vn";
            grfVs.Cols[colPreno].Caption = "preno";
            grfVs.Cols[colAn].Caption = "An";
            grfVs.Cols[colID].Caption = "ID";
            grfVs.Cols[colWeight].Caption = "Wright";

            if (txtAn.Text.Trim().Length > 0)
            {
                dt = bc.bcDB.vsDB.selectNHSOPrint3(txtAn.Text.Trim());
            }
            else
            {
                dt = bc.bcDB.vsDB.selectNHSOPrint(datestart, dateend,"");
            }
            
            
            //pB1.Maximum = dt.Rows.Count;
            //dgvPE.Columns[colPEId].HeaderText = "id";
            if (dt.Rows.Count > 0)
            {
                if (txtAn.Text.Length > 0)
                {
                    String where = "";
                    int len = txtAn.Text.Trim().Length;
                    //if (txtAn.Text.IndexOf(",") <= 0)
                    //{
                    //    where = " mnc_an_no = '" + txtAn.Text + "' ";
                    //    DataRow[] dr = dt.Select(where);
                        
                    //    int i = 0;
                    //    if (dr.Length == 0)
                    //    {
                    //        MessageBox.Show("ไม่พบ AN " + txtAn.Text + " ที่ต้องการค้นหา", "1.");
                    //        return;
                    //    }
                    //    grfVs.Rows.Count = dr.Length + 1;

                    //    foreach (DataRow row in dr)
                    //    {
                    //        if (dr[i]["mnc_an_no"].ToString().Equals("21364"))
                    //        {
                    //            sql = "";
                    //        }
                    //        grfVs[i+1,0] = (i + 1);
                    //        grfVs[i + 1, colHn] = dr[i]["mnc_hn_no"].ToString();
                    //        grfVs[i + 1, colName] = dr[i]["MNC_PFIX_DSC"].ToString() + " " + dr[i]["MNC_FNAME_T"].ToString() + " " + dr[i]["MNC_LNAME_T"].ToString() + " [" + dr[i]["MNC_id_no"].ToString() + "]";
                    //        grfVs[i + 1, colVn] = dr[i]["mnc_vn_no"].ToString() + "." + dr[i]["MNC_VN_SEQ"].ToString() + "." + dr[i]["MNC_VN_SUM"].ToString();
                    //        grfVs[i + 1, colPreno] = dr[i]["MNC_PRE_NO"].ToString();
                    //        grfVs[i + 1, colPaid] = bc.shortPaidName(dr[i]["MNC_FN_TYP_DSC"].ToString());
                    //        grfVs[i + 1, colDate] = bc.dateDBtoShowShort1(bc.datetoDB(dr[i]["MNC_DATE"].ToString())) + " " + bc.FormatTime(dr[i]["MNC_time"].ToString());
                    //        grfVs[i + 1, colDoctor] = dr[i]["prefix"].ToString() + " " + dr[i]["Fname"].ToString() + " " + dr[i]["Lname"].ToString() + " [" + dr[i]["mnc_dot_cd"].ToString() + "] ";
                    //        grfVs[i + 1, colPdf] = "PDF";
                    //        grfVs[i + 1, colDob] = bc.dateDBtoShowShort1(bc.datetoDB(dr[i]["MNC_BDAY"].ToString()));
                    //        grfVs[i + 1, colDateDisc] = bc.dateDBtoShowShort1(bc.datetoDB(dr[i]["mnc_ds_date"].ToString()));
                    //        grfVs[i + 1, colID] = dr[i]["mnc_id_no"].ToString();
                    //        grfVs[i + 1, colWeight] = dr[i]["mnc_weight"].ToString();
                    //        grfVs[i + 1, colAn] = dr[i]["mnc_an_no"].ToString() + "/" + dr[i]["mnc_an_yr"].ToString();
                    //        int cnt = bc.bcDB.vsDB.selectNHSOPrintHN2("", grfVs[i + 1, colHn].ToString(), grfVs[i + 1, colPreno].ToString(), grfVs[i + 1, colVn].ToString());
                    //        //if ((i % 2) != 0)
                    //        grfVs[i, colChk] = cnt > 0 ? "1" : "0";
                    //        //dgvView.Rows[i].DefaultCellStyle.BackColor = cnt > 0 ? Color.LightSalmon : Color.White;
                    //        i++;
                    //        //pB1.Value = i;

                    //    }
                    //}
                    //else
                    //{
                        String[] an = txtAn.Text.Split(',');
                        String wherean = "";
                        for (int j = 0; j < an.Length; j++)
                        {
                            wherean += "'" + an[j].Trim() + "',";
                        }
                        if (wherean.Length > 0)
                        {
                            if (wherean.IndexOf(',', wherean.Length - 1) > 0)
                            {
                                wherean = wherean.Substring(0, wherean.Length - 1);
                            }
                        }
                        where = " mnc_an_no in (" + wherean + ") ";
                        DataRow[] dr = dt.Select(where);
                        //pB1.Maximum = dr.Length;
                        int i = 0;
                        if (dr.Length == 0)
                        {
                            MessageBox.Show("ไม่พบ AN " + txtAn.Text + " ที่ต้องการค้นหา", "2.");
                            return;
                        }
                        grfVs.Rows.Count = dr.Length+1;
                        foreach (DataRow row in dr)
                        {
                            String hn = "", preno = "", vn = "";
                            hn = dr[i]["mnc_hn_no"].ToString();
                            preno = dr[i]["MNC_PRE_NO"].ToString();
                            vn = dr[i]["mnc_vn_no"].ToString() + "." + dr[i]["MNC_VN_SEQ"].ToString() + "." + dr[i]["MNC_VN_SUM"].ToString();
                            grfVs[i + 1, 0] = (i + 1);
                            grfVs[i + 1, colHn] = hn;
                            grfVs[i + 1, colName] = dr[i]["MNC_PFIX_DSC"].ToString() + " " + dr[i]["MNC_FNAME_T"].ToString() + " " + dr[i]["MNC_LNAME_T"].ToString() + " [" + dr[i]["MNC_id_no"].ToString() + "]";
                            grfVs[i + 1, colVn] = vn;
                            grfVs[i + 1, colPreno] = preno;
                            grfVs[i + 1, colPaid] = bc.shortPaidName(dr[i]["MNC_FN_TYP_DSC"].ToString());
                            grfVs[i + 1, colDate] = bc.dateDBtoShowShort(bc.datetoDB(dr[i]["MNC_DATE"].ToString())) + " " + bc.FormatTime(dr[i]["MNC_time"].ToString());
                            grfVs[i + 1, colDoctor] = dr[i]["prefix"].ToString() + " " + dr[i]["Fname"].ToString() + " " + dr[i]["Lname"].ToString() + " [" + dr[i]["mnc_dot_cd"].ToString() + "] ";
                            grfVs[i + 1, colPdf] = "PDF";
                            grfVs[i + 1, colDob] = bc.dateDBtoShowShort(bc.datetoDB(dr[i]["MNC_BDAY"].ToString()));
                            grfVs[i + 1, colDateDisc] = bc.dateDBtoShowShort(bc.datetoDB(dr[i]["mnc_ds_date"].ToString()));
                            grfVs[i + 1, colID] = dr[i]["mnc_id_no"].ToString();
                            grfVs[i + 1, colWeight] = dr[i]["mnc_weight"].ToString();
                            grfVs[i + 1, colAn] = dr[i]["mnc_an_no"].ToString() + "/" + dr[i]["mnc_an_yr"].ToString();
                            int cnt = bc.bcDB.vsDB.selectNHSOPrintHN2("", hn, preno, vn);
                            //if ((i % 2) != 0)
                            grfVs[i+1, colChk] = cnt > 0 ? "1" : "0";
                            //dgvView.Rows[i].DefaultCellStyle.BackColor = cnt > 0 ? Color.LightSalmon : Color.White;
                            i++;
                            //pB1.Value = i;

                        }
                    //}
                }
                else
                {
                    grfVs.Rows.Count = dt.Rows.Count + 1;
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["mnc_an_no"].ToString().Equals("21364"))
                        {
                            sql = "";
                        }
                        grfVs[i,0] = (i + 1);
                        grfVs[i, colHn] = dt.Rows[i]["mnc_hn_no"].ToString();
                        grfVs[i, colName] = dt.Rows[i]["MNC_PFIX_DSC"].ToString() + " " + dt.Rows[i]["MNC_FNAME_T"].ToString() + " " + dt.Rows[i]["MNC_LNAME_T"].ToString() + " [" + dt.Rows[i]["MNC_id_no"].ToString() + "]";
                        grfVs[i, colVn] = dt.Rows[i]["mnc_vn_no"].ToString() + "." + dt.Rows[i]["MNC_VN_SEQ"].ToString() + "." + dt.Rows[i]["MNC_VN_SUM"].ToString();
                        grfVs[i, colPreno] = dt.Rows[i]["MNC_PRE_NO"].ToString();
                        grfVs[i, colPaid] = bc.shortPaidName(dt.Rows[i]["MNC_FN_TYP_DSC"].ToString());
                        grfVs[i, colDate] = bc.dateDBtoShowShort(bc.datetoDB(dt.Rows[i]["MNC_DATE"].ToString())) + " " + bc.FormatTime(dt.Rows[i]["MNC_time"].ToString());
                        grfVs[i, colDoctor] = dt.Rows[i]["prefix"].ToString() + " " + dt.Rows[i]["Fname"].ToString() + " " + dt.Rows[i]["Lname"].ToString() + " [" + dt.Rows[i]["mnc_dot_cd"].ToString() + "] ";
                        grfVs[i, colPdf] = "PDF";
                        grfVs[i, colDob] = bc.dateDBtoShowShort(bc.datetoDB(dt.Rows[i]["MNC_BDAY"].ToString()));
                        grfVs[i, colDateDisc] = bc.dateDBtoShowShort(bc.datetoDB(dt.Rows[i]["mnc_ds_date"].ToString()));
                        grfVs[i, colID] = dt.Rows[i]["mnc_id_no"].ToString();
                        grfVs[i, colWeight] = dt.Rows[i]["mnc_weight"].ToString();
                        grfVs[i, colAn] = dt.Rows[i]["mnc_an_no"].ToString() + "/" + dt.Rows[i]["mnc_an_yr"].ToString();
                        int cnt = bc.bcDB.vsDB.selectNHSOPrintHN2("", grfVs[i, colHn].ToString(), grfVs[i, colPreno].ToString(), grfVs[i, colVn].ToString());
                        //if ((i % 2) != 0)
                        grfVs[i, colChk] = cnt > 0 ? "1" : "0";
                        //dgvView.Rows[i].DefaultCellStyle.BackColor = cnt > 0 ? Color.LightSalmon : Color.White;

                        //pB1.Value = i;
                    }
                }
            }

            for (int i = 0; i < grfVs.Rows.Count; i++)
            {
                if (grfVs.Rows.Count <= 1) break;
                if (grfVs[i, colChk] == null)
                {
                    continue;
                }
                if (grfVs[i, colChk].ToString().Equals("0"))
                {
                    grfVs.Rows.Remove(i);
                    i--;
                }
            }
            for (int i = 0; i < grfVs.Rows.Count; i++)
            {
                grfVs[i,0] = (i + 1);
            }

            grfVs.Font = font;
            grfVs.Cols[colPreno].Visible = false;
            grfVs.Cols[colChk].Visible = false;
            grfVs.Cols[colWeight].Visible = false;
            //dgvView.Columns[colAn].Visible = false;
            grfVs.Cols[colID].Visible = false;
            grfVs.Cols[colDob].Visible = false;

            grfVs.Cols[colHn].AllowEditing = false;
            grfVs.Cols[colName].AllowEditing = false;
            grfVs.Cols[colVn].AllowEditing = false;
            grfVs.Cols[colPreno].AllowEditing = false;
            grfVs.Cols[colPaid].AllowEditing = false;
            grfVs.Cols[colDate].AllowEditing = false;
            grfVs.Cols[colDoctor].AllowEditing = false;
            grfVs.Cols[colPdf].AllowEditing = false;
            grfVs.Cols[colDob].AllowEditing = false;
            grfVs.Cols[colDateDisc].AllowEditing = false;
            grfVs.Cols[colID].AllowEditing = false;
            grfVs.Cols[colWeight].AllowEditing = false;
            grfVs.Cols[colAn].AllowEditing = false;
            //pB1.Visible = false;
            Cursor.Current = cursor;
        }
        private void frmNHSOPrint_Load(object sender, EventArgs e)
        {

        }
    }
}
