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
        public Movie (string Title, int Duration, string Classification, DateTime OpeningDate, List<string> gList)
        {
            title = Title;
            duration=Duration;
            classification=Classification;
            openingDate=OpeningDate;
            genreList=gList;
 
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
            return "Title:" + Title +
                  "\tDuration:" + Duration +
                  "\tClassification:" + Classification +
                  "\tOpeningDate:" + OpeningDate +
                  "\tGenre:" + "replacepls";
        }
    }
}
