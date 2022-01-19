using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Screening
    {
        private int screeningno;
        public int ScreeningNo { get; set; }

        private DateTime screeningDateTime;
        public DateTime ScreeningDateTime { get; set; }
        
        private string screeningType;
        public string ScreeningType { get; set; }

        private int seatsRemaining; 
        public int SeatsRemaining { get; set; }

        private Cinema cinema;
        public Cinema Cinema { get; set; }

        private Movie movie;
        public Movie Movie { get; set; }

        public Screening() { }
        public Screening(int sno, DateTime sdtime, string stype, Cinema c, Movie m)
        {
            ScreeningNo = sno;
            ScreeningDateTime = sdtime;
            ScreeningType = stype;
            Cinema = c;
            Movie = m;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
