using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class LabOutRIALabs
    {
        public string ln { get; set; }
        public string result { get; set; }
        public object sub_result { get; set; }
        public string unit { get; set; }
        public string method { get; set; }
        public string test_code { get; set; }
        public string test_name { get; set; }
        public string specimen { get; set; }
        public string profile_name { get; set; }
        public string normal_range { get; set; }
        public string report { get; set; }
        public string comment { get; set; }
        public string status { get; set; }
        public string report_by { get; set; }
        public string report_time { get; set; }
        public string approve_by { get; set; }
        public string approve_time { get; set; }
        public List<Object> attach { get; set; }
    }
}
