using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class Queue:Persistent
    {
        public String queue_id { get; set; }
        public String queue_type_id { get; set; }
        public String queue_seq { get; set; }
        public String queue_date { get; set; }
    }
}
