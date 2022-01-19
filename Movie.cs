using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Movie
    {
        private string title;
        public string Title { get; set; }

        private int duration;
        public int Duration { get; set; }

        private string classification;
        public string Classification { get; set; }

        private DateTime openingDate;
        public DateTime OpeningDate { get; set; }

        public List<string> genreList { get; set; } = new List<string>();
        public List<Screening> screeningList { get; set; } = new List<Screening>();

        public Movie() { }
        public Movie (string tit, int dur, string cla, DateTime opn, List<string> gList)
        {
            Title = tit;
            Duration = dur;
            Classification = cla;
            OpeningDate = opn;
            //genreList = gList;
 
        }

        public void AddGenre(string genre)
        {
            genreList.Add(genre);
        }

        public void AddScreening(Screening s)
        {
            screeningList.Add(s);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
