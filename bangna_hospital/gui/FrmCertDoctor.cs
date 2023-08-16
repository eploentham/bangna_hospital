using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.Win.BarCode;
using C1.Win.C1FlexGrid;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmCertDoctor : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEditBig, ffB;
        Color bg, fc, color;
        Label lbLoading;
        Boolean pageLoad = false;
        Patient ptt;
        C1PdfDocument _c1pdf;
        C1BarCode qrcode;
        C1FlexGrid grfHn;
        int colHnHn = 1, colHnName = 2, colHnVn = 3, colHnVsDate=4, colHnPreno=5;
        String HN = "", PRENO="", VSDATE="";
        public MemoryStream streamCertiDtr;
        AutocompleteMenu acmLine1, acmLine2, acmLine3, acmLine4;
        string[] keywords = { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "explore", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "wound", "add", "alias", "ascending", "descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby", "partial", "remove", "select", "set", "value", "var", "where", "yield" };
        string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()" };
        string[] snippets = { "if(^)\n{\n}", "if(^)\n{\n}\nelse\n{\n}", "for(^;;)\n{\n}", "while(^)\n{\n}", "do${\n^}while();", "switch(^)\n{\n\tcase : break;\n}" };
        string[] declarationSnippets = {
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n}", "private struct ^\n{\n}", "internal struct ^\n{\n}",
               "public void ^()\n{\n}", "private void ^()\n{\n}", "internal void ^()\n{\n}", "protected void ^()\n{\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }"
               };

        public FrmCertDoctor(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        public FrmCertDoctor(BangnaControl bc, String hn, String vsdate, String preno)
        {
            this.bc = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEditBig = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 7, FontStyle.Regular);
            rb1.Text = bc.iniC.grdViewFontName;
            rb2.Text = "bc.iniC.grdViewFontName "+VSDATE+" "+PRENO;
            rb3.Text = bc.iniC.hostFTP + " " + bc.iniC.folderFTP;
            qrcode = new C1BarCode();
            qrcode.ForeColor = System.Drawing.Color.Black;
            qrcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            if (bc.iniC.statusStation.Equals("IPD"))
            {
                chkStkIPD.Checked = true;
            }
            else
            {
                chkStkOPD.Checked = true;
            }
            if (chkStkIPD.Checked)
            {
                bc.bcDB.pttDB.setCboDeptIPDWdNo(cboDept, bc.iniC.station);
            }
            else
            {
                bc.bcDB.pttDB.setCboDeptOPD(cboDept, bc.iniC.station);
            }
            ptt = new Patient();

            btnPrintCert.Click += BtnPrintStk_Click;
            cboDept.SelectedIndexChanged += CboDept_SelectedIndexChanged;
            txtDtrCode.KeyUp += TxtDtrCode_KeyUp;
            txtLine1.KeyUp += TxtLine1_KeyUp;
            txtLine2.KeyUp += TxtLine2_KeyUp;
            txtLine3.KeyUp += TxtLine3_KeyUp;
            txtChk3NumDays.KeyPress += TxtChk3NumDays_KeyPress;
            
            chk2.Click += Chk2_Click;
            chk3.Click += Chk3_Click;
            chk4.Click += Chk4_Click;
            chk1.Click += Chk1_Click;

            initGrfDtrCert();
            initAutoCom();
            BuildAutocompleteMenuLine1();
            setControlForm();
            pageLoad = false;
        }

        private void TxtChk3NumDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            setTxtChk3DateEnd(e.KeyChar.ToString());
        }
        private void setTxtChk3DateEnd(String numsday)
        {
            if (numsday.Equals(""))
            {
                txtChk3DateEnd.Value = null;
                return;
            }
            int numday = 0;
            if (!int.TryParse(numsday, out numday))
            {
                txtChk3DateEnd.Value = null;
                return;
            }
            numday -= 1;        // แก้ให้ ตามHRบริษัทต้องการ
            DateTime date = new DateTime();
            DateTime.TryParse(txtVsDate.Value.ToString(), out date);
            date = date.AddDays(numday);
            if (date.Year < 2000)
            {
                date = date.AddYears(543);
            }
            txtChk3DateStart.Value = txtVsDate.Text;
            if (txtChk3DateEnd.CultureInfo.Name.IndexOf("th") >= 0)
            {
                txtChk3DateEnd.Value = date;
            }
            else
            {
                txtChk3DateEnd.Value = date;
            }
            txtChk3DateEnd.Value = date;
        }
        private void setControlForm()
        {
            if (HN.Length > 0)
            {
                setControl(HN, VSDATE, PRENO);
                panel4.Hide();
                this.Width = panel2.Width + 30;
                panel1.Width = panel2.Width + 20;
            }
        }
        private void initAutoCom()
        {
            acmLine1 = new AutocompleteMenuNS.AutocompleteMenu();
            acmLine1.AllowsTabKey = true;
            acmLine1.Font = new System.Drawing.Font(bc.iniC.grdViewFontName, 9F);
            acmLine1.Items = new string[0];
            acmLine1.SearchPattern = "[\\w\\.:=!<>]";
            acmLine1.TargetControlWrapper = null;

            acmLine1.SetAutocompleteMenu(txtLine1, acmLine1);
        }
        private void BuildAutocompleteMenuLine1()
        {
            var items = new List<AutocompleteItem>();
            if (bc.postoperation != null)
            {
                foreach (var item in bc.postoperation)
                    items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            }

            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in methods)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item));

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            acmLine1.SetAutocompleteItems(items);
        }
        private void TxtLine3_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtLine4.Focus();
            }
        }

        private void TxtLine2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtLine3.Focus();
            }
        }

        private void TxtLine1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtLine2.Focus();
            }
        }

        private void Chk1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlDateClear();
        }

        private void Chk4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlDateClear();
            txtChk4Date.Value = txtVsDate.Value;
            txtChk4Time.Text = txtVsTime.Text;
        }

        private void Chk3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlDateClear();
            setTxtChk3DateEnd(txtChk3NumDays.Text);
        }

        private void Chk2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlDateClear();
            txtChk2DateStart.Value = txtVsDate.Value;
            txtChk2DateEnd.Value = txtVsDate.Value;
        }
        private void setControlDateClear()
        {
            //txtChk2DateStart.Clear();
            //txtChk2DateEnd.Clear();
            //txtChk3DateStart.Clear();
            //txtChk3DateEnd.Clear();
            //txtChk4Date.Clear();
        }
        private void TxtDtrCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbDtrName.Text = bc.selectDoctorName(txtDtrCode.Text.Trim());
            }
        }

        private void CboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String deptid = "";
            deptid = ((ComboBoxItem)cboDept.SelectedItem).Value;
            setGrfHn(deptid);
        }
        private void setGrfHn(String wardid)
        {
            DataTable dt = new DataTable();
            if (chkStkIPD.Checked)
            {
                dt = bc.bcDB.pttDB.selectPatientinWardIPD(wardid);
            }
            else
            {
                DateTime dtstart1 = DateTime.Now;
                String deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(wardid);
                dt = bc.bcDB.vsDB.selectPttHiinDept1(deptid, wardid, dtstart1.ToString("yyyy-MM-dd"), dtstart1.ToString("yyyy-MM-dd"));
            }

            grfHn.Rows.Count = 1;
            grfHn.Rows.Count = dt.Rows.Count + 1;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfHn[i, colHnHn] = row1["MNC_HN_NO"].ToString();
                grfHn[i, colHnName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                grfHn[i, colHnVsDate] = row1["MNC_DATE"].ToString();
                grfHn[i, colHnPreno] = row1["MNC_PRE_NO"].ToString();
                if (chkStkIPD.Checked)
                {
                    grfHn[i, colHnVn] = "";
                    
                }
                else
                {
                    grfHn[i, colHnVn] = "";
                }

                grfHn[i, 0] = i;
                if (i % 2 == 0)
                {
                    grfHn.Rows[i].StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
                }
                else
                {
                    grfHn.Rows[i].StyleDisplay.BackColor = Color.Cornsilk;
                }
            }
        }
        private void BtnPrintStk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (lbDtrName.Text.Length > 0)
            {
                printCertDoctoriTextSharpThai();
            }
            else
            {
                lb1.Text = "ไม่พบรหัสแพทย์";
            }
        }
        private String insertCertDoctor()
        {
            String certid = "";
            MedicalCertificate mcerti = new MedicalCertificate();
            mcerti.active = "1";
            mcerti.an = "";
            mcerti.certi_id = "";
            mcerti.certi_code = "";
            mcerti.dtr_code = txtDtrCode.Text.Trim();
            mcerti.dtr_name_t = lbDtrName.Text;
            mcerti.status_ipd = "O";
            mcerti.visit_date = VSDATE;
            mcerti.visit_time = txtVsTime.Text;
            mcerti.remark = "";
            mcerti.line1 = txtLine1.Text;
            mcerti.line2 = txtLine2.Text;
            mcerti.line3 = txtLine3.Text;
            mcerti.line4 = txtLine4.Text;
            mcerti.hn = txtHn.Text;
            mcerti.pre_no = PRENO;
            mcerti.ptt_name_e = txtNameE.Text;
            mcerti.ptt_name_t = txtNameT.Text;
            mcerti.doc_scan_id = "";
            bc.bcDB.mcertiDB.insertMedicalCertificate(mcerti, "");
            certid = bc.bcDB.mcertiDB.selectCertIDByHn(txtHn.Text, PRENO, VSDATE);

            return certid;
        }
        private void printCertDoctoriTextSharpThai()
        {
            String certid = "";
            certid = insertCertDoctor();
            certid = certid.Replace("555", "");





            System.Drawing.Font fontMS12 = new System.Drawing.Font("Microsoft Sans Serif", 12);
            BaseFont bfR, bfR1, bfRB;
            BaseColor clrBlack = new iTextSharp.text.BaseColor(0, 0, 0);
            string myFont = Environment.CurrentDirectory + "\\THSarabunNew.ttf";
            string myFontB = Environment.CurrentDirectory + "\\THSarabunNew Bold.ttf";
            String filename = Environment.CurrentDirectory + "\\" + txtHn.Text.Trim() + "_" + VSDATE + "_" + PRENO + ".pdf";

            bfR = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfR1 = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfRB = BaseFont.CreateFont(myFontB, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfR, 12, iTextSharp.text.Font.NORMAL, clrBlack);

            var logo = iTextSharp.text.Image.GetInstance(Environment.CurrentDirectory + "\\LOGO-BW-tran.jpg");
            logo.SetAbsolutePosition(20, PageSize.A4.Height - 60);
            logo.ScaleAbsoluteHeight(50);
            logo.ScaleAbsoluteWidth(50);

            FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
            FileStream output = null;
            PdfWriter writer = null;
            try
            {
                String vstime = "", vsDateTH="", docscanid = "";
                docscanid = bc.bcDB.mcertiDB.selectDocScanIDByHn(txtHn.Text.Trim(), PRENO, VSDATE);
                DateTime vsdat1 = new DateTime();
                vstime = "0000" + ptt.visitTime;
                vstime = vstime.Substring(vstime.Length - 4);
                vstime = vstime.Substring(0, 2) + ":" + vstime.Substring(vstime.Length - 2, 2);
                DateTime.TryParse(ptt.visitDate, out vsdat1);
                if (vsdat1.Year < 2000)
                {
                    vsdat1 = vsdat1.AddYears(543);
                }
                vsDateTH = vsdat1.ToString("dd-MM-yyyy");
                qrcode.CodeType = C1.BarCode.CodeType.QRCode;
                qrcode.Text = txtHn.Text.Trim()+" "+txtNameT.Text.Trim() + " " + vsDateTH+" "+ certid;
                System.Drawing.Image imgqrcode = qrcode.Image;
                var imgqrcode1 = iTextSharp.text.Image.GetInstance(imgqrcode,BaseColor.WHITE);

                output = new FileStream(filename, FileMode.Create);
                writer = PdfWriter.GetInstance(doc, output);
                doc.Open();
                doc.Add(logo);
                int linenumber = 820, colCenter = 200, fontSize0 = 8, fontSize1 = 16, fontSize2 = 18, fontSize3 = 18;
                PdfContentByte canvas = writer.DirectContent;

                canvas.BeginText();
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostname, 80, linenumber, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, 780, 0);
                canvas.SetFontAndSize(bfR, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostnamee, 80, linenumber - 15, 0);
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostaddresst, 80, linenumber - 30, 0);
                canvas.EndText();
                linenumber = 720;
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, fontSize3);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "ใบรับรองแพทย์", PageSize.A4.Width / 2, linenumber + 40, 0);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "เลขที่ " + certid, 530, linenumber + 40, 0);

                canvas.SetFontAndSize(bfR, fontSize1);
                linenumber += 10;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ข้าพเจ้า", 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................................................", 85, linenumber-2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์แผนปัจจุบันชั้นหนึ่งสาขาเวชกรรมเลขที่", 335, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............", 528, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, lbDtrName.Text.Trim(), 90, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDtrCode.Text.Trim(), 535, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ขอรับรองว่า", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................................................................................................................................................................", 90, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtNameT.Text.Trim()+" HN "+ txtHn.Text.Trim(), 93, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ได้รับการตรวจโรคจากโรงพยาบาลนี้เมื่อ วันที่", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............................................................................................", 225, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, vsDateTH, 230, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เวลามาตรวจ", 470, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............", 528, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "น. ", 570, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, vstime, 535, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ปรากฏว่าป่วยเป็นโรค", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".........................................................................................................................................................................", 130, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine1.Text.Trim(), 133, linenumber + 3, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine2.Text.Trim(), 40, linenumber + 3, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine3.Text.Trim(), 40, linenumber + 3, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine4.Text.Trim(), 40, linenumber + 3, 0);

                //linenumber -= 20;
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   " + chk1.Text, 50, linenumber, 0);
                if (chk1.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber+2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   " + chk2.Text, 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................", 245, linenumber - 2, 0);
                
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ถึงวันที่", 360, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................", 395, linenumber - 2, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, ptt.visitDate, 400, linenumber + 3, 0);
                if (chk2.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, vsDateTH, 250, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, vsDateTH, 400, linenumber + 3, 0);
                }//txtChk3NumDays
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   " + chk3.Text, 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............", 240, linenumber - 2, 0);
                
                canvas.ShowTextAligned(Element.ALIGN_LEFT, label6.Text.Trim(), 275, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................  ถึงวันที่  ..............................", 345, linenumber - 2, 0);
                
                if (chk3.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);

                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtChk3NumDays.Text.Trim(), 250, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, vsDateTH, 355, linenumber + 3, 0);
                    DateTime dateend = new DateTime();
                    DateTime.TryParse(txtChk3DateEnd.Value.ToString(), out dateend);

                    canvas.ShowTextAligned(Element.ALIGN_LEFT, dateend.ToString("dd-MM-yyyy"), 495, linenumber + 3, 0);
                }
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   " + chk4.Text, 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................  เวลา", 185, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................", 320, linenumber - 2, 0);
                if (chk4.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                    //txtChk4Date
                    DateTime date = new DateTime();
                    DateTime.TryParse(txtChk4Date.Text, out date);
                    if (date.Year < 2000)
                    {
                        date = date.AddYears(543);
                    }
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, date.ToString("dd-MM-yyyy"), 190, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtChk4Time.Text.Trim(), 325, linenumber + 3, 0);
                }
                //linenumber -= 20;
                linenumber -= 20;
                canvas.SetFontAndSize(bfRB, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(กรณีที่4. ใช้รับรองว่ามารับการตรวจรักษาจริงเท่านั้น  มิใช่เป็นใบรับรองแพทย์ลาป่วย)" , 30, linenumber, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "***  เอกสารนี้ไม่สามารถใช้ประกอบการดำเนินคดีได้", 30, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);

                //linenumber -= 20;
                //linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ลงชื่อ", 380, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................", 405, linenumber - 2, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................", 50, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(                                                      )", 385, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................", 395, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, lbDtrName.Text.Trim()+" ["+txtDtrCode.Text.Trim()+"]", 398, linenumber + 3, 0);

                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผู้รับเอกสารใบรับรองแพทย์", 75, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์ผู้ตรวจโรค", 440, linenumber, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "FM-MED-001 (01-05/02/59)(1/1)", 50, linenumber+2, 0);

                logo.SetAbsolutePosition(310, linenumber+3);
                logo.ScaleAbsoluteHeight(60);
                logo.ScaleAbsoluteWidth(60);
                doc.Add(logo);
                imgqrcode1.SetAbsolutePosition(240, linenumber+3);
                imgqrcode1.ScaleAbsoluteHeight(50);
                imgqrcode1.ScaleAbsoluteWidth(50);
                doc.Add(imgqrcode1);

                canvas.EndText();
                canvas.Stroke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                doc.Close();
                output.Close();
                writer.Close();

                byte[] data = File.ReadAllBytes(filename);
                streamCertiDtr = new MemoryStream(data);
                streamCertiDtr.Position = 0;
                Application.DoEvents();
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = "";
                dsc.hn = txtHn.Text;
                
                dsc.an = "";
                dsc.vn = ptt.vn;
                
                dsc.visit_date = VSDATE;
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = "1200000030";
                dsc.pre_no = PRENO;
                dsc.an_date = "";
                dsc.folder_ftp = bc.iniC.folderFTP;
                dsc.status_ipd = "O";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_ml = "2";
                dsc.ml_fm = "FM-MED-001";
                String reDocScanId = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);
                long chk = 0;
                if (long.TryParse(reDocScanId, out chk))
                {
                    dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                    String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-"));
                    ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                    {
                        //File.Delete(filename);
                        //MedicalCertificate mcerti = new MedicalCertificate();
                        //mcerti.active = "1";
                        //mcerti.an = "";
                        //mcerti.certi_id = "";
                        //mcerti.certi_code = "";
                        //mcerti.dtr_code = txtDtrCode.Text.Trim();
                        //mcerti.dtr_name_t = lbDtrName.Text;
                        //mcerti.status_ipd = "O";
                        //mcerti.visit_date = VSDATE;
                        //mcerti.visit_time = txtVsTime.Text;
                        //mcerti.remark = "";
                        //mcerti.line1 = txtLine1.Text;
                        //mcerti.line2 = txtLine2.Text;
                        //mcerti.line3 = txtLine3.Text;
                        //mcerti.line4 = txtLine4.Text;
                        //mcerti.hn = txtHn.Text;
                        //mcerti.pre_no = PRENO;
                        //mcerti.ptt_name_e = txtNameE.Text;
                        //mcerti.ptt_name_t = txtNameT.Text;
                        //mcerti.doc_scan_id = reDocScanId;
                        //bc.bcDB.mcertiDB.insertMedicalCertificate(mcerti, "");
                        //String certid = bc.bcDB.mcertiDB.selectCertIDByHn(txtHn.Text, PRENO, VSDATE);
                        bc.bcDB.mcertiDB.updateDocScanIdByPk("555" + certid, reDocScanId);
                        bc.bcDB.vsDB.updateMedicalCertId(txtHn.Text, PRENO, VSDATE, "555"+certid);
                        System.Threading.Thread.Sleep(500);
                    }
                }
                if ((HN.Length > 0) && (PRENO.Length > 0))
                {
                    this.Dispose(true);
                }
                else
                {
                    Process p = new Process();
                    ProcessStartInfo s = new ProcessStartInfo(Environment.CurrentDirectory + "\\" + txtHn.Text.Trim() + ".pdf");
                    p.StartInfo = s;
                    p.Start();
                }
            }
        }
        private void printCertDoctorC1()
        {
            _c1pdf = new C1PdfDocument();
            _c1pdf.DocumentInfo.Producer = "C1Pdf";
            _c1pdf.Security.AllowCopyContent = true;
            _c1pdf.Security.AllowEditAnnotations = true;
            _c1pdf.Security.AllowEditContent = true;
            _c1pdf.Security.AllowPrint = true;

            int gapLine = 16, gapLine1 = 8, gapX = 20, gapY = 24, xCol2 = 90, xCol1 = 20, xCol3 = 200, xCol4 = 390, xCol5 = 1030;
            Size size = new Size();

            PaperSize pkSize, pkA5;
            //PrintDocument printDoc = new PrintDocument();
            pkA5 = new PaperSize();
            //for (int i = 0; i < printDoc.PrinterSettings.PaperSizes.Count; i++)
            //{
            //    pkSize = printDoc.PrinterSettings.PaperSizes[i];
            //    if (pkSize.PaperName.IndexOf("A5")>=0)
            //    {
            //        pkA5 = pkSize;
            //    }
            //    //comboPaperSize.Items.Add(pkSize);
            //}
            pkA5.Width = 583;
            pkA5.Height = 827;
            SizeF sizeA5 = new SizeF();
            sizeA5.Width = pkA5.Width;
            sizeA5.Height = pkA5.Height;
            _c1pdf.PageSize = sizeA5;
            System.Drawing.Image imgLogo = Resources.LOGO_BW_tran;

            _c1pdf.Landscape = false;
            //_c1pdf.PaperKind
            _c1pdf.DocumentInfo.Title = "ใบรับรองแพทย์ HN " + txtHn.Text;

            RectangleF rcPage = GetPageRect();
            RectangleF rc = _c1pdf.PageRectangle; ;

            System.Drawing.Font titleFont = new System.Drawing.Font(bc.iniC.grdViewFontName, 18, FontStyle.Bold);
            System.Drawing.Font hdrFont = new System.Drawing.Font(bc.iniC.grdViewFontName, 16, FontStyle.Bold);
            System.Drawing.Font ftrFont = new System.Drawing.Font(bc.iniC.grdViewFontName, 8);
            System.Drawing.Font txtFont = new System.Drawing.Font(bc.iniC.grdViewFontName, 12, FontStyle.Regular);

            float newWidth = imgLogo.Width * 100 / imgLogo.HorizontalResolution;
            float newHeight = imgLogo.Height * 100 / imgLogo.VerticalResolution;

            float widthFactor = 4.8F;
            float heightFactor = 4.8F;

            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    widthFactor = 1;
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                    //newWidth = newWidth / 1.2;
                    //newHeight = newHeight / 1.2;
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }
            RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            _c1pdf.DrawImage(imgLogo, recf, ContentAlignment.MiddleCenter, ImageSizeModeEnum.Scale);

            //gapY += gapLine;
            rc.Y = gapY;
            rc.X = xCol2;
            //rc = RenderParagraph("Department "+ operNote.dept_name, txtFont, rcPage, rc);
            _c1pdf.DrawString(bc.iniC.hostname, hdrFont, Brushes.Black, rc);
            //gapY += gapLine;
            rc.Y += (gapY);
            _c1pdf.DrawString(bc.iniC.hostnamee, hdrFont, Brushes.Black, rc);
            rc.Y += (gapY);
            _c1pdf.DrawString(bc.iniC.hostaddresst, hdrFont, Brushes.Black, rc);
            rc.X = (sizeA5.Width / 2) + 20;
            rc.Y += (gapY);
            rc.Y += (gapY);
            _c1pdf.DrawString("ใบรับรองแพทย์ ", titleFont, Brushes.Black, rc);

            rc.Y += (gapY);
            rc.X = xCol1;
            _c1pdf.DrawString("ข้าพเจ้า ..............................................................................................................  แพทย์แผนปัจจุบันชั้นหนึ่งสาขาเวชกรรมเลขที่ ...........................", txtFont, Brushes.Black, rc);
            rc.X += 45;
            rc.Y -= 2;
            _c1pdf.DrawString(lbDtrName.Text, txtFont, Brushes.Black, rc);
            rc.X = 470;
            _c1pdf.DrawString(txtDtrCode.Text, txtFont, Brushes.Black, rc);

            String PathName = "medical", datetick = "", fileName = "";
            datetick = DateTime.Now.Ticks.ToString();
            if (!Directory.Exists("report"))
            {
                Directory.CreateDirectory("report");
            }
            fileName = "report\\" + datetick + ".pdf";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                System.Threading.Thread.Sleep(100);
            }
            // save to high-quality file and show it
            _c1pdf.ImageQuality = ImageQualityEnum.High;
            //statusBar1.Text = "Saving high-quality pdf...";
            String path = Path.GetDirectoryName(Application.ExecutablePath);
            _c1pdf.Save(path + "\\" + fileName);
            Process.Start(fileName);
        }
        internal RectangleF GetPageRect()
        {
            RectangleF rcPage = _c1pdf.PageRectangle;
            rcPage.Inflate(-72, -72);
            return rcPage;
        }
        private void initGrfDtrCert()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);

            pnPttinDept.Controls.Add(grfHn);

            grfHn.Rows.Count = 1;
            grfHn.Cols.Count = 6;
            grfHn.Cols[colHnHn].Caption = "HN";
            grfHn.Cols[colHnName].Caption = "Name";
            grfHn.Cols[colHnVn].Caption = "VN";

            grfHn.Cols[colHnHn].Width = 90;
            grfHn.Cols[colHnName].Width = 250;
            grfHn.Cols[colHnVn].Width = 100;

            grfHn.Cols[colHnHn].AllowEditing = false;
            grfHn.Cols[colHnName].AllowEditing = false;
            grfHn.Cols[colHnVn].AllowEditing = false;
            grfHn.Cols[colHnHn].Visible = true;
            grfHn.Cols[colHnName].Visible = true;
            grfHn.Cols[colHnVn].Visible = true;
            grfHn.Cols[colHnVsDate].Visible = true;
            grfHn.Cols[colHnPreno].Visible = false;

            grfHn.Click += GrfHn_Click;
        }

        private void GrfHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHn.Row <= 0) return;
            if (grfHn.Col <= 0) return;
            setControl(grfHn[grfHn.Row, colHnHn].ToString(), grfHn[grfHn.Row, colHnVsDate].ToString(), grfHn[grfHn.Row, colHnPreno].ToString());
        }
        private void setControl(String hn, String vsdate, String preno)
        {
            String vstime = "";

            ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetVisitOPDByHn(hn, vsdate, preno);
            txtHn.Text = hn;
            txtNameT.Text = ptt.Name;
            txtDOB.Text = ptt.patient_birthday;

            txtVsDate.Value = ptt.visitDate;
            
            vstime = "0000"+ptt.visitTime;
            vstime = vstime.Substring(vstime.Length - 4);
            vstime = vstime.Substring(0, 2)+":"+vstime.Substring(vstime.Length - 2, 2);
            txtVsTime.Text = vstime;

            txtLine1.Text = "";
            txtLine2.Text = "";
            txtLine3.Text = "";
            txtLine4.Text = "";

            chk1.Checked = true;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;

            txtChk3NumDays.Text = "1";
            txtChk4Time.Text = vstime;
            txtDtrCode.Text = ptt.dtrcode;
            lbDtrName.Text = bc.selectDoctorName(txtDtrCode.Text.Trim());
            setControlDateClear();
        }
        class DeclarationSnippet : SnippetAutocompleteItem
        {
            public static string RegexSpecSymbolsPattern = @"[\^\$\[\]\(\)\.\\\*\+\|\?\{\}]";

            public DeclarationSnippet(string snippet)
                : base(snippet)
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var pattern = Regex.Replace(fragmentText, RegexSpecSymbolsPattern, "\\$0");
                if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                    return CompareResult.Visible;
                return CompareResult.Hidden;
            }
        }
        /// <summary>
        /// Divides numbers and words: "123AND456" -> "123 AND 456"
        /// Or "i=2" -> "i = 2"
        /// </summary>
        class InsertSpaceSnippet : AutocompleteItem
        {
            string pattern;

            public InsertSpaceSnippet(string pattern)
                : base("")
            {
                this.pattern = pattern;
            }

            public InsertSpaceSnippet()
                : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                if (Regex.IsMatch(fragmentText, pattern))
                {
                    Text = InsertSpaces(fragmentText);
                    if (Text != fragmentText)
                        return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }

            public string InsertSpaces(string fragment)
            {
                var m = Regex.Match(fragment, pattern);
                if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                    return fragment;
                return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return Text;
                }
            }
        }

        /// <summary>
        /// Inerts line break after '}'
        /// </summary>
        class InsertEnterSnippet : AutocompleteItem
        {
            int enterPlace = 0;

            public InsertEnterSnippet()
                : base("[Line break]")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var tb = Parent.TargetControlWrapper;

                var text = tb.Text;
                for (int i = Parent.Fragment.Start - 1; i >= 0; i--)
                {
                    if (text[i] == '\n')
                        break;
                    if (text[i] == '}')
                    {
                        enterPlace = i;
                        return CompareResult.Visible;
                    }
                }

                return CompareResult.Hidden;
            }

            public override string GetTextForReplace()
            {
                var tb = Parent.TargetControlWrapper;

                //insert line break
                tb.SelectionStart = enterPlace + 1;
                tb.SelectedText = "\n";
                Parent.Fragment.Start += 1;
                Parent.Fragment.End += 1;
                return Parent.Fragment.Text;
            }

            public override string ToolTipTitle
            {
                get
                {
                    return "Insert line break after '}'";
                }
            }
        }
        private void FrmCertDoctor_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 2023-07-05";
        }
    }
}
