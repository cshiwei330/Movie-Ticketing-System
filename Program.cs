using System;
using System.IO;
using System.Collections.Generic;

namespace PRG2_Assignment
{
    class Program
    {
        static int OrderNo = 1;
        static int ScreeningNo = 1001;
        static void Main(string[] args)
        {
            List<Cinema> cList = new List<Cinema>();
            List<Movie> mList = new List<Movie>();
            List<Screening> sList = new List<Screening>();
            //ReadCinema(cList);
            //DisplayCinema(cList);
            //ReadMovie(mList);
            //DisplayMovie(mList);
            ReadScreening(sList, cList, mList);
            DisplayScreening(sList);
        }


        // ------------------- Load Cinema Data -------------------
        static void ReadCinema(List<Cinema> cList)
        {
            string[] cdata = File.ReadAllLines("Cinema.csv");
            for (int i = 1; i < cdata.Length; i++)
            {
                string[] cvalues = cdata[i].Split(",");
                cList.Add(new Cinema(cvalues[0], Convert.ToInt32(cvalues[1]), Convert.ToInt32(cvalues[2])));
            }
        }
        static void DisplayCinema(List<Cinema> cList) //to be deleted
        {
            Console.WriteLine("{0,-18}{1,-15}{2,-10}", "Name", "Hall Number", "Capacity");
            foreach (Cinema c in cList)
            {
                Console.WriteLine("{0,-18}{1,-15}{2,-10}", c.Name, c.HallNo, c.Capacity);
            }
        }

        // ------------------- Load Movie Data -------------------
        static void ReadMovie(List<Movie> mList)
        {
            string[] mdata = File.ReadAllLines("Movie.csv");
            for (int i = 1; i < mdata.Length; i++)
            {
                string[] mvalues = mdata[i].Split(",");
                mList.Add(new Movie(mvalues[0],Convert.ToInt32(mvalues[1]),Convert.ToString(mvalues[3]), Convert.ToDateTime(mvalues[4]),mvalues[2]));
            }
        }
        static void DisplayMovie(List<Movie> mList)  //to be deleted
        {
            Console.WriteLine("{0,-40}{1,-7}{2,40}{3,-8}{4,-12}", "Title", "Duration (mins)", "Genre", "Classification", "Opening Date");
            foreach (Movie m in mList)
            {
                Console.WriteLine("{0,-40}{1,-7}{2,40}{3,-8}{4,-12}", m.Title, m.Duration, m.genreList, m.Classification, m.OpeningDate);
            }
        }

        // ------------------- Load Screening Data -------------------
        static void ReadScreening(List<Screening> sList, List<Cinema> cList, List<Movie> mList)
        {
            string[] sdata = File.ReadAllLines("Screening.csv");
            for (int i = 1; i < sdata.Length; i++)
            {
                string[] svalues = sdata[i].Split(",");
                string cinemaName = svalues[2];
                static Cinema CinemaSearch(List<Cinema> cList, string cinemaName)
                {
                    for (int i = 0; i < cList.Count; i++)
                    {
                        Cinema c = cList[i];
                        if (cinemaName == c.Name)
                        {
                            return c;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return null;
                }
                Cinema result = CinemaSearch(cList, cinemaName);

                string movieName = svalues[3];
                static Movie MovieSearch(List<Movie> mList, string movieName)
                {
                    for (int a = 0; a < mList.Count; a++)
                    {
                        Movie m = mList[a];
                        if (movieName == m.Title)
                        {
                            return m;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return null;
                }
                Movie result2 = MovieSearch(mList, movieName);
                sList.Add(new Screening(ScreeningNo, Convert.ToDateTime(svalues[0]), svalues[1], result,result2));
            }
        }
        static void DisplayScreening(List<Screening> sList)  //to be deleted
        {
            foreach (Screening s in sList)
            {
                Console.WriteLine("{0,-30}{1,-20}{2,-6}{3,-30}{4,-40}", s.ScreeningNo, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Movie.Title);
            }
        }
    }
}
