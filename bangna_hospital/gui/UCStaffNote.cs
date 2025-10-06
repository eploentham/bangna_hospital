using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1Ribbon;
using C1.Win.C1SplitContainer;
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
    public partial class UCStaffNote : BaseUserControl
    {
        BangnaControl BC;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        String PRENO = "", HN = "", VSDATE = "", DTRCODE = "", StatusFormUs = "", TXT = "", STATUSFORMUS = "";
        Patient PTT;
        Visit VS;
        Boolean isLoaded = false;
        RibbonLabel lfSbMessage;
        C1PictureBox picLeft, picRight;
        private Size parentPanelSize;
        DataTable DTDRUG;
        C1FlexGrid grfDrug;
        C1ThemeController theme1;
        int colgrugcode = 1, coldrugname = 2, coldrugqty = 3, coldrugunit = 4;
        Panel pndrug;
        public UCStaffNote(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt, Visit vs, String statusformus, ref RibbonLabel lfSbMessage, Size parentPanelSize)
        {
            this.PTT = ptt;
            this.VS = vs;
            this.BC = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.STATUSFORMUS = statusformus;
            this.lfSbMessage = lfSbMessage;
            this.parentPanelSize = parentPanelSize;
            InitializeComponent();
            this.Size=parentPanelSize;
            initConfig();
        }
        public UCStaffNote(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt, Visit vs, String statusformus, ref RibbonLabel lfSbMessage, Size parentPanelSize, DataTable dtdrug)
        {
            this.PTT = ptt;
            this.VS = vs;
            this.BC = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.STATUSFORMUS = statusformus;
            this.lfSbMessage = lfSbMessage;
            this.parentPanelSize = parentPanelSize;
            this.DTDRUG = dtdrug;
            InitializeComponent();
            this.Size = parentPanelSize;
            initConfig();
        }
        public UCStaffNote(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt, Visit vs, String statusformus, ref RibbonLabel lfSbMessage, DataTable dtdrug)
        {
            this.PTT = ptt;
            this.VS = vs;
            this.BC = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.STATUSFORMUS = statusformus;
            this.lfSbMessage = lfSbMessage;
            this.DTDRUG = dtdrug;
            InitializeComponent();
            //this.Size = parentPanelSize;
            initConfig();
        }
        private void initConfig()
        {
            isLoaded = true;
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            initFont(BC);
            initControl();
            this.Load += UCStaffNote_Load;

            isLoaded = false;
        }
        private void initControl()
        {
            picLeft = new C1PictureBox();
            picRight = new C1PictureBox();
            picLeft.Dock = DockStyle.Fill;
            picLeft.SizeMode = PictureBoxSizeMode.StretchImage;
            picRight.Dock = DockStyle.Fill;
            picRight.SizeMode = PictureBoxSizeMode.StretchImage;
            picLeft.Image = null;
            picRight.Image = null;
            picRight.BorderStyle = BorderStyle.Fixed3D;
            spRight.Controls.Add(picRight);
            spLeft.Controls.Add(picLeft);
            spRight.BorderColor = Color.FromArgb(240, 240, 240);
            
            if(DTDRUG.Rows.Count > 0)
            {
                C1SplitterPanel spRight1 = new C1SplitterPanel();
                spRight1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
                c1SplitContainer1.Panels.Add(spRight1);
                pndrug = new Panel();
                pndrug.Dock = DockStyle.Fill;
                spRight1.Controls.Add(pndrug);
                initGrfDrug();
                setGrfDrug();
                spLeft.SizeRatio = 30;
            }
            else
            {
                spLeft.SizeRatio = 45;
            }
            //spRight.BackColor = Color.Red;
            //picL.Image = (Image)imgl.Clone();
        }
        private void initGrfDrug()
        {
            grfDrug = new C1FlexGrid();
            grfDrug.Font = fEdit;
            grfDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrug.Location = new System.Drawing.Point(0, 0);
            grfDrug.Rows.Count = 1;
            grfDrug.Cols.Count = 5;
            grfDrug.Cols[colgrugcode].Width = 50;
            grfDrug.Cols[coldrugname].Width = 200;
            grfDrug.Cols[coldrugqty].Width = 40;
            grfDrug.Cols[coldrugunit].Width = 60;
            grfDrug.Cols[0].Width = 30;
            grfDrug.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;1=ทั่วไป;2=ประกันสังคม;3=uc30บาท;4=ประกัน
            grfDrug.Cols[colgrugcode].Caption = "code";
            grfDrug.Cols[coldrugname].Caption = " name";
            grfDrug.Cols[coldrugqty].Caption = "qty";
            grfDrug.Cols[coldrugunit].Caption = "unit";
            
            grfDrug.Cols[colgrugcode].DataType = typeof(String);
            grfDrug.Cols[coldrugname].DataType = typeof(String);
            grfDrug.Cols[coldrugqty].DataType = typeof(String);
            grfDrug.Cols[coldrugunit].DataType = typeof(String);
            
            //grfDrugMap.Rows[0].Visible = false;
            //grfDrugMap.Cols[0].Visible = false;
            grfDrug.Cols[colgrugcode].AllowEditing = false;
            grfDrug.Cols[coldrugname].AllowEditing = false;
            grfDrug.Cols[coldrugqty].AllowEditing = false;
            grfDrug.Cols[coldrugunit].AllowEditing = false;
            pndrug.Controls.Add(grfDrug);
            theme1.SetTheme(grfDrug, "ExpressionLight");
        }
        private void setGrfDrug()
        {
            if ((DTDRUG.Rows.Count > 0) && (grfDrug == null))
            {
                if (c1SplitContainer1.Panels.Count == 2)
                {
                    C1SplitterPanel spRight1 = new C1SplitterPanel();
                    spRight1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
                    c1SplitContainer1.Panels.Add(spRight1);
                    pndrug = new Panel();
                    pndrug.Dock = DockStyle.Fill;
                    spRight1.Controls.Add(pndrug);
                    initGrfDrug();
                    setGrfDrug();
                    spLeft.SizeRatio = 30;
                }
            }
            if (grfDrug == null) { return; }
            if(grfDrug.Rows.Count <= 0) { return; }
            grfDrug.Rows.Count = 1;
            int i = 1, j = 1;
            grfDrug.Rows.Count = DTDRUG.Rows.Count + 1;
            Boolean chk = false;
            //foreach (DataColumn col in DTDRUG.Columns)
            //{
            //    if (DTDRUG.Columns.Contains("drugname_t"))          {                    chk = true;                }
            //    if (DTDRUG.Columns.Contains("MNC_PH_CD"))           {                    chk = true;                }
            //    if (DTDRUG.Columns.Contains("MNC_PH_QTY_PAID"))     {                    chk = true;                }
            //    if (DTDRUG.Columns.Contains("MNC_PH_UNT_CD"))       {                    chk = true;                }
            //}
            foreach (DataRow row1 in DTDRUG.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDrug.Rows[i];
                rowa[colgrugcode] = row1["MNC_PH_CD"] !=DBNull.Value ? row1["MNC_PH_CD"].ToString():"";
                rowa[coldrugname] = row1["drugname_t"] != DBNull.Value ? row1["drugname_t"].ToString() : "";
                rowa[coldrugqty] = row1["MNC_PH_QTY_PAID"] != DBNull.Value ? row1["MNC_PH_QTY_PAID"].ToString() : "";
                rowa[coldrugunit] = row1["MNC_PH_UNT_CD"] != DBNull.Value ? row1["MNC_PH_UNT_CD"].ToString() : "";
                rowa[0] = i;
                i++;
            }
        }
        private void clearControl()
        {

        }
        public void setControl(String dtrcode, String hn, String vsdate, String preno, Patient ptt, String statusformus, DataTable dtdrug)
        {
            this.SuspendLayout();
            DTRCODE = dtrcode;
            HN = hn;
            VSDATE = vsdate;
            PRENO = preno;
            PTT = ptt;
            StatusFormUs = statusformus;
            this.DTDRUG = dtdrug;
            setControl();
            this.ResumeLayout();
        }
        public void setControl()
        {
            this.SuspendLayout();
            String file = "", dd = "", mm = "", yy = "", err = "", preno1 = "";
            try
            {
                err = "00";
                picLeft.Image = null;
                picRight.Image = null;
                int chk = 0;
                err = "01";
                dd = VSDATE.Substring(VSDATE.Length - 2);
                mm = VSDATE.Substring(5, 2);
                yy = VSDATE.Substring(0, 4);
                err = "02";
                int.TryParse(yy, out chk);
                if (chk > 2500)
                    chk -= 543;
                file = "\\\\" + BC.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                preno1 = "000000" + PRENO;
                err = "03";
                preno1 = preno1.Substring(preno1.Length - 6);
                err = "04";
                //new LogWriter("e", "FrmScanView1 setStaffNote file  " + file + preno1+" hn "+txtHn.Text+" yy "+yy);
                picLeft.Image = Image.FromFile(file + preno1 + "R.JPG");
                picRight.Image = Image.FromFile(file + preno1 + "S.JPG");
                //picRight.Dock = DockStyle.Fill;
                //picRight.SizeMode = PictureBoxSizeMode.StretchImage;
                setGrfDrug();
                this.Update();
                Console.WriteLine($"spRight size: {spRight.Width}x{spRight.Height}");
                Console.WriteLine($"picRight size: {picRight.Width}x{picRight.Height}");
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = err + " setControl " + ex.Message;
                new LogWriter("e", this.Name+" setControl " + ex.Message);
                BC.bcDB.insertLogPage(BC.userId, this.Name, "setControl", ex.Message);
            }
            this.ResumeLayout();
        }
        private void UCStaffNote_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            c1SplitContainer1.HeaderHeight = 0;
            setControl();
            Console.WriteLine($"UCStaffNote size: {this.Width}x{this.Height}");
        }
    }
}
