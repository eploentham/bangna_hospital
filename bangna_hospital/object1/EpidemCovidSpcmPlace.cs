using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class EpidemCovidSpcmPlace
    {
        public String epidem_covid_spcm_place_id { get; set; }
        public String epidem_covid_spcm_place_name { get; set; }
        public void setCboMarital(C1ComboBox c, List<EpidemCovidSpcmPlace> lPersT, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            c.Items.Clear();
            int i = 0;
            if (lPersT.Count <= 0) return;
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (EpidemCovidSpcmPlace cus1 in lPersT)
            {
                item = new ComboBoxItem();
                item.Value = cus1.epidem_covid_spcm_place_id;
                item.Text = cus1.epidem_covid_spcm_place_name;
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
