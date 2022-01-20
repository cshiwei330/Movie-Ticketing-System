﻿using System;
using System.IO;
using System.Collections.Generic;

namespace PRG2_Assignment
{
    class Program
    {
        static int OrderNo = 1;
        public static int ScreeningNo = 1001;
        static void Main(string[] args)
        {
            //----------- Setting up lists -----------
            List<Cinema> cList = new List<Cinema>();
            List<Movie> mList = new List<Movie>();
            List<Screening> sList = new List<Screening>();

            //----------- Reading CSV & storing as objects + Populate lists -----------
            ReadCinema(cList);
            ReadMovie(mList);
            ReadScreening(sList, cList, mList);

            Boolean bookingover = false;

            while (bookingover == false)
            {
                DisplayMenu();
                Console.Write("Enter your option: ");
                string userOption = Console.ReadLine();

                if (userOption == "1") //display movies
                {
                    DisplayMovieDetails(mList);
                }

                else if (userOption == "2") //display movie screenings
                {
                    DisplayScreening(sList);
                }

                else if (userOption == "3") //add movie screening
                {
                    Console.WriteLine("waiting to implement heh");
                }

                else if (userOption == "4") //delete movie screening
                {
                    Console.WriteLine("waiting to implement heh");
                }

                else if (userOption == "5") //order movie tickets
                { 
                    Console.WriteLine("waiting to implement heh");
                }

                else if (userOption == "6") //cancel ticket
                {
                    Console.WriteLine("waiting to implement heh");
                }

                else if (userOption == "0") //exit
                {
                    Console.WriteLine("Thanks for using our Movie Ticket System! We hope to see you again :)");
                    bookingover = true;
                }

                else
                {
                    Console.WriteLine("Invalid choice.");
                    
                }

            }
        }

        //=====================================================  To be removed/might be useful  ===================================================

        //------------------- Display Screening (for checking) ------------------------------
        static void DisplayScreening(List<Screening> sList)  //to be deleted
        {
            Console.WriteLine("{0,-18}{1,-28}{2,-19}{3,-25}{4,-40}", "Screening No: ", "DateTime: ", "Screening Type: ", "Cinema Name: ", "Movie Title: ");
            foreach (Screening s in sList)
            {
                Console.WriteLine("{0,-18}{1,-28}{2,-19}{3,-25}{4,-40}", s.ScreeningNo, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Movie.Title);
            }
        }

        // ------------------- Displays cinema details (for checking) --------------------------------------------------
        static void DisplayCinema(List<Cinema> cList)
        {
            Console.WriteLine("{0,-18}{1,-15}{2,-10}", "Name", "Hall Number", "Capacity");
            foreach (Cinema c in cList)
            {
                Console.WriteLine("{0,-18}{1,-15}{2,-10}", c.Name, c.HallNo, c.Capacity);
            }
        }

        //=====================================================  General  ===================================================

        // ------------------- Display Main Menu -------------------
        static void DisplayMenu()
        {
            Console.WriteLine("Movie Tickting System" +
                "\n----------------------------" +
                "\n1. Display Movies" +
                "\n2. Display all Movies Screenings" +
                "\n3. Add Movie Screening" +
                "\n4. Delete Movie Screening" +
                "\n5. Order Movie Tickets" +
                "\n6. Cancel Ticket" +
                "\n0. Exit" +
                "\n----------------------------");
        }

        // ------------------- 1) Load Cinema Data & Populate Cinema List -----------------------------------------------------
        static void ReadCinema(List<Cinema> cList)
        {
            string[] cdata = File.ReadAllLines("Cinema.csv");
            for (int i = 1; i < cdata.Length; i++)
            {
                string[] cvalues = cdata[i].Split(",");
                cList.Add(new Cinema(cvalues[0], Convert.ToInt32(cvalues[1]), Convert.ToInt32(cvalues[2])));
            }
        }


        // ------------------- 1) Load Movie Data & Populate Movie List --------------------------------------------------------
        static void ReadMovie(List<Movie> mList)
        {
            string[] mdata = File.ReadAllLines("Movie.csv");
            for (int i = 1; i < mdata.Length; i++)
            {
                string[] mvalues = mdata[i].Split(",");
                string genreGiven = mvalues[2]; // ------------ stores the genre given in csv 

                static List<string> generateGenre(string genreGiven) //method to return genre list for movies obj
                {
                    List<string> genrelist = new List<string>(); // ------------ create a new string everytime 
                    string slash = "/";
                    Boolean sResult = genreGiven.Contains(slash);

                    if (sResult == true)
                    {
                        string[] genres = genreGiven.Split("/");
                        for (int j = 0; j < genres.Length; j++)
                        {
                            genrelist.Add(genres[j]); // ------------ add each genre into list
                        }
                    }
                    else
                    {
                        genrelist.Add(genreGiven);
                    }

                    return genrelist;
                }

                List<string> genreResults = generateGenre(genreGiven);

                Movie m = new Movie(mvalues[0], Convert.ToInt32(mvalues[1]), Convert.ToString(mvalues[3]), Convert.ToDateTime(mvalues[4]), genreResults);
                mList.Add(m);
            }
        }

        //------------------- 2) Load Screening Data & Populate Screening List --------------------------------------------------
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

                string movieName = svalues[4];
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
                Screening newscr = new Screening(ScreeningNo, Convert.ToDateTime(svalues[0]), svalues[1], result, result2);
                sList.Add(newscr);
                ScreeningNo++;
           
            }
        }


        // ------------------- 3) List all Movies Details -------------------
        static void DisplayMovieDetails(List<Movie> mList)
        {
            Console.WriteLine("{0,-35}{1,-23}{2,-20}{3,-27}{4,-25}", "Title", "Duration (mins)", "Classification", "Opening Date", "Genre");
            foreach (Movie m in mList)
            {
                Console.WriteLine("{0,-35}{1,-23}{2,-20}{3,-27}{4,-25}", m.Title, m.Duration, m.Classification, m.OpeningDate, m.genreList);
            }
        }


        // ------------------- 4) List Movie Screenings -------------------
        static void DisplayAllMovies(List<Movie> mList, List<Screening> sList)
        {
            Console.WriteLine("{0,5}{1,-35}", "", "Title");
            int count = 01;
            foreach (Movie m in mList)
            {
                Console.WriteLine("[" + count + "]  " + m.Title);
                count++;
            }

            Console.Write("\nSelect a Movie: ");
            int option = Convert.ToInt32(Console.ReadLine()); // --------- If chosen from Movie Number
            for (int i = 0; i < mList.Count; i++)
            {
                int k = option - 1;
                if (mList[k].Title == sList[k].Movie.Title)
                {
                    DisplayScreening(sList);
                }
            }

            //string option = Console.ReadLine(); // --------- If chosen from Movie Title
            //for (int i = 0; i < mList.Count; i++)
            //{
            //    if (option == mList[i].Title)
            //    {
            //        for (int k = 0; k < sList.Count; k++)
            //        {
            //            if (mList[i].Title == sList[k].Movie.Title)
            //            {
            //                DisplayScreening(sList);
            //            }
            //        }
            //    }
            //}
        }

        // ------------------- Add a Movie Screening Session -------------------
        static void AddScreeningSession(List<Movie> mList, List<Screening> sList, List<Cinema> cList)
        {
            DisplayAllMovies(mList, sList);
            Console.WriteLine("\nSelect a Movie: ");
            int option2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter screening type (2D/3D): ");
            string sType = Console.ReadLine();
            Console.WriteLine("Enter screening date and time: ");
            DateTime sdateTime = Convert.ToDateTime(Console.ReadLine());
            for (int i = 0; i < mList.Count; i++)
            {
                // Test if DateTime is after Opening date
            }

            DisplayCinema(cList);
        }


        // ------------------- Order Ticket/s -------------------
        static void OrderTicket(List<Movie> mList, List<Screening> sList)
        {
            DisplayAllMovies(mList, sList);
            Console.Write("\nSelect a Movie Screening: ");
        }
    }
}
