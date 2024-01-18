using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Rainfall_Api.Models;
using System.Diagnostics;
using System.Net;


namespace Rainfall_Api.Controllers
{
    public class HomeController : Controller
    {
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
                RainfallReading data = GetRainfallReadingsFromApi(stationId, count);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                var error = new Error();
                return StatusCode(500);
            }

        }

        private RainfallReading GetRainfallReadingsFromApi(int stationId, int count)
        {
            return new RainfallReading();
        }
    }
}
