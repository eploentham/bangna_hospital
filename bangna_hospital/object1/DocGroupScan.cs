using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class DocGroupScan:Persistent
    {
        public String doc_group_id { get; set; }
        public String doc_group_name { get; set; }
        public String active { get; set; }
        public String remark { get; set; }
        public String status_opd { get; set; }
    }
}
