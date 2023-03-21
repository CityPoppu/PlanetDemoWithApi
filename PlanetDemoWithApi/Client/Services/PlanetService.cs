using Microsoft.AspNetCore.Components;
using PlanetDemoWithApi.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;
using System.Web;
using static System.Net.WebRequestMethods;


namespace PlanetDemoWithApi.Client.Services
{
    public class PlanetService
    {
        public HttpClient SetupHttpClient(Uri baseUri)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = baseUri;
            return httpClient;
        }

        public async Task<List<Planet>> GetPlanetsAsync(Uri baseUri) 
        {
            var httpClient = SetupHttpClient(baseUri);
            return await httpClient.GetFromJsonAsync<List<Planet>>("Planet");
        }

        public async Task<Planet> GetPlanetByNameAsync(Uri baseUri, string name)
        {
            var httpClient = SetupHttpClient(baseUri);
            return await httpClient.GetFromJsonAsync<Planet>($"Planet/GetPlanetByName/{name}"); ;
        }

        public async Task<List<Planet>> GetPlanetsByPropertyAsync(Uri baseUri, string? classification, int? numberOfMoons)
        {
            var planets = new List<Planet>();
            var httpClient = SetupHttpClient(baseUri);

            if (!String.IsNullOrWhiteSpace(classification) && numberOfMoons != null)
            {
                planets = await httpClient.GetFromJsonAsync<List<Planet>>($"Planet/GetPlanetsFromProperty/?classification={HttpUtility.UrlEncode(classification)}&numberOfMoons={numberOfMoons}");
            }
            else if (!String.IsNullOrWhiteSpace(classification))
            {
                planets = await httpClient.GetFromJsonAsync<List<Planet>>($"Planet/GetPlanetsFromProperty/?classification={HttpUtility.UrlEncode(classification)}");
            }
            else if (numberOfMoons != null)
            {
                planets = await httpClient.GetFromJsonAsync<List<Planet>>($"Planet/GetPlanetsFromProperty/?numberOfMoons={numberOfMoons}");
            }

            return planets;
        }

    }
    
}
