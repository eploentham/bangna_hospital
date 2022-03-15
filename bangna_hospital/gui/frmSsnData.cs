using bangna_hospital.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class frmSsnData : Form
    {
        BangnaControl bc;
        DataTable dtBr1 = new DataTable();
        DataTable dtBr2 = new DataTable();
        DataTable dtBr5 = new DataTable();
        public frmSsnData(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            btnBrow1.Click += BtnBrow1_Click;
            btnBrow2.Click += BtnBrow2_Click;
            btnBrow5.Click += BtnBrow5_Click;
            btnImp1.Click += BtnImp1_Click;
            btnImp2.Click += BtnImp2_Click;
            btnImp5.Click += BtnImp5_Click;
            btnInsert.Click += BtnInsert_Click;
            btnM01.Click += BtnM01_Click;
        }

        private void BtnM01_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            listBox1.Items.Add("InsertPrakunM01 start");
            Application.DoEvents();
            String re = bc.bcDB.InsertPrakunM01();
            listBox1.Items.Add("InsertPrakunM01 end " + re);
            Application.DoEvents();
        }

        private void BtnImp5_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            btnImp5.Enabled = false;
            DataTable dt = setDataTable("5");
            dtBr5 = new DataTable();
            dtBr5 = dt.Copy();
            btnImp5.Enabled = true;
        }

        private void BtnImp2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            btnImp2.Enabled = false;
            DataTable dt = setDataTable("2");
            dtBr2 = new DataTable();
            dtBr2 = dt.Copy();
            btnImp2.Enabled = true;
        }

        private void BtnBrow5_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog res = new OpenFileDialog();
            res.Filter = "Text Files|*.txt;";
            res.Filter = "All Files|*.*;";
            if (res.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                txtPath5.Text = res.FileName;
                //Do something
            }
            btnImp5.Enabled = true;
            res.Dispose();
        }

        private void BtnBrow2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog res = new OpenFileDialog();
            res.Filter = "Text Files|*.txt;";
            res.Filter = "All Files|*.*;";
            if (res.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                txtPath2.Text = res.FileName;
                //Do something
            }
            btnImp2.Enabled = true;
            res.Dispose();
        }
        private void BtnBrow1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog res = new OpenFileDialog();
            res.Filter = "Text Files|*.txt;";
            res.Filter = "All Files|*.*;";
            if (res.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                txtPath1.Text = res.FileName;
                //Do something
            }
            btnImp1.Enabled = true;
            res.Dispose();
        }
        private void BtnInsert_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (dtBr1.Rows.Count > 0)
            {
                bc.bcDB.BulkInsert(dtBr1, "1");
                listBox1.Items.Add("BulkInsert 1 ok");
                Application.DoEvents();
            }
            if (dtBr2.Rows.Count > 0)
            {
                bc.bcDB.BulkInsert(dtBr2, "2");
                listBox1.Items.Add("BulkInsert 2 ok");
                Application.DoEvents();
            }
            if (dtBr5.Rows.Count > 0)
            {
                bc.bcDB.BulkInsert(dtBr5, "5");
                listBox1.Items.Add("BulkInsert 5 ok");
                Application.DoEvents();
            }
        }

        private void BtnImp1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            btnImp1.Enabled = false;
            DataTable dt = setDataTable("1");
            dtBr1 = new DataTable();
            dtBr1 = dt.Copy();
            btnImp1.Enabled = true;
        }

        private DataTable setDataTable(String flag)
        {
            String path = "", err="";
            DataTable dt = new DataTable();
            dt.Columns.Add("SocialID", typeof(System.String));
            dt.Columns.Add("Social_Card_no", typeof(System.String));
            dt.Columns.Add("TitleName", typeof(System.String));
            dt.Columns.Add("FirstName", typeof(System.String));
            dt.Columns.Add("LastName", typeof(System.String));
            dt.Columns.Add("FullName", typeof(System.String));
            dt.Columns.Add("PrakanCode", typeof(System.String));
            dt.Columns.Add("Prangnant", typeof(System.String));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("BirthDay", typeof(DateTime));
            dt.Columns.Add("UploadDate", typeof(DateTime));
            dt.Columns.Add("FLAG", typeof(System.String));
            listBox1.Items.Add("ssn_data1 add");
            try
            {
                if (flag.Equals("1"))
                {
                    path = txtPath1.Text;
                    label4.Text = "";
                    //dtBr1.Clear();
                    //dtBr1.Columns.Add("ssn_data", typeof(System.String));
                }
                else if (flag.Equals("2"))
                {
                    path = txtPath2.Text;
                    label5.Text = "";
                    //dtBr2.Clear();
                    //dtBr2.Columns.Add("ssn_data", typeof(System.String));
                }
                else if (flag.Equals("5"))
                {
                    path = txtPath5.Text;
                    label6.Text = "";
                    //dtBr5.Clear();
                    //dtBr5.Columns.Add("ssn_data", typeof(System.String));
                }
                Application.DoEvents();
                if (File.Exists(path))
                {
                    int counter = 0, cnt = 0;
                    try
                    {
                        err = "00";
                        CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                        String uploadate = "", uploadate1 = "", datetest="01-01-2022";
                        // Read file using StreamReader. Reads file line by line  
                        //pB1.Minimum = 0;
                        //pB1.Maximum=
                        Encoding utf8 = Encoding.UTF8;
                        Encoding inAsciiEncoding = Encoding.ASCII;
                        Encoding inAsciiEncoding874 = Encoding.GetEncoding(874);
                        uploadate = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
                        //uploadate = DateTime.Now.Year + DateTime.Now.ToString("MMdd");
                        uploadate1 = DateTime.Now.ToString("MM-dd")+"-"+ DateTime.Now.Year;
                        using (StreamReader file = new StreamReader(path, inAsciiEncoding874))
                        {
                            string ln;
                            while ((ln = file.ReadLine()) != null)
                            {
                                err = "01";
                                //Console.WriteLine(ln);
                                //byte[] utf8Bytes = utf8.GetBytes(ln);
                                //byte[] outUTF8Bytes = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, utf8Bytes);
                                //char[] inUTF8Chars = new char [Encoding.UTF8.GetCharCount(outUTF8Bytes, 0, outUTF8Bytes.Length)];
                                //Encoding.UTF8.GetChars(outUTF8Bytes, 0, outUTF8Bytes.Length, inUTF8Chars, 0);
                                //string outUTF8String = new string(inUTF8Chars);

                                //String txt = Encoding.GetEncoding(1252).GetString(utf8Bytes);


                                //byte[] utf8Bytes1 = bc.ToUTF8(utf8Bytes);
                                //string str = System.Text.Encoding.Default.GetString(utf8Bytes1);
                                //string s = System.Text.Encoding.UTF8.GetString(utf8Bytes1, 0, utf8Bytes1.Length);

                                //string value2 = utf8.GetString(utf8Bytes);
                                String socid = "", cardno = "", titlename = "", firstname = "", lastname = "", fullname = "", prakuncode = "", prang = "", startdate = "", enddate = "", dob = "", flag1 = "Y", year="", month="", day="";
                                DateTime datechk = new DateTime();
                                DateTime datechk1 = new DateTime();
                                DateTime datechk2 = new DateTime();
                                DateTime datechk3 = new DateTime();
                                DateTime datechk4 = new DateTime();
                                if (ln.Length > 20)
                                {
                                    err = "02";
                                    socid = ln.Substring(0, 13);
                                    cardno = ln.Substring(13, 13);
                                    titlename = ln.Substring(26, 15).Trim();
                                    firstname = ln.Substring(41, 30).Trim();
                                    lastname = ln.Substring(71, 28).Trim();
                                    fullname = titlename + " " + firstname + " " + lastname;
                                    prakuncode = ln.Substring(99, 11).Trim();
                                    prang = prakuncode.Substring(1, 1);
                                    prakuncode = prakuncode.Substring(4, 7);
                                    
                                    startdate = ln.Substring(110, 8).Trim();
                                    year = startdate.Substring(0, 4);
                                    month = startdate.Substring(4, 2);
                                    day = startdate.Substring(6, 2);
                                    if (int.Parse(year)>2500)
                                    {
                                        year = (int.Parse(year) - 543).ToString();
                                    }
                                    err = "021";
                                    startdate = year + "-" + month + "-" + day;
                                    //startdate = year + month + day;
                                    DateTime.TryParse(startdate, out datechk1);
                                    if (!DateTime.TryParse(startdate, out datechk))
                                    {
                                        listBox1.Items.Add("startdate " + startdate);
                                    }
                                    //startdate = month + "-" + day+"-"+year;

                                    enddate = ln.Substring(118, 8).Trim();
                                    year = enddate.Substring(0, 4);
                                    month = enddate.Substring(4, 2);
                                    day = enddate.Substring(6, 2);
                                    //year = (int.Parse(year) - 543).ToString();
                                    if (int.Parse(year) > 2500)
                                    {
                                        year = (int.Parse(year) - 543).ToString();
                                    }
                                    enddate = year + "-" + month + "-" + day;
                                    //enddate = year + month + day;
                                    DateTime.TryParse(enddate, out datechk2);
                                    if (!DateTime.TryParse(enddate, out datechk))
                                    {
                                        listBox1.Items.Add("enddate " + enddate);
                                    }
                                    //enddate = month + "-" + day+"-"+year;
                                    err = "022";
                                    dob = ln.Substring(126, 8).Trim();
                                    year = dob.Substring(0, 4);
                                    month = dob.Substring(4, 2);
                                    day = dob.Substring(6, 2);
                                    //year = (int.Parse(year) - 543).ToString();
                                    if (int.Parse(year) > 2500)
                                    {
                                        year = (int.Parse(year) - 543).ToString();
                                    }
                                    dob = year + "-" + month + "-" + day;
                                    //dob = year + month + day;
                                    DateTime.TryParse(dob, out datechk3);
                                    if (!DateTime.TryParse(dob, out datechk))
                                    {
                                        listBox1.Items.Add("dob " + dob);
                                        if (day.Equals("29"))
                                        {
                                            dob = year + "-" + month + "-28";
                                            if (!DateTime.TryParse(dob, out datechk3))
                                            {
                                                listBox1.Items.Add("dob1 " + dob);
                                            }
                                        }
                                    }
                                    //dob = month + "-" + day + "-" + year;                                    
                                    //DateTime.TryParse(uploadate, out datechk4);
                                    //uploadate = DateTime.Now.Year+"-"+DateTime.Now.ToString("MM-dd");
                                    err = "023";
                                    DataRow drow = dt.Rows.Add();
                                    drow["SocialID"] = socid;
                                    drow["Social_Card_no"] = cardno;
                                    drow["TitleName"] = titlename;
                                    drow["FirstName"] = firstname;
                                    drow["LastName"] = lastname;
                                    drow["FullName"] = fullname;
                                    drow["PrakanCode"] = prakuncode;
                                    drow["Prangnant"] = prang;
                                    drow["StartDate"] = startdate;
                                    drow["EndDate"] = enddate;
                                    drow["BirthDay"] = dob;
                                    drow["UploadDate"] = DateTime.Now;
                                    drow["FLAG"] = flag1;
                                    counter++;
                                }
                                //dt.Rows.Add(drow);
                                cnt++;
                            }
                            file.Close();
                            //Console.WriteLine($ "File has {counter} lines.");
                        }
                        if (flag.Equals("1"))
                        {
                            label4.Text = counter.ToString() + " " + cnt.ToString();
                        }
                        else if (flag.Equals("2"))
                        {
                            label5.Text = counter.ToString() + " " + cnt.ToString();
                        }
                        else if (flag.Equals("5"))
                        {
                            label6.Text = counter.ToString() + " " + cnt.ToString();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("error " + ex.Message+ " counter " + counter.ToString()+" err "+err, "");
                    }
                }
            }
            catch(Exception ex)
            {
                listBox1.Items.Add("error "+ex.Message);
            }
            
            listBox1.Items.Add("ssn_data1 add success");
            //Console.ReadKey();
            return dt;
        }
        private void frmSsnData_Load(object sender, EventArgs e)
        {
            this.Text = "2022-02-17";
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            lB1.Text = currentCulture.Name;
        }
    }
}
