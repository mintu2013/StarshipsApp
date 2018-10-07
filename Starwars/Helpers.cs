using System;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Starwars
{
    public static class Helpers
    {
        // This method validates input string to check if a valid positive decimal number
        public static bool ValidateInput(string input)
        {
            return Regex.IsMatch(StarshipManager.Distance, @"^\d+$") && long.TryParse(StarshipManager.Distance, out long n);
        }
    }
}
