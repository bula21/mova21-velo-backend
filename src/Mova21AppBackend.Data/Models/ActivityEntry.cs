using System.ComponentModel.DataAnnotations;

namespace Mova21AppBackend.Data.Models;

public class ActivityEntry
{
    public int Id { get; set; }
    public bool IsPermanent { get; set; }
    public string TitleDe { get; set; }
    public string TitleFr { get; set; }
    public string TitleIt { get; set; }
    public string LocationDe { get; set; }
    public string LocationFr { get; set; }
    public string LocationIt { get; set; }
    public string DescriptionDe { get; set; }
    public string DescriptionFr { get; set; }
    public string DescriptionIt { get; set; }
    public string OpeningHoursDe { get; set; }
    public string OpeningHoursFr { get; set; }
    public string OpeningHoursIt { get; set; }
    public string Category { get; set; }
    public DateTime? Date { get; set; }
}

public enum ActivityCategory
{
    WalkIn,
    Rover,
    WalkInAndRover
}