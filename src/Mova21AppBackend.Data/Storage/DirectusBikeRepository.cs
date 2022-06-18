using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.RestModels;
using RestSharp;

namespace Mova21AppBackend.Data.Storage
{
    public class DirectusBikeRepository : BaseDirectusRepository, IBikeRepository
    {
        const string BikeUrl = "items/bike";

        public DirectusBikeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<BikeAvailabilities> GetBikeAvailabilitiesAsync()
        {
            var request = new RestRequest(BikeUrl);
            var response = await Client.ExecuteGetAsync<BikesResponse>(request);
            return new BikeAvailabilities
            {
                Availabilities = response.Data.Data?.Select(x => new BikeAvailability
                {
                    AvailableCount = x.AvailableCount,
                    Id = x.Id,
                    Type = x.Type ?? ""
                })

            };
        }

        public async Task<BikeAvailability> ChangeBikeAvailabilityAsync(ChangeBikeAvailabilityCountModel model)
        {
            var getRequest = new RestRequest($"{BikeUrl}/{model.Id}", Method.Get);
            var getResponse = await Client.ExecuteAsync<BikeResponse>(getRequest);

            var patchRequest = new RestRequest($"{BikeUrl}/{model.Id}", Method.Patch)
                .AddJsonBody(new
                {
                    availablecount = (getResponse.Data.Data?.AvailableCount ?? 0) + model.AmountChange
                });
            var patchResponse = await Client.ExecuteAsync<BikeResponse>(patchRequest);
            return new BikeAvailability
            {
                AvailableCount = patchResponse.Data.Data?.AvailableCount ?? 0,
                Id = patchResponse.Data.Data?.Id ?? 0,
                Type = patchResponse.Data.Data?.Type ?? ""
            };
        }
    }
}
