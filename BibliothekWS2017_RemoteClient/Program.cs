using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliothekWS2017_RemoteClient.Media;

namespace BibliothekWS2017_RemoteClient
{

    class Program
    {
        private static RestClientController _controller;
        private static bool _userLoggedIn;

        static void Main(string[] args)
        {
            string url = "";

            //Check for command line arguments
            if (args.Length < 2)
            {
                Console.Write("Please enter the URL to the server: ");
                url = Console.ReadLine();
            }
            else
            {
                if (args[0].Contains("-url"))
                {
                    url = args[1];
                }
            }

            //Try to establish connection
            try
            {
                _controller = new RestClientController(url, "application/json");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connect to server faild.");
                return;
            }

            //Load welcome screen
            PrintWelcome();
            //Load the menu. Menu is also the main loop
            Menu();
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("##########################################");
            Console.WriteLine("#                                        #");
            Console.WriteLine("#            Welcome to Library          #");
            Console.WriteLine("#                                        #");
            Console.WriteLine("##########################################");
        }

        private static void Menu()
        {
            Console.WriteLine("#####################################");
            Console.WriteLine("#                Menu               #");
            Console.WriteLine("#####################################");

            bool exitApplication = false;
            string input = "";

            while (!exitApplication)
            {
                Console.WriteLine("Possible actions: ");
                Console.WriteLine("           - Enter \"b\" to search for books");
                Console.WriteLine("           - Enter \"d\" to search for dvds");
                Console.WriteLine("           - Enter \"l\" to log in");
                Console.WriteLine("           - Enter \"e\" to exit");
                input = Console.ReadLine();
                if (input.Contains("b") || input.Contains("d") || input.Contains("l"))
                {
                    switch (input)
                    {
                        case "b":
                            SearchForBook();
                            break;
                        case "d":
                            SearchForDvd();
                            break;
                        case "l":
                            Login();
                            break;
                        default:
                            Console.WriteLine("Wrong input!");
                            break;
                    }
                }
                else if (input.Contains("e"))
                {
                    exitApplication = true;
                }
            }
        }

        private static void MenuLoggedIn()
        {
            Console.WriteLine("#####################################");
            Console.WriteLine("#                Menu               #");
            Console.WriteLine("#####################################");

            string input = "";

            while (_userLoggedIn)
            {
                Console.WriteLine("Possible actions: ");
                Console.WriteLine("           - Enter \"b\" to search for books");
                Console.WriteLine("           - Enter \"d\" to search for dvds");
                Console.WriteLine("           - Enter \"r\" to rent a copy");
                Console.WriteLine("           - Enter \"l\" to log out");
                input = Console.ReadLine();
                if (input.Contains("b") || input.Contains("d") || input.Contains("l") || input.Contains("r"))
                {
                    switch (input)
                    {
                        case "b":
                            SearchForBook();
                            break;
                        case "d":
                            SearchForDvd();
                            break;
                        case "r":
                            RentCopy();
                            break;
                        case "l":
                            _userLoggedIn = false;
                            break;
                        default:
                            Console.WriteLine("Wrong input!");
                            break;
                    }
                }
            }
        }

        private static void SearchForBook()
        {
            Console.WriteLine("#####################################");
            Console.WriteLine("#         Search for book           #");
            Console.WriteLine("#####################################");

            //Request search information from user
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();
            Console.Write("Isbn: ");
            string isbn = Console.ReadLine();

            //Create book with information to search
            Book book = new Book();
            book.title = title;
            book.author = author;
            book.isbn = isbn;

            //Request to the webservice
            Book[] books = _controller.SearchForBook(book);

            //Check if there are any matches
            if (books == null || books.Length == 0)
            {
                Console.WriteLine("###########   No matches found!   ###########");
            }
            else
            {
                Console.WriteLine("###########   Results   ###########\n");
                foreach (Book resultBook in books)
                {
                    Console.WriteLine(resultBook.ToString());
                    Console.Write("Press [Enter] to view next book.");
                    Console.ReadLine();
                    Console.WriteLine();
                }
            }
        }

        private static void SearchForDvd()
        {
            Console.WriteLine("#####################################");
            Console.WriteLine("#           Search for dvd          #");
            Console.WriteLine("#####################################");

            //Request search information from user
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Director: ");
            string director = Console.ReadLine();
            Console.Write("Asin: ");
            string asin = Console.ReadLine();

            //Create dvd with information to search
            Dvd dvd = new Dvd();
            dvd.title = title;
            dvd.director = director;
            dvd.asin = asin;

            //Request to the webservice
            Dvd[] dvds = _controller.SearchForDvd(dvd);

            //Check if there are any matches
            if (dvds == null || dvds.Length == 0)
            {
                Console.WriteLine("###########   No matches found!   ###########");
            }
            else
            {
                Console.WriteLine("###########   Results   ###########\n");
                foreach (Dvd resultDvd in dvds)
                {
                    Console.WriteLine(resultDvd.ToString());
                    Console.Write("Press [Enter] to view next dvd.");
                    Console.ReadLine();
                    Console.WriteLine();
                }
            }
        }

        private static void Login()
        {
            Console.WriteLine("#####################################");
            Console.WriteLine("#                Menu               #");
            Console.WriteLine("#####################################");

            Console.Write("User: ");
            string user = Console.ReadLine();

            Console.Write("Password: ");
            //Do not show entered characters of password
            StringBuilder password = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    //Fill with blank
                    Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        password.Length--;
                    }

                    continue;
                }

                Console.Write('*');
                password.Append(cki.KeyChar);
            }

            bool success = _controller.Login(user, password.ToString());

            _userLoggedIn = true;
            MenuLoggedIn();
        }

        private static void RentCopy()
        {
            Console.WriteLine("#####################################");
            Console.WriteLine("#             Rent copy             #");
            Console.WriteLine("#####################################");

            //Request information to rent copy
            Console.Write("Customer number: ");
            string customerNumber = Console.ReadLine();
            Console.Write("Copy number: ");
            string copyNumber = Console.ReadLine();

            //Perform rent request
            String result = _controller.RentMedium(customerNumber, copyNumber);

            Console.WriteLine("Rent status: " + result);
        }
    }
}
