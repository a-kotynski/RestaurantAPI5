using System.Collections.Generic;

namespace RestaurantAPI5
{
    public interface IWeatherForecastService
    {
        //IEnumerable<WeatherForecast> Get();
        IEnumerable<WeatherForecast> Get(int returnedResultsCount, int minTemp, int maxTemp);
    }
}