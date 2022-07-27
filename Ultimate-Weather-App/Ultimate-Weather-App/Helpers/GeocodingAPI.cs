using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace Ultimate_Weather_App.Helpers
{
    public class GeocodingAPI : IGeocodingAPI
    {

        private string geocodingAPIKey;
        private string geocodingURL;

        public GeocodingAPI(IConfiguration configuration)
        {
            geocodingAPIKey = configuration.GetValue<string>("GeocodingAPIKey");
            geocodingURL = configuration.GetValue<string>("GeocodingEndpoint");
        }

        public async Task<IEnumerable<Geocoding>> GetCoordinates(string query, int limit)
        {
            HttpClient client = new HttpClient();
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                ["access_key"] = geocodingAPIKey,
                ["query"] = query,
                ["limit"] = limit.ToString()
            };

            string url = QueryHelpers.AddQueryString(geocodingURL, parameters);

            HttpResponseMessage result = await client.GetAsync(url);
            string responseBody = await result.Content.ReadAsStringAsync();

            JArray responeJSON = (JArray)JObject.Parse(responseBody)["data"];

            return responeJSON.ToObject<List<Geocoding>>();
        }
    }
}
