namespace Ultimate_Weather_App.Helpers
{
    public interface IGeocodingAPI
    {
        public Task<IEnumerable<Geocoding>> GetCoordinates(string query, int limit);
    }
}
