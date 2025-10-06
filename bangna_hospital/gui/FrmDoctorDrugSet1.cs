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
using System.Numerics;
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
        C1FlexGrid grfView, grfItem, grfDrugSet, grfChief, grfPhysical, grfDiag, grfDtrAdvice;
        C1ThemeController theme1;
        String DTRCODE = "";
        AutoCompleteStringCollection autoDrug;

        int colgrfDrugSetItemCode = 1, colgrfDrugSetItemName = 2, colgrfDrugSetUsing1=3, colgrfDrugSetFreq = 4, colgrfDrugSetPrecau = 5, colgrfDrugSetindica = 6, colgrfDrugSetInterac = 7, colgrfDrugSetItemStatus = 8, colgrfDrugSetItemQty = 9, colgrfDrugSetID = 10, colgrfDrugSetFlagSave = 11, colgrfDrugSetRemark = 12;
        int colgrfAutoId=1, colgrfAutoName=2, colgrfAutoFlagSave=3;
        Boolean isLoad = false;
        public FrmDoctorDrugSet1(BangnaControl bc, String dtrcode)
        {
            this.bc = bc;
            this.DTRCODE = dtrcode;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            isLoad = true;
            theme1 = new C1ThemeController();
            btnNew.Click += BtnNew_Click;
            btnDrugSetSave.Click += BtnDrugSetSave_Click;
            cboDrugSetName.SelectedIndexChanged += CboDrugSetName_SelectedIndexChanged;
            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            btnItemAdd.Click += BtnItemAdd_Click;
            btnItemDel.Click += BtnItemDel_Click;
            btnDrugSetDel.Click += BtnDrugSetDel_Click;
            tabMain.SelectedTabChanged += TabMain_SelectedTabChanged;

            initControl();
            setControl();

            isLoad = false;
        }

        private void TabMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (isLoad) return;
            if (tabMain.SelectedTab == tabDrugSet)
            {
                if (grfDrugSet.Rows.Count <= 2) setGrfDrugSet(cboDrugSetName.Text.Trim());
            }
            else if (tabMain.SelectedTab == tabChief)
            {
                if (grfChief.Rows.Count <= 2) setGrfChief();
            }
            else if (tabMain.SelectedTab == tabPhysical)
            {
                if (grfPhysical.Rows.Count <= 2) setGrfPhysical();
            }
            else if (tabMain.SelectedTab == tabDiag)
            {
                if (grfDiag.Rows.Count <= 2) setGrfDiag();
            }
            else if (tabMain.SelectedTab == tabDtrAdvice)
            {
                if (grfDiag.Rows.Count <= 2) setGrfDtrAdvice();
            }
        }

        private void BtnDrugSetDel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void CboDrugSetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (isLoad) return;

            setGrfDrugSet(cboDrugSetName.Text.Trim());
        }

        private void setControlFromGrf(int indexrow)
        {
            if (indexrow <= 0) return;
            txtDrugSetId.Value = grfDrugSet[indexrow, colgrfDrugSetID].ToString();
            txtItemCode.Value = grfDrugSet[indexrow, colgrfDrugSetItemCode].ToString();
            lbItemEng.Text = grfDrugSet[indexrow, colgrfDrugSetItemName].ToString();
            txtUsing.Value = grfDrugSet[indexrow, colgrfDrugSetFreq] != null ? grfDrugSet[indexrow, colgrfDrugSetFreq].ToString():"";
            txtIndication.Value = grfDrugSet[indexrow, colgrfDrugSetPrecau]!=null ? grfDrugSet[indexrow, colgrfDrugSetPrecau].ToString():"";
            txtInteraction.Value = grfDrugSet[indexrow, colgrfDrugSetInterac] != null ?grfDrugSet[indexrow, colgrfDrugSetInterac].ToString():"";
            txtItemQTY.Value = grfDrugSet[indexrow, colgrfDrugSetItemQty].ToString();
        }
        private void setGrdDrugSetRow(Row arow, String drugsetid, string itemcode, String itemname, String using1, String freq, String indica, String precau, String interac, String qty)
        {
            arow[colgrfDrugSetID] = drugsetid;
            arow[colgrfDrugSetItemCode] = itemcode;
            arow[colgrfDrugSetItemName] = itemname;
            arow[colgrfDrugSetUsing1] = using1;
            arow[colgrfDrugSetFreq] = freq;
            arow[colgrfDrugSetindica] = indica;
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
                setGrdDrugSetRow(arow,"",txtItemCode.Text.Trim(), lbItemEng.Text, txtUsing.Text.Trim(), txtFrequency.Text.Trim(), txtIndication.Text.Trim(),txtPrecautions.Text.Trim(), txtInteraction.Text.Trim(), txtItemQTY.Text.Trim());
                clearControl();
            }
            else
            {
                foreach(Row arow in grfDrugSet.Rows)
                {
                    if (arow[colgrfDrugSetItemCode].ToString().Equals("code")) continue;
                    if (txtDrugSetId.Text.Equals(arow[colgrfDrugSetID].ToString()))
                    {
                        setGrdDrugSetRow(arow, arow[colgrfDrugSetID].ToString(), txtItemCode.Text.Trim(), lbItemEng.Text, txtUsing.Text.Trim(), txtFrequency.Text.Trim(), txtIndication.Text.Trim(), txtPrecautions.Text.Trim(), txtInteraction.Text.Trim(), txtItemQTY.Text.Trim());
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
            txtGeneric.Value = drug1.MNC_PH_GN;
            lbItemNameThai.Text = drug1.MNC_PH_THAI;
            lbStrength.Text = drug1.MNC_PH_STRENGTH;
            txtUsing.Value = drug1.frequency;
            txtFrequency.Value = drug1.using1;
            txtPrecautions.Value = drug1.precautions;
            txtIndication.Value = drug1.indication;
            txtInteraction.Value = drug1.interaction;
            lbUnitName.Text = drug1.MNC_DOSAGE_FORM;
            
            txtItemQTY.Visible = true;
            txtItemQTY.Value = "1";
            //txtGeneric.Value = drug1.MNC_PH_GN;
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
            autoDrug = bc.bcDB.pharM01DB.setAUTODrug();

            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            initFont();
            initGrfDrugSet();
            initGrfChief();
            initGrfPhysical();
            initGrfDiagnosis();
            initGrfDtrAdvice();
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
            grfDrugSet.Cols.Count = 13;
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
            grfDrugSet.Cols[colgrfDrugSetRemark].AllowEditing = false;
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
                rowa[colgrfDrugSetRemark] = row1["remark"].ToString();
                rowa[colgrfDrugSetFlagSave] = "0";
                i++;
            }
        }
        private void initGrfChief()
        {
            grfChief = new C1FlexGrid();
            grfChief.Font = fEdit;
            grfChief.Dock = System.Windows.Forms.DockStyle.Fill;
            grfChief.Location = new System.Drawing.Point(0, 0);
            grfChief.Cols.Count = 4;
            grfChief.Rows.Count = 2;
            //FilterRow fr = new FilterRow(grfExpn);
            grfChief.Cols[colgrfAutoName].Width = 300;

            grfChief.Cols[colgrfAutoName].Caption = "auto complete Chief Compliant";
            grfChief.Cols[colgrfAutoId].Visible = false;
            grfChief.Cols[colgrfAutoFlagSave].Visible = false;

            grfChief.Click += GrfChief_Click;
            grfChief.AfterEdit += GrfChief_AfterEdit;
            grfChief.BeforeEdit += GrfChief_BeforeEdit;
            ContextMenu menu = new ContextMenu();
            MenuItem mnuSave = new MenuItem("ยกเลิก auto complete chief compliant");
            mnuSave.Click += MnuVoidChief_Click;
            menu.MenuItems.Add(mnuSave);
            grfChief.ContextMenu = menu;

            pnChief.Controls.Add(grfChief);
            theme1.SetTheme(grfChief, bc.iniC.themeApp);

        }

        private void MnuVoidChief_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfChief.Rows.Count <= 0) return;
            if (grfChief.Row <= 0) return;
            Row row = grfChief.Rows[grfChief.Row];
            if (row[colgrfAutoId] == null) return;
            String re = bc.bcDB.autoCompDB.voidAuto(row[colgrfAutoId].ToString(), DTRCODE);
            setGrfChief();
        }

        private void GrfChief_BeforeEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfChief.Rows.Count <= 0) return;
            if (e.Col == colgrfAutoName)
            {
                Row row = grfChief.Rows[e.Row];
                row[colgrfAutoFlagSave] = "0";
                row.StyleNew.Font = fEdit;
                row.StyleNew.BackColor = ColorTranslator.FromHtml("#FFCCCC");
            }
        }

        private void GrfChief_AfterEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfChief.Rows.Count <= 0) return;
            if (e.Col == colgrfAutoName)
            {
                Row row = grfChief.Rows[e.Row];
                if (row[colgrfAutoFlagSave] != null && row[colgrfAutoFlagSave].ToString().Equals("0"))
                {
                    String re = "";
                    AutoComp autoc = new AutoComp();
                    autoc.auto_com_line1 = row[colgrfAutoName].ToString();
                    autoc.modu1 = "doctor_chief";
                    autoc.user_id = DTRCODE;
                    autoc.auto_com_id = row[colgrfAutoId] != null ? row[colgrfAutoId].ToString() : "";
                    re = bc.bcDB.autoCompDB.insertAutoComp(autoc);
                    if(int.TryParse(re, out int chk))
                    {
                        row[colgrfAutoFlagSave] = "1";
                        row.StyleNew.Font = fEditB;
                        row.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        grfChief.Rows.Count = grfChief.Rows.Count + 1;
                    }
                }
            }
        }

        private void GrfChief_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfChief.Rows.Count <= 0) return;

        }
        private void setGrfChief()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.autoCompDB.selectLine1ByModu1("doctor_chief", DTRCODE);
            int i = 1, j = 1;
            grfChief.Rows.Count = 1; grfChief.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfChief.Rows[i];
                rowa[colgrfAutoId] = row1["auto_com_id"].ToString();
                rowa[colgrfAutoName] = row1["auto_com_line1"].ToString();
                rowa[colgrfAutoFlagSave] = "0";
                i++;
            }
        }
        private void initGrfPhysical()
        {
            grfPhysical = new C1FlexGrid();
            grfPhysical.Font = fEdit;
            grfPhysical.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPhysical.Location = new System.Drawing.Point(0, 0);
            grfPhysical.Cols.Count = 4;
            grfPhysical.Rows.Count = 2;
            //FilterRow fr = new FilterRow(grfExpn);
            grfPhysical.Cols[colgrfAutoName].Width = 300;

            grfPhysical.Cols[colgrfAutoName].Caption = "auto complete Physical Exam";
            
            grfPhysical.Cols[colgrfAutoId].Visible = false;
            grfPhysical.Cols[colgrfAutoFlagSave].Visible = false;

            grfPhysical.Click += GrfPhysical_Click;
            grfPhysical.AfterEdit += GrfPhysical_AfterEdit;
            grfPhysical.BeforeEdit += GrfPhysical_BeforeEdit;
            ContextMenu menu = new ContextMenu();
            MenuItem mnuSave = new MenuItem("ยกเลิก auto complete Physical Exam");
            mnuSave.Click += MnuVoidPhysicalClick;
            menu.MenuItems.Add(mnuSave);
            grfPhysical.ContextMenu = menu;
            pnPhysical.Controls.Add(grfPhysical);
            theme1.SetTheme(grfPhysical, bc.iniC.themeApp);

        }

        private void MnuVoidPhysicalClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPhysical.Rows.Count <= 0) return;
            if (grfPhysical.Row <= 0) return;
            Row row = grfPhysical.Rows[grfChief.Row];
            if (row[colgrfAutoId] == null) return;
            String re = bc.bcDB.autoCompDB.voidAuto(row[colgrfAutoId].ToString(), DTRCODE);
            setGrfPhysical();
        }

        private void GrfPhysical_BeforeEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPhysical.Rows.Count <= 0) return;
            if (e.Col == colgrfAutoName)
            {
                Row row = grfPhysical.Rows[e.Row];
                row[colgrfAutoFlagSave] = "0";
                row.StyleNew.Font = fEdit;
                row.StyleNew.BackColor = ColorTranslator.FromHtml("#FFCCCC");

            }
        }

        private void GrfPhysical_AfterEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPhysical.Rows.Count <= 0) return;
            if (e.Col == colgrfAutoName)
            {
                Row row = grfPhysical.Rows[e.Row];
                if (row[colgrfAutoFlagSave] != null && row[colgrfAutoFlagSave].ToString().Equals("0"))
                {
                    String re = "";
                    AutoComp autoc = new AutoComp();
                    autoc.auto_com_line1 = row[colgrfAutoName].ToString();
                    autoc.modu1 = "doctor_physical";
                    autoc.user_id = DTRCODE;
                    autoc.auto_com_id = row[colgrfAutoId] != null ? row[colgrfAutoId].ToString() : "";
                    re = bc.bcDB.autoCompDB.insertAutoComp(autoc);
                    if (int.TryParse(re, out int chk))
                    {
                        row[colgrfAutoFlagSave] = "1";
                        row.StyleNew.Font = fEditB;
                        row.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        grfPhysical.Rows.Count = grfPhysical.Rows.Count + 1;
                    }
                }
            }
        }

        private void GrfPhysical_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfPhysical()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.autoCompDB.selectLine1ByModu1("doctor_physical", DTRCODE);
            int i = 1, j = 1;
            grfPhysical.Rows.Count = 1; grfPhysical.Rows.Count = dt.Rows.Count ==0 ? dt.Rows.Count+2 : dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfPhysical.Rows[i];
                rowa[colgrfAutoId] = row1["auto_com_id"].ToString();
                rowa[colgrfAutoName] = row1["auto_com_line1"].ToString();
                rowa[colgrfAutoFlagSave] = "0";
                i++;
            }
        }
        private void initGrfDiagnosis()
        {
            grfDiag = new C1FlexGrid();
            grfDiag.Font = fEdit;
            grfDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDiag.Location = new System.Drawing.Point(0, 0);
            grfDiag.Cols.Count = 4;
            grfDiag.Rows.Count = 2;
            //FilterRow fr = new FilterRow(grfExpn);
            grfDiag.Cols[colgrfAutoName].Width = 300;

            grfDiag.Cols[colgrfAutoName].Caption = "auto complete Diagnosis";
            grfDiag.Cols[colgrfAutoId].Visible = false;
            grfDiag.Cols[colgrfAutoFlagSave].Visible = false;
            ContextMenu menu = new ContextMenu();
            MenuItem mnuSave = new MenuItem("ยกเลิก auto complete Diagnosis");
            mnuSave.Click += MnuVoidDiag_Click;
            menu.MenuItems.Add(mnuSave);
            grfDiag.ContextMenu = menu;

            grfDiag.Click += GrfDiag_Click;
            grfDiag.AfterEdit += GrfDiag_AfterEdit;
            grfDiag.BeforeEdit += GrfDiag_BeforeEdit;

            pnDiagnosis.Controls.Add(grfDiag);
            theme1.SetTheme(grfDiag, bc.iniC.themeApp);

        }

        private void MnuVoidDiag_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDiag.Rows.Count <= 0) return;
            if (grfDiag.Row <= 0) return;
            Row row = grfDiag.Rows[grfChief.Row];
            if (row[colgrfAutoId] == null) return;
            String re = bc.bcDB.autoCompDB.voidAuto(row[colgrfAutoId].ToString(), DTRCODE);
            setGrfPhysical();
        }

        private void GrfDiag_BeforeEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDiag.Rows.Count <= 0) return;
            if (e.Col == colgrfAutoName)
            {
                Row row = grfDiag.Rows[e.Row];
                row[colgrfAutoFlagSave] = "0";
                row.StyleNew.Font = fEdit;
                row.StyleNew.BackColor = ColorTranslator.FromHtml("#FFCCCC");

            }
        }

        private void GrfDiag_AfterEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDiag.Rows.Count <= 0) return;
            if (e.Col == colgrfAutoName)
            {
                Row row = grfDiag.Rows[e.Row];
                if (row[colgrfAutoFlagSave] != null && row[colgrfAutoFlagSave].ToString().Equals("0"))
                {
                    String re = "";
                    AutoComp autoc = new AutoComp();
                    autoc.auto_com_line1 = row[colgrfAutoName].ToString();
                    autoc.modu1 = "doctor_diag";
                    autoc.user_id = DTRCODE;
                    autoc.auto_com_id = row[colgrfAutoId] != null ? row[colgrfAutoId].ToString() : "";
                    re = bc.bcDB.autoCompDB.insertAutoComp(autoc);
                    if (int.TryParse(re, out int chk))
                    {
                        row[colgrfAutoFlagSave] = "1";
                        row.StyleNew.Font = fEditB;
                        row.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        grfDiag.Rows.Count = grfDiag.Rows.Count + 1;
                    }
                }
            }
        }

        private void GrfDiag_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfDiag()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.autoCompDB.selectLine1ByModu1("doctor_diag", DTRCODE);
            int i = 1, j = 1;
            grfDiag.Rows.Count = 1; grfDiag.Rows.Count = dt.Rows.Count == 0 ? dt.Rows.Count + 2 : dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfDiag.Rows[i];
                rowa[colgrfAutoId] = row1["auto_com_id"].ToString();
                rowa[colgrfAutoName] = row1["auto_com_line1"].ToString();
                rowa[colgrfAutoFlagSave] = "0";
                i++;
            }
        }
        private void initGrfDtrAdvice()
        {
            grfDtrAdvice = new C1FlexGrid();
            grfDtrAdvice.Font = fEdit;
            grfDtrAdvice.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDtrAdvice.Location = new System.Drawing.Point(0, 0);
            grfDtrAdvice.Cols.Count = 4;
            grfDtrAdvice.Rows.Count = 2;
            //FilterRow fr = new FilterRow(grfExpn);
            grfDtrAdvice.Cols[colgrfAutoName].Width = 300;

            grfDtrAdvice.Cols[colgrfAutoName].Caption = "auto complete Doctor Advice";
            grfDtrAdvice.Cols[colgrfAutoId].Visible = false;
            grfDtrAdvice.Cols[colgrfAutoFlagSave].Visible = false;
            ContextMenu menu = new ContextMenu();
            MenuItem mnuSave = new MenuItem("ยกเลิก auto complete Doctor Advice");
            mnuSave.Click += MnuVoidDiag_Click;
            menu.MenuItems.Add(mnuSave);
            grfDtrAdvice.ContextMenu = menu;

            grfDtrAdvice.Click += GrfDtrAdvice_Click;
            grfDtrAdvice.AfterEdit += GrfDtrAdvice_AfterEdit;
            grfDtrAdvice.BeforeEdit += GrfDtrAdvice_BeforeEdit;

            pnDtrAdvice.Controls.Add(grfDtrAdvice);
            theme1.SetTheme(grfDtrAdvice, bc.iniC.themeApp);

        }

        private void GrfDtrAdvice_BeforeEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDtrAdvice.Rows.Count <= 0) return;
            if (e.Col == colgrfAutoName)
            {
                Row row = grfDtrAdvice.Rows[e.Row];
                row[colgrfAutoFlagSave] = "0";
                row.StyleNew.Font = fEdit;
                row.StyleNew.BackColor = ColorTranslator.FromHtml("#FFCCCC");

            }
        }

        private void GrfDtrAdvice_AfterEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDtrAdvice.Rows.Count <= 0) return;
            if (e.Col == colgrfAutoName)
            {
                Row row = grfDtrAdvice.Rows[e.Row];
                if (row[colgrfAutoFlagSave] != null && row[colgrfAutoFlagSave].ToString().Equals("0"))
                {
                    String re = "";
                    AutoComp autoc = new AutoComp();
                    autoc.auto_com_line1 = row[colgrfAutoName].ToString();
                    autoc.modu1 = "doctor_advice";
                    autoc.user_id = DTRCODE;
                    autoc.auto_com_id = row[colgrfAutoId] != null ? row[colgrfAutoId].ToString() : "";
                    re = bc.bcDB.autoCompDB.insertAutoComp(autoc);
                    if (int.TryParse(re, out int chk))
                    {
                        row[colgrfAutoFlagSave] = "1";
                        row.StyleNew.Font = fEditB;
                        row.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        grfDtrAdvice.Rows.Count = grfDtrAdvice.Rows.Count + 1;
                    }
                }
            }
        }

        private void GrfDtrAdvice_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfDtrAdvice()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.autoCompDB.selectLine1ByModu1("doctor_advice", DTRCODE);
            int i = 1, j = 1;
            grfDtrAdvice.Rows.Count = 1; grfDtrAdvice.Rows.Count = dt.Rows.Count == 0 ? dt.Rows.Count + 2 : dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfDtrAdvice.Rows[i];
                rowa[colgrfAutoId] = row1["auto_com_id"].ToString();
                rowa[colgrfAutoName] = row1["auto_com_line1"].ToString();
                rowa[colgrfAutoFlagSave] = "0";
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
            txtGeneric.Value = "";
            lbStrength.Text = "";
            txtUsing.Value = "";
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
                flagsave = arow[colgrfDrugSetFlagSave] != null ? arow[colgrfDrugSetFlagSave].ToString():"";
                if (flagsave.Equals("0")) continue;
                id = arow[colgrfDrugSetID].ToString();
                if (flagsave.Equals("3")){String re1 = bc.bcDB.drugSetDB.updateVoid(id, DTRCODE); }
                
                DrugSet drugSet = new DrugSet();
                drugSet.doctor_id = DTRCODE;
                drugSet.drug_set_name = cboDrugSetName.Text.Trim();
                drugSet.drug_set_id = id;
                drugSet.item_code = arow[colgrfDrugSetItemCode].ToString();
                drugSet.item_name = arow[colgrfDrugSetItemName].ToString();
                drugSet.frequency = arow[colgrfDrugSetFreq] != null ? arow[colgrfDrugSetFreq].ToString():"";
                drugSet.precautions = arow[colgrfDrugSetPrecau] != null ?arow[colgrfDrugSetPrecau].ToString():"";
                drugSet.using1 = arow[colgrfDrugSetUsing1] != null ?arow[colgrfDrugSetUsing1].ToString():"";
                drugSet.qty = arow[colgrfDrugSetItemQty] != null ? arow[colgrfDrugSetItemQty].ToString() : "";
                drugSet.remark = arow[colgrfDrugSetRemark] != null ? arow[colgrfDrugSetRemark].ToString() : "";
                drugSet.indication = arow[colgrfDrugSetindica] != null ? arow[colgrfDrugSetindica].ToString() : "";
                drugSet.interaction = arow[colgrfDrugSetInterac] != null ? arow[colgrfDrugSetInterac].ToString() : "";
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
