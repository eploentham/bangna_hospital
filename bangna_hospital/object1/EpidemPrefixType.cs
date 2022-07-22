using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class EpidemPrefixType
    {
        public String prefix_type_id { get; set; }
        public String prefix_type_name { get; set; }
        public void setCboPrefix(C1ComboBox c, List<EpidemPrefixType> lPersT, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectAll();
            int i = 0;
            if (lPersT.Count <= 0) return;
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (EpidemPrefixType cus1 in lPersT)
            {
                item = new ComboBoxItem();
                item.Value = cus1.prefix_type_id;
                item.Text = cus1.prefix_type_name;
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
