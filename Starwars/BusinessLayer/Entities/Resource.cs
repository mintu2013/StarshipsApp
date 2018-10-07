using Newtonsoft.Json;
using System.Collections.Generic;

namespace Starwars
{
    //This class holds SwapiApi Starship resource data
    public class Resource
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }

        [JsonProperty("results")]
        public List<Starship> Starships { get; set; }
    }
}
