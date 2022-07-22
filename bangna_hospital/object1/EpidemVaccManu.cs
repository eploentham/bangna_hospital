using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class EpidemVaccManu
    {
        public String vaccine_manufacturer_id { get; set; }
        public String vaccine_manufacturer_name { get; set; }
        public String default_ref_name { get; set; }
        public String is_active { get; set; }
        public String max_interval_date { get; set; }
        public String total_plan_count { get; set; }
        public void setCboVaccManu(C1ComboBox c, List<EpidemVaccManu> lPersT, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            c.Items.Clear();
            int i = 0;
            if (lPersT.Count <= 0) return;
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (EpidemVaccManu cus1 in lPersT)
            {
                item = new ComboBoxItem();
                item.Value = cus1.vaccine_manufacturer_id;
                item.Text = cus1.vaccine_manufacturer_name;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (c.SelectedIndex <= 0)
            {
                if (c.Items.Count >= 1)
                {
                    c.SelectedIndex = 1;
                }
            }
        }
    }
}
