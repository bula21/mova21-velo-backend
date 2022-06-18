using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        [HttpGet("{startDate:datetime}/{endDate:datetime}")]
        public async Task<WeatherEntries> Get(DateTime startDate, DateTime endDate)
        {
            return await _weatherRepository.GetWeatherEntriesByDateRange(startDate, endDate);
        }

        [HttpPut]
        public async Task Update(WeatherEntry model)
        {
            await _weatherRepository.UpdateWeatherEntry(model);
        }

        [HttpPost]
        public async Task<WeatherEntry> Create(WeatherEntry model)
        {
            return await _weatherRepository.CreateWeatherEntry(model);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _weatherRepository.DeleteWeatherEntry(id);
        }
    }
}
