using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Interfaces;

public interface IActivityRepository
{
    Task<ActivityEntry> CreateActivityEntry(ActivityEntry model);
}