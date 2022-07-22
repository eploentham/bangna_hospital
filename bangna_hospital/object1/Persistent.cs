using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class Persistent
    {
        public String table = "";
        public String pkField = "";
        public String sited = "";

        public String active { get; set; }
        public String remark { get; set; }

        public String date_create { get; set; }
        public String date_modi { get; set; }
        public String date_cancel { get; set; }
        public String user_create { get; set; }
        public String user_modi { get; set; }
        public String user_cancel { get; set; }

        Random r = new Random();
        public String getGenID()
        {
            //return r.Next().ToString();
            return DateTime.Now.Ticks.ToString();
        }
        public String getGUIDID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
