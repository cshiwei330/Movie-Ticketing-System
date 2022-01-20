using System;
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
            List<Ticket> tList = new List<Ticket>();

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
                    ListMovieScreenings(mList, sList);
                }

                else if (userOption == "3") //add movie screening
                {
                    AddScreeningSession(mList, sList, cList);
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
                "\n1. View All Movies" +
                "\n2. View Available Screening for Movie" +
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
        static void ListMovieScreenings(List<Movie> mList, List<Screening> sList)
        {
            //1. list all movies 
            Console.WriteLine("\n{0,5}{1,-35}", "", "Title");
            int count = 01;
            for (int x = 0; x <mList.Count; x++)
            {
                Movie movie = mList[x];
                Console.WriteLine("[" + count + "]" + "\t" + movie.Title);
                count++;
            }

            //2.prompt user to select a movie
            Console.Write("\nPlease select a Movie: "); //******need validations
            int movieOption = Convert.ToInt32(Console.ReadLine());

            //3. retreive movie object
            Movie m = mList[movieOption - 1];

            //4. retrieve and display screening sessions for that movie
            Console.WriteLine("\n{0,-18}{1,-28}{2,-19}{3,-25}{4,-40}", "Screening No: ", "DateTime: ", "Screening Type: ", "Cinema Name: ", "Movie Title: ");
            for (int s = 0; s < sList.Count; s++)
            {
                Screening screen = sList[s];
                if (screen.Movie == m)
                {
                    Console.WriteLine("{0,-18}{1,-28}{2,-19}{3,-25}{4,-40}", screen.ScreeningNo, screen.ScreeningDateTime, screen.ScreeningType, screen.Cinema.Name, screen.Movie.Title);
                }
                else
                {
                    continue;
                }
            }


        }

        //=====================================================  Screening  ===================================================

        // ------------------- 5) Add a Movie Screening Session -------------------
        static void AddScreeningSession(List<Movie> mList, List<Screening> sList, List<Cinema> cList)
        {
            //1. list all movies 
            int count = 01;

            foreach (Movie m in mList)
            {
                Console.WriteLine("[" + count + "]" + "{0,-35}{1,-23}{2,-20}{3,-27}{4,-25}", m.Title, m.Duration, m.Classification, m.OpeningDate, m.genreList);
                count++;
            }

            //2. prompt user to select a movie
            Console.Write("\nSelect a Movie: "); //******need validations
            int movieOption2 = Convert.ToInt32(Console.ReadLine());
            Movie movie = mList[movieOption2 - 1]; //get movie obj

            //3. prompt user to enter a screening type 
            Console.Write("Enter screening type (2D/3D): "); //******need validations
            string sType = Console.ReadLine();

            //4. prompt user to enter a screening date and time (check to see if the datetime entered is after the opening date of the movie)
            Console.Write("Enter screening date and time: ");
            DateTime newSDateTime = Convert.ToDateTime(Console.ReadLine()); //******need validations

            int count2 = 01; 
            if (movie.OpeningDate < newSDateTime)
            {
                //5. list all cinema halls
                foreach (Cinema c in cList)
                {
                    Console.WriteLine("[" + count2 + "]" + "{0,-18}{1,-15}{2,-10}", c.Name, c.HallNo, c.Capacity);
                    count2++;
                }

                //6. prompt user to select a cinema hall (check to see if the cinema hall is available at the datetime entered in point 4)
                //[need to consider the movie duration and cleaning time]
                Console.Write("Select a Cinema & Hall: ");
                int cinemaOption = Convert.ToInt32(Console.ReadLine());
                Cinema cinema = cList[cinemaOption - 1];

                bool success = true;
                for (int j = 0; j < sList.Count; j++)  
                {
                    Screening screening = sList[j];
                    if (screening.Cinema == cinema)/*&& screening.ScreeningDateTime.Date == newSDateTime.Date)*/ //find that day n the cinema hall 
                    {
                        DateTime screeningtime = screening.ScreeningDateTime;
                        DateTime blockoff = screeningtime.AddMinutes(Convert.ToDouble(movie.Duration + 30)); //30mins for cleaning
                        Console.WriteLine("yo");

                        if (newSDateTime!< screeningtime && newSDateTime!> blockoff)
                        {
                            success = false;
                            Console.WriteLine("hey");
                            break;
                        }
                    }

                    else
                    {
                        Console.WriteLine("huh");
                    }
                }

                //7. 7. create a Screening object with the information given and add to the relevantscreening list
                if (success == true)
                {
                    Screening newS = new Screening(ScreeningNo, newSDateTime, sType, cinema, movie);
                    sList.Add(newS);
                    ScreeningNo++;
                    movie.AddScreening(newS);
                    Console.WriteLine("Screening created successfully!");
                }

                else;
                {
                    Console.WriteLine("Your selected cinema hall is not unavailable to screen at the screening date and time you want.");
                }
            }

            else
            {
                Console.WriteLine("Screening date and time must be after opening date");
            }

        }

        // ------------------- 6) Delete a Movie Screening Session -------------------


        //=====================================================  Order  ===================================================

        // ------------------- 7) Order Ticket/s -------------------
        static void OrderTicket(List<Movie> mList, List<Screening> sList, List<Cinema> cList,List<Ticket> tList)
        {
            ListMovieScreenings(mList, sList);
            Console.WriteLine("Select a movie screening: ");
            int screeningOption = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < sList.Count; i++)
            {
                Screening screen = sList[i];
                if (screeningOption==screen.ScreeningNo)
                {
                    continue;
                }
            }

            Console.WriteLine("Enter number of tickets to order: ");
            int toOrder=Convert.ToInt32(Console.ReadLine());
            for (int i=0;i<cList.Count;i++)
            {
                Screening screen = sList[i];
                cList[i].Capacity = screen.SeatsRemaining;
                if (toOrder > screen.SeatsRemaining)
                {
                    Console.WriteLine("Insufficient number of available seats for {0} people.", toOrder);
                }
                else
                {
                    continue;
                }
            }
            
            for (int i = 0; i < toOrder; i++)
            {
                Movie m = mList[i];
                if (m.Classification == "PG13")
                {
                    Console.WriteLine("Are all ticket holders above the age of 13?[Y/N] : ");
                    string metRequirements = Console.ReadLine();
                }
                else if (m.Classification == "M18")
                {
                    Console.WriteLine("Are all ticket holders above the age of 18?[Y/N] : ");
                    string metRequirements = Console.ReadLine();
                }
                else if (m.Classification == "R21")
                {
                    Console.WriteLine("Are all ticket holders above the age of 21?[Y/N] : ");
                    string metRequirements = Console.ReadLine();
                }
            }

            Order newOrder = new Order(OrderNo, DateTime.Now);
            newOrder.Status = "Unpaid";
            for (int i = 0; i < toOrder; i++)
            {
                Ticket t = tList[i];
                if ()
                {
                    Console.WriteLine("Enter your level of study [Primary, Secondary, Tertiary]: ");
                    string levelOfStudy = Console.ReadLine();
                }
                else if ()
                {
                    Console.WriteLine("Enter your year of birth: ");
                    int yearOfBirth = Convert.ToInt32(Console.ReadLine());
                    int age = DateTime.Now.Year - yearOfBirth;
                }
                else
                {
                    Console.WriteLine("Would you like to purchase a popcorn set for $3?[Y/N]: ");
                    string pOffer = Console.ReadLine();
                    if (pOffer == "Y")
                    {
                        bool popcornOffer = true;
                    }
                    else
                    {
                        bool popcornOffer = false;
                    }
                }

                tList.Add(new Student());
            }
        }
    }
}