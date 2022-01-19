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

            string x; //------ x will represent either weekday or weekend ------
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

            //------ based on weekday/weekend: ------
            double price;
            if (x == "weekday") //if weekday
            {
                if (screeningtype == "2D")
                {
                    price= 8.5;
                }
                else
                {  
                    price= 11.0;
  
                }
            }
            else //------ if weekend ------
            {
                if (screeningtype == "2D")
                {
                    price = 12.5;
                }
                else
                {
                    price = 14.0;
                }
            }
            if (popcornOffer==true)
            {
                return price+3.0;
            }
            else
            {
                return price;
            }
        }
        public override string ToString()
        {
            return base.ToString()+"\tPopcorn Offer: "+PopcornOffer;
        }
    }
}
