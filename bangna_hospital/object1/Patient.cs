using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class Patient:Persistent
    {
        public String Hn = "", Name = "", Age = "", vn="", preno="", visitDate="", an="", anDate="", statusIPD="", anCnt="", dob="", idcard="", fname="", lname="", title="", hnyr="";
        public String patient_birthday = "";
        public Age age = new Age(DateTime.Now);
        public String AgeString()
        {
            String re = "";
            DateTime dtB;
            if (DateTime.TryParse(patient_birthday, out dtB))
            {
                //age = new Age(dtB);
                age = new Age(dtB);
                re = age.AgeString;
            }
            return re;
        }
        public String AgeStringShort()
        {
            String re = "";
            DateTime dtB;
            if (DateTime.TryParse(patient_birthday, out dtB))
            {
                age = new Age(dtB);
                //re = age.AgeString.Replace("Years", "Y").Replace("Year", "Y").Replace("Months", "M").Replace("Month", "M").Replace("Days", "D").Replace("Day", "D");
                re = age.Years + "." + age.Months + "." + age.Days;
            }
            return re;
        }
        public String AgeStringShort1()
        {
            String re = "";
            DateTime dtB;
            if (DateTime.TryParse(patient_birthday, out dtB))
            {
                DateTime dtm = new DateTime();
                DateTime.TryParse(System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd"), out dtm);
                age = new Age(dtB, dtm);
                //re = age.AgeString.Replace("Years", "Y").Replace("Year", "Y").Replace("Months", "M").Replace("Month", "M").Replace("Days", "D").Replace("Day", "D");
                re = age.Years + "." + age.Months + "." + age.Days;
            }
            return re;
        }
        public String AgeStringShortEng()
        {
            String re = "", days="", months="", years="";
            DateTime dtB;
            if (DateTime.TryParse(patient_birthday, out dtB))
            {
                DateTime dtm = new DateTime();
                DateTime.TryParse(System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd"), out dtm);
                age = new Age(dtB, dtm);
                //re = age.AgeString.Replace("Years", "Y").Replace("Year", "Y").Replace("Months", "M").Replace("Month", "M").Replace("Days", "D").Replace("Day", "D");
                
                days = age.Days.Equals("1") ? "Day" : "Days";
                months = age.Months.Equals("1") ? "Month" : "Months";
                years = age.Years.Equals("1") ? "Year" : "Years";
                re = age.Years + years + " " + age.Months + months + " " + age.Days+ days;
            }
            return re;
        }
    }
}
