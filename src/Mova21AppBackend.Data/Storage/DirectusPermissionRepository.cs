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

    public async Task<PermissionEntry> GetPermissionEntry()
    {
        var request = new RestRequest(PermissionUrl);
        var response = await Client.ExecuteGetAsync<PermissionResponse>(request);

        var permissionResponse = response.Data ?? throw new ArgumentNullException();
        return new PermissionEntry
        {
            BikeEditors = permissionResponse.Data.BikeEditors.Split(';', ',').ToList(),
            WeatherEditors = permissionResponse.Data.WeatherEditors.Split(';', ',').ToList()
        };
    }
}