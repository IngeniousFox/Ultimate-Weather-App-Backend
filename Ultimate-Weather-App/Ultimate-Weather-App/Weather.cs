namespace Ultimate_Weather_App
{
    public class Weather
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Timezone { get; set; }
        public int Timezone_Offset { get; set; }
        public TemperatureData[] Data { get; set; }
    }

    public class TemperatureData
    {
        public long Dt { get; set; }
        public float Temp { get; set; }
    }
}
