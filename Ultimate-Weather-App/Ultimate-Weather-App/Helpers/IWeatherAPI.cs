namespace Ultimate_Weather_App.Helpers
{
    public interface IWeatherAPI
    {
        public Task<string> GetWeatherInformation(string latitude, string longitude, string units, string language);
    }
}
