using System.ComponentModel.DataAnnotations;

namespace Mova21AppBackend.Data.Models
{
    public class WeatherEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DayTime DayTime { get; set; }
        public double Temperature { get; set; }
        public WeatherType Weather { get; set; }
    }

    public enum WeatherType
    {
        Cloud,
        CloudSun,
        CloudSunRain,
        CloudRain,
        Sun,
        Thunderstorm,
        Fog,
        Snow
    }

    public enum DayTime
    {
        Morning,
        Midday,
        Evening,
        Night
    }
}
