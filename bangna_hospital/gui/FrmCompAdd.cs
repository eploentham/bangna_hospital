using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        int colgrfCompName = 1;
        public FrmCompAdd(BangnaControl bc)
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

            comp = new PatientM24();
            autoComp = new AutoCompleteStringCollection();
            autoComp = bc.bcDB.pm24DB.setAutoComp();
            autoInsur = new AutoCompleteStringCollection();
            autoInsur = bc.bcDB.pm24DB.setAutoInsur();

            initGrfComp();

            btnCompSave.Click += BtnCompSave_Click;
            txtCompNameT.KeyUp += TxtCompNameT_KeyUp;
            txtCompAddrT.KeyUp += TxtCompAddrT_KeyUp;

            panel1.Hide();
            pageLoad = false;
        }
        private void initGrfComp()
        {
            grfComp = new C1FlexGrid();
            grfComp.Font = fEdit;
            grfComp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfComp.Location = new System.Drawing.Point(0, 0);
            grfComp.Rows.Count = 1;
            grfComp.Cols.Count = 2;
            grfComp.Cols[colgrfCompName].DataType = typeof(String);

            grfComp.Cols[colgrfCompName].TextAlign = TextAlignEnum.LeftCenter;

            grfComp.Cols[colgrfCompName].Width = 200;

            grfComp.ShowCursor = true;
            grfComp.Cols[colgrfCompName].Caption = "hn";

            grfComp.Cols[colgrfCompName].AllowEditing = false;
            
            panel1.Controls.Add(grfComp);
            
        }
        private void setGrfComp(String name)
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pm24DB.selectCustByName(name);
            int i = 1, j = 1;
            grfComp.Rows.Count = 1;
            grfComp.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            checkCompNameFound = false;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                try
                {
                    Row rowa = grfComp.Rows[i];
                    rowa[colgrfCompName] = row1["MNC_COM_DSC"].ToString();
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
            if (e.KeyCode == Keys.Enter)
            {
                txtCompMobile1.SelectAll();
                txtCompMobile1.Focus();
            }
        }
        private void TxtCompNameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                setGrfComp(txtCompNameT.Text.Trim());
                if (checkCompNameFound)
                {
                    lfsbMessage.Text = "พบ รายชื่อบริษัท";
                }
                else
                {
                    lfsbMessage.Text = "ไม่พบ รายชื่อบริษัท";
                }
                txtCompAddrT.SelectAll();
                txtCompAddrT.Focus();
            }
        }
        private void setCompany()
        {
            comp.MNC_COM_CD = txtCompCode.Text.Trim();
            comp.MNC_COM_DSC = txtCompNameT.Text.Trim();
            comp.MNC_COM_TEL = txtCompMobile1.Text.Trim();
            comp.MNC_COM_ADD = txtCompAddrT.Text.Trim();
            comp.status_insur = txtCompInsur1.Text.Length>0 ? "1": "0";
            comp.insur1_code = bc.bcDB.pm24DB.selectCustByName1(txtCompInsur1.Text.Trim());
            comp.insur2_code = bc.bcDB.pm24DB.selectCustByName1(txtCompInsur2.Text.Trim());
            comp.MNC_COM_STS = "Y";
        }
        private void BtnCompSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtCompNameT.Text.Length == 0)
            {
                txtCompNameT.Focus();
                return;
            }
            setCompany();
            String re = bc.bcDB.pm24DB.insertCompany(comp,"");
            if (long.TryParse(re, out long chk))
            {
                //lfSbStatus.Text = "update OK";
                this.Dispose();
            }
            else
            {
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnCompSave_Click ", re);
            }
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
