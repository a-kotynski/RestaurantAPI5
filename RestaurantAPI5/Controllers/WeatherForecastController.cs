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

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var result = _service.Get();
        //    return result;
        //}
        //[HttpGet("currentDay/{max}")]
        ////[Route("currentDay")] // creates different path do that action: https://localhost:5001/weatherforecast/currentday
        //public IEnumerable<WeatherForecast> Get2([FromQuery]int take, [FromRoute]int max)
        //{
        //    var result = _service.Get();
        //    return result;
        //}
        [HttpPost]
        public ActionResult<string> Hello([FromBody] string name)
        {
            //#1 option of specifying status code
            //HttpContext.Response.StatusCode = 401; 
            //return $"Hello {name}";

            //#2 another way is to return a result by calling a method StatusCode():
            //return StatusCode(401, $"Hello {name}");

            //#3 returning method specfically tied to a status code of Http:
            return NotFound($"Hello {name}");
        }
        [HttpPost("generate")]
        public ActionResult<int> Temperature([FromHeader] int returnedResultsCount, int minTemp, int maxTemp)
        {
            var result = _service.Get(returnedResultsCount, minTemp, maxTemp);

            if (returnedResultsCount > 0 && minTemp > 0 && maxTemp > 0 && minTemp > maxTemp)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}