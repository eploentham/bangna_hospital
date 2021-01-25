using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                MessageBox.Show("333  AgeStringShortEng ", "");
                DateTime dtm = new DateTime();
                DateTime.TryParse(System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd"), out dtm);
                MessageBox.Show("dtB  "+ dtB.ToString() + "  dtm " + dtm.ToString(), "");
                //return "";


                age = new Age(dtB, dtm);
                //re = age.AgeString.Replace("Years", "Y").Replace("Year", "Y").Replace("Months", "M").Replace("Month", "M").Replace("Days", "D").Replace("Day", "D");

                //days = age.Days.Equals("1") ? "Day" : "Days";
                //months = age.Months.Equals("1") ? "Month" : "Months";
                //years = age.Years.Equals("1") ? "Year" : "Years";
                //re = age.Years + years + " " + age.Months + months + " " + age.Days+ days;
                re = age.AgeString;
            }
            return re;
        }
        public String AgeStringShortEngWindowsXP()
        {
            String re = "", days = "", months = "", years = "", dob="";
            DateTime dtB;
            //years = patient_birthday.Substring(0, 4);
            //if (int.Parse(years) > 2500)
            //{
            //    years = (int.Parse(years) - 543).ToString();
            //}
            //dob = years + patient_birthday.Substring(5);
            if (DateTime.TryParse(patient_birthday, out dtB))
            {
                MessageBox.Show("444  AgeStringShortEng ", "");
                dtB = dtB.AddYears(-543);
                DateTime dtm = new DateTime();
                DateTime.TryParse(System.DateTime.Now.ToString("yyyy-MM-dd"), out dtm);
                MessageBox.Show("888 dtB  " + dtB.ToString() + "  dtm " + dtm.ToString(), "");
                //return "";


                //age = new Age(dtB, dtm);
                age = new Age(dtB);
                //re = age.AgeString.Replace("Years", "Y").Replace("Year", "Y").Replace("Months", "M").Replace("Month", "M").Replace("Days", "D").Replace("Day", "D");

                //days = age.Days.Equals("1") ? "Day" : "Days";
                //months = age.Months.Equals("1") ? "Month" : "Months";
                //years = age.Years.Equals("1") ? "Year" : "Years";
                //re = age.Years + years + " " + age.Months + months + " " + age.Days+ days;
                re = age.AgeString;
            }
            return re;
        }
    }
}
