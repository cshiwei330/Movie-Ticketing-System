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
            return base.CalculatePrice();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
