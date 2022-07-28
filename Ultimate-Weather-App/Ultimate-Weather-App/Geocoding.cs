namespace Ultimate_Weather_App
{
    public class Geocoding
    {
        public Guid Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Label { get; set; }

        public Geocoding()
        {
            Id = Guid.NewGuid();
        }
    }
}
