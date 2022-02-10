﻿using System;
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
        public String patient_birthday = "", addr="";
        public Age age = new Age(DateTime.Now);

        public String MNC_HN_NO { get; set; }
        public String MNC_HN_YR { get; set; }
        public String MNC_PFIX_CDT { get; set; }
        public String MNC_PFIX_CDE { get; set; }
        public String MNC_FNAME_T { get; set; }
        public String MNC_LNAME_T { get; set; }
        public String MNC_FNAME_E { get; set; }
        public String MNC_LNAME_E { get; set; }
        public String MNC_AGE { get; set; }
        public String MNC_BDAY { get; set; }
        public String MNC_ID_NO { get; set; }
        public String MNC_SS_NO { get; set; }
        public String MNC_SEX { get; set; }
        public String MNC_FULL_ADD { get; set; }
        public String MNC_STAMP_DAT { get; set; }
        public String MNC_STAMP_TIM { get; set; }
        public String MNC_NAT_CD { get; set; }
        public String MNC_CUR_ADD { get; set; }
        public String MNC_CUR_TUM { get; set; }
        public String MNC_CUR_AMP { get; set; }
        public String MNC_CUR_CHW { get; set; }
        public String MNC_CUR_POC { get; set; }
        public String MNC_CUR_TEL { get; set; }
        public String MNC_DOM_ADD { get; set; }
        public String MNC_DOM_TUM { get; set; }
        public String MNC_DOM_AMP { get; set; }
        public String MNC_DOM_CHW { get; set; }
        public String MNC_DOM_POC { get; set; }
        public String MNC_DOM_TEL { get; set; }
        public String MNC_REF_NAME { get; set; }
        public String MNC_REF_REL { get; set; }
        public String MNC_REF_ADD { get; set; }
        public String MNC_REF_TUM { get; set; }
        public String MNC_REF_AMP { get; set; }
        public String MNC_REF_CHW { get; set; }
        public String MNC_REF_POC { get; set; }
        public String MNC_REF_TEL { get; set; }
        public String MNC_CUR_MOO { get; set; }
        public String MNC_DOM_MOO { get; set; }
        public String MNC_REF_MOO { get; set; }
        public String MNC_REG_DAT { get; set; }
        public String MNC_REG_TIM { get; set; }
        public String MNC_CUR_SOI { get; set; }
        public String MNC_DOM_SOI { get; set; }
        public String MNC_REF_SOI { get; set; }
        public String MNC_FN_TYP_CD { get; set; }
        public String MNC_ATT_NOTE { get; set; }
        public String MNC_OCC_CD { get; set; }
        public String MNC_EDU_CD { get; set; }
        //public String MNC_NAT_CD { get; set; }
        public String MNC_REL_CD { get; set; }
        public String MNC_NATI_CD { get; set; }
        public String MNC_CAR_CD { get; set; }
        public String MNC_CUR_ROAD { get; set; }
        public String req_no { get; set; }
        public String req_date { get; set; }
        public String MNC_COM_CD { get; set; }
        public String MNC_COM_CD2 { get; set; }
        public String passport { get; set; }
        public String filename { get; set; }
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

                //MessageBox.Show("444  AgeStringShortEng ", "");
                //dtB = dtB.AddYears(-543);
                if (dtB.Year < 2000)
                {
                    dtB = dtB.AddYears(543);
                }
                DateTime dtm = new DateTime();
                DateTime.TryParse(System.DateTime.Now.ToString("yyyy-MM-dd"), out dtm);
                //MessageBox.Show("888 dtB  " + dtB.ToString() + "  dtm " + dtm.ToString(), "");
                //return "";

                new LogWriter("d", "AgeStringShortEngWindowsXP patient_birthday " + patient_birthday + " dob " + dtB.ToString());
                //age = new Age(dtB, dtm);
                age = new Age(dtB);
                //re = age.AgeString.Replace("Years", "Y").Replace("Year", "Y").Replace("Months", "M").Replace("Month", "M").Replace("Days", "D").Replace("Day", "D");
                //new LogWriter("d", "AgeStringShortEngWindowsXP patient_birthday " + patient_birthday + " dob " + dtm.ToString()+"  "+);
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
