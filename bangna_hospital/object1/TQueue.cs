using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class TQueue:Persistent
    {
        public String t_queue_id { get; set; }
        public String queue_id { get; set; }
        public String date_queue { get; set; }
        public String queue_seq { get; set; }
    }
}
