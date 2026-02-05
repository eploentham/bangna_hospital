using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
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
    public partial class FrmOpdRoomDoctor : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1ThemeController theme1;
        C1FlexGrid grfDoctor, grfRoom;
        int colDId = 1, colDName = 2;
        int colRId = 1, colRName = 2, colRDtrcode=3;
        String DEPTNO = "";
        Boolean flagLoad = false;
        public FrmOpdRoomDoctor(BangnaControl bc, String deptcode)
        {
            this.bc = bc;
            this.DEPTNO = deptcode;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            flagLoad = true;
            theme1 = new C1ThemeController();
            theme1.Theme = bc.iniC.themeApp;
            theme1.SetTheme(this, bc.iniC.themeApp);
            sep = new C1SuperErrorProvider();
            stt = new C1SuperTooltip();
            txtDtrSearch.KeyPress += TxtDtrSearch_KeyPress;
            txtDtrSearch.KeyDown += TxtDtrSearch_KeyDown;
            initFont();
            initGrfDoctor();
            initGrfRoom();
            flagLoad = false;
        }

        private void TxtDtrSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (txtDtrSearch.Text.Length > 1)
            {
                DataTable dt = new DataTable();
                dt = bc.bcDB.pm26DB.selectByCode(txtDtrSearch.Text.Trim());
                grfDoctor.Rows.Count = 1;
                int i = 1; grfDoctor.Rows.Count = dt.Rows.Count + 1;
                foreach (DataRow rowa in dt.Rows)
                {
                    Row row = grfDoctor.Rows[i];
                    grfDoctor[i, colDId] = rowa["MNC_DOT_CD"].ToString();
                    grfDoctor[i, colDName] = rowa["MNC_PFIX_DSC"].ToString() + " " + rowa["MNC_DOT_FNAME"].ToString() + " " + rowa["MNC_DOT_LNAME"].ToString();
                    i++;
                }
            }
        }

        private void TxtDtrSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void CboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (flagLoad) return;
            setGrfDoctor();
        }
        private void initFont()
        {
            fEdit = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt5BL = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Underline);
            famt5B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Bold);
            famt2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Regular);
            famt2B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Bold);
            famt2BL = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Underline);
            famt4B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 4, FontStyle.Bold);

            famt7 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Regular);
            famt7B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new System.Drawing.Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new System.Drawing.Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            fEditS = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);

            fPDF = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fPDFs2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fPDFl2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 6, FontStyle.Regular);
            fPDFs8 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 8, FontStyle.Regular);
        }
        private void initGrfDoctor()
        {
            grfDoctor = new C1FlexGrid();
            grfDoctor.Font = fEdit;
            grfDoctor.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDoctor.Location = new System.Drawing.Point(0, 0);
            grfDoctor.Rows.Count = 1;
            grfDoctor.Cols.Count = 3;
            grfDoctor.Cols[colDId].Width = 60;
            grfDoctor.Cols[colDName].Width = 200;
            grfDoctor.Cols[colDId].Caption = "ID";
            grfDoctor.Cols[colDName].Caption = "ชื่อแพทย์";
            grfDoctor.Rows[0].Height = 40;
            grfDoctor.Cols[colDId].TextAlign = TextAlignEnum.CenterCenter;
            grfDoctor.DoubleClick += GrfDoctor_DoubleClick;
            this.pnDoctor.Controls.Add(grfDoctor);
        }
        private void GrfDoctor_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(grfDoctor.Row < 1)                                   {                return;            }
            if(grfDoctor[grfDoctor.Row, colDId] == null)            {                return;            }
            lbDtrName.Text = grfDoctor[grfDoctor.Row,colDName]!=null ? grfDoctor[grfDoctor.Row, colDName].ToString() : "";
        }
        private void setGrfDoctor()
        {
            String dept = "";
            DataTable dt = new DataTable();
            dt = bc.bcDB.pm26DB.selectByDept(dept);
            grfDoctor.Rows.Count = 1;
            int i = 1; grfDoctor.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow rowa in dt.Rows)
            {
                i++;
                Row row = grfDoctor.Rows[i];
                grfDoctor[i, colDId] = rowa["MNC_DOT_CD"].ToString();
                grfDoctor[i, colDName] = rowa["MNC_PFIX_DSC"].ToString()+" "+ rowa["MNC_DOT_FNAME"].ToString()+" "+ rowa["MNC_DOT_LNAME"].ToString();
            }
        }
        private void initGrfRoom()
        {
            grfRoom = new C1FlexGrid();
            grfRoom.Font = fEdit;
            grfRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRoom.Location = new System.Drawing.Point(0, 0);
            grfRoom.Rows.Count = 1;
            grfRoom.Cols.Count = 4;
            grfRoom.Cols[colRId].Width = 60;
            grfRoom.Cols[colRName].Width = 200;
            grfRoom.Cols[colRDtrcode].Width = 100;
            grfRoom.Cols[colRId].Caption = "ID";
            grfRoom.Cols[colRName].Caption = "ชื่อห้อง";
            grfRoom.Cols[colRDtrcode].Caption = "รหัสแพทย์";
            grfRoom.Rows[0].Height = 40;
            grfRoom.Cols[colRId].TextAlign = TextAlignEnum.CenterCenter;

            this.pnRoom.Controls.Add(grfRoom);
        }
        private void setGrfRoom()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pM321DB.selectByDept(DEPTNO);
            grfRoom.Rows.Count = 1;
            int i = 1; grfRoom.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow rowa in dt.Rows)
            {
                Row row = grfRoom.Rows[i];
                grfRoom[i, colRId] = rowa["MNC_MED_RM_CD"].ToString();
                grfRoom[i, colRName] = rowa["MNC_MED_RM_NAM"].ToString();
                grfRoom[i, colRDtrcode] = rowa["MNC_DOT_FNAME"].ToString()+" "+ rowa["MNC_DOT_LNAME"].ToString();
                i++;
            }
        }
        private void FrmOpdRoomDoctor_Load(object sender, EventArgs e)
        {
            setGrfRoom();
        }
    }
}
