using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    internal class TExpense:Persistent
    {
        public long expense_id { get; set; }
        public string expense_date { get; set; }
        public string expense_desc { get; set; }
        public decimal expense { get; set; }
        public string module_id { get; set; }
        public string t_expensecol { get; set; }
        public long? row_id { get; set; }
        public string table_name { get; set; }
        public string user_req_id { get; set; }
        public string user_approve_id { get; set; }
        public string user_cashier_id { get; set; }
        public string date_req { get; set; }
        public string date_approve { get; set; }
        public string date_cashier { get; set; }
    }
}
