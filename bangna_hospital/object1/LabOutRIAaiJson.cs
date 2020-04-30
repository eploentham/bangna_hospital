using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class LabOutRIAaiJson
    {
        public class aiJson
        {
            public Patient patient { get; set; }
            public Orderdetail orderdetail { get; set; }
            public List<Lab> labs { get; set; }
            public List<string> report { get; set; }
            public string report_original { get; set; }
        }
        public class Patient
        {
            public string hn { get; set; }
            public string idcard { get; set; }
            public string title { get; set; }
            public string fname { get; set; }
            public string lname { get; set; }
            public string sex { get; set; }
            public string dob { get; set; }
        }
        public class Orderdetail
        {
            public string order_number { get; set; }
            public string ln { get; set; }
            public string hn_customer { get; set; }
            public string ref_no { get; set; }
            public string ward_customer { get; set; }
            public string status { get; set; }
            public string doctor { get; set; }
            public string comment_order { get; set; }
            public string comment_patient { get; set; }
            public string time_register { get; set; }
            public int download { get; set; }
        }
        public class Lab
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
            public List<object> attach { get; set; }
        }
        //test/
        //public void readfileJson()
        //{
        //    while (isRun)
        //    {

        //        string folder = AppDomain.CurrentDomain.BaseDirectory + "temp";
        //        DirectoryInfo dirf = new DirectoryInfo(folder);
        //        List<FileInfo> dir = dirf.GetFiles("*.json", SearchOption.AllDirectories).OrderBy(o => o.CreationTime).ToList();
        //        jsonArgs jsonArgs = new jsonArgs();
        //        jsonArgs.jsoncount = dir.Count();

        //        if (dir.Count == 0)
        //        {
        //            jsonArgs.actingLog = "No file json...";
        //            jsonLoging(jsonArgs);
        //            System.Threading.Thread.Sleep(10000);
        //            continue;
        //        }

        //        var f = dir[0];
        //        StreamReader sf = f.OpenText();
        //        string json = sf.ReadToEnd();
        //        sf.Close();
        //        sf.Dispose();
        //        try
        //        {
        //            //get json to object
        //            aiJson ojbJson = JsonConvert.DeserializeObject<aiJson>(json);

        //            string fullname = ojbJson.patient.title + ojbJson.patient.fname + " " + ojbJson.patient.lname;
        //            string dateRegis = ojbJson.orderdetail.time_register;

        //            jsonArgs.actingLog = "Read .." + fullname + "\rRegis date :" + dateRegis + " LN :" + ojbJson.orderdetail.ln;
        //            jsonLoging(jsonArgs);


        //            updatedb.getJson(ojbJson);

        //            jsonArgs.actingLog = "Close .. " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss\r\n");
        //            jsonArgs.fullfileName = f.FullName;
        //            jsonArgs.Readsuccessful = true;
        //            ojbJson = null;
        //            jsonLoging(jsonArgs);

        //            System.Threading.Thread.Sleep(5000);
        //        }
        //        catch (JsonException jex)
        //        {

        //            jsonArgs.actingLog = "Error .. " + jex.Message;
        //            jsonArgs.fullfileName = f.FullName;
        //            jsonArgs.Readsuccessful = true;

        //            jsonLoging(jsonArgs);
        //            continue;

        //        }
        //    }//while


        //}
    }
}
