using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CSJ2K.j2k.codestream.HeaderInfo;

namespace bangna_hospital.gui
{
    public partial class FrmCompAdd : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B;
        Boolean pageLoad = false, findCust = false, findInsur = false;
        PatientM24 comp;
        C1FlexGrid grfComp;
        AutoCompleteStringCollection autoComp, autoInsur;
        Boolean checkCompNameFound = false;
        String COMPNAME = "";

        int colgrfCompName = 1, colgrfCompCode=2;
        public FrmCompAdd(BangnaControl bc, String compname)
        {
            this.bc = bc;
            this.COMPNAME = compname;
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

            comp = new PatientM24();
            autoComp = new AutoCompleteStringCollection();
            autoComp = bc.bcDB.pm24DB.getlPaid1(false);
            autoInsur = new AutoCompleteStringCollection();
            autoInsur = bc.bcDB.pm24DB.setAutoInsur(false);

            initGrfComp();

            btnCompSave.Click += BtnCompSave_Click;
            txtCompNameT.KeyUp += TxtCompNameT_KeyUp;
            txtCompAddrT.KeyUp += TxtCompAddrT_KeyUp;
            txtCompMobile1.KeyUp += TxtCompMobile1_KeyUp;
            txtCompMobile2.KeyUp += TxtCompMobile2_KeyUp;
            txtCompEmail.KeyUp += TxtCompEmail_KeyUp;
            txtCompInsur1.KeyUp += TxtCompInsur1_KeyUp;
            txtCompContractName.KeyUp += TxtCompContractName_KeyUp;
            if (COMPNAME.Length > 0) { txtCompCode.Value = bc.bcDB.pm24DB.getPaidCode(COMPNAME); txtCompNameT.Value = COMPNAME; setControl(); }
            if (txtCompCode.Text.Length > 0) lbMessage.Text = "ต้องการแก้ไข ข้อมูล"; else lbMessage.Text = "เพิ่มข้อมูล บริษัทใหม่";
            panel1.Hide();
            pageLoad = false;
        }
        private void TxtCompContractName_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtCompMobile1.SelectAll(); txtCompMobile1.Focus(); }
        }
        private void TxtCompInsur1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtCompInsur2.SelectAll(); txtCompInsur2.Focus(); }
        }
        private void TxtCompEmail_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtCompInsur1.SelectAll(); txtCompInsur1.Focus(); }
        }
        private void TxtCompMobile2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtCompEmail.SelectAll(); txtCompEmail.Focus(); }
        }
        private void TxtCompMobile1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtCompMobile2.SelectAll(); txtCompMobile2.Focus(); }
        }
        private void initGrfComp()
        {
            grfComp = new C1FlexGrid();
            grfComp.Font = fEdit;
            grfComp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfComp.Location = new System.Drawing.Point(0, 0);
            grfComp.Rows.Count = 1;
            grfComp.Cols.Count = 3;
            grfComp.Cols[colgrfCompName].DataType = typeof(String);
            grfComp.Cols[colgrfCompName].TextAlign = TextAlignEnum.LeftCenter;
            grfComp.Cols[colgrfCompName].Width = 400;
            grfComp.ShowCursor = true;
            grfComp.Cols[colgrfCompName].Caption = "ชื่อบริษัท";
            grfComp.Cols[colgrfCompName].AllowEditing = false;
            grfComp.Cols[colgrfCompCode].Visible = false;
            grfComp.Click += GrfComp_Click;
            panel1.Controls.Add(grfComp);
        }
        private void GrfComp_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            setGrfComp(grfComp[grfComp.Row, colgrfCompCode].ToString(), "code");
        }
        private void setGrfComp(String name, String flag)
        {
            DataTable dt = new DataTable();
            if (flag.Equals("code"))
            {
                dt = bc.bcDB.pm24DB.selectCustByCode(name);
            }
            else
            {
                dt = bc.bcDB.pm24DB.selectCustByName(name);
            }
            int i = 1, j = 1;
            grfComp.Rows.Count = 1;            grfComp.Rows.Count = dt.Rows.Count + 1;
            checkCompNameFound = false;
            foreach (DataRow row1 in dt.Rows)
            {
                try
                {
                    if (i == 1)
                    {
                        txtCompCode.Value = row1["MNC_COM_CD"].ToString();
                        txtCompNameT.Value = row1["MNC_COM_DSC"].ToString();
                        txtCompNameE.Value = "";
                        txtCompAddrT.Value = row1["MNC_COM_ADD"].ToString();
                        //txtCompAddrE.Value = row1[""].ToString();
                        txtCompContractName.Value = row1["MNC_COM_NAM"].ToString();
                        txtCompMobile1.Value = row1["MNC_COM_TEL"].ToString();
                        txtCompMobile2.Value = row1["phone2"].ToString();
                        txtCompEmail.Value = row1["email"].ToString();
                        txtCompInsur1.Value = bc.bcDB.pm24DB.selectCust(row1["insur1_code"].ToString());
                        txtCompInsur2.Value = bc.bcDB.pm24DB.selectCust(row1["insur2_code"].ToString());
                        chkCompInsur.Value = row1["status_insur"].ToString().Equals("1") ? true : false;
                    }
                    Row rowa = grfComp.Rows[i];
                    rowa[colgrfCompName] = row1["MNC_COM_DSC"].ToString();
                    rowa[colgrfCompCode] = row1["MNC_COM_CD"].ToString();
                    rowa[0] = i.ToString();
                    checkCompNameFound = true;
                    i++;
                }
                catch (Exception ex)
                {
                    lfsbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmCompAdd setGrfSrc " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfComp ", ex.Message);
                }
            }
            if(grfComp.Rows.Count > 1) { panel1.Show(); }else { panel1.Hide(); }
        }
        private void TxtCompAddrT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter){ txtCompContractName.SelectAll(); txtCompContractName.Focus(); }
        }
        private void TxtCompNameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)            {                setControl();            }
        }
        private void setControl()
        {
            setGrfComp(txtCompNameT.Text.Trim(),"name");
            if (checkCompNameFound) { lfsbMessage.Text = "พบ รายชื่อบริษัท"; }
            else { lfsbMessage.Text = "ไม่พบ รายชื่อบริษัท"; }
            txtCompAddrT.SelectAll();
            txtCompAddrT.Focus();
        }
        private void setCompany()
        {
            comp.MNC_COM_CD = txtCompCode.Text.Trim();
            comp.MNC_COM_DSC = txtCompNameT.Text.Trim();
            comp.MNC_COM_TEL = txtCompMobile1.Text.Trim();
            comp.MNC_COM_ADD = txtCompAddrT.Text.Trim();
            comp.MNC_COM_TEL = txtCompMobile1.Text.Trim();
            comp.MNC_COM_NAM = txtCompContractName.Text.Trim();
            comp.email = txtCompEmail.Text.Trim();
            comp.phone2 = txtCompMobile2.Text.Trim();
            comp.status_insur = chkCompInsur.Checked ? "1": "0";
            comp.insur1_code = bc.bcDB.pm24DB.selectCustByName1(txtCompInsur1.Text.Trim());
            comp.insur2_code = bc.bcDB.pm24DB.selectCustByName1(txtCompInsur2.Text.Trim());
            comp.MNC_COM_STS = "Y";
        }
        private void BtnCompSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtCompNameT.Text.Length == 0){ txtCompNameT.Focus(); return; }
            String re = "";
            setCompany();
            if (txtCompCode.Text.Trim().Length > 0) {
                if (MessageBox.Show("ต้องการแก้ไข ข้อมูลบริษัท", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    re = bc.bcDB.pm24DB.insertCompany(comp, "");
                }
            }
            else
            {
                comp.MNC_COM_CD = "";
                re = bc.bcDB.pm24DB.insertCompany(comp, "");
            }

            
            
            if (long.TryParse(re, out long chk)){ lfsbMessage.Text = "update OK";                this.Dispose(); }
            else{ bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnCompSave_Click ", re); }
        }
        private void FrmCompAdd_Load(object sender, EventArgs e)
        {
            txtCompNameT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCompNameT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCompNameT.AutoCompleteCustomSource = autoComp;

            txtCompInsur1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCompInsur1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCompInsur1.AutoCompleteCustomSource = autoInsur;

            txtCompInsur2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCompInsur2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCompInsur2.AutoCompleteCustomSource = autoInsur;
        }
    }
}