using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class FLocationDB
    {
        ConnectDB conn;
        FLocation floca;
        public List<FLocation> lChw;
        public FLocationDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            floca = new FLocation();
            floca.amp_code = "amp_code";
            floca.amp_name_t = "amp_name_t";
            floca.chw_code = "chw_code";
            floca.chw_name_t = "chw_name_t";
            floca.location_code = "location_code";
            floca.tmb_code = "tmb_code";
            floca.tmb_name_t = "tmb_name_t";

            floca.table = "f_location";
            floca.pkField = "location_code";
            lChw = new List<FLocation>();

            //getlChw();
        }
        public DataTable selectProvince()
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn = "", wherename = "";

            wherehn = "amp_code='00' and tmb_code = '00' ";
            
            sql = "Select chw_code,chw_name_t " +
                "From  f_location " +
                //" inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where  " + wherehn;
            dt = conn.selectData(conn.connSsnData, sql);
            return dt;
        }
        public DataTable selectAmphur(String chw_code)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn = "", wherename = "";

            wherehn = "chw_code='"+ chw_code + "' and amp_code != '00' and tmb_code = '00' ";

            sql = "Select amp_code,amp_name_t " +
                "From  f_location " +
                //" inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where  " + wherehn;
            dt = conn.selectData(conn.connSsnData, sql);
            return dt;
        }
        public DataTable selectTumbon(String chw_code, String amp_code)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn = "", wherename = "";

            wherehn = "chw_code='" + chw_code + "' and amp_code = '"+ amp_code + "' and tmb_code != '00' ";

            sql = "Select tmb_code,tmb_name_t " +
                "From  f_location " +
                //" inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where  " + wherehn;
            dt = conn.selectData(conn.connSsnData, sql);
            return dt;
        }
        public C1ComboBox setCboTumbon(C1ComboBox c, String provcode, String amp_code)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            //DataTable dt = selectC1();
            //lDistrict.Clear();
            DataTable dttumb = selectTumbon(provcode, amp_code);
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            c.Items.Clear();
            c.Items.Add(item1);
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (DataRow drow in dttumb.Rows)
            {
                item = new ComboBoxItem();
                item.Value = drow["tmb_code"].ToString();
                item.Text = drow["tmb_name_t"].ToString();
                c.Items.Add(item);
                i++;
            }
            c.SelectedText = select;
            c.SelectedIndex = row1;
            return c;
        }
        public C1ComboBox setCboAmphur(C1ComboBox c, String provcode)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            //DataTable dt = selectC1();
            //lDistrict.Clear();
            DataTable dtamp = selectAmphur(provcode);
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            c.Items.Clear();
            c.Items.Add(item1);
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (DataRow drow in dtamp.Rows)
            {
                item = new ComboBoxItem();
                item.Value = drow["amp_code"].ToString();
                item.Text = drow["amp_name_t"].ToString();
                c.Items.Add(item);
                i++;
            }
            c.SelectedText = select;
            c.SelectedIndex = row1;
            return c;
        }
        public void getlChw()
        {
            //lDept = new List<Position>();
            String err = "";
            try
            {
                lChw.Clear();
                DataTable dt = new DataTable();
                dt = selectProvince();
                foreach (DataRow row in dt.Rows)
                {
                    FLocation itm1 = new FLocation();
                    itm1.chw_code = row[floca.chw_code].ToString();
                    itm1.chw_name_t = row[floca.chw_name_t].ToString();
                    lChw.Add(itm1);
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FLocationDB getlChw   " + ex.Message+ " conn.connSsnData.ConnectionString " + conn.connSsnData.ConnectionString);
            }
            
        }
        public C1ComboBox setCboProvince(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            //lDistrict.Clear();
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            c.Items.Clear();
            c.Items.Add(item1);
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (FLocation drow in lChw)
            {
                item = new ComboBoxItem();
                item.Value = drow.chw_code;
                item.Text = drow.chw_name_t;
                c.Items.Add(item);
                i++;
            }
            c.SelectedText = select;
            c.SelectedIndex = row1;
            return c;
        }
    }
}
