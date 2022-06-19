using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Interfaces;

public interface IActivityRepository
{
    Task<WeatherEntry> CreateActivityEntry(ActivityEntry model);
}