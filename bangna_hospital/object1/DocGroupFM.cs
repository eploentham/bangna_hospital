using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class DocGroupFM:Persistent
    {
        public String fm_id { get; set; }
        public String doc_group_sub_id { get; set; }
        public String fm_code { get; set; }
        public String fm_name { get; set; }
        public String active { get; set; }
        public String doc_group_id { get; set; }
        public String status_doc_medical { get; set; }
        public String status_doc_nurse { get; set; }
        public String status_doc_adminsion { get; set; }
        public String status_doc_office { get; set; }
    }
}
