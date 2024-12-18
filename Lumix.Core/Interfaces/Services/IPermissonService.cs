using Lumix.Core.Enums;

namespace Lumix.Core.Interfaces;


public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}