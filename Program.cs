//============================================================
// Student Number : S10221902D, S10221849H
// Student Name : Alethea Chan, Chew Shi Wei
// Module Group : P10
//============================================================

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
            List<Order> oList = new List<Order>();

            bool loadedMnC = false;
            bool loadedS = false;

            while (!loadedMnC) // user can only select option 1; loading of movie n cinema data 
            {
                DisplayMenu();
                Console.Write("Enter your option: ");
                string userOption = Console.ReadLine();
                if (userOption == "1") // load movie and cinema data
                {
                    if (File.Exists("Movie.csv") && File.Exists("Cinema.csv")) // if file exists [validation]
                    {
                        ReadMovie(mList);
                        ReadCinema(cList);
                        Console.WriteLine("Loading of Movie and Cinema Data completed.\n");
                        loadedMnC = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please make sure that relevant files exists.");
                    }

                }
                else
                {
                    Console.WriteLine("Please load Movie and Cinema Data first.\n");
                }
            }

            while (!loadedS) // after loading movie n cinema data, user can view list of movies (option3) 
            {                                                                    // but not anything else 
                DisplayMenu();                                             
                Console.Write("Enter your option: ");
                string userOption = Console.ReadLine();
                if (userOption == "1") //load movie and cinema data
                {
                    Console.WriteLine("You have already loaded Movie and Cinema Data.\n");
                }
                else if (userOption == "2") //load screening data
                {
                    ReadScreening(sList, cList, mList);
                    Console.WriteLine("Loading of Screening Data completed.\n");
                    loadedS = true;
                    break;
                }

                else if (userOption == "3") //display movies
                {
                    DisplayMovieDetails(mList);
                    Console.WriteLine("\n"); 
                }
                else
                {
                    Console.WriteLine("Please Screening Data first.\n");
                }
            }

            // once user has loaded all data:
            bool bookingover = false;
            while (!bookingover)
            {
                DisplayMenu();
                Console.Write("Enter your option: ");
                string userOption = Console.ReadLine();
                if (userOption == "1") //load movie and cinema data
                {
                    Console.WriteLine("You have already loaded Movie and Cinema Data.\n");
                }
                else if (userOption == "2") //load screening data
                {
                    Console.WriteLine("You have already loaded Screening Data.\n");
                }
                else if (userOption == "3") //display movies
                {
                    DisplayMovieDetails(mList);
                    Console.WriteLine("\n"); 
                }
                else if (userOption == "4") //display movie screenings
                {
                    ListMovieScreenings(mList, sList);
                    Console.WriteLine("\n");
                }

                else if (userOption == "5") //add movie screening
                {
                    AddScreeningSession(mList, sList, cList);
                    Console.WriteLine("\n");
                }

                else if (userOption == "6") //delete movie screening
                {
                    DeleteScreeningSession(oList, sList);
                    Console.WriteLine("\n");
                }

                else if (userOption == "7") //order movie tickets
                {
                    OrderTicket(mList, sList, oList);
                    Console.WriteLine("\n");
                }

                else if (userOption == "8") //cancel ticket
                {
                    CancelOrder(oList);
                    Console.WriteLine("\n");
                }

                else if (userOption == "9") //recommended movies based on sales 
                {
                    RecommendMovies(mList, oList);
                    Console.WriteLine("\n");
                }

                else if (userOption == "10") //available seats of screening session
                {
                    DisplayAvailableSeats(sList);
                    Console.WriteLine("\n");
                }

                else if (userOption == "11") //top 3 movies based on number of tickets sold 
                {
                    DisplayTop3(mList, oList);
                    Console.WriteLine("\n");
                }

                else if (userOption == "12") //sales by cinema 
                {
                    SalesByCinema(cList, oList);
                    Console.WriteLine("\n");
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

            //*****************************************************  BASICS  **********************************************************

            // ------------------- Display Main Menu -------------------
            static void DisplayMenu()
            {
                Console.WriteLine("Movie Tickting System" +
                    "\n----------------------------" +
                    "\n1.  Load Movie and Cinema Data" +
                    "\n2.  Load Screening Data" +
                    "\n3.  View All Movies" +
                    "\n4.  View Available Screening for Movie" +
                    "\n5.  Add Movie Screening" +
                    "\n6.  Delete Movie Screening" +
                    "\n7.  Order Movie Tickets" +
                    "\n8.  Cancel Ticket" +
                    "\n9.  Recommended Movies Based on Sales" +
                    "\n10. Available Seats in Screening Sessions" +
                    "\n11. Top 3 Movies Based on Number of Tickets Sold " +
                    "\n12. Sales by Cinema" +
                    "\n0.  Exit" +
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
            //=====================================================  General  ===================================================

            // ------------------- 1) Load Cinema Data & Populate Cinema List -----------------------------------------------------
            static void ReadCinema(List<Cinema> cList)
            {
                string[] cdata = File.ReadAllLines("Cinema.csv");  // read cinema csv into string array
                for (int i = 1; i < cdata.Length; i++)
                {
                    string[] cvalues = cdata[i].Split(",");  // split the data in csv by commas

                    if (cvalues.Length == 3) // [validation of data set]
                    {
                        cList.Add(new Cinema(cvalues[0], Convert.ToInt32(cvalues[1]), Convert.ToInt32(cvalues[2]))); // convert to respective datatypes
                    }

                    else
                    {
                        continue;
                    }
                }
            }

            // ------------------- 1) Load Movie Data & Populate Movie List --------------------------------------------------------
            static void ReadMovie(List<Movie> mList)
            {
                string[] mdata = File.ReadAllLines("Movie.csv"); // read movie csv into string array
                for (int i = 1; i < mdata.Length; i++)
                {
                    string[] mvalues = mdata[i].Split(",");  // split the data in csv by commas
                    if (mvalues.Length == 5) // [validation of data set]
                    {
                        string genreGiven = mvalues[2]; // ------------ stores the genre given in csv 

                        static List<string> generateGenre(string genreGiven) //method to return genre list for movies obj
                        {
                            List<string> genrelist = new List<string>(); // ------------ create a new string everytime 
                            string slash = "/";
                            bool sResult = genreGiven.Contains(slash);

                            if (sResult == true)
                            {
                                string[] genres = genreGiven.Split("/");  // split the genres by slash
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

                        Movie m = new Movie(mvalues[0], Convert.ToInt32(mvalues[1]), Convert.ToString(mvalues[3]), Convert.ToDateTime(mvalues[4]), genreResults); // convert to respective datatypes
                        mList.Add(m);  // add movie object into movie list
                        m.AddGenre(genreGiven);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            //------------------- 2) Load Screening Data & Populate Screening List --------------------------------------------------
            static void ReadScreening(List<Screening> sList, List<Cinema> cList, List<Movie> mList)
            {
                string[] sdata = File.ReadAllLines("Screening.csv");  // read screening csv into string array
                for (int i = 1; i < sdata.Length; i++)
                {
                    string[] svalues = sdata[i].Split(","); // split data in csv by commas
                    if (svalues.Length == 5)  // [validation of data set]
                    {
                        string cinemaName = svalues[2];
                        int hallNo = Convert.ToInt32(svalues[3]);
                        static Cinema CinemaSearch(List<Cinema> cList, string cinemaName, int hallNo)
                        {
                            for (int i = 0; i < cList.Count; i++)
                            {
                                Cinema c = cList[i];
                                if (cinemaName == c.Name && hallNo == c.HallNo) // search for cinema with matching cinema name+hallno
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
                                if (movieName == m.Title)  // search for movie with the same title
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
                        newscr.SeatsRemaining = result.Capacity;  // update seats remaining of the new screening with the cinema hall capacity
                        sList.Add(newscr);  // add new screening to screening list
                        result2.AddScreening(newscr);
                        ScreeningNo++; // increase screening number by 1
                    }

                    else
                    {
                        continue; 
                    }
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

                    if (count <= 9) //formatting 
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
            static Movie ListMovieScreenings(List<Movie> mList, List<Screening> sList)
            {
                //1. list all movies 
                DisplayMovieDetails(mList);

                //2.prompt user to select a movie
                bool notValid = true; //[validation]
                Movie m = null;
                while (notValid)
                {
                    try
                    {
                        Console.Write("\nPlease select a Movie: ");
                        int movieOption = Convert.ToInt32(Console.ReadLine());

                        //3. retreive movie object
                        m = mList[movieOption - 1];
                        notValid = false;
                    }
                    catch
                    {
                        Console.WriteLine("Invalid choice. PLease enter the integer number next to the movie you want.");
                    }
                }

                //4. retrieve and display screening sessions for that movie
                if (m.screeningList.Count == 0)
                {
                    Console.WriteLine("This movie does not have any screenings yet.");
                }
                else
                {
                    Console.WriteLine("\n{0,-18}{1,-28}{2,-19}{3,-22}{4,-17}{5,-20}", "Screening No: ", "DateTime: ", "Screening Type: ", "Cinema Name: ", "Hall Number: ", "Seats Remaining: ");
                    foreach (Screening s in m.screeningList)
                    {
                        Console.WriteLine("{0,-18}{1,-28}{2,-19}{3,-22}{4,-17}{5,-20}", s.ScreeningNo, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Cinema.HallNo, s.SeatsRemaining);
                    }
                }
                return m;
            }

            //=====================================================  Screening  ===================================================

            // ------------------- 5) Add a Movie Screening Session -------------------
            static void AddScreeningSession(List<Movie> mList, List<Screening> sList, List<Cinema> cList)
            {
                //1. list all movies 
                DisplayMovieDetails(mList);

                //2. prompt user to select a movie
                Movie movie = null;
                bool validMovieOption = false; //[validation]
                while (!validMovieOption)
                {
                    try
                    {
                        Console.Write("\nSelect a Movie: ");
                        int movieOption2 = Convert.ToInt32(Console.ReadLine());
                        movie = mList[movieOption2 - 1]; //get movie obj
                        validMovieOption = true;
                    }

                    catch
                    {
                        Console.WriteLine("Invalid choice. PLease enter the integer number next to the movie you want.");
                    }
                }

                //3. prompt user to enter a screening type 
                string sType = null;
                bool validScreeningType = false; //[validation]
                while (!validScreeningType)
                {
                    Console.Write("\nEnter screening type (2D/3D): ");
                    sType = Console.ReadLine().ToUpper();

                    if (sType == "2D" || sType == "3D")
                    {
                        validScreeningType = true;
                    }

                    else
                    {
                        Console.WriteLine("Invalid Screening Type. Please try again.");
                    }
                }

                //4. prompt user to enter a screening date and time (check to see if the datetime entered is after the opening date of the movie)
                string temp = null; //[validation]
                DateTime newSDateTime = Convert.ToDateTime("01/01/2022 00:00:00"); //assign newSDateTime to a random datetime first
                bool validnewSDateTime = false;
                while (!validnewSDateTime)
                {
                    Console.Write("\nEnter screening date and time: ");
                    temp = Console.ReadLine();

                    if (DateTime.TryParse(temp, out newSDateTime))  //if valid datetime 
                    {
                        if (newSDateTime < DateTime.Now)  //new screening datetime cannot be in the past 
                        {
                            Console.WriteLine("Screening date and time must be after now. " + "(" + Convert.ToString(DateTime.Now) + ")");
                            continue;
                        }
                        validnewSDateTime = true;
                    }

                    else
                    {
                        Console.WriteLine("Incorrect formatting of screening date and time. Please try again.");
                    }
                }

                bool success = false; //for 6. 
                if (movie.OpeningDate < newSDateTime) //check if new screening datetime is after openingdate of movie 
                {
                    //5. list all cinema halls
                    DisplayCinema(cList);

                    //6. prompt user to select a cinema hall (check to see if the cinema hall is available at the datetime entered in point 4)
                    //[need to consider the movie duration and cleaning time]

                    Cinema cinema = null;
                    bool validCinema = false; //[validation]
                    while (!validCinema)
                    {
                        try
                        {
                            Console.Write("Select a Cinema & Hall: ");
                            int cinemaOption = Convert.ToInt32(Console.ReadLine());
                            cinema = cList[cinemaOption - 1];
                            validCinema = true;
                        }

                        catch
                        {
                            Console.WriteLine("Invalid choice. Please enter the number next to the cinema you want.");
                        }
                    }


                    //List will contain all the existing screening dates
                    List<DateTime> sDates = new List<DateTime>();
                    for (int j = 0; j < sList.Count; j++)
                    {
                        Screening s = sList[j];

                        if (sDates.Contains(s.ScreeningDateTime.Date)) //avoid adding duplicates
                        {
                            continue;
                        }
                        else
                        {
                            sDates.Add(s.ScreeningDateTime.Date);
                        }
                    }

                    bool dateExists = false;
                    for (int d = 0; d < sDates.Count; d++)
                    {
                        if (newSDateTime.Date == sDates[d])
                        {
                            dateExists = true;
                        }
                    }

                    //List will contain all the existing cinema halls
                    List<Cinema> cinemasInSList = new List<Cinema>();
                    for (int j = 0; j < sList.Count; j++)
                    {
                        Screening s = sList[j];

                        if (cinemasInSList.Contains(s.Cinema)) //avoid adding duplicates
                        {
                            continue;
                        }
                        else
                        {
                            cinemasInSList.Add(s.Cinema);
                        }

                    }

                    bool cinemaExists = false;
                    for (int c = 0; c < cinemasInSList.Count; c++)
                    {
                        if (cinema == cinemasInSList[c])
                        {
                            cinemaExists = true;
                        }
                    }

                    if (dateExists == true && cinemaExists == true)
                    {

                        for (int j = 0; j < sList.Count; j++)
                        {
                            Screening screening = sList[j];
                            if (screening.Cinema == cinema && screening.ScreeningDateTime.Date == newSDateTime.Date)  //find that day n the cinema hall 
                            {
                                DateTime screeningtime = screening.ScreeningDateTime;
                                DateTime blockoff = screeningtime.AddMinutes(Convert.ToDouble(screening.Movie.Duration + 30)); //30mins for cleaning, newSDateTime must be after 
                                DateTime blockoff2 = screeningtime.AddMinutes(-Convert.ToDouble(movie.Duration + 30)); //movie must end before next screening 

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
                    }

                    else
                    {
                        success = true;
                    }

                    //7. create a Screening object with the information given and add to the relevant screening list
                    if (success == true)
                    {
                        Screening newS = new Screening(ScreeningNo, newSDateTime, sType, cinema, movie);
                        newS.SeatsRemaining = cinema.Capacity; //assign seats remaining 
                        sList.Add(newS);
                        movie.AddScreening(newS);
                        ScreeningNo++;
                        Console.WriteLine("Screening created successfully!");
                    }

                    else 
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
            static void DeleteScreeningSession(List<Order> oList, List<Screening> sList)
            {
                //1. list all movie screening sessions that have not sold any tickets 
                List<int> ticketsSold = new List<int>(); //contains screenings that has tickets 
                for (int o = 0; o < oList.Count; o++)
                {
                    Order order = oList[o];
                    if (ticketsSold.Contains(order.TList[0].Screening.ScreeningNo)) //avoid adding duplicates
                    {
                        continue;
                    }
                    else
                    {
                        ticketsSold.Add(order.TList[0].Screening.ScreeningNo); //add screening number to list
                    }
                }

                List<int> noTicketsSold = new List<int>(); 
                foreach (Screening s in sList)
                {
                    noTicketsSold.Add(s.ScreeningNo); //add all screening numbers into list
                }

                foreach (int i in ticketsSold)
                {
                    noTicketsSold.Remove(i); //remove screening numbers that has order
                                             //noTicketsSold will be left with screening numbers that has no order
                }

                Console.WriteLine("\n{0,-18}{1,-28}{2,-19}{3,-22}{4,-17}{5,-20}", "Screening No: ", "DateTime: ", "Screening Type: ", "Cinema Name: ", "Hall Number: ");
                for (int s = 0; s < sList.Count; s++)
                {
                    Screening screening = sList[s];
                    for (int i = 0; i < noTicketsSold.Count; i++)
                    {
                        if (screening.ScreeningNo == noTicketsSold[i])
                        {
                            Console.WriteLine("{0,-18}{1,-28}{2,-19}{3,-22}{4,-17}{5,-20}", screening.ScreeningNo, screening.ScreeningDateTime, screening.ScreeningType, screening.Cinema.Name, screening.Cinema.HallNo, screening.SeatsRemaining);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                //2. prompt user to select a session
                bool validScreeningNo = false;
                int screeningOption = 0;

                while (!validScreeningNo) //[validation]
                {
                    try
                    {
                        Console.Write("\nSelect a Movie Screening: ");
                        screeningOption = Convert.ToInt32(Console.ReadLine());

                        if (noTicketsSold.Contains(screeningOption))
                        {
                            validScreeningNo = true;
                        }
                        else
                        {
                            bool withinRange = false;
                            for (int n = 0; n < sList.Count; n++)
                            {
                                if (sList[n].ScreeningNo == screeningOption)
                                {
                                    Console.WriteLine("Invalid choice. Unable to remove a screening that contains orders. ");
                                    withinRange = true;
                                }

                                else
                                    continue;
                            }

                            if (!withinRange)
                            {
                                Console.WriteLine("The screening number you have entered in not in our records. Please try again.");
                            }

                        }
                    }

                    catch
                    {
                        Console.WriteLine("Invalid choice. Please enter the number next to the Screening you want");
                    }
                }


                bool success = false;
                //3. delete the selected movie screening from sList
                for (int i = 0; i < sList.Count; i++)
                {
                    Screening s = sList[i];
                    if (s.ScreeningNo == screeningOption)
                    {
                        sList.Remove(s);
                        s.Movie.screeningList.Remove(s); //remove screening from the screeningList in movie obj
                        success = true;
                        Console.WriteLine("Sucessful. Screening {0} was removed!", s.ScreeningNo);
                    }
                    else
                    {
                        continue;
                    }
                }

                //4.display the status of the removal(i.e.successful or unsuccessful)
                if (success == false)
                {
                    Console.WriteLine("Unsucessful. Please try again.");
                }
            }

            //=====================================================  Order  ===================================================

            // ------------------- 7) Order Ticket/s -------------------
            static void OrderTicket(List<Movie> mList, List<Screening> sList, List<Order> oList)
            {
                //1.list all movies
                //2.prompt user to select a movie
                //3.list all movie screenings of the selected movie
                Movie m = ListMovieScreenings(mList, sList);   // display all screenings available for the selected movie

                if (m.screeningList.Count != 0)
                {
                    //4. prompt user to select movie screening
                    bool validMovieScreening = false;
                    int screeningOption = 0;

                    while (!validMovieScreening)
                    {
                        try
                        {
                            Console.Write("\nSelect a Movie Screening: ");
                            screeningOption = Convert.ToInt32(Console.ReadLine());

                            for (int s = 0; s < m.screeningList.Count; s++)
                            {
                                if (m.screeningList[s].ScreeningNo == screeningOption)  // test if screening number exists
                                    validMovieScreening = true;
                                else
                                    continue;
                            }

                            if (validMovieScreening == false)   // reject if screening number for another movie is given
                            {
                                Console.WriteLine("Invalid choice. The screening number that you have entered is not for the movie you chose.");
                            }
                        }
                        catch   // reject if screening number does not exist
                        {
                            Console.WriteLine("Invalid choice. Please enter the screening number of the screening you want.");
                        }
                    }

                    Screening findScreening = null;
                    //5. retrieve the selected movie screening
                    for (int i = 0; i < sList.Count; i++)
                    {
                        Screening s = sList[i];
                        if (s.ScreeningNo == screeningOption)
                        {
                            findScreening = s;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    //6.prompt user to enter the total number of tickets to order
                    bool validtoOrder = false;
                    int toOrder = 0;

                    while (!validtoOrder)
                    {
                        try
                        {
                            Console.Write("Enter number of tickets to order: ");
                            toOrder = Convert.ToInt32(Console.ReadLine());

                            if (toOrder <= findScreening.SeatsRemaining) //check if figure entered is more than the available seats for the screening
                            {
                                validtoOrder = true;
                            }
                            else  // reject if tickets to order>available
                            {
                                Console.WriteLine("Insufficient number of available seats for {0} people.", toOrder);
                            }
                        }
                        catch   // reject if the input is not a number
                        {
                            Console.WriteLine("Invalid input. Number of tickets should be a number.");
                        }
                    }

                    //7. prompt user if all ticket holders meet the movie classification requirements 
                    Movie z = findScreening.Movie;
                    bool meetrq = false;  // true if buyers meet age requirement to prompt for ticket type to purchase

                    string[] ages = { "13", "18", "21" };   // ages to meet for the different movie classifications
                    List<string> ageList = new List<string>();
                    ageList.AddRange(ages);
                    bool valid = false;

                    while (!valid)
                    {
                        string age = null;
                        if (z.Classification == "PG13")
                        {
                            age = ageList[0];
                        }
                        else if (z.Classification == "M18")
                        {
                            age = ageList[1];
                        }
                        else if (z.Classification == "R21")
                        {
                            age = ageList[2];
                        }
                        else
                        {
                            meetrq = true;
                            break;
                        }

                        Console.Write("Are all ticket holders above the age of {0}?[Y/N] : ", age);  // prompt if all buyers meet the age requirement
                        string metRequirements = Console.ReadLine().ToUpper();  // change all inputs to caps, for validation
                        if (metRequirements == "N" || metRequirements == "Y")
                        {
                            valid = true;
                            if (metRequirements == "N")  // reject as age requirement not met
                            {
                                Console.WriteLine("Unable to purchase ticket as the minimum age requirement of {0} is not met.", age);
                            }
                            else
                            {
                                meetrq = true;
                            }
                        }

                        else  // reject if Y/N not entered
                        {
                            Console.WriteLine("Please enter either Y or N.\n");
                        }
                    }

                    //9.
                    if (meetrq == true)
                    {
                        //8. create an Order object with the status “Unpaid”
                        Order newOrder = new Order(OrderNo, DateTime.Now);
                        newOrder.Status = "Unpaid";
                        double totalPrice = 0;

                        for (int k = 1; k <= toOrder; k++) // loop as many times as the number of tickets to be bought
                        {
                            bool validTicketType = false;
                            while (!validTicketType)
                            {
                                Console.Write("\n[1] Student\n[2] Adult\n[3] Senior Citizen");
                                Console.Write("\nEnter the type of ticket to purchase for person {0} (Student/Adult/Senior Citizen): ", k);
                                string ticketType = Console.ReadLine();
                                //a. prompt user for a response depending on the type of ticket ordered:
                                //b. create a Ticket object (Student, SeniorCitizen or Adult) with the information given
                                //d. update seats remaining for the movie screening
                                if (ticketType == "1")
                                {
                                    validTicketType = true;
                                    bool validLevelOfStudy = false;
                                    while (!validLevelOfStudy)
                                    {
                                        Console.Write("Enter your level of study [Primary, Secondary, Tertiary]: ");
                                        string levelOfStudy = Console.ReadLine().ToLower();  // change all inputs to lower, for validation

                                        if (levelOfStudy == "primary" || levelOfStudy == "secondary" || levelOfStudy == "tertiary")
                                        {
                                            validLevelOfStudy = true;
                                            Ticket t = new Student(findScreening, levelOfStudy);   // new ticket object & add to order
                                            newOrder.AddTicket(t);
                                            totalPrice += t.CalculatePrice();
                                            findScreening.SeatsRemaining--;  // update seats remaining
                                        }

                                        else  // if input isn't what is accepted
                                        {
                                            Console.WriteLine("Invalid input. Please check your spelling.");
                                        }
                                    }
                                }
                                else if (ticketType == "2")
                                {
                                    validTicketType = true;
                                    bool validpOffer = false;
                                    while (!validpOffer)
                                    {
                                        Console.Write("Would you like to purchase a popcorn set for $3?[Y/N]: ");
                                        string pOffer = Console.ReadLine().ToUpper(); // change all inputs to caps, for validation

                                        if (pOffer == "Y" || pOffer == "N")
                                        {
                                            validpOffer = true;
                                            findScreening.SeatsRemaining--; // update seats remaining

                                            if (pOffer == "Y")
                                            {
                                                bool popcornOffer = true;
                                                Ticket t = new Adult(findScreening, popcornOffer); // new adult object & add to order
                                                newOrder.AddTicket(t);
                                                totalPrice += t.CalculatePrice(); // with popcorn, +$3 to original adult ticket price
                                            }
                                            else
                                            {
                                                bool popcornOffer = false;
                                                Ticket t = new Adult(findScreening, popcornOffer); // new adult object & add to order
                                                newOrder.AddTicket(t);
                                                totalPrice += t.CalculatePrice();
                                            }
                                        }
                                        else  // if input isn't what is accepted
                                        {
                                            Console.WriteLine("Invalid input. Please enter either Y or N.");
                                        }
                                    }
                                }

                                else if (ticketType == "3")
                                {
                                    bool validYearOfBirth = false;
                                    while (!validYearOfBirth)
                                    {
                                        try
                                        {
                                            Console.Write("Enter your year of birth: ");
                                            int yearOfBirth = Convert.ToInt32(Console.ReadLine());
                                            int accepted = DateTime.Now.Year - 55;
                                            if (yearOfBirth < accepted)  // test if age is 55 and above
                                            {
                                                Ticket t = new SeniorCitizen(findScreening, yearOfBirth);
                                                newOrder.AddTicket(t);             // new senior citizen object & add to order
                                                totalPrice += t.CalculatePrice();
                                                findScreening.SeatsRemaining--;   // update seat remaining
                                                validYearOfBirth = true;
                                                validTicketType = true;
                                            }
                                            else if (yearOfBirth > DateTime.Now.Year)  // birth year must be 2022 and before
                                            {
                                                Console.WriteLine("Invalid input. Year of birth cannot be in the future.");
                                            }
                                            else   // age must be >=55
                                            {
                                                Console.WriteLine("You need to be above age 55 to be considered Senior Citizen");
                                                validYearOfBirth = true;
                                            }
                                        }
                                        catch  // reject if input is invalid
                                        {
                                            Console.WriteLine("Invalid input. Please enter a valid year.");
                                        }
                                    }
                                }

                                else  // if input more than number of choices given
                                {
                                    Console.WriteLine("Please enter the number next to ticket type. ");
                                }
                            }

                        }
                        OrderNo++;  // increase order number by 1

                        //10. list amount payable
                        if (totalPrice > 0)
                        {
                            Console.WriteLine("Total Amount Payable: ${0:c2}", totalPrice);
                            //11. prompt user to press any key to make payment
                            Console.Write("\nPress any key to make payment ");
                            Console.ReadKey();

                            //12. fill in the necessary details to the new order (e.g amount)
                            newOrder.Amount = totalPrice;
                            //13. change order status to “Paid”
                            newOrder.Status = "Paid";
                            oList.Add(newOrder);
                            //totalSold.Add(new Tuple<string, double>(newOrder.TList[0].Screening.Movie.Title, newOrder.Amount));
                            Console.WriteLine("\nOrder successful. Your order number is {0}.\n**Please note it down as it will be needed if you request for cancellation of order.", newOrder.orderNo);
                        }
                        else  // if total price is not greater than $0
                        {
                            Console.WriteLine("\nOrder unsuccessful, please try again.");
                        }
                    }
                }
            }

            // ------------------- 8) Cancel order of ticket -------------------
            static void CancelOrder(List<Order> oList)
            {
                //2. retrieve the selected order
                Order findOrderNo = null;
                bool orderNoFound = false;

                if (oList.Count!=0)
                {
                    while (!orderNoFound)
                    {
                        try
                        {
                            //1. prompt user for order number
                            Console.Write("Enter your order number: ");
                            int userOrderNo = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < oList.Count; i++)
                            {
                                Order o = oList[i];
                                if (o.orderNo == userOrderNo)
                                {
                                    findOrderNo = o;
                                    orderNoFound = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            if (findOrderNo == null)  // if order number does not exist
                            {
                                Console.WriteLine("The order number you have keyed in is not in our records. Please try again.");
                            }
                        }
                        catch    // if input is not numbers
                        {
                            Console.WriteLine("Invalid input. Order Number should only contain numbers.");
                        }
                    }

                    //3.check if the screening in the selected order is screened
                    if (DateTime.Now > findOrderNo.TList[0].Screening.ScreeningDateTime)
                    {
                        Console.WriteLine("\nRequest to cancel order denied. The movie has already been screened.");
                        //7. display the status of the cancelation (i.e. successful or unsuccessful)
                        Console.WriteLine("Order cancellation was unsuccessful.");
                    }
                    else
                    {
                        //4. update seat remaining for the movie screening based on the selected order
                        int seatsRemaining = findOrderNo.TList.Count;
                        findOrderNo.TList[0].SeatsRemaining += seatsRemaining;

                        //5.change order status to “Cancelled”
                        findOrderNo.Status = "Cancelled";
                        //6.display a message indicating that the amount is refunded
                        Console.WriteLine("\n${0:c2} has been refunded.", findOrderNo.Amount);
                        Console.WriteLine("Order cancelled successfully.");
                        oList.Remove(findOrderNo);   // remove order form order list
                    }
                }
                Console.WriteLine("There are no orders available to be cancelled.");
            }

            //*****************************************************  ADVANCED  **********************************************************

            // ------------------- 3.1) Recommend movies based on sales of tickets sold -------------------
            static void RecommendMovies(List<Movie> mList, List<Order> oList)
            {
                if (oList.Count != 0)
                {
                    List<Tuple<string, double>> totalSold = new List<Tuple<string, double>>();  // create list storing tuples that contain movie name+sales amount
                    double totalCosts = 0;
                    for (int x = 0; x < mList.Count; x++)
                    {
                        Movie m = mList[x];
                        foreach (Order o in oList)
                        {
                            if (o.TList[0].Screening.Movie.Title == m.Title)     // find movie titles from orders
                            {
                                totalCosts += o.Amount;
                            }
                        }
                        totalSold.Add(new Tuple<string, double>(m.Title, totalCosts));     // add movie title and total amount sold into a list
                        totalCosts = 0;
                    }
                    totalSold.Sort((a, b) => a.Item2.CompareTo(b.Item2));   // Sort based on sales amount
                    totalSold.Reverse();     // Highest sales first

                    Console.WriteLine("\nList of Recommended Movies");
                    int n = 1;
                    Console.WriteLine("{0,-3} {1,-35} {2,-6}", "  ", "Title", "Total Amount Sold");
                    for (int y = 0; y < mList.Count; y++)
                    {
                        if (totalSold[y].Item2 != 0)
                        {
                            Console.WriteLine("{0,-3} {1,-35} {2,-6:c2}", n, totalSold[y].Item1, totalSold[y].Item2);
                        }
                        else
                        {
                            continue;  // If no sales, don't display
                        }
                        n++;
                    }
                }
                else
                {
                    Console.WriteLine("No orders to recommend movies.");
                }  
            }

            // ------------------- 3.2) Display available seats of screening session in descending order -------------------
            static void DisplayAvailableSeats(List<Screening> sList)
            {
                sList.Sort(); //used IComparable 
                Console.WriteLine("\n{0,-27}{1,-18}{2,-28}{3,-19}{4,-22}{5,-17}{6,-20}", "Movie Title: ", "Screening No: ", "DateTime: ", "Screening Type: ", "Cinema Name: ", "Hall Number: ", "Seats Remaining: ");
                foreach (Screening s in sList)
                {
                    Console.WriteLine("{0,-27}{1,-18}{2,-28}{3,-19}{4,-22}{5,-17}{6,-20}",s.Movie.Title, s.ScreeningNo, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Cinema.HallNo, s.SeatsRemaining);
                }
            }

            // ------------------- 3.3) Top 3 movies based on number of tickets sold -------------------
            static void DisplayTop3(List<Movie> mList, List <Order> oList)
            {
                if (oList.Count != 0) //if thrs orders 
                {
                    List<Tuple<string, int>> seatsSold = new List<Tuple<string, int>>();
                    for (int x = 0; x < mList.Count; x++)
                    {
                        int totalCap = 0; 
                        int totalAvail = 0;
                        int totalSold = 0;
                        Movie m = mList[x];
                        foreach (Screening s in mList[x].screeningList)
                        {
                            totalCap += s.Cinema.Capacity;
                            totalAvail += s.SeatsRemaining;
                            totalSold = totalCap - totalAvail;
                        }
                        seatsSold.Add(new Tuple<string, int>(m.Title, totalSold));  // add movie title and total seats sold into a list
                    }

                    seatsSold.Sort((a, b) => a.Item2.CompareTo(b.Item2)); //sort based on total number of tickets sold 
                    seatsSold.Reverse();

                    Console.WriteLine("List of Recommended Movies");
                    int n = 1;
                    Console.WriteLine("\n    {0,-35} {1,-15}", "Title", "Tickets Sold");
                    for (int y = 0; y < 3; y++)
                    {
                        if (seatsSold[y].Item2 != 0)
                        {
                            Console.WriteLine("{0,-3} {1,-35} {2,-6}", n, seatsSold[y].Item1, seatsSold[y].Item2);
                            n++;
                        }

                    }

                    if (n != 4) //if theres not enough orders for three movies 
                    {
                        Console.WriteLine("\nNot enough orders to determine top 3.");
                    }
                }

                else
                {
                    Console.WriteLine("No orders to determine Top 3 Movies.");
                }
            }

            // ------------------- 3.3) Top sales chart of the Cinema Name -------------------
            static void SalesByCinema(List<Cinema> cList, List<Order> oList)
            {
                List<string> cinemaNames = new List<string>(); //list will contains cinema names 
                for (int c = 0; c < cList.Count; c++)
                {
                    if (cinemaNames.Contains(cList[c].Name)) //avoid adding duplicates 
                    {
                        continue;
                    }

                    else
                        cinemaNames.Add(cList[c].Name);  // add the unique cinema names into the list
                }

                List<Tuple<string, double>> SalesByCinema = new List<Tuple<string, double>>(); // create a list storing tuples that contain the cinemaName+salesAmount 

                for (int i = 0; i < cinemaNames.Count; i++)
                {
                    double salesAmount = 0;
                    foreach (Order o in oList)
                    {
                        if (o.TList[0].Screening.Cinema.Name == cinemaNames[i])  // find cinema names from orders
                        {
                            salesAmount += o.Amount;
                        }
                    }
                    SalesByCinema.Add(new Tuple<string, double>(cinemaNames[i], salesAmount));
                }

                SalesByCinema.Sort((a, b) => a.Item2.CompareTo(b.Item2)); //sort based on salesAmount
                SalesByCinema.Reverse(); //highest salesAmount will be first 

                Console.WriteLine("\nSales by Cinema");
                int n = 1;
                for (int j = 0; j < cinemaNames.Count; j++)  // display sales amount for all cinemas, even if sales is $0
                {
                    Console.WriteLine("{0,-3} {1,-20} {2,-6:c2}", n, SalesByCinema[j].Item1, SalesByCinema[j].Item2);
                    n++;
                }
            }
        }
    }
}