using bangna_hospital.control;
using C1.C1Excel;
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
    public partial class FrmExcel : Form
    {
        BangnaControl bc;
        public FrmExcel(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            btnOpenExcel.Click += BtnOpenExcel_Click;
            btnReadExcel.Click += BtnReadExcel_Click;
            btnDrugCOpenExcel.Click += BtnDrugCOpenExcel_Click;
            btnDrugCRead.Click += BtnDrugCRead_Click;
        }

        private void BtnDrugCRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1.C1Excel.C1XLBook _c1xl = new C1.C1Excel.C1XLBook();
            _c1xl.Load(txtDrugCPathExcel.Text);
            XLSheet sheet = _c1xl.Sheets[0];

            for (int i = 0; i < sheet.Rows.Count; i++)
            {
                int chk = 0;
                String hosdrugcode = "", tmtcode = "", name = "", cid = "", sql = "", datestart = "", dateend = "";
                DateTime datestart1, dateend1;
                hosdrugcode = sheet[i, 0].Text;
                tmtcode = sheet[i, 2].Text;
                if ((hosdrugcode.Length > 0) && (tmtcode.Length>0))
                {
                    sql = "update pharmacy_m01 set tmt_code_opbkk = '"+tmtcode+"' Where mnc_ph_cd = '"+hosdrugcode+"' ";
                    String re = bc.conn.ExecuteNonQuery(bc.conn.connMainHIS, sql);
                    if(int.TryParse(re, out chk))
                    {
                        label1.Text = (int.Parse(label1.Text)+ chk).ToString();
                    }
                    else
                    {
                        label2.Text += (int.Parse(label2.Text)).ToString();
                    }
                }
                Application.DoEvents();
            }
        }

        private void BtnDrugCOpenExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xlsx",
                Filter = "excel files (*.xls)|*.xls",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDrugCPathExcel.Text = openFileDialog1.FileName;
            }
        }

        private void BtnReadExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1.C1Excel.C1XLBook _c1xl = new C1.C1Excel.C1XLBook();
            _c1xl.Load(txtPath.Text);
            XLSheet sheet = _c1xl.Sheets[0];

            for(int i =0; i < sheet.Rows.Count; i++)
            {
                int chk = 0;
                String no = "", vsdate = "", name = "", cid = "", sql="", datestart="", dateend="";
                DateTime datestart1, dateend1;
                no = sheet[i, 0].Text;
                if(int.TryParse(no, out chk))
                {
                    vsdate = sheet[i, 1].Text;
                    name = sheet[i, 2].Text.Trim();
                    cid = sheet[i, 5].Text.Replace("'", "");
                    if (cid.Length > 0)
                    {
                        sheet[i, 8].Value = "cid ok";
                        if (vsdate.Length >= 10)
                        {
                            datestart = vsdate.Substring(vsdate.Length-4, 4)+"-"+vsdate.Substring(3, 2)+"-"+ vsdate.Substring(0,2);
                            DateTime.TryParse(datestart, out datestart1);
                            DateTime.TryParse(datestart, out dateend1);
                            datestart1 = datestart1.AddDays(-3);
                            dateend1 = dateend1.AddDays(5);
                            datestart = datestart1.ToString("yyyy-MM-dd");
                            dateend = dateend1.ToString("yyyy-MM-dd");
                            sheet[i, 9].Value = "date ok";
                            sheet[i, 10].Value = datestart;
                            sheet[i, 11].Value = dateend;
                        }
                        
                        sql = "Select pt01.mnc_hn_no, labt05.MNC_LB_RES_CD,labt05.MNC_LB_CD,labt05.MNC_RES_VALUE , convert(VARCHAR(20),labt05.MNC_STAMP_DAT,23) as MNC_STAMP_DAT "
                        + "From bng5_dbms_front.dbo.patient_m01 pm01 " +
                        "inner Join patient_t01 pt01 on pm01.mnc_hn_no = pt01.mnc_hn_no and pm01.mnc_hn_yr = pt01.mnc_hn_yr " +
                        "left join LAB_T01 labt01 ON pt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pt01.MNC_DATE = labt01.MNC_DATE and pt01.MNC_hn_NO = labt01.MNC_hn_NO and pt01.MNC_hn_yr = labt01.MNC_hn_yr " +
                        "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT  " +
                        "inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  " +
                        "Where pm01.MNC_ID_NO= '" + cid + "' and labt02.MNC_LB_CD in ('SE629','SE184','SE640','SE649','SE650')  and pt01.mnc_date >= '"+ datestart + "' and pt01.mnc_date <= '"+ dateend + "'  " +
                        "Order By labt05.MNC_LB_RES_CD ";
                        DataTable dt = bc.conn.selectData(bc.conn.connMainHIS, sql);
                        if (dt.Rows.Count > 0)
                        {
                            sheet[i, 12].Value = dt.Rows[0][0].ToString();
                            sheet[i, 13].Value = dt.Rows[0][1].ToString();
                            sheet[i, 14].Value = dt.Rows[0][2].ToString();
                            sheet[i, 15].Value = dt.Rows[0][3].ToString();
                            sheet[i, 16].Value = dt.Rows[0][4].ToString();
                        }
                        else
                        {

                            sql = "Select mnc_hn_no " +
                                "From bng5_dbms_front.dbo.patient_m01 pm01 " +
                                //"inner Join patient_t01 pt01 on pm01.mnc_hn_no = pt01.mnc_hn_no and pm01.mnc_hn_yr = pt01.mnc_hn_yr " +
                                "Where pm01.MNC_FNAME_T like '%" + (name.Replace("MR","").Replace("MR.", "").Replace("Mr", "").Replace("Mr.", "").Replace("คุณ", "")).Replace(".", "").Trim() + "%'   " +
                                "";
                            DataTable dthn = bc.conn.selectData(bc.conn.connMainHIS, sql);
                            if (dthn.Rows.Count > 0)
                            {
                                sheet[i, 17].Value = dthn.Rows[0][0].ToString();
                                sql = "Select pt01.mnc_hn_no, labt05.MNC_LB_RES_CD,labt05.MNC_LB_CD,labt05.MNC_RES_VALUE , convert(VARCHAR(20),labt05.MNC_STAMP_DAT,23) as MNC_STAMP_DAT "
                                    + "From bng5_dbms_front.dbo.patient_m01 pm01 " +
                                    "inner Join patient_t01 pt01 on pm01.mnc_hn_no = pt01.mnc_hn_no and pm01.mnc_hn_yr = pt01.mnc_hn_yr " +
                                    "left join LAB_T01 labt01 ON pt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pt01.MNC_DATE = labt01.MNC_DATE and pt01.MNC_hn_NO = labt01.MNC_hn_NO and pt01.MNC_hn_yr = labt01.MNC_hn_yr " +
                                    "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT  " +
                                    "inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  " +
                                    "Where pm01.MNC_hn_NO = '" + dthn.Rows[0][0].ToString() + "' and labt02.MNC_LB_CD in ('SE629','SE184','SE640','SE649','SE650')  and pt01.mnc_date >= '" + datestart + "' and pt01.mnc_date <= '" + dateend + "'  " +
                                    "Order By labt05.MNC_LB_RES_CD ";
                                DataTable dtchk = bc.conn.selectData(bc.conn.connMainHIS, sql);
                                if (dtchk.Rows.Count > 0)
                                {
                                    sheet[i, 18].Value = dtchk.Rows[0][0].ToString();
                                    sheet[i, 19].Value = dtchk.Rows[0][1].ToString();
                                    sheet[i, 20].Value = dtchk.Rows[0][2].ToString();
                                    sheet[i, 21].Value = dtchk.Rows[0][3].ToString();
                                    sheet[i, 22].Value = dtchk.Rows[0][4].ToString();
                                }
                                else
                                {
                                    sql = "select MNC_AN_NO from patient_t08 where MNC_hn_NO = '" + dthn.Rows[0][0].ToString() + "' and mnc_date >= '" + datestart + "' and mnc_date <= '" + dateend + "'  ";
                                    DataTable dtadmit = bc.conn.selectData(bc.conn.connMainHIS, sql);
                                    if (dtadmit.Rows.Count > 0)
                                    {
                                        sheet[i, 23].Value = "admit "+dtchk.Rows[0][0].ToString();
                                    }
                                }
                            }
                        }
                    }

                }
                
            }
            _c1xl.Save(txtPath.Text);
        }

        private void BtnOpenExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xlsx",
                Filter = "excel files (*.xlsx)|*.xlsx",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void FrmExcel_Load(object sender, EventArgs e)
        {

        }
    }
}
