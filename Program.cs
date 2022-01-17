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
        }


        // ------------------- Load Cinema Data -------------------
        static void ReadCinema(List<Cinema> cList)
        {
            string[] data = File.ReadAllLines("Cinema.csv");
            for (int i = 1; i < data.Length; i++)
            {
                string[] values = data[i].Split(",");
                cList.Add(new Cinema(values[0], Convert.ToInt32(values[1]), Convert.ToInt32(values[2])));
            }
        }
        static void DisplayCinema(List<Cinema> cList)
        {
            Console.WriteLine("{0,-18}{1,-15}{2,-10}", "Name", "Hall Number", "Capacity");
            foreach (Cinema c in cList)
            {
                Console.WriteLine("{0,-18}{1,-15}{2,-10}", c.Name, c.HallNo, c.Capacity);
            }
        }
    }
}
