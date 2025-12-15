using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Ribbon;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class UCDrugEdit : UserControl
    {
        BangnaControl BC;
        String DOCCODE = "", CFRNO = "", CFRYEAR = "", DRUGCODE = "", USERID = "", TXT = "", STATUSFORMUS = "";
        C1ThemeController theme1;
        Patient PTT;
        Visit VS;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        RibbonLabel lfSbMessage;
        FrmPharmacy frmPharmacy;
        DataTable DTDRUG;
        ListBox lstAutoComplete;
        List<string> originalDataUsing = new List<string>();
        List<string> originalDataFreq = new List<string>();
        List<string> originalDataPrecau = new List<string>();
        List<string> originalDataindica = new List<string>();
        List<string> originalDataproperties = new List<string>();

        Boolean isload = false, cursortxtusingfocus=false, cursortxtfreqfocus = false, cursortxtPrecaufocus = false, cursortxtIndicafocus = false, cursortxtproperfocus = false;
        public UCDrugEdit(BangnaControl bc, String drugcode, String cfryear, String cfrno, String doccode, String userid, ref RibbonLabel lfSbMessage)
        {
            isload = true;
            InitializeComponent();
            this.BC = bc;
            this.DRUGCODE = drugcode;
            this.CFRYEAR = cfryear;
            this.CFRNO = cfrno;
            this.DOCCODE = doccode;
            this.USERID = userid;
            this.lfSbMessage = lfSbMessage;
            initConfig();
            isload = false;
        }
        public UCDrugEdit(BangnaControl bc, ref RibbonLabel lfSbMessage, FrmPharmacy frmPharmacy)
        {
            isload = true;
            cursortxtusingfocus = true;
            InitializeComponent();
            this.BC = bc;
            this.lfSbMessage = lfSbMessage;
            this.frmPharmacy = frmPharmacy;
            initConfig();
            lstAutoComplete.Visible = false;
            cursortxtusingfocus = false;
            isload = false;
        }
        private void initConfig()
        {
            theme1 = new C1ThemeController();
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            
            BC.bcDB.pharM01DB.setAUTOUsingDesc();
            BC.bcDB.pharM01DB.setAUTOFrequency();
            BC.bcDB.pharM01DB.getAUTOPrecautionDesc();
            BC.bcDB.pharM01DB.setAUTOIndicationDesc();
            BC.bcDB.pharM01DB.setAUTOProperties();
            //txtUsing.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //txtUsing.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txtUsing.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOUSING1;
            lstAutoComplete = new ListBox();
            lstAutoComplete.Visible = false;
            lstAutoComplete.Width = txtUsing.Width;
            lstAutoComplete.Height = 150;
            this.Controls.Add(lstAutoComplete);
            lstAutoComplete.BringToFront();
            
            // โหลดข้อมูลจาก BC.bcDB.pharM01DB.AUTOUSING
            if(BC.bcDB.pharM01DB.AUTOUSING1!=null) foreach(string item in BC.bcDB.pharM01DB.AUTOUSING1)   { originalDataUsing.Add(item);            }
            if (BC.bcDB.pharM01DB.AUTOFRE1 != null) foreach (string item in BC.bcDB.pharM01DB.AUTOFRE1)     { originalDataFreq.Add(item); }
            if (BC.bcDB.pharM01DB.AUTOCAU1 != null) foreach (string item in BC.bcDB.pharM01DB.AUTOCAU1)     { originalDataPrecau.Add(item); }
            if (BC.bcDB.pharM01DB.AUTOINDICA1 != null) foreach (string item in BC.bcDB.pharM01DB.AUTOINDICA1)  { originalDataindica.Add(item); }
            if (BC.bcDB.pharM01DB.AUTOPROPER != null) foreach (string item in BC.bcDB.pharM01DB.AUTOPROPER)   { originalDataproperties.Add(item); }
            // lock fixed size      
            this.AutoSize = false;  
            this.MinimumSize = new Size(439, 221);
            this.MaximumSize = new Size(439, 221);
            // optional: prevent resizing by parent layout events
            this.Resize += (s, e) => { if (this.Width != 439 || this.Height != 221) this.Size = new Size(439, 221); };
            btnSave.Click += BtnSave_Click;
            btnPrint.Click += BtnPrint_Click;
            chkLangEng.Click += ChkLangEng_Click;
            chkLangThai.Click += ChkLangThai_Click;
            txtUsing.Enter += TxtUsing_Enter;
            txtFrequency.Enter += (s, e) => { cursortxtfreqfocus = true; };
            txtPrecautions.Enter += (s, e) => { cursortxtPrecaufocus = true; };
            txtIndication.Enter += (s, e) => { cursortxtIndicafocus = true; };
            txtProperties.Enter += (s, e) => { cursortxtproperfocus = true; };

            setEventTxtUsingChange();
            setEventTxtUsingClick();
            setEventTxtUsingKeyDown();
            setEventTxtUsingLeave();
            setEventTxtFreqLeave();
            setEventTxtFreqKeyDown();
            setEventTxtFreqClick();
            setEventTxtFreqChange();
            setEventTxtPrecauLeave();
            setEventTxtPrecauKeyDown();
            setEventTxtPrecauClick();
            setEventTxtPrecauChange();
            setEventTxtIndicaLeave();
            setEventTxtIndicaKeyDown();
            setEventTxtIndicaClick();
            setEventTxtIndicaChange();
            setEventTxtPropertiesLeave();
            setEventTxtPropertiesKeyDown();
            setEventTxtPropertiesClick();
            setEventTxtPropertiesChange();
            setControl();
        }
        private void TxtUsing_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            cursortxtusingfocus = true;
        }
        private void setEventTxtPropertiesLeave()
        {
            cursortxtIndicafocus = false;
            txtProperties.Leave += (s, e) =>
            {
                // ใช้ Timer เพื่อให้ Click ทำงานก่อน
                Timer timer = new Timer();
                timer.Interval = 200;
                timer.Tick += (ts, te) =>
                {
                    timer.Stop();
                    if (!lstAutoComplete.Focused)
                        lstAutoComplete.Visible = false;
                };
                timer.Start();
            };
        }
        private void setEventTxtPropertiesKeyDown()
        {
            txtProperties.KeyDown += (s, e) =>
            {
                if (lstAutoComplete.Visible)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        if (lstAutoComplete.SelectedIndex < lstAutoComplete.Items.Count - 1) lstAutoComplete.SelectedIndex++;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (lstAutoComplete.SelectedIndex > 0) lstAutoComplete.SelectedIndex--;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        if (lstAutoComplete.SelectedItem != null)
                        {
                            // ตัดเอาส่วนหลัง #
                            string selectedText = lstAutoComplete.SelectedItem.ToString();
                            if (selectedText.Contains("#"))
                            {
                                string afterHash = selectedText.Split('#')[1];
                                afterHash = afterHash.Contains("/") ? afterHash.Replace("/", "") : afterHash;
                                txtProperties.Text = afterHash.Trim();
                            }
                            else { txtProperties.Text = selectedText;  /* ถ้าไม่มี # ใช้ทั้งหมด*/                            }
                            //txtUsing.Text = lstAutoComplete.SelectedItem.ToString();
                            lstAutoComplete.Visible = false;
                            txtProperties.SelectionStart = txtProperties.Text.Length;
                        }
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        lstAutoComplete.Visible = false;
                        e.Handled = true;
                    }
                }
            };
        }
        private void setEventTxtPropertiesClick()
        {
            lstAutoComplete.Click += (s, e) =>
            {
                if (lstAutoComplete.SelectedItem != null)
                {
                    txtProperties.Text = lstAutoComplete.SelectedItem.ToString();
                    lstAutoComplete.Visible = false;
                    txtProperties.Focus();
                    txtProperties.SelectionStart = txtProperties.Text.Length;
                }
            };
        }
        private void setEventTxtPropertiesChange()
        {
            //if(isload) return;
            txtProperties.TextChanged += (s, e) =>
            {
                string searchText = txtProperties.Text;
                if (string.IsNullOrWhiteSpace(searchText)) { lstAutoComplete.Visible = false; return; }
                // ค้นหาทั้งหน้าและหลัง #
                var filtered = originalDataproperties.Where(item =>
                {
                    // ค้นหาจากทั้งสตริง
                    if (item.ToLower().Contains(searchText.ToLower())) return true;
                    // ค้นหาเฉพาะส่วนหลัง #
                    if (item.Contains("#"))
                    {
                        string afterHash = item.Split('#')[1];
                        if (afterHash.ToLower().Contains(searchText.ToLower())) return true;
                    }
                    return false;
                }).ToList();
                if (filtered.Count > 0)
                {
                    lstAutoComplete.Items.Clear();
                    lstAutoComplete.Items.AddRange(filtered.ToArray());
                    // กำหนดตำแหน่ง
                    Point location = txtProperties.PointToScreen(Point.Empty);
                    location = this.PointToClient(location);
                    lstAutoComplete.Location = new Point(location.X, location.Y + txtProperties.Height);
                    lstAutoComplete.Visible = cursortxtproperfocus ? true : false;
                }
                else { lstAutoComplete.Visible = false; }
            };
        }
        private void setEventTxtIndicaLeave()
        {
            cursortxtIndicafocus = false;
            txtIndication.Leave += (s, e) =>
            {
                // ใช้ Timer เพื่อให้ Click ทำงานก่อน
                Timer timer = new Timer();
                timer.Interval = 200;
                timer.Tick += (ts, te) =>
                {
                    timer.Stop();
                    if (!lstAutoComplete.Focused)
                        lstAutoComplete.Visible = false;
                };
                timer.Start();
            };
        }
        private void setEventTxtIndicaKeyDown()
        {
            txtIndication.KeyDown += (s, e) =>
            {
                if (lstAutoComplete.Visible)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        if (lstAutoComplete.SelectedIndex < lstAutoComplete.Items.Count - 1) lstAutoComplete.SelectedIndex++;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (lstAutoComplete.SelectedIndex > 0) lstAutoComplete.SelectedIndex--;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        if (lstAutoComplete.SelectedItem != null)
                        {
                            // ตัดเอาส่วนหลัง #
                            string selectedText = lstAutoComplete.SelectedItem.ToString();
                            if (selectedText.Contains("#"))
                            {
                                string afterHash = selectedText.Split('#')[1];
                                afterHash = afterHash.Contains("/") ? afterHash.Replace("/", "") : afterHash;
                                txtIndication.Text = afterHash.Trim();
                            }
                            else { txtIndication.Text = selectedText;  /* ถ้าไม่มี # ใช้ทั้งหมด*/                            }
                            //txtUsing.Text = lstAutoComplete.SelectedItem.ToString();
                            lstAutoComplete.Visible = false;
                            txtIndication.SelectionStart = txtIndication.Text.Length;
                        }
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        lstAutoComplete.Visible = false;
                        e.Handled = true;
                    }
                }
            };
        }
        private void setEventTxtIndicaClick()
        {
            lstAutoComplete.Click += (s, e) =>
            {
                if (lstAutoComplete.SelectedItem != null)
                {
                    txtIndication.Text = lstAutoComplete.SelectedItem.ToString();
                    lstAutoComplete.Visible = false;
                    txtIndication.Focus();
                    txtIndication.SelectionStart = txtIndication.Text.Length;
                }
            };
        }
        private void setEventTxtIndicaChange()
        {
            //if(isload) return;
            txtIndication.TextChanged += (s, e) =>
            {
                string searchText = txtIndication.Text;
                if (string.IsNullOrWhiteSpace(searchText)) { lstAutoComplete.Visible = false; return; }
                // ค้นหาทั้งหน้าและหลัง #
                var filtered = originalDataindica.Where(item =>
                {
                    // ค้นหาจากทั้งสตริง
                    if (item.ToLower().Contains(searchText.ToLower())) return true;
                    // ค้นหาเฉพาะส่วนหลัง #
                    if (item.Contains("#"))
                    {
                        string afterHash = item.Split('#')[1];
                        if (afterHash.ToLower().Contains(searchText.ToLower())) return true;
                    }
                    return false;
                }).ToList();
                if (filtered.Count > 0)
                {
                    lstAutoComplete.Items.Clear();
                    lstAutoComplete.Items.AddRange(filtered.ToArray());
                    // กำหนดตำแหน่ง
                    Point location = txtIndication.PointToScreen(Point.Empty);
                    location = this.PointToClient(location);
                    lstAutoComplete.Location = new Point(location.X, location.Y + txtIndication.Height);
                    lstAutoComplete.Visible = cursortxtIndicafocus ? true : false;
                }
                else { lstAutoComplete.Visible = false; }
            };
        }
        private void setEventTxtPrecauLeave()
        {
            cursortxtPrecaufocus = false;
            txtPrecautions.Leave += (s, e) =>
            {
                // ใช้ Timer เพื่อให้ Click ทำงานก่อน
                Timer timer = new Timer();
                timer.Interval = 200;
                timer.Tick += (ts, te) =>
                {
                    timer.Stop();
                    if (!lstAutoComplete.Focused)
                        lstAutoComplete.Visible = false;
                };
                timer.Start();
            };
        }
        private void setEventTxtPrecauKeyDown()
        {
            txtPrecautions.KeyDown += (s, e) =>
            {
                if (lstAutoComplete.Visible)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        if (lstAutoComplete.SelectedIndex < lstAutoComplete.Items.Count - 1) lstAutoComplete.SelectedIndex++;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (lstAutoComplete.SelectedIndex > 0) lstAutoComplete.SelectedIndex--;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        if (lstAutoComplete.SelectedItem != null)
                        {
                            // ตัดเอาส่วนหลัง #
                            string selectedText = lstAutoComplete.SelectedItem.ToString();
                            if (selectedText.Contains("#"))
                            {
                                string afterHash = selectedText.Split('#')[1];
                                afterHash = afterHash.Contains("/") ? afterHash.Replace("/", "") : afterHash;
                                txtPrecautions.Text = afterHash.Trim();
                            }
                            else { txtPrecautions.Text = selectedText;  /* ถ้าไม่มี # ใช้ทั้งหมด*/                            }
                            //txtUsing.Text = lstAutoComplete.SelectedItem.ToString();
                            lstAutoComplete.Visible = false;
                            txtPrecautions.SelectionStart = txtPrecautions.Text.Length;
                        }
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        lstAutoComplete.Visible = false;
                        e.Handled = true;
                    }
                }
            };
        }
        private void setEventTxtPrecauClick()
        {
            lstAutoComplete.Click += (s, e) =>
            {
                if (lstAutoComplete.SelectedItem != null)
                {
                    txtPrecautions.Text = lstAutoComplete.SelectedItem.ToString();
                    lstAutoComplete.Visible = false;
                    txtPrecautions.Focus();
                    txtPrecautions.SelectionStart = txtPrecautions.Text.Length;
                }
            };
        }
        private void setEventTxtPrecauChange()
        {
            //if(isload) return;
            txtPrecautions.TextChanged += (s, e) =>
            {
                string searchText = txtPrecautions.Text;
                if (string.IsNullOrWhiteSpace(searchText)) { lstAutoComplete.Visible = false; return; }
                // ค้นหาทั้งหน้าและหลัง #
                var filtered = originalDataPrecau.Where(item =>
                {
                    // ค้นหาจากทั้งสตริง
                    if (item.ToLower().Contains(searchText.ToLower())) return true;
                    // ค้นหาเฉพาะส่วนหลัง #
                    if (item.Contains("#"))
                    {
                        string afterHash = item.Split('#')[1];
                        if (afterHash.ToLower().Contains(searchText.ToLower())) return true;
                    }
                    return false;
                }).ToList();
                if (filtered.Count > 0)
                {
                    lstAutoComplete.Items.Clear();
                    lstAutoComplete.Items.AddRange(filtered.ToArray());
                    // กำหนดตำแหน่ง
                    Point location = txtPrecautions.PointToScreen(Point.Empty);
                    location = this.PointToClient(location);
                    lstAutoComplete.Location = new Point(location.X, location.Y + txtPrecautions.Height);
                    lstAutoComplete.Visible = cursortxtPrecaufocus ? true : false;
                }
                else { lstAutoComplete.Visible = false; }
            };
        }
        private void setEventTxtFreqLeave()
        {
            txtFrequency.Leave += (s, e) =>
            {
                cursortxtfreqfocus = false;
                // ใช้ Timer เพื่อให้ Click ทำงานก่อน
                Timer timer = new Timer();
                timer.Interval = 200;
                timer.Tick += (ts, te) =>
                {
                    timer.Stop();
                    if (!lstAutoComplete.Focused)
                        lstAutoComplete.Visible = false;
                };
                timer.Start();
            };
        }
        private void setEventTxtFreqKeyDown()
        {
            txtFrequency.KeyDown += (s, e) =>
            {
                if (lstAutoComplete.Visible)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        if (lstAutoComplete.SelectedIndex < lstAutoComplete.Items.Count - 1) lstAutoComplete.SelectedIndex++;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (lstAutoComplete.SelectedIndex > 0) lstAutoComplete.SelectedIndex--;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        if (lstAutoComplete.SelectedItem != null)
                        {
                            // ตัดเอาส่วนหลัง #
                            string selectedText = lstAutoComplete.SelectedItem.ToString();
                            if (selectedText.Contains("#"))
                            {
                                string afterHash = selectedText.Split('#')[1];
                                afterHash = afterHash.Contains("/") ? afterHash.Replace("/", "") : afterHash;
                                txtFrequency.Text = afterHash.Trim();
                            }
                            else { txtFrequency.Text = selectedText;  /* ถ้าไม่มี # ใช้ทั้งหมด*/                            }
                            //txtUsing.Text = lstAutoComplete.SelectedItem.ToString();
                            lstAutoComplete.Visible = false;
                            txtFrequency.SelectionStart = txtFrequency.Text.Length;
                        }
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        lstAutoComplete.Visible = false;
                        e.Handled = true;
                    }
                }
            };
        }
        private void setEventTxtFreqClick()
        {
            lstAutoComplete.Click += (s, e) =>
            {
                if (lstAutoComplete.SelectedItem != null)
                {
                    txtFrequency.Text = lstAutoComplete.SelectedItem.ToString();
                    lstAutoComplete.Visible = false;
                    txtFrequency.Focus();
                    txtFrequency.SelectionStart = txtFrequency.Text.Length;
                }
            };
        }
        private void setEventTxtFreqChange()
        {
            //if(isload) return;
            txtFrequency.TextChanged += (s, e) =>
            {
                string searchText = txtFrequency.Text;
                if (string.IsNullOrWhiteSpace(searchText)) { lstAutoComplete.Visible = false; return; }
                // ค้นหาทั้งหน้าและหลัง #
                var filtered = originalDataFreq.Where(item =>
                {
                    // ค้นหาจากทั้งสตริง
                    if (item.ToLower().Contains(searchText.ToLower())) return true;
                    // ค้นหาเฉพาะส่วนหลัง #
                    if (item.Contains("#"))
                    {
                        string afterHash = item.Split('#')[1];
                        if (afterHash.ToLower().Contains(searchText.ToLower())) return true;
                    }
                    return false;
                }).ToList();
                if (filtered.Count > 0)
                {
                    lstAutoComplete.Items.Clear();
                    lstAutoComplete.Items.AddRange(filtered.ToArray());
                    // กำหนดตำแหน่ง
                    Point location = txtFrequency.PointToScreen(Point.Empty);
                    location = this.PointToClient(location);
                    lstAutoComplete.Location = new Point(location.X, location.Y + txtFrequency.Height);
                    lstAutoComplete.Visible = cursortxtfreqfocus ? true : false;
                }
                else { lstAutoComplete.Visible = false; }
            };
        }
        private void setEventTxtUsingLeave()
        {
            txtUsing.Leave += (s, e) =>
            {
                // ใช้ Timer เพื่อให้ Click ทำงานก่อน
                cursortxtusingfocus = false;
                Timer timer = new Timer();
                timer.Interval = 200;
                timer.Tick += (ts, te) =>
                {
                    timer.Stop();
                    if (!lstAutoComplete.Focused)
                        lstAutoComplete.Visible = false;
                };
                timer.Start();
            };
        }
        private void setEventTxtUsingKeyDown()
        {
            txtUsing.KeyDown += (s, e) =>
            {
                if (lstAutoComplete.Visible)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        if (lstAutoComplete.SelectedIndex < lstAutoComplete.Items.Count - 1)                            lstAutoComplete.SelectedIndex++;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (lstAutoComplete.SelectedIndex > 0)                            lstAutoComplete.SelectedIndex--;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        if (lstAutoComplete.SelectedItem != null)
                        {
                            // ตัดเอาส่วนหลัง #
                            string selectedText = lstAutoComplete.SelectedItem.ToString();
                            if (selectedText.Contains("#")) {               string afterHash = selectedText.Split('#')[1];
                                afterHash = afterHash.Contains("/") ? afterHash.Replace("/", "") :afterHash;
                                txtUsing.Text = afterHash.Trim();}
                            else                            {               txtUsing.Text = selectedText;  /* ถ้าไม่มี # ใช้ทั้งหมด*/                            }
                            //txtUsing.Text = lstAutoComplete.SelectedItem.ToString();
                            lstAutoComplete.Visible = false;
                            txtUsing.SelectionStart = txtUsing.Text.Length;
                        }
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        lstAutoComplete.Visible = false;
                        e.Handled = true;
                    }
                }
            };
        }
        private void setEventTxtUsingClick()
        {
            lstAutoComplete.Click += (s, e) =>
            {
                if (lstAutoComplete.SelectedItem != null)
                {
                    txtUsing.Text = lstAutoComplete.SelectedItem.ToString();
                    lstAutoComplete.Visible = false;
                    txtUsing.Focus();
                    txtUsing.SelectionStart = txtUsing.Text.Length;
                }
            };
        }
        private void setEventTxtUsingChange()
        {
            //if(isload) return;
            txtUsing.TextChanged += (s, e) =>
            {
                string searchText = txtUsing.Text;
                if (string.IsNullOrWhiteSpace(searchText))          {               lstAutoComplete.Visible = false;            return;         }
                // ค้นหาทั้งหน้าและหลัง #
                var filtered = originalDataUsing.Where(item =>
                {
                    // ค้นหาจากทั้งสตริง
                    if (item.ToLower().Contains(searchText.ToLower()))                        return true;
                    // ค้นหาเฉพาะส่วนหลัง #
                    if (item.Contains("#"))
                    {
                        string afterHash = item.Split('#')[1];
                        if (afterHash.ToLower().Contains(searchText.ToLower()))                            return true;
                    }
                    return false;
                }).ToList();
                if (filtered.Count > 0)
                {
                    lstAutoComplete.Items.Clear();
                    lstAutoComplete.Items.AddRange(filtered.ToArray());
                    // กำหนดตำแหน่ง
                    Point location = txtUsing.PointToScreen(Point.Empty);
                    location = this.PointToClient(location);
                    lstAutoComplete.Location = new Point(location.X, location.Y + txtUsing.Height);
                    lstAutoComplete.Visible = cursortxtusingfocus ? true :false;
                }
                else                {                    lstAutoComplete.Visible = false;                }
            };
        }
        private void ChkLangThai_Click(object sender, EventArgs e)
        {
            setControl(DTDRUG,chkLangThai.Name);
        }
        private void ChkLangEng_Click(object sender, EventArgs e)
        {
            setControl(DTDRUG, chkLangEng.Name);
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtstcdrug = BC.bcDB.pharT06DB.selectByCFRNoDrug3(CFRYEAR, CFRNO, DOCCODE, DRUGCODE);
            frmPharmacy.setReportStricker(ref dtstcdrug, false, "thai", "stricker");     //ทำแบบนี้เพราะ ต้องการให้พิมพ์ต่อกัน  ถ้าพิมพ์ธรรมดา มันมีโอกาส สลับกัน
            System.Threading.Thread.Sleep(1000);                            //ทำแบบนี้เพราะ ต้องการให้พิมพ์ต่อกัน  ถ้าพิมพ์ธรรมดา มันมีโอกาส สลับกัน
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            FrmPasswordConfirm frm = new FrmPasswordConfirm(BC);
            frm.ShowDialog(this);
            frm.Dispose();
            if (BC.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String using1 = txtUsing.Text.Trim();
            String freq = txtFrequency.Text.Trim();
            String indica = txtIndication.Text.Trim();
            String precau = txtPrecautions.Text.Trim();
            String interac = txtInteraction.Text.Trim();
            String properties = txtProperties.Text.Trim();
            String result = BC.bcDB.pharT06DB.updateUsingFreqIndicau(using1, freq, indica, precau, interac, properties, DOCCODE, CFRYEAR, CFRNO, DRUGCODE, BC.USERCONFIRMID);
            if (result.Equals("1"))
            {
                lfSbMessage.Text = "บันทึกข้อมูลสำเร็จ";
                DataTable dt = new DataTable();
                dt = BC.bcDB.pharT06DB.selectByCFRNoDrug3(CFRYEAR, CFRNO, DOCCODE, "");
                frmPharmacy.setGrfDispDrugOPD(dt);
            }
            else
            {
                lfSbMessage.Text = "บันทึกข้อมูลไม่สำเร็จ";
            }
        }
        private void setControl()
        {
            setControl(CFRYEAR, CFRNO, DOCCODE, DRUGCODE);
        }
        public void setControl(String drugcode, String cfryear, String cfrno, String doccode)
        {
            DTDRUG = BC.bcDB.pharT06DB.selectByCFRNoDrug3(cfryear, cfrno, doccode, drugcode);
            if (DTDRUG.Rows.Count > 0)
            {
                setControl(DTDRUG,"");
            }
        }
        public void setControl(DataTable dtdreug, String flagclick)
        {
            clearControl();
            DTDRUG = dtdreug;
            //DTDRUG.AcceptChanges();
            setControl1(flagclick);
        }
        public void setControl1(String flagclick)
        {
            clearControl();
            if (DTDRUG.Rows.Count > 0)
            {
                //DTDRUG = dtdreug;
                if(flagclick.Length<=0)
                {
                    chkLangEng.Checked = DTDRUG.Rows[0]["nation"].ToString().Equals("01") ? false : true;
                    chkLangThai.Checked = DTDRUG.Rows[0]["nation"].ToString().Equals("01") ? true : false;
                }
                CFRYEAR = DTDRUG.Rows[0]["MNC_CFR_YR"].ToString();
                CFRNO = DTDRUG.Rows[0]["MNC_CFR_NO"].ToString();
                DOCCODE = DTDRUG.Rows[0]["MNC_DOC_CD"].ToString();
                DRUGCODE = DTDRUG.Rows[0]["MNC_PH_CD"].ToString();
                String using1 = DTDRUG.Rows[0]["using1"].ToString();           //จริงๆคือ Using วิธีใช้ pm04.mnc_ph_dir_dsc
                String freq1 = DTDRUG.Rows[0]["frequency"].ToString();         //ความถี่  pm21.MNC_PH_FRE_DSC
                String indica1 = DTDRUG.Rows[0]["indication"].ToString();      //Indication ข้อบ่งชี้  pm22.MNC_PH_TIM_DSC
                String interaction1 = DTDRUG.Rows[0]["interaction"].ToString();
                String precau1 = DTDRUG.Rows[0]["precautions"].ToString();     //Precautions จริงๆคือ คำเตือน pm11.MNC_PH_CAU_dsc
                String properties1 = DTDRUG.Rows[0]["properties"].ToString();  //สรรพคุณ
                if (chkLangThai.Checked)
                {
                    using1 = using1.Trim().Length > 0 ? using1 : DTDRUG.Rows[0]["drug_using"] != DBNull.Value ? DTDRUG.Rows[0]["drug_using"].ToString() : "";   //drug_using
                    indica1 = indica1.Trim().Length > 0 ? indica1 : DTDRUG.Rows[0]["drug_ind"] != DBNull.Value ? DTDRUG.Rows[0]["drug_ind"].ToString() : "";    //drug_ind
                    freq1 = freq1.Trim().Length > 0 ? freq1 : DTDRUG.Rows[0]["drug_freq"] != DBNull.Value ? DTDRUG.Rows[0]["drug_freq"].ToString() : "";        //drug_frequency
                    properties1 = properties1.Trim().Length > 0 ? properties1 : DTDRUG.Rows[0]["drug_properties"] != DBNull.Value ? DTDRUG.Rows[0]["drug_properties"].ToString() : "";   //drug_frequency
                    precau1 = precau1.Trim().Length > 0 ? precau1 : DTDRUG.Rows[0]["drug_cau"] != DBNull.Value ? DTDRUG.Rows[0]["drug_cau"].ToString() : "";   //drug_frequency
                }
                else
                {
                    using1 = using1.Trim().Length > 0 ? using1 : DTDRUG.Rows[0]["drug_using_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_using_e"].ToString() : "";   //drug_using
                    indica1 = indica1.Trim().Length > 0 ? indica1 : DTDRUG.Rows[0]["drug_ind_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_ind_e"].ToString() : "";    //drug_ind
                    freq1 = freq1.Trim().Length > 0 ? freq1 : DTDRUG.Rows[0]["drug_freq_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_freq_e"].ToString() : "";        //drug_frequency
                    properties1 = properties1.Trim().Length > 0 ? properties1 : DTDRUG.Rows[0]["drug_properties_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_properties_e"].ToString() : "";   //drug_frequency
                    precau1 = precau1.Trim().Length > 0 ? precau1 : DTDRUG.Rows[0]["drug_cau_e"] != DBNull.Value ? DTDRUG.Rows[0]["drug_cau_e"].ToString() : "";    //drug_frequency
                }

                using1 = (using1.Length > 0 && freq1.Length > 0) ? using1.Replace(freq1, "").Trim() : using1;
                if (using1.LastIndexOf("/") > 0) { using1 = using1.Substring(0, using1.LastIndexOf("/")); }
                freq1 = freq1.Length > 0 ? freq1.Replace("/", "").Trim() : "";
                indica1 = indica1.Length > 0 ? indica1.Replace("/", "").Trim() : "";
                precau1 = precau1.Length > 0 ? precau1.Replace("/", "").Trim() : "";
                properties1 = properties1.Length > 0 ? properties1.Replace("/", "").Trim() : "";
                txtItemCode.Value = DTDRUG.Rows[0]["MNC_PH_CD"].ToString();
                txtUsing.Value = using1;
                txtGeneric.Value = DTDRUG.Rows[0]["generic"].ToString();
                txtFrequency.Value = freq1;
                txtIndication.Value = indica1;
                txtPrecautions.Value = precau1;
                txtProperties.Value = properties1;
                txtInteraction.Value = interaction1;

            }
        }
        public void clearControl()
        {
            txtItemCode.Value = "";
            txtUsing.Value = "";
            txtGeneric.Value = "";
            txtFrequency.Value = "";
            txtIndication.Value = "";
            txtInteraction.Value = "";
            txtPrecautions.Value = "";
            txtProperties.Value = "";
        }
    }
}
