namespace Ultimate_Weather_App
{
    public class Weather
    {
        public string lat { get; set; }
        public string lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public TemperatureData[] data { get; set; }
    }

    public class TemperatureData
    {
        public long dt { get; set; }
        public float temp { get; set; }
    }
}
