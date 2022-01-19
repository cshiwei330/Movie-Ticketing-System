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
        public Student():base()
        {
            LevelOfStudy = "Tertiary";
        }
        public Student(string l):base()
        {

        }
        public override double CalculatePrice()
        {
            return base.CalculatePrice();
        }
    }
}