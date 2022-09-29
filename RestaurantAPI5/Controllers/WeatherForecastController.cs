using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI5.Controllers
{ 
    [ApiController] 
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service; //direct dependency causes a strict tie to that implementation, which means that during tests it's impossible to mock the results

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            //_service = new WeatherForecastService();
            _service = service;
        }



        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery] int returnedResultsCount, int minTemp, int maxTemp)
        {
            var result = _service.Get(returnedResultsCount, minTemp, maxTemp);

            if (returnedResultsCount > 0 && minTemp > 0 && maxTemp > 0 && minTemp < maxTemp)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}