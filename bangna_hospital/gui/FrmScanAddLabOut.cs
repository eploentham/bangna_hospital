using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScanAddLabOut : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        String filename1 = "", filename2 = "", filename3 = "", filename4 = "", filename5 = "", filename6 = "";

        LabEx le;
        public FrmScanAddLabOut(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            le = new LabEx();
            txtYearId.Value = DateTime.Now.Year.ToString();

            btnHn.Click += BtnHn_Click;
            
            btnOpen1.Click += BtnOpen1_Click;
            btnOpen2.Click += BtnOpen2_Click;
            btnOpen3.Click += BtnOpen3_Click;
            btnOpen4.Click += BtnOpen4_Click;
            btnOpen5.Click += BtnOpen5_Click;
            btnOpen6.Click += BtnOpen6_Click;
            btnSave1.Click += BtnSave1_Click;
            btnSave2.Click += BtnSave2_Click;
            btnSave3.Click += BtnSave3_Click;
            btnSave4.Click += BtnSave4_Click;
            btnSave5.Click += BtnSave5_Click;
            btnSave6.Click += BtnSave6_Click;
            lbName1.DoubleClick += LbName1_DoubleClick;
            lbName2.DoubleClick += LbName2_DoubleClick;
            lbName3.DoubleClick += LbName3_DoubleClick;
            lbName4.DoubleClick += LbName4_DoubleClick;
            lbName5.DoubleClick += LbName5_DoubleClick;
            lbName6.DoubleClick += LbName6_DoubleClick;
        }
        
        private void LbName1_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            openPDF("1");
        }
        private void LbName2_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            openPDF("2");
        }
        private void LbName3_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            openPDF("3");
        }
        private void LbName4_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            openPDF("4");
        }
        private void LbName5_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            openPDF("5");
        }
        private void LbName6_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            openPDF("6");
        }
        private void openPDF(String num)
        {
            String no = "", filename1 = "", st = "", datetick = "";
            datetick = DateTime.Now.Ticks.ToString();
            filename1 = System.IO.Directory.GetCurrentDirectory() + "\\report\\" + txtId.Text.Trim() + "_" + datetick + ".pdf";
            MemoryStream stream;
            stream = loadPDF(num);
            if(stream.Length > 0)
            {
                if (!Directory.Exists("report"))
                {
                    Directory.CreateDirectory("report");
                }
                if (File.Exists(filename1))
                {
                    File.Delete(filename1);
                    System.Threading.Thread.Sleep(200);
                }
                stream.Position = 0;
                using (var fileStream = new FileStream(filename1, FileMode.Create, FileAccess.Write))
                {
                    stream.WriteTo(fileStream);
                }
                //FileStream writeStream = new FileStream(filename1, FileMode.Create, FileAccess.Write);
                //stream.Position = 0;
                //int Length = 256;
                //Byte[] buffer = new Byte[Length];
                //int bytesRead = stream.Read(buffer, 0, Length);
                //// write the required bytes
                //while (bytesRead > 0)
                //{
                //    writeStream.Write(buffer, 0, bytesRead);
                //    bytesRead = stream.Read(buffer, 0, Length);
                //}
                //stream.Close();
                //writeStream.Close();
                System.Threading.Thread.Sleep(500);
                if (File.Exists(filename1))
                {
                    System.Diagnostics.Process.Start(filename1);
                }
            }
            
        }
        private void setLabEx()
        {
            le.Id = txtId.Text;
            le.Active = "1";
            le.Description = "";
            le.Hn = txtHn.Text;
            le.LabDate = "";
            le.LabExDate = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
            le.PatientName = txtName.Text;
            le.Remark = "";
            le.Vn = txtVN.Text.Replace("/",".").Replace("(", ".").Replace(")", "");
            le.VisitDate = txtVisitDate.Text;
            le.RowNumber = txtRowNumber.Text;
            le.YearId = txtYearId.Text;
            le.LabTime = "";
            le.DoctorId = "";
            le.ReqNo = "";
            le.DoctorName = "";

        }
        private MemoryStream loadPDF(String num)
        {
            MemoryStream stream=null;
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTPLabOut, bc.iniC.userFTPLabOut, bc.iniC.passFTPLabOut);
            stream = ftpc.download(bc.iniC.folderFTPLabOut + "//" + DateTime.Now.Year.ToString() + "//" + txtId.Text + "_" + num + ".pdf");
            if (stream.Length > 0)
            {
                if (bc.iniC.windows.ToLower().Equals("windowsxp"))
                {
                    if (num.Equals("1")) lbName1.Text = txtId.Text + "_" + num + ".pdf";
                    if (num.Equals("2")) lbName2.Text = txtId.Text + "_" + num + ".pdf";
                    if (num.Equals("3")) lbName3.Text = txtId.Text + "_" + num + ".pdf";
                    if (num.Equals("4")) lbName4.Text = txtId.Text + "_" + num + ".pdf";
                    if (num.Equals("5")) lbName5.Text = txtId.Text + "_" + num + ".pdf";
                    if (num.Equals("6")) lbName6.Text = txtId.Text + "_" + num + ".pdf";
                }
            }
            
            return stream;
        }
        private void savePDF(String filename, String num)
        {
            if (File.Exists(filename))
            {
                if (le.Id.Equals(""))
                {
                    setLabEx();
                    String re = bc.bcDB.labexDB.insertLabEx(le);
                    long chk = 0;
                    if (long.TryParse(re, out chk))
                    {
                        txtId.Value = re;
                        le.Id = re;
                    }
                }
                MemoryStream stream;
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTPLabOut, bc.iniC.userFTPLabOut, bc.iniC.passFTPLabOut);
                ftpc.createDirectory(bc.iniC.folderFTPLabOut + "//" + DateTime.Now.Year.ToString());
                ftpc.delete(bc.iniC.folderFTPLabOut + "//" + DateTime.Now.Year.ToString() + "//" + txtId.Text + "_" + num + ".pdf");
                ftpc.upload(bc.iniC.folderFTPLabOut + "//" + DateTime.Now.Year.ToString() + "//" + txtId.Text + "_" + num + ".pdf", filename);
                //ftpc.upload(bc.iniC.folderFTPLabOut + "//" + DateTime.Now.Year.ToString() + "//" + txtId.Text + "_" + num + ".pdf", filename, "172.25.10.2", 3128);
                MessageBox.Show("นำเข้าข้อมูลเรียบร้อย", "นำเข้าข้อมูล");
            }
            else
            {
                MessageBox.Show("ไม่พบ File นำเข้าผลOut Lab ", "นำเข้าข้อมูล");
            }
        }
        private void BtnSave1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            savePDF(filename1, "1");
        }
        private void BtnSave2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            savePDF(filename2, "2");
        }
        private void BtnSave3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            savePDF(filename3, "3");
        }
        private void BtnSave4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            savePDF(filename4, "4");
        }
        private void BtnSave5_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            savePDF(filename5, "5");
        }
        private void BtnSave6_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            savePDF(filename6, "6");
        }

        private void BtnOpen1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF File (Pdf Files|*.pdf|All files (*.*)|*.*";
            ofd.Multiselect = true;
            ofd.Title = "My Image Browser";
            if (bc.iniC.labOutOpenFileDialog.Length > 0)
            {
                ofd.InitialDirectory = bc.iniC.labOutOpenFileDialog;
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                String[] file1 = ofd.FileNames;
                filename1 = file1[0];
                lbName1.Text = filename1;
                if (!bc.iniC.windows.ToLower().Equals("windowsxp"))
                {
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromFile(filename1);
                    fvLabOut1.DocumentSource = pds;
                }
                tC.SelectedTab = tabLabOut1;
            }
        }
        private void BtnOpen2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF File (Pdf Files|*.pdf|All files (*.*)|*.*";
            ofd.Multiselect = true;
            ofd.Title = "My Image Browser";
            if (bc.iniC.labOutOpenFileDialog.Length > 0)
            {
                ofd.InitialDirectory = bc.iniC.labOutOpenFileDialog;
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                String[] file1 = ofd.FileNames;
                filename2 = file1[0];
                lbName2.Text = filename2;
                if (!bc.iniC.windows.ToLower().Equals("windowsxp"))
                {
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromFile(filename2);
                    fvLabOut2.DocumentSource = pds;
                }
                tC.SelectedTab = tabLabOut2;
            }
        }
        private void BtnOpen3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF File (Pdf Files|*.pdf|All files (*.*)|*.*";
            ofd.Multiselect = true;
            ofd.Title = "My Image Browser";
            if (bc.iniC.labOutOpenFileDialog.Length > 0)
            {
                ofd.InitialDirectory = bc.iniC.labOutOpenFileDialog;
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                String[] file1 = ofd.FileNames;
                filename3 = file1[0];
                lbName3.Text = filename3;
                if (!bc.iniC.windows.ToLower().Equals("windowsxp"))
                {
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromFile(filename3);
                    fvLabOut3.DocumentSource = pds;
                }
                tC.SelectedTab = tabLabOut3;
            }
        }
        private void BtnOpen4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF File (Pdf Files|*.pdf|All files (*.*)|*.*";
            ofd.Multiselect = true;
            ofd.Title = "My Image Browser";
            if (bc.iniC.labOutOpenFileDialog.Length > 0)
            {
                ofd.InitialDirectory = bc.iniC.labOutOpenFileDialog;
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                String[] file1 = ofd.FileNames;
                filename4 = file1[0];
                lbName4.Text = filename4;
                if (!bc.iniC.windows.ToLower().Equals("windowsxp"))
                {
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromFile(filename4);
                    fvLabOut4.DocumentSource = pds;
                }
                tC.SelectedTab = tabLabOut4;
            }
        }
        private void BtnOpen5_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF File (Pdf Files|*.pdf|All files (*.*)|*.*";
            ofd.Multiselect = true;
            ofd.Title = "My Image Browser";
            if (bc.iniC.labOutOpenFileDialog.Length > 0)
            {
                ofd.InitialDirectory = bc.iniC.labOutOpenFileDialog;
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                String[] file1 = ofd.FileNames;
                filename5 = file1[0];
                lbName5.Text = filename5;
                if (!bc.iniC.windows.ToLower().Equals("windowsxp"))
                {
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromFile(filename5);
                    fvLabOut5.DocumentSource = pds;
                }
                tC.SelectedTab = tabLabOut5;
            }
        }
        private void BtnOpen6_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF File (Pdf Files|*.pdf|All files (*.*)|*.*";
            ofd.Multiselect = true;
            ofd.Title = "My Image Browser";
            if (bc.iniC.labOutOpenFileDialog.Length > 0)
            {
                ofd.InitialDirectory = bc.iniC.labOutOpenFileDialog;
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                String[] file1 = ofd.FileNames;
                filename6 = file1[0];
                lbName6.Text = filename6;
                if (!bc.iniC.windows.ToLower().Equals("windowsxp"))
                {
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromFile(filename6);
                    fvLabOut6.DocumentSource = pds;
                }
                tC.SelectedTab = tabLabOut6;
            }
        }
        private void BtnHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            String[] an = bc.sPtt.an.Split('/');
            //if (an.Length > 1)
            //{
            //    txtAN.Value = an[0];
            //    txtAnCnt.Value = an[1];
            //}
            //else
            //{
            txtAN.Value = bc.sPtt.an;
            txtAnCnt.Value = "";
            //}
            txtHn.Value = bc.sPtt.Hn;
            txtName.Value = bc.sPtt.Name;
            txtVN.Value = bc.sPtt.vn;
            txtVisitDate.Value = bc.sPtt.visitDate;
            txtPreNo.Value = bc.sPtt.preno;

            txtAnDate.Value = bc.sPtt.anDate;
            chkIPD.Checked = bc.sPtt.statusIPD.Equals("I") ? true : false;

            if (chkIPD.Checked)
            {
                txtVisitDate.Hide();
                txtAnDate.Show();
                label6.Text = "AN Date :";
            }
            else
            {
                txtVisitDate.Show();
                txtAnDate.Hide();
                label6.Text = "Visit Date :";
            }
            DataTable dt = new DataTable();
            dt = bc.bcDB.labexDB.selectByVn(txtHn.Text.Trim(), txtVN.Text.Trim().Replace("/",".").Replace("(",".").Replace(")",""));
            if (dt.Rows.Count>0)
            {
                le.Id = dt.Rows[0][bc.bcDB.labexDB.labex.Id].ToString();
                le.Hn = dt.Rows[0][bc.bcDB.labexDB.labex.Hn].ToString();
                le.Vn = dt.Rows[0][bc.bcDB.labexDB.labex.Vn].ToString();
                le.YearId = dt.Rows[0][bc.bcDB.labexDB.labex.YearId].ToString();
                txtId.Value = le.Id;
            }
            else
            {
                le.Id = "";
                le.Hn = txtHn.Text.Trim();
                le.Vn = txtVN.Text.Trim();
                le.YearId = DateTime.Now.Year.ToString();
            }
            if (!bc.iniC.windows.ToLower().Equals("windowsxp"))
            {
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                pds.LoadFromStream(loadPDF("1"));
                fvLabOut1.DocumentSource = pds;
                pds = new C1PdfDocumentSource();
                pds.LoadFromStream(loadPDF("2"));
                fvLabOut2.DocumentSource = pds;
                pds = new C1PdfDocumentSource();
                pds.LoadFromStream(loadPDF("3"));
                fvLabOut3.DocumentSource = pds;
                pds = new C1PdfDocumentSource();
                pds.LoadFromStream(loadPDF("4"));
                fvLabOut4.DocumentSource = pds;
                pds = new C1PdfDocumentSource();
                pds.LoadFromStream(loadPDF("5"));
                fvLabOut5.DocumentSource = pds;
                pds = new C1PdfDocumentSource();
                pds.LoadFromStream(loadPDF("6"));
                fvLabOut6.DocumentSource = pds;
            }
            else
            {
                lbName1.Text = "...";
                lbName2.Text = "...";
                lbName3.Text = "...";
                lbName4.Text = "...";
                lbName5.Text = "...";
                lbName6.Text = "...";
                loadPDF("1");
                loadPDF("2");
                loadPDF("3");
                loadPDF("4");
                loadPDF("5");
                loadPDF("6");
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                //appExit();
                //if (MessageBox.Show("ต้องการออกจากโปรแกรม1", "ออกจากโปรแกรม", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                //{
                //frmmain.Show();
                Close();
                //    return true;
                //}
            }
            //else
            //{
            //    switch (keyData)
            //    {
            //        case Keys.K | Keys.Control:
            //            if (flagShowTitle)
            //                flagShowTitle = false;
            //            else
            //                flagShowTitle = true;
            //            setTitle(flagShowTitle);
            //            return true;
            //        case Keys.X | Keys.Control:
            //            //frmmain.Show();
            //            Close();
            //            return true;
            //    }
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FrmScanAddLabOut_Load(object sender, EventArgs e)
        {
            tC.SelectedTab = tabLabOut1;
        }
    }
}
