using bangna_hospital.control;
using C1.C1Excel;
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
    public partial class FrmVaccineInsurExcel : Form
    {
        BangnaControl bc;
        public FrmVaccineInsurExcel(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            btnBrowe.Click += BtnBrowe_Click;
            btnExcel.Click += BtnExcel_Click;
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1XLBook _c1xl = new C1XLBook();
            
            if (File.Exists(txtPath.Text))
            {
                _c1xl.Load(txtPath.Text);
                int rowstart = 0, colDOB = 0, colPID = 0, colProv = 0, row1 = 0, indexrow = 0;
                var sheet = _c1xl.Sheets[0];
                int rowcnt = sheet.Rows.Count;
                row1 = rowstart;
                pB1.Maximum = rowcnt;
                pB1.Minimum = 1;
                int.TryParse(txtDOB.Text, out colDOB);
                int.TryParse(txtPID.Text, out colPID);
                int.TryParse(txtProv.Text, out colProv);
                for (int row = 1; row < rowcnt; row++)
                {
                    if (row == 1) continue;
                    String doseid = "", prov = "", dob = "", pid = "", address="";
                    dob = sheet[row, colDOB].Value != null ? sheet[row, colDOB].Value.ToString() : "";
                    pid = sheet[row, colPID].Value != null ? sheet[row, colPID].Value.ToString() : "";
                    prov = sheet[row, colProv].Value != null ? sheet[row, colProv].Value.ToString() : "";
                    if (pid.Length <= 10) continue;
                    DataTable dt = new DataTable();
                    dt = bc.bcDB.vaccDB.SelectDoseByPID(pid);
                    if (dt.Rows.Count > 0)
                    {
                        doseid = dt.Rows[0]["reserve_vaccine_dose_id"].ToString();
                        address = dt.Rows[0]["address1"] != null ? dt.Rows[0]["address1"].ToString():"";
                        address = address.Length > 0 ? address : prov;
                        bc.bcDB.vaccDB.updateDoseDOBAddress1(doseid, address, dob, txtDate.Text.Trim());
                    }

                    pB1.Value++;
                }
            }
        }

        private void BtnBrowe_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\", Title = "Browse Text Files", CheckFileExists = true, CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Excel Files | *.xls; *.xlsx; *.*;",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void FrmVaccineInsurExcel_Load(object sender, EventArgs e)
        {
            this.Text = "Update 21-11-2021";
        }
    }
}
