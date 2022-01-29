using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Screening : IComparable <Screening>
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
        public Screening(int sn, DateTime sdt, string st, Cinema c, Movie m)
        {
            ScreeningNo=sn;
            ScreeningDateTime=sdt;
            ScreeningType = st;
            Cinema = c;
            Movie = m;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public int CompareSeatsRemaining(Screening s)
        {
            if (SeatsRemaining < s.SeatsRemaining)
                return 1;
            else if (SeatsRemaining == s.SeatsRemaining)
                return 0;
            else
                return -1;
        }
    }
}
