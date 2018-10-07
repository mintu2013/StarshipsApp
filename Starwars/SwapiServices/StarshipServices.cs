using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Starwars
{
    /* Service class for starships*/
    public class StarshipServices 
    {
        public string url = System.Configuration.ConfigurationManager.AppSettings["RootStarships"];

        /*This method extablishes asynchronous communication with SwapiApi and get Starship data.
         Since there is no pagination in client side, this method tries to build full list of objects here. 
        */
        public async Task<List<Starship>> GetStarshipAsync()
        {
            List<Starship> starships = new List<Starship>();
            using (var client = new HttpClient())
            {
                while (!string.IsNullOrEmpty(url))
                {
                    await client.GetAsync(url)
                   .ContinueWith(async (getstarships) =>
                   {
                       var response = await getstarships;
                       if (response.IsSuccessStatusCode)
                       {
                           string asyncResponse = await response.Content.ReadAsStringAsync();
                           var result = JsonConvert.DeserializeObject<Resource>(asyncResponse);
                           if (result != null)
                           {
                               if (result.Starships.Any())
                                   starships.AddRange(result.Starships.ToList());

                               url = result.Next ?? string.Empty;
                           }
                       }
                       else
                       {
                           url = string.Empty;
                       }
                   });
                }
            }
            return starships;
        }
    }
}
