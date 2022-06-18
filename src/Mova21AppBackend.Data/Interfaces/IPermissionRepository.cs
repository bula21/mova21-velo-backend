using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Interfaces;

public interface IPermissionRepository
{
    public Task<PermissionEntry> GetPermissionEntry();
}