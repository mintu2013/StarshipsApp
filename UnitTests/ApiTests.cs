using System;
using Starwars;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UnitTests
{
    [TestClass]
    public class ApiTests
    {
        [TestMethod]
        public async Task GetStarshipDataFromApi()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://swapi.co/api/starships/")
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(client.BaseAddress.ToString()),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await client.SendAsync(request))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

    }
}

