using bangna_hospital.control;
using bangna_hospital.object1;
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
    public partial class FrmEditJobNo : Form
    {
        BangnaControl bc;
        C1FlexGrid grfHn;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B;
        int colgrfHnHN = 1, colgrfHnDocNo=2, colgrfHnDocDate=3, colgrfHnDocCD=4, colgrfHnDocSts=5,colgrfHnAmt=6, colgrfHnJobNo=7, colgrfHnJobNoOld = 8;
        Boolean pageLoad = false;
        C1ThemeController theme1;
        Patient ptt;
        public FrmEditJobNo(BangnaControl bc)
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
            ptt = new Patient();
            initGrfHn();

            txtPttHn.KeyUp += TxtPttHn_KeyUp;
            btnGet.Click += BtnGet_Click;
            btnRevest.Click += BtnRevest_Click;
            c1Button1.Click += C1Button1_Click;
        }

        private void C1Button1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            long chk = 0;
            String sql = "";
            DataTable dtselect = new DataTable();
            DataTable dtpt08 = new DataTable();
            sql = "Select * From doc_scan Where status_record = '1' and status_ipd = 'I' ";
            dtselect = bc.conn.selectData(bc.conn.conn, sql);
            StringBuilder sbandate = new StringBuilder();
            StringBuilder sbhn = new StringBuilder();
            StringBuilder sban = new StringBuilder();
            StringBuilder sbanno = new StringBuilder();
            StringBuilder sbancnt = new StringBuilder();
            StringBuilder sbandatechk = new StringBuilder();
            StringBuilder sbdsnid = new StringBuilder();
            rb1.Text = "Count = " + dtselect.Rows.Count.ToString();
            foreach (DataRow dr in dtselect.Rows)
            {
                sbandate.Clear();
                sbhn.Clear();
                sban.Clear();
                sbancnt.Clear();
                sbanno.Clear();
                sbdsnid.Clear();
                sbandate.Append(dr["an_date"].ToString());
                sbhn.Append(dr["hn"].ToString());
                sban.Append(dr["an"].ToString());
                sbdsnid.Append(dr["doc_scan_id"].ToString());
                String[] an1 = sban.ToString().Split('/');
                if (an1.Length > 1)
                {
                    sbanno.Append(an1[0]);
                    sbancnt.Append(an1[1]);
                    //sbandate.Append(dr["an_date"].ToString());
                    sql = "Select convert(varchar(20),MNC_AD_DATE,23) as MNC_AD_DATE from PATIENT_T08 " +
                        "Where MNC_HN_NO = '" + sbhn.ToString() + "' and MNC_AN_NO = '"+ sbanno.ToString()+ "' and MNC_AN_YR = '"+ sbancnt.ToString()+"' ";
                    dtpt08 = bc.conn.selectData(bc.conn.connMainHIS, sql);
                    if (dtpt08.Rows.Count > 0)
                    {
                        sbandatechk.Clear();
                        sbandatechk.Append(dtpt08.Rows[0]["MNC_AD_DATE"].ToString());
                        if (!sbandatechk.ToString().Equals(sbandate.ToString()))
                        {
                            sql = "Update doc_scan set an_date = '"+ sbandatechk.ToString()+"' Where doc_scan_id = '"+sbdsnid.ToString()+"' ";
                            chk++;
                            bc.conn.ExecuteNonQuery(bc.conn.conn,sql);
                        }
                    }
                }
            }
            rb2.Text = chk.ToString();
        }

        private void BtnRevest_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.fint01DB.revestJobNo(txtPttHn.Text.Trim(), txtDocNo.Text.Trim(), txtJobNoOld.Text.Trim());
            if (int.TryParse(re, out int chk))
            {
                MessageBox.Show("OK ทำคืนเข้าเวรเก่า เรียบร้อย ", "");
            }
        }
        private void BtnGet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.fint01DB.setJobNo(txtPttHn.Text.Trim(), txtDocNo.Text.Trim(), txtJobNoCur.Text.Trim());
            if(int.TryParse(re, out int chk))
            {
                MessageBox.Show("OK ดึงกลับมาเวรปัจจุบัน เรียบร้อย ", "");
            }
        }
        private void initGrfHn()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);
            grfHn.Rows.Count = 1;
            grfHn.Cols.Count = 9;
            grfHn.Cols[colgrfHnHN].Width = 100;
            grfHn.Cols[colgrfHnDocNo].Width = 100;
            grfHn.Cols[colgrfHnDocDate].Width = 110;
            grfHn.Cols[colgrfHnDocCD].Width = 100;
            grfHn.Cols[colgrfHnDocSts].Width = 60;
            grfHn.Cols[colgrfHnAmt].Width = 80;
            grfHn.Cols[colgrfHnJobNo].Width = 60;
            grfHn.Cols[colgrfHnJobNoOld].Width = 60;
            grfHn.ShowCursor = true;
            grfHn.Cols[colgrfHnHN].Caption = "hn";
            grfHn.Cols[colgrfHnDocNo].Caption = "DOC NO";
            grfHn.Cols[colgrfHnDocDate].Caption = "Doc date";
            grfHn.Cols[colgrfHnDocCD].Caption = "DOC CD";
            grfHn.Cols[colgrfHnDocSts].Caption = "STS";
            grfHn.Cols[colgrfHnAmt].Caption = "AMT";
            grfHn.Cols[colgrfHnJobNo].Caption = "jbono";
            grfHn.Cols[colgrfHnJobNoOld].Caption = "jbonoold";

            grfHn.Cols[colgrfHnHN].AllowEditing = false;
            grfHn.Cols[colgrfHnDocNo].AllowEditing = false;
            grfHn.Cols[colgrfHnDocDate].AllowEditing = false;
            grfHn.Cols[colgrfHnDocCD].AllowEditing = false;
            grfHn.Cols[colgrfHnDocSts].AllowEditing = false;
            grfHn.Cols[colgrfHnAmt].AllowEditing = false;
            grfHn.Cols[colgrfHnJobNo].AllowEditing = false;
            grfHn.Cols[colgrfHnJobNoOld].AllowEditing = false;
            grfHn.AllowFiltering = true;

            grfHn.Click += GrfHn_Click;


            panel1.Controls.Add(grfHn);
            theme1.SetTheme(grfHn, bc.iniC.themeApp);
        }

        private void GrfHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHn.Rows == null) return;
            if(grfHn.Cols == null) return;
            if(grfHn.Row <= 0) return;
            if(grfHn.Col <= 0) return;

            txtDocNo.Value = grfHn[grfHn.Row, colgrfHnDocNo].ToString();
            txtJobNo.Value = grfHn[grfHn.Row, colgrfHnJobNo].ToString();
            txtJobNoOld.Value = grfHn[grfHn.Row, colgrfHnJobNoOld].ToString();
        }
        private void setGrfPttHn()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.fint01DB.SelectAllByHN(txtPttHn.Text.Trim());
            grfHn.Rows.Count = 1;
            grfHn.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfHn.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfHn.Rows[i];
                rowa[colgrfHnHN] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfHnDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfHnDocDate] = bc.datetoShow(row1["MNC_DOC_DAT"].ToString());
                rowa[colgrfHnDocCD] = row1["mnc_DOC_CD"].ToString();
                rowa[colgrfHnDocSts] = row1["mnc_DOC_STS"].ToString();
                rowa[colgrfHnAmt] = row1["mnc_SUM_PRI"].ToString();
                rowa[colgrfHnJobNo] = row1["MNC_JOB_NO"].ToString();
                rowa[colgrfHnJobNoOld] = row1["MNC_JOB_NOold"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
        }
        private void TxtPttHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByHn(txtPttHn.Text.Trim());
                lbVsPttNameT.Text = ptt.Name;
                txtJobNoCur.Value = bc.bcDB.fint01DB.SelectJOBNOCurrent();
                setGrfPttHn();
            }
        }

        private void FrmEditJobNo_Load(object sender, EventArgs e)
        {
            lb1.Text = "[hostDB " + bc.iniC.hostDB + " nameDB " + bc.iniC.nameDB + "] [hostDBMainHIS " + bc.iniC.hostDBMainHIS + " nameDBMainHIS " + bc.iniC.nameDBMainHIS+"]";
        }
    }
}
