using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Student:Ticket
    {
        private string levelOfStudy;

        public string LevelOfStudy
        {
            get { return levelOfStudy; }
            set { levelOfStudy = value; }
        }
        public Student() : base() { }
        public Student(Screening s, string LevelOfStudy):base()
        {
            Screening = s;
            levelOfStudy = LevelOfStudy;
        }
        public override double CalculatePrice()
        {
            DateTime date = Screening.ScreeningDateTime;
            string screeningtype = Screening.ScreeningType;
            DateTime openingdate7 = Movie.OpeningDate.AddDays(7);
            string x;
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
            //Pricings based on Weekday/Weekend
            if (x == "weekday")
            {
                if (screeningtype == "2D")
                {
                    if (date <= openingdate7)
                    {
                        return 8.5;
                    }
                    else
                    {
                        return 7.0;
                    }
                }
                else
                {
                    if (date <= openingdate7)
                    {
                        return 11.0;
                    }
                    else
                    {
                        return 8.0;
                    }
                }
            }
            else
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
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}