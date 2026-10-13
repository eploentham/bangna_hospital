using C1.Win.C1FlexGrid;
using bangna_hospital.control;
using bangna_hospital.object1;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmSearchPatient : Form
    {
        public enum SearchPatientMode { IPD, OPD, ALL }

        private BangnaControl bc;
        private C1FlexGrid grf;
        private Font fGrid, fGridB;
        private DataTable dtResults;
        private Timer searchTimer;

        public SearchPatientMode Mode { get; set; } = SearchPatientMode.IPD;

        // Return Properties
        public string SelectedHN { get; private set; } = "";
        public string SelectedName { get; private set; } = "";
        public string SelectedAN { get; private set; } = "";
        public string SelectedAnNo { get; private set; } = "";
        public string SelectedAnYr { get; private set; } = "";
        public string SelectedVN { get; private set; } = "";
        public string SelectedPreNo { get; private set; } = "";
        public string SelectedVisitDate { get; private set; } = "";
        public string SelectedAdmitDate { get; private set; } = "";
        public string SelectedDischargeDate { get; private set; } = "";
        public string SelectedWard { get; private set; } = "";
        public string SelectedRoom { get; private set; } = "";
        public string SelectedBed { get; private set; } = "";
        public string SelectedIdCard { get; private set; } = "";
        public Patient SelectedPatient { get; private set; }
        public DataRow SelectedRow { get; private set; }

        public FrmSearchPatient(BangnaControl bc) : this(bc, SearchPatientMode.IPD, "")
        {
        }

        public FrmSearchPatient(BangnaControl bc, SearchPatientMode mode) : this(bc, mode, "")
        {
        }

        public FrmSearchPatient(BangnaControl bc, SearchPatientMode mode, string initialSearch)
        {
            InitializeComponent();
            this.bc = bc;
            if (this.bc != null && this.bc.bcDB == null)
            {
                this.bc.bcDB = new objdb.BangnaHospitalDB(this.bc.conn);
            }
            this.Mode = mode;
            initConfig(initialSearch);
        }

        private void initConfig(string initialSearch)
        {
            fGrid = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fGridB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            theme1.Theme = bc.iniC.themeApplication;

            initGrid();

            // Set Mode dropdown
            if (Mode == SearchPatientMode.IPD)
                cboMode.SelectedIndex = 0;
            else if (Mode == SearchPatientMode.OPD)
                cboMode.SelectedIndex = 1;
            else
                cboMode.SelectedIndex = 2;

            dtpStart.Value = DateTime.Now.AddDays(-30);
            dtpEnd.Value = DateTime.Now;

            txtSearch.Text = initialSearch;

            // Auto-search Debounce Timer (350ms) เมื่อป้อนตั้งแต่ 3 ตัวอักษรขึ้นไป
            searchTimer = new Timer();
            searchTimer.Interval = 350;
            searchTimer.Tick += (s, e) => {
                searchTimer.Stop();
                string search = txtSearch.Text.Trim();
                if (search.Length >= 3 || search.Length == 0)
                {
                    DoSearch();
                }
            };

            txtSearch.TextChanged += (s, e) => {
                searchTimer.Stop();
                string search = txtSearch.Text.Trim();
                if (search.Length >= 3)
                {
                    lblStatus.Text = "กำลังรอค้นหา...";
                    searchTimer.Start();
                }
                else if (search.Length == 0)
                {
                    if (Mode == SearchPatientMode.IPD)
                    {
                        searchTimer.Start();
                    }
                    else
                    {
                        grf.Rows.Count = 1;
                        lblStatus.Text = "พิมพ์อย่างน้อย 3 ตัวอักษรเพื่อค้นหา";
                    }
                }
                else
                {
                    lblStatus.Text = string.Format("พิมพ์อย่างน้อย 3 ตัวอักษรเพื่อค้นหาอัตโนมัติ (ปัจจุบัน {0} ตัวอักษร)", search.Length);
                }
            };

            // Events
            btnSearch.Click += BtnSearch_Click;
            btnOk.Click += BtnOk_Click;
            cboMode.SelectedIndexChanged += CboMode_SelectedIndexChanged;
            chkActiveOnly.CheckedChanged += (s, e) => DoSearch();
            chkDateFilter.CheckedChanged += (s, e) => {
                dtpStart.Enabled = chkDateFilter.Checked;
                dtpEnd.Enabled = chkDateFilter.Checked;
                DoSearch();
            };

            txtSearch.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    searchTimer.Stop();
                    DoSearch();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (grf != null && grf.Rows.Count > 1)
                    {
                        grf.Focus();
                        grf.Row = 1;
                        e.Handled = true;
                    }
                }
            };

            this.KeyDown += FrmSearchPatient_KeyDown;

            dtpStart.Enabled = chkDateFilter.Checked;
            dtpEnd.Enabled = chkDateFilter.Checked;

            this.Load += (s, e) => {
                txtSearch.Focus();
                txtSearch.SelectAll();
                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    DoSearch();
                }
                else if (Mode == SearchPatientMode.IPD)
                {
                    // Auto-load current active IPD patients on open
                    DoSearch();
                }
            };
        }

        private void FrmSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (e.KeyCode == Keys.F2)
            {
                SelectAndClose();
            }
            else if (e.KeyCode == Keys.F3)
            {
                txtSearch.Focus();
                txtSearch.SelectAll();
            }
        }

        private void CboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMode.SelectedIndex == 0)
                Mode = SearchPatientMode.IPD;
            else if (cboMode.SelectedIndex == 1)
                Mode = SearchPatientMode.OPD;
            else
                Mode = SearchPatientMode.ALL;

            bool isIPD = (Mode == SearchPatientMode.IPD);
            chkActiveOnly.Visible = isIPD;
            chkDateFilter.Visible = isIPD;
            dtpStart.Visible = isIPD;
            dtpEnd.Visible = isIPD;

            DoSearch();
        }

        private void initGrid()
        {
            grf = new C1FlexGrid();
            grf.Font = fGrid;
            grf.Dock = DockStyle.Fill;
            grf.AllowEditing = false;
            grf.SelectionMode = SelectionModeEnum.Row;
            grf.Rows.Count = 1;
            grf.Cols.Count = 1;

            grf.DoubleClick += (s, e) => SelectAndClose();
            grf.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    SelectAndClose();
                    e.Handled = true;
                }
            };

            pnlGrid.Controls.Add(grf);
            theme1.SetTheme(grf, "Office2010Blue");
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            SelectAndClose();
        }

        public void DoSearch()
        {
            string search = txtSearch.Text.Trim();
            grf.Clear();
            lblStatus.Text = "กำลังค้นหาข้อมูล...";
            Application.DoEvents();

            try
            {
                if (Mode == SearchPatientMode.IPD)
                {
                    SearchIPD(search);
                }
                else if (Mode == SearchPatientMode.OPD)
                {
                    SearchOPD(search);
                }
                else
                {
                    SearchGeneral(search);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "เกิดข้อผิดพลาด: " + ex.Message;
                MessageBox.Show("เกิดข้อผิดพลาดในการค้นหา: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchIPD(string search)
        {
            string dateStart = "";
            string dateEnd = "";
            if (chkDateFilter.Checked)
            {
                dateStart = dtpStart.Value.ToString("yyyy-MM-dd");
                dateEnd = dtpEnd.Value.ToString("yyyy-MM-dd");
            }

            dtResults = bc.bcDB.pt08DB.SelectIPDPatients(search, chkActiveOnly.Checked, dateStart, dateEnd);

            SetupGridIPD();

            lblStatus.Text = string.Format("พบข้อมูลผู้ป่วยใน (IPD): {0} รายการ", dtResults != null ? dtResults.Rows.Count : 0);
        }

        private void SetupGridIPD()
        {
            grf.Rows.Count = 1;
            grf.Cols.Count = 13;

            grf.Cols[0].Caption = "ลำดับ";
            grf.Cols[1].Caption = "AN";
            grf.Cols[2].Caption = "HN";
            grf.Cols[3].Caption = "ชื่อ - นามสกุล";
            grf.Cols[4].Caption = "วันที่ Admit";
            grf.Cols[5].Caption = "วันที่จำหน่าย (D/C)";
            grf.Cols[6].Caption = "หอผู้ป่วย (Ward)";
            grf.Cols[7].Caption = "ห้อง";
            grf.Cols[8].Caption = "เตียง";
            grf.Cols[9].Caption = "อาการ / วินิจฉัย";
            grf.Cols[10].Caption = "PreNo";
            grf.Cols[11].Caption = "Visit Date";
            grf.Cols[12].Caption = "VN";

            grf.Cols[0].Width = 50;
            grf.Cols[1].Width = 90;
            grf.Cols[2].Width = 80;
            grf.Cols[3].Width = 200;
            grf.Cols[4].Width = 110;
            grf.Cols[5].Width = 110;
            grf.Cols[6].Width = 110;
            grf.Cols[7].Width = 60;
            grf.Cols[8].Width = 50;
            grf.Cols[9].Width = 180;
            grf.Cols[10].Width = 60;
            grf.Cols[11].Width = 90;
            grf.Cols[12].Width = 90;

            grf.Cols[10].Visible = false; // PreNo
            grf.Cols[11].Visible = false; // Visit Date
            grf.Cols[12].Visible = false; // VN

            if (dtResults == null || dtResults.Rows.Count == 0) return;

            for (int i = 0; i < dtResults.Rows.Count; i++)
            {
                DataRow dr = dtResults.Rows[i];
                Row row = grf.Rows.Add();
                row[0] = (i + 1);

                string an = dr["MNC_AN_NO"].ToString() + "/" + dr["MNC_AN_YR"].ToString();
                string fullName = dr["prefix"].ToString() + dr["MNC_FNAME_T"].ToString() + " " + dr["MNC_LNAME_T"].ToString();
                string dsDate = dr["MNC_DS_DATE"].ToString();
                if (dsDate.StartsWith("1900") || dsDate.StartsWith("0001")) dsDate = "";

                row[1] = an;
                row[2] = dr["MNC_HN_NO"].ToString();
                row[3] = fullName;
                row[4] = bc.datetoShow(dr["MNC_AD_DATE"].ToString());
                row[5] = string.IsNullOrEmpty(dsDate) ? "ยังพักรักษา" : bc.datetoShow(dsDate);
                row[6] = dr["ward_name"].ToString();
                row[7] = dr["MNC_RM_NAM"].ToString();
                row[8] = dr["MNC_BD_NO"].ToString();
                row[9] = dr["MNC_DIA_MEMO"].ToString();
                row[10] = dr["MNC_PRE_NO"].ToString();
                row[11] = dr["MNC_DATE"].ToString();
                row[12] = dr["MNC_VN_NO"].ToString();

                // Highlight active admissions
                if (string.IsNullOrEmpty(dsDate))
                {
                    CellStyle cs = grf.Styles.Add("ActiveIPD" + i);
                    cs.ForeColor = Color.DarkGreen;
                    cs.Font = fGridB;
                    grf.SetCellStyle(i + 1, 5, cs);
                }
            }

            if (grf.Rows.Count > 1)
            {
                grf.Row = 1;
            }
        }

        private void SearchOPD(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                lblStatus.Text = "กรุณาระบุคำค้นหา (HN, VN หรือ ชื่อผู้ป่วย)";
                return;
            }

            string sql = "SELECT TOP 100 " +
                "t01.MNC_HN_NO, " +
                "isnull(pm02.MNC_PFIX_DSC, '') as prefix, " +
                "m01.MNC_FNAME_T, " +
                "m01.MNC_LNAME_T, " +
                "convert(varchar(20), t01.MNC_DATE, 23) as MNC_DATE, " +
                "t01.MNC_VN_NO, " +
                "t01.MNC_VN_SEQ, " +
                "t01.MNC_VN_SUM, " +
                "t01.MNC_PRE_NO, " +
                "isnull(t01.MNC_SHIF_MEMO, '') as MNC_SHIF_MEMO, " +
                "isnull(m32.MNC_MD_DEP_DSC, t01.MNC_DEP_NO) as dept_name " +
                "FROM PATIENT_T01 t01 " +
                "INNER JOIN PATIENT_M01 m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
                "LEFT JOIN PATIENT_M02 pm02 ON m01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "LEFT JOIN PATIENT_M32 m32 ON t01.MNC_DEP_NO = m32.MNC_MD_DEP_NO " +
                "WHERE (t01.MNC_HN_NO LIKE '%" + search.Replace("'", "''") + "%' " +
                "OR m01.MNC_FNAME_T LIKE '%" + search.Replace("'", "''") + "%' " +
                "OR m01.MNC_LNAME_T LIKE '%" + search.Replace("'", "''") + "%') " +
                "ORDER BY t01.MNC_DATE DESC, t01.MNC_PRE_NO DESC";

            dtResults = bc.conn.selectData(sql);

            SetupGridOPD();

            lblStatus.Text = string.Format("พบข้อมูลผู้ป่วยนอก (OPD): {0} รายการ", dtResults != null ? dtResults.Rows.Count : 0);
        }

        private void SetupGridOPD()
        {
            grf.Rows.Count = 1;
            grf.Cols.Count = 9;

            grf.Cols[0].Caption = "ลำดับ";
            grf.Cols[1].Caption = "HN";
            grf.Cols[2].Caption = "ชื่อ - นามสกุล";
            grf.Cols[3].Caption = "Visit Date";
            grf.Cols[4].Caption = "VN";
            grf.Cols[5].Caption = "PreNo";
            grf.Cols[6].Caption = "แผนก (Dept)";
            grf.Cols[7].Caption = "อาการ / อาการสำคัญ";
            grf.Cols[8].Caption = "Seq";

            grf.Cols[0].Width = 50;
            grf.Cols[1].Width = 80;
            grf.Cols[2].Width = 220;
            grf.Cols[3].Width = 110;
            grf.Cols[4].Width = 100;
            grf.Cols[5].Width = 60;
            grf.Cols[6].Width = 150;
            grf.Cols[7].Width = 220;
            grf.Cols[8].Width = 40;

            grf.Cols[5].Visible = false;
            grf.Cols[8].Visible = false;

            if (dtResults == null || dtResults.Rows.Count == 0) return;

            for (int i = 0; i < dtResults.Rows.Count; i++)
            {
                DataRow dr = dtResults.Rows[i];
                Row row = grf.Rows.Add();
                row[0] = (i + 1);
                row[1] = dr["MNC_HN_NO"].ToString();
                row[2] = dr["prefix"].ToString() + dr["MNC_FNAME_T"].ToString() + " " + dr["MNC_LNAME_T"].ToString();
                row[3] = bc.datetoShow(dr["MNC_DATE"].ToString());
                row[4] = dr["MNC_VN_NO"].ToString() + "/" + dr["MNC_VN_SEQ"].ToString() + "(" + dr["MNC_VN_SUM"].ToString() + ")";
                row[5] = dr["MNC_PRE_NO"].ToString();
                row[6] = dr["dept_name"].ToString();
                row[7] = dr["MNC_SHIF_MEMO"].ToString();
            }

            if (grf.Rows.Count > 1)
            {
                grf.Row = 1;
            }
        }

        private void SearchGeneral(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                lblStatus.Text = "กรุณาระบุคำค้นหา (HN, เลขบัตรประชาชน หรือ ชื่อผู้ป่วย)";
                return;
            }

            string sql = "SELECT TOP 100 " +
                "m01.MNC_HN_NO, " +
                "isnull(pm02.MNC_PFIX_DSC, '') as prefix, " +
                "m01.MNC_FNAME_T, " +
                "m01.MNC_LNAME_T, " +
                "convert(varchar(20), m01.MNC_BDAY, 23) as MNC_BDAY, " +
                "m01.MNC_SEX, " +
                "m01.MNC_ID_NO, " +
                "isnull(m01.MNC_CUR_TEL, '') as MNC_CUR_TEL " +
                "FROM PATIENT_M01 m01 " +
                "LEFT JOIN PATIENT_M02 pm02 ON m01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "WHERE (m01.MNC_HN_NO LIKE '%" + search.Replace("'", "''") + "%' " +
                "OR m01.MNC_ID_NO LIKE '%" + search.Replace("'", "''") + "%' " +
                "OR m01.MNC_FNAME_T LIKE '%" + search.Replace("'", "''") + "%' " +
                "OR m01.MNC_LNAME_T LIKE '%" + search.Replace("'", "''") + "%') " +
                "ORDER BY m01.MNC_HN_NO DESC";

            dtResults = bc.conn.selectData(sql);

            grf.Rows.Count = 1;
            grf.Cols.Count = 7;
            grf.Cols[0].Caption = "ลำดับ";
            grf.Cols[1].Caption = "HN";
            grf.Cols[2].Caption = "ชื่อ - นามสกุล";
            grf.Cols[3].Caption = "วันเกิด (DOB)";
            grf.Cols[4].Caption = "เพศ";
            grf.Cols[5].Caption = "เลขบัตรประชาชน";
            grf.Cols[6].Caption = "เบอร์โทรศัพท์";

            grf.Cols[0].Width = 50;
            grf.Cols[1].Width = 90;
            grf.Cols[2].Width = 220;
            grf.Cols[3].Width = 110;
            grf.Cols[4].Width = 50;
            grf.Cols[5].Width = 140;
            grf.Cols[6].Width = 120;

            if (dtResults == null || dtResults.Rows.Count == 0) return;

            for (int i = 0; i < dtResults.Rows.Count; i++)
            {
                DataRow dr = dtResults.Rows[i];
                Row row = grf.Rows.Add();
                row[0] = (i + 1);
                row[1] = dr["MNC_HN_NO"].ToString();
                row[2] = dr["prefix"].ToString() + dr["MNC_FNAME_T"].ToString() + " " + dr["MNC_LNAME_T"].ToString();
                row[3] = bc.datetoShow(dr["MNC_BDAY"].ToString());
                row[4] = dr["MNC_SEX"].ToString();
                row[5] = dr["MNC_ID_NO"].ToString();
                row[6] = dr["MNC_CUR_TEL"].ToString();
            }

            if (grf.Rows.Count > 1)
            {
                grf.Row = 1;
            }

            lblStatus.Text = string.Format("พบข้อมูลผู้ป่วยทั่วไป: {0} รายการ", dtResults.Rows.Count);
        }

        private void SelectAndClose()
        {
            if (grf.Row < 1 || grf.Rows.Count <= 1 || dtResults == null || dtResults.Rows.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกรายการคนไข้ก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = grf.Row - 1;
            if (index < 0 || index >= dtResults.Rows.Count) return;

            SelectedRow = dtResults.Rows[index];

            if (Mode == SearchPatientMode.IPD)
            {
                SelectedHN = SelectedRow["MNC_HN_NO"].ToString();
                SelectedAnNo = SelectedRow["MNC_AN_NO"].ToString();
                SelectedAnYr = SelectedRow["MNC_AN_YR"].ToString();
                SelectedAN = SelectedAnNo + "/" + SelectedAnYr;
                SelectedName = (SelectedRow["prefix"].ToString() + SelectedRow["MNC_FNAME_T"].ToString() + " " + SelectedRow["MNC_LNAME_T"].ToString()).Trim();
                SelectedAdmitDate = bc.datetoShow(SelectedRow["MNC_AD_DATE"].ToString());
                SelectedDischargeDate = bc.datetoShow(SelectedRow["MNC_DS_DATE"].ToString());
                SelectedWard = SelectedRow["ward_name"].ToString();
                SelectedRoom = SelectedRow["MNC_RM_NAM"].ToString();
                SelectedBed = SelectedRow["MNC_BD_NO"].ToString();
                SelectedPreNo = SelectedRow["MNC_PRE_NO"].ToString();
                SelectedVisitDate = bc.datetoShow(SelectedRow["MNC_DATE"].ToString());
                SelectedVN = SelectedRow["MNC_VN_NO"].ToString();
                SelectedIdCard = SelectedRow["MNC_ID_NO"].ToString();
            }
            else if (Mode == SearchPatientMode.OPD)
            {
                SelectedHN = SelectedRow["MNC_HN_NO"].ToString();
                SelectedName = (SelectedRow["prefix"].ToString() + SelectedRow["MNC_FNAME_T"].ToString() + " " + SelectedRow["MNC_LNAME_T"].ToString()).Trim();
                SelectedVisitDate = bc.datetoShow(SelectedRow["MNC_DATE"].ToString());
                SelectedVN = SelectedRow["MNC_VN_NO"].ToString() + "/" + SelectedRow["MNC_VN_SEQ"].ToString() + "(" + SelectedRow["MNC_VN_SUM"].ToString() + ")";
                SelectedPreNo = SelectedRow["MNC_PRE_NO"].ToString();
            }
            else
            {
                SelectedHN = SelectedRow["MNC_HN_NO"].ToString();
                SelectedName = (SelectedRow["prefix"].ToString() + SelectedRow["MNC_FNAME_T"].ToString() + " " + SelectedRow["MNC_LNAME_T"].ToString()).Trim();
                SelectedIdCard = SelectedRow["MNC_ID_NO"].ToString();
            }

            // Sync with global bc.sPtt for full compatibility across all hospital modules
            bc.sPtt = new Patient();
            bc.sPtt.Hn = SelectedHN;
            bc.sPtt.Name = SelectedName;
            bc.sPtt.an = SelectedAN;
            bc.sPtt.anDate = SelectedAdmitDate;
            bc.sPtt.vn = SelectedVN;
            bc.sPtt.visitDate = SelectedVisitDate;
            bc.sPtt.preno = SelectedPreNo;
            bc.sPtt.statusIPD = (Mode == SearchPatientMode.IPD) ? "I" : "O";

            SelectedPatient = bc.sPtt;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
