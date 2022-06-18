using System.Collections.Generic;

namespace Mova21AppBackend.Data.Models
{
    public class WeatherEntries
    {
        public IEnumerable<WeatherEntry>? Entries { get; set; }
    }
}
