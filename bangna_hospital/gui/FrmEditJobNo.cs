using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1Themes;
using PCSC.Exceptions;
using PCSC.Utils;
using PCSC;
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
    public partial class FrmEditJobNo : Form
    {
        BangnaControl bc;
        C1FlexGrid grfHn, grfAn ,grfDeptVisit, grfvoid, grfXray, grfXrayItem, grfLabReq;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B;
        int colgrfHnHN = 1, colgrfHnDocNo=2, colgrfHnDocDate=3, colgrfHnDocCD=4, colgrfHnDocSts=5,colgrfHnAmt=6, colgrfHnJobNo=7, colgrfHnJobNoOld = 8;
        int colgrfAnanno = 1, colgrfAnAdDate = 2;
        int colgrfDeptVisitVsdate = 1, colgrfDeptVisitPreno=2, colgrfDeptVisitStatusAdmit=3, colgrfDeptDept=4, colgrfDeptVisitVn=5;
        int colgrfxrayreqno = 1, colgrfxrayyear = 2, colgrfxrayreqdate = 3, colgrfxraystatuspacs = 4, colgrfxrayitemcode=5, colgrfxrayitemname=6;
        int colgrfLabReqHn = 1, colgrfLabReqReqno = 2, colgrfLabReqReqdate = 3, colgrfLabReqlbcode = 4, colgrfLabReqyear = 5, colgrfLabReqlabname = 6, colgrfLabReqordname = 7, colgrfLabRequsrcode = 8, colgrfLabRequsrname = 9, colgrfLabReqstatussend = 10, colgrfLabReqdatesend = 11;
        Boolean pageLoad = false;
        C1ThemeController theme1;
        Patient ptt;
        String FLAGFOCUS = "";
        public FrmEditJobNo(BangnaControl bc)
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
            theme1 = new C1ThemeController();
            ptt = new Patient();
            initGrfHn();
            initGrfAn();
            initGrfDeptVisit();
            initGrfXray();
            initGrfXrayItems();
            initGrflabReq();
            chkDeptOPD.Checked = true;
            chkDeptIPD.Checked = false;
            chkDeptOPDNew.Checked = true;
            chkDeptIPDNew.Checked = false;
            //cboDeptNew
            chkDeptOPD1.Checked = true;
            chkDeptIPD1.Checked = false;
            bc.bcDB.pttDB.setCboDeptOPD(cboDept1, bc.iniC.station);

            txtPttHn.KeyUp += TxtPttHn_KeyUp;
            btnGet.Click += BtnGet_Click;
            btnRevest.Click += BtnRevest_Click;
            c1Button1.Click += C1Button1_Click;
            txtAnHN.KeyUp += TxtAnHN_KeyUp;
            txtAnANNO1.Enter += TxtAnANNO1_Enter;
            txtAnANNO1.Leave += TxtAnANNO1_Leave;
            txtAnANNO2.Enter += TxtAnANNO2_Enter;
            txtAnANNO2.Leave += TxtAnANNO2_Leave;
            btnAn1.Click += BtnAn1_Click;
            btnAn2.Click += BtnAn2_Click;
            btnAnMerge.Click += BtnAnMerge_Click;
            txtDeptHN.KeyUp += TxtDeptHN_KeyUp;
            btnDeptUpdate.Click += BtnDeptUpdate_Click;
            cboDeptNew.SelectedItemChanged += CboDeptNew_SelectedItemChanged;
            btnTokenNew.Click += BtnTokenNew_Click;
            btnTokenGen.Click += BtnTokenGen_Click;
            txtVoidHN.KeyUp += TxtVoidHN_KeyUp;
            btnVoid.Click += BtnVoid_Click;
            txtDtrcode.KeyUp += TxtDtrcode_KeyUp;
            btnDtrupdate.Click += BtnDtrupdate_Click;
            txtDtrAppoint.KeyPress += TxtDtrAppoint_KeyPress;
            c1Button2.Click += C1Button2_Click;
            btnUpdateDrugExpireDate.Click += BtnUpdateDrugExpireDate_Click;
            btnXray.Click += BtnXray_Click;
            txtXrayHn.KeyUp += TxtXrayHn_KeyUp;
            btnXrayDelReq.Click += BtnXrayDelReq_Click;
            cboDept1.SelectedItemChanged += CboDept1_SelectedItemChanged;
            txtLabReqHn.KeyUp += TxtLabReqHn_KeyUp;
            pageLoad = false;
        }

        private void TxtLabReqHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByHn(txtLabReqHn.Text.Trim());
                lbLabReqPttnameT.Text = ptt.Name;
                setGrfLabReq();
            }
        }
        private void CboDept1_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if (chkDeptOPD1.Checked)
            {
                String secid = ((ComboBoxItem)cboDept1.SelectedItem).Value;
                String deptid = bc.bcDB.pm32DB.getDeptNoOPD(secid);
                label34.Text = secid;
                label35.Text = deptid;
            }
            else
            {
                //bc.bcDB.pm32DB.setCboDeptIPD(cboDept1, bc.iniC.station);
            }
        }

        private void BtnXrayDelReq_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = bc.bcDB.xrayT02DB.deleteReqNo(txtXrayReqYear.Text.Trim(), txtXrayReqno.Text.Trim(), txtXrayReqdate.Text.Trim());
        }

        private void TxtXrayHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByHn(txtXrayHn.Text.Trim());
                lbXrayPttnameT.Text = ptt.Name;
                setGrfXray();
            }
        }

        private void BtnXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = bc.bcDB.xrayT02DB.updateStatusPaca(txtXrayReqYear.Text.Trim(), txtXrayReqno.Text.Trim(), txtXrayReqdate.Text.Trim(), txtXrayHn.Text.Trim());
        }

        private void BtnUpdateDrugExpireDate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "update pharmacy_t15 set mnc_exp_dat = mnc_exp_dat + 1000";
        }

        // Inside the C1Button2_Click method
        private void C1Button2_Click(object sender, EventArgs e)
        {
            //var contextFactory = ContextFactory.Instance;
            //using (var context = contextFactory.Establish(SCardScope.System))
            //{
            //    var readerNames = context.GetReaders();
            //    if (readerNames == null || readerNames.Length == 0)
            //    {
            //        Console.WriteLine("No smart card readers found.");
            //        return;
            //    }

            //    var readerName = readerNames[0];
            //    Console.WriteLine($"Using reader: {readerName}");

            //    using (var reader = context.ConnectReader(readerName, SCardShareMode.Shared, SCardProtocol.Any))
            //    {
            //        var apdu = new CommandAPDU(IsoCase.Case2Short, reader.Protocol)
            //        {
            //            CLA = 0x80, // Class
            //            INS = 0xB0, // Instruction
            //            P1 = 0x00,  // Parameter 1
            //            P2 = 0x11,  // Parameter 2
            //            Lc = 0x02,
            //            Data = 0x00,
            //            Le = 0x64   // Expected length of the response
            //        };
            //        //var fullNameThApdu = new CommandAPDU(IsoCase.Case2Short, reader.Protocol)
            //        //{
            //        //    CLA = 0x80,
            //        //    INS = 0xB0,
            //        //    P1 = 0x00,
            //        //    P2 = 0x11,
            //        //    Le = 0x64
            //        //};
            //        //ReadAndPrintData(reader, fullNameThApdu, "Full Name (TH)");
            //        var receivePci = new SCardPCI(); // IO returned protocol control information.
            //        var sendPci = SCardPCI.GetPci(reader.Protocol); // Protocol Control Information (T0, T1 or Raw)
            //        var receiveBuffer = new byte[256];
            //        var command = apdu.ToArray();

            //        var bytesReceived = reader.Transmit(
            //                sendPci, // Protocol Control Information (T0, T1 or Raw)
            //                command, // command APDU
            //                command.Length,
            //                receivePci, // returning Protocol Control Information
            //                receiveBuffer,
            //                receiveBuffer.Length); // data buffer

            //        var responseApdu = new ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, reader.Protocol);
            //        Console.WriteLine("SW1: {0:X2}, SW2: {1:X2}\nUid: {2}",
            //            responseApdu.SW1,
            //            responseApdu.SW2,
            //            responseApdu.HasData ? BitConverter.ToString(responseApdu.GetData()) : "No uid received");
            //    }
            //}
            //try
            //{
            //    ThaiNationalIDCardReader cardReader = new ThaiNationalIDCardReader();
            //    PersonalPhoto personalPhoto = cardReader.GetPersonalPhoto();

            //    Console.WriteLine($"CitizenID: {personalPhoto.CitizenID}");
            //    Console.WriteLine($"ThaiPersonalInfo: {personalPhoto.ThaiPersonalInfo}");
            //    Console.WriteLine($"EnglishPersonalInfo: {personalPhoto.EnglishPersonalInfo}");
            //    Console.WriteLine($"DateOfBirth: {personalPhoto.DateOfBirth}");
            //    Console.WriteLine($"Sex: {personalPhoto.Sex}");
            //    Console.WriteLine($"AddressInfo: {personalPhoto.AddressInfo}");
            //    Console.WriteLine($"IssueDate: {personalPhoto.IssueDate}");
            //    Console.WriteLine($"ExpireDate: {personalPhoto.ExpireDate}");
            //    Console.WriteLine($"Issuer: {personalPhoto.Issuer}");
            //    Console.WriteLine($"Photo: {personalPhoto.Photo}");
            //}
            //catch (Exception e1)
            //{
            //    Console.WriteLine(e1);
            //}
        }
        private void ReadAndPrintData(ICardReader reader, CommandAPDU apdu, string fieldName)
        {
            var receivePci = new SCardPCI(); // IO returned protocol control information.
            var sendPci = SCardPCI.GetPci(reader.Protocol); // Protocol Control Information (T0, T1 or Raw)
            var receiveBuffer = new byte[256];
            var command = apdu.ToArray();
            byte[] citizenID = { 0x80, 0xb0, 0x00, 0x04, 0x02, 0x00, 0x0d };
            var bytesReceived = reader.Transmit(
                    sendPci, // Protocol Control Information (T0, T1 or Raw)
                    citizenID, // command APDU
                    command.Length,
                    receivePci, // returning Protocol Control Information
                    receiveBuffer,
                    receiveBuffer.Length); // data buffer

            var responseApdu = new ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, reader.Protocol);
            if (responseApdu.HasData)
            {
                var data = Encoding.ASCII.GetString(responseApdu.GetData());
                Console.WriteLine($"{fieldName}: {data}");
            }
            else
            {
                Console.WriteLine($"{fieldName}: No data received.");
            }
        }
        private void TxtDtrAppoint_KeyPress(object sender, KeyPressEventArgs e)
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
        }
        private void BtnDtrupdate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(int.TryParse(txtDtrcode.Text.Trim(), out int flagFocus))
            {
                String re = bc.bcDB.pm26DB.updateAppoint(txtDtrcode.Text.Trim(), txtDtrAppoint.Text.Trim());
            }
        }
        private void TxtDtrcode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                PatientM26 dtr = new PatientM26();
                dtr = bc.bcDB.pm26DB.selectByPk(txtDtrcode.Text.Trim());
                lbDtrName.Text = dtr.dtrname;
                txtDtrAppoint.Value = dtr.MNC_APP_NO;
            }
        }
        private void BtnVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedExceptio  
            String re = bc.bcDB.vsDB.voidVisit(txtVoidHN.Text.Trim(),txtVoidpreno.Text.Trim(),txtVoidvsdate.Text.Trim());
            txtVoidpreno.Value = "";
            setGrfVoid();
        }
        private void setGrfVoid()
        {
            panel3.Controls.Clear();
            
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectByvsdate(txtVoidHN.Text.Trim(), txtVoidvsdate.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                grfvoid = new C1FlexGrid();
                grfvoid.Rows.Count = 1;
                grfvoid.Rows.Count = dt.Rows.Count + 1;
                grfvoid.Cols.Count = 4;
                grfvoid.AfterRowColChange += Grfvoid_AfterRowColChange;
                panel3.Controls.Add(grfvoid);
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    grfvoid[i, 1] = dr["MNC_DATE"].ToString();
                    grfvoid[i, 2] = dr["MNC_PRE_NO"].ToString();
                    grfvoid[i, 3] = dr["MNC_SHIF_MEMO"].ToString();
                    i++;
                }
            }
        }
        private void TxtVoidHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
           if(e.KeyCode== Keys.Enter)
            {
                setGrfVoid();
            }
        }

        private void Grfvoid_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if(grfvoid.Row==null) { return; }
            txtVoidpreno.Value = grfvoid[grfvoid.Row, 2].ToString();
        }

        private void TxtVoidHN_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void BtnTokenGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //var tokenGenerator = new JwtTokenGenerator("your_secret_key");
        }

        private void BtnTokenNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Token token = new Token();
            token.token_id = "";
            token.token_text = txtTokenToken.Text.Trim();
            token.user_name = "";
            token.number_days_expire = txtTokenDaysExpire.Text.Trim();
            token.date_expire = txtTokenDateExpire.Text.Trim();
            token.secret_key = txtTokenSecretKey.Text.Trim();

        }

        private void CboDeptNew_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;

        }
        private void BtnDeptUpdate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการย้ายแผนกคนไข้", "2222", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                String secid = "", deptid = "", re="";
                if (chkDeptOPDNew.Checked)
                {
                    secid = bc.bcDB.pm32DB.getSecCodeOPD(cboDeptNew.Text);
                    deptid = bc.bcDB.pm32DB.getDeptNoOPD(secid);
                    re = bc.bcDB.pt08DB.updateOPDSecNo(txtDeptHN.Text.Trim(), txtDeptVsdate.Text.Trim(), txtDeptPreno.Text.Trim(), deptid, secid);
                    if (int.TryParse(re, out int chk))
                    {
                        sbMessage.Text = "ย้ายแผนกเรียบร้อย";
                    }
                    else
                    {

                    }
                }
                else
                {
                    secid = bc.bcDB.pm32DB.getSecCodeIPD(cboDeptNew.Text);
                    deptid = bc.bcDB.pm32DB.getDeptNoIPD(secid);
                    re = bc.bcDB.pt08DB.updateSecNo(txtDeptHN.Text.Trim(), txtDeptVsdate.Text.Trim(), txtDeptPreno.Text.Trim(), deptid, secid);
                    if (int.TryParse(re, out int chk1))
                    {
                        sbMessage.Text = "ย้ายแผนกเรียบร้อย";
                    }
                    else
                    {
                        sbMessage.Text = "มีข้อผิดพลาด " + re;
                    }
                }
            }
        }
        private void TxtDeptHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByHn(txtDeptHN.Text.Trim());
                lbDeptPttnameT.Text = ptt.Name;
                setGrfDeptVisit();
            }
        }
        private void initGrfXrayItems()
        {
            grfXrayItem = new C1FlexGrid();
            grfXrayItem.Font = fEdit;
            grfXrayItem.Dock = System.Windows.Forms.DockStyle.Fill;
            grfXrayItem.Location = new System.Drawing.Point(0, 0);
            grfXrayItem.Rows.Count = 1;
            grfXrayItem.Cols.Count = 7;
            grfXrayItem.Cols[colgrfxrayreqno].Width = 100;
            grfXrayItem.Cols[colgrfxrayyear].Width = 100;
            grfXrayItem.Cols[colgrfxrayreqdate].Width = 60;
            grfXrayItem.Cols[colgrfxraystatuspacs].Width = 60;

            grfXrayItem.ShowCursor = true;
            grfXrayItem.Cols[colgrfxrayreqno].Caption = "reqno";
            grfXrayItem.Cols[colgrfxrayyear].Caption = "reqyr";
            grfXrayItem.Cols[colgrfxrayreqdate].Caption = "reqdate";
            grfXrayItem.Cols[colgrfxrayitemcode].Caption = "itemcode";
            grfXrayItem.Cols[colgrfxrayitemname].Caption = "itemname";

            grfXrayItem.Cols[colgrfxraystatuspacs].Visible = false;
            grfXrayItem.Cols[colgrfxrayreqno].AllowEditing = false;
            grfXrayItem.Cols[colgrfxrayyear].AllowEditing = false;
            grfXrayItem.Cols[colgrfxrayreqdate].AllowEditing = false;
            grfXrayItem.Cols[colgrfxraystatuspacs].AllowEditing = false;

            grfXrayItem.AllowFiltering = true;

            grfXrayItem.Click += GrfXrayItem_Click;

            pnxrayitems.Controls.Add(grfXrayItem);
            theme1.SetTheme(grfXrayItem, bc.iniC.themeApp);
        }
        private void GrfXrayItem_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfXrayItem.Row <= 0) return;
            if (grfXrayItem[grfXrayItem.Row, colgrfxrayitemcode] == null) return;
        }
        private void setGrfXrayItems(String hn, String reqno, String reqdate)
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.xrayT02DB.selectbyHNReqNo(hn, reqdate, reqno);
            grfXrayItem.Rows.Count = 1;
            grfXrayItem.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfXrayItem.Rows[i];
                rowa[colgrfxrayreqno] = row1["req_no"].ToString();
                rowa[colgrfxrayyear] = row1["MNC_REQ_YR"].ToString();
                rowa[colgrfxrayreqdate] = row1["req_date"].ToString();
                rowa[colgrfxrayitemcode] = row1["order_code"].ToString();
                rowa[colgrfxrayitemname] = row1["order_name"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void initGrfXray()
        {
            grfXray = new C1FlexGrid();
            grfXray.Font = fEdit;
            grfXray.Dock = System.Windows.Forms.DockStyle.Fill;
            grfXray.Location = new System.Drawing.Point(0, 0);
            grfXray.Rows.Count = 1;
            grfXray.Cols.Count = 5;
            grfXray.Cols[colgrfxrayreqno].Width = 100;
            grfXray.Cols[colgrfxrayyear].Width = 100;
            grfXray.Cols[colgrfxrayreqdate].Width = 60;
            grfXray.Cols[colgrfxraystatuspacs].Width = 60;

            grfXray.ShowCursor = true;
            grfXray.Cols[colgrfxrayreqno].Caption = "reqno";
            grfXray.Cols[colgrfxrayyear].Caption = "reqyr";
            grfXray.Cols[colgrfxrayreqdate].Caption = "reqdate";
            grfXray.Cols[colgrfxraystatuspacs].Caption = "status";

            grfXray.Cols[colgrfxrayreqno].AllowEditing = false;
            grfXray.Cols[colgrfxrayyear].AllowEditing = false;
            grfXray.Cols[colgrfxrayreqdate].AllowEditing = false;
            grfXray.Cols[colgrfxraystatuspacs].AllowEditing = false;

            grfXray.AllowFiltering = true;

            grfXray.Click += GrfXray_Click;

            pnXray.Controls.Add(grfXray);
            theme1.SetTheme(grfXray, bc.iniC.themeApp);
        }
        private void GrfXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfXray.Row <= 0) return;
            if (grfXray[grfXray.Row, colgrfxrayreqno] == null) return;
            txtXrayReqdate.Value = grfXray[grfXray.Row, colgrfxrayreqdate].ToString();
            txtXrayReqno.Value = grfXray[grfXray.Row, colgrfxrayreqno].ToString();
            txtXrayReqYear.Value = grfXray[grfXray.Row, colgrfxrayyear].ToString();
            setGrfXrayItems(txtXrayHn.Text.Trim(), txtXrayReqno.Text.Trim(), txtXrayReqdate.Text.Trim());
        }
        private void setGrfXray()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.xrayT01DB.selectVisitStatusPacsReqByHN(txtXrayDate.Text.Trim(), txtXrayHn.Text.Trim());
            grfXray.Rows.Count = 1;
            grfXray.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfXray.Rows[i];
                rowa[colgrfxrayreqno] = row1["MNC_REQ_NO"].ToString();
                rowa[colgrfxrayyear] = row1["MNC_REQ_YR"].ToString();
                rowa[colgrfxrayreqdate] = row1["mnc_req_dat1"].ToString();
                rowa[colgrfxraystatuspacs] = row1["status_pacs"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void initGrfDeptVisit()
        {
            grfDeptVisit = new C1FlexGrid();
            grfDeptVisit.Font = fEdit;
            grfDeptVisit.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDeptVisit.Location = new System.Drawing.Point(0, 0);
            grfDeptVisit.Rows.Count = 1;
            grfDeptVisit.Cols.Count = 6;
            grfDeptVisit.Cols[colgrfDeptVisitVsdate].Width = 100;
            grfDeptVisit.Cols[colgrfDeptVisitPreno].Width = 100;
            grfDeptVisit.Cols[colgrfDeptVisitStatusAdmit].Width = 60;
            grfDeptVisit.Cols[colgrfDeptVisitVn].Width = 60;

            grfDeptVisit.ShowCursor = true;
            grfDeptVisit.Cols[colgrfDeptVisitVsdate].Caption = "vsdate";
            grfDeptVisit.Cols[colgrfDeptVisitPreno].Caption = "preno";
            grfDeptVisit.Cols[colgrfDeptDept].Caption = "dept";
            grfDeptVisit.Cols[colgrfDeptVisitVn].Caption = "VNNO";

            grfDeptVisit.Cols[colgrfDeptVisitVsdate].AllowEditing = false;
            grfDeptVisit.Cols[colgrfDeptVisitPreno].AllowEditing = false;
            grfDeptVisit.Cols[colgrfDeptVisitStatusAdmit].AllowEditing = false;
            grfDeptVisit.Cols[colgrfDeptDept].AllowEditing = false;
            grfDeptVisit.Cols[colgrfDeptVisitVn].AllowEditing = false;

            grfDeptVisit.AllowFiltering = true;

            grfDeptVisit.Click += GrfDeptVisit_Click;

            pnDept.Controls.Add(grfDeptVisit);
            theme1.SetTheme(grfDeptVisit, bc.iniC.themeApp);
        }
        private void GrfDeptVisit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDeptVisit[grfDeptVisit.Row, colgrfDeptVisitVsdate] == null) return;

            String preno = grfDeptVisit[grfDeptVisit.Row, colgrfDeptVisitPreno].ToString();
            String vsdate = grfDeptVisit[grfDeptVisit.Row, colgrfDeptVisitVsdate].ToString();
            txtDeptPreno.Value = preno;
            txtDeptVsdate.Value = vsdate;
            if(grfDeptVisit[grfDeptVisit.Row, colgrfDeptVisitStatusAdmit].ToString().Equals("O"))
            {
                chkDeptOPD.Checked = true;
                chkDeptIPD.Checked = false;
                chkDeptOPDNew.Checked = true;
                chkDeptIPDNew.Checked = false;
                //cboDeptNew
                bc.bcDB.pttDB.setCboDeptOPD(cboDeptNew, bc.iniC.station);
            }
            else
            {
                chkDeptOPD.Checked = false;
                chkDeptIPD.Checked = true;
                chkDeptOPDNew.Checked = false;
                chkDeptIPDNew.Checked = true;
                bc.bcDB.pttDB.setCboDeptIPDWdNo(cboDeptNew, bc.iniC.station);
            }
            txtDeptDept.Value = grfDeptVisit[grfDeptVisit.Row, colgrfDeptDept].ToString();
        }
        private void setGrfDeptVisit()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.vsDB.selectVisitByHn(txtDeptHN.Text.Trim());
            grfDeptVisit.Rows.Count = 1;
            grfDeptVisit.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfDeptVisit.Rows[i];
                rowa[colgrfDeptVisitVsdate] = row1["MNC_DATE"].ToString();
                rowa[colgrfDeptVisitPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfDeptVisitStatusAdmit] = row1["MNC_AN_NO"].ToString().Equals("0") ? "O" : "I";
                rowa[colgrfDeptDept] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["MNC_SEC_NO"].ToString(): row1["MNC_SEC_NO_ipd"].ToString();
                rowa[colgrfDeptVisitVn] = row1["mnc_vn_no"].ToString()+"."+ row1["MNC_VN_SEQ"].ToString()+"."+ row1["MNC_VN_SUM"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void BtnAnMerge_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            rb2.Text = "";
            String re = bc.bcDB.MergeAN(txtAnANNO2.Text.Trim(), txtAnANNO1.Text.Trim(), txtRemark.Text.Trim());
            rb2.Text = re;
        }
        private void BtnAn2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtAnANNONew.Value = txtAnANNO2.Text.Trim();
        }
        private void BtnAn1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtAnANNONew.Value = txtAnANNO1.Text.Trim();
        }
        private void TxtAnANNO2_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //FLAGFOCUS = "";
        }
        private void TxtAnANNO2_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FLAGFOCUS = "txtAnANNO2";
        }

        private void TxtAnANNO1_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //FLAGFOCUS = "";
        }

        private void TxtAnANNO1_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FLAGFOCUS = "txtAnANNO1";
        }

        private void TxtAnHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByHn(txtAnHN.Text.Trim());
                lbAnPttnameT.Text = ptt.Name;
                setGrfPttAn();
            }
        }

        private void initGrfAn()
        {
            grfAn = new C1FlexGrid();
            grfAn.Font = fEdit;
            grfAn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfAn.Location = new System.Drawing.Point(0, 0);
            grfAn.Rows.Count = 1;
            grfAn.Cols.Count = 3;
            grfAn.Cols[colgrfAnanno].Width = 100;
            grfAn.Cols[colgrfAnAdDate].Width = 100;
            
            grfAn.ShowCursor = true;
            grfAn.Cols[colgrfAnanno].Caption = "ANNO";
            grfAn.Cols[colgrfAnAdDate].Caption = "ad date";

            grfAn.Cols[colgrfAnanno].AllowEditing = false;
            grfAn.Cols[colgrfAnAdDate].AllowEditing = false;
            
            grfAn.AllowFiltering = true;

            grfAn.Click += GrfAn_Click;

            pnAn.Controls.Add(grfAn);
            theme1.SetTheme(grfAn, bc.iniC.themeApp);
        }

        private void GrfAn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfAn[grfAn.Row, colgrfAnanno] == null) return;
            if (FLAGFOCUS.Equals("txtAnANNO1"))
            {
                txtAnANNO1.Value = grfAn[grfAn.Row, colgrfAnanno].ToString();
            }
            else if (FLAGFOCUS.Equals("txtAnANNO2"))
            {
                txtAnANNO2.Value = grfAn[grfAn.Row, colgrfAnanno].ToString();
            }
        }
        private void setGrfPttAn()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.pt08DB.SelectByHN(txtAnHN.Text.Trim());
            grfAn.Rows.Count = 1;
            grfAn.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfAn.Rows[i];
                rowa[colgrfAnanno] = row1["MNC_AN_NO"].ToString()+"."+ row1["MNC_AN_YR"].ToString();
                DateTime.TryParse(row1["MNC_AD_DATE"].ToString(), out DateTime dt);
                rowa[colgrfAnAdDate] = dt.ToString("yyyy-MM-dd");

                rowa[0] = i.ToString();
                i++;
            }
        }
        private void C1Button1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            long chk = 0;
            String sql = "";
            DataTable dtselect = new DataTable();
            DataTable dtpt08 = new DataTable();
            sql = "Select * From doc_scan Where status_record = '1' and status_ipd = 'I' ";
            dtselect = bc.conn.selectData(bc.conn.conn, sql);
            StringBuilder sbandate = new StringBuilder();
            StringBuilder sbhn = new StringBuilder();
            StringBuilder sban = new StringBuilder();
            StringBuilder sbanno = new StringBuilder();
            StringBuilder sbancnt = new StringBuilder();
            StringBuilder sbandatechk = new StringBuilder();
            StringBuilder sbdsnid = new StringBuilder();
            rb1.Text = "Count = " + dtselect.Rows.Count.ToString();
            foreach (DataRow dr in dtselect.Rows)
            {
                sbandate.Clear();
                sbhn.Clear();
                sban.Clear();
                sbancnt.Clear();
                sbanno.Clear();
                sbdsnid.Clear();
                sbandate.Append(dr["an_date"].ToString());
                sbhn.Append(dr["hn"].ToString());
                sban.Append(dr["an"].ToString());
                sbdsnid.Append(dr["doc_scan_id"].ToString());
                String[] an1 = sban.ToString().Split('/');
                if (an1.Length > 1)
                {
                    sbanno.Append(an1[0]);
                    sbancnt.Append(an1[1]);
                    //sbandate.Append(dr["an_date"].ToString());
                    sql = "Select convert(varchar(20),MNC_AD_DATE,23) as MNC_AD_DATE from PATIENT_T08 " +
                        "Where MNC_HN_NO = '" + sbhn.ToString() + "' and MNC_AN_NO = '"+ sbanno.ToString()+ "' and MNC_AN_YR = '"+ sbancnt.ToString()+"' ";
                    dtpt08 = bc.conn.selectData(bc.conn.connMainHIS, sql);
                    if (dtpt08.Rows.Count > 0)
                    {
                        sbandatechk.Clear();
                        sbandatechk.Append(dtpt08.Rows[0]["MNC_AD_DATE"].ToString());
                        if (!sbandatechk.ToString().Equals(sbandate.ToString()))
                        {
                            sql = "Update doc_scan set an_date = '"+ sbandatechk.ToString()+"' Where doc_scan_id = '"+sbdsnid.ToString()+"' ";
                            chk++;
                            bc.conn.ExecuteNonQuery(bc.conn.conn,sql);
                        }
                    }
                }
            }
            rb2.Text = chk.ToString();
        }

        private void BtnRevest_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.fint01DB.revestJobNo(txtPttHn.Text.Trim(), txtDocNo.Text.Trim(), txtJobNoOld.Text.Trim());
            if (int.TryParse(re, out int chk))
            {
                MessageBox.Show("OK ทำคืนเข้าเวรเก่า เรียบร้อย ", "");
            }
        }
        private void BtnGet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.fint01DB.setJobNo(txtPttHn.Text.Trim(), txtDocNo.Text.Trim(), txtJobNoCur.Text.Trim());
            if(int.TryParse(re, out int chk))
            {
                MessageBox.Show("OK ดึงกลับมาเวรปัจจุบัน เรียบร้อย ", "");
            }
        }
        private void initGrflabReq()
        {
            grfLabReq = new C1FlexGrid();
            grfLabReq.Font = fEdit;
            grfLabReq.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLabReq.Location = new System.Drawing.Point(0, 0);
            grfLabReq.Rows.Count = 1;
            grfLabReq.Cols.Count = 12;
            grfLabReq.Cols[colgrfLabReqHn].Width = 100;
            grfLabReq.Cols[colgrfLabReqReqno].Width = 100;
            grfLabReq.Cols[colgrfLabReqReqdate].Width = 110;
            grfLabReq.Cols[colgrfLabReqlbcode].Width = 100;
            grfLabReq.Cols[colgrfHnDocSts].Width = 60;
            grfLabReq.Cols[colgrfHnAmt].Width = 80;
            grfLabReq.Cols[colgrfHnJobNo].Width = 60;
            grfLabReq.Cols[colgrfHnJobNoOld].Width = 60;
            grfLabReq.ShowCursor = true;
            grfLabReq.Cols[colgrfLabReqHn].Caption = "hn";
            grfLabReq.Cols[colgrfLabReqReqno].Caption = "req NO";
            grfLabReq.Cols[colgrfLabReqReqdate].Caption = "req date";
            grfLabReq.Cols[colgrfLabReqlbcode].Caption = "labcode";
            grfLabReq.Cols[colgrfLabReqlabname].Caption = "labname";
            grfLabReq.Cols[colgrfLabReqlabname].AllowEditing = false;
            grfLabReq.Cols[colgrfLabReqHn].AllowEditing = false;
            grfLabReq.Cols[colgrfLabReqReqno].AllowEditing = false;
            grfLabReq.Cols[colgrfLabReqReqdate].AllowEditing = false;
            grfLabReq.Cols[colgrfLabReqlbcode].AllowEditing = false;
            grfLabReq.Cols[colgrfLabReqyear].Visible = false;

            grfLabReq.AllowFiltering = true;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("resend request", new EventHandler(ContextMenu_Resend_request));
            grfLabReq.ContextMenu = menuGw;
            grfLabReq.Click += GrfLabReq_Click;

            pnLabReqView.Controls.Add(grfLabReq);
            theme1.SetTheme(grfLabReq, bc.iniC.themeApp);
        }
        private void ContextMenu_Resend_request(object sender, System.EventArgs e)
        {
            if (grfLabReq.Rows == null) return;
            if (grfLabReq.Row <= 0) return;
            if (grfLabReq[grfLabReq.Row, colgrfLabReqReqno] == null) return;
            String reqno = grfLabReq[grfLabReq.Row, colgrfLabReqReqno].ToString();
            String reqdate = grfLabReq[grfLabReq.Row, colgrfLabReqReqdate].ToString();
            String reqyear = grfLabReq[grfLabReq.Row, colgrfLabReqyear].ToString();
            String re = bc.bcDB.labT01DB.updateStatusRequestLabUnsend(reqyear, reqno, reqdate);
            String re1 = bc.bcDB.labT02DB.updateStatusRequestLabUnsend(reqyear, reqno, reqdate);
            if(int.TryParse(re, out int chk) && int.TryParse(re1, out int chk1))
            {
                lb1.Text = "Updatere send OK";
            }
        }
        private void GrfLabReq_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfLabReq.Rows == null) return;
        }
        private void setGrfLabReq()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.labT05DB.selectRequestByHn(txtLabReqHn.Text.Trim());
            grfLabReq.Rows.Count = 1;
            grfLabReq.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfLabReq.Rows[i];
                rowa[colgrfLabReqHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfLabReqReqno] = row1["MNC_REQ_NO"].ToString();
                rowa[colgrfLabReqyear] = row1["MNC_REQ_YR"].ToString();
                rowa[colgrfLabReqReqdate] = row1["MNC_REQ_DAT"].ToString();
                rowa[colgrfLabReqstatussend] = row1["status_request_lab"].ToString();
                rowa[colgrfLabReqdatesend] = row1["date_request_lab"].ToString();
                rowa[colgrfLabReqlbcode] = row1["MNC_LB_CD"].ToString();
                rowa[colgrfLabReqlabname] = row1["MNC_LB_DSC"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void initGrfHn()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);
            grfHn.Rows.Count = 1;
            grfHn.Cols.Count = 9;
            grfHn.Cols[colgrfHnHN].Width = 100;
            grfHn.Cols[colgrfHnDocNo].Width = 100;
            grfHn.Cols[colgrfHnDocDate].Width = 110;
            grfHn.Cols[colgrfHnDocCD].Width = 100;
            grfHn.Cols[colgrfHnDocSts].Width = 60;
            grfHn.Cols[colgrfHnAmt].Width = 80;
            grfHn.Cols[colgrfHnJobNo].Width = 60;
            grfHn.Cols[colgrfHnJobNoOld].Width = 60;
            grfHn.ShowCursor = true;
            grfHn.Cols[colgrfHnHN].Caption = "hn";
            grfHn.Cols[colgrfHnDocNo].Caption = "DOC NO";
            grfHn.Cols[colgrfHnDocDate].Caption = "Doc date";
            grfHn.Cols[colgrfHnDocCD].Caption = "DOC CD";
            grfHn.Cols[colgrfHnDocSts].Caption = "STS";
            grfHn.Cols[colgrfHnAmt].Caption = "AMT";
            grfHn.Cols[colgrfHnJobNo].Caption = "jbono";
            grfHn.Cols[colgrfHnJobNoOld].Caption = "jbonoold";

            grfHn.Cols[colgrfHnHN].AllowEditing = false;
            grfHn.Cols[colgrfHnDocNo].AllowEditing = false;
            grfHn.Cols[colgrfHnDocDate].AllowEditing = false;
            grfHn.Cols[colgrfHnDocCD].AllowEditing = false;
            grfHn.Cols[colgrfHnDocSts].AllowEditing = false;
            grfHn.Cols[colgrfHnAmt].AllowEditing = false;
            grfHn.Cols[colgrfHnJobNo].AllowEditing = false;
            grfHn.Cols[colgrfHnJobNoOld].AllowEditing = false;
            grfHn.AllowFiltering = true;

            grfHn.Click += GrfHn_Click;

            panel1.Controls.Add(grfHn);
            theme1.SetTheme(grfHn, bc.iniC.themeApp);
        }

        private void GrfHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHn.Rows == null) return;
            if(grfHn.Cols == null) return;
            if(grfHn.Row <= 0) return;
            if(grfHn.Col <= 0) return;

            txtDocNo.Value = grfHn[grfHn.Row, colgrfHnDocNo].ToString();
            txtJobNo.Value = grfHn[grfHn.Row, colgrfHnJobNo].ToString();
            txtJobNoOld.Value = grfHn[grfHn.Row, colgrfHnJobNoOld].ToString();
        }
        private void setGrfPttHn()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.fint01DB.SelectAllByHN(txtPttHn.Text.Trim());
            grfHn.Rows.Count = 1;
            grfHn.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfHn.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfHn.Rows[i];
                rowa[colgrfHnHN] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfHnDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfHnDocDate] = bc.datetoShow(row1["MNC_DOC_DAT"].ToString());
                rowa[colgrfHnDocCD] = row1["mnc_DOC_CD"].ToString();
                rowa[colgrfHnDocSts] = row1["mnc_DOC_STS"].ToString();
                rowa[colgrfHnAmt] = row1["mnc_SUM_PRI"].ToString();
                rowa[colgrfHnJobNo] = row1["MNC_JOB_NO"].ToString();
                rowa[colgrfHnJobNoOld] = row1["MNC_JOB_NOold"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
        }
        private void TxtPttHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinetByHn(txtPttHn.Text.Trim());
                lbVsPttNameT.Text = ptt.Name;
                txtJobNoCur.Value = bc.bcDB.fint01DB.SelectJOBNOCurrent();
                setGrfPttHn();
            }
        }

        private void FrmEditJobNo_Load(object sender, EventArgs e)
        {
            lb1.Text = "[hostDB " + bc.iniC.hostDB + " nameDB " + bc.iniC.nameDB + "] [hostDBMainHIS " + bc.iniC.hostDBMainHIS + " nameDBMainHIS " + bc.iniC.nameDBMainHIS+"]";
            txtVoidvsdate.Value = DateTime.Now.Year.ToString()+"-"+DateTime.Now.ToString("MM-dd");
            txtXrayDate.Value = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
        }
    }
}
