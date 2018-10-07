using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Starwars
{
    /* 
     StarshipManager class manage Starship related operations
    */
    public static class StarshipManager
    {
        public static List<Starship> starships;
        private static string distance = String.Empty;

        public static string Distance
        {
            get
            {
                return distance;
            }

            set
            {
                distance = value;
            }
        }

        public static string GetStarships()
        {
            StarshipServices services = new StarshipServices();
            starships = services.GetStarshipAsync().Result;

            foreach (var starship in starships)
                starship.ResupplyFrequency = CalculateNumberofResupply(starship);
            
            starships = starships.OrderBy(x => x.Name).ToList();
            var res = String.Concat(starships.Select(o => o.Name + ":" + o.ResupplyFrequency + '\n'));
            return res;
        }


        public static string CalculateNumberofResupply(Starship starship)
        {
            // to filter unknown
            if (!Regex.IsMatch(starship.MGLT, @"^\d+$"))
                return starship.MGLT;

            DateTime travelStart = DateTime.Now;
            DateTime travelEnd = GetTravelEnd(starship.Consumables);

            int minutes = (int)(travelEnd - travelStart).TotalMinutes;
            TimeSpan time = TimeSpan.FromMinutes(minutes);
            double numHours = time.TotalHours;

            //calculate number of resupplies needed=total distance/(total hours per MGLTx MGLT per hour )and round to nearest integer
            return (Convert.ToInt64(Convert.ToInt64(Distance) / (numHours * Convert.ToInt64(starship.MGLT)))).ToString();
        }


        public static DateTime GetTravelEnd(string consumable)
        {
            DateTime travelEnd = DateTime.Now;

            string[] consumables = consumable.Split(' ');
            string timeLength = string.Empty;

            if (consumables.Length > 1)
                timeLength = consumables[1].ToLower();

            TimeLength length = (TimeLength)Enum.Parse(typeof(TimeLength), timeLength);

            switch (length)
            {
                case TimeLength.day:
                case TimeLength.days:
                    travelEnd = travelEnd.AddDays(Convert.ToDouble(consumables[0]));
                    break;
                case TimeLength.week:
                case TimeLength.weeks:
                    travelEnd = travelEnd.AddDays(Convert.ToDouble(consumables[0]) * 7);
                    break;
                case TimeLength.month:
                case TimeLength.months:
                    travelEnd = travelEnd.AddMonths(Convert.ToInt32(consumables[0]));
                    break;
                case TimeLength.year:
                case TimeLength.years:
                    travelEnd = travelEnd.AddYears(Convert.ToInt32(consumables[0]));
                    break;
            }

            return travelEnd;
        }
    }
}
