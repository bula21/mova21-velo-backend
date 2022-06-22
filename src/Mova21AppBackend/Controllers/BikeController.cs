﻿using System.Threading.Tasks;
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
        public async Task<BikeAvailabilities> Get()
        {
            return await _bikeRepository.GetBikeAvailabilitiesAsync();
        }

        [HttpPut("")]
        public async Task<BikeAvailabilities> AdjustCount(ChangeBikeAvailabilityCountModel changeBikeAvailabilityCountModel)
        {
            await _bikeRepository.ChangeBikeAvailabilityAsync(changeBikeAvailabilityCountModel);
            return await _bikeRepository.GetBikeAvailabilitiesAsync();
        }
    }
}
