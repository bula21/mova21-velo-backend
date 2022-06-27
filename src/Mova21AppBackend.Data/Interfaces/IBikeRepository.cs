using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.RestModels;

namespace Mova21AppBackend.Data.Storage
{
    public interface IBikeRepository
    {
        Task<BikeAvailability> GetBikeAvailabilityAsync();
        Task<BikeAvailability> UpdateBikeAvailabilityAsync(BikeAvailability model);
    }
}
