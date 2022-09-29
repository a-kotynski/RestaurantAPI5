using System.Collections.Generic;
using System.Linq;
using System;

namespace RestaurantAPI5
{
    public class WeatherForecastService : IWeatherForecastService

    // all logic regarding returning the results of the weather forecasts has been transfered to WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public IEnumerable<WeatherForecast> Get(int returnedResultsCount, int minTemp, int maxTemp)// int returnedResults, int minTemp, int maxTemp
        {
            var rng = new Random();
            try
            {
                return Enumerable.Range(1, returnedResultsCount).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(minTemp, maxTemp + 1),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                    .ToArray();
            }
            catch (Exception)
            {
                throw; 
            }
        }
    }
}