
using System.Text.Json.Serialization;

public class PermissionResponse
{
    [JsonPropertyName("data")]
    public PermissionResponseData Data { get; set; }
}

public class PermissionResponseData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("user_created")]
    public string UserCreated { get; set; }
    [JsonPropertyName("date_created")]
    public DateTime DateCreated { get; set; }
    [JsonPropertyName("user_updated")]
    public object UserUpdated { get; set; }
    [JsonPropertyName("date_updated")]
    public object DateUpdated { get; set; }
    [JsonPropertyName("bike_editors")]
    public string BikeEditors { get; set; }
    [JsonPropertyName("weather_editors")]
    public string WeatherEditors { get; set; }
}
