using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.Storage;

namespace Mova21AppBackend.Controllers
{
    [Authorize(PolicyNames.Bike)]
    [ApiController]
    [Route("api/[controller]")]
    public class BikeController : Controller
    {
        private readonly IBikeRepository _bikeRepository;

        public BikeController(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        [HttpGet]
        public async Task<BikeAvailability> Get()
        {
            return await _bikeRepository.GetBikeAvailabilityAsync();
        }

        [HttpPut]
        public async Task<BikeAvailability> Update(BikeAvailability updatedBikeAvailability)
        {
            await _bikeRepository.UpdateBikeAvailabilityAsync(updatedBikeAvailability);
            return await _bikeRepository.GetBikeAvailabilityAsync();
        }
    }
}
