using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1SplitContainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmStaffNote : Form
    {
        BangnaControl bc;
        Patient PTT;
        String FLAGVIEW = "", HN="", PTTNAME="", VSDATE="", PRENO="";
        FileInfo FILEL, FILER;
        public FrmStaffNote(BangnaControl bc, String hn, String pttname, String vsdate, String preno, String flagview)
        {
            this.bc = bc;
            InitializeComponent();
            this.FLAGVIEW = flagview;
            this.HN = hn;
            this.PTTNAME = pttname;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            initConfig();
        }
        private void initConfig()
        {
            picL.Dock = DockStyle.Fill;
            picL.SizeMode = PictureBoxSizeMode.StretchImage;
            picR.Dock = DockStyle.Fill;
            picR.SizeMode = PictureBoxSizeMode.StretchImage;
            picL.Image = null;
            picR.Image = null;
            txtOperHN.Value = HN;
            lbOperPttNameT.Text = PTTNAME;
            btnSave.Click += BtnSave_Click;
            this.Disposed += FrmStaffNote_Disposed;
            btnGet.Click += BtnGet_Click;
            btnDel.Click += BtnDel_Click;
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String filenameS = "", err="";
            filenameS = "000000" + PRENO;
            filenameS = filenameS.Substring(filenameS.Length - 6);

            String filenameR = "", path = bc.iniC.pathScanStaffNote, year = "", mon = "", day = "";
            year = VSDATE.Substring(0, 4);
            mon = VSDATE.Substring(5, 2);
            day = VSDATE.Substring(8, 2);
            path += year + "\\" + mon + "\\" + day + "\\";

            filenameR = "000000" + PRENO;
            filenameR = filenameR.Substring(filenameR.Length - 6);
            err = "07";
            string filePathS = "\\\\" + path + filenameS + "S.JPG";
            string filePathR = "\\\\" + path + filenameR + "R.JPG";

            if (!IsFileLocked(filePathS) && !IsFileLocked(filePathR))
            {
                if (File.Exists(filePathS))
                {
                    File.Delete(filePathS);
                }
                if (File.Exists(filePathR))
                {
                    File.Delete(filePathR);
                }

                using (Image bitmapL = (Image)picL.Image.Clone())
                {
                    bitmapL.Save(filePathS, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                using (Image bitmapR = (Image)picR.Image.Clone())
                {
                    bitmapR.Save(filePathR, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                String re = bc.bcDB.vsDB.updateActNo111(HN, PRENO, VSDATE);
                lfSbMessage.Text = "save OK";
                this.Dispose();
            }
            else
            {
                // Handle the case where the file is locked
                MessageBox.Show("One or more files are currently in use by another process. Please try again later.");
            }
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            getImage();
        }

        private void FrmStaffNote_Disposed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            saveImgStaffNote();
        }

        private void saveImgStaffNote()
        {
            if (VSDATE.Length <= 0) return;
            if (PRENO.Length <= 0) return;
            if (HN.Length <= 0) return;
            String file = "", dd = "", mm = "", yy = "", err = "", preno1 = "";
            try
            {
                err = "00";

                String filenameS = "";
                filenameS = "000000" + PRENO;
                filenameS = filenameS.Substring(filenameS.Length - 6);

                String filenameR = "", path = bc.iniC.pathScanStaffNote, year = "", mon = "", day = "";
                year = VSDATE.Substring(0, 4);
                mon = VSDATE.Substring(5, 2);
                day = VSDATE.Substring(8, 2);
                path += year + "\\" + mon + "\\" + day + "\\";

                filenameR = "000000" + PRENO;
                filenameR = filenameR.Substring(filenameR.Length - 6);
                err = "07";
                //new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameS);
                string filePathS = "\\\\" + path + filenameS + "S.JPG";
                string filePathR = "\\\\" + path + filenameR + "R.JPG";
                if (File.Exists(filePathS))
                {
                    if (!IsFileLocked(filePathS))
                    {
                        File.Delete(filePathS);
                    }
                }
                if (File.Exists(filePathR))
                {
                    if (!IsFileLocked(filePathR))
                    {
                        File.Delete(filePathR);
                    }
                }
                File.Move(FILEL.FullName, filePathS);
                File.Move(FILER.FullName, filePathR);
                String folderPath = bc.iniC.pathlocalStaffNote;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                string[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        File.Delete(file1);
                    }
                }
                String re = bc.bcDB.vsDB.updateActNo111(HN, PRENO, VSDATE);
                lfSbMessage.Text = "save OK";
                this.Dispose();
            }
            catch (Exception ex)
            {
                //lfSbStatus.Text = ex.Message.ToString();
                lfSbMessage.Text = err + " saveImgStaffNote " + ex.Message;
                new LogWriter("e", "FrmOPD saveImgStaffNote " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "saveImgStaffNote", ex.Message);
            }
        }
        private void getImage()
        {
            String folderPath = bc.iniC.pathlocalStaffNote;
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            
            string[] files = Directory.GetFiles(folderPath);
            if (files.Length > 0)
            {
                picL.Image = null;
                picR.Image = null;
                foreach (String file in files)
                {
                    if (picL.Image == null)
                    {
                        FILEL = new FileInfo(file);
                        using (FileStream fsR = new FileStream(FILEL.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (Image imgl = Image.FromStream(fsR))
                            {
                                picL.Image = (Image)imgl.Clone();
                            }
                        }
                        continue;
                    }
                    if (picR.Image == null)
                    {
                        FILER = new FileInfo(file);
                        using (FileStream fsR = new FileStream(FILER.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (Image imgr = Image.FromStream(fsR))
                            {
                                picR.Image = (Image)imgr.Clone();
                            }
                        }
                        continue;
                    }
                }
            }
        }
        private void setImage()
        {
            if (FLAGVIEW.Equals("checkup"))
            {
                String file1 = "", dd = "", mm = "", yy = "", err = "", preno1 = "";
                try
                {
                    err = "00";

                    String filenameS = "";
                    filenameS = "000000" + PRENO;
                    filenameS = filenameS.Substring(filenameS.Length - 6);

                    String filenameR = "", path = bc.iniC.pathScanStaffNote, year = "", mon = "", day = "";
                    year = VSDATE.Substring(0, 4);
                    mon = VSDATE.Substring(5, 2);
                    day = VSDATE.Substring(8, 2);
                    path += year + "\\" + mon + "\\" + day + "\\";

                    filenameR = "000000" + PRENO;
                    filenameR = filenameR.Substring(filenameR.Length - 6);
                    err = "07";
                    string filePathR = "\\\\" + path + filenameR + "R.JPG";
                    string filePathS = "\\\\" + path + filenameS + "S.JPG";
                    if (File.Exists(filePathS) && (!IsFileLocked(filePathS)))
                    {
                        using (FileStream fsR = new FileStream(filePathR, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (Image imgr = Image.FromStream(fsR))
                            {
                                picR.Image = (Image)imgr.Clone();
                            }
                        }
                    }
                    if (File.Exists(filePathR) && (!IsFileLocked(filePathR)))
                    {
                        using (FileStream fsS = new FileStream(filePathS, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (Image imgl = Image.FromStream(fsS))
                            {
                                picL.Image = (Image)imgl.Clone();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {

                }
                
            }
        }
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }
        private void FrmStaffNote_Load(object sender, EventArgs e)
        {
            c1SplitContainer1.HeaderHeight = 0;
            setImage();
            
        }
    }
}
