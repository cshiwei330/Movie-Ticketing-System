using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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
                Console.WriteLine("\n"); //better formatting
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
                    OrderTicket(mList, sList, cList, tList);
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

        //*****************************************************  BASICS  **********************************************************

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
        // ------------------- Display Cinemas -------------------
        static void DisplayCinema(List<Cinema> cList)
        {
            Console.WriteLine("     {0,-18}{1,-15}{2,-10}", "Name", "Hall Number", "Capacity");
            int count = 1;
            foreach (Cinema c in cList)
            {
                if (count <= 9)
                {
                    Console.WriteLine("[" + count + "]" + "  {0,-18}{1,-15}{2,-10}", c.Name, c.HallNo, c.Capacity);
                    count++;
                }

                else
                {
                    Console.WriteLine("[" + count + "]" + " {0,-18}{1,-15}{2,-10}", c.Name, c.HallNo, c.Capacity);
                    count++;
                }
            }
        }

        // ------------------- Integer Validation -------------------
        static bool BetweenRange (int min, int max, int option)
        {
            return (option <= max && option >= min);
        }

        //=====================================================  General  ===================================================

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
                m.AddGenre(genreGiven);
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
                int hallNo = Convert.ToInt32(svalues[3]);
                static Cinema CinemaSearch(List<Cinema> cList, string cinemaName, int hallNo)
                {
                    for (int i = 0; i < cList.Count; i++)
                    {
                        Cinema c = cList[i];
                        if (cinemaName == c.Name && hallNo == c.HallNo)
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
                Cinema result = CinemaSearch(cList, cinemaName, hallNo);

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
                newscr.SeatsRemaining = result.Capacity;
                sList.Add(newscr);
                ScreeningNo++;

            }
        }


        // ------------------- 3) List all Movies Details -------------------
        static void DisplayMovieDetails(List<Movie> mList)
        {
            Console.WriteLine("     {0,-35}{1,-23}{2,-20}{3,-27}{4,-25}", "Title", "Duration (mins)", "Classification", "Opening Date", "Genre");
            int count = 1;
            foreach (Movie m in mList)
            {
                string genres = null;
                for (int g = 0; g < m.genreList.Count; g++)
                {
                    genres = m.genreList[g];
                }

                if (count <= 9)
                {
                    Console.WriteLine("[" + count + "]" + "  {0,-35}{1,-23}{2,-20}{3,-27}{4,-25}", m.Title, m.Duration, m.Classification, m.OpeningDate, genres);
                    count++;
                }

                else
                {
                    Console.WriteLine("[" + count + "]" + " {0,-35}{1,-23}{2,-20}{3,-27}{4,-25}", m.Title, m.Duration, m.Classification, m.OpeningDate, genres);
                    count++;
                }
            }
        }


        // ------------------- 4) List Movie Screenings -------------------
        static void ListMovieScreenings(List<Movie> mList, List<Screening> sList)
        {
            //1. list all movies 
            DisplayMovieDetails(mList);

            //2.prompt user to select a movie
            Console.Write("\nPlease select a Movie: "); //******need validations
            int movieOption = Convert.ToInt32(Console.ReadLine());

            //3. retreive movie object
            Movie m = mList[movieOption - 1];

            //4. retrieve and display screening sessions for that movie
            Console.WriteLine("\n{0}{1,-18}{2,-28}{3,-19}{4,-25}{5,-40}", "", "Screening No: ", "DateTime: ", "Screening Type: ", "Cinema Name: ", "Hall Number: ");
            int count2 = 01;
            for (int s = 0; s < sList.Count; s++)
            {
                Screening screen = sList[s];
                if (screen.Movie == m)
                {
                    Console.WriteLine("{0,-18}{1,-28}{2,-19}{3,-25}{4,-40}", screen.ScreeningNo, screen.ScreeningDateTime, screen.ScreeningType, screen.Cinema.Name, screen.Cinema.HallNo);
                    count2++;
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
            DisplayMovieDetails(mList);

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
            bool success = false; //for 6. 
            if (movie.OpeningDate < newSDateTime)
            {
                //5. list all cinema halls
                DisplayCinema(cList);

                //6. prompt user to select a cinema hall (check to see if the cinema hall is available at the datetime entered in point 4)
                //[need to consider the movie duration and cleaning time]
                Console.Write("Select a Cinema & Hall: ");
                int cinemaOption = Convert.ToInt32(Console.ReadLine());
                Cinema cinema = cList[cinemaOption - 1];

                for (int j = 0; j < sList.Count; j++)
                {
                    Screening screening = sList[j];
                    if (screening.Cinema == cinema && screening.ScreeningDateTime.Date == newSDateTime.Date)  //find that day n the cinema hall 
                    {
                        DateTime screeningtime = screening.ScreeningDateTime;
                        DateTime blockoff = screeningtime.AddMinutes(Convert.ToDouble(screening.Movie.Duration + 30)); //30mins for cleaning, newSDateTime must be after 
                        DateTime blockoff2 = screeningtime.AddMinutes(-Convert.ToDouble(movie.Duration + 30)); //movie must end before next screening 

                        Console.WriteLine(blockoff2);
                        Console.WriteLine(blockoff);

                        if (newSDateTime < blockoff2 || newSDateTime > blockoff) // if newSDateTime is valid
                        {
                            success = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    else
                    {
                        continue;
                    }
                }

                //7. create a Screening object with the information given and add to the relevantscreening list
                if (success == true)
                {
                    Screening newS = new Screening(ScreeningNo, newSDateTime, sType, cinema, movie);
                    sList.Add(newS);
                    ScreeningNo++;
                    movie.AddScreening(newS);
                    Console.WriteLine("Screening created successfully!");
                }

                else if (success == false)
                {
                    Console.WriteLine("Your selected cinema hall is unavailable to screen at the screening date and time you want.");
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
        static void OrderTicket(List<Movie> mList, List<Screening> sList, List<Cinema> cList, List<Ticket> tList)
        {
            //1.list all movies
            //2. prompt user to select a movie
            //3.list all movie screenings of the selected movie
            ListMovieScreenings(mList, sList);

            //4. prompt user to select movie screening
            Console.Write("\nSelect a Movie Screening: ");
            int screeningOption = Convert.ToInt32(Console.ReadLine());

            Screening findScreening = null;
            //5. retrieve the selected movie screening
            for (int i=0;i<sList.Count;i++)
            {
                Screening s = sList[i];
                if (s.ScreeningNo==screeningOption)
                {
                    findScreening = s;
                }
                else
                {
                    continue;
                }
            }

            //6. prompt user to enter the total number of tickets to order
            Console.Write("Enter number of tickets to order: ");
            int toOrder = Convert.ToInt32(Console.ReadLine());

            if (toOrder > findScreening.SeatsRemaining)           //check if figure entered is more than the available seats for the screening
            {
                Console.WriteLine("Insufficient number of available seats for {0} people.", toOrder);
            }
            else
            {
                //7. prompt user if all ticket holders meet the movie classification requirements 
                Movie z = findScreening.Movie;
                for (int j = 1; j <= toOrder; j++)
                {
                    Console.WriteLine(z.Classification);
                    Console.WriteLine("Seats initially: "+findScreening.SeatsRemaining);
                    if (z.Classification == "PG13")
                    {
                        Console.WriteLine("Is ticket holder {0} above the age of 13?[Y/N] : ", j);
                        string metRequirements = Console.ReadLine().ToUpper();
                        if (metRequirements == "Y")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Unable to purchase ticket as the minimum age requirement of 13 is not met.");
                            break;
                        }
                    }
                    else if (z.Classification == "M18")
                    {
                        Console.WriteLine("Is ticket holder {0} above the age of 13?[Y/N] : ", j);
                        string metRequirements = Console.ReadLine().ToUpper();
                        if (metRequirements == "Y")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Unable to purchase ticket as the minimum age requirement of 18 is not met.");
                            break;
                        }
                    }
                    else if (z.Classification == "R21")
                    {
                        Console.WriteLine("Is ticket holder {0} above the age of 13?[Y/N] : ", j);
                        string metRequirements = Console.ReadLine().ToUpper();
                        if (metRequirements == "Y")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Unable to purchase ticket as the minimum age requirement of 21 is not met.");
                            break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                double totalPrice = 0;
                //8. create an Order object with the status “Unpaid”
                Order newOrder = new Order(OrderNo, DateTime.Now);
                newOrder.Status = "Unpaid";
                //9.
                for (int k = 1; k <= toOrder; k++)
                {
                    Console.Write("\n[1] Student\n[2] Adult\n[3] Senior Citizen");
                    Console.Write("\nEnter the type of ticket to purchase (Student/Adult/Senior Citizen): ");
                    int ticketType = Convert.ToInt32(Console.ReadLine());
                    //a. prompt user for a response depending on the type of ticket ordered:
                    //b. create a Ticket object (Student, SeniorCitizen or Adult) with the information given
                    //d. update seats remaining for the movie screening
                    if (ticketType == 1)
                    {
                        Console.WriteLine("Enter your level of study [Primary, Secondary, Tertiary]: ");
                        string levelOfStudy = Console.ReadLine();
                        Ticket t = new Student(findScreening, levelOfStudy);
                        newOrder.AddTicket(t);
                        totalPrice += t.CalculatePrice();
                        findScreening.SeatsRemaining--;
                    }
                    else if (ticketType == 3)
                    {
                        Console.Write("Enter your year of birth: ");
                        int yearOfBirth = Convert.ToInt32(Console.ReadLine());
                        int accepted = DateTime.Now.Year - 55;
                        if (yearOfBirth < accepted)
                        {
                            Ticket t = new SeniorCitizen(findScreening, yearOfBirth);
                            newOrder.AddTicket(t);
                            totalPrice += t.CalculatePrice();
                            findScreening.SeatsRemaining--;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Would you like to purchase a popcorn set for $3?[Y/N]: ");
                        string pOffer = Console.ReadLine().ToUpper();
                        if (pOffer == "Y")
                        {
                            bool popcornOffer = true;
                            Ticket t = new Adult(findScreening, popcornOffer);
                            newOrder.AddTicket(t);
                            totalPrice += t.CalculatePrice();
                        }
                        else
                        {
                            bool popcornOffer = false;
                            Ticket t = new Adult(findScreening, popcornOffer);
                            newOrder.AddTicket(t);
                            totalPrice += t.CalculatePrice();
                        }
                        findScreening.SeatsRemaining--;
                    }
                }

                //10. list amount payable
                if (totalPrice>0)
                {
                    Console.WriteLine("Total Amount Payable: ${0:c2}", totalPrice);
                    //11. prompt user to press any key to make payment
                    Console.Write("Press any key to make payment ");
                    Console.ReadKey();

                    //12. fill in the necessary details to the new order (e.g amount)
                    newOrder.Amount = totalPrice;
                    //13. change order status to “Paid”
                    newOrder.Status = "Paid";
                }
                else
                {
                    Console.WriteLine("Failed ticket purchase.");
                }
            }
        } 

        // ------------------- 8) Cancel order of ticket -------------------
    }
}