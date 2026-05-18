using bangna_hospital.control;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmDrugHistory : Form
    {
        BangnaControl bc;
        string HN;
        C1FlexGrid grfVisit, grfDrug;
        Panel pnLeft, pnRight, pnTop;
        Button btnOK, btnCancel;
        
        // Property เพื่อ return ค่ารหัสยาที่เลือก
        public List<string> SelectedDrugCodes { get; private set; }
        
        // Column indexes
        int colVisitNo = 0, colVisitPreno = 1, colVisitDate = 2, colVisitSymptom = 3, colVisitDoctor = 4;
        int colDrugCode = 1, colDrugName = 2, colDrugQty = 3, colDrugUnit = 4, colDrugUsage = 5, colDrugFrequency = 6, colDrugPrecau=7;
        
        string selectedPreno = "", selectedVsDate = "";
        Font fEdit;

        public FrmDrugHistory(BangnaControl bc, string hn)
        {
            this.bc = bc;
            this.HN = hn;
            this.SelectedDrugCodes = new List<string>();
            
            InitializeComponent();
            initConfig();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form settings
            this.Text = "ประวัติการใช้ยา - HN: " + HN;            this.Size = new Size(1200, 700);            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;            this.MaximizeBox = false;            this.MinimizeBox = false;
            
            this.ResumeLayout(false);
        }

        private void initConfig()
        {
            fEdit = new Font("Tahoma", 10, FontStyle.Regular);
            
            // Top Panel สำหรับปุ่ม
            pnTop = new Panel();            pnTop.Dock = DockStyle.Top;            pnTop.Height = 50;            pnTop.BackColor = Color.WhiteSmoke;
            
            btnOK = new Button();            btnOK.Text = "ตกลง (เลือกยา)";            btnOK.Font = new Font(fEdit.FontFamily, 10, FontStyle.Bold);
            btnOK.Size = new Size(150, 35);            btnOK.Location = new Point(10, 8);
            btnOK.Click += BtnOK_Click;
            
            btnCancel = new Button();            btnCancel.Text = "ยกเลิก";            btnCancel.Font = fEdit;
            btnCancel.Size = new Size(100, 35);            btnCancel.Location = new Point(170, 8);
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            
            pnTop.Controls.Add(btnOK);
            pnTop.Controls.Add(btnCancel);
            
            // Left Panel
            pnLeft = new Panel();            pnLeft.Dock = DockStyle.Left;            pnLeft.Width = 550;
            pnLeft.BorderStyle = BorderStyle.FixedSingle;
            
            // Right Panel
            pnRight = new Panel();            pnRight.Dock = DockStyle.Fill;            pnRight.BorderStyle = BorderStyle.FixedSingle;
            
            // Add panels to form
            this.Controls.Add(pnRight);
            this.Controls.Add(pnLeft);
            this.Controls.Add(pnTop);
            
            // Initialize grids
            initGrfVisit();
            initGrfDrug();
            
            // Load data
            setGrfVisit();
        }

        private void initGrfVisit()
        {
            grfVisit = new C1FlexGrid();
            grfVisit.Font = fEdit;
            grfVisit.Dock = DockStyle.Fill;
            grfVisit.Rows.Count = 1;
            grfVisit.Cols.Count = 5;
            
            grfVisit.Cols[colVisitNo].Width = 50;
            grfVisit.Cols[colVisitPreno].Width = 80;
            grfVisit.Cols[colVisitDate].Width = 100;
            grfVisit.Cols[colVisitSymptom].Width = 200;
            grfVisit.Cols[colVisitDoctor].Width = 100;
            
            grfVisit.Cols[colVisitNo].Caption = "ลำดับ";
            grfVisit.Cols[colVisitPreno].Caption = "Preno";
            grfVisit.Cols[colVisitDate].Caption = "วันที่";
            grfVisit.Cols[colVisitSymptom].Caption = "อาการ";
            grfVisit.Cols[colVisitDoctor].Caption = "แพทย์";
            
            grfVisit.Cols[colVisitNo].AllowEditing = false;
            grfVisit.Cols[colVisitPreno].AllowEditing = false;
            grfVisit.Cols[colVisitDate].AllowEditing = false;
            grfVisit.Cols[colVisitSymptom].AllowEditing = false;
            grfVisit.Cols[colVisitDoctor].AllowEditing = false;
            
            grfVisit.SelectionMode = SelectionModeEnum.Row;
            grfVisit.Click += GrfVisit_Click;
            
            pnLeft.Controls.Add(grfVisit);
        }

        private void initGrfDrug()
        {
            grfDrug = new C1FlexGrid();
            grfDrug.Font = fEdit;
            grfDrug.Dock = DockStyle.Fill;
            grfDrug.Rows.Count = 1;
            grfDrug.Cols.Count = 8;
            
            grfDrug.Cols[0].Width = 50;
            grfDrug.Cols[colDrugCode].Width = 100;
            grfDrug.Cols[colDrugName].Width = 300;
            grfDrug.Cols[colDrugQty].Width = 80;
            grfDrug.Cols[colDrugUnit].Width = 80;
            grfDrug.Cols[colDrugUsage].Width = 150;
            grfDrug.Cols[colDrugFrequency].Width = 150;
            grfDrug.Cols[colDrugPrecau].Width = 150;

            grfDrug.Cols[0].Caption = "ลำดับ";
            grfDrug.Cols[colDrugCode].Caption = "รหัสยา";
            grfDrug.Cols[colDrugName].Caption = "ชื่อยา";
            grfDrug.Cols[colDrugQty].Caption = "จำนวน";
            grfDrug.Cols[colDrugUnit].Caption = "หน่วย";
            grfDrug.Cols[colDrugUsage].Caption = "วิธีใช้";
            grfDrug.Cols[colDrugFrequency].Caption = "ความถี่";
            grfDrug.Cols[colDrugPrecau].Caption = "ข้อควรระวัง";

            grfDrug.Cols[0].AllowEditing = false;
            grfDrug.Cols[colDrugCode].AllowEditing = false;
            grfDrug.Cols[colDrugName].AllowEditing = false;
            grfDrug.Cols[colDrugQty].AllowEditing = false;
            grfDrug.Cols[colDrugUnit].AllowEditing = false;
            grfDrug.Cols[colDrugUsage].AllowEditing = false;
            grfDrug.Cols[colDrugFrequency].AllowEditing = false;
            grfDrug.Cols[colDrugPrecau].AllowEditing = false;

            // ✅ MultiSelect Mode
            grfDrug.SelectionMode = SelectionModeEnum.ListBox;
            
            pnRight.Controls.Add(grfDrug);
        }

        private void setGrfVisit()
        {
            DataTable dt = bc.bcDB.vsDB.selectVN_VisitAllByHn(HN);
            grfVisit.Rows.Count = 1;
            
            if (dt.Rows.Count == 0) return;
            
            grfVisit.Rows.Count = dt.Rows.Count + 1;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row row = grfVisit.Rows[i + 1];
                row[colVisitNo] = (i + 1).ToString();
                row[colVisitPreno] = dt.Rows[i]["MNC_PRE_NO"]?.ToString() ?? "";
                row[colVisitDate] = bc.datetoShow1(dt.Rows[i]["MNC_DATE"]?.ToString() ?? "");
                row[colVisitSymptom] = dt.Rows[i]["MNC_SHIF_MEMO"]?.ToString() ?? "";
                row[colVisitDoctor] =  "";
            }
        }

        private void GrfVisit_Click(object sender, EventArgs e)
        {
            if (grfVisit.Row <= 0) return;
            
            selectedPreno = grfVisit[grfVisit.Row, colVisitPreno]?.ToString() ?? "";
            string displayDate = grfVisit[grfVisit.Row, colVisitDate]?.ToString() ?? "";
            
            // แปลงวันที่จาก dd-MM-yyyy กลับเป็น yyyy-MM-dd
            if (!string.IsNullOrEmpty(displayDate))
            {
                string[] parts = displayDate.Split('-');
                if (parts.Length == 3)
                {
                    selectedVsDate = $"{parts[2]}-{parts[1]}-{parts[0]}";
                }
            }
            
            setGrfDrug(selectedPreno, selectedVsDate);
        }

        private void setGrfDrug(string preno, string vsdate)
        {
            DataTable dt = bc.bcDB.pharT02DB.selectReq(HN, vsdate, preno);
            grfDrug.Rows.Count = 1;
            
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("ไม่พบรายการยาในการเข้าพบครั้งนี้", "แจ้งเตือน");
                return;
            }
            
            grfDrug.Rows.Count = dt.Rows.Count + 1;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row row = grfDrug.Rows[i + 1];
                row[0] = (i + 1).ToString();
                row[colDrugCode] = dt.Rows[i]["MNC_PH_CD"]?.ToString() ?? "";
                row[colDrugName] = dt.Rows[i]["mnc_ph_gn"]?.ToString() ?? "";
                row[colDrugQty] = dt.Rows[i]["MNC_PH_QTY"]?.ToString() ?? "";
                row[colDrugUnit] = dt.Rows[i]["MNC_PH_UNT_CD"]?.ToString() ?? "";
                row[colDrugUsage] = dt.Rows[i]["using_code"].ToString().Length>0 ? dt.Rows[i]["using_code"].ToString(): dt.Rows[i]["drug_using"]?.ToString();             //MNC_PH_DIR_DSC
                row[colDrugFrequency] = dt.Rows[i]["freq_code"].ToString().Length > 0 ? dt.Rows[i]["freq_code"].ToString() : dt.Rows[i]["drug_freq"]?.ToString();
                row[colDrugPrecau] = dt.Rows[i]["precautions"].ToString().Length > 0 ? dt.Rows[i]["precautions"].ToString() : dt.Rows[i]["drug_cau"]?.ToString();

            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // ✅ ดึงรหัสยาที่เลือกทั้งหมด
            SelectedDrugCodes.Clear();
            
            var selectedRows = grfDrug.Rows.Cast<Row>()
                .Where(r => r.Index > 0 && r.Selected)
                .ToList();
            
            if (selectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกรายการยาอย่างน้อย 1 รายการ", "แจ้งเตือน");
                return;
            }
            
            foreach (var row in selectedRows)
            {
                string drugCode = grfDrug[row.Index, colDrugCode]?.ToString() ?? "";
                string drugName = grfDrug[row.Index, colDrugName]?.ToString() ?? "";
                string usage = grfDrug[row.Index, colDrugUsage]?.ToString() ?? "";
                string frequency = grfDrug[row.Index, colDrugFrequency]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(drugCode))
                {
                    //SelectedDrugCodes.Add(drugCode+" "+ drugName+" "+ usage+" "+ frequency);
                    SelectedDrugCodes.Add(drugCode + " " + drugName);
                }
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}