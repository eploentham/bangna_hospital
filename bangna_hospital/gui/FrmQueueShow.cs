using bangna_hospital.control;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
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
    public partial class FrmQueueShow : Form
    {
        BangnaControl bc;
        Timer timeQueImg, timeQueQue;
        C1FlexGrid grfQue, grfQueToday;
        Font fEdit, fEditB;
        C1ThemeController theme1;
        ImageList imglist;

        int colTodayrowno = 1, colTodayQueName = 2, colTodayqueSum = 3, colTodayQueCurr = 4;
        int indexQueImg = 0;
        Boolean pageLoad = false;
        String[] filesQueImg;
        public FrmQueueShow(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new Font(bc.iniC.grdQueFontName, bc.grdQueFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdQueFontName, bc.grdQueFontSize, FontStyle.Bold);
            theme1 = new C1ThemeController();

            timeQueQue = new Timer();
            timeQueQue.Interval = 3000;
            timeQueQue.Enabled = true;
            timeQueQue.Tick += TimeQueQue_Tick;

            timeQueImg = new Timer();
            timeQueImg.Interval = 15000;
            timeQueImg.Enabled = true;
            timeQueImg.Tick += TimeQueImg_Tick;

            imglist = new ImageList();

            initGrfQueToday();
            setGrfQueueToday();
            getImageQue();
            timeQueQue.Start();
            timeQueImg.Start();
            pageLoad = false;
        }
        private void initGrfQueToday()
        {
            grfQueToday = new C1FlexGrid();
            grfQueToday.Font = fEdit;
            grfQueToday.Dock = System.Windows.Forms.DockStyle.Fill;
            grfQueToday.Location = new System.Drawing.Point(0, 0);

            grfQueToday.DataSource = null;
            grfQueToday.Rows.Count = 1;
            //grfQue.Rows.Count = 200;
            grfQueToday.Cols.Count = 5;

            grfQueToday.Cols[colTodayrowno].Width = 70;
            grfQueToday.Cols[colTodayQueName].Width = 340;
            grfQueToday.Cols[colTodayQueCurr].Width = 100;
            grfQueToday.Cols[colTodayqueSum].Width = 150;

            grfQueToday.ShowCursor = true;

            grfQueToday.Cols[colTodayrowno].Caption = "no";
            grfQueToday.Cols[colTodayQueName].Caption = "";
            grfQueToday.Cols[colTodayQueCurr].Caption = "คิว";
            grfQueToday.Cols[colTodayqueSum].Caption = "ทั้งหมด";

            //FilterRow fr = new FilterRow(grfExpn);

            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);            
            pnQueToday.Controls.Add(grfQueToday);
            grfQueToday.Rows.Count = 1;
            grfQueToday.Cols.Count = 5;
            grfQueToday.Cols[colTodayrowno].Visible = false;
            grfQueToday.Cols[colTodayqueSum].Visible = false;
            grfQueToday.Cols[0].Visible = false;

            //grfImg.Cols[colPathPic].Visible = false;
            grfQueToday.Cols[colTodayrowno].AllowEditing = false;
            grfQueToday.Cols[colTodayQueName].AllowEditing = false;
            grfQueToday.Cols[colTodayQueCurr].AllowEditing = false;
            grfQueToday.Cols[colTodayqueSum].AllowEditing = false;

            //grfImg.AutoSizeCols();
            grfQueToday.AutoSizeRows();

            theme1.SetTheme(grfQueToday, "Office2007Blue");
        }
        private void setGrfQueueToday()
        {
            //grfQue.Clear();
            pageLoad = true;
            //timeQueImg.Enabled = false;
            //timeQueQue.Enabled = false;
            DataTable dt = new DataTable();
            //DateTime dtToday = new DateTime();
            //DateTime.TryParse(txtDate.Text, out dtToday);
            String date = "";
            date = DateTime.Now.Year + DateTime.Now.ToString("-MM-dd");
            dt = bc.bcDB.sumt03DB.selectQueDoctorToday();
            grfQueToday.Rows.Count = dt.Rows.Count + 1;
            //if (dt.Rows.Count == 0)
            //    grfQueToday.Rows.Count++;
            int i = 1;
            foreach (DataRow drow in dt.Rows)
            {
                grfQueToday.Rows[i][colTodayrowno] = i;
                grfQueToday[i, 0] = i;
                grfQueToday[i, colTodayQueName] = drow["dtr_name"].ToString();
                grfQueToday[i, colTodayQueCurr] = drow["queue_current"].ToString();
                grfQueToday[i, colTodayqueSum] = drow["MNC_SUM_VN_ADD"].ToString();

                i++;
            }
            //grfQue.Rows[0].Visible = false;
            
            pageLoad = false;
            //theme1.SetTheme(grfQue, "Office2016Colorful");
            //timeQueImg.Enabled = true;
            //timeQueQue.Enabled = true;
        }
        private void TimeQueImg_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            indexQueImg++;
            if (indexQueImg >= filesQueImg.Length)
            {
                indexQueImg = 0;
            }
            picQue.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\"+ filesQueImg[indexQueImg]);
            //MessageBox.Show(System.IO.Directory.GetCurrentDirectory() + "\\" + filesQueImg[indexQueImg], "");
            //Application.DoEvents();
        }

        private void TimeQueQue_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfQueueToday();
        }
        private void getImageQue()
        {
            filesQueImg = Directory.GetFiles("image_que_show");
            foreach (string file in filesQueImg)
            {
                Console.WriteLine(Path.GetFileName(file));
                //picQue.Image = Image.FromFile(file);
                //imglist.Images.Add(Image.FromFile(file));
                
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                appExit();
            }
            else
            {
                //keyData
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private Boolean appExit()
        {
            //flagExit = true;
            if (MessageBox.Show("ต้องการออกจากโปรแกรม2", "ออกจากโปรแกรม menu", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void FrmQueueShow_Load(object sender, EventArgs e)
        {
            indexQueImg++;
            if (indexQueImg > imglist.Images.Count)
            {
                indexQueImg = 0;
            }
            picQue.Image = Image.FromFile(filesQueImg[indexQueImg]);
            picQue.Dock = DockStyle.Fill;
            //picQue.SizeMode = PictureBoxSizeMode.StretchImage;
            picQue.SizeMode = PictureBoxSizeMode.AutoSize;
            grfQueToday.Focus();
            
        }
    }
}
