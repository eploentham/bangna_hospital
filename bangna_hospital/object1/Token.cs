using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class Token:Persistent
    {
        public String token_id = "", token_module_name = "", date_expire = "", user_name = "", secret_key = "", token_text = "", number_days_expire = "", token_type = "";
        public String used = "", used_by_table_id="", used_date="";
    }
}
