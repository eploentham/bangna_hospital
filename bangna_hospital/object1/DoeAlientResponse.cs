﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class DoeAlientResponse
    {
        public String statuscode = "", statusdesc = "", empname = "", wkaddress = "", reqcode = "", btname = "";
        public List<DoeAlienList> alienlist { get; set; }
    }
}
