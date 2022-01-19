using System;
using System.IO;
using System.Collections.Generic;

namespace PRG2_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cinema> cList = new List<Cinema>();
            List<Movie> mList = new List<Movie>();
            List<Screening> sList = new List<Screening>();
            ReadCinema(cList);
            DisplayCinema(cList);
            ReadMovie(mList);
            DisplayMovie(mList);
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
                mList.Add(new Movie(mvalues[0],Convert.ToInt32(mvalues[1]),mvalues[2],Convert.ToString(mvalues[3]),Convert.ToDateTime(mvalues[4])));
            }
        }
        static void DisplayMovie(List<Movie> mList)  //to be deleted
        {
            foreach (Movie m in mList)
            {
                Console.WriteLine("{0,-40}{1,-7}{2,40}{3,-8}{4,-12}", m.Title, m.Duration, m.Genre, m.Classification, m.OpeningDate);
            }
        }

        // ------------------- Load Screening Data -------------------

    }
}
