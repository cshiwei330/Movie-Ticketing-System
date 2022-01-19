using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class SeniorCitizen:Ticket
    {
        private int yearOfBirth;

        public int YearOfBirth
        {
            get { return yearOfBirth; }
            set { yearOfBirth = value; }
        }
        public SeniorCitizen() : base() { }
        public SeniorCitizen(Screening s, int YearOfBirth)
        {
            Screening = s;
            yearOfBirth = YearOfBirth;
        }
        public override double CalculatePrice()
        {
            DateTime date = Screening.ScreeningDateTime;
            string screeningtype = Screening.ScreeningType;
            DateTime openingdate7 = Movie.OpeningDate.AddDays(7);

            string x; // x will represent either weekday or weekend
            if (date.DayOfWeek == DayOfWeek.Friday)
            {
                x = "weekend";
            }
            else if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                x = "weekend";
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                x = "weekend";
            }
            else 
            {
                x = "weekday";
            }

            //based on weekday/weekend:
            if (x == "weekday") //if weekday
            {
                if (screeningtype == "2D")
                {
                    if (date <= openingdate7)
                    {
                        return 8.5;
                    }

                    else
                        return 5.0;
                }
                else
                {
                    if (date <= openingdate7)
                    {
                        return 11.0;
                    }

                    else
                        return 6.0;
                }   
            }

            else //if weekend
            {
                if (screeningtype == "2D")
                {
                    return 12.5;
                }
                else
                {
                    return 14.0;
                }
            }


            //if (date <= dt7) //if within first 7 days od movie opening
            //{
            //    // all adult price 
            //    if(screeningtype == "2D")
            //    {
            //        return ;
            //    }

            //    else
            //        return 6.0;
            //}

            //if (date.DayOfWeek==DayOfWeek.Monday)
            //{
            //    if (screeningtype == "2D")
            //    {
            //        return 5.0;
            //    }

            //    else
            //        return 6.0;
            //}

            //else if (date.DayOfWeek == DayOfWeek.Tuesday)
            //{
            //    if (screeningtype == "2D")
            //    {
            //        return 5.0;
            //    }

            //    else
            //        return 6.0;
            //}

            //else if (date.DayOfWeek == DayOfWeek.Wednesday)
            //{
            //    if (screeningtype == "2D")
            //    {
            //        return 5.0;
            //    }

            //    else
            //        return 6.0;
            //}

            //else if (date.DayOfWeek == DayOfWeek.Thursday)
            //{
            //    if (screeningtype == "2D")
            //    {
            //        return 5.0;
            //    }

            //    else
            //        return 6.0;
            //}

            //else
            //{
            //    if (screeningtype == "2D")
            //    {
            //        return 12.5;
            //    }

            //    else
            //        return 14.0;
            //}
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
