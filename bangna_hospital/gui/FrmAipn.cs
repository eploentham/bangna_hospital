using bangna_hospital.control;
using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmAipn : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEditBig, ffB;
        Color bg, fc, color;
        Label lbLoading;
        Boolean pageLoad = false;
        public FrmAipn(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 7, FontStyle.Regular);
            lbLoading = new Label();
            lbLoading.Font = new Font("AngsanaUPC", 24, FontStyle.Bold);
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            lbLoading.Location = new Point(btnSendEmail.Width + 45, 250);
            this.Controls.Add(lbLoading);

            btnGenXML.Click += btnGenXML_Click;
            btnOpenXML.Click += BtnOpenXML_Click;
            btnSendEmail.Click += BtnSendEmail_Click;
            txtAn.KeyUp += TxtAn_KeyUp;
            pageLoad = false;
        }

        private void TxtAn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable();
                dt = bc.bcDB.aipnDB.selectAipnByAnno(txtAn.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    lbPttName.Text = dt.Rows[0]["patient_fullname"].ToString();
                }
                else
                {
                    lbPttName.Text = "ไม่พบ patient";
                }
            }
        }

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setLbLoading("เตรียม Email");
            showLbLoading();
            btnSendEmail.Enabled = false;
            SmtpClient SmtpServer;

            rb1.Text = "เตรียม Email";
            //FrmWaiting frmW = new FrmWaiting();
            //frmW.Show();
            String filename = "", datetick = "";
            String pathFile = "";
            pathFile = bc.iniC.aipnXmlPath + "\\" + txtSessionNO.Text;
            if (!Directory.Exists("report"))
            {
                MessageBox.Show("Folder do not exit", "");
                return;
            }
            datetick = DateTime.Now.Ticks.ToString();
            //String pathFolder = Application.StartupPath + "\\billtext\\";
            String pathFolder = bc.iniC.aipnXmlPath+"\\"+txtSessionNO.Text.Trim();
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            string[] files = Directory.GetFiles(pathFolder, "*.zip", SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                filename = files[0];
            }

            rb1.Text = "เริ่มส่ง Email";
            new LogWriter("d", "FrmAipn BtnSendEmail_Click เริ่มส่ง Email pathFile "+ pathFile);
            MailMessage mail = new MailMessage();

            //txtEmailSubject.Value = "Routine LAB Result HN " + txtHn.Text.ToUpper() + " Name " + txtPttNameE.Text + " [VN " + txtVnShow.Text + "] Hormone Report Date " + System.DateTime.Now.ToString("dd/MM/") + System.DateTime.Now.Year;

            //mail.From = new MailAddress(txtEmailTo.Text); ic.iniC.email_form_lab_opu
            mail.From = new MailAddress(txtFrom.Text);
            mail.To.Add(txtTO.Text);
            mail.Subject = txtSubject.Text;
            mail.Body = "";

            mail.IsBodyHtml = true;
            if (File.Exists(filename))
            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(filename);
                mail.Attachments.Add(attachment);
            }
            else
            {
                rb1.Text = "ไม่พบ Attach File";
                return;
            }
            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            //mail.AlternateViews.Add(htmlView);
            new LogWriter("d", "FrmAipn BtnSendEmail_Click SmtpServer " + bc.iniC.EmailAuthUserAIPN+ " bc.iniC.EmailAuthPassAIPN " + bc.iniC.EmailAuthPassAIPN);
            SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential(bc.iniC.EmailAuthUserAIPN, bc.iniC.EmailAuthPassAIPN);
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            new LogWriter("d", "FrmAipn BtnSendEmail_Click SmtpServer.Send(mail) ");
            SmtpServer.Send(mail);
            rb1.Text = "ส่ง Email เรียบร้อย";
            btnSendEmail.Enabled = true;
            hideLbLoading();
        }
        private void BtnOpenXML_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void btnGenXML_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setLbLoading("เตรียม บิล");
            showLbLoading();
            String sessionno = bc.genAipnFile(txtAuthorName.Text.Trim(), txtAn.Text.Trim(), cboSubMType.Text, ChkAn.Checked, chkNoAdd.Checked, chkAnNew.Checked);
            txtSessionNO.Text = sessionno;
            hideLbLoading();
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
        }
        private void showLbLoading()
        {
            lbLoading.Show();
            lbLoading.BringToFront();
            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();
            Application.DoEvents();
        }
        private void FrmAipn_Load(object sender, EventArgs e)
        {
            this.Text = "last Update 2025-03-11 แก้ gen prefix AN OBS, message AN new เลข anเป็น9ตัวเดิม10 txtsession readonly AN แบบ บาง1 update session on";
            txtFrom.Text = bc.iniC.EmailFromAIPN;
            txtTO.Text = bc.iniC.EmailToAIPN;
            txtSubject.Text = bc.iniC.EmailSubjectAIPN;
            lb1.Text = bc.iniC.hostDBMainHIS;
            txtAuthorName.Text = bc.iniC.aipnAuthorName;
            cboSubMType.SelectedIndex = 0;
            //lbLoading.Location = new Point(this.Location.X+50, 200);
            //showLbLoading();
        }
    }
}
