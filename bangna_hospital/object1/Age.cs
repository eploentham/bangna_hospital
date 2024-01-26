/**
* Calculate Age in C#
* https://gist.github.com/faisalman
*
* Copyright 2012-2013, Faisalman <fyzlman@gmail.com>
* Licensed under The MIT License
* http://www.opensource.org/licenses/mit-license
*/
using System;
namespace bangna_hospital.object1
{
    public class Age
    {
        public int Years;
        public int Months;
        public int Days;

        public Age(DateTime Bday)
        {
            this.Count(Bday);
        }

        public Age(DateTime Bday, DateTime Cday)
        {
            if (Bday.Year > 2100)
            {
                return;
            }
            this.Count(Bday, Cday);
        }

        public Age Count(DateTime Bday)
        {
            //int yeardob = 0, year = 0;
            //DateTime dt = DateTime.Today;
            //yeardob = Bday.Year;
            //year = DateTime.Today.Year;
            //if (yeardob < 1500)
            //{
            //    Bday.AddYears(543);
            //}
            return this.Count(Bday, DateTime.Today);
        }
        public Age Count(DateTime Bday, DateTime Cday)
        {
            if ((Cday.Year - Bday.Year) > 0 ||
                (((Cday.Year - Bday.Year) == 0) && ((Bday.Month < Cday.Month) ||
                    ((Bday.Month == Cday.Month) && (Bday.Day <= Cday.Day)))))
            {
                int DaysInBdayMonth = DateTime.DaysInMonth(Bday.Year, Bday.Month);
                int DaysRemain = Cday.Day + (DaysInBdayMonth - Bday.Day);

                if (Cday.Month > Bday.Month)
                {
                    this.Years = Cday.Year - Bday.Year;
                    this.Months = Cday.Month - (Bday.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
                else if (Cday.Month == Bday.Month)
                {
                    if (Cday.Day >= Bday.Day)
                    {
                        this.Years = Cday.Year - Bday.Year;
                        this.Months = 0;
                        this.Days = Cday.Day - Bday.Day;
                    }
                    else
                    {
                        this.Years = (Cday.Year - 1) - Bday.Year;
                        this.Months = 11;
                        this.Days = DateTime.DaysInMonth(Bday.Year, Bday.Month) - (Bday.Day - Cday.Day);
                    }
                }
                else if (Cday.Month < Bday.Month)
                {
                    this.Years = (Cday.Year - 1) - Bday.Year;
                    this.Months = Cday.Month + (11 - Bday.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                    //this.Years = Cday.Year - Bday.Year;
                    //this.Months = Bday.Month - (Cday.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    //this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
                else
                {
                    //this.Years = (Cday.Year - 1) - Bday.Year;
                    //this.Months = Cday.Month + (11 - Bday.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    //this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                    this.Years = Cday.Year - Bday.Year;
                    this.Months = Bday.Month - (Cday.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
            }
            else
            {
                throw new ArgumentException("Birthday date must be earlier than current date");
            }
            return this;
        }
        public string AgeString
        {
            get
            {
                string ageString = string.Empty;

                if (Years >= 1)
                {
                    if (Years == 1)
                    {
                        ageString = string.Format("{0} Year", Years);
                    }
                    else
                    {
                        ageString = string.Format("{0} Years", Years);
                    }

                    if (Months >= 1)
                    {
                        if (Months == 1)
                        {
                            ageString += string.Format(" {0} Month", Months);
                        }
                        else
                        {
                            ageString += string.Format(" {0} Months", Months);
                        }

                        if (Days >= 1)
                        {
                            if (Days == 1)
                            {
                                ageString += string.Format(" {0} Day", Days);
                            }
                            else
                            {
                                ageString += string.Format(" {0} Days", Days);
                            }
                        }
                    }
                }
                return ageString;
            }
        }
        public string AgeString1
        {
            get
            {
                string ageString = string.Empty;

                if (Years >= 1)
                {
                    if (Years == 1)
                    {
                        ageString = string.Format("{0}Y", Years);
                    }
                    else
                    {
                        ageString = string.Format("{0}Y", Years);
                    }

                    if (Months >= 1)
                    {
                        if (Months == 1)
                        {
                            ageString += string.Format(" {0}M", Months);
                        }
                        else
                        {
                            ageString += string.Format(" {0}M", Months);
                        }

                        if (Days >= 1)
                        {
                            if (Days == 1)
                            {
                                ageString += string.Format(" {0}D", Days);
                            }
                            else
                            {
                                ageString += string.Format(" {0}D", Days);
                            }
                        }
                    }
                }
                else
                {
                    if (Months >= 1)
                    {
                        if (Months == 1)
                        {
                            ageString += string.Format(" {0}M", Months);
                        }
                        else
                        {
                            ageString += string.Format(" {0}M", Months);
                        }

                        if (Days >= 1)
                        {
                            if (Days == 1)
                            {
                                ageString += string.Format(" {0}D", Days);
                            }
                            else
                            {
                                ageString += string.Format(" {0}D", Days);
                            }
                        }
                    }
                }
                return ageString;
            }
        }
        public string AgeStringDOT
        {
            get
            {
                string ageString = string.Empty;

                if (Years >= 1)
                {
                    if (Years == 1)
                    {
                        ageString = string.Format("{0}.", Years);
                    }
                    else
                    {
                        ageString = string.Format("{0}.", Years);
                    }

                    if (Months >= 1)
                    {
                        if (Months == 1)
                        {
                            ageString += string.Format(" {0}.", Months);
                        }
                        else
                        {
                            ageString += string.Format(" {0}.", Months);
                        }
                    }
                    else if (Months == 0)
                    {
                        ageString += string.Format(" {0}.", Months);
                    }
                        if (Days >= 1)
                    {
                        if (Days == 1)
                        {
                            ageString += string.Format(" {0}", Days);
                        }
                        else
                        {
                            ageString += string.Format(" {0}", Days);
                        }
                    }
                }
                else
                {
                    if (Months >= 1)
                    {
                        if (Months == 1)
                        {
                            ageString += string.Format(" {0}.", Months);
                        }
                        else
                        {
                            ageString += string.Format(" {0}.", Months);
                        }

                        if (Days >= 1)
                        {
                            if (Days == 1)
                            {
                                ageString += string.Format(" {0}", Days);
                            }
                            else
                            {
                                ageString += string.Format(" {0}", Days);
                            }
                        }
                    }
                }
                return ageString;
            }
        }
    }

}




/**
 * Usage example:
 * ==============
 * DateTime bday = new DateTime(1987, 11, 27);
 * DateTime cday = DateTime.Today;
 * Age age = new Age(bday, cday);
 * Console.WriteLine("It's been {0} years, {1} months, and {2} days since your birthday", age.Year, age.Month, age.Day);
 */