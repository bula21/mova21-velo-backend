using Microsoft.Extensions.Configuration;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;
using RestSharp;

namespace Mova21AppBackend.Data.Storage;

public class DirectusPermissionRepository : BaseDirectusRepository, IPermissionRepository
{
    const string PermissionUrl = "items/Permission";

    public DirectusPermissionRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<PermissionEntry> GetPermissionEntryAsync()
    {
        var request = new RestRequest(PermissionUrl);
        var response = await Client.ExecuteGetAsync<PermissionResponse>(request);

        var permissionResponseData = response.Data?.Data;
        return new PermissionEntry
        {
            ActivityEditors = permissionResponseData?.ActivityEditors?.Split(';', ',').ToList() ?? new List<string>(),
            BikeEditors = permissionResponseData?.BikeEditors?.Split(';', ',').ToList() ?? new List<string>(),
            WeatherEditors = permissionResponseData?.WeatherEditors?.Split(';', ',').ToList() ?? new List<string>()
        };
    }
}