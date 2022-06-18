using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Interfaces
{
    public interface IWeatherRepository
    {
        public Task<WeatherEntries> GetWeatherEntriesByDateRange(DateTime startDate, DateTime endDate);
        public Task UpdateWeatherEntry(WeatherEntry weatherEntry);
        Task DeleteWeatherEntry(int id);
        Task<WeatherEntry> CreateWeatherEntry(WeatherEntry model);
    }
}
