using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class DocGroupSubScan:Persistent
    {
        public String doc_group_sub_id { get; set; }
        public String doc_group_id { get; set; }
        public String doc_group_sub_name { get; set; }
        public String active { get; set; }
        public String remark { get; set; }
        public String dept_us { get; set; }
    }
}
