using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
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
    public partial class FrmDocGroupFm : Form
    {
        BangnaControl bc;
        DocGroupSubScan dgss;
        DocGroupFM dfm;
        Font fEdit, fEditB;

        Color bg, fc;
        Font ff, ffB;
        int colID = 1, colCode = 2, colGrpName = 3, colSubName = 4;

        C1FlexGrid grfFMCode;
        Boolean flagEdit = false;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        String userIdVoid = "";
        Boolean pageLoad = false;

        public FrmDocGroupFm(BangnaControl x)
        {
            InitializeComponent();
            bc = x;
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            dgss = new DocGroupSubScan();
            dfm = new DocGroupFM();

            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            C1ThemeController.ApplicationTheme = bc.iniC.themeApplication;
            theme1.Theme = C1ThemeController.ApplicationTheme;
            theme1.SetTheme(sB, "BeigeOne");
            bg = txtFmCode.BackColor;
            fc = txtFmCode.ForeColor;
            ff = txtFmCode.Font;
            bc.bcDB.dgssDB.setCboDGSS(cboDocGroupSubName, "");
            bc.bcDB.dgsDB.setCboDgs(cboDocGroupName, "");

            sB.Text = "";
            btnVoid.Hide();
            txtPasswordVoid.Hide();
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            btnNew.Click += BtnNew_Click;
            btnEdit.Click += BtnEdit_Click;
            btnSave.Click += BtnSave_Click;
            txtPasswordVoid.KeyUp += TxtPasswordVoid_KeyUp;
            txtFmCode.KeyUp += TxtFmCode_KeyUp;
            cboDocGroupName.SelectedIndexChanged += CboDocGroupName_SelectedIndexChanged;
            btnVoid.Click += BtnVoid_Click;
            chkVoid.Click += ChkVoid_Click;

            initGrfFMCode();
            setGrfFMCode();
            setControlEnable(false);
            setFocusColor();
            pageLoad = false;
        }

        private void ChkVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkVoid.Checked)
            {
                btnVoid.Show();
            }
            else
            {
                btnVoid.Hide();
            }
        }
        private void BtnVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการ ยกเลิกช้อมูล ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                setDocFMCode();

                String re = bc.bcDB.dfmDB.Void(txtID.Text.Trim());
                int chk = 0;
                if (int.TryParse(re, out chk))
                {
                    btnVoid.Hide();
                }
                else
                {
                    btnSave.Image = Resources.accept_database24;
                }
                setGrfFMCode();
                //setGrdView();
                //this.Dispose();
            }
        }

        private void TxtFmCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode== Keys.Enter)
            {
                DocGroupFM fm = new DocGroupFM();
                fm = bc.bcDB.dfmDB.selectByFMCode(dfm.fm_code);
                if (fm.fm_code.Equals(dfm.fm_code))
                {
                    MessageBox.Show("พบรหัส FM CODE ซ้ำ", "");
                    return;
                }
            }
        }

        private void CboDocGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String dgsid = "";
            dgsid = cboDocGroupName.SelectedItem == null ? "" : ((ComboBoxItem)cboDocGroupName.SelectedItem).Value;
            if (dgsid.Length > 0)
            {
                bc.bcDB.dgssDB.setCboDGSS(cboDocGroupSubName, dgsid, "");
            }
        }

        private void initGrfFMCode()
        {
            grfFMCode = new C1FlexGrid();
            grfFMCode.Font = fEdit;
            grfFMCode.Dock = System.Windows.Forms.DockStyle.Fill;
            grfFMCode.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfPosi);

            grfFMCode.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grfPosi_AfterRowColChange);
            //grfPosi.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfPosi_CellButtonClick);
            //grfPosi.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfPosi_CellChanged);

            panel2.Controls.Add(this.grfFMCode);

            C1Theme theme = C1ThemeController.GetThemeByName("Office2013Red", false);
            C1ThemeController.ApplyThemeToObject(grfFMCode, theme);
        }
        private void grfPosi_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;

            String deptId = "";
            try
            {
                deptId = grfFMCode[e.NewRange.r1, colID] != null ? grfFMCode[e.NewRange.r1, colID].ToString() : "";
                setControl(deptId);
                setControlEnable(false);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "");
            }
            
            //setControlAddr(addrId);
            //setControlAddrEnable(false);
        }
        private void setFocusColor()
        {
            this.txtFmCode.Leave += new System.EventHandler(this.textBox_Leave);
            this.txtFmCode.Enter += new System.EventHandler(this.textBox_Enter);
        }
        private void textBox_Enter(object sender, EventArgs e)
        {
            C1TextBox a = (C1TextBox)sender;
            //a.BackColor = bc.cTxtFocus;
            a.Font = new Font(ff, FontStyle.Bold);
        }
        private void textBox_Leave(object sender, EventArgs e)
        {
            C1TextBox a = (C1TextBox)sender;
            a.BackColor = bg;
            a.ForeColor = fc;
            a.Font = new Font(ff, FontStyle.Regular);
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            flagEdit = true;
            setControlEnable(flagEdit);
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtID.Value = "";
            txtFmCode.Value = "";
            txtFmName.Value = "";
            cboDocGroupSubName.Text = "";
            chkVoid.Checked = false;
            btnVoid.Hide();
            flagEdit = true;
            setControlEnable(true);
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //DocGroupFM fm = new DocGroupFM();
            //fm = bc.bcDB.dfmDB.selectByFMCode(dfm.fm_code);
            //if (fm.fm_code.Equals(dfm.fm_code))
            //{
            //    MessageBox.Show("พบรหัส FM CODE ซ้ำ", "");
            //    //return;
            //}
            //if (MessageBox.Show("ต้องการ บันทึกช้อมูล ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            //{
            String grpid =cboDocGroupName.SelectedItem == null ? "" : ((ComboBoxItem)cboDocGroupName.SelectedItem).Value;
            if (grpid.Length <= 0)
            {
                MessageBox.Show("ไม่พบ กลุ่มเอกสารหลัก", "");
                return;
            }
            setDocFMCode();
            String re = bc.bcDB.dfmDB.insertDocGroupFM(dfm, bc.user.staff_id);
            int chk = 0;
            if (int.TryParse(re, out chk))
            {
                btnSave.Image = Resources.accept_database24;
                this.Enabled = false;
                re = bc.bcDB.dscDB.updateGrpChange(grpid, dfm.doc_group_sub_id, dfm.fm_code);
                this.Enabled = true;
            }
            else
            {
                btnSave.Image = Resources.accept_database24;
            }
            setGrfFMCode();
            //setGrdView();
            //this.Dispose();
            //}
        }
        private void setDocFMCode()
        {
            dfm.fm_id = txtID.Text;
            dfm.fm_code = txtFmCode.Text.Trim();
            dfm.fm_name = txtFmName.Text.Trim();
            dfm.doc_group_sub_id = cboDocGroupSubName.SelectedItem == null ? "" : ((ComboBoxItem)cboDocGroupSubName.SelectedItem).Value;
            dfm.status_doc_adminsion = ChkStatusDocAdmision.Checked ? "1" : "0";
            dfm.status_doc_medical = chkStatusDocMedical.Checked ? "1" : "0";
            dfm.status_doc_nurse = ChkStatusDocNurse.Checked ? "1" : "0";
            dfm.status_doc_office = chkStatusDocOffice.Checked ? "1" : "0";
        }
        private void TxtPasswordVoid_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //userIdVoid = bc.bcDB.stfDB.selectByPasswordAdmin(txtPasswordVoid.Text.Trim());
                if (userIdVoid.Length > 0)
                {
                    txtPasswordVoid.Hide();
                    btnVoid.Show();
                    //stt.Show("<p><b>ต้องการยกเลิก</b></p> <br> รหัสผ่านถูกต้อง", btnVoid);
                }
                else
                {
                    sep.SetError(txtPasswordVoid, "333");
                }
            }
        }
        private void setControl(String posiId)
        {
            dfm = bc.bcDB.dfmDB.selectByPk(posiId);
            bc.setC1Combo(cboDocGroupName, dfm.doc_group_id);
            bc.bcDB.dgssDB.setCboDGSS(cboDocGroupSubName, dfm.doc_group_id, "");
            bc.setC1Combo(cboDocGroupSubName, dfm.doc_group_sub_id);
            txtID.Value = dfm.fm_id;
            txtFmCode.Value = dfm.fm_code;
            txtFmName.Value = dfm.fm_name;

            chkStatusDocMedical.Checked = dfm.status_doc_medical.Equals("1") ? true : false;
            ChkStatusDocNurse.Checked = dfm.status_doc_nurse.Equals("1") ? true : false;
            ChkStatusDocAdmision.Checked = dfm.status_doc_adminsion.Equals("1") ? true : false;
            chkStatusDocOffice.Checked = dfm.status_doc_office.Equals("1") ? true : false;
        }
        private void setControlEnable(Boolean flag)
        {
            //txtID.Enabled = flag;
            txtFmCode.Enabled = flag;
            txtFmName.Enabled = flag;
            chkVoid.Enabled = flag;
            btnEdit.Image = !flag ? Resources.lock24 : Resources.open24;
        }
        private void setGrfFMCode()
        {
            //grfDept.Rows.Count = 7;
            DataTable dt = new DataTable();
            dt = bc.bcDB.dfmDB.selectAll();
            grfFMCode.DataSource = null;
            grfFMCode.Cols.Count = 5;
            grfFMCode.Rows.Count = 1;

            C1TextBox txt = new C1TextBox();
            //grfPosi.Cols[colName].Editor = txt;
            grfFMCode.Cols[colID].Width = 60;

            //grfPosi.Cols[colCode].Width = 80;
            grfFMCode.Cols[colCode].Width = 300;
            grfFMCode.Cols[colSubName].Width = 300;

            grfFMCode.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            //grfPosi.Cols[colCode].Caption = "รหัส";
            grfFMCode.Cols[colCode].Caption = "FM-CODE";
            //grfPosi.Cols[colRemark].Caption = "หมายเหตุ";

            //grfDept.Cols[coledit].Visible = false;
            //CellRange rg = grfPosi.GetCellRange(2, colE);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row row = grfFMCode.Rows.Add();
                row[colID] = dt.Rows[i]["fm_id"].ToString();
                row[colCode] = dt.Rows[i]["fm_code"].ToString();
                row[colSubName] = dt.Rows[i]["doc_group_sub_name"].ToString();
                row[colGrpName] = dt.Rows[i]["doc_group_name"].ToString();
                row[0] = (i + 1);
                if (i % 2 == 0)
                    grfFMCode.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }

            grfFMCode.Cols[colID].Visible = false;
            grfFMCode.Cols[colSubName].AllowEditing = false;
            grfFMCode.Cols[colCode].AllowEditing = false;
        }
        private void FrmDocGroupFm_Load(object sender, EventArgs e)
        {

        }
    }
}
