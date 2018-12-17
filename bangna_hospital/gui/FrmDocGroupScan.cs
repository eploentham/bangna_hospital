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
using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;

namespace bangna_hospital.gui
{
    public partial class FrmDocGroupScan : Form
    {
        BangnaControl bc;
        DocGroupScan dgs;

        Font fEdit, fEditB;

        Color bg, fc;
        Font ff, ffB;
        int colID = 1, colCode = 2, colName = 3, colRemark = 4, colE = 5, colS = 6, coledit = 7, colCnt = 7;

        C1FlexGrid grfPosi;

        //C1TextBox txtPassword = new C1.Win.C1Input.C1TextBox();
        Boolean flagEdit = false;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;

        String userIdVoid = "";
        public FrmDocGroupScan(BangnaControl x)
        {
            InitializeComponent();
            bc = x;
            initConfig();
        }

        private void initConfig()
        {
            dgs = new DocGroupScan();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            C1ThemeController.ApplicationTheme = bc.iniC.themeApplication;
            theme1.Theme = C1ThemeController.ApplicationTheme;
            theme1.SetTheme(sB, "BeigeOne");
            foreach (Control c in panel3.Controls)
            {
                theme1.SetTheme(c, "Office2013Red");
            }

            bg = txtDocGroupName.BackColor;
            fc = txtDocGroupName.ForeColor;
            ff = txtDocGroupName.Font;
            txtPasswordVoid.KeyUp += TxtPasswordVoid_KeyUp;

            initGrfPosi();
            setGrfPosi();
            setControlEnable(false);
            setFocusColor();
            sB1.Text = "";
            btnVoid.Hide();
            txtPasswordVoid.Hide();
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            //stt.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.Gold;
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

            grfPosi.DataSource = bc.bcDB.dgsDB.selectAll();
            grfPosi.Cols.Count = colCnt;
            CellStyle cs = grfPosi.Styles.Add("btn");
            cs.DataType = typeof(Button);
            //cs.ComboList = "|Tom|Dbck|Harry";
            cs.ForeColor = Color.Navy;
            cs.Font = new Font(Font, FontStyle.Bold);
            cs = grfPosi.Styles.Add("date");
            cs.DataType = typeof(DateTime);
            cs.Format = "dd-MMM-yy";
            cs.ForeColor = Color.DarkGoldenrod;

            grfPosi.Cols[colE].Style = grfPosi.Styles["btn"];
            grfPosi.Cols[colS].Style = grfPosi.Styles["date"];

            grfPosi.Cols[colID].Width = 60;

            grfPosi.Cols[colCode].Width = 80;
            grfPosi.Cols[colName].Width = 300;

            grfPosi.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            grfPosi.Cols[colCode].Caption = "รหัส";
            grfPosi.Cols[colName].Caption = "ชื่อตำแหน่ง";
            grfPosi.Cols[colRemark].Caption = "หมายเหตุ";

            //grfDept.Cols[coledit].Visible = false;
            CellRange rg = grfPosi.GetCellRange(2, colE);
            for (int i = 1; i < grfPosi.Rows.Count; i++)
            {
                grfPosi[i, 0] = i;
                if (i % 2 == 0)
                    grfPosi.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            grfPosi.Cols[colID].Visible = false;
            grfPosi.Cols[colE].Visible = false;
            grfPosi.Cols[colS].Visible = false;
        }
        private void textBox_Enter(object sender, EventArgs e)
        {
            C1TextBox a = (C1TextBox)sender;
            a.BackColor = bc.cTxtFocus;
            a.Font = new Font(ff, FontStyle.Bold);
        }
        private void setFocusColor()
        {
            this.txtDocGroupName.Leave += new System.EventHandler(this.textBox_Leave);
            this.txtDocGroupName.Enter += new System.EventHandler(this.textBox_Enter);
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
            dgs = bc.bcDB.dgsDB.selectByPk(posiId);
            txtID.Value = dgs.doc_group_id;
            txtDocGroupName.Value = dgs.doc_group_name;
        }
        private void setControlEnable(Boolean flag)
        {
            //txtID.Enabled = flag;
            txtDocGroupName.Enabled = flag;
            chkVoid.Enabled = flag;
            btnEdit.Image = !flag ? Resources.lock24 : Resources.open24;
        }
        private void setPosition()
        {
            dgs.doc_group_id = txtID.Text;
            dgs.doc_group_name = txtDocGroupName.Text;
            //posi.posi_name_e = txtPosiNameE.Text;
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
                if (userIdVoid.Length>0)
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
        private void btnNew_Click(object sender, EventArgs e)
        {
            txtID.Value = "";
            txtDocGroupName.Value = "";
            chkVoid.Checked = false;
            btnVoid.Hide();
            flagEdit = true;
            setControlEnable(true);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            flagEdit = true;
            setControlEnable(flagEdit);
        }
        private void btnVoid_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการ ยกเลิกข้อมูล ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                //bc.bcDB.dgsDB.v(txtID.Text, userIdVoid);
                setGrfPosi();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการ บันทึกช้อมูล ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                setPosition();
                String re = bc.bcDB.dgsDB.insertDocGroupScan(dgs, bc.user.staff_id);
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
        private void FrmPosition_Load(object sender, EventArgs e)
        {

        }
    }
}
