using Microsoft.Extensions.Configuration;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.RestModels;
using RestSharp;

namespace Mova21AppBackend.Data.Storage;

public class DirectusBikeRepository : BaseDirectusRepository, IBikeRepository
{
    const string BikeUrl = "items/Bike";

    public DirectusBikeRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<BikeAvailability> GetBikeAvailabilityAsync()
    {
        var request = new RestRequest(BikeUrl);
        var response = await Client.ExecuteGetAsync<BikeResponse>(request);
        return new BikeAvailability
        {
            BikeTrailers = response.Data?.Data?.BikeTrailers ?? 0,
            CargoBikes = response.Data?.Data?.CargoBikes ?? 0,
            RegularBikes = response.Data?.Data?.RegularBikes ?? 0,
            Id = response.Data?.Data?.Id ?? 0,
            IsOpen = response.Data?.Data?.IsOpen ?? false,
        };
    }

    public async Task<BikeAvailability> UpdateBikeAvailabilityAsync(BikeAvailability model)
    {
        var patchRequest = new RestRequest(BikeUrl, Method.Patch)
            .AddJsonBody(new BikeResponseData
            {
                Id = model.Id,
                IsOpen = model.IsOpen,
                RegularBikes = model.RegularBikes,
                CargoBikes = model.CargoBikes,
                BikeTrailers = model.BikeTrailers
            });
        var patchResponse = await Client.ExecuteAsync<BikeResponse>(patchRequest);
        return new BikeAvailability
        {
            BikeTrailers = patchResponse.Data?.Data?.BikeTrailers ?? 0,
            CargoBikes = patchResponse.Data?.Data?.CargoBikes ?? 0,
            RegularBikes = patchResponse.Data?.Data?.RegularBikes ?? 0,
            Id = patchResponse.Data?.Data?.Id ?? 0,
            IsOpen = patchResponse.Data?.Data?.IsOpen ?? false,
        };
    }
}