using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvc.Models;

namespace TodoListClient.Services
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GetAsync();
    }
}
