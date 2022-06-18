using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mova21AppBackend.Data.RestModels;

public class BikesResponse
{
    [JsonPropertyName("data")]
    public IEnumerable<BikeResponseData>? Data { get; set; }
}

public class BikeResponse
{
    [JsonPropertyName("data")]
    public BikeResponseData? Data { get; set; }
}

public class BikeResponseData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("owner")]
    public int Owner { get; set; }

    [JsonPropertyName("created_on")]
    public DateTime CreatedOn { get; set; }

    [JsonPropertyName("modified_on")]
    public DateTime ModifiedOn { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("availablecount")]
    public int AvailableCount { get; set; }
}