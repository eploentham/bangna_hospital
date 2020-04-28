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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmDocGroupSub : Form
    {
        BangnaControl bc;
        DocGroupSubScan dgss;
        Font fEdit, fEditB;

        Color bg, fc;
        Font ff, ffB;
        int colID = 1, colName = 2, colSubName=3;

        C1FlexGrid grfPosi;

        //C1TextBox txtPassword = new C1.Win.C1Input.C1TextBox();
        Boolean flagEdit = false;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;

        String userIdVoid = "";
        public FrmDocGroupSub(BangnaControl x)
        {
            InitializeComponent();
            bc = x;
            initConfig();
        }
        private void initConfig()
        {
            dgss = new DocGroupSubScan();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            C1ThemeController.ApplicationTheme = bc.iniC.themeApplication;
            theme1.Theme = C1ThemeController.ApplicationTheme;
            theme1.SetTheme(sB, "BeigeOne");
            foreach (Control c in panel3.Controls)
            {
                theme1.SetTheme(c, "Office2013Red");
            }

            bg = txtDocGroupSubName.BackColor;
            fc = txtDocGroupSubName.ForeColor;
            ff = txtDocGroupSubName.Font;
            bc.bcDB.dgsDB.setCboDgs(cboDocGroupName, "");

            txtPasswordVoid.KeyUp += TxtPasswordVoid_KeyUp;
            btnNew.Click += BtnNew_Click;
            btnEdit.Click += BtnEdit_Click;
            btnSave.Click += BtnSave_Click;

            initGrfPosi();
            setGrfPosi();
            setControlEnable(false);
            setFocusColor();
            sB1.Text = "";
            btnVoid.Hide();
            txtPasswordVoid.Hide();
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการ บันทึกช้อมูล ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                setDocGroupScan();
                String re = bc.bcDB.dgssDB.insertDocGroupSubScan(dgss, bc.user.staff_id);
                int chk = 0;
                if (int.TryParse(re, out chk))
                {
                    btnSave.Image = Resources.accept_database24;
                }
                else
                {
                    btnSave.Image = Resources.accept_database24;
                }
                setGrfPosi();
                //setGrdView();
                //this.Dispose();
            }
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
            txtDocGroupSubName.Value = "";
            cboDocGroupName.Text = "";
            chkVoid.Checked = false;
            btnVoid.Hide();
            flagEdit = true;
            setControlEnable(true);
        }

        private void initGrfPosi()
        {
            grfPosi = new C1FlexGrid();
            grfPosi.Font = fEdit;
            grfPosi.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPosi.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfPosi);

            grfPosi.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grfPosi_AfterRowColChange);
            grfPosi.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfPosi_CellButtonClick);
            grfPosi.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfPosi_CellChanged);

            panel2.Controls.Add(this.grfPosi);

            C1Theme theme = C1ThemeController.GetThemeByName("Office2013Red", false);
            C1ThemeController.ApplyThemeToObject(grfPosi, theme);
        }
        private void setGrfPosi()
        {
            //grfDept.Rows.Count = 7;
            DataTable dt = new DataTable();
            dt = bc.bcDB.dgssDB.selectAll();
            grfPosi.DataSource = null;
            grfPosi.Cols.Count = 4;
            grfPosi.Rows.Count = 1;
            
            C1TextBox txt = new C1TextBox();
            grfPosi.Cols[colName].Editor = txt;
            grfPosi.Cols[colID].Width = 60;

            //grfPosi.Cols[colCode].Width = 80;
            grfPosi.Cols[colName].Width = 300;
            grfPosi.Cols[colSubName].Width = 300;

            grfPosi.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            //grfPosi.Cols[colCode].Caption = "รหัส";
            grfPosi.Cols[colName].Caption = "ชื่อประเภทเอกสารย่อย";
            //grfPosi.Cols[colRemark].Caption = "หมายเหตุ";

            //grfDept.Cols[coledit].Visible = false;
            //CellRange rg = grfPosi.GetCellRange(2, colE);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row row = grfPosi.Rows.Add();
                row[colID] = dt.Rows[i]["doc_group_sub_id"].ToString();
                row[colName] = dt.Rows[i]["doc_group_name"].ToString();
                row[colSubName] = dt.Rows[i]["doc_group_sub_name"].ToString();
                row[0] = (i + 1);
                if (i % 2 == 0)
                    grfPosi.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }

            grfPosi.Cols[colID].Visible = false;
            //grfPosi.Cols[colE].Visible = false;
            //grfPosi.Cols[colS].Visible = false;
        }
        private void textBox_Enter(object sender, EventArgs e)
        {
            C1TextBox a = (C1TextBox)sender;
            a.BackColor = bc.cTxtFocus;
            a.Font = new Font(ff, FontStyle.Bold);
        }
        private void setFocusColor()
        {
            this.txtDocGroupSubName.Leave += new System.EventHandler(this.textBox_Leave);
            this.txtDocGroupSubName.Enter += new System.EventHandler(this.textBox_Enter);
        }
        private void textBox_Leave(object sender, EventArgs e)
        {
            C1TextBox a = (C1TextBox)sender;
            a.BackColor = bg;
            a.ForeColor = fc;
            a.Font = new Font(ff, FontStyle.Regular);
        }
        private void setControl(String posiId)
        {
            dgss = bc.bcDB.dgssDB.selectByPk(posiId);
            bc.setC1Combo(cboDocGroupName, dgss.doc_group_id);
            txtID.Value = dgss.doc_group_sub_id;
            txtDocGroupSubName.Value = dgss.doc_group_sub_name;
        }
        private void setControlEnable(Boolean flag)
        {
            //txtID.Enabled = flag;
            txtDocGroupSubName.Enabled = flag;
            chkVoid.Enabled = flag;
            btnEdit.Image = !flag ? Resources.lock24 : Resources.open24;
        }
        private void setDocGroupScan()
        {
            dgss.doc_group_sub_id = txtID.Text;
            dgss.doc_group_sub_name = txtDocGroupSubName.Text;
            dgss.doc_group_id = cboDocGroupName.SelectedItem == null ? "" : ((ComboBoxItem)cboDocGroupName.SelectedItem).Value;
        }
        private void grfPosi_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;

            String deptId = "";
            deptId = grfPosi[e.NewRange.r1, colID] != null ? grfPosi[e.NewRange.r1, colID].ToString() : "";
            setControl(deptId);
            setControlEnable(false);
            //setControlAddr(addrId);
            //setControlAddrEnable(false);
        }
        private void grfPosi_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {

        }
        private void grfPosi_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //if (e.Row == 0) return;
            //CellStyle cs = grfDept.Styles.Add("text");
            //cs.BackColor = Color.DimGray;
            //sB1.Text = grfDept[e.Row, e.Col].ToString();
            ////grfDept[e.Row, coledit] = "1";
            //grfDept.Rows[e.Row].Style = cs;
            //if((e.Row+1) == ((RowCollection)grfDept.Rows).Count)
            //{
            //    ((RowCollection)grfDept.Rows).Count = ((RowCollection)grfDept.Rows).Count + 1;
            //}
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
        
        
        private void btnVoid_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการ ยกเลิกข้อมูล ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                //bc.bcDB.dgsDB.v(txtID.Text, userIdVoid);
                setGrfPosi();
            }
        }
        
        private void chkVoid_Click(object sender, EventArgs e)
        {
            if (btnVoid.Visible)
            {
                btnVoid.Hide();
            }
            else
            {
                txtPasswordVoid.Show();
                txtPasswordVoid.Focus();
                //stt.Show("<p><b>ต้องการยกเลิก</b></p> <br> กรุณาป้อนรหัสผ่าน", txtPasswordVoid);
            }
        }
        private void FrmDocGroupSub_Load(object sender, EventArgs e)
        {

        }
    }
}
