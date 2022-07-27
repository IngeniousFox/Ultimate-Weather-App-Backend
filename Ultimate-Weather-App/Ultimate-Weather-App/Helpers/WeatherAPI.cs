using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Ultimate_Weather_App.Helpers
{
    public class WeatherAPI : IWeatherAPI
    {

        private string weatherAPIKey;
        private string weatherURL;

        public WeatherAPI(IConfiguration configuration)
        {
            weatherAPIKey = configuration.GetValue<string>("WeatherAPIKey");
            weatherURL = configuration.GetValue<string>("WeatherEndpoint");
        }

        public async Task<string> GetWeatherInformation(string latitude, string longitude, string units, string language)
        {
            HttpClient client = new HttpClient();
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                ["lat"] = latitude,
                ["lon"] = longitude,
                ["exclude"] = "minutely",
                ["appid"] = weatherAPIKey,
                ["units"] = units,
                ["lang"] = language,
            };

            string url = QueryHelpers.AddQueryString(weatherURL, parameters);

            HttpResponseMessage result = await client.GetAsync(url);
            string responseBody = await result.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async Task<IEnumerable<Weather>> GetTemperaturePrevision(string latitude, string longitude, string units, int hours)
        {
            List<Weather> temperature = new List<Weather>();
            List<Task<HttpResponseMessage>> responses = new List<Task<HttpResponseMessage>>();
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
                    ["appid"] = weatherAPIKey,
                    ["units"] = units
                };

                string url = QueryHelpers.AddQueryString(weatherURL + "/timemachine", parameters);

                Task<HttpResponseMessage> result = client.GetAsync(url);
                responses.Add(result);
            }

            await Task.WhenAll(responses);
            foreach (var response in responses)
            {
                string responseBody = await response.Result.Content.ReadAsStringAsync();
                temperature.Add(JsonConvert.DeserializeObject<Weather>(responseBody));
            }

            return temperature;
        }
    }
}
