using System;

namespace Starwars
{
    public partial class StarwarsApp
    {
        public static void Main(string[] args)
        {
            Console.Title = typeof(StarwarsApp).Name;
            Run();
        }

        static void Run()
        {
            
            do
            {
                Console.Clear();
                DisplayOutput("\nPlease enter distance to travel in MGLT: ");
                StarshipManager.Distance = Console.ReadLine();

                if (!Helpers.ValidateInput(StarshipManager.Distance))
                    DisplayOutput("\nPlease enter a positive number for the distance to travel.. ");

                else
                {
                    try
                    {
                        DisplayOutput("\nPlease wait...  retrieving starships data...\n");
                        string result = ProcessInput();
                        DisplayOutput(result);
                    }
                    catch (Exception ex)
                    {
                        DisplayOutput(ex.Message);
                    }
                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        static string ProcessInput()
        {
            return StarshipManager.GetStarships();
        }

        public static void DisplayOutput(string message = null)
        {
            if (message != null && message.Length > 0)
            {
                Console.WriteLine(message);
            }
        }
    }
}

