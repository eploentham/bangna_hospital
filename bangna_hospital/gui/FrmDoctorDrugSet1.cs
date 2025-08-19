using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Row = C1.Win.C1FlexGrid.Row;

namespace bangna_hospital.gui
{
    public partial class FrmDoctorDrugSet1 : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfView, grfItem, grfDrugSet;
        C1ThemeController theme1;
        String DTRCODE = "";
        AutoCompleteStringCollection autoDrug;

        int colgrfDrugSetItemCode = 1, colgrfDrugSetItemName = 2, colgrfDrugSetUsing1=3, colgrfDrugSetFreq = 4, colgrfDrugSetPrecau = 5, colgrfDrugSetindica = 6, colgrfDrugSetInterac = 7, colgrfDrugSetItemStatus = 8, colgrfDrugSetItemQty = 9, colgrfDrugSetID = 10, colgrfDrugSetFlagSave = 11;
        Boolean pageLoad = false;
        public FrmDoctorDrugSet1(BangnaControl bc, String dtrcode)
        {
            this.bc = bc;
            this.DTRCODE = dtrcode;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            theme1 = new C1ThemeController();
            btnNew.Click += BtnNew_Click;
            btnDrugSetSave.Click += BtnDrugSetSave_Click;
            cboDrugSetName.SelectedIndexChanged += CboDrugSetName_SelectedIndexChanged;
            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            btnItemAdd.Click += BtnItemAdd_Click;
            btnItemDel.Click += BtnItemDel_Click;
            btnDrugSetDel.Click += BtnDrugSetDel_Click;

            initControl();
            setControl();

            pageLoad = false;
        }
        private void BtnDrugSetDel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void CboDrugSetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;

            setGrfDrugSet(cboDrugSetName.Text.Trim());
        }

        private void setControlFromGrf(int indexrow)
        {
            if (indexrow <= 0) return;
            txtDrugSetId.Value = grfDrugSet[indexrow, colgrfDrugSetID].ToString();
            txtItemCode.Value = grfDrugSet[indexrow, colgrfDrugSetItemCode].ToString();
            lbItemEng.Text = grfDrugSet[indexrow, colgrfDrugSetItemName].ToString();
            txtFrequency1.Value = grfDrugSet[indexrow, colgrfDrugSetFreq] != null ? grfDrugSet[indexrow, colgrfDrugSetFreq].ToString():"";
            txtIndication.Value = grfDrugSet[indexrow, colgrfDrugSetPrecau]!=null ? grfDrugSet[indexrow, colgrfDrugSetPrecau].ToString():"";
            txtInteraction.Value = grfDrugSet[indexrow, colgrfDrugSetInterac] != null ?grfDrugSet[indexrow, colgrfDrugSetInterac].ToString():"";
            txtItemQTY.Value = grfDrugSet[indexrow, colgrfDrugSetItemQty].ToString();
        }
        private void setGrdDrugSetRow(Row arow, String drugsetid, string itemcode, String itemname, String freq, String precau, String interac, String qty)
        {
            arow[colgrfDrugSetID] = drugsetid;
            arow[colgrfDrugSetItemCode] = itemcode;
            arow[colgrfDrugSetItemName] = itemname;
            arow[colgrfDrugSetFreq] = freq;
            arow[colgrfDrugSetPrecau] = precau;
            arow[colgrfDrugSetInterac] = interac;
            arow[colgrfDrugSetItemStatus] = "drug";
            arow[colgrfDrugSetItemQty] = qty;
            arow[colgrfDrugSetFlagSave] = "1";
            arow.StyleNew.Font = fEditB;
            if (drugsetid.Length<=0) { arow.StyleNew.BackColor = ColorTranslator.FromHtml("#CCCCFF"); }
        }
        private void BtnItemDel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtDrugSetId.Text.Trim().Length > 0)
            {
                foreach (Row arow in grfDrugSet.Rows)
                {
                    if (arow[colgrfDrugSetItemCode].ToString().Equals("code")) continue;
                    if (txtDrugSetId.Text.Equals(arow[colgrfDrugSetID].ToString()))
                    {
                        arow[colgrfDrugSetFlagSave] = "3";
                        arow.StyleNew.Font = fEditS;
                        clearControl();
                        break;
                    }
                }
            }
        }
        private void BtnItemAdd_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtDrugSetId.Text.Trim().Length == 0) 
            {
                Row arow = grfDrugSet.Rows.Add();
                setGrdDrugSetRow(arow,"",txtItemCode.Text.Trim(), lbItemEng.Text, txtFrequency1.Text.Trim(), txtIndication.Text.Trim(), txtInteraction.Text.Trim(), txtItemQTY.Text.Trim());
                clearControl();
            }
            else
            {
                foreach(Row arow in grfDrugSet.Rows)
                {
                    if (arow[colgrfDrugSetItemCode].ToString().Equals("code")) continue;
                    if (txtDrugSetId.Text.Equals(arow[colgrfDrugSetID].ToString()))
                    {
                        setGrdDrugSetRow(arow, arow[colgrfDrugSetID].ToString(), txtItemCode.Text.Trim(), lbItemEng.Text, txtFrequency1.Text.Trim(), txtIndication.Text.Trim(), txtInteraction.Text.Trim(), txtItemQTY.Text.Trim());
                        clearControl();
                        break;
                    }
                }
            }
        }
        private void setOrderItem()
        {
            String[] txt = txtSearchItem.Text.Split('#');
            if (txt.Length <= 1)
            {
                lfSbMessage.Text = "no item";
                txtItemCode.Value = "";
                lbItemNameThai.Text = "";
                txtItemQTY.Value = "1";
                return;
            }
            String name = txt[0].Trim();
            String code = txt[1].Trim();
            PharmacyM01 drug1 = bc.bcDB.pharM01DB.SelectNameByPk1(code);
            txtItemCode.Value = code;
            lbItemEng.Text = drug1.MNC_PH_TN;
            lbTradeName.Text = drug1.MNC_PH_GN;
            lbItemNameThai.Text = drug1.MNC_PH_THAI;
            lbStrength.Text = drug1.MNC_PH_STRENGTH;
            txtFrequency.Value = drug1.using1;
            txtFrequency1.Value = drug1.frequency;
            txtIndication.Value = drug1.indication;
            txtInteraction.Value = drug1.interaction;
            lbUnitName.Text = drug1.MNC_DOSAGE_FORM;
            txtPrecautions.Value = drug1.precautions;
            txtItemQTY.Visible = true;
            txtItemQTY.Value = "1";
            
        }
        private void TxtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearchItem.Text.Trim().Length <= 0) return;
                setOrderItem();
                txtItemCode.Focus();
            }
        }

        private void TxtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setGrfOrderItem();
            }
        }
        private void setGrfOrderItem()
        {
            setGrfOrderItem(txtItemCode.Text.Trim(), lbItemNameThai.Text, txtItemQTY.Text.Trim());
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void setGrfOrderItem(String code, String name, String qty)
        {
            if (grfDrugSet == null) { return; }
            ////if(grfOrder.Row<=0) { return; }
            Row rowitem = grfDrugSet.Rows.Add();
            rowitem[colgrfDrugSetItemCode] = code;
            rowitem[colgrfDrugSetItemName] = name;
            rowitem[colgrfDrugSetItemQty] = qty;
            rowitem[colgrfDrugSetItemStatus] = "drug";
            rowitem[colgrfDrugSetID] = cboDrugSetName.SelectedItem;
            //rowitem[colgrfOrderReqNO] = "";
            rowitem[colgrfDrugSetFlagSave] = "1";//ต้องการ save ลง table temp_order
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void initControl()
        {
            autoDrug = new AutoCompleteStringCollection();
            autoDrug = bc.bcDB.pharM01DB.getlDrugAll();

            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            initFont();
            initGrfDrugSet();

            bc.bcDB.drugSetDB.setCboDrugSet(cboDrugSetName, DTRCODE, "");
        }
        private void initFont()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt7B = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Strikeout);
            fEditS1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);

        }
        private void initGrfDrugSet()
        {
            grfDrugSet = new C1FlexGrid();
            grfDrugSet.Font = fEdit;
            grfDrugSet.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrugSet.Location = new System.Drawing.Point(0, 0);
            grfDrugSet.Cols.Count = 12;
            grfDrugSet.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);
            grfDrugSet.Cols[colgrfDrugSetItemCode].Width = 70;
            grfDrugSet.Cols[colgrfDrugSetItemName].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetFreq].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetPrecau].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetInterac].Width = 250;

            grfDrugSet.Cols[colgrfDrugSetItemCode].Caption = "code";
            grfDrugSet.Cols[colgrfDrugSetItemName].Caption = "Name";
            grfDrugSet.Cols[colgrfDrugSetUsing1].Caption = "วิธีใช้ ";
            grfDrugSet.Cols[colgrfDrugSetFreq].Caption = "ความถี่ในการใช้ยา";
            grfDrugSet.Cols[colgrfDrugSetPrecau].Caption = "ข้อบ่งชี้ ";
            grfDrugSet.Cols[colgrfDrugSetindica].Caption = "ข้อควรระวัง";
            grfDrugSet.Cols[colgrfDrugSetInterac].Caption = "ปฎิกิริยาต่อยาอื่น";
            grfDrugSet.Cols[colgrfDrugSetID].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetFlagSave].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetItemStatus].Visible = false;

            grfDrugSet.Cols[colgrfDrugSetItemCode].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetItemName].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetItemQty].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetFreq].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetPrecau].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetInterac].AllowEditing = false;

            grfDrugSet.Click += GrfDrugSet_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnDrugSet.Controls.Add(grfDrugSet);
            theme1.SetTheme(grfDrugSet, bc.iniC.themeApp);

        }
        private void GrfDrugSet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDrugSet.Rows.Count <= 0) return;
            setControlFromGrf(grfDrugSet.Row);
        }
        private void setGrfDrugSet(String drugsetname)
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.drugSetDB.selectDrugSet(DTRCODE, drugsetname);
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1 ;
            grfDrugSet.Rows.Count = 1; grfDrugSet.Rows.Count = dt.Rows.Count+1;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfDrugSet.Rows[i];
                String status = "", vn = "";
                rowa[colgrfDrugSetID] = row1["drug_set_id"].ToString();
                rowa[colgrfDrugSetItemCode] = row1["item_code"].ToString();
                rowa[colgrfDrugSetItemName] = row1["item_name"].ToString();
                rowa[colgrfDrugSetItemQty] = row1["qty"].ToString();
                rowa[colgrfDrugSetItemStatus] = row1["status_item"].ToString();
                rowa[colgrfDrugSetUsing1] = row1["using1"].ToString();
                rowa[colgrfDrugSetFreq] = row1["frequency"].ToString();
                rowa[colgrfDrugSetPrecau] = row1["precautions"].ToString();
                rowa[colgrfDrugSetindica] = row1["indication"].ToString();
                rowa[colgrfDrugSetInterac] = row1["interaction"].ToString();
                rowa[colgrfDrugSetFlagSave] = "0";
                i++;
            }
        }
        private void clearControl()
        {
            txtSearchItem.Value = "";
            txtItemCode.Value = "";
            lbItemNameThai.Text = "";
            txtItemRemark.Value = "";
            lbItemEng.Text = "";
            lbTradeName.Text = "";
            lbStrength.Text = "";
            txtFrequency1.Value = "";
            txtIndication.Value = "";
            txtInteraction.Value = "";

            txtItemQTY.Visible = true;
            txtItemQTY.Value = "1";
            txtDrugSetId.Value = "";
        }
        private void BtnDrugSetSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(grfDrugSet.Rows.Count<=1) return;
            if (cboDrugSetName.Text.Length <= 0) { lfSbMessage.Text = "No Name"; return; }
            foreach (Row arow in grfDrugSet.Rows)
            {
                if(arow[colgrfDrugSetItemCode].ToString().Equals("code")) continue;
                String itemcode = "", itemname = "", freq = "", precau = "", qty = "", flagsave = "", id = "";
                flagsave = arow[colgrfDrugSetFlagSave].ToString();
                if (flagsave.Equals("0")) continue;
                id = arow[colgrfDrugSetID].ToString();
                if (flagsave.Equals("3")){String re1 = bc.bcDB.drugSetDB.updateVoid(id, DTRCODE); }
                
                DrugSet drugSet = new DrugSet();
                drugSet.doctor_id = DTRCODE;
                drugSet.drug_set_name = cboDrugSetName.Text.Trim();
                drugSet.drug_set_id = id;
                drugSet.item_code = arow[colgrfDrugSetItemCode].ToString();
                drugSet.item_name = arow[colgrfDrugSetItemName].ToString();
                drugSet.frequency = arow[colgrfDrugSetFreq].ToString();
                drugSet.precautions = arow[colgrfDrugSetPrecau].ToString();
                drugSet.using1 = arow[colgrfDrugSetUsing1] != null ?arow[colgrfDrugSetUsing1].ToString():"";
                drugSet.qty = arow[colgrfDrugSetItemQty].ToString();
                String re = bc.bcDB.drugSetDB.insertDrugSet(drugSet, "");
                if(!int.TryParse(re, out int chk))
                {
                    new LogWriter("e", "FrmPatient BtnDrugSetSave_Click " + re);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmPatient BtnDrugSetSave_Click  ", re);
                    lfSbMessage.Text = re;
                }
            }
            setGrfDrugSet(cboDrugSetName.Text.Trim());
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ComboBoxItem item = new ComboBoxItem();
            item.Text = "";
            item.Value = "";
            cboDrugSetName.SelectedItem =item;
        }
        private void setControl()
        {
            lbDtrName.Text = bc.user.fullname;
            DTRCODE = bc.user.username;//เปิดโปรแกรม login ด้วย แพทย์ ถือว่าเป็น แพทย์
            
        }
        private void FrmDoctorDrugSet1_Load(object sender, EventArgs e)
        {
            spMain.HeaderHeight = 0;
            spDrugSet.HeaderHeight = 0;

            this.Text = "Last Update 2024-03-08";
            bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmDoctorDrugSet1", "Application DrugSet Start");
        }
    }
}
