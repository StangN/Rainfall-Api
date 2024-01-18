using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Rainfall_Api.Models;
using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Linq;

namespace Rainfall_Api.Controllers
{
    public class HomeController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("rainfall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Ra1infall(int stationId, [FromQuery] int count = 10)
        {
            return Ok();
        }

        [HttpGet("rainfall/id/{stationId}/readings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Rainfall(int stationId, [FromQuery] int count = 10)
        {
            try
            {
                if (stationId == null) return BadRequest();
                RainfallReadingResponse data = await GetRainfallReadingsFromApi(stationId, count);
                if (data.Readings.Count == 0) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                var error = new Error();
                return StatusCode(500);
            }

        }

        private async Task<RainfallReadingResponse> GetRainfallReadingsFromApi(int stationId, int count)
        {
            RainfallReadingResponse parsedResponce = new RainfallReadingResponse { Readings = new List<RainfallReading>() };

            string QUERY_URL = "https://environment.data.gov.uk/flood-monitoring/id/stations/" + stationId + "/readings?_sorted&_limit=" + count;
            Uri queryUri = new Uri(QUERY_URL);
            using HttpResponseMessage responce = await client.GetAsync(queryUri);
            dynamic obj = JsonConvert.DeserializeObject(await responce.Content.ReadAsStringAsync());
            foreach (var item in obj.items)
            {
                var parsedItem = new RainfallReading { DateMeasured = item.dateTime, AmountMeasured = item.value };
                if (parsedItem != null)
                {
                    parsedResponce.Readings.Add(parsedItem);
                }
            }
            
            return parsedResponce;
        }
    }
}
