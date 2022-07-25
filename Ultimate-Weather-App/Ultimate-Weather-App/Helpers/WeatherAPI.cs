using Microsoft.AspNetCore.WebUtilities;

namespace Ultimate_Weather_App.Helpers
{
    public class WeatherAPI : IWeatherAPI
    {

        private string APIKey;
        private string WeatherURL;

        public WeatherAPI(IConfiguration configuration)
        {
            APIKey = configuration.GetValue<string>("APIKey");
            WeatherURL = configuration.GetValue<string>("Endpoint");
        }

        public async Task<string> GetWeatherInformation(string latitude, string longitude, string units, string language)
        {
            HttpClient client = new HttpClient();
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                ["lat"] = latitude,
                ["lon"] = longitude,
                ["exclude"] = "minutely",
                ["appid"] = APIKey,
                ["units"] = units,
                ["lang"] = language,
            };

            string url = QueryHelpers.AddQueryString(WeatherURL, parameters);

            HttpResponseMessage result = await client.GetAsync(url);
            string responseBody = await result.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
