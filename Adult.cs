using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Adult:Ticket
    {
        private bool popcornOffer;

        public bool PopcornOffer
        {
            get { return popcornOffer; }
            set { popcornOffer = value; }
        }
        public Adult() : base() { }
        public Adult(Screening s, bool PopcornOffer)
        {
            Screening = s;
            popcornOffer = PopcornOffer;
        }
        public override double CalculatePrice()
        {
            DateTime date = Screening.ScreeningDateTime;
            string screeningtype = Screening.ScreeningType;

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
                    return 8.5;
                }
                else
                {  
                    return 11.0;
  
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
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
