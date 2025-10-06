using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class AttachNote:Persistent
    {
        public String attach_note_id { get; set; }
        public String attach_note { get; set; }
        public String hn { get; set; }
        public AttachNote()
        {
            attach_note_id = "attach_note_id";
            attach_note = "attach_note";
            active = "active";
            remark = "remark";
            date_create = "date_create";
            user_create = "user_create";
            user_modi = "user_modi";
            date_modi = "date_modi";
            hn = "hn";
        }
    }
}
