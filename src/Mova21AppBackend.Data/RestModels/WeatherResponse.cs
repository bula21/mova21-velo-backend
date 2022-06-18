using System.Text.Json.Serialization;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.RestModels;

public class WeatherEntriesResponse
{
    [JsonPropertyName("data")]
    public IEnumerable<WeatherEntryResponseData>? Data { get; set; }
}

public class WeatherEntryResponse
{
    [JsonPropertyName("data")]
    public WeatherEntryResponseData? Data { get; set; }
}

public class WeatherEntryResponseData
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

    [JsonPropertyName("weather")]
    public WeatherType Weather { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("daytime")]
    public DayTime DayTime { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    public WeatherEntry ToWeatherEntry()
    {
        return new WeatherEntry
        {
            Date = Date,
            DayTime = DayTime,
            Id = Id,
            Temperature = Temperature,
            Weather = Weather,
        };
    }
}

public class WeatherEntryUpdateData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("weather")]
    public WeatherType Weather { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("daytime")]
    public DayTime DayTime { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    public static WeatherEntryUpdateData FromWeatherEntry(WeatherEntry entry)
    {
        return new WeatherEntryUpdateData
        {
            Date = entry.Date,
            DayTime = entry.DayTime,
            Id = entry.Id,
            Temperature = entry.Temperature,
            Weather = entry.Weather,
        };
    }
}
