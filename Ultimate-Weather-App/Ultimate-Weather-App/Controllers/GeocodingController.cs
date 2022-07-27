using Microsoft.AspNetCore.Mvc;
using Ultimate_Weather_App.Helpers;

namespace Ultimate_Weather_App.Controllers
{
    [ApiController]
    [Route("api")]
    public class GeocodingController : ControllerBase
    {

        private readonly IGeocodingAPI geocodingAPI;

        public GeocodingController(IGeocodingAPI geocodingAPI)
        {
            this.geocodingAPI = geocodingAPI;
        }

        [HttpGet]
        [Route("Geocoding")]
        public async Task<IEnumerable<Geocoding>> getCoordinatesFromCity([FromQuery] string query, [FromQuery] int limit = 4)
        {
            var result = await geocodingAPI.GetCoordinates(query, limit);
            return result;
        }
    }
}
