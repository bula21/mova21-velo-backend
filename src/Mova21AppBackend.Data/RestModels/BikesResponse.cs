using System.Text.Json.Serialization;

namespace Mova21AppBackend.Data.RestModels;

public class BikeResponse
{
    [JsonPropertyName("data")]
    public BikeResponseData? Data { get; set; }
}

public class BikeResponseData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("is_open")]
    public bool IsOpen { get; set; }

    [JsonPropertyName("regular_bikes")]
    public int RegularBikes { get; set; }

    [JsonPropertyName("cargo_bikes")]
    public int CargoBikes { get; set; }

    [JsonPropertyName("bike_trailers")]
    public int BikeTrailers { get; set; }
}