using System.Collections.Generic;
using Zanis.Ostad.Web.Models;

namespace Zanis.Ostad.Web.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
