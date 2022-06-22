
using System.Text.Json.Serialization;
using Mova21AppBackend.Data.Models;

public class ActivityResponse
{
    [JsonPropertyName("data")]
    public IEnumerable<ActivityData>? Data { get; set; }
}

public class ActivityData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("user_created")]
    public Guid UserCreated { get; set; }
    [JsonPropertyName("date_created")]
    public DateTime DateCreated { get; set; }
    [JsonPropertyName("user_updated")]
    public Guid UserUpdated { get; set; }
    [JsonPropertyName("date_updated")]
    public DateTime? DateUpdated { get; set; }
    [JsonPropertyName("is_permanent")]
    public bool IsPermanent { get; set; }
    [JsonPropertyName("title_de")]
    public string TitleDe { get; set; }
    [JsonPropertyName("title_fr")]
    public string TitleFr { get; set; }
    [JsonPropertyName("title_it")]
    public string TitleIt { get; set; }
    [JsonPropertyName("location_de")]
    public string LocationDe { get; set; }
    [JsonPropertyName("location_fr")]
    public string LocationFr { get; set; }
    [JsonPropertyName("location_it")]
    public string LocationIt { get; set; }
    [JsonPropertyName("description_de")]
    public string DescriptionDe { get; set; }
    [JsonPropertyName("description_fr")]
    public string DescriptionFr { get; set; }
    [JsonPropertyName("description_it")]
    public string DescriptionIt { get; set; }
    [JsonPropertyName("opening_hours_de")]
    public string OpeningHoursDe { get; set; }
    [JsonPropertyName("opening_hours_fr")]
    public string OpeningHoursFr { get; set; }
    [JsonPropertyName("opening_hours_it")]
    public string OpeningHoursIt { get; set; }
    [JsonPropertyName("category")]
    public string Category { get; set; }
    [JsonPropertyName("date")]
    public DateTime? Date { get; set; }

    public ActivityEntry ToActivityEntry()
    {
        return new ActivityEntry
        {
            Id = Id,
            IsPermanent = IsPermanent,
            TitleDe = TitleDe,
            TitleFr = TitleFr,
            TitleIt = TitleIt,
            LocationDe = LocationDe,
            LocationFr = LocationFr,
            LocationIt = LocationIt,
            DescriptionDe = DescriptionDe,
            DescriptionFr = DescriptionFr,
            DescriptionIt = DescriptionIt,
            OpeningHoursDe = OpeningHoursDe,
            OpeningHoursFr = OpeningHoursFr,
            OpeningHoursIt = OpeningHoursIt,
            Category = Category switch
            {
                "rover" => ActivityCategory.Rover,
                "all" => ActivityCategory.Both,
                "walk-in" => ActivityCategory.WalkIn,
                _ => ActivityCategory.Unknown
            },
            Date = Date
        };
    }
}
