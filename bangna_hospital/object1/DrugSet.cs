using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class DrugSet:Persistent
    {
        public String drug_set_id { get; set; }
        public String doctor_id { get; set; }
        public String drug_set_name { get; set; }
        public String item_code { get; set; }
        public String item_name { get; set; }
        public String status_item { get; set; }
        public String qty { get; set; }
        public String frequency { get; set; }
        public String precautions { get; set; }
        public String interaction { get; set; }
        public String indication { get; set; }
    }
}
