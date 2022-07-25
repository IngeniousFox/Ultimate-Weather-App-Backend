using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

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
        public async Task<IEnumerable<Weather>> GetTemperaturePrevision(string latitude, string longitude, string units, int hours)
        {
            List<Weather> temperature = new List<Weather>();
            DateTime currentDate = DateTime.Now;

            hours = (hours > 24) ? 24 : hours;

            for (int i = 0; i < hours; i++)
            {
                HttpClient client = new HttpClient();
                DateTime desiredDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0);
                desiredDate = desiredDate.AddHours(i);
                long desiredTimeStamp = ((DateTimeOffset)desiredDate).ToUnixTimeSeconds();
                Dictionary<string, string> parameters = new Dictionary<string, string>()
                {
                    ["lat"] = latitude,
                    ["lon"] = longitude,
                    ["dt"] = desiredTimeStamp.ToString(),
                    ["appid"] = APIKey,
                    ["units"] = units
                };

                string url = QueryHelpers.AddQueryString(WeatherURL + "/timemachine", parameters);

                HttpResponseMessage result = await client.GetAsync(url);
                string responseBody = await result.Content.ReadAsStringAsync();
                temperature.Add(JsonConvert.DeserializeObject<Weather>(responseBody));
            }

            return temperature;
        }
    }
}
