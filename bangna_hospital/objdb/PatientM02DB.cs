using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientM02DB
    {
        public PatientM02 pm02;
        ConnectDB conn;
        public List<PatientM02> lPm02;

        public PatientM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm02 = new PatientM02();
            pm02.MNC_PFIX_CD = "MNC_PFIX_CD";
            pm02.MNC_PFIX_DSC = "MNC_PFIX_DSC";
            pm02.MNC_SEX = "MNC_SEX";
            pm02.MNC_PFIX_DSC_e = "MNC_PFIX_DSC_e";

            pm02.table = "patient_M02";

            lPm02 = new List<PatientM02>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm02.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM02 cus1 = new PatientM02();
                cus1.MNC_PFIX_CD = row[pm02.MNC_PFIX_CD].ToString();
                cus1.MNC_PFIX_DSC = row[pm02.MNC_PFIX_DSC].ToString();
                cus1.MNC_SEX = row[pm02.MNC_SEX].ToString();
                cus1.MNC_PFIX_DSC_e = row[pm02.MNC_PFIX_DSC_e].ToString();

                lPm02.Add(cus1);
            }
        }
        public String getSexByCode(String code)
        {
            String re = "";
            foreach (PatientM02 row in lPm02)
            {
                if (row.MNC_PFIX_CD.Equals(code))
                {
                    re = row.MNC_SEX;
                    break;
                }
            }
            return re;
        }
        public String getCodeByNameE(String name)
        {
            String re = "";
            foreach (PatientM02 row in lPm02)
            {
                if (row.MNC_PFIX_DSC_e.Equals(name))
                {
                    re = row.MNC_PFIX_CD;
                    break;
                }
            }
            return re;
        }
        public String getCodeByName(String name)
        {
            String re = "", txt= name;
            if(name.Equals("MISS")) txt = "Miss.";
            foreach (PatientM02 row in lPm02)
            {
                if (row.MNC_PFIX_DSC.Equals(txt))
                {
                    re = row.MNC_PFIX_CD;
                    break;
                }
            }
            if((re.Length <= 0)&&name.Equals("MISS"))
            {
                foreach (PatientM02 row in lPm02)
                {
                    if (row.MNC_PFIX_DSC_e.Equals(name))
                    {
                        re = row.MNC_PFIX_CD;
                        break;
                    }
                }
            }
            return re;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm02.* " +
                "From  patient_M02 pm02 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void setCboPrefixT(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm02.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM02 row in lPm02)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_PFIX_CD;
                item.Text = row.MNC_PFIX_DSC;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public void setCboPrefixT1(C1ComboBox c, String selected)
        {
            // ✅ Start timer
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            // Load data once if needed
            if (lPm02.Count <= 0) getlCus();

            // ✅ Check if already populated (cache check)
            if (c.Items.Count > 0 && string.IsNullOrEmpty(selected))
            {
                return; // Already loaded, skip
            }

            c.SuspendLayout();
            try
            {
                c.Items.Clear();

                // ✅ Pre-allocate capacity if supported
                // c.Items.Capacity = lPm02.Count + 1;

                // Add empty item
                c.Items.Add(new ComboBoxItem { Value = "", Text = "" });

                int selectedIndex = 0;

                // ✅ Use for loop with index (slightly faster than foreach)
                for (int i = 0; i < lPm02.Count; i++)
                {
                    var row = lPm02[i];
                    c.Items.Add(new ComboBoxItem
                    {
                        Value = row.MNC_PFIX_CD,
                        Text = row.MNC_PFIX_DSC
                    });

                    if (row.MNC_PFIX_CD.Equals(selected))
                    {
                        selectedIndex = i + 1; // +1 for empty item
                    }
                }

                c.SelectedIndex = selectedIndex;
            }
            finally
            {
                c.ResumeLayout();
            }
            // ✅ Stop timer and log
            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine(
                $"setCboPrefixT1: {stopwatch.ElapsedMilliseconds}ms ({stopwatch.ElapsedTicks} ticks)"
            );
            new LogWriter("d", $"setCboPrefixT1: {stopwatch.ElapsedMilliseconds}ms");
        }
        public void setCboPrefixT2_DataSource(C1ComboBox c, String selected)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            if (lPm02.Count <= 0) getlCus();

            // ✅ สร้าง binding list
            var bindingList = new List<ComboBoxItem>(lPm02.Count + 1){  new ComboBoxItem { Value = "", Text = "" }    };

            int selectedIndex = 0;
            for (int i = 0; i < lPm02.Count; i++)
            {
                var row = lPm02[i];
                bindingList.Add(new ComboBoxItem
                {
                    Value = row.MNC_PFIX_CD,
                    Text = row.MNC_PFIX_DSC
                });

                if (row.MNC_PFIX_CD.Equals(selected))
                {
                    selectedIndex = i + 1;
                }
            }

            // ✅ Bind ครั้งเดียว - เร็วกว่า Items.Add เยอะ!
            c.DataSource = bindingList;
            //c.DisplayMember = "Text";
            //c.ValueMember = "Value";
            //c.SelectedIndex = selectedIndex;

            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine($"setCboPrefixT2_DataSource: {stopwatch.ElapsedMilliseconds}ms");
        }
        public void setCboPrefixT_Fast(C1ComboBox c, String selected)
        {
            //var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            if (lPm02.Count <= 0) getlCus();

            c.SuspendLayout();
            try
            {
                c.Items.Clear();

                // ✅ สร้าง array ของ items ก่อน
                var itemsArray = new ComboBoxItem[lPm02.Count + 1];
                itemsArray[0] = new ComboBoxItem { Value = "", Text = "" };

                int selectedIndex = 0;

                for (int i = 0; i < lPm02.Count; i++)
                {
                    var row = lPm02[i];
                    itemsArray[i + 1] = new ComboBoxItem
                    {
                        Value = row.MNC_PFIX_CD,
                        Text = row.MNC_PFIX_DSC
                    };

                    if (row.MNC_PFIX_CD.Equals(selected))
                    {
                        selectedIndex = i + 1;
                    }
                }

                // ✅ Add ทีเดียวทั้งหมด (ถ้ารองรับ)
                try
                {
                    c.Items.AddRange(itemsArray);
                }
                catch (Exception)
                {
                    // Fallback: ถ้า AddRange ไม่มี ก็ใช้ Add ทีละตัว
                    foreach (var item in itemsArray)
                    {
                        c.Items.Add(item);
                    }
                }

                c.SelectedIndex = selectedIndex;
            }
            finally
            {
                c.ResumeLayout();
            }

            //stopwatch.Stop();
            //System.Diagnostics.Debug.WriteLine($"setCboPrefixT_Fast: {stopwatch.ElapsedMilliseconds}ms");
            //new LogWriter("d", $"setCboPrefixT_Fast: {stopwatch.ElapsedMilliseconds}ms");
        }
        public void setCboPrefixE(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm02.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM02 row in lPm02)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_PFIX_CD;
                item.Text = row.MNC_PFIX_DSC_e;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public void setCboPrefixE1(C1ComboBox c, String selected)
        {
            // Load data once if needed
            if (lPm02.Count <= 0) getlCus();

            // ✅ Check if already populated (cache check)
            if (c.Items.Count > 0 && string.IsNullOrEmpty(selected))
            {
                return; // Already loaded, skip
            }

            c.SuspendLayout();
            try
            {
                c.Items.Clear();

                // ✅ Pre-allocate capacity if supported
                // c.Items.Capacity = lPm02.Count + 1;

                // Add empty item
                c.Items.Add(new ComboBoxItem { Value = "", Text = "" });

                int selectedIndex = 0;

                // ✅ Use for loop with index (slightly faster than foreach)
                for (int i = 0; i < lPm02.Count; i++)
                {
                    var row = lPm02[i];
                    c.Items.Add(new ComboBoxItem
                    {
                        Value = row.MNC_PFIX_CD,
                        Text = row.MNC_PFIX_DSC_e
                    });

                    if (row.MNC_PFIX_CD.Equals(selected))
                    {
                        selectedIndex = i + 1; // +1 for empty item
                    }
                }

                c.SelectedIndex = selectedIndex;
            }
            finally
            {
                c.ResumeLayout();
            }
        }
        public void setCboPrefixE_Fast(C1ComboBox c, String selected)
        {
            //var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            if (lPm02.Count <= 0) getlCus();

            c.SuspendLayout();
            try
            {
                c.Items.Clear();

                // ✅ สร้าง array ของ items ก่อน
                var itemsArray = new ComboBoxItem[lPm02.Count + 1];
                itemsArray[0] = new ComboBoxItem { Value = "", Text = "" };

                int selectedIndex = 0;

                for (int i = 0; i < lPm02.Count; i++)
                {
                    var row = lPm02[i];
                    itemsArray[i + 1] = new ComboBoxItem
                    {
                        Value = row.MNC_PFIX_CD,
                        Text = row.MNC_PFIX_DSC_e
                    };

                    if (row.MNC_PFIX_CD.Equals(selected))
                    {
                        selectedIndex = i + 1;
                    }
                }

                // ✅ Add ทีเดียวทั้งหมด (ถ้ารองรับ)
                try
                {
                    c.Items.AddRange(itemsArray);
                }
                catch (Exception)
                {
                    // Fallback: ถ้า AddRange ไม่มี ก็ใช้ Add ทีละตัว
                    foreach (var item in itemsArray)
                    {
                        c.Items.Add(item);
                    }
                }

                c.SelectedIndex = selectedIndex;
            }
            finally
            {
                c.ResumeLayout();
            }

            //stopwatch.Stop();
            //System.Diagnostics.Debug.WriteLine($"setCboPrefixT_Fast: {stopwatch.ElapsedMilliseconds}ms");
            //new LogWriter("d", $"setCboPrefixT_Fast: {stopwatch.ElapsedMilliseconds}ms");
        }
        public PatientM02 setPatientM08(DataTable dt)
        {
            PatientM02 pm08 = new PatientM02();
            if (dt.Rows.Count > 0)
            {
                pm08.MNC_PFIX_CD = dt.Rows[0]["MNC_PFIX_CD"].ToString();
                pm08.MNC_PFIX_DSC = dt.Rows[0]["MNC_PFIX_DSC"].ToString();
                pm08.MNC_SEX = dt.Rows[0]["MNC_SEX"].ToString();
                pm08.MNC_PFIX_DSC_e = dt.Rows[0]["MNC_PFIX_DSC_e"].ToString();
            }
            else
            {
                setPatientM08(pm08);
            }
            return pm08;
        }
        public PatientM02 setPatientM08(PatientM02 p)
        {
            p.MNC_PFIX_CD = "";
            p.MNC_PFIX_DSC = "";
            p.MNC_SEX = "";
            p.MNC_PFIX_DSC_e = "";
            return p;
        }
    }
}
